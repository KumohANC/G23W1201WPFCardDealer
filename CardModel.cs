using System;
using System.Linq;

namespace G23W1201WPFCardDealer
{
    public class CardModel
    {
        private int[] _cards = new int[] { -1, -1, -1, -1, -1 };

        public int[] Cards { get => _cards; }

        public void Shuffle()
        {
            Random r = new Random();
            int[] num = new int[5];

            for (int i = 0; i < 5; i++)
            {
                int n;
                do
                {
                    n = r.Next(52);
                }while(num.Contains(n));
                num[i] = n;
            }

            Array.Sort(num);
            _cards = num;
        }
    }
}
