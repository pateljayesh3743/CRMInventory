using CRMInventory.Model;
using CRMInventory.ViewModel;
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
    /// Interaction logic for AddLedger.xaml
    /// </summary>
    public partial class AddLedger : Window
    {
        public AddLedger()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            this.DataContext = new LedgerViewModel();
            LedgerName.Focus();
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

        private void LedgerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                UnderName.Focus();
        }
        private void UnderName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                BillingStyle.Focus();
        }

        private void BillingStyle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                OpeningBalance.Focus();
        }

        private void OpeningBalance_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                CreditDebitType.Focus();
        }

        private void CreditDebitType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                PrintName.Focus();
        }

        private void PrintName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                OwnerName.Focus();
        }

        private void OwnerName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Address.Focus();
        }

        private void Address_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                State.Focus();
        }

        private void State_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                PinCode.Focus();
        }

        private void PinCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Mobile.Focus();
        }

        private void Mobile_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                AadharNo.Focus();
        }

        private void AadharNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                EmailID.Focus();
        }

        private void EmailID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Area.Focus();
        }

        private void Area_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Agent.Focus();
        }

        private void Agent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Interest.Focus();
        }

        private void Interest_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                CreditPeriod.Focus();
        }

        private void CreditPeriod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                LessPeriod.Focus();
        }

        private void LessPeriod_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                InterestRate.Focus();
        }

        private void InterestRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Description.Focus();
        }

        private void Description_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Save();
        }

        private void Save()
                {
            try
            {
                if (LedgerName.Text != "")
                {
                    using (invetoryEntities db = new invetoryEntities())
                    {
                        ledger_master ledger_Master = new ledger_master
                        {
                            name = LedgerName.Text,
                            under_id = Convert.ToInt32(UnderName.SelectedValue),
                            billing_style = BillingStyle.Text,
                            opening_balance = Convert.ToDecimal(OpeningBalance.Text),
                            credit_debit_type = Convert.ToInt32(CreditDebitType.SelectedValue),
                            print_name = PrintName.Text,
                            owner_name = OwnerName.Text,
                            address = Address.Text,
                            state = 1,
                            pin_code = PinCode.Text,
                            mobile = Mobile.Text,
                            aadhar_no = AadharNo.Text,
                            email = EmailID.Text,
                            area = Area.Text,
                            agent = 1,
                            interest = Convert.ToBoolean(Interest.SelectedValue),
                            credit_priod = 1,
                            less_priod = 1,
                            interest_rate = 1,
                            description = Description.Text,
                            company_id = 1,
                            created_on = DateTime.Now,
                        };
                        db.ledger_master.Add(ledger_Master);
                        db.SaveChanges();
                        MessageBox.Show("Ledger saved successfully.");
                    }
                }
                else
                {
                    MessageBox.Show("Please fill compulsory data.");
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var cmbx = sender as ComboBox;
            var data = LedgerViewModel.GetUnderCollection();
            cmbx.ItemsSource = from item in data
                               where item.UnderName.ToLower().Contains(cmbx.Text.ToLower())
                               select item;
            cmbx.IsDropDownOpen = true;
        }
    }
}
