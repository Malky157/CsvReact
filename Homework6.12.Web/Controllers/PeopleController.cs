using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Homework6._12.Data;
using Homework6._12.Web.ViewModel;
using CsvHelper;
using System.Globalization;
using System.Text;

namespace Homework6._12.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly string _connectionString;
        public PeopleController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        [HttpGet]
        [Route("generatepeople")]
        public IActionResult GeneratePeople(int amount)
        {
            var people = GenerateFakerPeople(amount);
            var csv = BuildPeopleCsv(people);
            return File(Encoding.UTF8.GetBytes(csv), "text/csv", "people.csv");
        }
        
        [HttpPost]
        [Route("uploadfile")]
        public void UploadFile(UploadViewModel upload)
        {
            string base64 = upload.Base64.Substring(upload.Base64.IndexOf(",") + 1);
            byte[] csvBytes = Convert.FromBase64String(base64);
            var people = GetCsvFromBytes(csvBytes);
            var pr = new PersonRepository(_connectionString);
            pr.Add(people);
        }

        [HttpGet]
        [Route("getpeople")]
        public List<Person> GetPeople()
        {
            var pr = new PersonRepository(_connectionString);
            return pr.GetAll();
        }

        [HttpPost]
        [Route("deleteall")]
        public void DeleteAll()
        {
            var pr = new PersonRepository(_connectionString);
            pr.DeleteAll();
        }

        private static List<Person> GenerateFakerPeople(int amount)
        {
            return Enumerable.Range(1, amount).Select(_ => new Person
            {
                FirstName = Faker.Name.First(),
                LastName = Faker.Name.Last(),
                Age = Faker.RandomNumber.Next(2, 120),
                Address = Faker.Address.StreetAddress(),               
                Email = Faker.Internet.Email()
            }).ToList();
        }

        private static string BuildPeopleCsv(List<Person> people)
        {
            var builder = new StringBuilder();
            var stringWriter = new StringWriter(builder);
            using var csv = new CsvWriter(stringWriter, CultureInfo.InvariantCulture);
            csv.WriteRecords(people);
            return builder.ToString();
        }

        private static List<Person> GetCsvFromBytes(byte[] csvBytes)
        {
            using var memoryStream = new MemoryStream(csvBytes);
            var streamReader = new StreamReader(memoryStream);
            using var reader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
            return reader.GetRecords<Person>().ToList();
        }

    }
}
