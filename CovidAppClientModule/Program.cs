using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace CovidAppClientModule
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //TODO
            //add clientId, tenantId, secret token
            var app = ConfidentialClientApplicationBuilder.Create("") // client id
            .WithAuthority("") // tenant id
            .WithClientSecret("")
            .Build();

            var tokenClient = app.AcquireTokenForClient(new[] { "api://eaca2605-e312-4220-813d-97af4ebe2595/.default" });
            var result = await tokenClient.ExecuteAsync();

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse($"Bearer {result.AccessToken}");
            CovidAppClientModule service = new CovidAppClientModule("https://localhost:5001/", httpClient);

            _ = service.CreatePatientAsync(new Patient
            {
                Id = 0,
                FirstName = "new",
                LastName = "Client",
                Age = 32,
                Email = "wwsipr0@gmail.com",
                TestDate = DateTimeOffset.Now
            });

            var patients = await service.GetAllPatientsAsync();

            foreach (var item in patients)
            {
                Console.WriteLine(item.FirstName + " " + item.LastName);
            }
            await service.InvalidAsync();
            Console.ReadLine();
        }
    }
}
