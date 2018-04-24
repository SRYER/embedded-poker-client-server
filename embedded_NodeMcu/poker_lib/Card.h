/*
  Morse.h - Library for flashing Morse code.
  Created by David A. Mellis, November 2, 2007.
  Released into the public domain.
*/
#ifndef Card_h
#define Card_h

#include "Arduino.h"

class Card
{
  public:
    Card(byte card_data);
    byte value();
    byte color();
    String toString();
   private:
    byte _card_data;
};

#endif
