using System.ComponentModel.DataAnnotations;
using TestProjectWebApi.Models;

namespace TestProjectWebApi.Dtos
{
    public class PersonDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int? Age { get; set; }
        [Required]
        public string Passport { get; set; }
        public ICollection<Account>? Accounts { get; set; }

    }
}
