using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedDog
{
    public struct Bet {
        public double amount;
    };

    public class Credit(double initial, double minBet, double maxBet)
    {
        public double Amount { get; private set; } = initial;
        public Bet CurrentBet { get; private set; }

        private double minBet = minBet;
        private double maxBet = maxBet;

        public void GetBet() 
        {
            while (true)
            {
                Console.WriteLine("Enter your bet: $");
                string s = Console.ReadLine() ?? String.Empty;

                if (!double.TryParse(s, out double bet)) 
                {
                    Console.WriteLine("Invalid bet!");
                    continue;
                }

                if (bet < minBet || bet > maxBet) 
                {
                    Console.WriteLine($"Bet must be at least ${minBet} and at most ${maxBet}!");
                    continue;
                }

                if (bet > Amount) 
                {
                    Console.WriteLine("You do not have enough credit to make a bet that large!");
                    continue;
                }

                Bet b = new Bet();
                b.amount = bet;
                this.CurrentBet = b;

                this.Amount -= bet;

                break;
            }
        }

        public void HandlePayout(HandResult hand) 
        {
            double total = (hand.payout + 1) * this.CurrentBet.amount;

            this.Amount += total;
        }
    }
}
