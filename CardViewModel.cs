using System;
using System.ComponentModel;

namespace G23W1201WPFCardDealer
{
    public class CardViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public CardModel Card = new CardModel();
        private string[] _cardResource;

        public string CardResult { get; set; } = string.Empty;

        public string[] CardResource
        {
            get { return _cardResource; }
        }

        public CardViewModel() 
        {
            _cardResource = new string[Card.Cards.Length];
            GetResourceName();
            OnPropertyChanged(nameof(CardResource));
            OnPropertyChanged(nameof(CardResult));
        }

        public void Shuffle()
        {
            Card.Shuffle();

            GetResourceName();

            OnPropertyChanged(nameof(CardResource));
            OnPropertyChanged(nameof(CardResult));
        }

        private void GetResourceName()
        {
            for (int i = 0; i < _cardResource.Length; i++)
                _cardResource[i] = GetCardName(Card.Cards[i]);

            CardResult = Util.GetString(Util.GetResult(Card.Cards), Card.Cards);
        }

        private string GetCardName(int c)
        {
            if (c < 0)
                return "Images/black_joker.png";

            string suit = "";
            switch (c / 13)
            {
                case 0: suit = "spades"; break;
                case 1: suit = "diamonds"; break;
                case 2: suit = "hearts"; break;
                case 3: suit = "clubs"; break;
            }

            string rank = "";
            switch (c % 13)
            {
                case 0: rank = "ace"; break;
                case int n when (n > 0 && n < 10):
                    rank = (c % 13 + 1).ToString(); break;
                case 10: rank = "jack"; break;
                case 11: rank = "queen"; break;
                case 12: rank = "king"; break;
            }

            if (Array.Exists(new int[] { 10, 11, 12 }, x => x == c % 13))
                return string.Format("Images/{0}_of_{1}2.png", rank, suit);
            else
                return string.Format("Images/{0}_of_{1}.png", rank, suit);
        }

        private void OnPropertyChanged(string propName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
