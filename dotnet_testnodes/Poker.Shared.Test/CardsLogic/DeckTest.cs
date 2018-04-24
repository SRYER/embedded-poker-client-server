using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker.Shared.CardsLogic;

namespace Poker.Shared.Test.CardsLogic
{
    [TestClass]
    public class DeckTest
    {
        [TestMethod]
        public void DrawHand_Test()
        {
            var deck = new Deck();

            var drawnCards = new HashSet<Card>();

            // Ensure exactly 52 cards
            for(var i = 0; i < 52; i+=2)
            {
                var hand = deck.DrawHand();

                Assert.IsFalse(drawnCards.Contains(hand.Card1));
                Assert.IsFalse(drawnCards.Contains(hand.Card2));

                drawnCards.Add(hand.Card1);
                drawnCards.Add(hand.Card2);

                Assert.IsTrue(drawnCards.Contains(hand.Card1));
                Assert.IsTrue(drawnCards.Contains(hand.Card2));
            }

            Assert.AreEqual(52, drawnCards.Count);

            try
            {
                deck.DrawHand();
                Assert.Fail("Expected exception as not more cards in deck");
            }
            catch (NoMoreCardsException)
            {
                // Assert this
            }
        }
    }
}
