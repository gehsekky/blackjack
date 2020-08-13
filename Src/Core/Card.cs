using System;
using System.Text;

namespace blackjack.Src.Core
{
    /// <summary>
    /// enum for card suits
    /// </summary>
    public enum Suit
    {
        Hearts,
        Diamonds,
        Clubs,
        Spades
    }

    /// <summary>
    /// enum for card ranks
    /// </summary>
    public enum Rank
    {
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }

    /// <summary>
    /// playing card
    /// </summary>
    public class Card
    {
        private Suit _suit;

        private Rank _rank;

        public Suit Suit
        {
            get => _suit;
        }

        public Rank Rank
        {
            get => _rank;
        }

        public Card(Suit suit, Rank rank)
        {
            _suit = suit;
            _rank = rank;
        }

        override
        public string ToString()
        {
            StringBuilder sb = new StringBuilder();
            switch (_rank)
            {
                case Rank.Two:
                    sb.Append("2");
                    break;
                case Rank.Three:
                    sb.Append("3");
                    break;
                case Rank.Four:
                    sb.Append("4");
                    break;
                case Rank.Five:
                    sb.Append("5");
                    break;
                case Rank.Six:
                    sb.Append("6");
                    break;
                case Rank.Seven:
                    sb.Append("7");
                    break;
                case Rank.Eight:
                    sb.Append("8");
                    break;
                case Rank.Nine:
                    sb.Append("9");
                    break;
                case Rank.Ten:
                    sb.Append("10");
                    break;
                case Rank.Jack:
                    sb.Append("J");
                    break;
                case Rank.Queen:
                    sb.Append("Q");
                    break;
                case Rank.King:
                    sb.Append("K");
                    break;
                case Rank.Ace:
                    sb.Append("A");
                    break;
                default:
                    throw new Exception(string.Format("Unknown rank encountered: {0}", _rank));
            }

            switch (_suit)
            {
                case Suit.Clubs:
                    sb.Append("C");
                    break;
                case Suit.Diamonds:
                    sb.Append("D");
                    break;
                case Suit.Hearts:
                    sb.Append("H");
                    break;
                case Suit.Spades:
                    sb.Append("S");
                    break;
                default:
                    throw new Exception(string.Format("Unknown suit encountered: {0}", _suit));
            }

            return sb.ToString();
        }
    }
}
