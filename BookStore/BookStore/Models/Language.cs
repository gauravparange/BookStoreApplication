using BookStore.Data;
using System.Collections.Generic;

namespace BookStore.Models
{
    public class Language
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public ICollection<Books> Books { get; set; }
    }
}
