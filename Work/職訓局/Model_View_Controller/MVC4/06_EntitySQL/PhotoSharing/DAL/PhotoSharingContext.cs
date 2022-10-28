using PhotoSharing.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PhotoSharing.DAL
{
    public class PhotoSharingContext : DbContext
    {
        //加入Photos及Comments的DbSet<Photo>泛型及 DbSet<Comment>泛型,
        //讓EntityFramework可以建立對映資料庫的Photos及Comments資料表
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public PhotoSharingContext():base("ConnectionString")
        {}

    }
}