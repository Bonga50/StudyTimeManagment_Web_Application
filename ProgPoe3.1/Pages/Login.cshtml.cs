using Microsoft.AspNetCore.Mvc.RazorPages;
using ProgPoe3._1.Classes;
namespace ProgPoe3._1.Pages
{
    public class LoginModel : PageModel
    {
        
        public string errMsg;
        public void OnGet()
        {
            if (StudentUser.inValidUser)
            {
                 errMsg = "Error incorrect username or password";
            }
            else
            {
                errMsg = "";

            }
    
        }

        public void OnPost()
        {
            Student st = new Student();

            string Username = Request.Form["txtuserID"];
            string Password = Request.Form["txtPassword"];

           
            try
            {
                st.getStudent(Username);
                string decryptPassword = st.decryptPass(st.Password);
                if (st.Username == Username && Password.Equals(decryptPassword))
                {

                    Response.Redirect("/Modules");
                    StudentUser.Username = Username;
                }
                else {
                    StudentUser.inValidUser = true;
                    Response.Redirect("/Login");
                }
                
            }
            catch (System.Exception)
            {

                StudentUser.inValidUser = true;
                Response.Redirect("/Login");
            }
            
        }
    }
}
