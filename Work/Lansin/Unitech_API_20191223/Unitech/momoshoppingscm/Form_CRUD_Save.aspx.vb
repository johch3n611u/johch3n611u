'/////////////////////////////////////////////////////////////////////////////////
' 儲存選單資料 (新增/修改/刪除)
'
' 建檔人員: 育誠
' 建檔日期: 2019-11-27
' 修改記錄: 範例--> 日期 記錄人員 簡述
' 關連程式:
' 呼叫來源: 
'/////////////////////////////////////////////////////////////////////////////////
Imports System.Data
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Net
Imports AmazonUploadFile
Imports EC.Library.Security
Imports Newtonsoft.Json
Imports YahooShoppingSCM_API

Partial Class mng_product_list_Form_Save
    Inherits System.Web.UI.Page

    Public prglimit As EC.mng.Limit       '讀取程式的權限

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Response.CacheControl = "no-cache"             '避免被 Cache 住
        EC.mng.Login.LoginCheck()                      '未登入則導到登入頁

        Dim action As String = RequestString("action", RequestActMode.None, RequestMode.XSS)    '空值 or del

        Dim errStatus As String = "ok"
        Dim errMessage As String = "儲存完成"

        prglimit = EC.mng.Limit.CheckLimit(ViewState)    '檢查使用者權限
        If prglimit.errStatus = "error" Then   '權限不符
            errStatus = prglimit.errStatus
            errMessage = prglimit.errMessage
        Else  '儲存

            'momo欄位
            Dim ProductNo = RequestString("ProductNo", RequestActMode.None, RequestMode.XSS) & ""
            Dim isPrompt = RequestString("isPrompt", RequestActMode.None, RequestMode.XSS) & ""
            Dim isGift = RequestString("isGift", RequestActMode.None, RequestMode.XSS) & ""
            Dim webBrandNo = RequestString("webBrandNo", RequestActMode.None, RequestMode.XSS) & ""
            Dim goodsType = RequestString("goodsType", RequestActMode.None, RequestMode.XSS) & ""
            Dim temperatureType = RequestString("temperatureType", RequestActMode.None, RequestMode.XSS) & ""
            Dim width = RequestString("width", RequestActMode.None, RequestMode.XSS) & ""
            Dim length = RequestString("length", RequestActMode.None, RequestMode.XSS) & ""
            Dim height = RequestString("height", RequestActMode.None, RequestMode.XSS) & ""
            Dim weight = RequestString("weight", RequestActMode.None, RequestMode.XSS) & ""
            Dim isECWarehouse = RequestString("isECWarehouse", RequestActMode.None, RequestMode.XSS) & ""
            Dim isPointReachDate = RequestString("isPointReachDate", RequestActMode.None, RequestMode.XSS) & ""
            Dim mainEcCategoryCode = RequestString("mainEcCategoryCode", RequestActMode.None, RequestMode.XSS) & ""
            Dim salesMethods = RequestString("salesMethods", RequestActMode.None, RequestMode.XSS) & ""

            Dim main_achievement = ""
            Dim agreed_delivery_yn = ""
            Dim tax_yn = ""
            Dim disc_mach_yn = ""
            Dim gov_subsidize_yn = ""

            Dim hasAs = RequestString("hasAs", RequestActMode.None, RequestMode.XSS) & ""
            Dim asDays = RequestString("asDays", RequestActMode.None, RequestMode.XSS) & ""
            Dim asNote = RequestString("asNote", RequestActMode.None, RequestMode.XSS) & ""
            Dim ecFirstQty = RequestString("ecFirstQty", RequestActMode.None, RequestMode.XSS) & ""
            Dim ecMinQty = RequestString("ecMinQty", RequestActMode.None, RequestMode.XSS) & ""
            Dim ecLeadTime = RequestString("ecLeadTime", RequestActMode.None, RequestMode.XSS) & ""
            Dim isCommission = RequestString("isCommission", RequestActMode.None, RequestMode.XSS) & ""
            Dim saleUnit = RequestString("saleUnit", RequestActMode.None, RequestMode.XSS) & ""
            Dim expDays = RequestString("expDays", RequestActMode.None, RequestMode.XSS) & ""
            Dim isAcceptTravelCard = RequestString("isAcceptTravelCard", RequestActMode.None, RequestMode.XSS) & ""
            Dim isIncludeInstall = RequestString("isIncludeInstall", RequestActMode.None, RequestMode.XSS) & ""

            '新增欄位 20191218 育誠
            Dim NewProductName = RequestString("NewProductName", RequestActMode.None, RequestMode.XSS) & ""

            Try

                Dim sql As String = <sql>
                              DELETE FROM MomoshoppingSCM_API_list
                              WHERE ProductNo ='{0}'
                          </sql>
                sql = String.Format(sql, ProductNo)
                EC.DB.ExecuteScalar(sql)

                sql = <sql>
                            INSERT INTO MomoshoppingSCM_API_list 
                            ( ProductNo,
                              isPrompt,
                              isGift,                              
                              mainEcCategoryCode,                              
                              webBrandNo,                              
                              goodsType,                              
                              temperatureType,                              
                              width,                              
                              length,                              
                              height,                              
                              weight,                              
                              isECWarehouse,                              
                              isPointReachDate,                              
                              main_achievement,                              
                              agreed_delivery_yn,                              
                              tax_yn,                              
                              disc_mach_yn,                              
                              gov_subsidize_yn,
                              salesMethods,
                hasAs,
                asDays,
                asNote,
                ecFirstQty,
                ecMinQty,
                ecLeadTime,
                isCommission,
                saleUnit,
                expDays,
                isAcceptTravelCard,
                isIncludeInstall,
                NewProductName
                            )
                            VALUES 
                            ('{0}',
                             '{1}',
                             '{2}',
                             '{3}',
                             '{4}',
                             '{5}',
                             '{6}',
                             '{7}',
                             '{8}',
                             '{9}',
                             '{10}',
                             '{11}',
                             '{12}',
                             '{13}',
                             '{14}',
                             '{15}',
                             '{16}',
                             '{17}',
                             '{18}',
                             '{19}',
                '{20}',
                '{21}',
                '{22}',
                '{23}',
                '{24}',
                '{25}',
                '{26}',
                '{27}',
                '{28}',
                '{29}',
                '{30}'
                             )
                      </sql>
                sql = String.Format(sql,
                              ProductNo,
                              isPrompt,
                              isGift,
                              mainEcCategoryCode,
                              webBrandNo,
                              goodsType,
                              temperatureType,
                              width,
                              length,
                              height,
                              weight,
                              isECWarehouse,
                              isPointReachDate,
                              main_achievement,
                              agreed_delivery_yn,
                              tax_yn,
                              disc_mach_yn,
                              gov_subsidize_yn,
                              salesMethods,
                              hasAs,
                              asDays,
                              asNote,
                              ecFirstQty,
                              ecMinQty,
                              ecLeadTime,
                              isCommission,
                              saleUnit,
                              expDays,
                              isAcceptTravelCard,
                              isIncludeInstall,
                              NewProductName)

                EC.DB.ExecuteScalar(sql)

                '連動選項儲存
                Dim attrdivcount = RequestString("attrdivcount", RequestActMode.None, RequestMode.XSS) & "" '連動選項數目

                '動態存取 indexNo & selected 
                '部分連動選項可複選但此處沒實作，複選選項可至資料表表MomoshoppingSCM_API_AttrIndex欄位CHECK_YN查詢

                Dim DELETEsql As String = <sql>DELETE FROM MomoshoppingSCM_API_list_attributes WHERE ProductNo = '{0}'</sql>
                DELETEsql = String.Format(DELETEsql, ProductNo)
                EC.DB.ExecuteScalar(DELETEsql)

                For num As Integer = 0 To attrdivcount - 1 Step 1

                    Dim DynamicIndexNo = "indexNo_{0}"
                    DynamicIndexNo = String.Format(DynamicIndexNo, num)
                    Dim indexNo = RequestString(DynamicIndexNo, RequestActMode.None, RequestMode.XSS) & ""

                    Dim DynamicChosenItemNos = "select_{0}"
                    DynamicChosenItemNos = String.Format(DynamicChosenItemNos, num)
                    Dim chosenItemNos = RequestString(DynamicChosenItemNos, RequestActMode.None, RequestMode.XSS) & ""


                    '類似雅虎階層屬性但值為自訂
                    Dim colSeq1 = RequestString("colSeq1", RequestActMode.None, RequestMode.XSS) & ""
                    Dim colDetail1 = RequestString("colDetail1", RequestActMode.None, RequestMode.XSS) & ""
                    Dim colSeq2 = RequestString("colSeq2", RequestActMode.None, RequestMode.XSS) & ""
                    Dim colDetail2 = RequestString("colDetail2", RequestActMode.None, RequestMode.XSS) & ""

                    For colnum As Integer = 0 To 1 Step 1

                        Select Case colSeq1

                            Case "001"
                                colSeq1 = "001,尺寸"
                            Case "002"
                                colSeq1 = "002,容量"
                            Case "003"
                                colSeq1 = "003,重量"
                            Case "004"
                                colSeq1 = "004,顏色"
                            Case "005"
                                colSeq1 = "005,口味"
                            Case "006"
                                colSeq1 = "006,規格"

                        End Select

                        Select Case colSeq2

                            Case "001"
                                colSeq2 = "001,尺寸"
                            Case "002"
                                colSeq2 = "002,容量"
                            Case "003"
                                colSeq2 = "003,重量"
                            Case "004"
                                colSeq2 = "004,顏色"
                            Case "005"
                                colSeq2 = "005,口味"
                            Case "006"
                                colSeq2 = "006,規格"

                        End Select

                    Next

                    Dim INSERTsql As String = <sql>INSERT INTO MomoshoppingSCM_API_list_attributes (ProductNo, indexNo, chosenItemNos,colSeq1,colDetail1,colSeq2,colDetail2)
                                                   VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}')</sql>
                    INSERTsql = String.Format(INSERTsql, ProductNo, indexNo, chosenItemNos, colSeq1, colDetail1, colSeq2, colDetail2)
                    EC.DB.ExecuteScalar(INSERTsql)

                Next


                ''20191219 育誠 一律改為使用目錄內的 10001_B1.jpg ， 再由後台人員去MomoSCM更改商品圖片

                '★★★★★★★★★★ 圖片檔案擷取更改檔名後轉存檔案與新URL ★★★★★★★★★★
                'Dim httpRequest = HttpContext.Current.Request
                'Dim imgfile As HttpPostedFile


                ''If httpRequest.Files.Count > 0 Then

                ''For Each fileName As String In httpRequest.Files.Keys
                ''    imgfile = httpRequest.Files(fileName)

                ''Response.Write(Server.MapPath("."))
                'Dim imgstream As FileStream = File.Open(Server.MapPath(".") + "\10001_B1.jpg", FileMode.OpenOrCreate)

                ''取圖檔、改規格、改檔名
                'Dim oldimg As System.Drawing.Image = System.Drawing.Image.FromStream(imgstream)
                'imgstream.Dispose()
                'Dim newimg = New System.Drawing.Bitmap(oldimg, New System.Drawing.Size(1000, 1000))
                ''Dim graphic As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(newimg)

                ''graphic.InterpolationMode = Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
                ''graphic.SmoothingMode = Drawing.Drawing2D.SmoothingMode.HighQuality
                ''graphic.PixelOffsetMode = Drawing.Drawing2D.PixelOffsetMode.HighQuality
                ''graphic.CompositingQuality = Drawing.Drawing2D.CompositingQuality.HighQuality

                ''graphic.DrawImageUnscaledAndClipped(newimg, New Rectangle(0, 0, 1000, 1000))
                ''newimg.SetResolution(1000, 1000)

                'oldimg.Dispose()
                ''圖檔改規格後存為串流 詳細請參照MomoSCM上的文件
                'Dim newimgStream = New MemoryStream()
                'Dim newimgfilename = ProductNo + "_B1.jpg"
                'Dim newpath = "\\10.3.10.151\Web\www.eclife.com.tw\photo2011\prod_2019\MomoSCM\" + newimgfilename
                ''URL整理為path \\10.3.10.151\Web\www.eclife.com.tw\photo2011\prod_2019\MomoSCM\ProductNo_B1.jpg
                'newimg.Save(newpath)
                ''新圖片網址存入
                ''測試 http://img.eclife.com.tw/photo2011/prod_2019/MomoSCM/F4030629_B1.bmp
                ''部分網域有鎖不是所有地方都可以外聯故需要存死位子
                'Dim NewImg_Url = "http://www.eclife.com.tw/photo2011/prod_2019/MomoSCM/" + newimgfilename
                'Dim newImgsql2 As String = <SQL>UPDATE MomoshoppingSCM_API_list
                '                                SET NewImg_Url = '{0}'
                '                                WHERE ProductNo = '{1}'
                '                           </SQL>
                'newImgsql2 = String.Format(newImgsql2, NewImg_Url, ProductNo)
                'EC.DB.ExecuteScalar(newImgsql2)

                'Next

                'Else
                '    Dim newimgfilename = ProductNo + "_B1.jpg"
                '    Dim NewImg_Url = "http://www.eclife.com.tw/photo2011/prod_2019/MomoSCM/" + newimgfilename
                '    Dim newImgsql2 As String = <SQL>UPDATE MomoshoppingSCM_API_list
                '                                            SET NewImg_Url = '{0}'
                '                                            WHERE ProductNo = '{1}'
                '                                       </SQL>
                '    newImgsql2 = String.Format(newImgsql2, NewImg_Url, ProductNo)
                '    EC.DB.ExecuteScalar(newImgsql2)
                'End If

                '單筆提報
                Dim REQUEST = RequestString("RequestString", RequestActMode.None, RequestMode.XSS) & ""
                '暫存欄位驗證(即時更正資料)
                If REQUEST = "Temporary_proposals" Then
                    Response.Write("<h5 >此暫存方式為:<br/>將資料暫存至EC資料庫，並嘗試一次提交暫存至API驗證資料有無錯誤，</br>與正式提交所需驗證欄位不相同，<font style='color:red;'>(部分欄位測試暫存驗證會比正式提交驗證嚴格)</font>因此欄位需以正式提交驗證為準。</h5>")
                    Response.Write(MomoSCM_API.POST_proposals_API(ProductNo, "tempReportGoods"))
                End If

            Catch ex As Exception
                errStatus = "錯誤"
                errMessage = ex.Message
                Dim StackTrace = ex.StackTrace
                Response.Write("</br>[{status: '" & errStatus & "', message:'" & errMessage & ",</br></br>StackTrace:" + StackTrace + "'}]</br></br>如錯誤為 : 在GDI+ 中發生泛型錯誤。為圖片儲存權限不符合。</br>如錯誤為 : Cannot access child value on Newtonsoft.Json.Linq.JValue。為AWS圖檔存取錯誤。")
            End Try

        End If






    End Sub


End Class




