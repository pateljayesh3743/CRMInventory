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
    public partial class Godown : Page
    {
        GodownViewModel godownViewModel = new GodownViewModel();
        public Godown()
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
        public List<godown_master> GetGodown()
        {
            List<godown_master> godown = new List<godown_master>();
            using (invetoryEntities db = new invetoryEntities())
            {
                if (db.godown_master.ToList().Count > 0)
                {
                    foreach (var item in db.godown_master.ToList())
                    {
                        godown.Add(new godown_master
                        {
                            name = item.name.ToString(),
                            Address = item.Address,
                            id=item.id
                        });
                    }
                }
            }
            return godown;
        }
        //private void Button_Click_Save(object sender, RoutedEventArgs e)
        //{
        //    if (GodownName.Text != "" && GodownName.Text != "")
        //    {
        //        using (invetoryEntities db = new invetoryEntities())
        //        {
        //            db.godown_master.Add(new godown_master
        //            {
        //                name = GodownName.Text,
        //                Address = Address.Text
        //            });
        //            db.SaveChanges();
        //            MessageBox.Show("Godown save successfully.");
        //            Clear_Form();
        //            LoadGrid();
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Please fill compulsory data.");
        //    }
        //}

        //private void Button_Click_Clear(object sender, RoutedEventArgs e)
        //{
        //    Clear_Form();
        //}
        //private void Clear_Form()
        //{
        //    GodownName.Text = "";
        //    Address.Text = "";
        //    btnModify.Visibility = Visibility.Hidden;
        //    btnSave.Visibility = Visibility.Visible;
        //}
        public void LoadGrid()
        {
            godownViewModel.pagingCollectionView = new PagingCollectionView(
                  this.GetGodown(),
                    100000
                );
            this.DataContext = godownViewModel;
        }

        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            AddGodown addGodownWindow = new AddGodown();
            if (datagrid.SelectedIndex >= 0)
            {
                godown_master godown = datagrid.SelectedItem as godown_master;
                addGodownWindow.GodownName.Text = godown.name;
                addGodownWindow.Address.Text = godown.Address;
                addGodownWindow.GodownId.Text = godown.id.ToString();
                addGodownWindow.btnModify.Visibility = Visibility.Visible;
                addGodownWindow.btnSave.Visibility = Visibility.Hidden;
                addGodownWindow.ShowDialog();
            }
            else {
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
                    godown_master godown = datagrid.SelectedItem as godown_master;
                    using (invetoryEntities db = new invetoryEntities())
                    {
                        if (db.godown_master.Where(x => x.id == godown.id).ToList().Count > 0)
                        {
                            db.godown_master.Remove(db.godown_master.Find(godown.id));
                            db.SaveChanges();
                            MessageBox.Show("Godown deleted successfully.");
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

        private void GodownSearch_KeyUp(object sender, KeyEventArgs e)
        {
            List<godown_master> godowns = new List<godown_master>();

            if (GodownNameSearch.Text != null && GodownNameSearch.Text != "")
            {
                using (invetoryEntities db = new invetoryEntities())
                {
                    if (db.godown_master.Where(x => x.name.ToLower().Contains(GodownNameSearch.Text)).ToList().Count > 0)
                    {
                        foreach (var item in db.godown_master.Where(x => x.name.ToLower().Contains(GodownNameSearch.Text)).ToList())
                        {
                            godowns.Add(new godown_master
                            {
                                name = item.name.ToString(),
                                Address = item.Address.ToString(),
                                id = item.id
                            });
                        }

                        godownViewModel.pagingCollectionView = new PagingCollectionView(godowns, 100000);
                        this.DataContext = godownViewModel;
                    }
                    else
                    {
                        LoadGrid();
                    }
                }
            }
            else if (AddressSearch.Text != null && AddressSearch.Text != "")
            {
                using (invetoryEntities db = new invetoryEntities())
                {
                    if (db.godown_master.Where(x => x.Address.ToLower().Contains(AddressSearch.Text)).ToList().Count > 0)
                    {
                        foreach (var item in db.godown_master.Where(x => x.Address.ToLower().Contains(AddressSearch.Text)).ToList())
                        {
                            godowns.Add(new godown_master
                            {
                                name = item.name.ToString(),
                                Address = item.Address.ToString(),
                                id = item.id
                            });
                        }

                        godownViewModel.pagingCollectionView = new PagingCollectionView(godowns, 100000);
                        this.DataContext = godownViewModel;
                    }
                    else
                    {
                        LoadGrid();
                    }
                }
            }
            else if (GodownNameSearch.Text != null && GodownNameSearch.Text != "" && AddressSearch.Text != null && AddressSearch.Text != "")
            {
                using (invetoryEntities db = new invetoryEntities())
                {
                    if (db.godown_master.Where(x => x.Address.ToLower().Contains(AddressSearch.Text) || x.name.ToLower().Contains(GodownNameSearch.Text)).ToList().Count > 0)
                    {
                        foreach (var item in db.godown_master.Where(x => x.Address.ToLower().Contains(AddressSearch.Text) || x.name.ToLower().Contains(GodownNameSearch.Text)).ToList())
                        {
                            godowns.Add(new godown_master
                            {
                                name = item.name.ToString(),
                                Address = item.Address.ToString(),
                                id = item.id
                            });
                        }
                        godownViewModel.pagingCollectionView = new PagingCollectionView(godowns, 100000);
                        this.DataContext = godownViewModel;
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
