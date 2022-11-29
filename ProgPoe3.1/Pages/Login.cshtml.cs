using Microsoft.AspNetCore.Mvc.RazorPages;
using ProgPoe3._1.Classes;
namespace ProgPoe3._1.Pages
{
    public class LoginModel : PageModel
    {
        public void OnGet()
        {

        }

        public void OnPost()
        {
            Student st = new Student();

            string Username = Request.Form["txtuserID"];
            string Password = Request.Form["txtPassword"];

            st.getStudent(Username);
            string decryptPassword =  st.decryptPass(st.Password);
            if (st.Username == Username && Password.Equals(decryptPassword))
            {

                Response.Redirect("/Modules");
                StudentUser.Username = Username;
            }
            else
            {
                Response.Redirect("/Login");
            }
        }
    }
}
