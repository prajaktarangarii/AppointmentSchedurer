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
        public static string Admin = "Admin";
        public static string Patient = "Patient";
        public static string Doctor = "Doctor";

        public static List<SelectListItem> GetRolesForDropDown()
        {
            return new List<SelectListItem>
            {
            new SelectListItem{Value=Helper.Admin,Text=Helper.Admin},
            new SelectListItem{Value=Helper.Patient,Text=Helper.Patient},
            new SelectListItem{Value=Helper.Doctor,Text=Helper.Doctor},
            };
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
