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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CRMInventory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                Main.Content = new Company();
            }
        }
        private void company_List_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Company();
        }

        private void company_Create_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = "";
            AddCompany addCompanyWindown = new AddCompany();
            addCompanyWindown.ShowDialog();
        }

        private void unit_List_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Unit();
        }

       
        private void stockgroup_List_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new StockGroup();
        }

        private void godown_List_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Godown();
        }

        private void godown_Create_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = "";
            AddGodown addGodownWindown = new AddGodown();
            addGodownWindown.ShowDialog();
        }

        private void stockgroup_Create_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = "";
            AddStockGroup addStockGroupWindown = new AddStockGroup();
            addStockGroupWindown.ShowDialog();
        }

        private void unit_Create_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = "";
            AddUnit addUnitWindown = new AddUnit();
            addUnitWindown.ShowDialog();
        }

        private void StockItem_Create_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = "";
            AddProduct addProductWindown = new AddProduct();
            addProductWindown.ShowDialog();
        }

        private void StockItem_List_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Product();
        }

        private void Stock_Create_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = "";
            AddStock addStockWindown = new AddStock();
            addStockWindown.ShowDialog();
        }

        private void Stock_List_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SaleInvoice_Create_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = "";
            AddSalesInvoice addSalesInvoiceWindown = new AddSalesInvoice();
            addSalesInvoiceWindown.ShowDialog();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Receipt_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = "";
            test addSalesInvoiceWindown = new test();
            addSalesInvoiceWindown.ShowDialog();
        }

        private void Under_Create_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = "";
            AddUnder addUnderWindown = new AddUnder();
            addUnderWindown.ShowDialog();
        }

        private void Under_List_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = new Under();
        }

        private void Ledger_Create_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = "";
            AddLedger addLedgerWindown = new AddLedger();
            addLedgerWindown.ShowDialog();
        }

        private void Purchase_Create_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = "";
            AddPurchase addPurchaseWindown = new AddPurchase();
            addPurchaseWindown.ShowDialog();
        }
    }
}
