using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace DateBook
{
    public static class AuthorizationAndRegistation
    {
        public static (bool, ModelUser) Authorization()
        {
            Console.Write("Ведите логин: ");
            string login = Console.ReadLine();
            Console.Write("Ведите пароль: ");
            string password = Console.ReadLine();

            var sqlCode = $"select id_user from users_db where login_user = '{login}' and password_user = '{password}'";

            using var cmd = new NpgsqlCommand(sqlCode, DatabaseConnection.GetSqlConnection());
            
            int? access = Convert.ToInt32(cmd.ExecuteScalar()); 

            if (access != 0)
            {
                return (true, new ModelUser(access, login, password));
            }

            return (false, new ModelUser(null, null, null));
        }

        public static (bool, ModelUser) Registation()
        {
            Console.Write("Ведите логин: ");
            string login = Console.ReadLine();
            Console.Write("Ведите пароль: ");
            string password = Console.ReadLine();

            var sqlCode = $"select COUNT(*) from users_db where login_user = '{login}'";

            using (var cmd = new NpgsqlCommand(sqlCode, DatabaseConnection.GetSqlConnection()))
            {
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count == 1)
                {
                    Console.WriteLine("\nТакой логин уже существует\n");
                    return (false, new ModelUser(null, null, null));
                }
            }

            var sqlCode2 = $"insert into users_db (login_user, password_user) values ('{login}', '{password}')";

            using (var cmd = new NpgsqlCommand(sqlCode2, DatabaseConnection.GetSqlConnection()))
            {
                cmd.ExecuteNonQuery();
            }

            int access;

            var sqlCode3 = $"select id_user from users_db where login_user = '{login}' and password_user = '{password}'";

            using (var cmd = new NpgsqlCommand(sqlCode3, DatabaseConnection.GetSqlConnection()))
            {
                access = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return (true, new ModelUser(access, login, password));
        }
    }
}
