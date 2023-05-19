using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdzNaRyby
{
    public class CardComparerRandom : IComparer<Card>
    {
        Random random = new Random();
        public int Compare(Card x, Card y)
        {
            return random.Next(-1, 1);
        }
    }
}
