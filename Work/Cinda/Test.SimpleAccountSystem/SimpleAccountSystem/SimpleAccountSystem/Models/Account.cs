namespace SimpleAccountSystem.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Account : DbContext
    {
        public Account()
            : base("name=Account")
        {
        }

        public virtual DbSet<tblGroup> tblGroup { get; set; }
        public virtual DbSet<tblUser> tblUser { get; set; }
        public virtual DbSet<tblUserGroup> tblUserGroup { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblUser>()
                .Property(e => e.cAccount)
                .IsUnicode(false);

            modelBuilder.Entity<tblUserGroup>()
                .Property(e => e.cAccount)
                .IsUnicode(false);
        }
    }
}
