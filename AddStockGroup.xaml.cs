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
    public partial class AddStockGroup : Window
    {
        public AddStockGroup()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            StockGroupName.Focus();
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
            if (StockGroupName.Text != "")
            {
                using (invetoryEntities db = new invetoryEntities())
                {
                    db.stock_group_master.Add(new stock_group_master
                    {
                        name = StockGroupName.Text
                    });
                    db.SaveChanges();
                    MessageBox.Show("Stock Group save successfully.");
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
            if (StockGroupName.Text != "")
            {
                using (invetoryEntities db = new invetoryEntities())
                {
                    var StockGroupID = Convert.ToInt32(StockGroupId.Text);
                    if (db.stock_group_master.Where(x => x.id == StockGroupID).ToList().Count > 0)
                    {
                        var stockgroup = db.stock_group_master.Where(x => x.id == StockGroupID).FirstOrDefault();
                        stockgroup.name = StockGroupName.Text;
                        db.SaveChanges();
                        MessageBox.Show("Stock Group updated successfully.");
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
            StockGroupId.Text = "";
            StockGroupName.Text = "";
            btnModify.Visibility = Visibility.Hidden;
            btnSave.Visibility = Visibility.Visible;
        }

        private void StockGroupName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (StockGroupId.Text != "")
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
