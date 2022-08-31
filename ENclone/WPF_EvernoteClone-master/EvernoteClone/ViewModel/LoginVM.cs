using EvernoteClone.Model;
using EvernoteClone.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvernoteClone.ViewModel
{
    public class LoginVM
    {
        private User user;

        public User MyUser
        {
            get { return user; }
            set { user = value; }
        }

        public RegisterCommand MyRegistercommand { get; set; }

        public LoginCommand MyLoginCommand { get; set; }

        public LoginVM()
        {
            MyRegistercommand = new RegisterCommand(this);
            MyLoginCommand = new LoginCommand(this);
        }
    }
}
