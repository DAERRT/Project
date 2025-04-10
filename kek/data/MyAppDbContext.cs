using kek.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace kek.data
{
    public class MyAppDbContext : IdentityDbContext<User>
    {
        public MyAppDbContext(DbContextOptions options) : base(options) {}
        
        public DbSet<Idea> Ideas { get; set; } = null!;
        public DbSet<Teams> Teams { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            string adminName = "admin";
            string studentRoleName = "Student";
            string roleAdminId = "aeae5fd9-59f1-4f32-a2b4-226d4fed7b57";
            string roleStudentId = "14bbdb9d-df1d-4771-899f-e811a571043a";
            string userAdminId = "261514b4-de9d-4d6f-97c2-9c93b0a9a529";
            string Email = "olegoviz.2006@gmail.com";


            //добавляем роль администратора сайта
            builder.Entity<IdentityRole>().HasData(new IdentityRole()
            {
                Id = roleAdminId,
                Name = adminName,
                NormalizedName = adminName.ToUpper()
            });

            builder.Entity<Idea>().HasData(new Idea()
            {
                Id = "88BCE748-D1C9-41A6-B1BA-870C71A6B599",
                IdeaName = "testidea",
                Problem = "testproblem",
                Solution = "testsolution",
                ExpectedResult = "testexpectedresult",
                NecessaryResourses = "testnecesseryresoutses",
                Stack = "asd",
                Customer = "olegoviz.2006@gmail.com",
                Ininiator = "lox",
                Status = 2,
                TeamId = "A3741C3F-8465-43E3-B815-7254811973C1"
            });

            //добавляем роль студента
            builder.Entity<IdentityRole>().HasData(new IdentityRole()
            {
                Id = roleStudentId,
                Name = studentRoleName,
                NormalizedName = studentRoleName.ToUpper()
            });

            //добавляем нового IdentityUser в качестве администратора сайта
            builder.Entity<User>().HasData(new User()
            {
                Id = userAdminId,
                UserName = adminName,
                NormalizedUserName = adminName.ToUpper(),
                FirstName = "Егор",
                LastName = "Рубанов",
                Status = 2,
                Email = Email,
                NormalizedEmail = Email,
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<User>().HashPassword(new User(), adminName),
                SecurityStamp = string.Empty,
                PhoneNumberConfirmed = true
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>()
            {
                RoleId = roleAdminId,
                UserId = userAdminId,
            });
        }

    }
}
