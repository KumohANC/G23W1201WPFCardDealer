using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace G23W1201WPFCardDealer
{
    public partial class MainWindow : Window
    {
        public static string Result = string.Empty;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = vm;
        }

        public CardViewModel vm = new CardViewModel();

        private void OnSimulation(object sender, RoutedEventArgs e)
        {
            InputWindow ip = new InputWindow();
            ip.ShowDialog();
            if (int.TryParse(Result, out int result))
            {
                Dictionary<string, int> keyValuePairs = new Dictionary<string, int>();
                for (int i = 0; i < result; i++)
                {
                    vm.Shuffle();
                    if (!keyValuePairs.ContainsKey(vm.CardResult))
                        keyValuePairs.Add(vm.CardResult, 0);
                    keyValuePairs[vm.CardResult]++;
                }
                StringBuilder sb = new StringBuilder();
                var ordered_dictionary = keyValuePairs.OrderByDescending(x => x.Value);
                foreach (var i in ordered_dictionary)
                {
                    sb.AppendLine($"{i.Key}: {i.Value}");
                }

                MessageBox.Show(sb.ToString(), "횟수");

                sb.Clear();

                foreach (var i in ordered_dictionary)
                {
                    sb.AppendLine($"{i.Key}: {((double)(i.Value / (double)result) * 100).ToString("0.00")}%");
                }
                MessageBox.Show(sb.ToString(), "확률");
            }
        }

        private void OnDeal(object sender, RoutedEventArgs e)
        {
            vm.Shuffle();
        }
    }
}