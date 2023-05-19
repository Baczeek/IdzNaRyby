using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IdzNaRyby
{
    class Game
    {
        List<Player> players;
        Dictionary<Values, Player> books;
        Deck stock;
        TextBox textBoxOnForm;

        public Game(string playerName, IEnumerable<string> opponentNames, TextBox textBoxOnForm)
        {
            Random random = new Random();
            this.textBoxOnForm = textBoxOnForm;
            players = new List<Player>();
            players.Add(new Player(playerName, random, textBoxOnForm));
            foreach (string player in opponentNames)
                players.Add(new Player(player, random, textBoxOnForm));
            books = new Dictionary<Values, Player>();
            stock = new Deck();
            Deal();
            players[0].SortHand();
        }

        void Deal()         
        {
            stock.Shuffle();
            for (int j = 0; j < 5; j++)
            {
                foreach(Player player in players)
                {
                    player.TakeCard(stock.Deal());
                }
            }
            foreach(Player player in players)
            {
                PullOutBooks(player);
            }
        }

        public bool PlayOneRound(int selectedPlayerCard)
        {          
            for (int i = 0; i < 3; i++)
            {
                if (i == 0)
                    players[i].AskForACard(players, 0, stock, players[0].Peek(selectedPlayerCard).Value); 
                else
                    players[i].AskForACard(players, 1, stock);
                if (PullOutBooks(players[i]) == true)
                {
                    int card = 0;
                    while (stock.Count > 0 && card < 5)
                    {
                        players[i].TakeCard(stock.Deal());
                        card++;
                    }   
                }
            }
            players[0].SortHand();
            if (stock.Count == 0)
            {
                textBoxOnForm.Text = "Na kupce nie ma żadnych kart. Gra skończona!" + Environment.NewLine;
                return true;
            }
            else
            {
                return false;
            }
                
        }

        public bool PullOutBooks(Player player)
        {
            foreach(Values value in player.PullOutBooks())
                books.Add(value, player);
            if (player.CardCount == 0)
                return true;
            else
                return false;
        }

        public string DescribeBooks()
        {
            string textPlayer1 = "", textPlayer2 = "", textPlayer3 = "";
            string textToReturn;
            foreach(Values value in books.Keys)
            {
                if (books[value] == players[0])
                {
                    if (String.IsNullOrEmpty(textPlayer1))
                        textPlayer1 = players[0].Name + " ma grupę: ";
                    textPlayer1 += Card.Plural(value, 0) + " ";
                }
                if (books[value] == players[1])
                {
                    if (String.IsNullOrEmpty(textPlayer2))
                        textPlayer2 = players[1].Name + " ma grupę: ";
                    textPlayer2 += Card.Plural(value, 0) + " ";
                }
                if (books[value] == players[2])
                {
                    if (String.IsNullOrEmpty(textPlayer3))
                        textPlayer3 = players[2].Name + " ma grupę: ";
                    textPlayer3 += Card.Plural(value, 0) + " ";
                }
            }
            textToReturn = textPlayer1 + Environment.NewLine + textPlayer2 + Environment.NewLine + textPlayer3 + Environment.NewLine;
            return textToReturn;
        }

        public string GetWinnerName()
        {
            string toReturn = "";
            Dictionary<string, int> winners = new Dictionary<string, int>();
            winners.Add(players[0].Name, 0);
            winners.Add(players[1].Name, 0);
            winners.Add(players[2].Name, 0);
            foreach (Values value in books.Keys)
            {
                if (books[value].Name == players[0].Name)
                    winners[players[0].Name]++;
                if (books[value].Name == players[1].Name)
                    winners[players[1].Name]++;
                if (books[value].Name == players[2].Name)
                    winners[players[2].Name]++;
            }
            bool oneWinner = true;
            int biggestBooks = 0;
            foreach (string name in winners.Keys)
            {
                if (winners[name] > biggestBooks)
                {
                    biggestBooks = winners[name];
                    toReturn = name;
                    oneWinner = true;
                }
                else if (winners[name] == biggestBooks)
                {
                    oneWinner = false;    
                }
            }
            if (oneWinner == false)
            {
                toReturn = "Remis pomiędzy" ;
                foreach(string name in winners.Keys)
                {
                    if (winners[name] == biggestBooks)
                        toReturn += name + " i ";
                }
            }
            if (biggestBooks == 1) toReturn += ": " + biggestBooks + " grupa";
            else toReturn += ": " + biggestBooks + " grupy";
            return toReturn;
        }

        public IEnumerable<string> GetPlayerCardNames()
        {
            return players[0].GetCardNames();
        }

        public string DescribePlayerHands()
        {
            string description = "";
            for(int i = 0; i < players.Count; i++)
            {
                description += players[i].Name + " ma " + players[i].CardCount;
                if (players[i].CardCount == 1)
                    description += " kartę.\r\n";
                else if (players[i].CardCount == 2 || players[i].CardCount == 3 || players[i].CardCount == 4)
                    description += " karty.\r\n";
                else
                    description += " kart.\r\n";
            }
            description += "Na kupce pozostało: " + stock.Count + "\r\n";
            return description;
        }
    }
}



















