namespace RedDog
{
    public class Pack
    {
        public Card[] Cards { get; private set; } = new Card[0];
        public int NextCard { get; private set; }

        private readonly Func<int, int, int>? scoreFunc;

        public Pack(bool autoshuffle = true, Func<int, int, int>? autoscore = null)
        {
            this.scoreFunc = autoscore;
            this.ResetCards();
            if (autoshuffle)
            {
                this.ShuffleCards();
            }
        }

        public void ShuffleCards()
        {
            Card[] cards = this.Cards;
            Card[] newCards = new Card[52];

            Random r = new Random();

            // Perform the equivalent of a 'riffle' shuffle
            int lCards = 52 / 2;
            int rCards = 52 / 2;
            for (int i = 0; i < 52; i++)
            {
                Card selectedCard;

                if (lCards == 0)
                {
                    selectedCard = cards[rCards + (52 / 2) - 1];
                    rCards--;
                }
                else if (rCards == 0)
                {
                    selectedCard = cards[lCards - 1];
                    lCards--;
                }
                else
                {
                    int side = r.Next() % 2;
                    if (side == 1)
                    {
                        selectedCard = cards[lCards - 1];
                        lCards--;
                    }
                    else
                    {
                        selectedCard = cards[rCards + (52 / 2) - 1];
                        rCards--;
                    }
                }

                newCards[i] = selectedCard;
            }

            this.Cards = newCards;
        }

        public void ResetCards()
        {
            Cards = new Card[52];
            for (int i = 0; i < 52; i++)
            {
                Cards[i] = new Card(i % 13, i % 4, scoreFunc);
            }

            this.NextCard = 0;
        }

        public List<Card> GetCards(int n)
        {
            if (this.NextCard + n >= 52)
            {
                ShuffleCards();
                this.NextCard = 0;
            }

            List<Card> nextCard = new List<Card>();

            for (int i = 0; i < n; i++)
            {
                nextCard.Add(this.Cards[this.NextCard]);
                this.NextCard++;
            }

            return nextCard;
        }
    }
}
