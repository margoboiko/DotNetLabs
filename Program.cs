using Lr2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Lr2
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new phone_callContext();

            Console.WriteLine("\nList of 10 persons:\n");
            var person = context.Person.ToList();
            Console.WriteLine(string.Join("\n", person));

            Console.WriteLine("\n\nList of 5 cities:\n");
            var city = context.City.ToList();
            Console.WriteLine(string.Join("\n", city));

            Console.WriteLine("\n\nPerson made at least 2 calls to different cities:\n");
            var calling = context.Person
                    .Join(context.Calling, person => person.Id, calling => calling.Personid,
                    (p, c) => new { PersonId = p.Id, Surname = p.Surname, Name = p.Name, FName = p.Fname, c.Personid })
                    .ToList()
                    .GroupBy(table => new { table.PersonId, table.Surname, table.Name, table.FName })
                    .Where(g => g.Count() >= 2);

            foreach (var p in calling)
            {
                Console.WriteLine($"{p.Key.PersonId}   {p.Key.Surname}    {p.Key.Name}    {p.Key.FName} " + $"Count: {p.Count()}");
            }

            Console.ReadLine();

        }

    }
}