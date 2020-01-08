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
    /// Interaction logic for AddCompany.xaml
    /// </summary>
    public partial class AddCompany : Window
    {
        public AddCompany()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            Name.Focus();
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
        private void Button_Click_Clear(object sender, RoutedEventArgs e)
        {
            Creal_Form();
        }

        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            using (invetoryEntities db = new invetoryEntities())
            {
                db.company_master.Add(new company_master
                {
                    name = Name.Text,
                    address = Address.Text,
                    finanacial_year = Financialyear.SelectedDate.Value,
                    pin_no = PINNo.Text
                });
                db.SaveChanges();
                MessageBox.Show("Company save successfully.");
                Creal_Form();
            }
        }

        private void Creal_Form()
        {
            Name.Text = "";
            City.Text = "";
            Address.Text = "";
            State.Text = "";
            PINNo.Text = "";
            Phone1.Text = "";
            Phone2.Text = "";
            Mobile.Text = "";
            Website.Text = "";
            TypeofBusiness.Text = "";
            EmailID.Text = "";
        }

        private void Name_KeyDown(object sender, KeyEventArgs e)
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
                City.Focus();
            }
        }

        private void City_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                State.Focus();
            }
        }

        private void State_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                PINNo.Focus();
            }
        }

        private void PINNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Phone1.Focus();
            }
        }

        private void Phone1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Phone2.Focus();
            }
        }

        private void Phone2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Mobile.Focus();
            }
        }

        private void Mobile_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Website.Focus();
            }
        }

        private void Website_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                EmailID.Focus();
            }
        }

        private void EmailID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                RegistrationType.Focus();
            }
        }

        private void RegistrationType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TypeofBusiness.Focus();
            }
        }

        private void TypeofBusiness_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Financialyear.Focus();
            }
        }

        private void Financialyear_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CSTNO.Focus();
            }
        }

        private void CSTNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CSTDateFrom.Focus();
            }
        }

        private void CSTDateFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TINNo.Focus();
            }
        }

        private void TINNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TINDateFrom.Focus();
            }
        }

        private void TINDateFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                GSTINNo.Focus();
            }
        }

        private void GSTINNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                GSTINDateFrom.Focus();
            }
        }

        private void GSTINDateFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                DLNo.Focus();
            }
        }

        private void DLNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                FLNO.Focus();
            }
        }

        private void FLNO_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TANNo.Focus();
            }
        }

        private void TANNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                PANNo.Focus();
            }
        }

        private void PANNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_Click_Save(sender, e);
            }
        }
    }
}
