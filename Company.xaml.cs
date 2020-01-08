using CRMInventory.Infrastructure;
using CRMInventory.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CRMInventory
{
    /// <summary>
    /// Interaction logic for Company.xaml
    /// </summary>
    public partial class Company : Page
    {
        private  PagingCollectionView _cview;
        public Company()
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
                Login childwin = new Login();
                childwin.ShowDialog();
                datagrid.Focus();
            }
            else if (e.Key == Key.Delete)
            {
                Delete();
                datagrid.Focus();
            }
        }

        public void LoadGrid()
        {
            _cview = new PagingCollectionView(
                  this.GetCompanies(),
                    100000
                );
            this.DataContext = _cview;
        }

        private void Delete()
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation", MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.Yes)  // error is here
            {
                if (datagrid.SelectedIndex >= 0)
                {
                    company_master company = datagrid.SelectedItem as company_master;
                    using (invetoryEntities db = new invetoryEntities())
                    {
                        if (db.company_master.Where(x => x.id == company.id).ToList().Count > 0)
                        {
                            db.company_master.Remove(db.company_master.Find(company.id));
                            db.SaveChanges();
                            MessageBox.Show("Company deleted successfully.");
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
        public List<company_master> GetCompanies()
        {
            List<company_master> companies = new List<company_master>();
            using (invetoryEntities db = new invetoryEntities())
            {
                if (db.company_master.ToList().Count > 0)
                {
                    foreach (var item in db.company_master.ToList())
                    {
                        companies.Add(new company_master
                        {
                            name = item.name.ToString(),
                            finanacial_year = item.finanacial_year
                        });
                    }
                }
            }
            return companies;
        }
      
        private void CompanyNameSearch_KeyUp(object sender, KeyEventArgs e)
        {
            List<company_master> company = new List<company_master>();

            if (CompanyNameSearch.Text != null && CompanyNameSearch.Text != "")
            {
                using (invetoryEntities db = new invetoryEntities())
                {
                    if (db.company_master.Where(x => x.name.ToLower().Contains(CompanyNameSearch.Text)).ToList().Count > 0)
                    {
                        foreach (var item in db.company_master.Where(x => x.name.ToLower().Contains(CompanyNameSearch.Text)).ToList())
                        {
                            company.Add(new company_master
                            {
                                name = item.name.ToString(),
                                finanacial_year = item.finanacial_year,
                                id = item.id
                            });
                        }

                        this.DataContext = new PagingCollectionView(company, 10000);
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
