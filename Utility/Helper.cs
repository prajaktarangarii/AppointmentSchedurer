using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentScheduler.Utility
{
    public static class Helper
    {
        public const string Admin = "Admin";
        public static string Patient = "Patient";
        public static string Doctor = "Doctor";
        public static string appointmentAdded = "Appointment added successfully.";
        public static string appointmentUpdated = "Appointment updated successfully.";
        public static string appointmentDeleted = "Appointment deleted successfully.";
        public static string appointmentExists = "Appointment for selected date and time already exists.";
        public static string appointmentNotExists = "Appointment not exists.";
        public static string meetingConfirm = "Meeting confirm successfully.";
        public static string meetingConfirmError = "Error while confirming meeting.";
        public static string appointmentAddError = "Something went wront, Please try again.";
        public static string appointmentUpdatError = "Something went wront, Please try again.";
        public static string somethingWentWrong = "Something went wront, Please try again.";
        public static int success_code = 1;
        public static int failure_code = 0;

        public static List<SelectListItem> GetRolesForDropDown(bool isAdmin)
        {
            if (isAdmin) 
            {
                return new List<SelectListItem>
                {
                new SelectListItem{Value=Helper.Admin,Text=Helper.Admin},
            
                };
            }
            else 
            {
                return new List<SelectListItem>
                {
                new SelectListItem{Value=Helper.Patient,Text=Helper.Patient},
                new SelectListItem{Value=Helper.Doctor,Text=Helper.Doctor},
                };
            }
            
        }
        public static List<SelectListItem> GetTimeDropDown(int startHour = 1, int endHour = 12)
        {
            List<SelectListItem> duration = new List<SelectListItem>();

            for (int i = startHour; i <= endHour; i++)
            {
                duration.Add(new SelectListItem { Value = (i * 60).ToString(), Text = $"{i} Hr" });
                duration.Add(new SelectListItem { Value = (i * 60 + 30).ToString(), Text = $"{i} Hr 30 min" });
            }

            return duration;
        }

    }
}
