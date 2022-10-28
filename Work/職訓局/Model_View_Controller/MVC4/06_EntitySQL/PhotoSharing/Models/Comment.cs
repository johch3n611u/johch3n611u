using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PhotoSharing.Models
{
    public class Comment
    {
        [Key]
        public int CommentID { get; set; }

        [Required]
        [DisplayName("回覆標題")]
        public string Subject { get; set; }

        [Required]
        [DisplayName("回覆內容")]
        [DataType(DataType.MultilineText), StringLength(200)]
        public string Body { get; set; }

        [Required]
        public string UserName { get; set; }

        //建立關聯
        //1.5.1加入Photo 與 Comment 的 relationship
        //繼承virtual的任何類別均可將其覆寫：
        public virtual Photo Photo { get; set; }
        //1.5.2 Foreign Key 
        public int PhotoID { get; set; }

    }
}