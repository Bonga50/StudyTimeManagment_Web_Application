using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProgPoe3._1.Classes
{
    public class Connections
    {
        public static SqlConnection GetConeection()
        {

            //Used to get connection string for database
            string fileName = "StudentDB.mdf";
            string pathToFile = AppDomain.CurrentDomain.BaseDirectory + $"\\{fileName}";
            string filePath = Path.GetFullPath(pathToFile).Replace(@"\bin\Debug\net6.0", @"\Data");
            return new SqlConnection($@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={filePath};Integrated Security=True");
        }
    }
}
