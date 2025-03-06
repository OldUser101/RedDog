using System.Buffers.Text;

namespace RedDog
{
    internal class Program
    {
        public static void ShowTitle()
        {
            Console.Clear();
            Console.Write("=== ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("RED DOG");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(" ===");
            Console.WriteLine("(c) Nathan Gill\n");
        }

        static void SaveGame(Game g)
        {
            string s = $"{g.Credit.Amount};{g.name}";
            File.WriteAllText("game.sav", s);
        }

        static Game LoadGame()
        {
            string[] s = File.ReadAllText("game.sav").Split(";");

            Game g = new Game(s[1], Convert.ToDouble(s[0]), 10, -1);

            return g;
        }

        static void PlayGame(Game g)
        {
            while (!g.GameOver)
            {
                ShowTitle();

                int result = g.SelectGameOption();

                if (result == 0)
                {
                    ShowTitle();
                    g.GameBetSelect();
                    g.GamePrep();
                    while (!g.NextGame)
                    {
                        g.GamePlay();
                    }
                }
                else if (result == 1)
                {
                    SaveGame(g);
                    break;
                }
            }

            if (g.GameOver && File.Exists("game.sav"))
            {
                File.Delete("game.sav");
            }
        }

        static void ShowHelp() 
        {
            ShowTitle();

            // AI-GENERATED GAME HELP TEXT //

            string help = @"Welcome to Red Dog!

Rules:
    1. Two cards are dealt face-up.
    2. If the cards are consecutive, it's a push (no win or loss).
    3. If the cards are identical, a third card is drawn:
        - If the third card matches, you win 11:1.
        - Otherwise, it's a push.
    4. If the cards are not consecutive or identical:
        - A spread is determined(difference between card values minus 1).
        - You can double your bet or continue.
        - A third card is drawn.
        - If the third card falls within the spread, you win based on the odds.
        - Otherwise, you lose.

Payouts:
        - Spread of 1: 5:1
        - Spread of 2: 4:1
        - Spread of 3: 2:1
        - Spread of 4 or more: 1:1

Good luck!
";
            Console.WriteLine(help);
            Console.Write("Press ANY KEY to continue...");
            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.CursorVisible = true;

            bool quit = false;

            while (!quit)
            {
                bool showLoad = File.Exists("game.sav");

                Menu m = new Menu(showLoad ? ["New Game", "Load Game", "Game Help", "Quit"] : ["New Game", "Game Help", "Quit"], ShowTitle, null);

                int result = m.ShowMenu();

                if (result > 0 && !showLoad)
                {
                    result++;
                }

                switch (result)
                {
                    case 0:
                        {
                            Console.Write("\nYour name: ");
                            string name = Console.ReadLine() ?? String.Empty;
                            Game g = new Game(name, 1000, 10, -1);
                            PlayGame(g);
                            break;
                        }
                    case 1:
                        {
                            if (!File.Exists("game.sav"))
                            {
                                Console.WriteLine("\nNo game saved!\n");
                                Console.Write("Press ANY KEY to continue...");
                                Console.ReadKey();
                                break;
                            }
                            Game g = LoadGame();
                            PlayGame(g);
                            break;
                        }
                    case 2:
                        ShowHelp();
                        break;
                    case 3:
                        quit = true;
                        break;
                }
            }
        }
    }
}
