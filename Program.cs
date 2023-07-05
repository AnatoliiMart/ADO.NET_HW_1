using System;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.Remoting.Messaging;


namespace ADO.NET_HW_1
{
    internal class Program
    {
        static SqlConnection connection;
        static void Main()
        {
            connection = new SqlConnection
            {
                ConnectionString =
                @"Data Source = DESKTOP-VR0F9ME\SQLEXPRESS;
                  Initial Catalog = VegsAndFruits;
                  Integrated Security = true;"
            };

            try
            {
                //открываем соединение
                connection.Open();
                Console.WriteLine("Connection openned");

                Console.WriteLine("\n***************ЗАДАНИЕ 3***************");
                Console.WriteLine("\n------------------3.1------------------");
                ShowAll();
                Console.WriteLine("\n------------------3.2------------------");
                ShowOnlyNames();
                Console.WriteLine("\n------------------3.3------------------");
                ShowOnlyColors();
                Console.WriteLine("\n------------------3.4------------------");
                ShowMaxCalories();
                Console.WriteLine("\n------------------3.5------------------");
                ShowMinCalories();
                Console.WriteLine("\n------------------3.6------------------");
                ShowAverageCalories();


                Console.WriteLine("\n***************ЗАДАНИЕ 4***************");
                Console.WriteLine("\n------------------4.1------------------");
                ShowAmountOfVegs();
                Console.WriteLine("\n------------------4.2------------------");
                ShowAmountOfFruits();
                Console.WriteLine("\n------------------4.3------------------");
                ShowAmountOfFruitsAndVegsConcreteColor("Yellow");
                Console.WriteLine("\n------------------4.4------------------");
                ShowAmountOfFruitsAndVegsEachColor();
                Console.WriteLine("\n------------------4.5------------------");
                ShowAmountOfFruitsAndVegsLowerByCalories(120);
                Console.WriteLine("\n------------------4.6------------------");
                ShowAmountOfFruitsAndVegsHigerByCalories(120);
                Console.WriteLine("\n------------------4.7------------------");
                ShowAmountOfFruitsAndVegsInSelectedRangeOfCalories(120, 200);
                Console.WriteLine("\n------------------4.8------------------");
                ShowAmountOfFruitsAndVegsWithRedOrYellowColor();

            }
            finally
            {
                //закрываем соединение
                connection.Close();
                Console.WriteLine("Connection closed");
            }
        }

        static void ShowAll()
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT * 
                                FROM VegsFruits";

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
                Console.WriteLine($"{reader[0]}:\t{reader[1]}\t{reader[2]}\t\t{reader[3]}\t{reader[4]}");

            reader.Close();
        }

        static void ShowOnlyNames()
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT ID, Name 
                                FROM VegsFruits
                                ORDER BY ID";

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
                Console.WriteLine($"{reader[0]}:\t{reader[1]}");

            reader.Close();
        }

        static void ShowOnlyColors()
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT ID, Color 
                                FROM VegsFruits
                                ORDER BY ID";

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
                Console.WriteLine($"{reader[0]}:\t{reader[1]}");

            reader.Close();
        }

        static void ShowMaxCalories()
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT MAX(Calories) 
                                FROM VegsFruits";
            Console.WriteLine("Max calories:\t" + cmd.ExecuteScalar().ToString());
        }

        static void ShowMinCalories()
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT MIN(Calories) 
                                FROM VegsFruits";
            Console.WriteLine("Min calories:\t" + cmd.ExecuteScalar().ToString());
        }

        static void ShowAverageCalories()
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT AVG(CONVERT(real, Calories)) 
                                FROM VegsFruits";
            Console.WriteLine("Average calories:\t" + cmd.ExecuteScalar().ToString());
        }


        static void ShowAmountOfVegs() 
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT COUNT(*)
                                FROM VegsFruits
                                WHERE Type = 'Vegetable'";
            Console.WriteLine("Amount of vegetables:\t" + cmd.ExecuteScalar().ToString());
        }

        static void ShowAmountOfFruits()
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT COUNT(*)
                                FROM VegsFruits
                                WHERE Type = 'Fruit'";
            Console.WriteLine("Amount of fruits:\t" + cmd.ExecuteScalar().ToString());
        }

        static void ShowAmountOfFruitsAndVegsConcreteColor(string color)
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = $"SELECT COUNT(*) " +
                              $"FROM VegsFruits" +
                              $" WHERE Color = '{color}'";
            Console.WriteLine("Amount of fruits and vegetables of concrete color:\t" + cmd.ExecuteScalar().ToString());
        }

        static void ShowAmountOfFruitsAndVegsEachColor()
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = $"SELECT COUNT(*), " +
                              $"Color FROM VegsFruits" +
                              $" GROUP BY Color";
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
                Console.WriteLine($"{reader[0]}:\t{reader[1]}");

            reader.Close();
        }

        static void ShowAmountOfFruitsAndVegsLowerByCalories(int caloriesAmount)
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = $"SELECT * " +
                              $"FROM VegsFruits" +
                              $" WHERE Calories < {caloriesAmount}";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
                Console.WriteLine($"{reader[0]}:\t{reader[1]}\t{reader[2]}\t\t" +
                                  $"{reader[3]}\t{reader[4]}");

            reader.Close();
        }

        static void ShowAmountOfFruitsAndVegsHigerByCalories(int caloriesAmount)
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = $"SELECT * " +
                              $"FROM VegsFruits" +
                              $" WHERE Calories > {caloriesAmount}";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
                Console.WriteLine($"{reader[0]}:\t{reader[1]}\t{reader[2]}\t\t" +
                                  $"{reader[3]}\t{reader[4]}");

            reader.Close();
        }

        static void ShowAmountOfFruitsAndVegsInSelectedRangeOfCalories(int caloriesAmountLowerRange, int caloriesAmountUpperRange)
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = $"SELECT * " +
                              $"FROM VegsFruits" +
                              $" WHERE Calories BETWEEN {caloriesAmountLowerRange} AND {caloriesAmountUpperRange}";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
                Console.WriteLine($"{reader[0]}:\t{reader[1]}\t{reader[2]}\t\t" +
                                  $"{reader[3]}\t{reader[4]}");

            reader.Close();
        }

        static void ShowAmountOfFruitsAndVegsWithRedOrYellowColor()
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = $"SELECT * " +
                              $"FROM VegsFruits" +
                              $" WHERE Color = 'Red' OR Color = 'Yellow'";
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
                Console.WriteLine($"{reader[0]}:\t{reader[1]}\t{reader[2]}\t\t" +
                                  $"{reader[3]}\t{reader[4]}");

            reader.Close();
        }
    }
}
