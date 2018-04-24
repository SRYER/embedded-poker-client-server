using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Shared.CardsLogic
{
    public class Hand
    {
        public Card Card1 { get; set; }
        public Card Card2 { get; set; }

        public Hand(Card card1, Card card2)
        {
            if (card1.Value == card2.Value && card1.Color == card2.Color)
                throw new ArgumentException("Cards cannot be identical. Value " + card1.Value + " color " + card1.Color);
            Card1 = card1 ?? throw new ArgumentNullException(nameof(card1));
            Card2 = card2 ?? throw new ArgumentNullException(nameof(card2));
        }

        public override string ToString()
        {
            return Card1 + ", " + Card2;
        }
    }
}
