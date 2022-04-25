using System.ComponentModel.DataAnnotations;

namespace StudentApp.Api.Model
{
    public class Student
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string CellNumber { get; set; }

        [Required]
        public string IDorPassport { get; set; }
    }
}