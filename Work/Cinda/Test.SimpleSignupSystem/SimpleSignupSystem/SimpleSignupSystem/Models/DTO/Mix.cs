using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleSignupSystem.Models.DTO
{
    public class Mix
    {
        // tblSignup報名資訊
        [StringLength(10)]
        [DisplayName("手機")]
        [Required]
        public string cMobile { get; set; }
        [StringLength(20)]
        [DisplayName("姓名")]
        [Required]
        public string cName { get; set; }
        [StringLength(50)]
        [DisplayName("Email")]
        [Required]
        public string cEmail { get; set; }
        [Column(TypeName = "date")]
        [DisplayName("帳號申請時間")]
        public DateTime cCreateDT { get; set; }

        //tblSignupItem 報名項目
        //cMobile varchar(10), 手機，,PK
        //cItemID,int, 項目ID，PK

        // tblActiveItem 活動項目
        [DisplayName("項目ID")]
        public int cItemID { get; set; }
        [Column(TypeName = "text")]
        [DisplayName("活動項目名稱")]
        public string cItemName { get; set; }
        [Column(TypeName = "text")]
        [DisplayName("活動時間")]
        public string cActiveDt { get; set; }

        // 額外欄位
        [DisplayName("報名人數")]
        public int JoinCount { get; set; }

        // Delete tblSignupItem ID
        [DisplayName("報名人數")]
        public int tblSignupItem_ID { get; set; }

    }
}