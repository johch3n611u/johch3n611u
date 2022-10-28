using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace PhotoSharing.Models
{
    //1.1加入Photo Model==> Properties & Annotations & Validation
    public class Photo
    {
        //我是primary key
        public int PhotoID { get; set; }

        //Annotations & Validation
        [Required]
        [StringLength(100)]
        [DisplayName("主題")]
        public string Title { get; set; }

        [Required]
        [MaxLength]
        [DisplayName("上傳照片")]
        public byte[] PhotoFile { get; set; }

        [HiddenInput(DisplayValue =false)]
        public string ImageMimeType { get; set; }

        [Required]
        [DataType(DataType.MultilineText),StringLength(400)]
        [DisplayName("照片描述")]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy/MM/dd}",ApplyFormatInEditMode =true)]
        [DisplayName("建立日期")]
        public DateTime CreatedDate { get; set; }

        [Required]
        //1.3 在UserName屬性採用自訂驗證擴充 CheckUsername
        [DisplayName("發表人名稱")]
        [CheckUsername]
        public string UserName { get; set; }

        //1.2 自訂驗證擴充 CheckUsername Model,繼承ValidationAttribute
        public class CheckUsername:ValidationAttribute
        {
            //1.2.2 在Constructor給ErrorMessage初始值
            public CheckUsername()
            {
                ErrorMessage = "發表人名稱至少2個字";
            }
            //1.2.1 override  IsValid 加入自訂規則
            public override bool IsValid(object value)
            {
                return (value.ToString().Length >= 2) ? true : false;
            }

        }


    }
}

//1.1加入Photo Model==> Properties & Annotations & Validation
//1.2 自訂驗證擴充 CheckUsername Model
//1.2.1 override  IsValid 加入自訂規則
//1.2.2 在Constructor給ErrorMessage初始值
//1.3 在UserName屬性採用自訂驗證擴充 CheckUsername

//1.4.加入 Comment Model==> Properties & Annotations & Validation
//1.5.加入Photo 與 Comment 一對多的 relationship

//*1.6.透過NuGet Package Manager加入 EntityFramework
// ->ManageNuGet Packages...=>EntityFramework

//1.7.建立PhotoSharingContext繼承DbContext(DAL/PhotoSharingContext.cs)

//1.8.加PhotoSharingInitializer繼承 DropCreateDatabaseAlways<PhotoSharingContext>泛型(PhotoSharingInitializer.cs)
//並override覆寫Seed方法,建構Photo Model及Comment Model初始值

//1.9.在Global.asax的Application_Start方法中,建立PhotoSharingInitializer

//1.11.加入Controller及View(需Build Project)
//Add Controller->PhotosController-->MVC 5 Controller with views, using Entity Framwork
//Model class->Photo(PhotoSharingApplication.Models)
//Data context class->PhotoSharingContext(PhotoSharingApplication.Models)

//1.12.測試