#include "Arduino.h"
#include <ESP8266WiFi.h>
#include "Poker.h"

Poker::Poker(char* wifi_ssid, char* wifi_password, byte* server_ip, int server_port)
{
  _wifi_ssid = wifi_ssid;
  _wifi_password = wifi_password;

  _server_ip = server_ip;
  _server_port = server_port;
}

bool Poker::connectToWifi() {
  WiFi.begin(_wifi_ssid, _wifi_password);
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

Hand Poker::getHand()
{
  if(false == connectToWifi())
  {
    Serial.println("Couldnt connect to wifi!");
  }
  
  WiFiClient client;
  
  if (false == client.connect(_server_ip, _server_port))
  {
    Serial.println("Failed to connect to poker server");
  }

  Serial.print("Connected to poker server. Awaiting hand"); 
  while(client.available() < 2){
    Serial.print(".");
    delay(50);
  }
  Serial.println();
  byte card_data[2];
  client.readBytes(card_data, 2);
  Serial.println("Hand received");
  
  Hand hand = Hand(card_data[0],card_data[1]);
  return hand;
}


