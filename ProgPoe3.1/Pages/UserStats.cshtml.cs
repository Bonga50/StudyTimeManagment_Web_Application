using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProgPoe3._1.Classes;
namespace ProgPoe3._1.Pages
{
    public class UserStatsModel : PageModel
    {
        public List<newUserStats> Stats = new List<newUserStats>();
        public void OnGet()
        {
            GetModuleStats();
        }

        public void GetModuleStats() {

            Module Mod = new Module();
            string modCode = Request.Query["ModuleCode"];
            Mod.GetMyModule(StudentUser.Username,modCode);
            int numberOfWeeks = Mod.SemesterWeeks;
            string weekt = "Week ";
            List<double> hrs = new List<double>();
            
            for (int i = 0; i < numberOfWeeks; i++)
            {
                hrs = Mod.getStudyWeekhrs(StudentUser.Username, modCode, weekt+(i + 1));

                Stats.Add(new newUserStats
                {
                    Week = weekt + (i+1),
                    weeklySelfStudyHrs=hrs[0]



                }) ;
            }


        }
    }
}
