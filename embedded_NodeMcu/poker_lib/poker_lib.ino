#include "Poker.h"

// Connection settings
char* wifi_ssid = "Frandsen International";
char* wifi_password = "11111111";
byte server_ip[4] = {192,168,1,118};
int serverPort = 7676;

// Init a poker object
Poker poker(wifi_ssid, wifi_password, server_ip, serverPort);

void setup()
{
  Serial.begin(9600);
}

void loop()
{
  Serial.println("Getting hand...");
  Hand hand = poker.getHand();
  Serial.println("Got hand: " + hand.toString());
  delay(1000);
}
