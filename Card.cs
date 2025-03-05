using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedDog
{
    public class Card(int rank, int suit, Func<int, int, int>? autoscore = null)
    {
        public int Rank { get; private set; } = rank;
        public int Suit { get; private set; } = suit;

        private readonly string[] RANKINGS = { "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Jack", "Queen", "King", "Ace"};
        private readonly string[] SUITS = { "Spades", "Hearts", "Diamonds", "Clubs" };

        private readonly Func<int, int, int>? score = autoscore;

        public int GetScore()
        {
            if (score is null)
            {
                throw new NullReferenceException($"GetScore (auto) called with no auto-score function set.");
            }

            return score(this.Rank, this.Suit);
        }

        public int GetScore(Func<int, int, int> scoreFunction)
        {
            if (scoreFunction is null)
            {
                throw new NullReferenceException($"GetScore called with score function set to null.");
            }

            return scoreFunction(this.Rank, this.Suit);
        }

        public string RankAsString()
        {
            return RANKINGS[this.Rank];
        }

        public string SuitAsString()
        {
            return SUITS[this.Suit];
        }

        public override string ToString()
        {
            return $"{this.RankAsString()} of {this.SuitAsString()}";
        }
    }
}
