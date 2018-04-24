using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Poker.Shared.CardsLogic;

namespace Poker.Shared.Test.CardsLogic
{
    [TestClass]
    public class CardTest
    {
        [TestMethod]
        public void Equals_Test()
        {
            var list = new HashSet<Card>();

            list.Add(new Card(CardValue.Deuce, CardColor.Diamonds));
            Assert.AreEqual(true, list.Contains(new Card(CardValue.Deuce, CardColor.Diamonds)));
            Assert.AreEqual(false, list.Contains(new Card(CardValue.Deuce, CardColor.Spades)));
            Assert.AreEqual(false, list.Contains(new Card(CardValue.Eight, CardColor.Diamonds)));
        }
    }
}
