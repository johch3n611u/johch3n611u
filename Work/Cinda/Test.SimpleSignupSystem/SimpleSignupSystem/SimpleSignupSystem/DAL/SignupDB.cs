namespace SimpleSignupSystem.DAL
{
    using SimpleSignupSystem.Models.Entity;
    using System.Data.Entity;

    public class SignupDB : DbContext
    {
        // 您的內容已設定為使用應用程式組態檔 (App.config 或 Web.config)
        // 中的 'SignupDB' 連接字串。根據預設，這個連接字串的目標是
        // 您的 LocalDb 執行個體上的 'SimpleSignupSystem.DAL.SignupDB' 資料庫。
        // 
        // 如果您的目標是其他資料庫和 (或) 提供者，請修改
        // 應用程式組態檔中的 'SignupDB' 連接字串。
        public SignupDB()
            : base("name=SignupDB")
        {
        }

        // 針對您要包含在模型中的每種實體類型新增 DbSet。如需有關設定和使用
        // Code First 模型的詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=390109。

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<tblActiveItem> tblActiveItem { get; set; }
        public virtual DbSet<tblSignup> tblSignup { get; set; }
        public virtual DbSet<tblSignupItem> tblSignupItem { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<tblActiveItem>()
            //   .Property(e => e.cItemName)
            //   .IsUnicode(false);

            //modelBuilder.Entity<tblSignup>()
            //    .Property(e => e.cMobile)
            //    .IsUnicode(false);

            //modelBuilder.Entity<tblSignupItem>()
            //    .Property(e => e.cMobile)
            //    .IsUnicode(false);

            // <https://dotblogs.com.tw/wasichris/2014/08/23/146339>
            // modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}