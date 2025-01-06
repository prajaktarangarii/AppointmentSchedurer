using AppointmentScheduler.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentSchedurer.Models;
using AppointmentScheduler.Utility;

namespace AppointmentScheduler.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _db;

        public AppointmentService(ApplicationDbContext db)
        {
            _db = db;
        }

        public List<DoctorVM> GetDoctorList()
        {
            var Doctor = (from users in _db.Users
                          join userRoles in _db.UserRoles on users.Id equals userRoles.UserId
                          join roles in _db.Roles.Where(x =>x.Name == Helper.Doctor) on userRoles.RoleId equals roles.Id
                          select new DoctorVM
                          {
                              Id = users.Id,
                              Name = users.Name
                          }).ToList();
            return Doctor;
        }


        public List<PatientVM> GetPatientList()
        {
            var Patients = (from users in _db.Users
                          join userRoles in _db.UserRoles on users.Id equals userRoles.UserId
                          join roles in _db.Roles.Where(x => x.Name == Helper.Patient) on userRoles.RoleId equals roles.Id
                          select new PatientVM
                          {
                              Id = users.Id,
                              Name = users.Name
                          }).ToList();
            return Patients;
        }


        public List<PatientVM> GetPatientsList()
        {
            throw new NotImplementedException();
        }
    }
}
