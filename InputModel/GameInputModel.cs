using System;
using System.ComponentModel.DataAnnotations;

namespace ApiGamesCatalog.InputModel
{
    public class GameInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The game name must have between 3 and 100 caracthers")]
        public string Name { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The game name must have between 3 and 100 caracthers")]
        public string Producer { get; set; }
        [Required]
        [Range(0,1000,ErrorMessage ="The price must be between 0 and 1000 USD")]
        public double Price { get; set; }


    }
}
