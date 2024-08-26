using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class DataContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.UseSqlServer("Server=.;Database=DersProgramiVT;User Id=sa;Password=1;TrustServerCertificate=True;Integrated Security=true;");

        }
        //public DataContext(DbContextOptions<DataContext> options)
        //    : base(options)
        //{

        //}

        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        public DbSet<Students> Students { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Academics> Academics { get; set; }
        public DbSet<Courses> Courses { get; set; }
        public DbSet<ClassCourse> ClassCourses { get; set; }
        public DbSet<ScheduleSetting> ScheduleSettings { get; set; }
        public DbSet<Timetable> Timetables { get; set; }
        public DbSet<Permission> Permission { get; set; }
        public DbSet<PermissionRole> PermissionRol { get; set; }
    }
}
