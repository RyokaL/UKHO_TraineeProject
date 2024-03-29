using System;
using Azure.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TraineeProject.Controllers;
using TraineeProject.Database;
using TraineeProject.Models.Views;
using TraineeProject.Repository;

using Azure.Extensions.AspNetCore.Configuration.Secrets;

using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Data.SqlClient;
using Azure.Storage.Blobs;

using Microsoft.Extensions.Azure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Logging;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace TraineeProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var secretClientOptions = new SecretClientOptions()
            {
                Retry =
                    {
                        Delay = TimeSpan.FromSeconds(2),
                        MaxDelay = TimeSpan.FromSeconds(16),
                        MaxRetries = 5,
                        Mode = RetryMode.Exponential
                    }
            };

            var client = new SecretClient(new Uri("https://cdbtrainee.vault.azure.net/"),
                new DefaultAzureCredential(new DefaultAzureCredentialOptions { ExcludeVisualStudioCredential = false }),
                secretClientOptions);

            KeyVaultSecret storageAccountConnection = client.GetSecret("StorageAccountConnectionString");

            services.AddAzureClients(builder =>
            {
                builder.AddBlobServiceClient(storageAccountConnection.Value);
            });

            if (Configuration.GetValue<string>("UseLocalSqlServer") == null)
            {

                KeyVaultSecret sqlSecret = client.GetSecret("SQLLogDbPass");

                BlobServiceClient blobServiceClient = new BlobServiceClient(storageAccountConnection.Value);
                BlobContainerClient cotainterClient = blobServiceClient.GetBlobContainerClient("traineeprojectblobstorage");

                var sqlBuilder = new SqlConnectionStringBuilder
                {
                    UserID = "calumdb",
                    Password = sqlSecret.Value,
                    InitialCatalog = "traineedb-Logs",
                    DataSource = "tcp:cdbtrainee.database.windows.net"
                };

                services.AddDbContext<LogContext>(options =>
                {
                    options.UseSqlServer(sqlBuilder.ConnectionString);
                });
            }
            else
            {
                services.AddDbContext<LogContext>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("TraineeSQLDbLocal"));
                });
            }

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddMicrosoftIdentityWebApi(Configuration.GetSection("AzureAdB2C"));
            //services.AddMicrosoftIdentityWebApiAuthentication(Configuration, "AzureAdB2C");

            services.AddControllersWithViews();

            services.AddScoped<ICharacterRepository<CharacterApiView>, CharacterRepository>();
            services.AddScoped<IParseRepository<LogParseApiView>, ParseRepository>();
            IdentityModelEventSource.ShowPII = true;
            services.AddOptions();
            services.Configure<OpenIdConnectOptions>(Configuration.GetSection("AzureAdB2C"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCors((builder) =>
            {
                builder.WithOrigins("http://localhost:4200", "https://happy-mushroom-038ec4503.1.azurestaticapps.net/");
                builder.AllowCredentials();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
