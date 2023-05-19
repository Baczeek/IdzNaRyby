using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IdzNaRyby
{
    class Player
    {
        string name;
        public string Name {  get { return name; } }
        Random random;
        Deck cards;
        TextBox textBoxOnForm;

        public Player(String name, Random random, TextBox textBoxOnForm)
        {
            this.name = name;
            this.random = random;
            this.textBoxOnForm = textBoxOnForm;
            this.textBoxOnForm.Text +=  name + " dołączył do gry" + Environment.NewLine;
            this.cards = new Deck(new Card[] { });
        }

        public IEnumerable<Values> PullOutBooks()
        {
            List<Values> books = new List<Values>();
            for (int i = 1; i <=13; i++)
            {
                Values value = (Values)i;
                int howMany = 0;
                for (int card = 0; card < cards.Count; card++)
                    if (cards.Peek(card).Value == value)
                        howMany++;
                if(howMany == 4)
                {
                    books.Add(value);
                    for (int card = cards.Count - 1; card >= 0; card--)
                        if (cards.Peek(card).Value == value)
                            cards.Deal(card);
                }
            }
            return books;
        }

        public Values GetRandomValue()
        {
            Card randomCard = cards.Peek(random.Next(cards.Count));
            return randomCard.Value;
        }

        public Deck DoYouHaveAny(Values value)
        {
            if (cards.ContainsValue(value))
            {
                Deck deckToReturn = cards.PullOutValues(value);
                textBoxOnForm.Text += name + " ma " + deckToReturn.Count + " " + Card.Plural(value, deckToReturn.Count) + Environment.NewLine;
                return deckToReturn;
            }
            else
            {
                textBoxOnForm.Text += name + " ma " + 0 + " " + Card.Plural(value, 0) + Environment.NewLine;
            }
            return null;
        }

        public void AskForACard(List<Player> players, int myIndex, Deck stock)
        {
            if (stock.Count > 0)
                if (cards.Count == 0)
                    cards.Add(stock.Deal());
            AskForACard(players, myIndex, stock, GetRandomValue());
        }

        public void AskForACard(List<Player> players, int myIndex, Deck stock, Values value)
        {
            textBoxOnForm.Text += name + " pyta, czy ktoś ma " + Card.Plural(value, 1) + Environment.NewLine;
            bool noCard = true;
            foreach(Player player in players)
            {
                if(player.Name != this.Name)
                {
                    Deck tempDeck = player.DoYouHaveAny(value);
                    if (tempDeck != null)
                    {
                        if (tempDeck.Count > 0)
                        {
                            noCard = false;
                            while (tempDeck.Count > 0)
                            {
                                TakeCard(tempDeck.Deal());
                            }
                        }
                    }
                }
            }
            if (noCard == true)
            {
                cards.Add(stock.Deal());
                textBoxOnForm.Text += name + " pobrał karte z kupki." + Environment.NewLine;
            }
  
        }

        public override string ToString()
        {
            return this.name;
        }

        public int CardCount { get { return cards.Count; } }
        public void TakeCard(Card card) { cards.Add(card); }
        public IEnumerable<string> GetCardNames() { return cards.GetCardNames(); }
        public Card Peek(int cardNumber) { return cards.Peek(cardNumber); }
        public void SortHand() { cards.SortByValue(); }

    }
}
