using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace DateBook
{
    public class DatabaseTask
    {
        public static void AddTask(ModelTask task)
        {
            string sql = "INSERT INTO tasks_db (task_name, id_user, description_task, date_end) Values (@TaskName, @IdUser, @Description, @DateEndTask)";
            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, DatabaseConnection.GetSqlConnection()))
            {
                cmd.Parameters.AddWithValue("@TaskName", task.TaskName);
                cmd.Parameters.AddWithValue("@IdUser", task.User.IdUser);
                cmd.Parameters.AddWithValue("@Description", task.Description);
                cmd.Parameters.AddWithValue("@DateEndTask", task.DateEndTask);

                cmd.ExecuteNonQuery();
            }
        }
        
        public static void EditTask(ModelTask task, string name)
        {
            string sql = "UPDATE tasks_db SET description_task = @NewDescription, date_end = @NewDateEndTask, task_name = @NewTaskName WHERE id_user = @IdUser AND task_name = @name";

            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, DatabaseConnection.GetSqlConnection()))
            {
                cmd.Parameters.AddWithValue("@NewTaskName", task.TaskName);
                cmd.Parameters.AddWithValue("@IdUser", task.User.IdUser);
                cmd.Parameters.AddWithValue("@NewDescription", task.Description);
                cmd.Parameters.AddWithValue("@NewDateEndTask", task.DateEndTask);
                cmd.Parameters.AddWithValue("@name", name);

                cmd.ExecuteNonQuery();
            }
        }
        
        public static void DeleteTask(int user_id, string task_name)
        {
            string sql = "DELETE FROM tasks_db WHERE task_name = @TaskName AND id_user = @IdUser";
            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, DatabaseConnection.GetSqlConnection()))
            {
                cmd.Parameters.AddWithValue("@IdUser", user_id);
                cmd.Parameters.AddWithValue("@TaskName", task_name);

                cmd.ExecuteNonQuery();
            }
        }
        
        public static void GetTasksToday(int user_id, DateTime today)
        {
            string sql = "SELECT * FROM tasks_db WHERE id_user = @IdUser AND date_end = @DateEndTask";

            using var cmd = new NpgsqlCommand(sql, DatabaseConnection.GetSqlConnection());

            cmd.Parameters.AddWithValue("@IdUser", user_id);
            cmd.Parameters.AddWithValue("@DateEndTask", today);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"\nНазвание: {reader[0]} Описание: {reader[2]} Дата окончания: {reader[3]}");
            }
            Console.WriteLine();
        }
        
        public static void GetTasksTomottow(int user_id, DateTime tomorrow)
        {
            string sql = "SELECT * FROM tasks_db WHERE id_user = @user_id AND date_end = @due_date";

            using var cmd = new NpgsqlCommand(sql, DatabaseConnection.GetSqlConnection());

            cmd.Parameters.AddWithValue("@user_id", user_id);
            cmd.Parameters.AddWithValue("@due_date", tomorrow);

            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine($"\nНазвание: {reader[0]} Описание: {reader[2]} Дата окончания: {reader[3]}");
            }
            Console.WriteLine();
        }
        
        public static void GetTasksWeekly(int user_id, DateTime startDate, DateTime endDate)
        {
            string sql = "SELECT * FROM tasks_db WHERE id_user = @user_id AND date_end >= @start_date AND date_end <= @end_date";

            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, DatabaseConnection.GetSqlConnection()))
            {
                cmd.Parameters.AddWithValue("@user_id", user_id);
                cmd.Parameters.AddWithValue("@start_date", startDate);
                cmd.Parameters.AddWithValue("@end_date", endDate);

                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"\nНазвание: {reader[0]} Описание: {reader[2]} Дата окончания: {reader[3]}");
                    }
                }
                Console.WriteLine();
            }
        }

        public static void GetAllTasks(int user_id)
        {
            string sql = "SELECT * FROM tasks_db WHERE id_user = @IdUser";

            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, DatabaseConnection.GetSqlConnection()))
            {
                cmd.Parameters.AddWithValue("@IdUser", user_id);

                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"\nНазвание: {reader[0]} Описание: {reader[2]} Дата окончания: {reader[3]}");
                    }
                    Console.WriteLine();
                }
            }
        }

        public static void GetUpcomingTasks(int user_id, DateTime currentDate)
        {
            string sql = "SELECT * FROM tasks_db WHERE id_user = @IdUser AND date_end >= @current_date";
            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, DatabaseConnection.GetSqlConnection()))
            {
                cmd.Parameters.AddWithValue("@IdUser", user_id);
                cmd.Parameters.AddWithValue("@current_date", currentDate);

                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"\nНазвание: {reader[0]} Описание: {reader[2]} Дата окончания: {reader[3]}");
                    }
                    Console.WriteLine();
                }
            }
        }

        public static void GetPastDueTasks(int user_id, DateTime currentDate)
        {
            string sql = "SELECT * FROM tasks_db WHERE id_user = @IdUser AND date_end < @current_date";
            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, DatabaseConnection.GetSqlConnection()))
            {
                cmd.Parameters.AddWithValue("@IdUser", user_id);
                cmd.Parameters.AddWithValue("@current_date", currentDate);

                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"\nНазвание: {reader[0]} Описание: {reader[2]} Дата окончания: {reader[3]}");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
