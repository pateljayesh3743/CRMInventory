using CRMInventory.Infrastructure;
using Microsoft.VisualStudio.PlatformUI;
using System;

namespace CRMInventory.ViewModel
{
    public class LoginViewModel : ObservableObject 
    {
        private string _username;
        public string UserName
        {
            get { return _username; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("User Name is required");
                }
                else
                {
                    _username = value;
                }
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Password is required");
                }
                else
                {
                    _password = value;
                }
            }
        }

    }
}
