using System;
using System.Linq;
using System.Text;
using blackjack.Src.Core;

namespace blackjack
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("welcome to blackjack");

            // initialize game and engine loop
            Game game = new Game();
            startNewGame(game, true);
            bool exit = false;
            while (!exit)
            {
                // display game info
                writeGameInfo(game);

                // display menu and read user input
                Console.Write("what would you like to do? (S)urrender? (H)it? (C)all? (Q)uit?");
                string userSelection = Console.ReadLine();
                switch (userSelection.ToLower())
                {
                    case "s":
                        Console.WriteLine("you surrender. half your money back, half to the dealer.");
                        game.PlayerSurrenders();
                        startNewGame(game);
                        break;
                    case "h":
                        game.Hit();
                        if (game.PlayerHand.IsOver21())
                        {
                            writeGameInfo(game, true);
                            Console.WriteLine("over 21. bust. you lose.");
                            game.PlayerLoses();
                            startNewGame(game);
                        }

                        if (game.PlayerHand.Is21())
                        {
                            game.DealersTurn();

                            if (game.DealerHand.IsOver21())
                            {
                                writeGameInfo(game, true);
                                Console.WriteLine("dealer busts. you win!");
                                game.PlayerWins();
                                startNewGame(game);
                            }
                            else if (game.DealerHand.Is21())
                            {
                                writeGameInfo(game, true);
                                Console.WriteLine("you have 21, but dealer also has 21. tie. nothing lost, nothing gained.");
                                game.PlayerPushes();
                                startNewGame(game);
                            }
                            else
                            {
                                writeGameInfo(game, true);
                                bool isBlackjack = game.PlayerHand.Count == 2;
                                if (isBlackjack)
                                {
                                    Console.WriteLine(string.Format("you have blackjack, dealer has {0}. you win!", game.DealerHand.GetSumOfCards()));
                                }
                                else
                                {
                                    Console.WriteLine(string.Format("you have 21, dealer has {0}. you win!", game.DealerHand.GetSumOfCards()));
                                }
                                game.PlayerWins(isBlackjack);
                                startNewGame(game);
                            }
                        }
                        break;
                    case "c":
                        game.DealersTurn();
                        if (game.DealerHand.IsOver21())
                        {
                            writeGameInfo(game, true);
                            Console.WriteLine("dealer busts. you win!");
                            game.PlayerWins();
                            startNewGame(game);
                        }
                        else
                        {
                            int dealerSum = game.DealerHand.GetSumOfCards();
                            int playerSum = game.PlayerHand.GetSumOfCards();

                            if (dealerSum == playerSum)
                            {
                                writeGameInfo(game, true);
                                Console.WriteLine(string.Format("you have {0}, dealer has {1}. tie. nothing lost, nothing gained.", playerSum, dealerSum));
                                game.PlayerPushes();
                                startNewGame(game);
                            }
                            else if (dealerSum == 21)
                            {
                                writeGameInfo(game, true);
                                Console.WriteLine(string.Format("you have {0}, dealer has 21. you lose.", playerSum));
                                game.PlayerLoses();
                                startNewGame(game);
                            }
                            else if (dealerSum > playerSum)
                            {
                                writeGameInfo(game, true);
                                Console.WriteLine(string.Format("you have {0}, dealer has {1}. you lose.", playerSum, dealerSum));
                                game.PlayerLoses();
                                startNewGame(game);
                            }
                            else
                            {
                                writeGameInfo(game, true);
                                Console.WriteLine(string.Format("you have {0}, dealer has {1}. you win!", playerSum, dealerSum));
                                game.PlayerWins();
                                startNewGame(game);
                            }
                        }
                        break;
                    case "q":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("invalid choice. please try again.");
                        break;
                }
            }

            Console.WriteLine("thanks for playing!");
        }

        private static void writeGameInfo(Game game, bool showAll = false)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("player's cards: " + String.Join(", ", game.PlayerHand.GetAllCards().Select(c => c.ToString())) + Environment.NewLine);
            if (showAll)
            {
                sb.Append("dealer's cards: " + String.Join(", ", game.DealerHand.GetAllCards().Select(c => c.ToString())));
            }
            else
            {
                sb.Append("dealer's visible cards: " + String.Join(", ", game.DealerHand.GetVisibleCards().Select(c => c.ToString())));
            }
            Console.WriteLine(sb.ToString());
        }

        private static void startNewGame(Game game, bool isNewGame = false)
        {
            game.Initialize(isNewGame);
            Console.WriteLine("new game has started");
            Console.WriteLine(string.Format("player has ${0}.", game.PlayerMoney));
            Console.Write("bet how much?");
            string userBetInput = Console.ReadLine();
            int userBet;
            while (!int.TryParse(userBetInput, out userBet))
            {
                Console.WriteLine("bet must be a positive integer.");
                Console.Write("bet how much?");
                userBetInput = Console.ReadLine();
            }
            game.Bet(userBet);
        }
    }
}
