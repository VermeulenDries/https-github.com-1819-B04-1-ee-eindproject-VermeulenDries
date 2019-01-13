using System;
using System.Collections.Generic;
using System.Text;
using FreshMvvm;
using B4.EE.VermeulenDries.Domain;
using B4.EE.VermeulenDries.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace B4.EE.VermeulenDries.ViewModels
{
    public class MainAppViewModel: FreshBasePageModel
    {
        private User currentUser;
        public IStorageService StorageService;
        public MainAppViewModel(IStorageService storageService)
        {
            this.StorageService = storageService;
        }



        public override void Init(object initData)
        {
            base.Init(initData);
            currentUser = initData as User;
            Name = currentUser.FirstName;
            if (currentUser.IsAdmin)
            {
                IsAdmin = true;
                Appointments = StorageService.GetAllAppointments(); 
            }
            else
            {
                IsNotAdmin = true;
                Appointments = StorageService.GetAppointments(currentUser.ID);
            }
        }

        public override void ReverseInit(object initData)
        {
            base.ReverseInit(initData);
            currentUser = initData as User;
            Name = currentUser.FirstName;
            if (currentUser.IsAdmin)
            {
                IsAdmin = true;
                Appointments = StorageService.GetAllAppointments();
            }
            else
            {
                IsNotAdmin = true;
                Appointments = currentUser.Appointments;
            }
        }
        #region properties
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged(nameof(name));
            }
        }

        private bool isNotAdmin;
        public bool IsNotAdmin
        {
            get { return isNotAdmin; }
            set
            {
                isNotAdmin = value;
                RaisePropertyChanged(nameof(isNotAdmin));
            }
        }
        private bool isAdmin;
        public bool IsAdmin
        {
            get { return isAdmin; }
            set
            {
                isAdmin = value;
                RaisePropertyChanged(nameof(isAdmin));
            }
        }

        private List<Appointment> appointments;
        public List<Appointment> Appointments
        {
            get { return appointments; }
            set
            {
                appointments = value;
                RaisePropertyChanged(nameof(appointments));
            }
        }
        #endregion
        public ICommand MakeAppointmentCommand => new Command(
            async () => {
                await CoreMethods.PushPageModel<NewAppointmentViewModel>(currentUser, false, true);
            }
        );
    }
}
