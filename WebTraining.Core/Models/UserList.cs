using Microsoft.AspNetCore.Mvc.Rendering;
using WebTraining.Core.DTO;
using WebTraining.DB.Models;

namespace WebTraining.Core.Models
{
    public class UserList
    {
            public UserList(List<User> users)
            {

                UsersList = new SelectList(users, "Id", "Name");
            }
            public SelectList? UsersList { get; private set; }        
    }
}
