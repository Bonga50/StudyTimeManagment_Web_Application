using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgPoe3._1.Classes
{
    /// <summary>
    /// This class will hold all relevent methods related to the student 
    /// it will hold methods to authenticate users, add users to the database,
    /// encrypt and decrypt users credentials.
    /// 
    /// </summary>
    public class Student
    {
        SqlConnection conn = Connections.GetConeection();
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string Password { get; set; }

        public Student() { }
        public Student(string userId, string name, string password)
        {
            this.Username = userId;
            this.FirstName = name;
            this.Password = password;
        }

        //method user to login the user
        public void getStudent(string StudentID)
        {
            string sqlSelect = $"SELECT * FROM Student WHERE StudentId = '{StudentID}'";

            using (conn)
            {
                SqlCommand cmdSelect = new SqlCommand(sqlSelect, conn);
                conn.Open();
                using (SqlDataReader reader = cmdSelect.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Username = (string)reader[0];
                        Password = (string)reader[1];
                        FirstName = (string)reader[2];
                        
                    }
                }
            }
        }

        //method used to sign up a user
        public void addStudent(string Username,string Password,string Name) {

            string sqlInsert = @$"insert into Student values('{Username}','{Password}','{Name}');";

            using (conn)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlInsert, conn);

                SqlDataReader reader = cmd.ExecuteReader();
                conn.Close();
            }
        }

        //method used to encrypt user password
        public string encryptPass(string password) {
            string hashedPassword = Convert.ToBase64String(Encoding.UTF8.GetBytes(password));
            return hashedPassword;
        }

        //method used to decrypt user password
        public string decryptPass(string password) {
            string unhased = Encoding.UTF8.GetString(Convert.FromBase64String(password));
            return unhased;
        }
      
        
    }
}
