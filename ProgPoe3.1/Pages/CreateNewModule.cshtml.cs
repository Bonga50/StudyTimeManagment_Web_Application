using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProgPoe3._1.Classes;
namespace ProgPoe3._1.Pages
{
    public class CreateNewModuleModel : PageModel
    {
        public void OnGet()
        {
        }
        public void OnPost()
        {
            Module ModObj = new Module();

            string ModuleCode = Request.Form["txtModCode"];
            string ModuleName = Request.Form["txtModName"];
            int NumberOfCredits = Convert.ToInt32(Request.Form["txtNumOfCredits"]);
            int Classhrs = Convert.ToInt32(Request.Form["txtClassHrs"]);
            DateTime SemesterStartDate = Convert.ToDateTime(Request.Form["txtSemStartDate"]);
            int NumOfWeeks = Convert.ToInt32(Request.Form["txtNumOfWeeks"]);

            ModObj.AddNewModule(StudentUser.Username, ModuleCode, ModuleName, NumberOfCredits, Classhrs, SemesterStartDate, NumOfWeeks);

            Response.Redirect("/Modules");
        }
    }
}
