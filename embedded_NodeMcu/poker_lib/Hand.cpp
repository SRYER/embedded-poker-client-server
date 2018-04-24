#include "Hand.h"

Hand::Hand(byte card_data_1, byte card_data_2)
{
  _card_data_1 = card_data_1;
  _card_data_2 = card_data_2;
}

Card Hand::card1(){
  return Card(_card_data_1);
}

Card Hand::card2(){
  return Card(_card_data_2);
}

String Hand::toString(){
  return "[" + card1().toString() + " | " + card2().toString() + "]";
}
