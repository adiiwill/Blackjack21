namespace Blackjack21
{
    internal class Program : Assets
    {
        static void Main(string[] args)
        {
            Statistics stats = new Statistics();

            if (File.Exists("data.csv"))
            {
                StreamReader sr = new StreamReader("data.csv");
                string[] dataSplit = sr.ReadLine().Split(';');
                stats.wins = Convert.ToInt32(dataSplit[0]);
                stats.loses = Convert.ToInt32(dataSplit[1]);
                stats.draws = Convert.ToInt32(dataSplit[2]);
                stats.blackjacks = Convert.ToInt32(dataSplit[3]);
                stats.level = Convert.ToInt32(dataSplit[4]);
                stats.xp = Convert.ToInt32(dataSplit[5]);
                stats.levelUpXp = Convert.ToInt32(dataSplit[6]);
                stats.games = Convert.ToInt32(dataSplit[7]);
                sr.Close();
            }

            string[] rankNames = { "BEGINNER", "NOVICE", "REGULAR", "DECENT", "ADVANCED", "PROFICIENT", "EXPERT", "MASTER", "GODLIKE", "BLACKJACK GOD" };

            while (true)
            {
                string[] choices = { "Play", "Statistics", "Exit" };

                string outcome = "";

                Console.WriteLine("=================");
                Console.WriteLine("      PLAYER     ");
                Console.WriteLine("-----------------");

                Console.WriteLine($"Rank: {rankNames[stats.level]}");
                Console.WriteLine($"TIER > {stats.level + 1} <");
                Console.WriteLine($"\nXP: {stats.xp}/{stats.levelUpXp}");
                Console.Write("<");
                float len = (stats.xp / stats.levelUpXp) * 100;
                int rem = 0;
                for (int i = 1; i < ((int)len / 10) + 1; i++)
                {
                    Console.Write("#");
                    rem = i;
                }
                for (int i = 0; i < 10 - rem; i++)
                {
                    Console.Write("-");
                }
                Console.Write($"> {(int)len}%");

                Console.WriteLine("\n=================");

                switch (Choice("\n=================\n    Game Menu    \n=================", choices))
                {
                    case "play":
                        ClearScene();
                        outcome = Game(stats);
                        Console.ReadLine();
                        ClearScene();

                        //XP BREAKDOWN
                        if (outcome == "won") stats.xp += 3;
                        else if (outcome == "tie") stats.xp += 2;
                        else if (outcome == "lose" || outcome == "busted") stats.xp += 1;

                        //XP CALCULATING
                        if (stats.xp >= stats.levelUpXp)
                        {
                            stats.xp -= stats.levelUpXp;
                            stats.level++;
                            stats.levelUpXp += 21;
                        }

                        break;

                    case "statistics":
                        ClearScene();
                        Console.WriteLine("================\n" +
                            "   STATISTICS\n" +
                            "----------------\n" +
                            $"Total games: {stats.games}\n\n" +
                            $"Wins: {stats.wins}\n" +
                            $"Loses: {stats.loses}\n" +
                            $"Draws: {stats.draws}\n" +
                            $"\nBlackjacks: {stats.blackjacks}");
                        Console.WriteLine("================");
                        Console.WriteLine("\n!!! If you'd like to reset your progress, type: reset");
                        Console.WriteLine("\nBack to menu [ENTER]");
                        string input = Console.ReadLine();
                        if (input == "reset")
                        {
                            ClearScene();
                            Console.WriteLine("Are you sure? Y/N");
                            input = Console.ReadLine();
                            if (input == "Yes" || input == "y" || input == "Y" || input == "yes") stats = new Statistics();
                        }
                        ClearScene();
                        break;

                    case "exit":
                        StreamWriter sw = new StreamWriter("data.csv");
                        sw.WriteLine($"{stats.wins};{stats.loses};{stats.draws};{stats.blackjacks};{stats.level};{stats.xp};{stats.levelUpXp};{stats.games}");
                        sw.Close();
                        Environment.Exit(0);
                        break;

                    default:
                        Console.WriteLine("Invalid selection.");
                        continue;
                }
            }
        }
        
        static string Game(Statistics stats)
        {
            string gameState = "ongoing";

            List<Card> CardPack = new List<Card>();

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    CardPack.Add(new Card((i + 1).ToString(), i + 1));
                }
            }

            for (int i = 0; i < 4; i++)
            {
                CardPack.Add(new Card("J", 10));
                CardPack.Add(new Card("Q", 10));
                CardPack.Add(new Card("K", 10));
                CardPack.Add(new Card("A", 0));
            }

            var Player = new GameEntity("Player", true);
            var Bank = new GameEntity("Bank");

            giveCard(Player, CardPack, 2);
            giveCard(Bank, CardPack, 2);

            bool blackjack = false;

            Bank.hand[0].isHidden = true;

            while (true) //Game Loop
            {
                RenderScene(CardPack, Player, Bank);

                string[] choices = { "Hit", "Stand" };

                switch (Choice("Hit or Stand?", choices))
                {
                    case "hit":
                        if (Player.trueHandValue + 11 > 21) Player.SetAceValue(1);
                        else Player.SetAceValue(11);
                        giveCard(Player, CardPack, 1);
                        RenderScene(CardPack, Player, Bank);
                        if (Player.trueHandValue > 21) { gameState = "busted"; break; }
                        continue;

                    case "stand":
                        while (true)
                        {
                            if (Bank.trueHandValue + 11 > 21) Bank.SetAceValue(1);
                            else Bank.SetAceValue(11);
                            Bank.hand[0].isHidden = false;
                            RenderScene(CardPack, Player, Bank, false);
                            if (Bank.trueHandValue < 16)
                            {
                                giveCard(Bank, CardPack, 1);
                            }
                            else
                            {
                                if (Player.trueHandValue > Bank.trueHandValue) { gameState = "won"; break; }
                                else if (Player.trueHandValue == Bank.trueHandValue) { gameState = "tie"; break; }
                                else { gameState = "lose"; break; }
                            }
                            if (Bank.trueHandValue > 21) { gameState = "won"; break; }
                        }
                        break;

                    default:
                        RenderScene(CardPack, Player, Bank);
                        continue;
                }

                if (Player.trueHandValue == 21) blackjack = true;
                if (gameState != "ongoing") break;
            }

            RenderScene(CardPack, Player, Bank, false, true);
            if (gameState == "busted") { Console.WriteLine("Busted!\n\n+1xp"); stats.loses++; }
            else if (gameState == "won") { Console.WriteLine("You won!\n\n+3xp"); stats.wins++; }
            else if (gameState == "tie") { Console.WriteLine("Push!\n\n+2xp"); stats.draws++; }
            else if (gameState == "lose") { Console.WriteLine("You lost!\n\n+1xp"); stats.loses++; }

            if (blackjack) { Console.WriteLine("BLACKJACK! +3xp"); stats.blackjacks++; stats.xp += 3; }

            stats.games++;

            return gameState;
        }
    }
}