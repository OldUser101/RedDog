using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedDog
{
    public class Pack
    {
        private Card[] cards = new Card[0];
        int nextCard;

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
            Card[] cards = this.cards;
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

            this.cards = newCards;
        }

        public void ResetCards()
        {
            cards = new Card[52];
            for (int i = 0; i < 52; i++)
            {
                cards[i] = new Card(i % 13, i % 4, scoreFunc);
            }

            this.nextCard = 0;
        }

        public List<Card> GetCards(int n)
        {
            if (this.nextCard + n >= 52) 
            {
                ShuffleCards();
                this.nextCard = 0;
            }

            List<Card> nextCard = new List<Card>();

            for (int i = 0; i < n; i++) 
            {
                nextCard.Add(this.cards[this.nextCard]);
                this.nextCard++;
            }

            return nextCard;
        }
    }
}
