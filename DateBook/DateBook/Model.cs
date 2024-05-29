using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DateBook
{
    public class ModelUser
    {
        public int? IdUser { get; private set; }
        public string? Login { get; private set; }
        public string? Password { get; private set; }

        public ModelUser(int? id, string? login, string? password)
        {
            IdUser = id;
            Login = login;
            Password = password;
        }
    }

    public class ModelTask
    {
        public string TaskName { get; private set; }
        public ModelUser User { get; private set; }
        public string Description { get; private set; }
        public DateTime DateEndTask { get; private set; }

        public ModelTask(string name, ModelUser user, string description, DateTime dateEndTask)
        {
            TaskName = name;
            User = user;
            Description = description;
            DateEndTask = dateEndTask;
        }
    }
}
