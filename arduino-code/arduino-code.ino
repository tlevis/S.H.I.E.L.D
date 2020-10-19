#include <FastLED.h>
#include <ArduinoJson.h>

#define NUM_LEDS 2
#ifdef ESP32
  #define DATA_PIN 32
  #define BUZZER_PIN 22
  #define BUZZER_FREQ 1000
#else
  #define DATA_PIN 8
  #define BUZZER_PIN 9
#endif
CRGB leds[NUM_LEDS];
bool states[NUM_LEDS] = {0};
bool allowBeep = true;


StaticJsonDocument<500> jsonDoc;

CRGB webcamOnColor = CRGB::Red;
CRGB webcamOffColor = CRGB::Black;
CRGB micOnColor = CRGB::Red;
CRGB micOffColor = CRGB::Black;

void beep() {
  if (allowBeep) 
  {
#ifdef ESP32
    ledcWriteTone(0, BUZZER_FREQ);
    ledcWrite(0, 255);
    delay(100);
    ledcWrite(0, 0);
#else
    tone(BUZZER_PIN, 1000);
    delay(100);
    noTone(BUZZER_PIN);
#endif
  }
}

void setup() 
{ 
#ifdef ESP32   
  ledcSetup(0, BUZZER_FREQ, 8);
  ledcAttachPin(BUZZER_PIN, 0);
#else
  pinMode(BUZZER_PIN, OUTPUT);
#endif

  FastLED.addLeds<WS2812B, DATA_PIN, GRB>(leds, NUM_LEDS);
  Serial.begin(115200);
  FastLED.clear();
  FastLED.show();
}

CRGB stringToColor(String color)
{
  int r, g, b;
  sscanf(color.c_str(), "%02x%02x%02x", &r, &g, &b);
  return CRGB(r, g, b);
}

void SetColors(String inCamOnColor, String inCamOffColor, String inMicOnColor, String inMicOffColor) 
{
  webcamOnColor = stringToColor(inCamOnColor);
  webcamOffColor = stringToColor(inCamOffColor);
  micOnColor = stringToColor(inMicOnColor);
  micOffColor = stringToColor(inMicOffColor);

  leds[0] = (states[0] == 1) ? webcamOnColor : webcamOffColor;
  leds[1] = (states[1] == 1) ? micOnColor : micOffColor;
  FastLED.show();
}

void ChangeLEDColor(String deviceType, int state) {
  int ledIndex = (deviceType == "webcam") ? 0 : 1;
  CRGB color = (deviceType == "webcam") ? ((state == 1) ? webcamOnColor : webcamOffColor) : ((state == 1) ? micOnColor : micOffColor);
  states[ledIndex] = state;
  leds[ledIndex] = color;
  FastLED.show();
}


void loop() 
{ 
  if (Serial.available() > 0) 
  {
    String data = Serial.readStringUntil('\n');
    deserializeJson(jsonDoc, data.c_str());
    serializeJson(jsonDoc, Serial); // Just to confirm we received the message
    if (jsonDoc.containsKey("Command")) 
    {
      String command = jsonDoc["Command"].as<String>();
      if (command == "State") 
      {
        String device = jsonDoc["Device"].as<String>();
        int state = jsonDoc["State"].as<int>();
        ChangeLEDColor(device, state);

        if (state == 1)
          beep();
      } 
      else if (command == "Color") 
      {
        String camOnColor = jsonDoc["CamOnColor"].as<String>();
        String camOffColor = jsonDoc["CamOffColor"].as<String>();

        String micOnColor = jsonDoc["MicOnColor"].as<String>();
        String micOffColor = jsonDoc["MicOffColor"].as<String>();
        SetColors(camOnColor, camOffColor, micOnColor, micOffColor);
      }
      else if (command == "Beep") 
      {
        allowBeep = jsonDoc["Allow"].as<int>();
      }
    }
  }
}
