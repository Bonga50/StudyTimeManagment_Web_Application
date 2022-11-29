using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProgPoe3._1.Classes;
namespace ProgPoe3._1.Pages
{
    public class SignUpModel : PageModel
    {
        public void OnGet()
        {

        }

        public void OnPost() {
            try
            {
                Student stu = new Student();

                string Username = Request.Form["txtuserID"];
                string Name = Request.Form["txtName"];
                string Password = Request.Form["txtPassword"];

                string encryptedpass = stu.encryptPass(Password);
                stu.addStudent(Username, encryptedpass, Name);
                Response.Redirect("/Login");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

           

        }
    }
}
