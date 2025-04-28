using kek.data;
using kek.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace kek
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

            //���������� � ������������ ���� appsettings.json
            IConfigurationBuilder configBuild = new ConfigurationBuilder()
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            //���������� �������� ��
            builder.Services.AddDbContext<MyAppDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
                //���������� ���
                .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning)));

            //����������� Identity �������
            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<MyAppDbContext>();

            //����������� Auth Cookie!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            builder.Services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "MyWebApplicationAuth";
                options.Cookie.HttpOnly = true;
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/LogOut";
                options.AccessDeniedPath = "/account/accessdenied";
                options.SlidingExpiration = true;
            });

            //���������� ���������� ������������
            builder.Services.AddControllersWithViews();

            //�������� ������������
            WebApplication app = builder.Build();

            //! ������� ���������� middleware ����� �����, ��� ����� ����������� �������� ����

            //���������� ������������� ��������� ������(js,css,�����)
            app.UseStaticFiles();

            //���������� ������� �������������
            app.UseRouting();

            //���������� �������������� � �����������
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseAuthorization();


            //������������ ������ ��� ��������
            app.MapControllerRoute("default", "{controller=Account}/{action=Login}/{id?}");

            await app.RunAsync();
        }
    }
}
