using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework6._12.Data
{
    public class PersonRepository
    {
        private readonly string _connectionString;
        public PersonRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Add(List<Person> people)
        {
            var context = new PeopleDbContext(_connectionString);
            context.People.AddRange(people);
            context.SaveChanges();
        }

        public List<Person> GetAll()
        {
            var context = new PeopleDbContext(_connectionString);
            return context.People.ToList();
        }

        public void DeleteAll()
        {
            var context = new PeopleDbContext(_connectionString);
            context.Database.ExecuteSqlInterpolated($"DELETE People");
        }
    }
}
