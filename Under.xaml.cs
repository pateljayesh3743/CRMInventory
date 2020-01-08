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
    public partial class Under : Page
    {

        UnderViewModel underViewModel = new UnderViewModel();
        public Under()
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
        public List<under_master> GetUnder()
        {
            List<under_master> stockgroups = new List<under_master>();
            using (invetoryEntities db = new invetoryEntities())
            {
                if (db.under_master.ToList().Count > 0)
                {
                    foreach (var item in db.under_master.ToList())
                    {
                        stockgroups.Add(new under_master
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
            underViewModel.pagingCollectionView = new PagingCollectionView(
             this.GetUnder(),
              100000
          );
            this.DataContext = underViewModel;
        }

        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            AddUnder addUnderWindow = new AddUnder();
            if (datagrid.SelectedIndex >= 0)
            {
                under_master under = datagrid.SelectedItem as under_master;
                addUnderWindow.UnderName.Text = under.name;
                addUnderWindow.UnderId.Text = under.id.ToString();
                addUnderWindow.btnModify.Visibility = Visibility.Visible;
                addUnderWindow.btnSave.Visibility = Visibility.Hidden;
                addUnderWindow.ShowDialog();
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
                    under_master under = datagrid.SelectedItem as under_master;
                    using (invetoryEntities db = new invetoryEntities())
                    {
                        if (db.under_master.Where(x => x.id == under.id).ToList().Count > 0)
                        {
                            db.under_master.Remove(db.under_master.Find(under.id));
                            db.SaveChanges();
                            MessageBox.Show("Under deleted successfully.");
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

       
        private void UnderSearch_KeyUp(object sender, KeyEventArgs e)
        {
            List<under_master> under = new List<under_master>();

            if (UnderNameSearch.Text != null && UnderNameSearch.Text != "")
            {
                using (invetoryEntities db = new invetoryEntities())
                {
                    if (db.under_master.Where(x => x.name.ToLower().Contains(UnderNameSearch.Text)).ToList().Count > 0)
                    {
                        foreach (var item in db.under_master.Where(x => x.name.ToLower().Contains(UnderNameSearch.Text)).ToList())
                        {
                            under.Add(new under_master
                            {
                                name = item.name.ToString(),
                                id = item.id
                            });
                        }

                        underViewModel.pagingCollectionView = new PagingCollectionView(under, 100000);
                        this.DataContext = underViewModel;
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
