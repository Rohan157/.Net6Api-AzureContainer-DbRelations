using System.ComponentModel.DataAnnotations.Schema;

namespace BookProject.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string BookUrl { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string ISBNNUMBER { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
        [NotMapped]
        public IFormFile BookFile  { get; set; }

        public int BookCoverId { get; set; }
        public BookCover BookCover { get; set; }
        public int BookWriterId { get; set; }
        public BookWriter BookWriter { get; set; }
    }
}
