using AppointmentScheduler.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppointmentSchedurer.Models;
using AppointmentScheduler.Utility;
using AppointmentScheduler.ViewModels;
using AppointmentScheduler.Models;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace AppointmentScheduler.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDbContext _db;
        private readonly IEmailSender _emailSender;

        public AppointmentService(ApplicationDbContext db, IEmailSender emailSender)
        {
            _db = db;
            _emailSender= emailSender;
        }

        public async Task<int> AddUpdate(AppointmentVM model)
        {
            var startdate = DateTime.Parse(model.StartDate);
            var enddate = DateTime.Parse(model.StartDate).AddMinutes(Convert.ToDouble(model.Duration));
            var patient =_db.Users.FirstOrDefault(u=>u.Id==model.PatientId);
            var doctor = _db.Users.FirstOrDefault(u => u.Id == model.DoctorId);
            if (model!=null && model.Id > 0) 
            {
                var appointment = _db.Appointments.FirstOrDefault(x => x.Id == model.Id);
                appointment.Title = model.Title;
                appointment.Description = model.Description;
                appointment.StartDate = startdate;
                appointment.EndDate = enddate;
                appointment.Duration = model.Duration;
                appointment.DoctorId = model.DoctorId;
                appointment.PatientId = model.PatientId;
                appointment.IsDoctorApproved = false;
                appointment.AdminId = model.AdminId;

                await _db.SaveChangesAsync();
                //update
                return 1;

            }
            else 
            {
                //create
                Appointment appointment = new Appointment()
                {
                    Title = model.Title,
                    Description = model.Description,
                    StartDate = startdate,
                    EndDate = enddate,
                    Duration = model.Duration,
                    DoctorId = model.DoctorId,
                    PatientId = model.PatientId,
                    IsDoctorApproved=false,
                    AdminId = model.AdminId
                };

                await _emailSender.SendEmailAsync(doctor.Email, "Appointment Created",
                    $"Your Appointment with {patient.Name} is created and in pending status");
                
                await _emailSender.SendEmailAsync(patient.Email, "Appointment Created",
                    $"Your Appointment with {doctor.Name} is created and in pending status");
                _db.Appointments.Add(appointment);
                await _db.SaveChangesAsync();
                return 2;
            }
        }

        public async Task<int> ConfirmEvent(int id)
        {
            var appointment = _db.Appointments.FirstOrDefault(x => x.Id == id);
            if (appointment != null) {
                appointment.IsDoctorApproved = true;
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task <int> Delete(int id)
        {
            var appointment = _db.Appointments.FirstOrDefault(x => x.Id == id);
            _db.Appointments.Remove(appointment);
            if (appointment != null)
            {
                appointment.IsDoctorApproved = true;
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public List<AppointmentVM> DoctorsEventsById(string doctorId)
        {
            return _db.Appointments.Where(x => x.DoctorId == doctorId).Select(c => new AppointmentVM()
            {
                Id = c.Id,
                Description = c.Description,
                StartDate = c.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = c.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Title = c.Title,
                Duration = c.Duration,
                IsDoctorApproved = c.IsDoctorApproved
            }).ToList();
        }

        public AppointmentVM GetById(int id)
        {
            return _db.Appointments.Where(x => x.Id == id).Select(c => new AppointmentVM()
            {
                Id = c.Id,
                Description = c.Description,
                StartDate = c.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = c.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Title = c.Title,
                Duration = c.Duration,
                IsDoctorApproved = c.IsDoctorApproved,
                PatientId=c.PatientId,
                DoctorId=c.DoctorId,
                PatientName=_db.Users.Where(x=>x.Id==c.PatientId).Select(x=>x.Name).FirstOrDefault(),
                DoctorName = _db.Users.Where(x => x.Id == c.DoctorId).Select(x => x.Name).FirstOrDefault(),

            }).SingleOrDefault();
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

        public List<AppointmentVM> PatientsEventsById(string patientId)
        {
            return _db.Appointments.Where(x => x.PatientId == patientId).Select(c => new AppointmentVM()
            {
                Id = c.Id,
                Description = c.Description,
                StartDate = c.StartDate.ToString("yyyy-MM-dd HH:mm:ss"),
                EndDate = c.EndDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Title = c.Title,
                Duration = c.Duration,
                IsDoctorApproved = c.IsDoctorApproved
            }).ToList();
        }
    }
}
