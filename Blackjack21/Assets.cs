using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Blackjack21
{
    internal class Assets
    {
        /// <summary>
        /// Renders game scene on console.
        /// </summary>
        /// <param name="Pack">The card pack.</param>
        /// <param name="Player">The player GameEntity.</param>
        /// <param name="Bank">The bank Game entity.</param>
        public static void RenderScene(List<Card> Pack, GameEntity Player, GameEntity Bank, bool hideFirst = true, bool trueValue = false)
        {
            ClearScene();
            Console.WriteLine("======= GAME =======");

            Console.Write($"{Bank.Name}: ");
            bool hide = hideFirst;
            foreach (var c in Bank.hand)
            {
                if (hide) { Console.Write(" ?"); hide = false; }
                else Console.Write($" {c.Name}");
            }
            if (trueValue) Console.Write($" ({Bank.trueHandValue})");
            else Console.Write($" ({Bank.handValue})");
            if (Bank.handValue == 21) Console.Write(" [ BLACKJACK! ]");
            
            Console.WriteLine("\n====================");
            
            Console.Write($"{Player.Name}: ");
            foreach (var c in Player.hand)
            {
                Console.Write($" {c.Name}");
            }
            Console.Write($" ({Player.trueHandValue})");
            if (Player.trueHandValue == 21) Console.Write(" [ BLACKJACK! ]");

            Console.WriteLine("\n====================");
        }

        /// <summary>
        /// Clears console.
        /// </summary>
        public static void ClearScene()
        {
            Console.Clear();
        }

        /// <summary>
        /// Choice dialog.
        /// </summary>
        /// <param name="question">Question.</param>
        /// <param name="choices">Array of choices.</param>
        /// <returns>Returns a lowercased version of the answer.</returns>
        public static string Choice(string question, string[] choices)
        {
            Console.WriteLine($"{question}\n");
            for (int i = 0; i < choices.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {choices[i]}");
            }

            bool error = true;
            string chosen;
            Console.Write("\n>>> ");
            do
            {
                chosen = Console.ReadLine();
                if (!int.TryParse(chosen, out int x))
                {
                    foreach (var item in choices)
                    {
                        if (item.ToLower() == chosen.ToLower()) error = false;
                    }
                }
                else
                {
                    if (x - 1 < choices.Length) return choices[x - 1].ToLower();
                }
            } while (error != false);

            return chosen.ToLower();
        }

        /// <summary>
        /// Gives a card to the player.
        /// </summary>
        /// <param name="entity">Entity.</param>
        /// <param name="CardPack">The cardpack the Entity draws from.</param>
        /// <param name="amount">The amount of cards the Entity gets.</param>
        public static void giveCard(GameEntity entity, List<Card> CardPack, int amount)
        {
            Random rnd = new Random();

            for (int i = 0; i < amount; i++)
            {
                while (true)
                {
                    int card = rnd.Next(0, CardPack.Count());
                    if (!CardPack[card].isDrawn)
                    {
                        if (CardPack[card].Name != "A")
                        {
                            entity.AddCard(CardPack[card]);
                            break;
                        }
                        else
                        {
                            if (entity.trueHandValue + 11 > 21)
                            {
                                CardPack[card].Value = 1;
                                entity.AddCard(CardPack[card]);
                                break;
                            }
                            else
                            {
                                CardPack[card].Value = 11;
                                entity.AddCard(CardPack[card]);
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
