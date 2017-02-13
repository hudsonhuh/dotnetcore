using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Core.Entity;
using Microsoft.EntityFrameworkCore.Metadata;
using Core.Entity.Decanter;

namespace Core.Data
{
    public partial class DecanterContext : DbContext
    {
        public DecanterContext(DbContextOptions options) : base(options)
        {
            
        }

        public virtual DbSet<Audit> Audit { get; set; }
        public virtual DbSet<Banner> Banner { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Language> Language { get; set; }
        public virtual DbSet<Member> Member { get; set; }
        public virtual DbSet<MemberRole> MemberRole { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<Service> Service { get; set; }
        
        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Member>().ToTable("Member");
            modelBuilder.Entity<Member>().HasKey(k => k.MemberNo);

            modelBuilder.Entity<Member>()
                .HasOne(a => a.Department)
                .WithMany(s => s.Member)
                .HasForeignKey(a => a.DepartmentNo)
                .IsRequired();

            modelBuilder.Entity<Member>()
                .HasMany(a => a.MemberRole)
                .WithOne(s => s.Member);

            modelBuilder.Entity<Member>()
                .HasMany(a => a.Audit)
                .WithOne(s => s.Member);

            modelBuilder.Entity<MemberRole>().ToTable("MemberRole");
            modelBuilder.Entity<MemberRole>().HasKey(k => k.RoleNo);
            
            modelBuilder.Entity<MemberRole>()
                .HasOne(a => a.Member)
                .WithMany(s => s.MemberRole)
                .HasForeignKey(a => a.MemberNo)
                .IsRequired();
            
            modelBuilder.Entity<MemberRole>()
                .HasOne(a => a.Menu)
                .WithMany(s => s.MemberRole)
                .HasForeignKey(a => a.MenuNo)
                .IsRequired();

            modelBuilder.Entity<Menu>().ToTable("Menu");
            modelBuilder.Entity<Menu>().HasKey(k => k.MenuNo);

            modelBuilder.Entity<Menu>()
                .HasOne(a => a.Menu2)
                .WithMany(s => s.Menu1)
                .HasForeignKey(a => a.ParentMenuNo);

            modelBuilder.Entity<Menu>()
                .HasOne(a => a.Service)
                .WithMany(s => s.Menu)
                .HasForeignKey(a => a.ServiceNo);

            modelBuilder.Entity<Menu>()
                .HasMany(a => a.MemberRole)
                .WithOne(s => s.Menu);

            modelBuilder.Entity<Menu>()
                .HasMany(a => a.Audit)
                .WithOne(s => s.Menu);

            modelBuilder.Entity<Service>().ToTable("Service");
            modelBuilder.Entity<Service>().HasKey(k => k.ServiceNo);

            modelBuilder.Entity<Service>()
                .HasMany(a => a.Language)
                .WithOne(s => s.Service)
                .IsRequired();

            modelBuilder.Entity<Service>()
                .HasMany(a => a.Banner)
                .WithOne(s => s.Service)
                .IsRequired();

            modelBuilder.Entity<Service>()
                .HasMany(a => a.Menu)
                .WithOne(s => s.Service)
                .IsRequired();
            
            modelBuilder.Entity<Language>().ToTable("Language");
            modelBuilder.Entity<Language>().HasKey(k => k.LanguageNo);

            modelBuilder.Entity<Language>()
                .HasOne(a => a.Service)
                .WithMany(s => s.Language)
                .HasForeignKey(a => a.ServiceNo)
                .IsRequired();

            modelBuilder.Entity<Audit>().ToTable("Audit");
            modelBuilder.Entity<Audit>().HasKey(k => k.AuditNo);

            modelBuilder.Entity<Audit>()
                .HasOne(a => a.Member)
                .WithMany(s => s.Audit)
                .HasForeignKey(a => a.MemberNo);

            modelBuilder.Entity<Audit>()
                .HasOne(a => a.Menu)
                .WithMany(s => s.Audit)
                .HasForeignKey(a => a.MenuNo);
            
            modelBuilder.Entity<Banner>().ToTable("Banner");
            modelBuilder.Entity<Banner>().HasKey(k => k.BannerNo);

            modelBuilder.Entity<Department>().ToTable("Department");
            modelBuilder.Entity<Department>().HasKey(k => k.DepartmentNo);

            modelBuilder.Entity<Department>()
                .HasMany(a => a.Member)
                .WithOne(s => s.Department)
                .IsRequired();

            modelBuilder.Entity<Department>()
               .HasOne(a => a.Department2)
               .WithMany(s => s.Department1)
               .HasForeignKey(a => a.ParentDepartmentNo);

        }
    }
}
