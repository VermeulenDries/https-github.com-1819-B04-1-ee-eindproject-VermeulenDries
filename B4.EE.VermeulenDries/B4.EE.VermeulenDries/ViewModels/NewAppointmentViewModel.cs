using System;
using System.Collections.Generic;
using System.Text;
using FreshMvvm;
using B4.EE.VermeulenDries.Services;
using System.Windows.Input;
using Xamarin.Forms;
using B4.EE.VermeulenDries.Domain;

namespace B4.EE.VermeulenDries.ViewModels
{
    public class NewAppointmentViewModel: FreshBasePageModel
    {
        private User currentUser;
        private IStorageService StorageService;

        public NewAppointmentViewModel(IStorageService storageService)
        {
            this.StorageService = storageService;
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            currentUser = initData as User;
            Date = DateTime.Now;
        }
        #region properties
        private DateTime dateTime;
        public DateTime Date
        {
            get { return dateTime; }
            set
            {
                dateTime = value;
                RaisePropertyChanged(nameof(dateTime));
            }
        }
        private DateTime time;
        public DateTime Time
        {
            get { return time; }
            set
            {
                time = value;
                RaisePropertyChanged(nameof(time));
            }
        }
        private string msg;
        public string Msg
        {
            get { return msg; }
            set
            {
                msg = value;
                RaisePropertyChanged(nameof(msg));
            }
        }
        #endregion
        public ICommand ConfirmAppCommand => new Command(
            async () => {
                try
                {
                    Appointment app = new Appointment();   
                    if (Validate())
                    {
                        if (msg != null)
                        {
                            app.OptionalMsg = msg;
                        }
                        app.DateTime = Date;
                        app.ID = StorageService.GetNewAppointmentID();
                        app.Name = currentUser.FirstName + " " + currentUser.LastName;
                        currentUser.Appointments.Add(app);
                        StorageService.AddAppointment(app, currentUser);
                        await CoreMethods.PopPageModel(currentUser, false, true);
                    }
                }
                catch (Exception e)
                {

                }

            }
        );

        private bool Validate()
        {
            bool val = true;
            if(Date == null)
            {
                val = false;
            }
            return val;
        }
    }
}
