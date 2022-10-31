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
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Net
Imports AmazonUploadFile
Imports EC.Library.Security
Imports Newtonsoft.Json
Imports YahooSCM_API

Partial Class mng_product_list_Form_Save
    Inherits System.Web.UI.Page

    Public prglimit As EC.mng.Limit       '讀取程式的權限
    Public YahooIMGurl As String
    Public YahooHTML As String

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

            '雅虎購物欄位 儲存至 YahooshoppingSCM_API_list 一筆ProductNo一筆資料
            Dim ProductNo = RequestString("ProductNo", RequestActMode.None, RequestMode.XSS) & ""
            Dim applicant = RequestString("applicant", RequestActMode.None, RequestMode.XSS) & ""
            Dim contentRating = RequestString("contentRating", RequestActMode.None, RequestMode.XSS) & ""
            Dim videos = ""
            Dim images = ""
            Dim copy = ""
            Dim displayName = RequestString("displayName", RequestActMode.None, RequestMode.XSS) & ""
            Dim shortDescription_1 = RequestString("shortDescription_1", RequestActMode.None, RequestMode.XSS) & ""
            Dim shortDescription_2 = RequestString("shortDescription_2", RequestActMode.None, RequestMode.XSS) & ""
            Dim shortDescription_3 = RequestString("shortDescription_3", RequestActMode.None, RequestMode.XSS) & ""
            Dim shortDescription_4 = RequestString("shortDescription_4", RequestActMode.None, RequestMode.XSS) & ""
            Dim shortDescription_5 = RequestString("shortDescription_5", RequestActMode.None, RequestMode.XSS) & ""

            Dim subStationId = RequestString("subStationId", RequestActMode.None, RequestMode.XSS) & ""
            Dim catItemId = RequestString("catItemId", RequestActMode.None, RequestMode.XSS) & ""

            '新增欄位 20191218 育誠
            Dim NewProductName = RequestString("NewProductName", RequestActMode.None, RequestMode.XSS) & ""

            '一般欄位儲存
            Dim sqlcount = "SELECT COUNT(*) FROM YahooshoppingSCM_API_list WITH(NOLOCK) WHERE ProductNo='{0}'"
            sqlcount = String.Format(sqlcount, ProductNo)
            If EC.DB.ExecuteScalar(sqlcount) > 0 Then 'UPDATE

                Dim sqlupdate = "UPDATE YahooshoppingSCM_API_list SET ProductNo='{0}',applicant='{1}',contentRating='{2}',videos='{3}',images='{4}',copy='{5}',displayName='{6}',shortDescription_1='{7}',shortDescription_2='{8}',shortDescription_3='{9}',shortDescription_4='{10}',shortDescription_5='{11}',subStationId='{12}',catItemId='{13}',NewProductName='{14}' WHERE ProductNo='{0}'"
                sqlupdate = String.Format(sqlupdate, ProductNo, applicant, contentRating, videos, images, copy, displayName, shortDescription_1, shortDescription_2, shortDescription_3, shortDescription_4, shortDescription_5, subStationId, catItemId, NewProductName)
                EC.DB.ExecuteScalar(sqlupdate)

            Else 'INSERT

                Dim sqlupdate = "INSERT INTO YahooshoppingSCM_API_list (ProductNo,applicant,contentRating,videos,images,copy,displayName,shortDescription_1,shortDescription_2,shortDescription_3,shortDescription_4,shortDescription_5, subStationId, catItemId,NewProductName) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}')"
                sqlupdate = String.Format(sqlupdate, ProductNo, applicant, contentRating, videos, images, copy, displayName, shortDescription_1, shortDescription_2, shortDescription_3, shortDescription_4, shortDescription_5, subStationId, catItemId, NewProductName)
                EC.DB.ExecuteScalar(sqlupdate)

            End If

            '雅虎購物屬性 儲存至 YahooshoppingSCM_API_list_attributes

            '屬性ID

            Dim struDataAttrClusterId = RequestString("struDataAttrClusterId", RequestActMode.None, RequestMode.XSS)

            'select數量    attrname0，attrnoncustom0
            Dim attrnoncustomcount = RequestString("attrnoncustomcount", RequestActMode.None, RequestMode.XSS)

            Dim attrnoncustomlist As New List(Of String)

            For num As Integer = 0 To attrnoncustomcount - 1 Step 1

                With attrnoncustomlist
                    Dim requestname = String.Format("attrname{0}", num)
                    Dim attrname = Request(requestname)
                    Dim requestname1 = String.Format("attrnoncustom{0}", num)
                    Dim attrnoncustom = Request(requestname1)

                    attrnoncustomlist.Add(Trim(attrname))
                    attrnoncustomlist.Add(Trim(attrnoncustom))

                End With

            Next

            'input數量      textattrname0，attrcustom0
            Dim attrcustomcount = RequestString("attrcustomcount", RequestActMode.None, RequestMode.XSS)

            Dim attrcustomlist As New List(Of String)

            For num As Integer = 0 To attrcustomcount - 1 Step 1

                With attrcustomlist
                    Dim requestname = String.Format("textattrname{0}", num)
                    Dim attrname = Request(requestname)
                    Dim requestname1 = String.Format("attrcustom{0}", num)
                    Dim attrnoncustom = Request(requestname1)

                    attrcustomlist.Add(Trim(attrname))
                    attrcustomlist.Add(Trim(attrnoncustom))

                End With

            Next

            If EC.DB.ExecuteScalar(sqlcount) > 0 Then

                'INSERT 'UPDATE 都須直接刪除再儲存 避免大中小類不同而衝突
                Dim sqldelet = "DELETE FROM YahooshoppingSCM_API_list_attributes WHERE ProductNo='{0}'"
                sqldelet = String.Format(sqldelet, ProductNo)
                EC.DB.ExecuteScalar(sqldelet)

                '非自定義屬性
                For num As Integer = 0 To attrnoncustomlist.Count - 1 Step 2

                    Dim Attrname = attrnoncustomlist.Item(num)
                    Attrname = Attrname.Replace(Convert.ToChar(9), "")
                    Attrname = Attrname.Replace(vbCrLf, "")
                    Dim valuenum = num + 1
                    Dim Attrvalues = attrnoncustomlist.Item(valuenum)
                    Attrvalues = Attrvalues.Replace(Convert.ToChar(9), "")
                    Attrvalues = Attrvalues.Replace(vbCrLf, "")
                    Dim sqlupdate = "INSERT INTO YahooshoppingSCM_API_list_attributes (ProductNo,struDataAttrClusterId,Attrname,Attrvalues,contenttype) VALUES('{0}','{1}','{2}','{3}','{4}')"
                    sqlupdate = String.Format(sqlupdate, ProductNo, struDataAttrClusterId, Trim(Attrname), Trim(Attrvalues), "attrnoncustom")
                    EC.DB.ExecuteScalar(sqlupdate)

                Next

                '自定義屬性

                For num As Integer = 0 To attrcustomlist.Count - 1 Step 2

                    Dim Attrname = attrcustomlist.Item(num)
                    Attrname = Attrname.Replace(Convert.ToChar(9), "")
                    Attrname = Attrname.Replace(vbCrLf, "")
                    Dim valuenum = num + 1
                    Dim Attrvalues = attrcustomlist.Item(valuenum)
                    Attrvalues = Attrvalues.Replace(Convert.ToChar(9), "")
                    Attrvalues = Attrvalues.Replace(vbCrLf, "")
                    Dim sqlupdate = "INSERT INTO YahooshoppingSCM_API_list_attributes (ProductNo,struDataAttrClusterId,Attrname,Attrvalues,contenttype) VALUES('{0}','{1}','{2}','{3}','{4}')"
                    sqlupdate = String.Format(sqlupdate, ProductNo, struDataAttrClusterId, Trim(Attrname), Trim(Attrvalues), "attrcustom")
                    EC.DB.ExecuteScalar(sqlupdate)

                Next

            End If

            '初始值為0 需將所有 level歸零後重新 UPDATE level & displayname

            Dim level1 = RequestString("level1", RequestActMode.None, RequestMode.XSS)
            Dim level2 = RequestString("level2", RequestActMode.None, RequestMode.XSS)
            Dim lv1displayname = RequestString("lv1displayname", RequestActMode.None, RequestMode.XSS)
            Dim lv2displayname = RequestString("lv2displayname", RequestActMode.None, RequestMode.XSS)

            Dim sqlcount3 = "SELECT COUNT(*) FROM YahooshoppingSCM_API_list_attributes WITH(NOLOCK) WHERE ProductNo='{0}'"
            sqlcount3 = String.Format(sqlcount3, ProductNo)
            If EC.DB.ExecuteScalar(sqlcount3) > 0 Then 'UPDATE

                Dim sqlupdate = "UPDATE YahooshoppingSCM_API_list_attributes SET [level]='{0}',displayName = NULL WHERE ProductNo='{1}'"
                sqlupdate = String.Format(sqlupdate, 0, ProductNo)
                EC.DB.ExecuteScalar(sqlupdate)

                'level 1
                Dim sqlupdate1 = "UPDATE YahooshoppingSCM_API_list_attributes SET [level]='1',displayName ='{0}' ,type='{3}' WHERE ProductNo='{1}' AND Attrname='{2}'"
                sqlupdate1 = String.Format(sqlupdate1, lv1displayname, ProductNo, level1, "modelitem")
                EC.DB.ExecuteScalar(sqlupdate1)
                'level 2
                Dim sqlupdate2 = "UPDATE YahooshoppingSCM_API_list_attributes SET [level]='2',displayName ='{0}' ,type='{3}' WHERE ProductNo='{1}' AND Attrname='{2}'"
                sqlupdate2 = String.Format(sqlupdate2, lv2displayname, ProductNo, level2, "modelitem")
                EC.DB.ExecuteScalar(sqlupdate2)

            End If

            ''★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★
            ''AWS 圖片/HTML 審核後存入 SQL
            ''★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★

            '圖檔與HTML需求欄位 
            Dim CategoryO_old = RequestString("CategoryO_old", RequestActMode.None, RequestMode.SQLInjection)
            Dim feature = Request("feature")


            '圖檔相對路徑塞入存放站台位置
            Dim imgurl = EC.mng.Info.Eclife_HomeURL & CategoryO_old
            ''取得雅虎購物中心驗證cookie與wssid
            '不靜態存取重複存取雅虎或AWS那會出錯
            'Dim CookieCollection As CookieCollection = YahooShoppingSCM_API.Get_YSSCM_CookieCollection()
            'Dim WSSID As String = YahooShoppingSCM_API.Get_YSSCM_WSSID_token(CookieCollection)

            Dim CookieCollection As CookieCollection = YahooRequestDataStatic.YahooRequestData.Cookies
            Dim WSSID As String = YahooRequestDataStatic.YahooRequestData.WSSID

            '移除VB排版字元
            feature = feature.Replace(vbCrLf, "")

            'AWS核心使用 : 來源App_Code/AmazonUploadFile.vb
            '將圖檔傳至雅虎 AWS S3 並傳回雅虎購物中心能使用的圖檔(包含HTML IMG內SRC)
            Try
                '圖檔轉大小後上傳檔名置換為 檔名+_AWS
                imgurl = imgurldownload_turnpx_updateaws_returnurl(imgurl)
                'AWS核心物件
                Dim AWSUploadFile = New AmazonUploadFile()
                '取得URL方法
                YahooIMGurl = AWSUploadFile.GenAWSUrlByUrl(imgurl, Path.GetFileName(imgurl), WSSID, CookieCollection).url
                Dim img = "LogYahooIMGurl:" + JsonConvert.SerializeObject(YahooIMGurl) + "</br>"
                img += "<img src='{0}' alt='儲存失敗請連絡相關人員'></br>"
                img = String.Format(img, YahooIMGurl)
                Dim detail As String = "<details><summary>AWS IMG與HTML儲存狀況(請點擊左側查看)</summary>{0}{1}</details>"
                If Not String.IsNullOrWhiteSpace(Path.GetExtension(YahooIMGurl)) Then
                    '取得HTML方法
                    YahooHTML = AWSUploadFile.ModifyHtmlImgUrl(feature, WSSID, CookieCollection)
                    Dim html = "LogYahooHTML:{0}</br>"
                    html = String.Format(html, YahooHTML)
                    detail = String.Format(detail, img, html)
                End If
                Response.Write(detail)
                '將取回之圖檔與HTML轉存YahooshoppingSCM_API_list以備上架至雅虎購物中心
                Dim AWSdatainputSQL = "UPDATE YahooshoppingSCM_API_list SET images='{1}',[copy] ='{2}' WHERE ProductNo='{0}'"
                AWSdatainputSQL = String.Format(AWSdatainputSQL, ProductNo, YahooIMGurl, YahooHTML)
                EC.DB.ExecuteScalar(AWSdatainputSQL)

                '單筆提報
                Dim REQUEST1 = RequestString("RequestString", RequestActMode.None, RequestMode.XSS) & ""
                '暫存欄位驗證(即時更正資料)
                If REQUEST1 = "Temporary_proposals" Then
                    Response.Write("<h5 >此暫存方式為:<br/>將資料暫存至EC資料庫，並嘗試一次提交暫存至API驗證資料有無錯誤，</br>與正式提交所需驗證欄位不相同，<font style='color:red;'>(部分欄位測試暫存驗證會比正式提交驗證嚴格)</font>因此欄位需以正式提交驗證為準。</h5>")
                    Response.Write(YahooSCM_API.POST_proposals_API(ProductNo, "tempReportGoods"))
                End If

            Catch ex As Exception
                errStatus = "錯誤"
                errMessage = ex.Message
                Dim StackTrace = ex.StackTrace
                Response.Write("</br>[{status: '" & errStatus & "', message:'" & errMessage & ",</br></br>StackTrace:" + StackTrace + "'}]</br></br>如錯誤為 : 在GDI+ 中發生泛型錯誤。為圖片儲存權限不符合。</br>如錯誤為 : Cannot access child value on Newtonsoft.Json.Linq.JValue。為AWS圖檔存取錯誤。")
            End Try

        End If

    End Sub

    '從URL取得圖檔並將px轉為1000*1000轉存於資料夾並反傳url位址
    Public Shared Function imgurldownload_turnpx_updateaws_returnurl(ByVal imgurl As String) As String
        '參考 : http://www.blueshop.com.tw/board/FUM20050124192253INM/BRD20100129155902S4A.html
        '參考 : https://stackoverflow.com/questions/2144592/resizing-images-in-vb-net/22474102
        Dim WC As System.Net.WebClient = New System.Net.WebClient()
        Dim oldimg As System.Drawing.Image = System.Drawing.Image.FromStream(WC.OpenRead(imgurl))
        Dim newimg = New System.Drawing.Bitmap(oldimg, New System.Drawing.Size(1000, 1000))
        oldimg.Dispose()

        '反傳新路徑 (舊檔名+_forAWS)
        Dim imgfilename() = Split(Path.GetFileName(imgurl), ".")
        Dim newimgfilename = imgfilename(0) + "_forAWS." + imgfilename(1)
        Dim newpath = EC.mng.Info.EcUNCPath_photo()
        'URL整理為path http://www.eclife.com.tw/photo2011/prod_2019/11/G1260008.jpg
        imgurl = imgurl.Replace("http://www.eclife.com.tw", "")
        imgurl = imgurl.Replace(Path.GetFileName(imgurl), "")
        newpath = newpath + imgurl + newimgfilename
        newpath = newpath.Replace("/", "\")
        newimg.Save(newpath)
        '整理為path整理為URL \\10.3.10.151\Web\www.eclife.com.tw\photo2011\prod_2019\11\G1260008_forAWS.jpg
        imgurl = newpath.Replace("\\10.3.10.151\Web\", "")
        imgurl = imgurl.Replace("\", "/")
        imgurl = "http://" + imgurl
        Return imgurl
    End Function

End Class




