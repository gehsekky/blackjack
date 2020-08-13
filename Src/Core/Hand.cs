using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace blackjack.Src.Core
{
    /// <summary>
    /// hand of playing cards
    /// </summary>
    public class Hand
    {
        private List<Card> _cards;
        private Dictionary<Card, bool> _cardVisibility;

        public int Count
        {
            get => _cards.Count;
        }

        public Hand()
        {
            _cards = new List<Card>();
            _cardVisibility = new Dictionary<Card, bool>();
        }

        /// <summary>
        /// insert card into deck
        /// </summary>
        /// <param name="card">card to insert</param>
        /// <param name="visible">is it visible on the table?</param>
        public void Add(Card card, bool visible = true)
        {
            _cards.Add(card);
            _cardVisibility.Add(card, visible);
        }

        /// <summary>
        /// get only the cards that are visible on the table
        /// </summary>
        /// <returns>cards which are visible</returns>
        public List<Card> GetVisibleCards()
        {
            return _cards.FindAll(c => _cardVisibility[c] == true);
        }

        /// <summary>
        /// get all cards in hand
        /// </summary>
        /// <returns>all cards</returns>
        public List<Card> GetAllCards()
        {
            return _cards;
        }

        /// <summary>
        /// get sum of cards in hand. by default, aces are 11 unless they cause the hand to bust at which point they become 1
        /// </summary>
        /// <returns>number value of sum of cards</returns>
        public int GetSumOfCards()
        {
            int sum = 0;
            foreach (Card card in _cards)
            {
                switch (card.Rank)
                {
                    case Rank.Two:
                        sum += 2;
                        break;
                    case Rank.Three:
                        sum += 3;
                        break;
                    case Rank.Four:
                        sum += 4;
                        break;
                    case Rank.Five:
                        sum += 5;
                        break;
                    case Rank.Six:
                        sum += 6;
                        break;
                    case Rank.Seven:
                        sum += 7;
                        break;
                    case Rank.Eight:
                        sum += 8;
                        break;
                    case Rank.Nine:
                        sum += 9;
                        break;
                    case Rank.Ten:
                        sum += 10;
                        break;
                    case Rank.Jack:
                        sum += 10;
                        break;
                    case Rank.Queen:
                        sum += 10;
                        break;
                    case Rank.King:
                        sum += 10;
                        break;
                    case Rank.Ace:
                        sum += 11;
                        break;
                    default:
                        throw new Exception(string.Format("Unknown rank encountered: {0}", card.Rank));
                }
            }

            if (sum > 21)
            {
                List<Card> aces = _cards.FindAll(c => c.Rank == Rank.Ace);
                if (aces.Count > 0)
                {
                    foreach (Card ace in aces)
                    {
                        if (sum > 21) sum -= 10;
                    }
                }
            }

            return sum;
        }

        /// <summary>
        /// checks if sum of hand is 21
        /// </summary>
        /// <returns>is sum 21?</returns>
        public bool Is21()
        {
            int sum = GetSumOfCards();
            if (sum == 21) return true;
            return false;
        }

        /// <summary>
        /// checks if sum of hand is over 21
        /// </summary>
        /// <returns>is sum over 21?</returns>
        public bool IsOver21()
        {
            int sum = GetSumOfCards();
            return sum > 21;
        }
    }
}
