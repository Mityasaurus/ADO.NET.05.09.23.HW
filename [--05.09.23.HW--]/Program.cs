using System.Configuration;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.Data;

namespace ___30._08._23.HW___
{
    internal class Program
    {
        private static DbProviderFactory factory;
        private static string connectionString => ConfigurationManager.ConnectionStrings["Default"].ToString();
        private static Stopwatch timer = new Stopwatch();
        static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("Select database\n1 - SqlServer\n2 - Oracle");

                string answer = Console.ReadLine();

                DbProviderFactories.RegisterFactory("Microsoft.Data.SqlClient", SqlClientFactory.Instance);

                if (answer == "1")
                {
                    factory = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["Default"].ProviderName);
                }
                else if (answer == "2")
                {
                    factory = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["OracleDB"].ProviderName);
                }
                else
                {
                    factory = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["Default"].ProviderName);
                }

                int choice = -1;
                while (choice != 0)
                {
                    Console.WriteLine("\nВведiть свiй вибiр");
                    Console.WriteLine("1 - Показати всю iнформацiю");
                    Console.WriteLine("2 - Показати всi назви овочiв та фруктiв");
                    Console.WriteLine("3 - Показати всi кольори");
                    Console.WriteLine("4 - Показати максимальну калорiйнiсть");
                    Console.WriteLine("5 - Показати мiнiмальну калорiйнiсть");
                    Console.WriteLine("6 - Показати середню калорiйнiсть");
                    Console.WriteLine();
                    Console.WriteLine("7 - Показати кiлькiсть овочiв");
                    Console.WriteLine("8 - Показати кiлькiсть фруктiв");
                    Console.WriteLine("9 - Показати кiлькiсть овочiв i фруктiв заданого кольору");
                    Console.WriteLine("10 - Показати кiлькiсть овочiв i фруктiв кожного кольору");
                    Console.WriteLine("11 - Показати овочi та фрукти з калорiйнiстю нижче вказаної");
                    Console.WriteLine("12 - Показати овочi та фрукти з калорiйнiстю вище вказаної");
                    Console.WriteLine("13 - Показати овочi та фрукти з калорiйнiстю у вказаному дiапазонi");
                    Console.WriteLine("14 - Показати усi овочi та фрукти жовтого або червоного кольору");
                    Console.WriteLine();
                    Console.WriteLine("15 - Додати новий овоч або фрукт");
                    Console.WriteLine("16 - Видалили iснуючий овоч або фрукт");
                    Console.WriteLine();
                    Console.WriteLine("0 - Вихiд");

                    choice = int.Parse(Console.ReadLine());
                    string query = "select * from FruitsAndveggies";
                    switch (choice)
                    {
                        case 1:
                            Console.Clear();
                            StartTimer();
                            await ReadDataAsync(factory, query);
                            Console.WriteLine($"\nElapsed time: {StopTimer()} ms");
                            break;
                        case 2:
                            Console.Clear();
                            query = "SELECT Name FROM FruitsAndVeggies";

                            StartTimer();
                            await ReadDataAsync(factory, query);
                            Console.WriteLine($"\nElapsed time: {StopTimer()} ms");
                            break;
                        case 3:
                            Console.Clear();
                            query = "SELECT Color FROM FruitsAndVeggies GROUP BY Color";

                            StartTimer();
                            await ReadDataAsync(factory, query);
                            Console.WriteLine($"\nElapsed time: {StopTimer()} ms");
                            break;
                        case 4:
                            Console.Clear();
                            query = "SELECT MAX(Calories) AS 'Max calories' FROM FruitsAndVeggies";

                            StartTimer();
                            await ReadDataAsync(factory, query);
                            Console.WriteLine($"\nElapsed time: {StopTimer()} ms");
                            break;
                        case 5:
                            Console.Clear();
                            query = "SELECT MIN(Calories) AS 'Min calories' FROM FruitsAndVeggies";

                            StartTimer();
                            await ReadDataAsync(factory, query);
                            Console.WriteLine($"\nElapsed time: {StopTimer()} ms");
                            break;
                        case 6:
                            Console.Clear();
                            query = "SELECT AVG(Calories) AS 'Average calories' FROM FruitsAndVeggies";

                            StartTimer();
                            await ReadDataAsync(factory, query);
                            Console.WriteLine($"\nElapsed time: {StopTimer()} ms");
                            break;
                        case 7:
                            Console.Clear();
                            query = "SELECT COUNT(*) AS 'Number of veggies' FROM FruitsAndVeggies WHERE Type=0";

                            StartTimer();
                            await ReadDataAsync(factory, query);
                            Console.WriteLine($"\nElapsed time: {StopTimer()} ms");
                            break;
                        case 8:
                            Console.Clear();
                            query = "SELECT COUNT(*) AS 'Number of fruits' FROM FruitsAndVeggies WHERE Type=1";

                            StartTimer();
                            await ReadDataAsync(factory, query);
                            Console.WriteLine($"\nElapsed time: {StopTimer()} ms");
                            break;
                        case 9:
                            Console.Clear();

                            Console.WriteLine("Введiть бажаний колiр");
                            string color = Console.ReadLine();
                            query = $"SELECT COUNT(*) AS 'Number' FROM FruitsAndVeggies WHERE Color='{color}'";

                            StartTimer();
                            await ReadDataAsync(factory, query);
                            Console.WriteLine($"\nElapsed time: {StopTimer()} ms");
                            break;
                        case 10:
                            Console.Clear();
                            query = "SELECT COUNT(*) AS 'Number', Color FROM FruitsAndVeggies GROUP BY Color";

                            StartTimer();
                            await ReadDataAsync(factory, query);
                            Console.WriteLine($"\nElapsed time: {StopTimer()} ms");
                            break;
                        case 11:
                            Console.Clear();

                            Console.WriteLine("Введiть максимально допустиму калорiйнiсть");
                            int maxCalories = int.Parse(Console.ReadLine());
                            query = $"{query} WHERE Calories < {maxCalories}";

                            StartTimer();
                            await ReadDataAsync(factory, query);
                            Console.WriteLine($"\nElapsed time: {StopTimer()} ms");
                            break;
                        case 12:
                            Console.Clear();

                            Console.WriteLine("Введiть мiнiмально допустиму калорiйнiсть");
                            int minCalories = int.Parse(Console.ReadLine());
                            query = $"{query} WHERE Calories > {minCalories}";

                            StartTimer();
                            await ReadDataAsync(factory, query);
                            Console.WriteLine($"\nElapsed time: {StopTimer()} ms");
                            break;
                        case 13:
                            Console.Clear();

                            Console.WriteLine("Введiть мiнiмальну границю калорiйностi");
                            int start = int.Parse(Console.ReadLine());
                            Console.WriteLine("Введiть максимальну границю калорiйностi");
                            int end = int.Parse(Console.ReadLine());
                            query = $"{query} WHERE Calories BETWEEN {start} AND {end}";

                            StartTimer();
                            await ReadDataAsync(factory, query);
                            Console.WriteLine($"\nElapsed time: {StopTimer()} ms");
                            break;
                        case 14:
                            Console.Clear();
                            query = $"{query} WHERE Color = 'Red' OR Color = 'Yellow'";

                            StartTimer();
                            await ReadDataAsync(factory, query);
                            Console.WriteLine($"\nElapsed time: {StopTimer()} ms");
                            break;
                        case 15:
                            Console.Clear();
                            query = GetAddItemQuery();

                            StartTimer();
                            await ExecCommandAsync(factory, query);
                            Console.WriteLine($"\nElapsed time: {StopTimer()} ms");
                            break;
                        case 16:
                            Console.Clear();
                            query = GetDeleteItemQuery();

                            StartTimer();
                            await ExecCommandAsync(factory, query);
                            Console.WriteLine($"\nElapsed time: {StopTimer()} ms");
                            break;
                        case 0:
                            break;
                        default:
                            Console.WriteLine("Помилковий вибiр!");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void StartTimer()
        {
            timer.Reset();
            timer.Start();
        }
        private static long StopTimer()
        {
            timer.Stop();
            return timer.ElapsedMilliseconds;
        }

        private static async Task ReadDataAsync(DbProviderFactory factory, string query)
        {
            using (DbConnection conn = factory.CreateConnection())
            {
                conn.ConnectionString = connectionString;

                await conn.OpenAsync();

                using (DbCommand cmd = factory.CreateCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = query;

                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            Console.Write(reader.GetName(i).ToString().PadRight(20));
                        }
                        Console.WriteLine("\n");
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                Console.Write(reader[reader.GetName(i)].ToString().PadRight(20));
                            }
                            Console.WriteLine();
                        }
                    }
                }

                await conn.CloseAsync();
            }
        }
        private static async Task ExecCommandAsync(DbProviderFactory factory, string query)
        {
            using (DbConnection conn = factory.CreateConnection())
            {
                conn.ConnectionString = connectionString;

                await conn.OpenAsync();

                using (DbCommand cmd = factory.CreateCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = query;

                    Console.WriteLine(cmd.ExecuteNonQuery());
                }

                await conn.CloseAsync();
            }
        }

        private static string GetAddItemQuery()
        {
            Console.WriteLine("Enter name");
            string name = Console.ReadLine();

            Console.WriteLine("Enter type [0 - Vegetable | 1 - Fruit]");
            string type = Console.ReadLine();

            if (type != "0")
            {
                type = "1";
            }

            Console.WriteLine("Enter color");
            string color = Console.ReadLine();

            Console.WriteLine("Enter calories");
            int calories = int.Parse(Console.ReadLine());

            return $"insert into FruitsAndVeggies values('{name}', {type}, '{color}', {calories})";
        }
        private static string GetDeleteItemQuery()
        {
            Console.WriteLine("Enter the id of the fruit or vegetable you want to delete");
            string id = Console.ReadLine();

            return $"delete from FruitsAndVeggies where ID={id}";
        }
    }
}
