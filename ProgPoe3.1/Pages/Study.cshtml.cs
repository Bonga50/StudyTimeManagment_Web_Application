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
        public void OnGet()
        {
            string Modcode = Request.Query["ModuleCode"];
            ModObj.GetMyModule(StudentUser.Username, Modcode);

            Module objProject1 = new Module();
            //populating the list with data from the database
            prList = objProject1.GetModules(StudentUser.Username);
        }
        public void OnPost() {
            Module ModObj1 = new Module();
            string Modcode = Request.Query["ModuleCode"];
            DateTime Studydate = Convert.ToDateTime(Request.Form["txtStudyDate"]);
            double StudyHrs = double.Parse(Request.Form["txtStudyHrs"]);

            ModObj1.getThatOneWeek(StudentUser.Username, Modcode);
            string week =  ModObj1.trackThatOneWeek(Studydate, ModObj1.SemesterStartDate, ModObj1.SemesterStartDate.AddDays(7), ModObj1.SemesterWeeks);
            if (StudentUser.ValidDate)
            {
                ModObj1.CreateLog(StudentUser.Username, Modcode, Studydate, StudyHrs, ModObj1.ModuleName, week);
                
            }
            Response.Redirect("/Modules");

        }
    }
}
