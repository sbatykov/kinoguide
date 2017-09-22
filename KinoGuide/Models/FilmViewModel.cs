using System.ComponentModel.DataAnnotations;
using System.Web;

namespace KinoGuide.Models
{
    public class FilmViewModel
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        [Required]
        [Display(Name = "Название фильма")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Описание фильма")]
        public string Description { get; set; }
        [Required, Range(1890, 3000)]
        [Display(Name = "Год", ShortName = "Год")]
        public int Year { get; set; }
        [Required]
        [Display(Name = "Режисер")]
        public string Director { get; set; }
        [Display(Name = "Постер")]
        public HttpPostedFileBase FilePoster { get; set; }
        [Display(Name = "Автор публикации")]
        public string Author { get; set; }
    }
    
    [MetadataType(typeof(NewFilmMetadata))]
    public class NewFilmViewModel: FilmViewModel
    {
    }

    public class NewFilmMetadata
    {
        [Required]
        public HttpPostedFileBase FilePoster { get; set; }
    }
}