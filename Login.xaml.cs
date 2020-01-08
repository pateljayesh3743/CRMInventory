using CRMInventory.Model;
using CRMInventory.ViewModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace CRMInventory
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            this.DataContext = new LoginViewModel();
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            UserName.Focus();
        }
        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure close this windown?", "Window Close Confirmation", MessageBoxButton.YesNo);

                if (messageBoxResult == MessageBoxResult.Yes)  // error is here
                {
                    Close();
                }
            }
        }
        private void Button_Click_Login(object sender, RoutedEventArgs e)
        {
            if (UserName.Text != "" && Password.Text != "")
            {
                using (invetoryEntities db = new invetoryEntities())
                {
                    if (db.user_master.Where(x => x.username == UserName.Text && x.password == Password.Text).ToList().Count > 0)
                    {
                        foreach (Window window in Application.Current.Windows)
                        {
                            if (window.GetType() == typeof(MainWindow))
                            {
                                (window as MainWindow).InvetoryInfo.IsEnabled = true;
                                (window as MainWindow).AccountInfo.IsEnabled = true;
                                (window as MainWindow).TransactionInfo.IsEnabled = true;
                                (window as MainWindow).Display.IsEnabled = true;
                                this.Close();
                                (window as MainWindow).Main.Content = null;
                            }
                        }

                    }
                    else
                    {
                        MessageBox.Show("Details are wrong please try again");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please fill compulsory fields");
            }
        }

        private void UserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Password.Focus();
            }
        }

        private void Password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Button_Click_Login(sender,e);
            }
        }
    }
}
