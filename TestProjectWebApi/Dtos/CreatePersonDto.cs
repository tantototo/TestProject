using System.ComponentModel.DataAnnotations;
using TestProjectWebApi.Models;

namespace TestProjectWebApi.Dtos
{
    public class CreatePersonDto
    {
        [MinLength(1)]
        [Required]
        public string Name { get; set; }
        
        [Required]
        [Range(18, Double.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
        public int Age { get; set; }

        [MinLength(5)]
        [Required]
        public string Passport { get; set; }
    }
}
