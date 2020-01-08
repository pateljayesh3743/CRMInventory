using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CRMInventory
{
    /// <summary>
    /// Interaction logic for test.xaml
    /// </summary>
    public partial class test : Window
    {
        List<string> stringCollection;
        public test()
        {
            InitializeComponent();
            stringCollection = new List<string>
            {
                "abc","ayr","bef","bcs","caa","lmn"

            };

            txtAuto.TextChanged += txtAuto_TextChanged;
        }
        void txtAuto_TextChanged(object sender, TextChangedEventArgs e)
        {
            string typedString = txtAuto.Text;

            List<string> autoList = new List<string>();

            autoList.Clear();

            foreach (string item in stringCollection)
            {
                if (!string.IsNullOrEmpty(txtAuto.Text))
                {

                    if (item.Contains(typedString))
                    {
                        autoList.Add(item);
                    }
                }
            }
            if (autoList.Count > 0)
            {
                lblSuggestion.ItemsSource = autoList;
                lblSuggestion.Visibility = Visibility.Visible;
            }
            else if (txtAuto.Text.Equals(""))
            {
                lblSuggestion.Visibility = Visibility.Collapsed;
                lblSuggestion.ItemsSource = null;
            }
            else
            {
                lblSuggestion.Visibility = Visibility.Collapsed;
                lblSuggestion.ItemsSource = null;
            }
        }

        void lblSuggestion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lblSuggestion.ItemsSource != null)
            {
                lblSuggestion.KeyDown += lblSuggestion_KeyDown;
            }
        }
        private void txtAuto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                lblSuggestion.Focus();
            }

        }
        private void lblSuggestion_KeyDown(object sender, KeyEventArgs e)
        {
            if (ReferenceEquals(sender, lblSuggestion))
            {
                if (e.Key == Key.Enter)
                {
                    txtAuto.Text = lblSuggestion.SelectedItem.ToString();
                    lblSuggestion.Visibility = Visibility.Collapsed;
                }

                if (e.Key == Key.Down)
                {
                    e.Handled = true;
                    lblSuggestion.Items.MoveCurrentToNext();
                }
                if (e.Key == Key.Up)
                {
                    e.Handled = true;
                    lblSuggestion.Items.MoveCurrentToPrevious();
                }
            }
        }

    }
}
