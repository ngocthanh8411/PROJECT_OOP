using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HOTEL_MANAGER_SYSTEM
{
    public abstract class Person
    {
        private int ID;
        private string Name;
        private DateTime BirthDay;
        private string Gender;
        private string PhoneNumber;

        

        public Person()
        {

        }
        public Person(int id, string name, DateTime birthday, string gender)
        {
            id = ID;
            Name = name;
            BirthDay = birthday;
            Gender = gender;

        }
        
        public Person(int id, string name, DateTime birthday, string gender, string PhoneNumber)
        {
            id = ID;
            Name = name;
            BirthDay = birthday;
            Gender = gender;
            phonenumber = PhoneNumber;
        }
        

        public int id { get => ID; set => ID = value; }
        public string name { get => Name; set => Name = value; }
        public DateTime birthday { get => BirthDay; set => BirthDay = value; }
        public string gender { get => Gender; set => Gender = value; }
        public string phonenumber { get => PhoneNumber; set => PhoneNumber = value; }


        // Phương thức PrintDetails
        public virtual string PrintDetails()
        {
            return "ID" + ID + "\nName" + Name + "\nBirthday" + BirthDay + "\nGender" + Gender + "\nPhoneNumber" + PhoneNumber;
        }
    }
}