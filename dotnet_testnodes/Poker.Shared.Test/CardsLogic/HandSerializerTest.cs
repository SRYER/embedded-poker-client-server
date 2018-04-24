using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker.Shared.CardsLogic;

namespace Poker.Shared.Test.CardsLogic
{
    [TestClass]
    public class HandSerializerTest
    {
        [TestMethod]
        public void SerializeCard_Test()
        {
            foreach (CardValue value in Enum.GetValues(typeof(CardValue)))
            {
                foreach (CardColor color in Enum.GetValues(typeof(CardColor)))
                {
                    var card = new Card(value: value, color: color);
                    Assert.AreEqual(value, card.Value);
                    Assert.AreEqual(color, card.Color);

                    var s = new HandSerializer();

                    var bytes = new byte[16];
                    int index = 3;

                    s.Serialize(card: card, outputData: bytes, index: ref index);

                    // Ensure nothing written before start index and after end index
                    Assert.AreEqual(0, bytes[0]);
                    Assert.AreEqual(0, bytes[1]);
                    Assert.AreEqual(0, bytes[2]);

                    for (var i = index; i < bytes.Length; i++)
                    {
                        Assert.AreEqual(0, bytes[i]);
                    }

                    index = 3;
                    var deserializedCard = s.DeserializeCard(inputData: bytes, index: ref index);
                    Assert.IsNotNull(deserializedCard);
                    Assert.AreEqual(card.Value, deserializedCard.Value);
                    Assert.AreEqual(card.Color, deserializedCard.Color);
                }
            }
        }

        [TestMethod]
        public void Serializehand_Test()
        {
            foreach (CardValue value1 in Enum.GetValues(typeof(CardValue)))
            {
                foreach (CardValue value2 in Enum.GetValues(typeof(CardValue)))
                {
                    foreach (CardColor color1 in Enum.GetValues(typeof(CardColor)))
                    {
                        foreach (CardColor color2 in Enum.GetValues(typeof(CardColor)))
                        {
                            var card1 = new Card(value: value1, color: color1);
                            var card2 = new Card(value: value2, color: color2);
                            if (card1.Value == card2.Value && card1.Color == card2.Color)
                                continue; // Hand cannot consist of 2 identical cards

                            var hand = new Hand(card1:card1, card2:card2);

                            var s = new HandSerializer();

                            var bytes = new byte[16];
                            int index = 3;

                            s.Serialize(hand: hand, outputData: bytes, index: ref index);

                            // Ensure nothing written before start index and after end index
                            Assert.AreEqual(0, bytes[0]);
                            Assert.AreEqual(0, bytes[1]);
                            Assert.AreEqual(0, bytes[2]);

                            for (var i = index; i < bytes.Length; i++)
                            {
                                Assert.AreEqual(0, bytes[i]);
                            }

                            index = 3;
                            var deserializedHand = s.DeserializeHand(inputData: bytes, index: ref index);
                            Assert.IsNotNull(deserializedHand);

                            Assert.AreEqual(card1.Value, deserializedHand.Card1.Value);
                            Assert.AreEqual(card1.Color, deserializedHand.Card1.Color);
                            Assert.AreEqual(card2.Value, deserializedHand.Card2.Value);
                            Assert.AreEqual(card2.Color, deserializedHand.Card2.Color);
                        }
                    }
                }
            }
            
        }
    }
}
