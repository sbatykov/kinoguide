using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace KinoGuide.DbModels
{
    public class SiteDbInitializer: DropCreateDatabaseIfModelChanges<SiteDbContext>
    {
        protected override void Seed(SiteDbContext context)
        {
            var admin = new User
            {
                Login = "Admin",
                Name = "Admin",
                Password = "12345"
            };
            var user = new User
            {
                Login = "User",
                Name = "User",
                Password = "qwerty"
            };
            context.Users.Add(admin);
            context.Users.Add(user);
            context.SaveChanges();

            var film = new Film
            {
                Name = "Оно",
                Year = 2017,
                Director = "Андрес Мускетти",
                Description = "Когда в городке Дерри, штат Мэн, начинают пропадать дети, несколько ребят сталкиваются со своими величайшими страхами и вынуждены помериться силами со злобным клоуном Пеннивайзом, чьи проявления жестокости и список жертв уходят в глубь веков.",
                User = admin
            };
            var file = File.Open(HostingEnvironment.MapPath(@"~/Test/ONO.jpg"), FileMode.Open);
            using (var reader = new BinaryReader(file))
            {
                film.Poster = reader.ReadBytes((int)file.Length);
            }
            context.Films.Add(film);


            film = new Film
            {
                Name = "Напарник",
                Year = 2017,
                Director = "Александр Андрющенко",
                Description = "Неудачная спецоперация заканчивается для майора Хромова переселением в тело маленького ребенка. И всему виной проклятие гадалки! Но даже пересев из полицейской машины в детскую коляску, он умудряется доставить немало хлопот преступному синдикату на Дальнем Востоке. При этом мама ребенка не подозревает, что ее сын способен вести жесткие допросы, а папа вынужден стать невольным «напарником» Хромова. Ведь для того, чтобы вернуть все на свои места, им предстоит закончить спецоперацию, а значит, отец с младенцем начинают охоту на самого опасного главаря местной мафии.",
                User = user
            };
            file = File.Open(HostingEnvironment.MapPath(@"~/Test/PARA.jpg"), FileMode.Open);
            using (var reader = new BinaryReader(file))
            {
                film.Poster = reader.ReadBytes((int)file.Length);
            }
            context.Films.Add(film);

            film = new Film
            {
                Name = "мама!",
                Year = 2017,
                Director = "Даррен Аронофски",
                Description = "Отношения молодой пары оказываются под угрозой, когда, нарушая безмятежное существование супругов, в их дом заявляются незваные гости.",
                User = admin
            };
            file = File.Open(HostingEnvironment.MapPath(@"~/Test/mama.jpg"), FileMode.Open);
            using (var reader = new BinaryReader(file))
            {
                film.Poster = reader.ReadBytes((int)file.Length);
            }
            context.Films.Add(film);

            context.SaveChanges();
        }
    }
}