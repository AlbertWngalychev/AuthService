using AuthService.Models;

namespace AuthService
{
    public class UsernamePasswordParams
    {
        public UsernamePasswordParams(string name, string password)
        {
            Name = name;
            Password = password;
        }

        public string Name { get; set; }
        public string Password { get; set; }


    }
}
