using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TraineeProject;
using TraineeProject.Database;

namespace TraineeProject.Tests.Integration
{
    internal class TraineeApplication : WebApplicationFactory<Program>
    {
        public UserIdProvider UserIdProvider { get; } = new UserIdProvider();
        private string dbContextGuid = Guid.NewGuid().ToString();

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddSingleton<IUserIdProvider>(UserIdProvider);
                services.Remove(services.First(s => (s.ServiceType == typeof(LogContext))));
                services.Remove(services.First(s => (s.ServiceType == typeof(DbContextOptions<LogContext>))));
                services.AddDbContext<LogContext>(options =>
                {
                    options.UseInMemoryDatabase(dbContextGuid);
                });
                services.AddAuthentication("Test").AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>("Test", null);
            });
        }
    }
}
