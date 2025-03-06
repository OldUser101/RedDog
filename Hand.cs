namespace RedDog
{
    public struct HandResult
    {
        public int spread;
        public bool isPush;
        public bool win;
        public int payout;
    };

    public class Hand
    {
        public Card LowerCard { get; private set; } = new Card(0, 0);
        public Card MiddleCard { get; private set; } = new Card(0, 0);
        public Card UpperCard { get; private set; } = new Card(0, 0);

        public bool Revealed { get; private set; }
        public Pack Pack { get; private set; }

        public Hand()
        {
            this.Pack = new Pack();

            // Shuffle 5 times
            for (int i = 0; i < 5; i++)
            {
                Pack.ShuffleCards();
            }

            this.Revealed = false;
        }

        public void LoadHand()
        {
            List<Card> c = Pack.GetCards(3);

            this.LowerCard = c[0];
            this.MiddleCard = c[1];
            this.UpperCard = c[2];

            if (this.LowerCard.Rank > this.UpperCard.Rank)
            {
                Card cu = this.UpperCard;
                this.UpperCard = this.LowerCard;
                this.LowerCard = cu;
            }

            this.Revealed = false;
        }

        public void Reveal()
        {
            this.Revealed = true;
        }

        public HandResult GetResult()
        {
            HandResult hr = new HandResult();

            hr.spread = UpperCard.Rank - LowerCard.Rank - 1;
            hr.isPush = false;
            hr.payout = 1;

            if (LowerCard.Rank < MiddleCard.Rank && MiddleCard.Rank < UpperCard.Rank)
            {
                hr.win = true;
            }
            else
            {
                hr.win = false;
            }

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
            if (Revealed)
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
