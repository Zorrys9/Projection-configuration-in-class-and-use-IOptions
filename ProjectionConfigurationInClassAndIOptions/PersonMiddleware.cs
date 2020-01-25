using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using ProjectionConfigurationInClassAndIOptions.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectionConfigurationInClassAndIOptions
{
    public class PersonMiddleware
    {
        private readonly RequestDelegate _next;
        public Person Person { get; }
        public PersonMiddleware(RequestDelegate next, IOptions<Person> options)
        {
            _next = next;
            Person = options.Value;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.ContentType = "text/html;charset=utf-8";
            var sb = new StringBuilder();
            sb.Append($"{Person.Name} {Person.Age} <br><b>Languages:<b><ul>");
            foreach(string lang in Person.Languages)
            {
                 sb.Append($"<li>{lang}</li>");
            }
            sb.Append($"</ul><br>{Person.Company.Title} {Person.Company.Country}");
            await context.Response.WriteAsync(sb.ToString());
        }
    }
}
