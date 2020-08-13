using System;
using System.Collections.Generic;
using System.Text;

namespace blackjack.Src.Core
{
    /// <summary>
    /// represents session of blackjack games
    /// </summary>
    class Game
    {
        private Deck _deck;
        private Hand _dealerHand;
        private Hand _playerHand;
        private int _playerMoney;
        private int _bet;

        public Game()
        {
            _deck = new Deck();
        }

        public Hand DealerHand
        {
            get => _dealerHand;
        }

        public Hand PlayerHand
        {
            get => _playerHand;
        }

        public int PlayerMoney
        {
            get => _playerMoney;
            set => _playerMoney = value;
        }

        /// <summary>
        /// initialize blackjack round. if new game, player gets free money to start with.
        /// </summary>
        /// <param name="newGame">is this new game?</param>
        public void Initialize(bool newGame = false)
        {
            if (newGame) _playerMoney = 1000;
            _deck.Shuffle();
            _dealerHand = new Hand();
            _playerHand = new Hand();

            Deal();
        }

        /// <summary>
        /// set round bet
        /// </summary>
        /// <param name="bet">amount of bet</param>
        public void Bet(int bet)
        {
            _bet = bet;
            _playerMoney -= bet;
        }

        /// <summary>
        /// deal cards to player and dealer. first is face down. next (and rest) are face up.
        /// </summary>
        public void Deal()
        {
            _playerHand.Add(_deck.GetNextCard(), false);
            _dealerHand.Add(_deck.GetNextCard(), false);

            _playerHand.Add(_deck.GetNextCard());
            _dealerHand.Add(_deck.GetNextCard());
        }

        /// <summary>
        /// perform hit action for player
        /// </summary>
        public void Hit()
        {
            _playerHand.Add(_deck.GetNextCard());
        }

        /// <summary>
        /// perform turn for dealer
        /// </summary>
        public void DealersTurn()
        {
            if (_dealerHand.GetSumOfCards() > 16) return;

            while (_dealerHand.GetSumOfCards() < 17)
            {
                _dealerHand.Add(_deck.GetNextCard());
            }
        }

        /// <summary>
        /// perform actions for player winning round
        /// </summary>
        /// <param name="blackjack">did player have blackjack?</param>
        public void PlayerWins(bool blackjack = false)
        {
            _playerMoney += _bet + (blackjack ? ((int)(1.5 * _bet)) : _bet);
        }

        /// <summary>
        /// perform actions for player losing round
        /// </summary>
        public void PlayerLoses()
        {

        }

        /// <summary>
        /// perform actions for player surrendering round
        /// </summary>
        public void PlayerSurrenders()
        {
            _playerMoney += ((int)(_bet / 2));
        }

        /// <summary>
        /// perform actions for player pushing round (draw or tie)
        /// </summary>
        public void PlayerPushes()
        {
            _playerMoney += _bet;
        }
    }
}
