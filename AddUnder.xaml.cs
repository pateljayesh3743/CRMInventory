using CRMInventory.Model;
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
    /// Interaction logic for AddStockGroup.xaml
    /// </summary>
    public partial class AddUnder : Window
    {
        public AddUnder()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            UnderName.Focus();
        }
        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure close this windown?", "Window Close Confirmation", MessageBoxButton.YesNo);

                if (messageBoxResult == MessageBoxResult.Yes)  // error is here
                {
                    this.Close();
                }
            }
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            if (UnderName.Text != "")
            {
                using (invetoryEntities db = new invetoryEntities())
                {
                    db.under_master.Add(new under_master
                    {
                        name = UnderName.Text
                    });
                    db.SaveChanges();
                    MessageBox.Show("Under saved successfully.");
                    Clear_Form();
                }
            }
            else
            {
                MessageBox.Show("Please fill compulsory data.");
            }
        }

        private void Button_Click_Modify(object sender, RoutedEventArgs e)
        {
            if (UnderName.Text != "")
            {
                using (invetoryEntities db = new invetoryEntities())
                {
                    var UnderID = Convert.ToInt32(UnderId.Text);
                    if (db.under_master.Where(x => x.id == UnderID).ToList().Count > 0)
                    {
                        var under = db.under_master.Where(x => x.id == UnderID).FirstOrDefault();
                        under.name = UnderName.Text;
                        db.SaveChanges();
                        MessageBox.Show("Under updated successfully.");
                        Clear_Form();
                    }
                    else
                    {
                        MessageBox.Show("Something went wrong please try again.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please fill compulsory data.");
            }
        }


        private void Button_Click_Clear(object sender, RoutedEventArgs e)
        {
            Clear_Form();
        }
        private void Clear_Form()
        {
            UnderId.Text = "";
            UnderName.Text = "";
            btnModify.Visibility = Visibility.Hidden;
            btnSave.Visibility = Visibility.Visible;
        }

        private void UnderName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (UnderId.Text != "")
                {
                    Button_Click_Modify(sender, e);
                    this.Close();
                }
                else
                {
                    Button_Click_Save(sender, e);
                }
            }
        }
    }
}
