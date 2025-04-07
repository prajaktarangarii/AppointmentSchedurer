using AppointmentScheduler.Models;
using AppointmentScheduler.Utility;
using AppointmentSchedurer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace AppointmentScheduler.DBInitializer
{
    public class DBInitializer : IDBInitializer
    {

        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        //private readonly RoleManager<IdentityUser> _roleManager;

        private readonly RoleManager<IdentityRole> _roleManager;


        public DBInitializer(ApplicationDbContext db
        , UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _roleManager= roleManager;
            _userManager = userManager;
        }

        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0) 
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex)
            { 
            }

            if (_db.Roles.Any(x => x.Name == Utility.Helper.Admin)) return;
            
            _roleManager.CreateAsync(new IdentityRole(Helper.Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Helper.Doctor)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Helper.Patient)).GetAwaiter().GetResult();
            
            _userManager.CreateAsync(new ApplicationUser { UserName="anita@gmail.com",Email= "anita@gmail.com",
                EmailConfirmed=true,Name="Anita Spark" },"Pass@123").GetAwaiter().GetResult();

            ApplicationUser user = _db.Users.FirstOrDefault(u => u.Email == "anita@gmail.com");
            _userManager.AddToRoleAsync(user,Helper.Admin).GetAwaiter().GetResult();


        }
    }
}
