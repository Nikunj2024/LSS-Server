
using System.ComponentModel.DataAnnotations;


namespace LSS.Model
{
    public class AppUser
    {
        public AppUser()
        {
        }

        public AppUser(string user_name, string email, string password)
        {
            this.user_name = user_name;
            this.email = email;
            this.password = password;
        }

        [Key]
        public int Id { get; set; }
        public string user_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string role { get; set; } = "Servicer";
    }
}