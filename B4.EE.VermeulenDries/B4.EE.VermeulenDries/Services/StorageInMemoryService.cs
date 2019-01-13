using System;
using System.Collections.Generic;
using System.Text;
using B4.EE.VermeulenDries.Domain;

namespace B4.EE.VermeulenDries.Services
{
    public class StorageInMemoryService: IStorageService
    {
        private List<User> users;
        public List<User> Users { get { return users; } }
        public StorageInMemoryService()
        {
            users = new List<User>();
            User admin = new User();
            admin.Email = "dries.vermeulen@infohos.be";
            admin.FirstName = "Dries";
            admin.LastName = "Vermeulen";
            admin.Password = "admin";
            admin.IsAdmin = true;
            admin.Appointments = new List<Appointment>();
            admin.ID = 1;
            Users.Add(admin);

            User regular = new User();
            regular.Email = "regular";
            regular.FirstName = "Dries";
            regular.LastName = "Vermeulen";
            regular.Password = "regular";
            regular.IsAdmin = false;
            regular.Appointments = new List<Appointment>();
            regular.Appointments.Add(new Appointment("I'm looking to make loads of money", DateTime.Now, "Dries Vermeulen", 1));
            regular.ID = 2;
            Users.Add(regular);

        }

        public User GetById(int id)
        {
            foreach(User u in Users)
            {
                if(u.ID == id)
                {
                    return u;
                }
            }
            return null;
        }

        public int GetNewId()
        {
            return Users.Count + 1;
        }

        public List<User> GetAll()
        {
            return Users;
        }

        public void AddUser(User u)
        {
            Users.Add(u);
        }

        public int GetNewAppointmentID()
        {
            int count = 0;
            foreach(User u in Users)
            {
                count += u.Appointments.Count;
            }
            return count+1;
        }

        public List<Appointment> GetAllAppointments()
        {
            List<Appointment> apps = new List<Appointment>();
            foreach(User u in Users)
            {
                apps.AddRange(u.Appointments);
            }
            return apps;
        }

        public List<Appointment> GetAppointments(int id)
        {
            List<Appointment> apps = new List<Appointment>();
            foreach(User u in Users)
            {
                if (u.ID == id)
                {
                    apps = u.Appointments;
                }
            }
            return apps;
        }

        public void AddAppointment(Appointment app, User u)
        {
            u.Appointments.Add(app);
        }
    }
}
