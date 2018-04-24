using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Shared.CardsLogic
{
    public class Card
    {
        public CardValue Value { get; set; }
        public CardColor Color { get; set; }

        public Card(CardValue value, CardColor color)
        {
            Value = value;
            Color = color;
        }

        public override bool Equals(object obj)
        {
            var card = obj as Card;
            return card != null &&
                   Value == card.Value &&
                   Color == card.Color;
        }

        public override int GetHashCode()
        {
            var hashCode = 1916625138;
            hashCode = hashCode * -1521134295 + Value.GetHashCode();
            hashCode = hashCode * -1521134295 + Color.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return Value.ToString() + " of " + Color.ToString();
        }
    }
}
