using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DPERFUME_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        public HomePage homepage;

        /// <summary>
        /// 查詢頁面詳細資料
        /// </summary>
        /// <param name="PageName">頁面名稱</param>
        /// <param name="PartName">元件名稱</param>
        /// <param name="id">識別值</param>
        /// <returns>頁面資料</returns>
        [HttpGet]
        public ResultModel Get(string PageName, string PartName, string id)
        {
            ResultModel Response = new ResultModel();
            if (PageName != null)
            {
                if (PartName != null)
                {
                    if (id != null)
                    {
                        switch (PartName)
                        {
                            case "ShopBag":

                                if (homepage.Layout.Header.Navbar.ShopBag.Any(x => x.Email == id))
                                {
                                    Response.IsSuccess = true;
                                    Response.Message = "Welcome to used DPERFUME_API";
                                    Response.Data = homepage.Layout.Header.Navbar.ShopBag.Where(x => x.Email == id).ToList();
                                }
                                else
                                {
                                    Response.IsSuccess = false;
                                    Response.Message = "Can't find Data";
                                    Response.Data = new object();
                                }

                                break;
                            case "ColumnProducts":

                                if (homepage.ColumnProducts.Any(x => x.Id == Int32.Parse(id)))
                                {
                                    Response.IsSuccess = true;
                                    Response.Message = "Welcome to used DPERFUME_API";
                                    Response.Data = homepage.ColumnProducts.Where(x => x.Id == Int32.Parse(id)).ToList();
                                }
                                else
                                {
                                    Response.IsSuccess = false;
                                    Response.Message = "Can't find Data";
                                    Response.Data = new object();
                                }

                                break;

                            case "Steps":

                                if (homepage.Steps.Any(x => x.Id == Int32.Parse(id)))
                                {
                                    Response.IsSuccess = true;
                                    Response.Message = "Welcome to used DPERFUME_API";
                                    Response.Data = homepage.Steps.Where(x => x.Id == Int32.Parse(id)).ToList();
                                }
                                else
                                {
                                    Response.IsSuccess = false;
                                    Response.Message = "Can't find Data";
                                    Response.Data = new object();
                                }

                                break;
                            case "RowProducts":
                                if (homepage.RowProducts.Any(x => x.Id == Int32.Parse(id)))
                                {
                                    Response.IsSuccess = true;
                                    Response.Message = "Welcome to used DPERFUME_API";
                                    Response.Data = homepage.RowProducts.Where(x => x.Id == Int32.Parse(id)).ToList();
                                }
                                else
                                {
                                    Response.IsSuccess = false;
                                    Response.Message = "Can't find Data";
                                    Response.Data = new object();
                                }

                                break;
                            default:
                                Response.IsSuccess = false;
                                Response.Message = "Can't find Data";
                                Response.Data = new object();
                                break;
                        }

                    }
                    else
                    {

                        switch (PartName)
                        {
                            case "ShopBag":
                                Response.IsSuccess = true;
                                Response.Message = "Welcome to used DPERFUME_API";
                                Response.Data = homepage.Layout.Header.Navbar.ShopBag;
                                break;
                            case "Layout":
                                Response.IsSuccess = true;
                                Response.Message = "Welcome to used DPERFUME_API";
                                Response.Data = homepage.Layout;
                                break;
                            case "Banner":
                                Response.IsSuccess = true;
                                Response.Message = "Welcome to used DPERFUME_API";
                                Response.Data = homepage.Banner;
                                break;
                            case "ColumnProducts":
                                Response.IsSuccess = true;
                                Response.Message = "Welcome to used DPERFUME_API";
                                Response.Data = homepage.ColumnProducts;
                                break;
                            case "Steps":
                                Response.IsSuccess = true;
                                Response.Message = "Welcome to used DPERFUME_API";
                                Response.Data = homepage.Steps;
                                break;
                            case "RowProducts":
                                Response.IsSuccess = true;
                                Response.Message = "Welcome to used DPERFUME_API";
                                Response.Data = homepage.RowProducts;
                                break;
                            case "FinalProduct":
                                Response.IsSuccess = true;
                                Response.Message = "Welcome to used DPERFUME_API";
                                Response.Data = homepage.FinalProduct;
                                break;
                            default:
                                Response.IsSuccess = false;
                                Response.Message = "Can't find Data";
                                Response.Data = new object();
                                break;
                        }
                    }
                }
                else
                {

                    switch (PageName)
                    {
                        case "HomePage":
                            Response.IsSuccess = true;
                            Response.Message = "Welcome to used DPERFUME_API";
                            Response.Data = homepage;
                            break;
                        default:
                            Response.IsSuccess = false;
                            Response.Message = "Can't find Data";
                            Response.Data = new object();
                            break;
                    }
                }
            }
            else
            {
                Response.IsSuccess = false;
                Response.Message = "Can't find Data";
                Response.Data = new object();
            }

            return Response;
        }

        /// <summary>
        /// 帳號密碼登入 測試帳號密碼為 Carl@gmail.com/1234
        /// </summary>
        /// <param name="Authentication">帳號密碼</param>
        /// <returns>登入成功或失敗</returns>
        [HttpPost]
        public ResultModel Post([FromForm] string Email, [FromForm] string Password)
        {
            // https://blog.johnwu.cc/article/ironman-day09-asp-net-core-model-binding.html

            // [formbody] https://stackoverflow.com/questions/19446544/post-request-to-include-content-type-and-json
            // 這裡的狀況：主要不是前端問題，而是涉及到後端取封包的邏輯，每個框架不太一樣需要依據接口格式，所以 form 就必須符合格式，否則根本找不到相對應的接口。
            // https://stackoverflow.com/questions/19446544/post-request-to-include-content-type-and-json
            // 變成必須用非正規方式把資料組成 JSON 格式傳入。
            // 此處較不像一般的方式，通常在 form 傳出前攔截是為了前端驗證資料格式 e.g. email / length / 非法字元 ...，而不是轉 JSON 格式
            // https://www.w3school.com.cn/jquery/event_submit.asp

            // $('#myform').submit() // 等於直述句會直接觸法此 form submit

            // [FromBody] Authentication Authentication
            ResultModel Response = new ResultModel();

            if (Email != null || Email != "")
            {

                if (
                    Email == "Carl@gmail.com"
                    &&
                    Password == "1234"
                    )
                {
                    Response.IsSuccess = true;
                    Response.Message = "Congratulations on successful Verification";
                    Response.Data = new object();
                }
                else
                {
                    Response.IsSuccess = false;
                    Response.Message = "Please make sure enter the right Authentication";
                    Response.Data = new object();
                }
            }
            else
            {
                Response.IsSuccess = false;
                Response.Message = "Please make sure enter the right Authentication";
                Response.Data = new object();
            }
            return Response;
        }

        [HttpGet("{All}")]
        public HomePage Put(string All)
        {
            // ...
            return homepage;
        }

        //[HttpPut("{id}")]
        //public HomePage Put(string Email, [FromBody] Authentication Authentication)
        //{
        //    // ...
        //    return homepage;
        //}

        //[HttpDelete("{id}")]
        //public ResultModel Delete(int id)
        //{
        //    ResultModel Response = new ResultModel();
        //    // ...
        //    return Response;
        //}


        public DefaultController()
        {
            homepage = new HomePage();
            homepage.Layout = new Layout();
            homepage.Layout.Header = new Header();

            homepage.Layout.Header.Title = "D'PERFUME";

            homepage.Layout.Header.Navbar = new Navbar();
            homepage.Layout.Header.Navbar.NavNames = new List<string>();

            homepage.Layout.Header.Navbar.NavNames.Add("Product");
            homepage.Layout.Header.Navbar.NavNames.Add("log in");
            homepage.Layout.Header.Navbar.NavNames.Add("Wishlist");
            homepage.Layout.Header.Navbar.NavNames.Add("Cart");

            homepage.Layout.Header.Navbar.ShopBag = new List<ShopBag>();

            ShopBag ShopBag_1 = new ShopBag();
            ShopBag_1.Email = "Carl@gmail.com";
            ShopBag_1.ProductNames = "Rose";
            homepage.Layout.Header.Navbar.ShopBag.Add(ShopBag_1);

            ShopBag ShopBag_2 = new ShopBag();
            ShopBag_2.Email = "Carl@gmail.com";
            ShopBag_2.ProductNames = "Apple";
            homepage.Layout.Header.Navbar.ShopBag.Add(ShopBag_1);

            ShopBag ShopBag_3 = new ShopBag();
            ShopBag_3.Email = "Carl@gmail.com";
            ShopBag_3.ProductNames = "Ocean";
            homepage.Layout.Header.Navbar.ShopBag.Add(ShopBag_1);

            homepage.Layout.Footer = new Footer();
            homepage.Layout.Footer.Navbar = new Navbar();
            homepage.Layout.Footer.Navbar.NavNames = new List<string>();

            homepage.Layout.Footer.Title = "D'PERFUME";
            homepage.Layout.Footer.Copyright = "© " + DateTime.Now.ToString("yyyy") + ". All Rights Reserved.";

            homepage.Layout.Footer.Navbar.NavNames.Add("Product");
            homepage.Layout.Footer.Navbar.NavNames.Add("Blog");
            homepage.Layout.Footer.Navbar.NavNames.Add("Contact");

            homepage.Banner = new Banner();
            homepage.Banner.Title = "Perfume your day";
            homepage.Banner.Info = "A perfume is like a piece of clothing, a message, a way of presenting oneself a costume that according to the person who wears it.";
            homepage.ColumnProducts = new List<ColumnProduct>();

            ColumnProduct ColumnProduct_1 = new ColumnProduct();
            ColumnProduct_1.Id = 1;
            ColumnProduct_1.Title = "Gabrielle CHANEL";
            ColumnProduct_1.Info = "全新嗅覺體驗，花香四溢、明亮馥郁。結合茉莉、依蘭依蘭、橙花和最迷人的格拉斯晚香玉之花香，呈現出夢幻花香氣息。 極具女性特質的香奈兒嘉柏麗香水，專為光芒四射的她而生。";
            ColumnProduct_1.Link = "03.html";
            homepage.ColumnProducts.Add(ColumnProduct_1);

            ColumnProduct ColumnProduct_2 = new ColumnProduct();
            ColumnProduct_2.Id = 2;
            ColumnProduct_2.Title = "Five O’clock Au Gingembre";
            ColumnProduct_2.Info = "生薑與溫醇的肉桂香、紅糖的香甜摻和成一氣，溫暖微辛的氣息反倒形成一股輕煙氤氳開來，還帶點微微煙醺感漫溢在整個鼻息之間，細聞之下,又還有溫溫淡淡的木香，溫和而暖心。";
            ColumnProduct_2.Link = "03.html";
            homepage.ColumnProducts.Add(ColumnProduct_2);

            ColumnProduct ColumnProduct_3 = new ColumnProduct();
            ColumnProduct_3.Id = 3;
            ColumnProduct_3.Title = "CHANEL N°5";
            ColumnProduct_3.Info = "N°5，女性魅力的極致精髓。散發柔美澄透的乙醛花束香氣。經典傳奇香氛，蘊藏於極簡設計的獨特瓶身。";
            ColumnProduct_3.Link = "03.html";
            homepage.ColumnProducts.Add(ColumnProduct_3);


            homepage.Steps = new List<Step>();

            Step Step_1 = new Step();
            Step_1.Id = 1;
            Step_1.Title = "Levels of Preventive Maintenance Compliance";
            Step_1.Info = "The core idea of preventative maintenance is the performance of proactive actions against unscheduled downtime in order to identify potential defects before they evolve into something more severe.";
            homepage.Steps.Add(Step_1);

            Step Step_2 = new Step();
            Step_2.Id = 2;
            Step_2.Title = "Levels of Preventive Maintenance Compliance";
            Step_2.Info = "The core idea of preventative maintenance is the performance of proactive actions against unscheduled downtime in order to identify potential defects before they evolve into something more severe.";
            homepage.Steps.Add(Step_2);

            Step Step_3 = new Step();
            Step_3.Id = 3;
            Step_3.Title = "Critical percentage of planned maintenance";
            Step_3.Info = "Calculating the critical percentage of planned maintenance will help you prioritize the most important operations that should be performed immediately.";
            homepage.Steps.Add(Step_3);

            homepage.RowProducts = new List<RowProduct>();

            RowProduct RowProduct_1 = new RowProduct();
            RowProduct_1.Id = 1;
            RowProduct_1.Title = "Poppy & Barley";
            RowProduct_1.Info = "Jo Malone";
            RowProduct_1.Price = "NT$1,580";
            RowProduct_1.Discount = "NT$1,380";
            homepage.RowProducts.Add(RowProduct_1);

            RowProduct RowProduct_2 = new RowProduct();
            RowProduct_2.Id = 2;
            RowProduct_2.Title = "Body Wash";
            RowProduct_2.Info = "Curology";
            RowProduct_2.Price = "NT$1,580";
            homepage.RowProducts.Add(RowProduct_2);

            RowProduct RowProduct_3 = new RowProduct();
            RowProduct_3.Id = 3;
            RowProduct_3.Title = "Apple Cider Vinegar Capsule";
            RowProduct_3.Info = "Tonik";
            RowProduct_3.Price = "NT$1,580";
            homepage.RowProducts.Add(RowProduct_3);

            homepage.FinalProduct = new FinalProduct();
            homepage.FinalProduct.Title = "Bottle-Recycling";
            homepage.FinalProduct.SubTitle = "Recycle your used empty bottles and be rewarded!";
            homepage.FinalProduct.Info = "For the past 10 years, we have collected over 800,000 bottles through Empty Bottle Recycling Campaign and planted more than 860 pine trees for a better environment.For the past 10 years, we have collected over 800,000 bottles through Empty Bottle Recycling Campaign and planted more than 860 pine trees for a better environment.";
            homepage.FinalProduct.Link = "03.html";
        }

        #region DTO

        /// <summary>
        /// 驗證用途
        /// </summary>
        public class Authentication
        {
            /// <summary>
            /// 帳號 Email Key
            /// </summary>
            public string Email { get; set; }
            /// <summary>
            /// 密碼
            /// </summary>
            public string Password { get; set; }
        }

        /// <summary>
        /// 回傳用途
        /// </summary>
        public class ResultModel
        {
            /// <summary>
            /// 確認此次 Request 是否成功
            /// </summary>
            public bool IsSuccess { get; set; }
            /// <summary>
            /// Request 相關訊息
            /// </summary>
            public string Message { get; set; }
            /// <summary>
            /// Request 相關資料
            /// </summary>
            public object Data { get; set; }

        }

        /// <summary>
        /// 共用元件
        /// </summary>
        public class Layout
        {
            public Header Header { get; set; }

            public Footer Footer { get; set; }

        }

        /// <summary>
        /// 標頭元件
        /// </summary>
        public class Header
        {
            /// <summary>
            /// 標頭標題
            /// </summary>
            public string Title { get; set; }
            public Navbar Navbar { get; set; }
        }
        /// <summary>
        /// 導航欄元件
        /// </summary>
        public class Navbar
        {
            /// <summary>
            /// 導航名稱
            /// </summary>
            public List<string> NavNames { get; set; }

            public List<ShopBag> ShopBag { get; set; }
        }
        /// <summary>
        /// 購物車元件
        /// </summary>
        public class ShopBag
        {
            /// <summary>
            /// 帳號 Email Key
            /// </summary>
            public string Email { get; set; }
            /// <summary>
            /// 商品名稱
            /// </summary>
            public string ProductNames { get; set; }
        }
        /// <summary>
        /// 頁腳元件
        /// </summary>
        public class Footer
        {
            /// <summary>
            /// 版權聲明
            /// </summary>
            public string Copyright { get; set; }
            /// <summary>
            /// 頁腳標題
            /// </summary>
            public string Title { get; set; }
            public Navbar Navbar { get; set; }
        }
        /// <summary>
        /// 橫幅旗幟元件
        /// </summary>
        public class Banner
        {
            /// <summary>
            /// 橫幅旗幟標題
            /// </summary>
            public string Title { get; set; }
            /// <summary>
            /// 橫幅旗幟資訊
            /// </summary>
            public string Info { get; set; }
        }
        /// <summary>
        /// 列排商品元件
        /// </summary>
        public class ColumnProduct
        {
            /// <summary>
            /// 商品 Id Key
            /// </summary>
            public int Id { get; set; }
            /// <summary>
            /// 商品標題
            /// </summary>
            public string Title { get; set; }
            /// <summary>
            /// 商品資訊
            /// </summary>
            public string Info { get; set; }
            /// <summary>
            /// 商品連結
            /// </summary>
            public string Link { get; set; }
        }
        /// <summary>
        /// 步驟元件
        /// </summary>
        public class Step
        {
            /// <summary>
            /// 步驟 Id Key
            /// </summary>
            public int Id { get; set; }
            /// <summary>
            /// 步驟標題
            /// </summary>
            public string Title { get; set; }
            /// <summary>
            /// 步驟資訊
            /// </summary>
            public string Info { get; set; }

        }
        /// <summary>
        /// 行排商品元件
        /// </summary>
        public class RowProduct
        {
            /// <summary>
            /// 行排商品 Id Key
            /// </summary>
            public int Id { get; set; }
            /// <summary>
            /// 行排商品標題
            /// </summary>
            public string Title { get; set; }
            /// <summary>
            /// 行排商品資訊
            /// </summary>
            public string Info { get; set; }
            /// <summary>
            /// 行排商品價格
            /// </summary>
            public string Price { get; set; }
            /// <summary>
            /// 行排商品優惠價格
            /// </summary>
            public string Discount { get; set; }

        }

        /// <summary>
        /// 頁腳商品元件
        /// </summary>
        public class FinalProduct
        {
            /// <summary>
            /// 頁腳商品標題
            /// </summary>
            public string Title { get; set; }
            /// <summary>
            /// 頁腳商品副標題
            /// </summary>
            public string SubTitle { get; set; }
            /// <summary>
            /// 頁腳商品資訊
            /// </summary>
            public string Info { get; set; }
            /// <summary>
            /// 頁腳商品連結
            /// </summary>
            public string Link { get; set; }

        }

        /// <summary>
        /// HomePage 元件名稱
        /// </summary>
        public class HomePage
        {
            public Layout Layout { get; set; }
            public Banner Banner { get; set; }
            public List<ColumnProduct> ColumnProducts { get; set; }
            public List<Step> Steps { get; set; }
            public List<RowProduct> RowProducts { get; set; }
            public FinalProduct FinalProduct { get; set; }
        }

        #endregion DTO
    }
}
