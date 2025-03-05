using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedDog
{
    public struct HandResult {
        public int spread;
        public bool isPush;
        public int payout;
    };

    public class Hand
    {
        private Card LowerCard = new Card(0, 0);
        private Card MiddleCard = new Card(0, 0);
        private Card UpperCard = new Card(0, 0);

        private bool revealed;
        private Pack p;

        public Hand() 
        {
            this.p = new Pack(autoshuffle:true);
            this.revealed = false;
        }

        private static int SortCards(Card a, Card b) 
        {
            if (a.Rank > b.Rank)
            {
                return 1;
            }
            else if (a.Rank < b.Rank)
            {
                return -1;
            }
            else 
            {
                return 0;
            }
        }

        public void LoadHand() 
        {
            List<Card> c = p.GetCards(3);

            c.Sort(SortCards);

            this.LowerCard = c[0];
            this.MiddleCard = c[1];
            this.UpperCard = c[2];
            this.revealed = false;
        }

        public void Reveal() 
        {
            this.revealed = true;
        }

        public HandResult GetResult() 
        {
            if (!revealed) 
            {
                throw new Exception("Not revealed");
            }

            HandResult hr = new HandResult();

            hr.spread = UpperCard.Rank - LowerCard.Rank - 1;
            hr.isPush = false;
            hr.payout = 1;

            if (hr.spread == 0)
            {
                hr.isPush = true;
                hr.payout = 0;
            }
            else if (hr.spread == -1 && MiddleCard.Rank != UpperCard.Rank)
            {
                hr.isPush = true;
                hr.payout = 0;
            }
            else if (hr.spread == -1 && MiddleCard.Rank == UpperCard.Rank) 
            {
                hr.payout = 11;
            }

            switch (hr.spread) 
            {
                case 1:
                    hr.payout = 5;
                    break;
                case 2:
                    hr.payout = 4;
                    break;
                case 3:
                    hr.payout = 2;
                    break;
            }

            return hr;
        }

        public override string ToString()
        {
            if (revealed)
            {
                return $"{LowerCard.RankAsString()} {MiddleCard.RankAsString()} {UpperCard.RankAsString()}";
            }
            else 
            {
                return $"{LowerCard.RankAsString()} ????? {UpperCard.RankAsString()}";
            }
        }
    }
}
