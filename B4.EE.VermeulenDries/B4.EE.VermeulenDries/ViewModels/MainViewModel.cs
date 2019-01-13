using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using FreshMvvm;
using Xamarin.Forms;
using B4.EE.VermeulenDries.Domain;
using B4.EE.VermeulenDries.Services;

namespace B4.EE.VermeulenDries.ViewModels
{
    public class MainViewModel: FreshBasePageModel
    {
        public List<User> Users;
        public IStorageService StorageService;

        public MainViewModel(IStorageService storageService)
        {
            this.StorageService = storageService;
            
        }

        public ICommand LoginCommand => new Command(
            async () => {
                await CoreMethods.PushPageModel<LoginViewModel>(Users,false,true);
                
            }
        );

        public ICommand RegisterCommand => new Command(
            async () => {
                await CoreMethods.PushPageModel<RegisterViewModel>(Users,false,true);
            }
        );

        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);
            User user = returnedData as User;
        }

        
    }
}
