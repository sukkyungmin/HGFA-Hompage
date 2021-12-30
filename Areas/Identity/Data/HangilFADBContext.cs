using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using HangilFA.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HangilFA.Data
{
    public class HangilFADBContext : IdentityDbContext<HangilFAUser>
    {
        public HangilFADBContext(DbContextOptions<HangilFADBContext> options)
            : base(options)
        {
        }

        public DbSet<RoleMenuPermission> RoleMenuPermission { get; set; }

        public DbSet<NavigationMenu> NavigationMenu { get; set; }

        public DbSet<SupporNotices> SupporNotices { get; set; }

        public DbSet<SupporQuestions> SupporQuestions { get; set; }

        public DbSet<SupporQuestionsAnswer> SupporQuestionsAnswer { get; set; }

        public DbSet<FileMaster> FileMaster { get; set; }

        public DbSet<FileGroupList> FileGroupList { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<SupporNotices>().ToTable("AspNetSupporNotices");
            builder.Entity<SupporQuestions>().ToTable("AspNetSupporQuestions");
            builder.Entity<SupporQuestionsAnswer>().ToTable("AspNetSupporQuestionsAnswer");
            builder.Entity<FileMaster>().ToTable("AspNetFileMaster");
            builder.Entity<FileGroupList>().ToTable("AspNetFileGroupList");

            //builder.Entity<SupporNotices>(entity =>
            //{
            //    entity.ToTable(name: "AspNetSupporNotices");
            //    entity.Property(e => e.Id).HasColumnName("");
            //});

            builder.Entity<RoleMenuPermission>()
            .HasKey(c => new { c.RoleId, c.NavigationMenuId });

            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
