using System.ComponentModel.DataAnnotations;
using TestProjectWebApi.Models;

namespace TestProjectWebApi.Dtos
{
    public class AccountDto
    {
        public int Id { get; set; }
        //[DisplayName("Number")]
        public string? AccNumber { get; set; }
        public int? Sum { get; set; }

        //[DisplayName("Person")]
        public int? PersonId { get; set; }
        public Person? Person { get; set; }

        public ICollection<History>? Histories { get; set; }

    }
}
