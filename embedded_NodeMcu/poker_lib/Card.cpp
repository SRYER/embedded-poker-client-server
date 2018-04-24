#include "Card.h"

Card::Card(byte card_data)
{
  _card_data = card_data;
}
byte Card::value(){
  return _card_data / 10;
}
byte Card::color(){
  return _card_data - value()*10;
}
String Card::toString(){
  return "Value: " + String(value()) + ", color: " + String(color());
}
