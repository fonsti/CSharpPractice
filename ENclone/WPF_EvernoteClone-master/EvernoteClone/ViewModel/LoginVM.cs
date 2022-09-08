using EvernoteClone.Model;
using EvernoteClone.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;

namespace EvernoteClone.ViewModel
{
    public class LoginVM : INotifyPropertyChanged
    {
        private bool isShowingRegister = false;

        private User user;

        public User MyUser
        {
            get { return user; }
            set { 
                user = value;
                OnPropertyChanged("MyUser");
            }
        }

        private Visibility loginVis;

        public event PropertyChangedEventHandler PropertyChanged;

        public Visibility LoginVis
        {
            get { return loginVis; }
            set { loginVis = value; OnPropertyChanged("LoginVis"); }
        }

        private Visibility registerVis;

        public Visibility RegisterVis
        {
            get { return registerVis; }
            set { registerVis = value; OnPropertyChanged("RegisterVis"); }
        }

        private string userName;

        public string UserName
        {
            get { return userName; }
            set { 
                userName = value;
                MyUser = new User
                {
                    Username = userName,
                    Password = this.Password
                };
                OnPropertyChanged("UserName");
            }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value;
                MyUser = new User
                {
                    Username = this.UserName,
                    Password = this.Password
                };
                OnPropertyChanged("Password");
            }
        }


        public RegisterCommand MyRegistercommand { get; set; }

        public LoginCommand MyLoginCommand { get; set; }

        public ShowRegisterCommand MyShowRegisterCommand { get; set; }

        public LoginVM()
        {
            LoginVis = Visibility.Visible;
            RegisterVis = Visibility.Collapsed;

            MyRegistercommand = new RegisterCommand(this);
            MyLoginCommand = new LoginCommand(this);
            MyShowRegisterCommand = new ShowRegisterCommand(this);

            MyUser = new User();
        }

        public void SwitchViews()
        {
            isShowingRegister = !isShowingRegister;

            if (isShowingRegister)
            {
                RegisterVis = Visibility.Visible;
                LoginVis = Visibility.Collapsed;
            }
            else
            {
                RegisterVis = Visibility.Collapsed;
                LoginVis = Visibility.Visible;
            }
        }

        public void Login()
        {
            // TODO: Login
        }

        public void Register()
        {
            // TODO: register
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
