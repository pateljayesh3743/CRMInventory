using CRMInventory.Infrastructure;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CRMInventory
{
    /// <summary>
    /// Interaction logic for StockGroup.xaml
    /// </summary>
    public partial class StockGroup : Page
    {

        StockGroupViewModel stockGroupViewModel = new StockGroupViewModel();
        public StockGroup()
        {
            InitializeComponent();
            LoadGrid();
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            datagrid.Focus();
        }
        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure close this windown?", "Window Close Confirmation", MessageBoxButton.YesNo);

                if (messageBoxResult == MessageBoxResult.Yes)  // error is here
                {
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(MainWindow))
                        {
                            (window as MainWindow).Main.Content = null;
                        }
                    }
                }
            }
            else if (e.Key == Key.Enter)
            {
                Button_Click_Update(sender, e);
                LoadGrid();
                datagrid.Focus();
            }
            else if (e.Key == Key.Delete)
            {
                Button_Click_Delete(sender, e);
                datagrid.Focus();
            }
        }
        public List<stock_group_master> GetStockGroup()
        {
            List<stock_group_master> stockgroups = new List<stock_group_master>();
            using (invetoryEntities db = new invetoryEntities())
            {
                if (db.stock_group_master.ToList().Count > 0)
                {
                    foreach (var item in db.stock_group_master.ToList())
                    {
                        stockgroups.Add(new stock_group_master
                        {
                            name = item.name.ToString(),
                            id = item.id
                        });
                    }
                }
            }
            return stockgroups;
        }
       

        private void LoadGrid()
        {
            stockGroupViewModel.pagingCollectionView = new PagingCollectionView(
             this.GetStockGroup(),
              100000
          );
            this.DataContext = stockGroupViewModel;
        }

        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            AddStockGroup addStockGroupWindow = new AddStockGroup();
            if (datagrid.SelectedIndex >= 0)
            {
                stock_group_master stckgroup = datagrid.SelectedItem as stock_group_master;
                addStockGroupWindow.StockGroupName.Text = stckgroup.name;
                addStockGroupWindow.StockGroupId.Text = stckgroup.id.ToString();
                addStockGroupWindow.btnModify.Visibility = Visibility.Visible;
                addStockGroupWindow.btnSave.Visibility = Visibility.Hidden;
                addStockGroupWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Something went wroung!");
            }
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)  // error is here
            {
                if (datagrid.SelectedIndex >= 0)
                {
                    stock_group_master stckgroup = datagrid.SelectedItem as stock_group_master;
                    using (invetoryEntities db = new invetoryEntities())
                    {
                        if (db.stock_group_master.Where(x => x.id == stckgroup.id).ToList().Count > 0)
                        {
                            db.stock_group_master.Remove(db.stock_group_master.Find(stckgroup.id));
                            db.SaveChanges();
                            MessageBox.Show("Stock Group deleted successfully.");
                            LoadGrid();
                        }
                        else
                        {
                            MessageBox.Show("Something went wrong please try again.");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Delete operation Terminated");
            }
        }

       
        private void StockGroupSearch_KeyUp(object sender, KeyEventArgs e)
        {
            List<stock_group_master> stockgroup = new List<stock_group_master>();

            if (StockGroupNameSearch.Text != null && StockGroupNameSearch.Text != "")
            {
                using (invetoryEntities db = new invetoryEntities())
                {
                    if (db.stock_group_master.Where(x => x.name.ToLower().Contains(StockGroupNameSearch.Text)).ToList().Count > 0)
                    {
                        foreach (var item in db.stock_group_master.Where(x => x.name.ToLower().Contains(StockGroupNameSearch.Text)).ToList())
                        {
                            stockgroup.Add(new stock_group_master
                            {
                                name = item.name.ToString(),
                                id = item.id
                            });
                        }

                        stockGroupViewModel.pagingCollectionView = new PagingCollectionView(stockgroup, 100000);
                        this.DataContext = stockGroupViewModel;
                    }
                    else
                    {
                        LoadGrid();
                    }
                }
            }
            else
            {
                LoadGrid();
            }
        }
    }
}
