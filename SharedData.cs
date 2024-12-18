using Shlyapnikova_lr.Models;
using System.Collections.Generic;

namespace Shlyapnikova_lr
{
    public static class SharedData
    {
        public static List<User> Users { get; } = new List<User>
        {
            new User(){ Login = "user", Password = "user" },
            new User(){ Login = "volunteer", Password = "volunteer" },
            new User(){ Login = "admin", Password = "admin" },
        };
    }
}
