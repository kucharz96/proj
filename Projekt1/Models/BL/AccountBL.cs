using Projekt1.DAL;
using Projekt1.Models.VM.Account;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Projekt1.Models.BL
{
    public class AccountBL
    {
      
        private ProjectContext db = new ProjectContext();

        public bool Login(LoginVM model)
        {
            // Lets first check if the Model is valid or not


            string username = model.Email;
            string password = model.Password;



            return db.Users.Any(user => user.Email == username && user.Password == password);

            // User found in the database





        }
        public bool CheckEmail(string email)
        {
            // Lets first check if the Model is valid or not

            



            return db.Users.Any(user => user.Email == email);

            // User found in the database





        }
        public Guid AddPatientWithActivationCode(PatientRegistrationVM model)
        {
            Patient newPatient = new Patient();
            newPatient.FirstName = model.FirstName;
            newPatient.LastName = model.LastName;
            newPatient.Email = model.Email;
            newPatient.Password = model.Password;
            newPatient.IsEnable = false;
            newPatient.BirthDay = DateTime.Now;

            Guid activationCode = Guid.NewGuid();
            newPatient.UserKey = activationCode;
            db.Patients.Add(newPatient);
            
            db.SaveChanges();

            return activationCode;



    
        }




        public string GetRoles(string username)
        {



           User user = db.Users.FirstOrDefault(s => s.Email == username);
            if (db.Doctors.Find(user.UserId) != null)
                return "Doctor";
            if (db.Patients.Find(user.UserId) != null)
                return "Patient";
            else
                return "";

        }
        public bool IsActive(string username)
        {
            return db.Users.SingleOrDefault(s => s.Email == username).IsEnable;
        }
        public bool ActivationAccount(string username, Guid code)
        {
            User user = db.Users.FirstOrDefault(s => s.Email == username);
            if (code == user.UserKey)
            {
                user.IsEnable = true;
                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}