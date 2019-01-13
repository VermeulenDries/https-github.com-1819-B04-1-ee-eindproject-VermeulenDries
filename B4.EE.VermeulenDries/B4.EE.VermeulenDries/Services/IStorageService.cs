using B4.EE.VermeulenDries.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace B4.EE.VermeulenDries.Services
{
    public interface IStorageService
    {
        User GetById(int Id);

        int GetNewId();

        List<User> GetAll();

        void AddUser(User u);

        int GetNewAppointmentID();

        List<Appointment> GetAllAppointments();

        List<Appointment> GetAppointments(int id);

        void AddAppointment(Appointment app, User u);
    }
}
