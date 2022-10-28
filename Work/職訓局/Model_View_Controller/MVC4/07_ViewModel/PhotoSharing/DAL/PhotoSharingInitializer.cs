using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PhotoSharing.Models;
using System.IO;

namespace PhotoSharing.DAL
{
    //1.8.1加PhotoSharingInitializer繼承 DropCreateDatabaseAlways<PhotoSharingContext>泛型
    public class PhotoSharingInitializer:DropCreateDatabaseAlways<PhotoSharingContext>
    {
        //1.8.2override覆寫Seed方法,建構Photo Model及Comment Model初始值
        protected override void Seed(PhotoSharingContext context)
        {
            base.Seed(context);

            List<Photo> photos = new List<Photo>
            {
                new Photo
                {
                   Title="Me standing on top of a mountain",
                    Description="I was very impressed with myself",
                    PhotoFile=getFileBytes("\\Images\\img1.jpg"),
                    ImageMimeType="image/jpeg",
                    CreatedDate=DateTime.Today,
                    UserName="Fred"


                },
               new Photo {
                    Title = "My New Adventure Works Bike",
                    Description = "It's the bees knees!",
                    UserName = "Fred",
                    PhotoFile = getFileBytes("\\Images\\img2.jpg"),
                    ImageMimeType = "image/jpeg",
                    CreatedDate = DateTime.Today
                },
                new Photo {
                    Title = "View from the start line",
                    Description = "I took this photo just before we started over my handle bars.",
                    UserName = "Sue",
                    PhotoFile = getFileBytes("\\Images\\img3.jpg"),
                    ImageMimeType = "image/jpeg",
                    CreatedDate = DateTime.Today
                },
                new Photo {
                    Title = "Sample Beauty Flower",
                    Description = "This is a Sample flower",
                    UserName = "Lee",
                    PhotoFile = getFileBytes("\\Images\\img4.jpg"),
                    ImageMimeType = "image/jpeg",
                    CreatedDate = DateTime.Today
                }

            };
            photos.ForEach(s => context.Photos.Add(s));
            context.SaveChanges();

           
            List<Comment> comments = new List<Comment>
            {
                new Comment
                {
                    UserName = "Bert",
                    Subject = "A Big Mountain",
                    Body = "That looks like a very high mountain you have climbed",
                    PhotoID=1
                },
                new Comment {
                    PhotoID = 1,
                    UserName = "Sue",
                    Subject = "So?What",
                    Body = "I climbed a mountain that high before breakfast everyday"
                },
                new Comment {
                    PhotoID = 2,
                    UserName = "Fred",
                    Subject = "Jealous",
                    Body = "Wow, that new bike looks great!"
                },
                  new Comment {
                    PhotoID = 3,
                    UserName = "Li",
                    Subject = "WOW",
                    Body = "Wow, goodshot!"
                },
                new Comment {
                    PhotoID = 4,
                    UserName = "Lin",
                    Subject = "AWESOME",
                    Body = "Wow, AWESOME Beauty Flower"
                }
            };

            comments.ForEach(s => context.Comments.Add(s));
            context.SaveChanges();
        }

        private byte[] getFileBytes(string path)
        {
            FileStream fileOnDisk = new FileStream(HttpRuntime.AppDomainAppPath+ path,FileMode.Open);

            byte[] fileBytes;
            using (BinaryReader br=new BinaryReader(fileOnDisk))
            {
                fileBytes = br.ReadBytes((int)fileOnDisk.Length);
            }

            return fileBytes;
        }


    }
}