using EvoSys.Models;
using Microsoft.EntityFrameworkCore;

namespace EvoSys.Data
{
    public class MainContext : DbContext
    {
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Password=123456;Persist Security Info=True;User ID=as;Initial Catalog=EvoSys;Data Source=PC-OF-DREAMS;TrustServerCertificate=Yes");
        }
    }
}