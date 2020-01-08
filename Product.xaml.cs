using CRMInventory.Infrastructure;
using CRMInventory.Model;
using CRMInventory.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CRMInventory
{
    /// <summary>
    /// Interaction logic for Godown.xaml
    /// </summary>
    public partial class Product : Page
    {
        PagingCollectionView pagingCollectionView = null;
        public Product()
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
                //Button_Click_Update(sender, e);
                LoadGrid();
                datagrid.Focus();
            }
            else if (e.Key == Key.Delete)
            {
                Button_Click_Delete(sender, e);
                datagrid.Focus();
            }
        }
        public List<ProductGridViewModel> GetProduct()
        {
            List<ProductGridViewModel> product = new List<ProductGridViewModel>();
            using (invetoryEntities db = new invetoryEntities())
            {
                if (db.product_master.ToList().Count > 0)
                {
                    foreach (var item in db.product_master.ToList())
                    {
                        product.Add(new ProductGridViewModel
                        {
                            product = item.product.ToString(),
                            unit = db.unit_master.Where(x=>x.id==item.unit_id).FirstOrDefault().name,
                            id=item.id,
                            mrp=item.mrp
                        });
                    }
                }
            }
            return product;
        }
        public void LoadGrid()
        {
            pagingCollectionView = new PagingCollectionView(
                  this.GetProduct(),
                    100000
                );
            this.DataContext = pagingCollectionView;
        }

        //private void Button_Click_Update(object sender, RoutedEventArgs e)
        //{
        //    AddGodown addGodownWindow = new AddGodown();
        //    if (datagrid.SelectedIndex >= 0)
        //    {
        //        godown_master godown = datagrid.SelectedItem as godown_master;
        //        addGodownWindow.GodownName.Text = godown.name;
        //        addGodownWindow.Address.Text = godown.Address;
        //        addGodownWindow.GodownId.Text = godown.id.ToString();
        //        addGodownWindow.btnModify.Visibility = Visibility.Visible;
        //        addGodownWindow.btnSave.Visibility = Visibility.Hidden;
        //        addGodownWindow.ShowDialog();
        //    }
        //    else {
        //        MessageBox.Show("Something went wroung!");
        //    }
        //}

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation", MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.Yes)  
            {
                if (datagrid.SelectedIndex >= 0)
                {
                    product_master product = datagrid.SelectedItem as product_master;
                    using (invetoryEntities db = new invetoryEntities())
                    {
                        if (db.product_master.Where(x => x.id == product.id).ToList().Count > 0)
                        {
                            db.product_master.Remove(db.product_master.Find(product.id));
                            db.SaveChanges();
                            MessageBox.Show("Product deleted successfully.");
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

        //private void Button_Click_Modify(object sender, RoutedEventArgs e)
        //{
        //    if (GodownName.Text != "" && GodownName.Text != "")
        //    {
        //        using (invetoryEntities db = new invetoryEntities())
        //        {
        //            var GodownID = Convert.ToInt32(GodownId.Text);
        //            if (db.godown_master.Where(x => x.id == GodownID).ToList().Count > 0)
        //            {
        //                var godown = db.godown_master.Where(x => x.id == GodownID).FirstOrDefault();
        //                godown.name = GodownName.Text;
        //                godown.Address = Address.Text;
        //                db.SaveChanges();
        //                MessageBox.Show("Godown updated successfully.");
        //                Clear_Form();
        //                LoadGrid();
        //            }
        //            else
        //            {
        //                MessageBox.Show("Something went wrong please try again.");
        //            }
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Please fill compulsory data.");
        //    }
        //}

        private void ProductSearch_KeyUp(object sender, KeyEventArgs e)
        {
            List<ProductGridViewModel> product = new List<ProductGridViewModel>();
            using (invetoryEntities db = new invetoryEntities())
            {
                var searchquery = from p in db.product_master
                                  join u in db.unit_master on p.unit_id equals u.id
                                  where (p.product.Contains(ProductNameSearch.Text) || ProductNameSearch.Text == "")
                                  && (p.mrp.ToString().Contains(MRPSearch.Text) || MRPSearch.Text== "")
                                     &&( u.name.Contains(UnitSearch.Text) || UnitSearch.Text== "")
                                     select new { p.product, p.mrp, u.name,p.id};
                    if (searchquery.ToList().Count > 0)
                    {
                        foreach (var item in searchquery)
                        {
                            product.Add(new ProductGridViewModel
                            {
                                product = item.product.ToString(),
                                mrp = item.mrp,
                                id = item.id,
                                unit= item.name
                            });
                        }

                        pagingCollectionView = new PagingCollectionView(product, 100000);
                        this.DataContext = pagingCollectionView;
                    }
                    else
                    {
                        LoadGrid();
                    }
                }
        }
    }
}
