using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KinoGuide.DbModels
{
    public class Film
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "ntext")]
        public string Description { get; set; }
        public int Year { get; set; }
        public string Director { get; set; }
        public byte[] Poster { get; set; }

        public virtual User User { get; set; }
    }
}