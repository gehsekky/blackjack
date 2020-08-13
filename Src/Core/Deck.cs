using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace blackjack.Src.Core
{
    /// <summary>
    /// deck of playing cards
    /// </summary>
    class Deck
    {
        private List<Card> _cards;

        public Deck()
        {
            // add cards to deck
            Suit[] suits = new Suit[] { Suit.Clubs, Suit.Diamonds, Suit.Hearts, Suit.Spades };
            Rank[] ranks = new Rank[] { Rank.Two, Rank.Three, Rank.Four, Rank.Five, Rank.Six, Rank.Seven,
                Rank.Eight, Rank.Nine, Rank.Ten, Rank.Jack, Rank.Queen, Rank.King, Rank.Ace };
            _cards = new List<Card>();
            foreach(Suit suit in suits)
            {
                foreach(Rank rank in ranks)
                {
                    _cards.Add(new Card(suit, rank));
                }
            }
        }

        /// <summary>
        /// top level shuffle function
        /// </summary>
        public void Shuffle()
        {
            // research shows it takes 7 shuffles to perfectly mix a deck
            for (int i = 0; i < 7; i++)
            {
                RandomizeDeck();
            }

            // customary to cut deck at end
            CutDeck();
        }

        /// <summary>
        /// gets card on top of deck
        /// </summary>
        /// <returns>next card</returns>
        public Card GetNextCard()
        {
            if (_cards.Count == 0) throw new ArgumentOutOfRangeException("no card left to draw");
            Card card = _cards[0];
            _cards.Remove(card);
            return card;
        }

        /// <summary>
        /// shuffle cards using random stacking strategy
        /// </summary>
        private void RandomizeDeck()
        {
            List<Card> randomized = new List<Card>();
            while (_cards.Count > 0)
            {
                Random seed = new Random();
                Card randomCard = _cards[seed.Next(0, _cards.Count)];
                randomized.Add(randomCard);
                _cards.Remove(randomCard);
            }

            _cards = randomized;
        }

        /// <summary>
        /// split deck and put back together in opposite order
        /// </summary>
        private void CutDeck()
        {
            if (_cards.Count == 1) return;
            Random seed = new Random();
            int totalCards = _cards.Count;
            int cutPoint = seed.Next(0, totalCards);
            List<Card> cutCards = _cards.GetRange(0, cutPoint);
            _cards.RemoveRange(0, cutPoint);
            cutCards.InsertRange(0, _cards.GetRange(0, _cards.Count));
            _cards = cutCards;
        }

        /// <summary>
        /// returns string representation of deck in current order
        /// </summary>
        /// <returns>deck of cards text</returns>
        override
        public string ToString()
        {
            return String.Join(", ", _cards.Select(c => c.ToString()));
        }
    }
}
