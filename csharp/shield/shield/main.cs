using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Management;

using shield.Properties;
using System.Security.Principal;

namespace shield
{
    public partial class main : Form
    {

        static SerialPort serial_connection = null;
        WmiChangeEventTester receiveEvent = null;
        static System.Windows.Forms.NotifyIcon myNotifyIcon;

        ColorDialog colorDialog = new ColorDialog();

        public class WmiChangeEventTester
        {
            ManagementEventWatcher _watcher = null;
            long _lastNotification = 0;

            main _mainForm;

            public WmiChangeEventTester(main mainForm)
            {
                try
                {
                    _mainForm = mainForm;

                    WindowsIdentity identity = null;
                    identity = WindowsIdentity.GetCurrent();
                    string cameraPath = identity.User.ToString() + @"\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\CapabilityAccessManager\\ConsentStore\\webcam";
                    string micPath = identity.User.ToString() + @"\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\CapabilityAccessManager\\ConsentStore\\microphone";
                    string qry = String.Format("SELECT * FROM RegistryTreeChangeEvent  WHERE Hive = \"HKEY_USERS\" AND (Rootpath = \"{0}\" OR Rootpath = \"{1}\")", cameraPath, micPath);
                    Console.WriteLine(qry);
                    WqlEventQuery query = new WqlEventQuery(qry);

                    _watcher = new ManagementEventWatcher(query);

                    _watcher.EventArrived += new EventArrivedEventHandler(HandleEvent);
                    _watcher.Start();
                }
                catch (ManagementException managementException)
                {
                    Console.WriteLine("An error occurred: " + managementException.Message);
                }
            }

            public void Stop()
            {
                // Stop listening for events.
                if (_watcher != null)
                    _watcher.Stop();
            }
            private void HandleEvent(object sender, EventArrivedEventArgs e)
            {
                // Since two values are changing (LastUsedTimeStart & LastUsedTimeStop) we filter out the second notification
                long now = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
                if (now - _lastNotification > 100)
                {
                    _mainForm.Invoke(new Action(() => {
                        _mainForm.lblCamActive.Visible = CheckWhatChanged("webcam");
                        _mainForm.lblMicActive.Visible = CheckWhatChanged("microphone");
                    }));
                }
                _lastNotification = now;
            }

        }

        static string[] CheckAccess(RegistryKey searchPath, string key, string program)
        {
            List<string> programs = new List<string>();
            RegistryKey subKey = searchPath.OpenSubKey(key);
            string[] vals = subKey.GetValueNames();
            foreach (string v in vals)
            {
                if (v.ToLower() == "LastUsedTimeStop".ToLower())
                {
                    long stopTime = (long)subKey.GetValue(v);
                    if (stopTime == 0)
                    {
                        programs.Add(program);
                    }

                }
            }
            return programs.ToArray();
        }
        static bool CheckWhatChanged(string device)
        {
            List<string> activePrograms = new List<string>();

            RegistryKey searchPath = Registry.CurrentUser;
            searchPath = searchPath.OpenSubKey("SOFTWARE").OpenSubKey("Microsoft").OpenSubKey("Windows").OpenSubKey("CurrentVersion").OpenSubKey("CapabilityAccessManager").OpenSubKey("ConsentStore").OpenSubKey(device);
            string[] AllKeys = searchPath.GetSubKeyNames();
            foreach (string key in AllKeys)
            {
                try
                {
                    if (key == "NonPackaged")
                        continue;

                    string[] tmp = key.Split('_');
                    string program = tmp[0].Split('.')[1];

                    string[] programs = CheckAccess(searchPath, key, program);
                    if (programs.Length > 0)
                    {
                        foreach (string p in programs)
                        {
                            activePrograms.Add(program);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            searchPath = searchPath.OpenSubKey("NonPackaged");
            string[] AllPackagedKeys = searchPath.GetSubKeyNames();
            foreach (string key in AllPackagedKeys)
            {
                try
                {
                    string[] tmp = key.Split('#');
                    string program = tmp.Last();

                    string[] programs = CheckAccess(searchPath, key, program);
                    if (programs.Length > 0)
                    {
                        foreach (string p in programs)
                        {
                            activePrograms.Add(program);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            if (activePrograms.Count > 0)
            {
                foreach (string p in activePrograms)
                {
                    string msg = String.Format("The program {0} is using your {1}!", p, device);
                    
                    if (serial_connection != null && serial_connection.IsOpen) 
                        serial_connection.WriteLine("{Command: \"State\", Device: \"" + device + "\", State: 1}");

                    Console.WriteLine(msg);
                    if (myNotifyIcon != null)
                        myNotifyIcon.ShowBalloonTip(2000, "Someone is watching you!", msg, ToolTipIcon.Warning);
                }
                return true;
            }
            else
            {
                Console.WriteLine("Your {0} is not beeing used!", device);
                if (serial_connection != null && serial_connection.IsOpen)
                    serial_connection.WriteLine("{Command: \"State\", Device: \"" + device + "\", State: 0}");

                return false;
            }

        }


        private void LoadCOMPortList()
        {
            cmbComPort.Items.Clear();   // Clear the list of the COM ports
            foreach (string port in SerialPort.GetPortNames()) // Loop through all the available COM ports
            {
                cmbComPort.Items.Add(port); // Add the port to the list
            }

            // Check if the default COM port is in the list and if so select it
            cmbComPort.SelectedIndex = cmbComPort.FindString(Settings.Default.com_port);

            if (cmbComPort.SelectedIndex < 0) // If none of the ports is selected select the first one
            {
                cmbComPort.SelectedIndex = 0;
                Settings.Default.com_port = cmbComPort.Text; // Save the new selection
                Settings.Default.Save();
            }
        }

        private void SetupSerialPort(ref SerialPort sPort, string ComPort, int BaudRate, int DataBits)
        {
            if (sPort == null)
            {
                sPort = new SerialPort(Settings.Default.com_port, 115200);
                sPort.DataReceived += new SerialDataReceivedEventHandler(serial_connection_DataReceived);
                sPort.DataBits = 8;
                sPort.DtrEnable = true;

            }
            serial_connection.Open();

            // Delay to make sure we have stable connection
            Task.Delay(2000).ContinueWith(t => {
                Invoke(new Action(() => {
                    SendColorsToDevice();
                    SendBeepToDevice();

                    lblCamActive.Visible = CheckWhatChanged("webcam");
                    lblMicActive.Visible = CheckWhatChanged("microphone");
                }));
            });
        }

        void serial_connection_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            Console.WriteLine("Data Received:");
            Console.Write(indata);
        }

        private void ConnectToDevice()
        {
            try
            {


                // Use the button text as an indecator for conneciton
                if (btnConnect.Text == "Connect")
                {
                    // Load the information to the serial port for connection
                    SetupSerialPort(ref serial_connection, Settings.Default.com_port, 115200, 8);
                    btnConnect.Text = "Disconnect"; // Change the button caption
                }
                else
                {
                    serial_connection.Close(); // Close the connection
                    btnConnect.Text = "Connect"; // Change the button caption
                }
            }
            catch (Exception err)
            {
                Console.WriteLine(err.Message);
                btnConnect.Text = "Connect"; // Faild to connect
            }

        }


        public main()
        {
            InitializeComponent();
        }

        private void main_Load(object sender, EventArgs e)
        {
            LoadCOMPortList();

            receiveEvent = new WmiChangeEventTester(this);

            colorDialog.AllowFullOpen = false;
            colorDialog.ShowHelp = true;

            picCamOnColor.BackColor = ColorTranslator.FromHtml("#" + Settings.Default.cam_on_color);
            picCamOffColor.BackColor = ColorTranslator.FromHtml("#" + Settings.Default.cam_off_color);
            picMicOnColor.BackColor = ColorTranslator.FromHtml("#" + Settings.Default.mic_on_color);
            picMicOffColor.BackColor = ColorTranslator.FromHtml("#" + Settings.Default.mic_off_color);


            myNotifyIcon = new System.Windows.Forms.NotifyIcon();
            myNotifyIcon.Icon = shield.Properties.Resources.camera; // new Icon(shield.Properties.Resources.;

            myNotifyIcon.Text = "I'm watching your back!";
            myNotifyIcon.Visible = true;

            myNotifyIcon.DoubleClick += MyNotifyIcon_DoubleClick;

            chkBeep.Checked = Settings.Default.beep;
        }

        private void RunOnStartup()
        {
            RegistryKey regKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (chkStartup.Checked)
            {
                regKey.SetValue(Text, Application.ExecutablePath);
            }
            else
            {
                regKey.DeleteValue(Text, false);
            }
        }

        private bool CheckIfRunningInStartup()
        {
            RegistryKey regKey = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            var val = regKey.GetValue(Text);
            if  (val != null)
            {
                chkStartup.Checked = true;
                return true;
            }
            return false;
        }


        private void MyNotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private static String Color2Hex(System.Drawing.Color c)
        {
            return c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        
        private void btnConnect_Click(object sender, EventArgs e)
        {
            ConnectToDevice();
        }

        private void cmbComPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Default.com_port = cmbComPort.Text;
            Settings.Default.Save();
        }


        private void SendColorsToDevice()
        {
            if (serial_connection != null && serial_connection.IsOpen)
            {
                
                string command = "{Command: \"Color\", CamOnColor: \"" + Settings.Default.cam_on_color +
                                    "\", CamOffColor: \"" + Settings.Default.cam_off_color +
                                    "\", MicOnColor: \"" + Settings.Default.mic_on_color +
                                    "\", MicOffColor: \"" + Settings.Default.mic_off_color + "\"}";
                Console.WriteLine("################################## " + command);
                serial_connection.WriteLine(command);
            }
        }

        private void SendBeepToDevice()
        {
            if (serial_connection != null && serial_connection.IsOpen)
            {
                string command = "{Command: \"Beep\", Allow: " + (Settings.Default.beep ? 1 : 0) + "}";
                serial_connection.WriteLine(command);
            }
        }



        private void btnComReload_Click(object sender, EventArgs e)
        {
            LoadCOMPortList();
        }

        private void picCamOnColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                picCamOnColor.BackColor = colorDialog.Color;
                Settings.Default.cam_on_color = Color2Hex(colorDialog.Color);
                Settings.Default.Save();
                SendColorsToDevice();
            }
        }

        private void picCamOffColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                picCamOffColor.BackColor = colorDialog.Color;
                Settings.Default.cam_off_color = Color2Hex(colorDialog.Color);
                Settings.Default.Save();
                SendColorsToDevice();
            }
        }

        private void picMicOnColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                picMicOnColor.BackColor = colorDialog.Color;
                Settings.Default.mic_on_color = Color2Hex(colorDialog.Color);
                Settings.Default.Save();
                SendColorsToDevice();
            }
        }

        private void picMicOffColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                picMicOffColor.BackColor = colorDialog.Color;
                Settings.Default.mic_off_color = Color2Hex(colorDialog.Color);
                Settings.Default.Save();
                SendColorsToDevice();
            }
        }

        private void main_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
            }
        }

        private void chkStartup_CheckedChanged(object sender, EventArgs e)
        {
            RunOnStartup();
        }

        private void main_Shown(object sender, EventArgs e)
        {
            if (CheckIfRunningInStartup())
            {
                Hide();
                ConnectToDevice();
            }

        }

        private void chkBeep_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.beep = chkBeep.Checked;
            Settings.Default.Save();
            SendBeepToDevice();
        }
    }
}
