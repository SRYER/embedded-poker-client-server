/*
  Morse.h - Library for flashing Morse code.
  Created by David A. Mellis, November 2, 2007.
  Released into the public domain.
*/
#ifndef Poker_h
#define Poker_h

#include "Arduino.h"
#include <ESP8266WiFi.h>
#include "Hand.h"

class Poker
{
  public:
    Poker(char* wifi_ssid, char* wifi_password, byte* server_ip, int server_port);
    Hand getHand();
  private:
    char* _wifi_ssid;
    char* _wifi_password;
    byte* _server_ip;
    int _server_port;
    bool connectToWifi();
};

#endif
