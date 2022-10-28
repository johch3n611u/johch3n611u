namespace Member.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MemberDB : DbContext
    {
        public MemberDB()
            : base("name=MemberDB")
        {
        }

        public virtual DbSet<tblActiveItem> tblActiveItem { get; set; }
        public virtual DbSet<tblSignup> tblSignup { get; set; }
        public virtual DbSet<tblSignupItem> tblSignupItem { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<tblActiveItem>()
                .Property(e => e.cItemName)
                .IsUnicode(false);

            modelBuilder.Entity<tblSignup>()
                .Property(e => e.cMobile)
                .IsUnicode(false);

            modelBuilder.Entity<tblSignupItem>()
                .Property(e => e.cMobile)
                .IsUnicode(false);
        }
    }
}
