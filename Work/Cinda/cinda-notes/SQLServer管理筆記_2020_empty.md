# SQL Server 管理筆記

## SQL Server 申請帳號

![alt](/sinda-notes/img/dbstep1.png)

![alt](/sinda-notes/img/dbstep2.png)

![alt](/sinda-notes/img/dbstep3.png)

![alt](/sinda-notes/img/dbstep4.png)

<https://dotblogs.com.tw/chis_itnote/2017/10/31/sql>

<https://www.itread01.com/content/1547495120.html>

## SQL connectionStrings

```XML
<connectionStrings>
    <!--
    <add name="MemberDB" connectionString="data source=LAPTOP-0R5VTCD9\SQLEXPRESS;initial catalog=Member;User ID=Test;Password=123;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework" providerName="System.Data.SqlClient" />
    -->
    <add name="MemberDB" connectionString="Data Source=LAPTOP-0R5VTCD9\SQLEXPRESS;Initial Catalog=Member;User ID=Test;Password=123" providerName="System.Data.SqlClient"/>
  </connectionStrings>
```

## CShape DbContext

``` C#

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

```

## DB first 會產生的程式碼 每次開都會用此去重新 check db 所以要記得註解

```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using TommyTest.Models;

namespace TommyTest.DAL
{
    public class UserInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<UserContent>
    {
        protected override void Seed(UserContent context)
        {
            var Groups = new List<tblActiveItem>
            {
            new tblActiveItem{cItemName="排球",cActiveDt="AM 10:00~AM 11:00"},
            new tblActiveItem{cItemName="羽球",cActiveDt="AM 11:00~PM 12:00"},
            new tblActiveItem{cItemName="自行車",cActiveDt="PM 15:00~PM 16:00"},
            };
            Groups.ForEach(s => context.Groups.Add(s));
            context.SaveChanges();
        }
    }
}
```
