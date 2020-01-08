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
    /// Interaction logic for AddGodown.xaml
    /// </summary>
    public partial class AddGodown : Window
    {
        public AddGodown()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            GodownName.Focus();
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
            if (GodownName.Text != "" && GodownName.Text != "")
            {
                using (invetoryEntities db = new invetoryEntities())
                {
                    db.godown_master.Add(new godown_master
                    {
                        name = GodownName.Text,
                        Address = Address.Text
                    });
                    db.SaveChanges();
                    MessageBox.Show("Godown save successfully.");
                    Clear_Form();
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
            GodownId.Text = "";
            GodownName.Text = "";
            Address.Text = "";
            btnModify.Visibility = Visibility.Hidden;
            btnSave.Visibility = Visibility.Visible;
        }

        private void Button_Click_Modify(object sender, RoutedEventArgs e)
        {
            if (GodownName.Text != "" && GodownName.Text != "")
            {
                using (invetoryEntities db = new invetoryEntities())
                {
                    var GodownID = Convert.ToInt32(GodownId.Text);
                    if (db.godown_master.Where(x => x.id == GodownID).ToList().Count > 0)
                    {
                        var godown = db.godown_master.Where(x => x.id == GodownID).FirstOrDefault();
                        godown.name = GodownName.Text;
                        godown.Address = Address.Text;
                        db.SaveChanges();
                        MessageBox.Show("Godown updated successfully.");
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

        private void GodownName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Address.Focus();
            }
        }

        private void Address_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (GodownId.Text != "")
                {
                    Button_Click_Modify(sender, e);
                    this.Close();

                    //Godown godown = new Godown();
                    //godown.datagrid.ItemsSource = godown.GetGodown();
                }
                else
                {
                    Button_Click_Save(sender, e);
                }
            }
        }
    }
}
