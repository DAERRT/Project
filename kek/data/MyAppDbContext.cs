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
            string adminName = "Администратор";
            string studentRoleName = "Студент";
            string roleAdminId = "aeae5fd9-59f1-4f32-a2b4-226d4fed7b57";
            string roleStudentId = "14bbdb9d-df1d-4771-899f-e811a571043a";
            string userAdminId = "261514b4-de9d-4d6f-97c2-9c93b0a9a529";
            string studygroup = "ПКТб-24-1";
            string Email = "olegoviz.2006@gmail.com";



            builder.Entity<IdentityRole>().HasData(new IdentityRole()
            {
                Id = roleAdminId,
                Name = adminName,
                NormalizedName = adminName.ToUpper()
            });


            builder.Entity<IdentityRole>().HasData(new IdentityRole()
            {
                Id = roleStudentId,
                Name = studentRoleName,
                NormalizedName = studentRoleName.ToUpper()
            });

            builder.Entity<IdentityRole>().HasData(new IdentityRole()
            {
                Id = "9061B6D9-C4B0-4CDD-9D32-8D7E7BC73ADA",
                Name = "Заказчик",
                NormalizedName = "Заказчик".ToUpper()
            });


            builder.Entity<User>().HasData(new User()
            {
                Id = userAdminId,
                UserName = "admin",
                NormalizedUserName = "admin".ToUpper(),
                FirstName = "Егор",
                LastName = "Рубанов",
                Status = 2,
                StudyGroup = studygroup,
                Email = Email,
                DateCreated = DateTime.UtcNow.Date,
                NormalizedEmail = Email,
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<User>().HashPassword(new User(), "admin"),
                SecurityStamp = string.Empty,
                PhoneNumber = "89091856237",
                PhoneNumberConfirmed = true
            });

            builder.Entity<User>().HasData(new User()
            {
                Id = "4D929078-D64B-483B-9F91-B5E943981BB2",
                UserName = "customer@gmail.com",
                NormalizedUserName = "customer@gmail.com".ToUpper(),
                FirstName = "заказчик",
                LastName = "заказчик",
                Status = 2,
                StudyGroup = "заказчик",
                Email = "customer@gmail.com",
                DateCreated = DateTime.UtcNow.Date,
                NormalizedEmail = "customer@gmail.com".ToUpper(),
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<User>().HashPassword(new User(), "123123"),
                SecurityStamp = string.Empty,
                PhoneNumber = "89999999999",
                PhoneNumberConfirmed = true
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>()
            {
                RoleId = roleAdminId,
                UserId = userAdminId,
            });
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>()
            {
                RoleId = "9061B6D9-C4B0-4CDD-9D32-8D7E7BC73ADA",
                UserId = "4D929078-D64B-483B-9F91-B5E943981BB2",
            });
        }

    }
}
