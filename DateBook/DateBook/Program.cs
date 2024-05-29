using System;

namespace DateBook
{
    class Program
    {
        static void Main(string[] args)
        {
            bool Access = false;
            ModelUser user = new ModelUser(null, null, null);

            while (!Access)
            {
                Console.WriteLine("Нажмите 1 - для авторизации\nНажмите 2 - для регистации");
                int n = int.Parse(Console.ReadLine());

                if (n == 1)
                {
                    (Access, user) = AuthorizationAndRegistation.Authorization();
                }
                else if (n == 2)
                {
                    (Access, user) = AuthorizationAndRegistation.Registation();
                }
            }
            
            
            Console.WriteLine($"\nДобро пожаловать {user.Login}");
            
            while (true)
            {
                Console.WriteLine("\nСписок действий:");
                Console.WriteLine("1 - Добавить задачу; 2 - Редактировать задачу; 3 - Удалить задачу" +
                    "\nПосмотреть задачи: 4 - на сегодня; 5 - до завтра; 6 - за неделю" +
                    "\nПосмотерть список: 7 - всех задач; 8 - предстоящих задач; 9 - прошедших задач\n");

                Console.Write("Введите действие: ");
                int n = int.Parse(Console.ReadLine());

                switch (n)
                {
                    case 1:
                        VariousFunctionsTask.AddTask(user);
                        break;
                    case 2:
                        VariousFunctionsTask.EditTask(user);
                        break;
                    case 3:
                        VariousFunctionsTask.DeleteTask(user);
                        break;
                    case 4:
                        DatabaseTask.GetTasksToday(Convert.ToInt32(user.IdUser), DateTime.Today);
                        break;
                    case 5:
                        DatabaseTask.GetTasksTomottow(Convert.ToInt32(user.IdUser), DateTime.Today.AddDays(1));
                        break;
                    case 6:
                        DatabaseTask.GetTasksWeekly(Convert.ToInt32(user.IdUser), DateTime.Today, DateTime.Today.AddDays(7));
                        break;
                    case 7:
                        DatabaseTask.GetAllTasks(Convert.ToInt32(user.IdUser));
                        break;
                    case 8:
                        DatabaseTask.GetUpcomingTasks(Convert.ToInt32(user.IdUser), DateTime.Today);
                        break;
                    case 9:
                        DatabaseTask.GetPastDueTasks(Convert.ToInt32(user.IdUser), DateTime.Today);
                        break;
                }
            }            
        }
    }
}