using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using TraineeProject.Database;
using TraineeProject.Models;
using TraineeProject.Models.Views;

namespace TraineeProject.Tests.Integration
{
    public class CharacterApiTests
    {
        private TraineeApplication _traineeApplication;

        [SetUp]
        public void Setup()
        {
            _traineeApplication = new TraineeApplication();
        }

        [Test]
        public async Task GetCharactersForUserOnlyGetsDataForSpecifiedUser()
        {
            var userGuid = Guid.Parse("5bfc6c46-d632-41de-bb7a-7b597ebe414c");
            var otherGuid = Guid.Parse("93747c97-b68b-471f-af0a-7cfbfe08e3a0");

            _traineeApplication.UserIdProvider.UserId = userGuid;

            using (var scope = _traineeApplication.Services.CreateScope())
            {
                var _logDbContext = scope.ServiceProvider.GetRequiredService<LogContext>();
                _logDbContext.Character.Add(new Character
                {
                    CharacterName = "Testy",
                    WorldServer = "Test",
                    UserId = userGuid.ToString()
                });

                _logDbContext.Character.Add(new Character
                {
                    CharacterName = "Mr Test",
                    WorldServer = "Test2",
                    UserId = otherGuid.ToString()
                });

                _logDbContext.SaveChanges();
            }

            using var client = _traineeApplication.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test", null);

            var response = await client.GetAsync("/api/character/user");

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var resContent = await response.Content.ReadAsStringAsync();
            CharacterApiView[] result = JsonSerializer.Deserialize<CharacterApiView[]>(resContent)!;
            Assert.That(result.Length, Is.EqualTo(1));
        }
    }
}