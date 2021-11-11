using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CovidAppClientModule
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ClientService service = new ClientService("https://localhost:5001/", new HttpClient());

            _ = service.CreatePatientAsync(new Patient
            {
                Id = 0,
                FirstName = "new",
                LastName = "Client",
                Age = 32,
                Email = "wwsipr0@gmail.com",
                TestDate = DateTimeOffset.Now
            });

            var patients= await service.GetAllPatientsAsync();

            foreach(var item in patients)
            {
                Console.WriteLine(item.FirstName + " " + item.LastName);
            }
            Console.ReadLine();
        }
    }
}
