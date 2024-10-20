using BookStore.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class BookModel
    {
        //[DataType(DataType.Url)]
        //[Required]
        //public string MyField { get; set; }
        public int Id { get; set; }
        [StringLength(100,MinimumLength = 5)]
        [Required(ErrorMessage ="Please enter the title of your book")]
        public string Title { get; set; }
        [StringLength(100, MinimumLength = 5)]
        [Required(ErrorMessage = "Please enter the author name.")]
        public string Author { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        [Required(ErrorMessage = "Please choose language of your book.")]
        public string Language { get; set; }

        [Required(ErrorMessage = "Please choose language of your book.")]
        public List<string> MultiLanguage { get; set; }

        [Required(ErrorMessage = "Please choose language of your book.")]
        public LanguageEnum LanguageEnum { get; set; }



        [Display(Name = "Total pages of book")]
        [Required(ErrorMessage = "Please enter the total pages.")]
        public int? TotalPages { get; set; }
    }
}
