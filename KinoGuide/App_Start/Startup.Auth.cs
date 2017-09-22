using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using KinoGuide.DbModels;
using Microsoft.Owin.Security.Cookies;
using Autofac;
using System.Web.Helpers;
using System.Security.Claims;

namespace KinoGuide
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            // Включение использования файла cookie, в котором приложение может хранить информацию для пользователя, выполнившего вход,
            // и использование файла cookie для временного хранения информации о входах пользователя с помощью стороннего поставщика входа
            // Настройка файла cookie для входа
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookie",
                LoginPath = new PathString("/User/Login")
            });

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
        }
    }

}