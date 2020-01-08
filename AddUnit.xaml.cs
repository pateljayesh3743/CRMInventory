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
    /// Interaction logic for AddUnit.xaml
    /// </summary>
    public partial class AddUnit : Window
    {
        public AddUnit()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            UnitName.Focus();
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
            if (UnitName.Text != "" && ShortCode.Text != "")
            {
                using (invetoryEntities db = new invetoryEntities())
                {
                    db.unit_master.Add(new unit_master
                    {
                        name = UnitName.Text,
                        code = ShortCode.Text
                    });
                    db.SaveChanges();
                    MessageBox.Show("Unit save successfully.");
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
            if (UnitName.Text != "" && ShortCode.Text != "")
            {
                using (invetoryEntities db = new invetoryEntities())
                {
                    var UnitID = Convert.ToInt32(UnitId.Text);
                    if (db.unit_master.Where(x => x.id == UnitID).ToList().Count > 0)
                    {
                        var unit = db.unit_master.Where(x => x.id == UnitID).FirstOrDefault();
                        unit.name = UnitName.Text;
                        unit.code = ShortCode.Text;
                        db.SaveChanges();
                        MessageBox.Show("Unit updated successfully.");
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
            UnitId.Text = "";
            UnitName.Text = "";
            ShortCode.Text = "";
            btnModify.Visibility = Visibility.Hidden;
            btnSave.Visibility = Visibility.Visible;
        }

        private void UnitName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ShortCode.Focus();
            }
        }
        private void ShortCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (UnitId.Text != "")
                {
                    Button_Click_Modify(sender, e);
                }
                else {
                    Button_Click_Save(sender, e);
                }
            }
        }
    }
}
