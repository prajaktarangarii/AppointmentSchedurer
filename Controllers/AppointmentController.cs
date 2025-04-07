using AppointmentScheduler.Services;
using AppointmentScheduler.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentScheduler.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService) 
        {
            _appointmentService = appointmentService;
        }
        [Authorize(Roles = Helper.Admin)]
        public IActionResult Index()
        {
            ViewBag.Doctorlist = _appointmentService.GetDoctorList();
            ViewBag.Duration = Helper.GetTimeDropDown();
            ViewBag.PatientList = _appointmentService.GetPatientList();
            return View();
        }
    }
}
