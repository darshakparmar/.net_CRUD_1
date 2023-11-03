using Microsoft.EntityFrameworkCore;
using Task_2.Models;

namespace Task_2.DataLayer
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) 
        { 
            
        }

        public DbSet<OFC_EmployeeModel> Employees { get; set; }
        public DbSet<OFC_DepartmentModel> Departments { get; set; }
    }
}
