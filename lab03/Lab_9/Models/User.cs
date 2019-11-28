using System.ComponentModel.DataAnnotations;

namespace PIS_MVC5_1.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Phone { get; set; }
    }
}