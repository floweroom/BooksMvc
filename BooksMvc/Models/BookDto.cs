using System.ComponentModel.DataAnnotations;

namespace BooksMvc.Models
{
    public class BookDto
    {
        [Required]
        public int Id { get; set; }

        [Display]
        public string Name { get; set; }

        [Display]
        public string Author { get; set; }

        [Display]
        public int Pages { get; set; }

        [Display]
        public int Price { get; set; }
    }
}
