using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker.Shared.CardsLogic
{
    public class HandSerializer
    {
        public Hand DeserializeHand(byte[] inputData, ref int index)
        {
            var card1 = DeserializeCard(inputData: inputData, index: ref index);
            var card2 = DeserializeCard(inputData: inputData, index: ref index);
            return new Hand(card1: card1, card2: card2);
        }

        public Card DeserializeCard(byte[] inputData, ref int index)
        {
            var dataValue = inputData[index];
            index++;
            
            var value = (CardValue)((int)dataValue / 10);
            CardColor color = (CardColor)(dataValue - (int)value*10);
            return new Card(value: value, color: color);
        }

        public void Serialize(Hand hand, byte[] outputData, ref int index)
        {
            if (index + 4 > outputData.Length)
            {
                throw new ArgumentException("Invalid index: " + index);
            }
            if (index < 0)
            {
                throw new ArgumentException("Invalid index: " + index);
            }
            
            if (hand == null)
            {
                throw new ArgumentNullException(nameof(hand));
            }

            if (outputData == null)
            {
                throw new ArgumentNullException(nameof(outputData));
            }

            Serialize(card: hand.Card1, outputData: outputData, index: ref index);
            Serialize(card: hand.Card2, outputData: outputData, index: ref index);
        }

        public void Serialize(Card card, byte[] outputData, ref int index)
        {
            if (index + 2 > outputData.Length)
            {
                throw new ArgumentException("Invalid index: " + index);
            }

            if (index < 0)
            {
                throw new ArgumentException("Invalid index: " + index);
            }
            if (card == null)
            {
                throw new ArgumentNullException(nameof(card));
            }

            if (outputData == null)
            {
                throw new ArgumentNullException(nameof(outputData));
            }

            byte dataValue = (byte)(10 * (int)card.Value + (int)card.Color);
            outputData[index] = dataValue;
            index++;
        }
    }
}
