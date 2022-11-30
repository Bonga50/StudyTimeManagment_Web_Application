using Microsoft.AspNetCore.Mvc.RazorPages;
using ProgPoe3._1.Classes;
using System;
using System.Collections.Generic;

namespace ProgPoe3._1.Pages
{
    public class ModulesModel : PageModel
    {

        public List<Module> prList = new List<Module>();
        //variable that will be used to display the result 

        public DateTime curretdate = DateTime.Now;
        public List<double> totalhrsDone = new List<double>();
        public List<double> weekhrs = new List<double>();
        public string week;
        public string username = StudentUser.Username;
        public List<ViewModule> ModulesList = new List<ViewModule>();
        public void OnGet()
        {
            Module objProject1 = new Module();
            //populating the list with data from the database
            prList = objProject1.GetModules(StudentUser.Username);
            ListSearch();


        }

        public void ListSearch()
        {
            Module objProject = new Module();

            if (prList != null)
            {

                for (int i = 0; i < prList.Count; i++)
                {
                    
                    //Remaining semester hrs 
                    totalhrsDone = objProject.getTotalhrs(StudentUser.Username, prList[i].ModuleCode);

                    //Getting the current week
                    week = objProject.trackWeek(curretdate, prList[i].SemesterStartDate, prList[i].SemesterStartDate.AddDays(7), i, prList);
                    //Remaining study hrs for week
                    weekhrs = objProject.getStudyWeekhrs(StudentUser.Username, prList[i].ModuleCode, week);

                    double tempweeklySelfStudy = ProjectModule.TheStudyClass.SelfStudy(
                        prList[i].NumOfCredits,
                        prList[i].SemesterStartDate,
                        prList[i].SemesterWeeks,
                        prList[i].HoursPerWeek);
                    double tempTotalSelfStudy = ProjectModule.TheStudyClass.TotalhrsPreMod(
                        prList[i].NumOfCredits,
                        prList[i].SemesterStartDate,
                        prList[i].SemesterWeeks,
                        prList[i].HoursPerWeek);

                    ModulesList.Add(new ViewModule
                    {
                        //Module code
                        ModuleCode = prList[i].ModuleCode,
                        //Project Name
                        ModuleName = prList[i].ModuleName,
                        //Weekly study hrs 
                        weeklySelfStudy = tempweeklySelfStudy,
                        //Total study hrs for the semester
                        TotalSelfStudy = tempTotalSelfStudy,
                        //total self study hrs remaining
                        TotalRemainingHrs = tempTotalSelfStudy - totalhrsDone[0],
                        //Total weekly hrs remaining
                        weeklyRemainingHrs = tempweeklySelfStudy - weekhrs[0],
                        //Weeklyhrs done 
                        weeklyHrsDone = weekhrs[0],
                        //totalhrs done
                        TotalHrsDone = totalhrsDone[0],
                        //Number of credits 
                        NumOfCredits = prList[i].NumOfCredits,
                        //Class hrs per week
                        HoursPerWeek = prList[i].HoursPerWeek,
                        //semester start date
                        SemesterStartDate = prList[i].SemesterStartDate,
                        //Number of weeks in semester
                        SemesterWeeks = prList[i].SemesterWeeks
                    });















                }
            }

        }

    }
}
