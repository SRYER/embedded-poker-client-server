using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Shared.CardsLogic
{
    public class Deck
    {
        private readonly object _syncLock = new object();
        private Random _ran = new Random();
        private List<Card> _cards;

        public int Count
        {
            get
            {
                lock (_syncLock)
                {
                    return _cards.Count;
                }
            }
        }

        public Deck()
        {
            _cards = new List<Card>(capacity: 52);
            foreach (CardValue value in Enum.GetValues(typeof(CardValue)))
            {
                foreach (CardColor color in Enum.GetValues(typeof(CardColor)))
                {
                    var card = new Card(value: value, color: color);
                    _cards.Add(card);
                }
            }
        }

        public Hand DrawHand()
        {
            lock (_syncLock)
            {
                var card1 = DrawCard();
                var card2 = DrawCard();
                var hand = new Hand(card1: card1, card2: card2);
                return hand;
            }
        }

        private Card DrawCard()
        {
            lock (_syncLock)
            {
                if (_cards.Count == 0)
                    throw new NoMoreCardsException();
                var card = _cards[_ran.Next(maxValue: _cards.Count)];
                _cards.Remove(card);
                return card;
            }
        }
    }

    public class NoMoreCardsException : Exception
    {
    }
}
