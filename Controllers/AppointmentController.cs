using AppointmentScheduler.Services;
using AppointmentScheduler.Utility;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentScheduler.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService) 
        {
            _appointmentService = appointmentService;
        }
        public IActionResult Index()
        {
            ViewBag.Doctorlist = _appointmentService.GetDoctorList();
            ViewBag.Duration = Helper.GetTimeDropDown();
            ViewBag.PatientList = _appointmentService.GetPatientList();
            return View();
        }
    }
}
