/*
  Morse.h - Library for flashing Morse code.
  Created by David A. Mellis, November 2, 2007.
  Released into the public domain.
*/
#ifndef Hand_h
#define Hand_h

#include "Arduino.h"
#include "Card.h"

class Hand
{
  public:
    Hand(byte card_data_1, byte card_data_2);
    Card card1();
    Card card2();
    String toString();
   private:
    byte _card_data_1;
    byte _card_data_2;
};

#endif
