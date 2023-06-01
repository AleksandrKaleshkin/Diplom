using Microsoft.AspNetCore.Identity;

namespace WebTraining.DB.Models
{
    public class User: IdentityUser
    {
        public string Name { get; set; }

        public List<User> Users { get; set; }
    }
}
