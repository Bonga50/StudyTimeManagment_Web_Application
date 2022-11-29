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
            string Modcode = Request.Query["ModuleCode"];
            DateTime Studydate = Convert.ToDateTime(Request.Form["txtStudyDate"]);
            double StudyHrs = double.Parse(Request.Form["txtStudyHrs"]);
            ModObj.CreateLog(StudentUser.Username, Modcode, Studydate, StudyHrs,ModObj.ModuleName,
                ModObj.trackWeek(
                        dtStudydate.SelectedDate.Value,
                        AddNewModulePage.ModuleList[cmbModuleDropDown.SelectedIndex].SemesterStartDate,
                        AddNewModulePage.ModuleList[cmbModuleDropDown.SelectedIndex].SemesterStartDate.AddDays(7),
                        cmbModuleDropDown.SelectedIndex));


        }
    }
}
