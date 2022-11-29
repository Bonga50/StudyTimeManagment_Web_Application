using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ProgPoe3._1.Classes
{
    public class Module
    {
        
        public string ModuleCode { get; set; }
        public string ModuleName { get; set; }
        public int NumOfCredits { get; set; }
        public int HoursPerWeek { get; set; }
        public DateTime SemesterStartDate { get; set; }
        public int SemesterWeeks { get; set; }
        public string Username { get; set; }


        public Module() { }

        public Module(string ModCode, string ModName, int Credits, int Hours, DateTime StartDate, int Weeks, string UserID)
        {
            this.ModuleCode = ModCode;
            this.ModuleName = ModName;
            this.NumOfCredits = Credits;
            this.HoursPerWeek = Hours;
            this.SemesterStartDate = StartDate;
            this.SemesterWeeks = Weeks;
            this.Username = UserID;
        }

        public List<Module> GetModules(string userId)
        {
            SqlConnection conn = Connections.GetConeection();
            string text = $@"select ModuleCode as ModuleCode
            ,ModuleName as ModuleName
            ,NumOfCredits as NumOfCredits
            ,HoursPerWeek as HoursPerWeek
            ,SemesterStartDate as SemesterStartDate
            ,SemesterWeeks as SemesterWeeks
            ,Username as Username
            from Module where Username = '{userId}';";
            List<Module> Proj = null;

            using (conn)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(text, conn);
                var dataRead = cmd.ExecuteReader();
                Proj = getList<Module>(dataRead);
                
            }
            return Proj;

        }

        public void GetMyModule(string userId,string ModCode) {
            SqlConnection conn = Connections.GetConeection();
            string strSelect = $"select Module.ModuleName, Module.ModuleCode,Module.Username from Module where ModuleCode = '{ModCode}' and Username ='{userId}';";
            SqlCommand cmdSelect = new SqlCommand(strSelect, conn);
            DataTable myTable = new DataTable();
            DataRow myRow;
            SqlDataAdapter myAdapter = new SqlDataAdapter(cmdSelect);

            conn.Open();
            myAdapter.Fill(myTable);
            if (myTable.Rows.Count > 0)
            {
                for (int i = 0; i < myTable.Rows.Count; i++)
                {
                    myRow = myTable.Rows[i];
                    ModuleName = (string)myRow[0];
                    ModuleCode = (string)myRow[1];
                    Username = (string)myRow[2];


                }
            }

            conn.Close();

        }

        public List<T> getList<T>(IDataReader Reader)
        {
            List<T> lst = new List<T>();
            while (Reader.Read())
            {
                var type = typeof(T);
                T obj = (T)Activator.CreateInstance(type);
                foreach (var item in type.GetProperties())
                {
                    var propType = item.PropertyType;
                    item.SetValue(obj, Convert.ChangeType(Reader[item.Name].ToString(), propType));
                }
                lst.Add(obj);
            }
            return lst;

        }

        public string trackWeek(DateTime studyDate, DateTime weekStartDate, DateTime weekEndDate, int index, List<Module> projectModules)
        {
            string week = "";
            DateTime tempweekStartDate = weekStartDate;
            DateTime tempweekEndDate = weekEndDate;

            for (int i = 0; i < projectModules[index].SemesterWeeks; i++)
            {
                if (studyDate >= tempweekStartDate && studyDate <= tempweekEndDate)
                {
                    week = "week " + (i + 1);
                    break;
                }
                else
                {
                    tempweekStartDate = tempweekEndDate;
                    tempweekEndDate = tempweekStartDate.AddDays(7);
                }

            }
            return week;
        }

        public List<double> getTotalhrs(string UserID, string ModCode)
        {
            SqlConnection conn = Connections.GetConeection();
            string text = $@"select sum(Studyhrs) from StudyLogger where Username='{UserID}' and ModuleCode = '{ModCode}';";

            List<double> hrs = new List<double>();
            hrs.Add(0);
            using (conn)
            {
                SqlCommand cmdSelect = new SqlCommand(text, conn);
                conn.Open();
                using (SqlDataReader reader = cmdSelect.ExecuteReader())
                {
                    reader.Read();
                    if (reader.IsDBNull(0) != true)
                    {

                        double thrs = Convert.ToDouble(reader[0]);
                        hrs[0] = thrs;
                    }
                    else
                    {
                        hrs[0] = 0;
                    }

                }
                
            }
            return hrs;
        }

        public List<double> getStudyWeekhrs(string username, string ModCode, string weeks)
        {
            SqlConnection conn = Connections.GetConeection();
            string text = $@"select sum(Studyhrs) from StudyLogger where Username = '{username}' and ModuleCode = '{ModCode}' and weeks = '{weeks}';";
            List<double> hrs = new List<double>();
            hrs.Add(0);
            using (conn)
            {
               
                SqlCommand cmdSelect = new SqlCommand(text, conn);
                conn.Open();
                using (SqlDataReader reader = cmdSelect.ExecuteReader())
                {
                    reader.Read();
                    if (reader.IsDBNull(0) != true)
                    {

                        double thrs = Convert.ToDouble(reader[0]);
                        hrs[0] = thrs;
                    }
                    else
                    {
                        hrs[0] = 0;
                    }

                }
                
            }
            return hrs;
        }

        public void CreateLog(
            string Username,
            string ModuleCode,
            DateTime Studydate,
            double studyhrs,
            string ModuleName,
            string weeks
            )
        {
            SqlConnection conn = Connections.GetConeection();
            string text = $"insert into StudyLogger values('{Username}','{Studydate}',{studyhrs},'{ModuleName}','{ModuleCode}','{weeks}');";
            using (conn)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(text, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                conn.Close();
            }
        }

    }
    /// <summary>
    /// The following class wll be used to view all the attributes in table form
    /// </summary>
    public class ViewModule {
        public string ModuleCode { get; set; }
        public string ModuleName { get; set; }
        public int NumOfCredits { get; set; }
        public int HoursPerWeek { get; set; }
        public DateTime SemesterStartDate { get; set; }
        public int SemesterWeeks { get; set; }
        public string Username { get; set; }
        public double weeklySelfStudy { get; set; }

        public double TotalSelfStudy { get; set; }
        public double weeklyRemainingHrs { get; set; }
        public double TotalRemainingHrs { get; set; }
    }
}
