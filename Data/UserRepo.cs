using SpotifyController.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpotifyController.Data
{
    static public class UserRepo
    {
        static public List<User> Users = new List<User>()
        {
            new User { Name = "Test" },
        };

        static public User TestUser = new User { Name = "Test" };
    }
}
