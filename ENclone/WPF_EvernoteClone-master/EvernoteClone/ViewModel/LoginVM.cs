using EvernoteClone.Model;
using EvernoteClone.ViewModel.Commands;
using EvernoteClone.ViewModel.Helpers;
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
                    Password = this.Password,
                    ConfirmPassword = this.ConfirmPassword,
                    Name = this.Name,
                    Lastname = this.LastName
                };
                OnPropertyChanged("UserName");
            }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                MyUser = new User
                {
                    Username = this.UserName,
                    Password = this.Password,
                    ConfirmPassword = this.ConfirmPassword,
                    Name = name,
                    Lastname = this.LastName
                };
                OnPropertyChanged("Name");
            }
        }

        private string lastName;

        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                MyUser = new User
                {
                    Username = this.UserName,
                    Password = this.Password,
                    ConfirmPassword = this.ConfirmPassword,
                    Name = this.Name,
                    Lastname = lastName
                };
                OnPropertyChanged("LastName");
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
                    Password = password,
                    ConfirmPassword = this.ConfirmPassword,
                    Name = this.Name,
                    Lastname = this.LastName
                };
                OnPropertyChanged("Password");
            }
        }

        private string confirmPassword;

        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                confirmPassword = value;
                MyUser = new User
                {
                    Username = this.UserName,
                    Password = this.Password,
                    ConfirmPassword = confirmPassword,
                    Name = this.Name,
                    Lastname = this.LastName
                };
                OnPropertyChanged("ConfirmPassword");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler Authenticated;

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

        public async void Login()
        {
            bool result = await FirebaseAuthHelper.Login(MyUser);

            if (result)
            {
                Authenticated?.Invoke(this, new EventArgs());
            }
        }

        public async void Register()
        {
            bool result = await FirebaseAuthHelper.Register(MyUser);

            if (result)
            {

            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
