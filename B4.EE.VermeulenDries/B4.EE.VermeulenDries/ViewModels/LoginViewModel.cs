using System;
using System.Collections.Generic;
using System.Text;
using FreshMvvm;
using B4.EE.VermeulenDries.Domain;
using System.Windows.Input;
using Xamarin.Forms;
using B4.EE.VermeulenDries.Services;

namespace B4.EE.VermeulenDries.ViewModels
{
    public class LoginViewModel: FreshBasePageModel
    {
        private IStorageService StorageService;
        public LoginViewModel(IStorageService storageService)
        {
            this.StorageService = storageService;
        }

        public async override void Init(object initData)
        {
            base.Init(initData);
            Users = StorageService.GetAll();
        }

        #region properties
        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                RaisePropertyChanged(nameof(email));
            }
        }
        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                RaisePropertyChanged(nameof(password));
            }
        }
        public List<User> Users { get; set; }
        private string errorMsg;
        public string ErrorMsg
        {
            get { return errorMsg; }
            set
            {
                errorMsg = value;
                RaisePropertyChanged(nameof(errorMsg));
            }
        }
        public User loginUser { get; set; }
        #endregion
        public ICommand LoginCommand => new Command(
            async () => {
                foreach(User u in Users)
                {
                    if(u.Email.Equals(Email) && u.Password.Equals(Password))
                    {
                        loginUser = u;
                        break;
                    }
                }
                if (loginUser != null)
                {
                    await CoreMethods.PushPageModel<MainAppViewModel>(loginUser, false, true);
                }
                ErrorMsg = "Couldn't log you in with this information.";
            }
        );

        
    }
}
