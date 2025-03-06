namespace RedDog
{
    public struct Bet
    {
        public double amount;
    };

    public class Credit(double initial, double minBet, double maxBet)
    {
        public double Amount { get; private set; } = initial;
        public Bet CurrentBet;

        public double MinBet { get; private set; } = minBet;
        public double MaxBet { get; private set; } = maxBet;

        public void GetBet()
        {
            while (true)
            {
                Console.Write("\tBet:\t$");
                string s = Console.ReadLine() ?? String.Empty;

                if (!double.TryParse(s, out double bet))
                {
                    Console.WriteLine("\n\tInvalid bet!\n");
                    continue;
                }

                if (bet < MinBet)
                {
                    Console.WriteLine($"\n\tBet must be at least ${MinBet}!\n");
                    continue;
                }

                if (bet > MaxBet && MaxBet != -1)
                {
                    Console.WriteLine($"\n\tBet can be at most ${MaxBet}!\n");
                    continue;
                }

                if (bet > Amount)
                {
                    Console.WriteLine("\n\tYou do not have enough credit to make a bet that large!\n");
                    continue;
                }

                Bet b = new Bet();
                b.amount = bet;
                this.CurrentBet = b;

                this.Amount -= bet;

                break;
            }
        }

        public bool CanDouble()
        {
            if (this.Amount >= CurrentBet.amount)
            {
                return true;
            }
            return false;
        }

        public void Double()
        {
            if (this.Amount >= CurrentBet.amount)
            {
                this.Amount -= CurrentBet.amount;
                CurrentBet.amount *= 2;

                Console.WriteLine($"\tYour bet is now ${this.CurrentBet.amount}.\n");

                Console.Write("Press ANY KEY to continue...");
                Console.ReadKey();
            }
        }

        public void HandlePayout(HandResult hand)
        {
            if (hand.win)
            {
                double total = (hand.payout + 1) * this.CurrentBet.amount;
                this.Amount += total;

                Console.WriteLine($"\tYou won ${hand.payout * this.CurrentBet.amount}.\n");
            }
            else if (hand.isPush)
            {
                this.Amount += this.CurrentBet.amount;
                Console.WriteLine("\tPush.\n");
            }
            else
            {
                Console.WriteLine($"\tYou lost ${this.CurrentBet.amount}.\n");
            }

            Console.Write("Press ANY KEY to continue...");
            Console.ReadKey();
        }

        public bool IsOut()
        {
            if (Amount < MinBet)
            {
                return true;
            }
            return false;
        }
    }
}
