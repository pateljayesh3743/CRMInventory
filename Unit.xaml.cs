using CRMInventory.Infrastructure;
using CRMInventory.Model;
using CRMInventory.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CRMInventory
{
    /// <summary>
    /// Interaction logic for Unit.xaml
    /// </summary>
    public partial class Unit : Page
    {

        UnitViewModel unitViewModel = new UnitViewModel();
        public Unit()
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
        public List<unit_master> Getunities()
        {
            List<unit_master> unities = new List<unit_master>();
            using (invetoryEntities db = new invetoryEntities())
            {
                if (db.unit_master.ToList().Count > 0)
                {
                    foreach (var item in db.unit_master.ToList())
                    {
                        unities.Add(new unit_master
                        {
                            name = item.name.ToString(),
                            code = item.code.ToString(),
                            id = item.id
                        });
                    }
                }
            }
            return unities;
        }
        private void LoadGrid()
        {
            unitViewModel.pagingCollectionView = new PagingCollectionView(
            this.Getunities(),
             100000
            );

            this.DataContext = unitViewModel;
        }

        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            AddUnit addUnitWindow = new AddUnit();
            if (datagrid.SelectedIndex >= 0)
            {
                unit_master unit = datagrid.SelectedItem as unit_master;
                addUnitWindow.UnitName.Text = unit.name;
                addUnitWindow.ShortCode.Text = unit.code;
                addUnitWindow.UnitId.Text = unit.id.ToString();
                addUnitWindow.btnModify.Visibility = Visibility.Visible;
                addUnitWindow.btnSave.Visibility = Visibility.Hidden;
                addUnitWindow.ShowDialog();
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
                    unit_master unit = datagrid.SelectedItem as unit_master;
                    using (invetoryEntities db = new invetoryEntities())
                    {
                        if (db.unit_master.Where(x => x.id == unit.id).ToList().Count > 0)
                        {
                            db.unit_master.Remove(db.unit_master.Find(unit.id));
                            db.SaveChanges();
                            MessageBox.Show("Unit deleted successfully.");
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

        

        private void UnitSearch_KeyUp(object sender, KeyEventArgs e)
        {
            List<unit_master> unities = new List<unit_master>();

            if (UnitNameSearch.Text != null && UnitNameSearch.Text != "")
            {
                using (invetoryEntities db = new invetoryEntities())
                {
                    if (db.unit_master.Where(x => x.name.ToLower().Contains(UnitNameSearch.Text)).ToList().Count > 0)
                    {
                        foreach (var item in db.unit_master.Where(x => x.name.ToLower().Contains(UnitNameSearch.Text)).ToList())
                        {
                            unities.Add(new unit_master
                            {
                                name = item.name.ToString(),
                                code = item.code.ToString(),
                                id = item.id
                            });
                        }

                        unitViewModel.pagingCollectionView = new PagingCollectionView(unities, 100000);
                        this.DataContext = unitViewModel;
                    }
                    else
                    {
                        LoadGrid();
                    }
                }
            }
            else if (UnitCodeSearch.Text != null && UnitCodeSearch.Text != "")
            {
                using (invetoryEntities db = new invetoryEntities())
                {
                    if (db.unit_master.Where(x => x.code.ToLower().Contains(UnitCodeSearch.Text)).ToList().Count > 0)
                    {
                        foreach (var item in db.unit_master.Where(x => x.code.ToLower().Contains(UnitCodeSearch.Text)).ToList())
                        {
                            unities.Add(new unit_master
                            {
                                name = item.name.ToString(),
                                code = item.code.ToString(),
                                id = item.id
                            });
                        }

                        unitViewModel.pagingCollectionView = new PagingCollectionView(unities, 100000);
                        this.DataContext = unitViewModel;

                    }
                    else
                    {
                        LoadGrid();
                    }
                }
            }
            else if (UnitNameSearch.Text != null && UnitNameSearch.Text != "" && UnitCodeSearch.Text != null && UnitCodeSearch.Text != "")
            {
                using (invetoryEntities db = new invetoryEntities())
                {
                    if (db.unit_master.Where(x => x.code.ToLower().Contains(UnitCodeSearch.Text) || x.name.ToLower().Contains(UnitNameSearch.Text)).ToList().Count > 0)
                    {
                        foreach (var item in db.unit_master.Where(x => x.code.ToLower().Contains(UnitCodeSearch.Text) || x.name.ToLower().Contains(UnitNameSearch.Text)).ToList())
                        {
                            unities.Add(new unit_master
                            {
                                name = item.name.ToString(),
                                code = item.code.ToString(),
                                id = item.id
                            });
                        }
                        unitViewModel.pagingCollectionView = new PagingCollectionView(unities, 100000);
                        this.DataContext = unitViewModel;
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
