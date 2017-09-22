using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KinoGuide.DbModels
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Film> Films { get; set; }
    }
}