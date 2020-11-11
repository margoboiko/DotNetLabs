using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NET3.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace NET3
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class DBMiddleware
    {
        private readonly RequestDelegate _next;

        public DBMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, phone_callContext db)
        {
            try

            {
                httpContext.Response.ContentType = "text/html; charset=utf-8";

                await httpContext.Response.WriteAsync("<img src='/images/logo.png' width=100 heigh=100 alt='My image' />");
                await httpContext.Response.WriteAsync("<h2>ТЕЛЕФОННІ ДЗВІНКИ</h2>");

                var person = db.Person.ToList();

                await httpContext.Response.WriteAsync("</br><h4>Список користувачів:</h4>");

                await httpContext.Response.WriteAsync(String.Join("</br>", person));


                await httpContext.Response.WriteAsync("</br></br><h4>Список 5 міст:</h4>");
                var city = db.City.ToList();
                await httpContext.Response.WriteAsync(string.Join("</br>", city));


                await httpContext.Response.WriteAsync("</br></br><h4>Список користувачів, які здійснили хоча б 2 дзвінки в різні міста:</h4>");
                var calling = db.Person
                        .Join(db.Calling, person => person.Id, calling => calling.Personid,
                        (p, c) => new { PersonId = p.Id, Surname = p.Surname, Name = p.Name, FName = p.Fname, c.Personid })
                        .ToList()
                        .GroupBy(table => new { table.PersonId, table.Surname, table.Name, table.FName })
                        .Where(g => g.Count() >= 2);

                foreach (var p in calling)
                {
                    await httpContext.Response.WriteAsync($"{p.Key.PersonId}   {p.Key.Surname}    {p.Key.Name}    {p.Key.FName} " + $"Дзвінки: {p.Count()}</br>");
                }
            }
            catch (Exception ex)
            {
                await httpContext.Response.WriteAsync($"Error: {ex.Message}");
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DBMiddleware>();
        }
    }
}