using CRMInventory.Model;
using CRMInventory.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CRMInventory
{
    /// <summary>
    /// Interaction logic for AddSalesInvoice.xaml
    /// </summary>
    public partial class AddPurchase : Window
    {
        List<SalesInvoiceCollectionViewModel> salesInvoiceCollectionViewModels=new List<SalesInvoiceCollectionViewModel>();
        public AddPurchase()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            PurchaseInvoiceDataGrid.ItemsSource = salesInvoiceCollectionViewModels;
            DataContext = new PurchaseViewModel();
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
        private void SerialNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                BookNo.Focus();
        }
        private void BookNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                PurchaseInvoiceDate.Focus();
                PurchaseInvoiceDate.IsDropDownOpen = true;
            }
        }
        private void SaleInvoiceDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Send(Key.Tab);
        }
        private void PartyName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                SupplierInvoiceNo.Focus();
        }
        private void SupplierInvoiceNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Date.Focus();
                Date.IsDropDownOpen = true;
            }
        }
        private void Date_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Date.IsDropDownOpen = false;
                PurchaseInvoiceDataGrid.Focus();
            }
        }
       
        private void SalesInvoiceDataGrid_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                e.Handled = true;
                DataGrid grid = (DataGrid)sender;
                int col = grid.CurrentCell.Column.DisplayIndex;
                DataGridCellInfo cell = PurchaseInvoiceDataGrid.SelectedCells[0];
                int rowIndex = PurchaseInvoiceDataGrid.Items.IndexOf(cell.Item);
                int row = rowIndex;
                List<SalesInvoiceCollectionViewModel> salesInvoiceCollectionViewModels = (List<SalesInvoiceCollectionViewModel>)PurchaseInvoiceDataGrid.ItemsSource;
                if (col < grid.Columns.Count - 1)
                {
                    if (!string.IsNullOrEmpty(salesInvoiceCollectionViewModels[row - 1].name))
                    {
                        ProductViewModel productmodel = GetProductById(Convert.ToInt32(salesInvoiceCollectionViewModels[row - 1].name));

                        if (string.IsNullOrEmpty(salesInvoiceCollectionViewModels[row - 1].rate))
                             salesInvoiceCollectionViewModels[row - 1].rate = productmodel.MRP; 

                        salesInvoiceCollectionViewModels[row - 1].per = productmodel.Unit;
                        
                            if (string.IsNullOrEmpty(salesInvoiceCollectionViewModels[row - 1].grossnet) && string.IsNullOrEmpty(salesInvoiceCollectionViewModels[row - 1].less))
                            {
                                salesInvoiceCollectionViewModels[row - 1].netweight = "00.00 " + productmodel.Unit;
                            }
                            else if (!string.IsNullOrEmpty(salesInvoiceCollectionViewModels[row - 1].grossnet) && string.IsNullOrEmpty(salesInvoiceCollectionViewModels[row - 1].less))
                            {
                                salesInvoiceCollectionViewModels[row - 1].netweight = salesInvoiceCollectionViewModels[row - 1].grossnet + productmodel.Unit;
                            }
                            else if (!string.IsNullOrEmpty(salesInvoiceCollectionViewModels[row - 1].grossnet) && !string.IsNullOrEmpty(salesInvoiceCollectionViewModels[row - 1].less))
                            {
                                salesInvoiceCollectionViewModels[row - 1].netweight = (Convert.ToDouble(salesInvoiceCollectionViewModels[row - 1].grossnet)- Convert.ToDouble(salesInvoiceCollectionViewModels[row - 1].less)) + productmodel.Unit;
                            }
                            else
                            {
                                salesInvoiceCollectionViewModels[row - 1].netweight = "00.00 " + productmodel.Unit;
                            }
                            
                        
                        salesInvoiceCollectionViewModels[row - 1].amount =
                            ((Convert.ToDecimal(salesInvoiceCollectionViewModels[row - 1].rate))
                            * (Convert.ToDecimal(salesInvoiceCollectionViewModels[row - 1].netweight.Replace(productmodel.Unit, "")))).ToString();

                        double sum = 0;
                        for (int i = 0; i < PurchaseInvoiceDataGrid.Items.Count-1; i++)
                        {
                            sum += Convert.ToDouble(salesInvoiceCollectionViewModels[i].amount);
                        }
                        GrossAmount.Text = sum.ToString();
                        this.CalculateNetAmount();

                        salesInvoiceCollectionViewModels[row - 1].sr = row;
                        PurchaseInvoiceDataGrid.ItemsSource = salesInvoiceCollectionViewModels;
                        PurchaseInvoiceDataGrid.Items.Refresh();
                    }

                    if (col == 4)
                    {
                        grid.CurrentCell = new DataGridCellInfo(
                        grid.Items[row - 1], grid.Columns[6]);
                        grid.BeginEdit();
                    }
                    else {
                        grid.CurrentCell = new DataGridCellInfo(
                        grid.Items[row - 1], grid.Columns[col + 1]);
                        grid.BeginEdit();
                    }
                }
                else if (col == grid.Columns.Count - 1)
                {
                    if (!string.IsNullOrEmpty(salesInvoiceCollectionViewModels[row - 1].name))
                    {
                        grid.CurrentCell = new DataGridCellInfo(
                        grid.Items[rowIndex++], grid.Columns[1]);
                        grid.BeginEdit();
                    }
                    else {
                        GrossAmount.Focus();
                    } 
                }
            }
        }

        public static void Send(Key key)
        {
            if (Keyboard.PrimaryDevice != null)
            {
                if (Keyboard.PrimaryDevice.ActiveSource != null)
                {
                    var e = new KeyEventArgs(Keyboard.PrimaryDevice, Keyboard.PrimaryDevice.ActiveSource, 0, key)
                    {
                        RoutedEvent = Keyboard.KeyDownEvent
                    };
                    InputManager.Current.ProcessInput(e);

                    // Note: Based on your requirements you may also need to fire events for:
                    // RoutedEvent = Keyboard.PreviewKeyDownEvent
                    // RoutedEvent = Keyboard.KeyUpEvent
                    // RoutedEvent = Keyboard.PreviewKeyUpEvent
                }
            }
        }
        private void GrossAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Paking.Focus();
        }
        private void Paking_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.CalculateNetAmount();
                Dis.Focus();
            }
        }
        private void Dis_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.CalculateNetAmount();
                OtherCharges.Focus();
            }
        }
        private void OtherCharges_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.CalculateNetAmount();
                RoundOff.Focus();
            }
        }
        private void RoundOff_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.CalculateNetAmount();
                NetAmount.Focus();
            }
        }
        private void NetAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Save();
        }
        private void Save()
        {
            if (SerialNo.Text != null && SerialNo.Text != "")
            {
                using (invetoryEntities db = new invetoryEntities())
                {
                    purchase_master purchase_master = new purchase_master
                    {
                        serial_no = Convert.ToInt32(SerialNo.Text),
                        book_no = Convert.ToInt32(BookNo.Text),
                        purchase_date = PurchaseInvoiceDate.SelectedDate,
                        party_name_id = Convert.ToInt32(PartyName.SelectedValue),
                        supplier_invoice_no = Convert.ToInt32(SupplierInvoiceNo.Text),
                        purchase_date1 = Date.SelectedDate,
                        gross_amount = Convert.ToDecimal(GrossAmount.Text),
                        paking_amount = Convert.ToDecimal(Paking.Text),
                        dis_amount = Convert.ToDecimal(Dis.Text),
                        other_charges = Convert.ToDecimal(OtherCharges.Text),
                        round_off = Convert.ToDecimal(RoundOff.Text),
                        net_amount = Convert.ToDecimal(NetAmount.Text),
                        status = 1,
                        created_by = 1,
                        company_id = 1,
                        created_on = DateTime.Now,
                    };
                    db.purchase_master.Add(purchase_master);
                    db.SaveChanges();

                    db.purchase_expenses.Add(new purchase_expenses {
                        purchase_id= purchase_master.purchase_id,
                        adhat =Convert.ToDecimal(Adhat.Text),
                        taulai = Convert.ToDecimal(Taulai.Text),
                        majoori = Convert.ToDecimal(Majdoori.Text),
                        packing = Convert.ToDecimal(Packing.Text),
                        mandi_sulk = Convert.ToDecimal(MandiSulk.Text),
                        vikash_sesh = Convert.ToDecimal(VikashSesh.Text),
                        other = Convert.ToDecimal(Other.Text),
                        status=1,
                        company_id=1,
                        created_by=1,
                        created_on=DateTime.Now
                    });
                    db.SaveChanges();

                    List<SalesInvoiceCollectionViewModel> salesInvoiceCollectionViewModels = (List<SalesInvoiceCollectionViewModel>)PurchaseInvoiceDataGrid.ItemsSource;
                    for (int i = 0; i < salesInvoiceCollectionViewModels.Count-1; i++)
                    {
                        purchase_details purchase_details = new purchase_details
                        {
                            purchase_id = purchase_master.purchase_id,
                            product_id = Convert.ToInt32(salesInvoiceCollectionViewModels[i].name),
                            sr_no = Convert.ToInt32(salesInvoiceCollectionViewModels[i].sr),
                            alternate_qty = salesInvoiceCollectionViewModels[i].qty.ToString(),
                            gross_weight = Convert.ToDecimal(salesInvoiceCollectionViewModels[i].grossnet),
                            less = Convert.ToDecimal(salesInvoiceCollectionViewModels[i].less),
                            net_weight = salesInvoiceCollectionViewModels[i].netweight.ToString(),
                            rate = Convert.ToDecimal(salesInvoiceCollectionViewModels[i].rate),
                            per = salesInvoiceCollectionViewModels[i].per,
                            amount = Convert.ToDecimal(salesInvoiceCollectionViewModels[i].amount),
                            created_by = 1,
                            company_id = 1,
                            created_on = DateTime.Now,
                            status = 1
                        };
                        db.purchase_details.Add(purchase_details);
                        db.SaveChanges();
                    }
                    MessageBox.Show("Purchase save successfully.");
                    //Clear_Form();
                }
            }
            else
            {
                MessageBox.Show("Please fill compulsory data.");
            }
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var cmbx = sender as ComboBox;
            var data = PurchaseViewModel.GetPartyCollection();
            cmbx.ItemsSource = from item in data
                               where item.party_name.ToLower().Contains(cmbx.Text.ToLower())
                               select item;
            cmbx.IsDropDownOpen = true;
        }

        private ProductViewModel GetProductById(int id)
        {
            ProductViewModel data = new ProductViewModel();
            try
            {
                using (invetoryEntities db = new invetoryEntities())
                {
                    var query = (from pro in db.product_master
                                 join unit in db.unit_master
                                 on pro.unit_id equals unit.id
                                 where pro.id == id
                                 select new
                                 {
                                     mrp = pro.mrp,
                                     unit_name = unit.code
                                 }).FirstOrDefault();
                    data.MRP = query.mrp.ToString();
                    data.Unit = query.unit_name;
                }
            }
            catch (Exception)
            {
            }
            return data;
        }

        private void CalculateNetAmount()
        {
            double sum = Convert.ToDouble(GrossAmount.Text);
            sum += Convert.ToDouble(Paking.Text);
            sum -= Convert.ToDouble(Dis.Text);
            sum += Convert.ToDouble(OtherCharges.Text);
            sum -= Convert.ToDouble(RoundOff.Text);
            NetAmount.Text = sum.ToString();
        }

        private void CalculateOtherCharges()
        {
            double sum = Convert.ToDouble(Adhat.Text);
            sum += Convert.ToDouble(Taulai.Text);
            sum += Convert.ToDouble(Majdoori.Text);
            sum += Convert.ToDouble(Packing.Text);
            sum += Convert.ToDouble(MandiSulk.Text);
            sum += Convert.ToDouble(VikashSesh.Text);
            sum += Convert.ToDouble(Other.Text);
            OtherCharges.Text = sum.ToString();
            this.CalculateNetAmount();
        }

        private void Adhat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.CalculateOtherCharges();
                Taulai.Focus();
            }
        }

        private void Taulai_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.CalculateOtherCharges();
                Majdoori.Focus();
            }
        }

        private void Majdoori_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.CalculateOtherCharges();
                Packing.Focus();
            }
        }

        private void Packing_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.CalculateOtherCharges();
                MandiSulk.Focus();
            }
        }

        private void MandiSulk_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.CalculateOtherCharges();
                VikashSesh.Focus();
            }
        }

        private void VikashSesh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.CalculateOtherCharges();
                Other.Focus();
            }
        }

        private void Other_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.CalculateOtherCharges();
                GrossAmount.Focus();
            }
        }
    }
}
