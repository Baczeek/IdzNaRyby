using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdzNaRyby
{
    public class Card
    {
        public Card(Suits suit, Values value)
        {
            Suit = suit;
            Value = value;
        }
        public Suits Suit;
        public Values Value;

        public string Name
        {
            get
            {
                return names[(int)Value] + " " + suits[(int)Suit];
            }
        }

        public override string ToString()
        {
            return Name;
        }

        static string[] suits = new string[] { "pik", "trefl", "karo", "kier" };
        static string[] names = new string[]
        {"", "As", "Dwójka", "Trojka", "Czwórka", "Piątka", "Szóstka", "Siódemka", "Ósemka", "Dziewiątka", "Dziesiątka", "Walet", "Dama", "Król"};

        static string[] names0 = new string[]
        {"", "asów", "dwójek", "trójek", "czwórek", "piątek", "szóstek", "siódemek", "ósemek", "dziewiątek", "dziesiątek", "waletów", "dam", "królów"};

        static string[] names1 = new string[]
        {"", "asa", "dwójkę", "trójkę", "czwórkę", "piątkę", "szóstkę", "siódemkę", "ósemkę", "dziewiątkę", "dziesiątkę", "waleta", "damę", "króla"};

        static string[] names2AndMore = new string[]
        {"", "asy", "dwójki", "trójki", "czwórki", "piątki", "szóstki", "siódemki", "ósemki", "dziewiątki", "dziesiątki", "walety", "damy", "króle"};

        public static string Plural(Values value, int count)
        {
            if (count == 0)
                return names0[(int)value];
            if (count == 1)
                return names1[(int)value];
            return names2AndMore[(int)value]; 
        }
    }
}
