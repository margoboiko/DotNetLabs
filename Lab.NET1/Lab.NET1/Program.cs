using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1
{
    class Program
    {
        public static List<object[]> GetResult(NpgsqlConnection connection, string select, string from)
        {
            var sqlStatement = string.Format("SELECT {0} FROM {1}", select, from);

            try
            {
                connection.Open();

                List<object[]> result = new List<object[]>();
                using (NpgsqlCommand command = new NpgsqlCommand(sqlStatement, connection))
                {
                    using (NpgsqlDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            var values = new object[dataReader.FieldCount];
                            for (int i = 0; i < dataReader.FieldCount; i++)
                            {
                                values[i] = dataReader[i];
                            }
                            result.Add(values);
                        }
                    }
                }
                connection.Close();
                return result;
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }

        }

        public static void PrintData(List<object[]> list)
        {
            foreach (object[] element in list)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var v in element)
                {
                    sb.Append(v.ToString());
                    sb.Append("   \b   ");
                }
                Console.WriteLine(sb);
            }
        }

        static void Main(string[] args)
        {
            try
            {
                NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder
                {
                    ConnectionString = "Server=127.0.0.1;Port=5432;User Id=postgres;Password=123456;Database=phone_call;"
                };

                using NpgsqlConnection connection = new NpgsqlConnection(builder.ConnectionString);
                Console.WriteLine("\nList of 10 persons:\n");
                List<object[]> person = GetResult(connection, "surname||' '||name||' '||fname, adress, number", "person");
                PrintData(person);

                Console.WriteLine("\n\nList of 5 cities:\n");
                List<object[]> city = GetResult(connection, "city, code", "city");
                PrintData(city);

                Console.WriteLine("\n\nPerson made at least 2 calls to different cities:\n");
                List<object[]> calling = GetResult(connection, "DISTINCT surname||' '||name||' '||fname", "person CROSS JOIN calling WHERE(SELECT COUNT(*) FROM calling WHERE person.id = calling.personid) > 1");
                PrintData(calling);
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.ReadLine();
        }
    }
}