namespace shield
{
    partial class main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(main));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnComReload = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.cmbComPort = new System.Windows.Forms.ComboBox();
            this.lblBoard = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblMicActive = new System.Windows.Forms.Label();
            this.lblCamActive = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.picMicOffColor = new System.Windows.Forms.PictureBox();
            this.picMicOnColor = new System.Windows.Forms.PictureBox();
            this.picCamOffColor = new System.Windows.Forms.PictureBox();
            this.picCamOnColor = new System.Windows.Forms.PictureBox();
            this.chkBeep = new System.Windows.Forms.CheckBox();
            this.chkStartup = new System.Windows.Forms.CheckBox();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMicOffColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMicOnColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCamOffColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCamOnColor)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnComReload);
            this.groupBox3.Controls.Add(this.btnConnect);
            this.groupBox3.Controls.Add(this.cmbComPort);
            this.groupBox3.Controls.Add(this.lblBoard);
            this.groupBox3.Location = new System.Drawing.Point(11, 173);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(270, 56);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Serial Connection:";
            // 
            // btnComReload
            // 
            this.btnComReload.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnComReload.BackgroundImage")));
            this.btnComReload.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnComReload.Location = new System.Drawing.Point(147, 20);
            this.btnComReload.Name = "btnComReload";
            this.btnComReload.Size = new System.Drawing.Size(25, 23);
            this.btnComReload.TabIndex = 15;
            this.btnComReload.UseVisualStyleBackColor = true;
            this.btnComReload.Click += new System.EventHandler(this.btnComReload_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(178, 20);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 1;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // cmbComPort
            // 
            this.cmbComPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbComPort.FormattingEnabled = true;
            this.cmbComPort.Location = new System.Drawing.Point(77, 21);
            this.cmbComPort.Name = "cmbComPort";
            this.cmbComPort.Size = new System.Drawing.Size(63, 21);
            this.cmbComPort.TabIndex = 10;
            this.cmbComPort.SelectedIndexChanged += new System.EventHandler(this.cmbComPort_SelectedIndexChanged);
            // 
            // lblBoard
            // 
            this.lblBoard.AutoSize = true;
            this.lblBoard.Location = new System.Drawing.Point(6, 26);
            this.lblBoard.Name = "lblBoard";
            this.lblBoard.Size = new System.Drawing.Size(60, 13);
            this.lblBoard.TabIndex = 11;
            this.lblBoard.Text = "Board Port:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblMicActive);
            this.groupBox1.Controls.Add(this.lblCamActive);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.picMicOffColor);
            this.groupBox1.Controls.Add(this.picMicOnColor);
            this.groupBox1.Controls.Add(this.picCamOffColor);
            this.groupBox1.Controls.Add(this.picCamOnColor);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(270, 132);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Colors:";
            // 
            // lblMicActive
            // 
            this.lblMicActive.AutoSize = true;
            this.lblMicActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMicActive.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblMicActive.Location = new System.Drawing.Point(185, 83);
            this.lblMicActive.Name = "lblMicActive";
            this.lblMicActive.Size = new System.Drawing.Size(58, 20);
            this.lblMicActive.TabIndex = 9;
            this.lblMicActive.Text = "Active";
            this.lblMicActive.Visible = false;
            // 
            // lblCamActive
            // 
            this.lblCamActive.AutoSize = true;
            this.lblCamActive.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCamActive.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblCamActive.Location = new System.Drawing.Point(185, 40);
            this.lblCamActive.Name = "lblCamActive";
            this.lblCamActive.Size = new System.Drawing.Size(58, 20);
            this.lblCamActive.TabIndex = 8;
            this.lblCamActive.Text = "Active";
            this.lblCamActive.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(134, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "OFF";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(92, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "ON";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Microphone:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Webcam:";
            // 
            // picMicOffColor
            // 
            this.picMicOffColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picMicOffColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picMicOffColor.Location = new System.Drawing.Point(133, 78);
            this.picMicOffColor.Name = "picMicOffColor";
            this.picMicOffColor.Size = new System.Drawing.Size(32, 32);
            this.picMicOffColor.TabIndex = 3;
            this.picMicOffColor.TabStop = false;
            this.picMicOffColor.Click += new System.EventHandler(this.picMicOffColor_Click);
            // 
            // picMicOnColor
            // 
            this.picMicOnColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picMicOnColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picMicOnColor.Location = new System.Drawing.Point(88, 78);
            this.picMicOnColor.Name = "picMicOnColor";
            this.picMicOnColor.Size = new System.Drawing.Size(32, 32);
            this.picMicOnColor.TabIndex = 2;
            this.picMicOnColor.TabStop = false;
            this.picMicOnColor.Click += new System.EventHandler(this.picMicOnColor_Click);
            // 
            // picCamOffColor
            // 
            this.picCamOffColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picCamOffColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picCamOffColor.Location = new System.Drawing.Point(133, 35);
            this.picCamOffColor.Name = "picCamOffColor";
            this.picCamOffColor.Size = new System.Drawing.Size(32, 32);
            this.picCamOffColor.TabIndex = 1;
            this.picCamOffColor.TabStop = false;
            this.picCamOffColor.Click += new System.EventHandler(this.picCamOffColor_Click);
            // 
            // picCamOnColor
            // 
            this.picCamOnColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picCamOnColor.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picCamOnColor.Location = new System.Drawing.Point(88, 35);
            this.picCamOnColor.Name = "picCamOnColor";
            this.picCamOnColor.Size = new System.Drawing.Size(32, 32);
            this.picCamOnColor.TabIndex = 0;
            this.picCamOnColor.TabStop = false;
            this.picCamOnColor.Click += new System.EventHandler(this.picCamOnColor_Click);
            // 
            // chkBeep
            // 
            this.chkBeep.AutoSize = true;
            this.chkBeep.Location = new System.Drawing.Point(11, 150);
            this.chkBeep.Name = "chkBeep";
            this.chkBeep.Size = new System.Drawing.Size(261, 17);
            this.chkBeep.TabIndex = 18;
            this.chkBeep.Text = "Beep when camera or microphone are being used";
            this.chkBeep.UseVisualStyleBackColor = true;
            this.chkBeep.CheckedChanged += new System.EventHandler(this.chkBeep_CheckedChanged);
            // 
            // chkStartup
            // 
            this.chkStartup.AutoSize = true;
            this.chkStartup.Location = new System.Drawing.Point(11, 235);
            this.chkStartup.Name = "chkStartup";
            this.chkStartup.Size = new System.Drawing.Size(143, 17);
            this.chkStartup.TabIndex = 19;
            this.chkStartup.Text = "Run on Windows startup";
            this.chkStartup.UseVisualStyleBackColor = true;
            this.chkStartup.CheckedChanged += new System.EventHandler(this.chkStartup_CheckedChanged);
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 259);
            this.Controls.Add(this.chkStartup);
            this.Controls.Add(this.chkBeep);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "S.H.I.E.L.D";
            this.Load += new System.EventHandler(this.main_Load);
            this.Shown += new System.EventHandler(this.main_Shown);
            this.Resize += new System.EventHandler(this.main_Resize);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMicOffColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMicOnColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCamOffColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCamOnColor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnComReload;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ComboBox cmbComPort;
        private System.Windows.Forms.Label lblBoard;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picMicOffColor;
        private System.Windows.Forms.PictureBox picMicOnColor;
        private System.Windows.Forms.PictureBox picCamOffColor;
        private System.Windows.Forms.PictureBox picCamOnColor;
        private System.Windows.Forms.CheckBox chkBeep;
        private System.Windows.Forms.Label lblMicActive;
        private System.Windows.Forms.Label lblCamActive;
        private System.Windows.Forms.CheckBox chkStartup;
    }
}

