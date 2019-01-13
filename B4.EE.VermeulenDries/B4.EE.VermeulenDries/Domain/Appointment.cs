using System;
using System.Collections.Generic;
using System.Text;

namespace B4.EE.VermeulenDries.Domain
{
    public class Appointment
    {
        public string OptionalMsg { get; set; }
        public DateTime DateTime { get; set; }
        public string Name { get; set; }
        public int ID { get; set; }

        public Appointment()
        {

        }
        public Appointment(string msg, DateTime dt, string name, int ID)
        {
            this.OptionalMsg = msg;
            this.DateTime = dt;
            this.Name = name;
            this.ID = ID;
        }
    }
}
