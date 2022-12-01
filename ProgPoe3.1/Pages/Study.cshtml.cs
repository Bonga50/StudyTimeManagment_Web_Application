using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProgPoe3._1.Classes;

namespace ProgPoe3._1.Pages
{
    public class StudyModel : PageModel
    {
        public List<Module> prList = new List<Module>();
        public DateTime StudyDate;
        public Module ModObj = new Module();
        public string Modcode;
        public void OnGet()
        {
            Modcode = Request.Query["ModuleCode"];
            ModObj.GetMyModule(StudentUser.Username, Modcode);

            Module objProject1 = new Module();
            //populating the list with data from the database
            prList = objProject1.GetModules(StudentUser.Username);
        }
        public void OnPost() {
            Module ModObj1 = new Module();
            try
            {
            //Module code that has been sent from The Module page
            string Modcode = Request.Query["ModuleCode"];
            //The date that the user chooses to study on.
            DateTime Studydate = Convert.ToDateTime(Request.Form["txtStudyDate"]);
                //The number of hours that the user 
            double StudyHrs = double.Parse(Request.Form["txtStudyHrs"]);
               //method to get the semester start date and number of weeks
            ModObj1.getThatOneWeek(StudentUser.Username, Modcode);
                //Calling the get week of the date that the user chose
            string week =  ModObj1.trackThatOneWeek(Studydate, ModObj1.SemesterStartDate, ModObj1.SemesterStartDate.AddDays(7), ModObj1.SemesterWeeks);
                //Will only execute if a the previous method returns a valid value
            if (StudentUser.ValidDate)
            {
                    //Sending the user study data to the database 
                ModObj1.CreateLog(StudentUser.Username, Modcode, Studydate, StudyHrs, ModObj1.ModuleName, week);
                
            }
            Response.Redirect("/Modules");


            }
            catch (Exception)
            {
                Response.Redirect("/Modules");


            }

        }
    }
}
