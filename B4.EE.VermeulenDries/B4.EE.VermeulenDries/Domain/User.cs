using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace B4.EE.VermeulenDries.Domain
{
    public class User
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Image Image { get; set; }

        public List<Appointment> Appointments { get; set; }

        public Boolean IsAdmin { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        public string CurrentJobTitle { get; set; }
        public string Coords { get; set; }
        public int ID { get; set; }
    }
}
