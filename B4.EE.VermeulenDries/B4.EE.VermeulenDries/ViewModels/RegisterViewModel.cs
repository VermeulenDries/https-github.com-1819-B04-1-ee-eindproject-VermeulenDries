using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using B4.EE.VermeulenDries.Domain;
using B4.EE.VermeulenDries.Services;
using FreshMvvm;
using Xamarin.Forms;
using Plugin.Permissions;
using Rg.Plugins.Popup;

namespace B4.EE.VermeulenDries.ViewModels
{
    public class RegisterViewModel: FreshBasePageModel
    {
        public User user;
        private ILocationService LocationService;
        private IStorageService StorageService;
        public RegisterViewModel(IStorageService storageService)
        {
            this.StorageService = storageService;
            
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            LocationService = DependencyService.Get<ILocationService>();
            LocationService.GetLocation();
            Coordinates = LocationService.GetCoords();
        }
        #region properties
        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                firstName = value;
                RaisePropertyChanged(nameof(firstName));
            }
        }
        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                RaisePropertyChanged(nameof(lastName));
            }
        }
        
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
        private string currentJobTitle;
        public string CurrentJobTitle
        {
            get { return currentJobTitle; }
            set
            {
                currentJobTitle = value;
                RaisePropertyChanged(nameof(currentJobTitle));
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
        private string coordinates;
        public string Coordinates
        {
            get { return coordinates; }
            set
            {
                coordinates = value;
                RaisePropertyChanged(nameof(coordinates));
            }
        }
        private Image userImage;
        public Image UserImage
        {
            get { return userImage; }
            set
            {
                userImage = value;
                RaisePropertyChanged(nameof(userImage));
            }
        }
        private bool errorVsbl;
        public bool ErrorVsbl
        {
            get { return errorVsbl; }
            set
            {
                errorVsbl = value;
                RaisePropertyChanged(nameof(errorVsbl));
            }
        }
        #endregion
        public ICommand RegisterCommand => new Command(
            async () => {
                try
                {
                    user = new User();
                    if (ValidateInput())
                    {
                        user.Email = Email;
                        user.FirstName = FirstName;
                        user.LastName = LastName;
                        user.Password = Password;
                        user.IsAdmin = false;
                        if (CurrentJobTitle != null)
                        {
                            user.CurrentJobTitle = CurrentJobTitle;
                        }
                        user.ID = StorageService.GetNewId();
                        if (userImage != null)
                        {
                            user.Image = UserImage;
                        }
                        if (Coordinates != null)
                        {
                            user.Coords = Coordinates;
                        }
                        user.ID = StorageService.GetNewId();
                        StorageService.AddUser(user);
                        await CoreMethods.PopPageModel(user, false, true);
                    }
                }
                catch(Exception e)
                {

                }
                
            }
        );

        public ICommand TakePictureCommand => new Command(
            async () =>
            {
                try
                {
                    var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Camera);
                    //var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Plugin.Permissions.Abstractions.Permission.Camera });
                    //await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Plugin.Permissions.Abstractions.Permission.Camera);
                    //if (results[Plugin.Permissions.Abstractions.Permission.Camera] == Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                    //{
                        var picture = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });

                        if (picture != null)
                        {
                            userImage.Source = ImageSource.FromStream(() => { return picture.GetStream(); });
                        }
                    //}
                    
                }
                catch(Exception e)
                {

                }
            });

        public ICommand UploadCoordsCommand => new Command(
           () =>  Coordinates = LocationService.GetCoords())
        
            ;

        private bool ValidateInput()
        {
            bool validate = true;
            if( Email == null | !Email.Contains("@"))
            {
                ErrorMsg += "Invalid email\n";
                validate = false;
            }
            if ( FirstName == null | FirstName.Equals(""))
            {
                ErrorMsg += "Please provide a first name.\n";
                validate = false;
            }
            if (LastName == null | LastName.Equals("") )
            {
                ErrorMsg += "Please provide a last name.\n";
                validate = false;
            }
            ErrorVsbl = true;
            return validate;
        }
    }
}
