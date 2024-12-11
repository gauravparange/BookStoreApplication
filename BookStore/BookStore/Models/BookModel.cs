using BookStore.Enums;
using BookStore.Helpers;
using Microsoft.AspNetCore.Http;
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
        [StringLength(100, MinimumLength = 5)]
        [Required(ErrorMessage = "Please enter the title of your book")]
        //[MyCustomValidation("abc")]
        //[Required]
        public string Title { get; set; }
        [StringLength(100, MinimumLength = 5)]
        [Required(ErrorMessage = "Please enter the author name.")]
        public string Author { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        [Required(ErrorMessage = "Please choose language of your book.")]
        [Display(Name = "Language")]
        public int LanguageId { get; set; }
        public string Language { get; set; }

        [Display(Name = "Total pages of book")]
        [Required(ErrorMessage = "Please enter the total pages.")]
        public int? TotalPages { get; set; }
        [Display(Name = "Choose the cover photo of the book.")]
        [Required]
        public IFormFile CoverPhoto { get; set; }
        public string CoverImageUrl { get; set; }

        [Display(Name = "Choose the gallery images of the book.")]
        [Required]
        public IFormFileCollection GalleryFiles { get; set; }
        public List<GalleryModel> Gallery { get; set; }
        public IFormFile BookPdf { get; set; }
        public string BookPdfUrl { get; set; }

    }

}
