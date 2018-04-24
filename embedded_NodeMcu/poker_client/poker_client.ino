#include "TM1637.h"
int PIN_clk = D3;
int PIN_dio = D2;
TM1637 tm1637(PIN_clk,PIN_dio);
void setProgress(int number){
  tm1637.init();
  tm1637.set(BRIGHT_TYPICAL); 
  tm1637.display(0,number/1000%10);  // put a C at the end
  tm1637.display(1,number/100%10);  // put a C at the end
  tm1637.display(2,number/10%10);  // put a C at the end
  tm1637.display(3,number%10);  // put a C at the end
}



// http://www.instructables.com/id/Getting-Started-With-ESP8266LiLon-NodeMCU-V3Flashi/
// WIFI - https://techtutorialsx.com/2016/07/17/esp8266-http-get-requests/
#include <ESP8266WiFi.h>

const char* ssid = "Frandsen International";
const char* password = "11111111";
 
bool connectToWifi() {
  
  WiFi.begin(ssid, password);
  int retryCount = 50;
  Serial.print("Connecting to WIFI: ");
  while (WiFi.status() != WL_CONNECTED && retryCount > 0) {
    delay(1000);
    Serial.print(".");
    retryCount--;
  }
  Serial.println();

  return WiFi.status() == WL_CONNECTED;
}

void setup()
{
  Serial.begin(9600);
  //pinMode(PIN_clk, OUTPUT);
  
  
  setProgress(1);
  
  // Connect to wifi
  if(false == connectToWifi()){
    Serial.println("Could not connect!");
  }

  setProgress(2);
  
  // Connect to server
  WiFiClient client;
  byte host[]={192,168,1,118};
  
  if (client.connect(host, 7676)) {
    Serial.println("Connected to server. Waiting for hand...");
  }else{
    Serial.println("Could not connect to server");
    return;
  }

  setProgress(3);

  // Listen for hand
  byte hand = 255;
  Serial.print("Receiving hand");
  while(hand > 254){
    hand = client.read();
    Serial.print(".");
    delay(50);
    }

  Serial.println("Received hand: " + String(hand));
  setProgress(hand);
}

void loop() {
  Serial.print("-");
  delay(1000);
}





