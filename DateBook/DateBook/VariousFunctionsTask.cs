using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DateBook
{
    public static class VariousFunctionsTask
    {
        public static void AddTask(ModelUser user)
        {
            Console.WriteLine("Создание задачи");
            Console.Write("Введите название задачи: ");
            string task_name = Console.ReadLine();
            Console.Write("Введите описание задачи: ");
            string description = Console.ReadLine();
            
            Console.WriteLine("Введите срок выполнения задачи: ");
            Console.Write("день: ");
            int day = int.Parse(Console.ReadLine());
            Console.Write("месяц: ");
            int month = int.Parse(Console.ReadLine());
            Console.Write("год: ");
            int year = int.Parse(Console.ReadLine());

            if (month >= 1 && month <= 12 && day >= 1 && day <= 31)
            {
                
                DatabaseTask.AddTask(new ModelTask(task_name, user, description, new DateTime(year, month, day)));
            }
            else
            {
                Console.WriteLine("Проверьте значения ");
            }
        }
        public static void EditTask(ModelUser user)
        {
            Console.WriteLine("Редактировать задачу");
            Console.Write("Введите название задачи: ");
            string task_name = Console.ReadLine();
           
            Console.WriteLine("Введите новые изменения");
            Console.Write("Введите новое название задачи: ");
            string new_task_name = Console.ReadLine();
            Console.Write("Введите новое описание задачи: ");
            string new_description = Console.ReadLine();
            
            Console.WriteLine("Введите срок выполнения задачи: ");
            Console.Write("день: ");
            int day = int.Parse(Console.ReadLine());
            Console.Write("месяц: ");
            int month = int.Parse(Console.ReadLine());
            Console.Write("год: ");
            int year = int.Parse(Console.ReadLine());
            
            if (month >= 1 && month <= 12 && day >= 1 && day <= 31)
            {
                DatabaseTask.EditTask(new ModelTask(new_task_name, user, new_description, new DateTime(year, month, day)), task_name);
            }
            else
            {
                Console.WriteLine("Проверьте значения даты");
            }
        }
        public static void DeleteTask(ModelUser user)
        {
            Console.WriteLine("Удалить задачу");
            Console.Write("Введите название задачи: ");
            string task_name = Console.ReadLine();
            DatabaseTask.DeleteTask((int)user.IdUser, task_name);
        }
    }
}
