namespace RedDog
{
    public class Game
    {
        public bool GameOver { get; private set; } = false;
        public bool NextGame { get; private set; } = false;

        public Hand Hand { get; private set; }
        public Credit Credit { get; private set; }
        public string name { get; private set; }

        public double maxBet { get; private set; }
        public double minBet { get; private set; }
        public double initialCredit { get; private set; }

        public Game(string name, double initialCredit, double minBet, double maxBet)
        {
            this.name = name;
            this.Hand = new Hand();
            this.Credit = new Credit(initialCredit, minBet, maxBet);
            this.maxBet = maxBet;
            this.minBet = minBet;
            this.initialCredit = initialCredit;
        }

        public void GameBetSelect()
        {
            Console.WriteLine($"{name.ToUpper()}:");
            Console.WriteLine($"\tCredit:\t${Credit.Amount}");
            Credit.GetBet();
        }

        public void GamePrep()
        {
            NextGame = false;
            Hand.LoadHand();
        }

        private void ShowMoney()
        {
            Console.Clear();
            Console.Write("=== ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("RED DOG");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(" ===");
            Console.WriteLine("(c) Nathan Gill");
            Console.WriteLine();
            Console.WriteLine($"{name.ToUpper()}:");
            Console.WriteLine($"\tCredit:\t${Credit.Amount}\n");
        }

        public int SelectGameOption()
        {
            Menu m = new Menu(["Place Bet", "Leave"], ShowMoney, null);
            int result = m.ShowMenu();

            if (result == 1)
            {
                Console.Clear();
                Console.Write("=== ");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("RED DOG");
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(" ===");
                Console.WriteLine("(c) Nathan Gill");
                Console.WriteLine($"\n\tYou leave with ${Credit.Amount}.\n");
                Console.Write("Press ANY KEY to continue...");
                Console.ReadKey();
            }

            return result;
        }

        private void ShowCardDisplay()
        {
            Console.Clear();
            Console.Write("=== ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("RED DOG");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(" ===");
            Console.WriteLine("(c) Nathan Gill");
            Console.WriteLine();
            Console.WriteLine($"{name.ToUpper()}:");
            Console.WriteLine($"\tCards: {Hand.ToString()}\n");
        }

        private void ShowResult()
        {
            Console.Clear();
            Console.Write("=== ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("RED DOG");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(" ===");
            Console.WriteLine("(c) Nathan Gill");
            Console.WriteLine();
            Console.WriteLine($"{name.ToUpper()}:");
            Console.WriteLine($"\tCards: {Hand.ToString()}\n");
        }

        public void GamePlay()
        {
            HandResult hr = Hand.GetResult();

            if (hr.isPush)
            {
                Hand.Reveal();
                ShowResult();
                Credit.HandlePayout(hr);

                if (Credit.IsOut())
                {
                    Console.WriteLine("\n\nYou're out.\n");
                    Console.Write("Press ANY KEY to continue...");
                    Console.ReadKey();
                    this.GameOver = true;
                }

                NextGame = true;
                return;
            }

            Menu m = new Menu((Credit.CanDouble() ? ["Deal", "Double"] : ["Deal"]), ShowCardDisplay, null);
            int result = m.ShowMenu();

            switch (result)
            {
                case 0:
                    Hand.Reveal();
                    ShowResult();
                    Credit.HandlePayout(hr);

                    if (Credit.IsOut())
                    {
                        Console.WriteLine("\n\nYou're out.\n");
                        Console.Write("Press ANY KEY to continue...");
                        Console.ReadKey();
                        this.GameOver = true;
                    }

                    NextGame = true;
                    break;
                case 1:
                    Credit.Double();
                    break;
            }
        }
    }
}
