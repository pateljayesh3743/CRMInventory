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
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        
        public AddProduct()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            this.DataContext = new ProductViewModel();
            ProductName.Focus();
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
        private void ProductName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Category.Focus();
            }
        }

        private void Category_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ItemGroup.Focus();
            }
        }

        private void OrderQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SaveProduct();
            }
        }

        private void SaveProduct()
        {
            if (ProductName.Text != "")
            {
                using (invetoryEntities db = new invetoryEntities())
                {
                    product_master product_Master=new product_master{
                        product = ProductName.Text,
                        category_id = 1,
                        //item_group_id = Convert.ToInt32(ItemGroup.SelectedValue),
                        unit_id = Convert.ToInt32(Unit.SelectedValue),
                        alternat_unit_id = Convert.ToInt32(AlternetUnit.SelectedValue),
                        mrp = Convert.ToDecimal(MRP.Text),
                        rate_on = 1,
                        tax_type = 1,
                        sale_rate_on = 1,
                        rate_persantage = 1,
                        discount_consumer = 1,
                        discount_other = 1,
                        discount_on_qty = 1,
                        brokerage = 1,
                        min_qty = 1,
                        order_qty = 1,
                        status=1,
                        created_on=DateTime.Now,
                    };
                    db.product_master.Add(product_Master);
                    db.SaveChanges();
                    product_expenses_master product_Expenses_Master = new product_expenses_master {
                        adhat=(Adhat.Text == ""?0:Convert.ToDecimal(Adhat.Text)),
                        adhat_unit_id=Convert.ToInt32(AdhatPer.SelectedValue),
                        taulai= (Taulai.Text == "" ? 0 : Convert.ToDecimal(Taulai.Text)),
                        taulai_unit_id = Convert.ToInt32(TaulaiPer.SelectedValue),
                        majoori= (Majoori.Text == "" ? 0 : Convert.ToDecimal(Majoori.Text)),
                        majoori_unit_id = Convert.ToInt32(MajooriPer.SelectedValue),
                        packing = (Packing.Text == "" ? 0 : Convert.ToDecimal(Packing.Text)),
                        packing_unit_id = Convert.ToInt32(PackingPer.SelectedValue),
                        mandi_sulk = (MundiSulk.Text == "" ? 0 : Convert.ToDecimal(MundiSulk.Text)),
                        mandi_sulk_unit_id = Convert.ToInt32(MundiSulkPer.SelectedValue),
                        vikash_sesh = (VikashSesh.Text == "" ? 0 : Convert.ToDecimal(VikashSesh.Text)),
                        vikash_sesh_unit_id = Convert.ToInt32(VikashSeshPer.SelectedValue),
                        packing_less = (PackingLess.Text == "" ? 0 : Convert.ToDecimal(VikashSesh.Text)),
                        packing_less_unit_id = Convert.ToInt32(PackingLessPer.SelectedValue),
                        product_id= product_Master.id,
                        created_on=DateTime.Now,
                        status=1
                    };
                    db.product_expenses_master.Add(product_Expenses_Master);
                    db.SaveChanges();
                    MessageBox.Show("Product save successfully.");
                    //Clear_Form();
                }
            }
            else
            {
                MessageBox.Show("Please fill compulsory data.");
            }
        }

        private void ItemGroup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TaxType.Focus();
            }
        }

        private void TaxType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Unit.Focus();
            }
        }

        private void Unit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AlternetUnit.Focus();
            }
        }

        private void AlternetUnit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                MRP.Focus();
            }
        }

        private void MRP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                RateOn.Focus();
            }
        }

        private void RateOn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Adhat.Focus();
            }
        }

        private void Adhat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AdhatPer.Focus();
            }
        }

        private void AdhatPer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Taulai.Focus();
            }
        }

        private void Taulai_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TaulaiPer.Focus();
            }
        }

        private void TaulaiPer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Majoori.Focus();
            }
        }

        private void Majoori_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                MajooriPer.Focus();
            }
        }

        private void MajooriPer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Packing.Focus();
            }
        }

        private void Packing_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                PackingPer.Focus();
            }
        }

        private void PackingPer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                MundiSulk.Focus();
            }
        }

        private void MundiSulk_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                MundiSulkPer.Focus();
            }
        }

        private void MundiSulkPer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                VikashSesh.Focus();
            }
        }

        private void VikashSesh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                VikashSeshPer.Focus();
            }
        }

        private void VikashSeshPer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                PackingLess.Focus();
            }
        }

        private void PackingLess_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                PackingLessPer.Focus();
            }
        }

        private void PackingLessPer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SaleRateOn.Focus();
            }
        }

        private void SaleRateOn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Rate.Focus();
            }
        }

        private void Rate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                DisConsumer.Focus();
            }
        }

        private void Brokerge_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BrokergePer.Focus();
            }
        }

        private void BrokergePer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                MinQty.Focus();
            }
        }

        private void MinQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                OrderQty.Focus();
            }
        }

        private void DisConsumer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                DisOther.Focus();
            }
        }

        private void DisOther_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                DisOnQty.Focus();
            }
        }

        private void DisOnQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Brokerge.Focus();
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var cmbx = sender as ComboBox;
            var data = ProductViewModel.GetItemGroupCollection();
            cmbx.ItemsSource = from item in data
                               where item.ItemGroupName.ToLower().Contains(cmbx.Text.ToLower())
                               select item;
            cmbx.IsDropDownOpen = true;
        }
    }
}
