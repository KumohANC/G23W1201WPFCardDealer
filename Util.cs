using System;
using System.Collections.Generic;
using System.Linq;

namespace G23W1201WPFCardDealer
{
    public static class Util
    {
        public static string GetSuit(int c)
        {
            string suit;
            switch (c / 13)
            {
                case 0:
                    suit = "spades";
                    break;
                case 1:
                    suit = "diamonds";
                    break;
                case 2:
                    suit = "hearts";
                    break;
                case 3:
                    suit = "clubs";
                    break;
                default:
                    suit = "error";
                    break;
            }
            return suit;

        }

        public static string GetString(POKER_RESULT pr, int[] array)
        {
            int[] numArray = new int[array.Length];
            for (int i = 0; i < numArray.Length; i++)
            {
                numArray[i] = (array[i] % 13) + 1;
            }
            switch (pr)
            {
                case POKER_RESULT.TOP:
                    if (numArray.Contains(1))
                        return "TOP(A)";

                    int value = numArray.Max();
                    switch (value)
                    {
                        case 11:
                            return $"TOP(J)";
                        case 12:
                            return $"TOP(Q)";
                        case 13:
                            return $"TOP(K)";
                        default:
                            return $"TOP({value})";
                    }
            }
            return pr.ToString().Replace("_", "-");
        }

        public static POKER_RESULT GetResult(int[] array)
        {
            int[] numbers = new int[array.Length];
            string[] suits = new string[array.Length];
            for (int i = 0; i < numbers.Length; i++)
            {
                suits[i] = GetSuit(array[i]);
                numbers[i] = (array[i] % 13) + 1;
            }

            List<int> sorted_array = numbers.OrderBy(x => x).ToList();
            if (suits.Count(x => !x.Equals(suits[0])) == 0)
            {
                if (numbers.Contains(1) &&
                    numbers.Contains(13) &&
                    numbers.Contains(10) &&
                    numbers.Contains(11) &&
                    numbers.Contains(12))
                    return POKER_RESULT.ROYAL_STRAIGHT_FLUSH; 

                if (numbers.Contains(1) &&
                    numbers.Contains(2) &&
                    numbers.Contains(3) &&
                    numbers.Contains(4) &&
                    numbers.Contains(5))
                    return POKER_RESULT.BACK_STRAIGHT_FLUSH;

                bool finded = false;
                for (int i = 0; i < 4; i++)
                {
                    if (sorted_array[i + 1] != sorted_array[i] + 1)
                    {
                        finded = true;
                        break;
                    }
                }
                if (!finded)
                    return POKER_RESULT.STRAIGHT_FLUSH;
                return POKER_RESULT.FLUSH;
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    if (numbers.Count(x => x == numbers[i]) == 4)
                    {
                        return POKER_RESULT.FOUR_CARD;
                    }
                }

                bool _3 = false;
                bool _2 = false;

                for (int i = 0; i < 5; i++)
                {
                    if (numbers.Count(x => x == numbers[i]) == 3)
                    {
                        _3 = true;
                    }
                    if (numbers.Count(x => x == numbers[i]) == 2)
                    {
                        _2 = true;
                    }
                }

                if (_3 && _2)
                    return POKER_RESULT.FULL_HOUSE;

                if (numbers.Contains(1) &&
                    numbers.Contains(13) &&
                    numbers.Contains(12) &&
                    numbers.Contains(11) &&
                    numbers.Contains(10))
                    return POKER_RESULT.MOUNTAIN;

                if (numbers.Contains(1) &&
                    numbers.Contains(2) &&
                    numbers.Contains(3) &&
                    numbers.Contains(4) &&
                    numbers.Contains(5))
                    return POKER_RESULT.BACK_STRAIGHT;

                bool finded = false;
                for (int i = 0; i < 4; i++)
                {
                    if (sorted_array[i + 1] != sorted_array[i] + 1)
                    {
                        finded = true;
                        break;
                    }
                }

                if (!finded)
                    return POKER_RESULT.STRAIGHT;


                for (int i = 0; i < 5; i++)
                {
                    if (numbers.Count(x => x == numbers[i]) == 3)
                    {
                        return POKER_RESULT.TRIPLE;
                    }
                }

                Dictionary<int, int> keyValuePairs = new Dictionary<int, int>();
                for (int i = 0; i < 5; i++)
                {
                    if (!keyValuePairs.ContainsKey(numbers[i]))
                        keyValuePairs[numbers[i]] = 0;

                    if (keyValuePairs[numbers[i]] < 2 && 
                        numbers.Count(x => x == numbers[i]) == 2)
                    {
                        keyValuePairs[numbers[i]]++;
                    }
                }

                if (keyValuePairs.Count(x => x.Value == 2) == 2)
                    return POKER_RESULT.TWO_PAIR;

                if (keyValuePairs.Count(x => x.Value == 2) == 1)
                    return POKER_RESULT.ONE_PAIR;
            }

            return POKER_RESULT.TOP;
        }

    }
}
