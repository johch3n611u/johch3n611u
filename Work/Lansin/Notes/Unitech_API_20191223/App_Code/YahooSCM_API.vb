Imports System.IO
Imports System.Security.Cryptography
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Net
Imports System.Data
Imports System.Runtime.Serialization

Public Class YahooSCM_API

#Region "2019-11-15 精技雅虎購物中心API專案"
    '/////////////////////////////////////////////////////////////////////////////////
    ' 主程式表單
    '
    ' 建檔人員: 育誠
    ' 版   本: version 1
    ' 建檔日期: 2019-11-15
    ' 參考   : 雅虎AP
    ' 修改記錄: 
    ' 關連程式:
    ' 呼叫來源:
    '/////////////////////////////////////////////////////////////////////////////////

#Region "靜態存取驗證欄位"
    '靜態存放雅虎 Cookies & WSSID

    ''' <summary>
    ''' Yahoo驗證欄位存取
    ''' 如要強制更新請直接存取 
    ''' <para>Cookies = YahooShoppingSCM_API.Get_YSSCM_CookieCollection()</para>
    ''' <para>YahooShoppingSCM_API.Get_YSSCM_WSSID_token(Cookies)</para>
    ''' </summary>
    Public Class YahooRequestDataStatic
        ''' <summary>
        ''' 驗證類別
        ''' </summary>
        Public Class RequestData
            Public Property Cookies As CookieCollection
            Public Property WSSID As String
            Public Property Updatetime As DateTime
        End Class
        ''' <summary>
        ''' Yahoo驗證欄位 Cookie 、 WSSID
        ''' </summary>
        Private Shared _YahooRequestData As RequestData = Nothing
        Public Shared Property YahooRequestData As RequestData
            Get
                If _YahooRequestData Is Nothing OrElse (DateDiff("h", Now, _YahooRequestData.Updatetime) > 4) Then
                    _YahooRequestData = New RequestData
                    _YahooRequestData.Cookies = Get_YahooSCM_CookieCollection()
                    _YahooRequestData.WSSID = Get_YahooSCM_WSSID_token(_YahooRequestData.Cookies)
                    _YahooRequestData.Updatetime = Now
                End If

                Return _YahooRequestData
            End Get
            Set
            End Set
        End Property

    End Class

#End Region

#Region "YahooShoppingSCM_API_request (header_加密取得_sp cookie)"
    '依雅虎購物中心api_sign in_規格需求加密
    ''' <summary>
    ''' header_加密取得_sp cookie
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Function Get_YahooSCM_CookieCollection()

        'HTTP_request需求包含4個 header (api-token、api-version、api-timesta、api-signture)與 Credential密文(加密後的cookie)
        '雅虎購物中心API_KEY查詢 : https://scm.monday.com.tw/ApprovalForm/Query/ApiKeyQuery.aspx

        'request實際欄位名稱為 api-timestamp 為Request發送當下的UnixTimestamp，例如1548225833，此Timestamp的有效期限為90秒
        Dim timestamp As String = Int((DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds + 0.5) '1573455721 
        'request實際欄位名稱為 api-token 為API Key查詢頁提供之Token
        Dim token As String = "Supplier_478"
        '供應商編號
        Dim supplierId As Integer = 478
        '為API Key查詢頁提供之KeyValue
        Dim secretKey As String = "yrsW8ukCXsEZOkKmegF7T2WrK0MTq9U2jH3DmSIod24="
        '為API Key查詢頁提供之KeyIV
        Dim iv As String = "MmThDVTcmRpN5jFgzxb+nQ=="
        '為API Key查詢頁提供之SaltKey
        Dim saltKey As String = "doTzTL6Ev0ttm2OF3kxpzloGTtxky1eI"
        'request實際欄位名稱為 api-Version 為API Key查詢頁提供之Version
        Dim Version As String = "1"

        '***** checked *****

        '***** Credential Plaintext 憑證文本 *****

        Dim cookie As JObject = New Newtonsoft.Json.Linq.JObject
        cookie.Add("supplierId", supplierId)
        'Dim plaintext As String = cookie.ToString
        Dim plaintext As String = "{" + Chr(34) + "supplierId" + Chr(34) + ":478}"

        '***** checked *****

        '***** Credential Signature 憑證密文 *****
        '參考文章 : https://stackoverflow.com/questions/5987186/aes-encrypt-string-in-vb-net
        '參考文章 : https://dotblogs.com.tw/yc421206/archive/2012/04/18/71609.aspx
        '參考文章 : https://stackoverflow.com/questions/723129/using-aescryptoserviceprovider-in-vb-net
        '參考文章 : https://stackoverflow.com/questions/3962900/how-do-i-encrypt-a-string-in-vb-net-using-rijndaelmanaged-and-using-pkcs5-paddi
        'AES / CBC / PKCS5Padding 加密方式
        '此步驟使用到 supplierId / secretKey / iv

        Dim aes As AesCryptoServiceProvider = New AesCryptoServiceProvider()

        aes.Key = Convert.FromBase64String(secretKey)
        aes.IV = Convert.FromBase64String(iv)
        aes.Mode = CipherMode.CBC
        aes.Padding = PaddingMode.PKCS7

        'Dim encryptor = aes.CreateEncryptor()
        'Dim encrypted = encryptor.TransformFinalBlock(Encoding.UTF8.GetBytes(plaintext), 0, Encoding.UTF8.GetBytes(plaintext).Length)
        'aes.Clear()
        Dim ms As MemoryStream = New MemoryStream()
        Dim cs As CryptoStream = New CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write)
        cs.Write(Encoding.UTF8.GetBytes(plaintext), 0, Encoding.UTF8.GetBytes(plaintext).Length)
        cs.FlushFinalBlock()
        Dim encrypted = ms.ToArray()

        Dim ciphertext As String = ""
        ciphertext = Convert.ToBase64String(encrypted)

        '***** checked *****

        '***** Credential Signature 憑證簽章 *****
        '參考文章 : https://kknews.cc/zh-tw/code/33bzv9y.html
        'hmacsha512 加密方式
        '此步驟使用到 timestamp / token / saltKey / secretKey

        Dim signatureSource As String = String.Format("{0}{1}{2}{3}", timestamp, token, saltKey, ciphertext)

        Dim utf8 As UTF8Encoding = New UTF8Encoding()
        Dim keyByte As Byte() = utf8.GetBytes(secretKey)
        Dim HMACSHA512 As HMACSHA512 = New HMACSHA512(keyByte)
        Dim messageBytes As Byte() = utf8.GetBytes(signatureSource)
        Dim hashmessage As Byte() = HMACSHA512.ComputeHash(messageBytes)

        Dim builder As New StringBuilder(hashmessage.Length * 2)
        For Each data As Byte In hashmessage
            builder.Append(Convert.ToString(data, 16).PadLeft(2, "0"c).PadRight(2, " "c))
        Next

        Dim metadata As Byte() = Encoding.UTF8.GetBytes(builder.ToString)

        Dim signaturebyte As Byte() = Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding("iso-8859-1"), metadata)
        Dim signature As String = System.Text.Encoding.Default.GetString(signaturebyte)

        '***** 將包裹塞入封包header *****
        '實作 HttpWebRequest
        Dim request As HttpWebRequest = WebRequest.Create("https://tw.supplier.yahoo.com/api/spa/v1/signIn")
        request.CookieContainer = New CookieContainer()
        request.Method = "POST"
        request.Headers.Add("api-token", token)
        request.Headers.Add("api-keyversion", Version)
        request.Headers.Add("api-timestamp", timestamp)
        request.Headers.Add("api-signature", signature)
        request.ContentType = "application/json"
        request.Headers.Add("charset", "utf-8")
        '禁止轉址
        request.AllowAutoRedirect = False

        '內容轉為資料流
        Dim st As Stream = request.GetRequestStream()
        Dim byteArray As Byte() = Encoding.Default.GetBytes(ciphertext)
        st.Write(byteArray, 0, byteArray.Length)

        '執行HttpWebRequest
        Dim response As HttpWebResponse = request.GetResponse()
        '擷取response cookie string
        '參考 http://storynsong01.pixnet.net/blog/post/116435414-%E3%80%90vba%E3%80%91%E5%B8%B8%E7%94%A8vb%E5%AD%97%E4%B8%B2%E8%99%95%E7%90%86%E5%87%BD%E6%95%B8
        Dim _sp As String = response.Cookies.Item("_sp").ToString
        _sp = Microsoft.VisualBasic.Mid(_sp, 5, _sp.Length)
        '釋放response
        response.Close()
        '回傳依header計算之CookieCollection
        Return response.Cookies




    End Function

#End Region

#Region "YahooShoppingSCM_API_request (使用_sp cookie 取 WSSID token)"
    '依雅虎購物中心api_sign in_取得之cookie用另外一個api取得X-YahooWSSID-Authorization
    ''' <summary>
    ''' 使用_sp cookie 取 WSSID token
    ''' </summary>
    ''' <param name="CookieCollection">_sp cookie</param>
    ''' <remarks></remarks>
    Public Shared Function Get_YahooSCM_WSSID_token(CookieCollection As CookieCollection)

        'Dim CookieCollection As CookieCollection = Get_YSSCM_CookieCollection()

        '***** 將_sp塞入封包cookie *****
        '實作 HttpWebRequest
        '參考 https://blog.csdn.net/u011412226/article/details/51049045
        '參考 http://hk.voidcc.com/question/p-abgfsgcw-re.html
        Dim request As HttpWebRequest = WebRequest.Create("https://tw.supplier.yahoo.com/api/spa/v1/token")

        request.CookieContainer = New CookieContainer()
        request.CookieContainer.Add(CookieCollection)

        request.Method = "GET"

        '禁止轉址
        request.AllowAutoRedirect = False

        '執行HttpWebRequest
        Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
        '回傳資料解碼
        '參考 https://stackoverflow.com/questions/3273205/read-text-from-response
        '參考 https://liuder.pixnet.net/blog/post/96806748-%5Bvb.net%5D%E6%8E%A5%E6%94%B6json%E6%A0%BC%E5%BC%8F%E8%B3%87%E6%96%99%EF%BC%8C%E4%B8%A6%E8%BD%89%E7%82%BA%E7%89%A9%E4%BB%B6%E6%88%96%E8%B3%87%E6%96%99%E8%A1%A8
        Dim reader = New System.IO.StreamReader(response.GetResponseStream(), ASCIIEncoding.ASCII)
        Dim responseText As String = reader.ReadToEnd()
        Dim Obj As Newtonsoft.Json.Linq.JObject = Newtonsoft.Json.JsonConvert.DeserializeObject(responseText)
        Dim wssid As String = Obj.Item("wssid").ToString
        '釋放response
        response.Close()
        '回傳依cookie計算之wssid JObject
        Return wssid

    End Function

#End Region

#Region "YahooShoppingSCM_API_request (使用API)"
    ''' <summary>
    ''' 依雅虎購物中心文件規則呼叫api
    ''' (註:無做錯誤處理與反饋)
    ''' </summary>
    ''' <param name="Method">Get/Post</param>
    ''' <param name="URL"></param>
    ''' <param name="JSONString">整理過後的JSONString</param>
    ''' <remarks></remarks>
    Public Shared Function Request_YahooSCM_API(Method As String, URL As String, JSONString As String)

        '取得雅虎購物中心驗證cookie與wssid
        Dim CookieCollection As CookieCollection = Get_YahooSCM_CookieCollection()
        Dim WSSID As String = Get_YahooSCM_WSSID_token(CookieCollection)
        '***** 將cookie與wssid塞入封包 *****
        '實作 HttpWebRequest
        Dim request As HttpWebRequest = WebRequest.Create(URL) '丟入函數的URL參數
        request.CookieContainer = New CookieContainer()
        request.CookieContainer.Add(CookieCollection)
        request.Headers.Add("X-YahooWSSID-Authorization", WSSID)
        request.Method = Method '丟入函數的Method參數
        request.ContentType = "application/json"

        '禁止轉址
        request.AllowAutoRedirect = False
        '內容轉為資料流
        If JSONString <> "" Or JSONString <> Nothing Then
            Dim st As Stream = request.GetRequestStream()
            Dim byteArray As Byte() = Encoding.UTF8.GetBytes(JSONString)
            st.Write(byteArray, 0, byteArray.Length)
        End If
        '執行HttpWebRequest

        Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
        '回傳資料解碼
        Dim reader = New System.IO.StreamReader(response.GetResponseStream(), ASCIIEncoding.UTF8)
        Dim responseText As String = reader.ReadToEnd()
        'Dim Obj As Newtonsoft.Json.Linq.JObject = Newtonsoft.Json.JsonConvert.DeserializeObject(responseText)
        '釋放response
        response.Close()
        '回傳API JObject
        Return responseText

    End Function

#End Region

#Region "Post YahooShoppingSCM_proposals API (上傳上架商品)"
    Public Shared successmsg As String
    Public Shared errormsg As String
    '依雅虎購物中心api_sign in_規格需求加密
    ''' <summary>
    ''' YahooSCM_商品提報
    ''' <para>相關資料表為YahooshoppingSCM_API_list</para>
    ''' <para>單筆請輸入ProductNo</para>
    ''' <para>多筆請以半形逗號分隔Ex(ProductNo,ProductNo,...</para>
    ''' <para>全部ProductNo請傳空字串或Nothing</para>
    ''' <paramref name="doAction"/> tempReportGoods API暫存 , verifyReportGoods API送審
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Function POST_proposals_API(ByVal ProductNo As String, ByVal doAction As String) As String

        'WebAPI反傳錯誤處理
        Dim ProductNames As String = "" '錯誤商品名稱
        Dim failLists As String = "" '錯誤清單
        Dim totalCnt As Integer  '總筆數
        Dim successCnt As Integer  '成功筆數
        Dim failCnt As Integer '失敗筆數
        Dim error_html = ""
        Dim postback = ""

        Try

            '兩種方式:使用第二種方式
            '1查完整拉出一張大表 ( 較複雜且須在後端操控資料在查詢
            '2分別依需求查 ( 較直覺但容易出錯但也容易偵錯

            '資料庫欄位設計將level設為死資料如要更改再進程式更改，較方便資料庫欄位設計
            '資料庫內的level只來判定spec階層

            'list筆數
            Dim listreader As IDataReader
            Dim countdt As DataTable
            If ProductNo = Nothing Or ProductNo = "" Then
                listreader = EC.DB.ExecuteReader("SELECT ProductNo FROM YahooshoppingSCM_API_list with(NOLOCK) WHERE posteddate IS NULL")
                countdt = EC.DB.ExecuteDataTable("SELECT ProductNo FROM YahooshoppingSCM_API_list with(NOLOCK) WHERE posteddate IS NULL")
            Else
                Dim sql As String = <sql>SELECT ProductNo FROM YahooshoppingSCM_API_list with(NOLOCK) WHERE ProductNo ='{0}' AND posteddate IS NULL</sql>
                sql = String.Format(sql, ProductNo)
                listreader = EC.DB.ExecuteReader(sql)
                countdt = EC.DB.ExecuteDataTable(sql)
            End If

            If countdt.Rows.Count > 0 Then

                totalCnt = countdt.Rows.Count

                Do While listreader.Read()

                    If ProductNo = Nothing Or ProductNo = "" Then
                        ProductNo = listreader.GetString(0)
                    End If

                    '查表 YahooshoppingSCM_API_list
                    Dim sql = "SELECT * FROM YahooshoppingSCM_API_list with(NOLOCK) WHERE ProductNo='" + ProductNo + "'"
                    Dim dt = EC.DB.ExecuteDataTable(sql)
                    '查表 list
                    Dim sqlec = "SELECT * FROM list with(NOLOCK) WHERE ProductNo='" + ProductNo + "'"
                    Dim ecdt = EC.DB.ExecuteDataTable(sqlec)
                    '查表 YahooshoppingSCM_API_list_attributes
                    Dim sqlattr = "SELECT * FROM YahooshoppingSCM_API_list_attributes with(NOLOCK) WHERE ProductNo='" + ProductNo + "'"
                    Dim attrdt = EC.DB.ExecuteDataTable(sqlattr)


                    '對於值有疑問可以看左邊類別檔註解
                    Dim obj = New YahooShoppingSCM.JSONobj
                    obj.applicant = dt.Select().FirstOrDefault.Item("applicant").ToString

                    If doAction = "tempReportGoods" Then
                        obj.reviewStatus = "draft" '定為常數: 測試 draft，正式上架審核 composing
                    ElseIf doAction = "verifyReportGoods" Then
                        obj.reviewStatus = "composing" '定為常數: 測試 draft，正式上架審核 composing
                    End If

                    obj.subStationId = dt.Select().FirstOrDefault.Item("subStationId").ToString
                    obj.type = "newListing" '定為常數: newListing 新增一般賣場

                    obj.listing = New YahooShoppingSCM.Listing
                    With obj.listing
                        .catItemId = dt.Select().FirstOrDefault.Item("catItemId").ToString
                        .deliveryType = "normal" '定為常數: normal 正常交貨期


                        If InStr(1, ecdt.Select().FirstOrDefault.Item("SpicalPrice").ToString, ".", 1) = 0 Then
                            .price = ecdt.Select().FirstOrDefault.Item("SpicalPrice").ToString + ".00"
                        Else
                            .price = CStr(Math.Round(CDbl(ecdt.Select().FirstOrDefault.Item("SpicalPrice").ToString), 0)) + ".00"
                        End If

                        .seoUrl = Regex.Replace(dt.Select().FirstOrDefault.Item("NewProductName").ToString, "[\W_]+", "")
                    End With

                    obj.product = New YahooShoppingSCM.Product
                    With obj.product
                        .attributeDisplayMode = "table" '定為常數
                        .catItemId = dt.Select().FirstOrDefault.Item("catItemId").ToString
                        .contentRating = dt.Select().FirstOrDefault.Item("contentRating").ToString

                        If InStr(1, ecdt.Select().FirstOrDefault.Item("Cost").ToString, ".", 1) = 0 Then
                            .cost = ecdt.Select().FirstOrDefault.Item("Cost").ToString + ".00"
                        Else
                            .cost = CStr(Math.Round(CDbl(ecdt.Select().FirstOrDefault.Item("Cost").ToString), 0)) + ".00"
                        End If

                        If InStr(1, ecdt.Select().FirstOrDefault.Item("spicalprice").ToString, ".", 1) = 0 Then
                            .msrp = ecdt.Select().FirstOrDefault.Item("spicalprice").ToString + ".00"
                        Else
                            .msrp = CStr(Math.Round(CDbl(ecdt.Select().FirstOrDefault.Item("spicalprice").ToString), 0)) + ".00"
                        End If


                        .name = dt.Select().FirstOrDefault.Item("NewProductName").ToString
                        .struDataAttrClusterId = attrdt.Select().FirstOrDefault.Item("struDataAttrClusterId").ToString
                        .copy = dt.Select().FirstOrDefault.Item("copy").ToString
                    End With

                    obj.product.attributes() = New List(Of YahooShoppingSCM.Attribute)
                    'attributes迴圈次數與資料查詢
                    Dim attributesreader = EC.DB.ExecuteReader("SELECT * FROM YahooshoppingSCM_API_list_attributes with(NOLOCK) WHERE ProductNo ='" + ProductNo + "' AND type ='attr'")
                    Do While attributesreader.Read()
                        With obj.product.attributes()

                            Dim items As YahooShoppingSCM.Attribute = New YahooShoppingSCM.Attribute
                            items.name = attributesreader.Item(2).ToString
                            items.values = New List(Of String)
                            'values陣列迴圈
                            Dim values() = Split(attributesreader.GetString(3), ",")
                            For number As Integer = 0 To values.Count - 1 Step 1

                                Dim feedback = values(number).ToString()
                                items.values.Add(feedback)

                            Next

                            obj.product.attributes().Add(items)

                        End With
                    Loop

                    With obj.product
                        '很詭異的寫法做完一包後在塞進類別內，原來那一包類別檔呼叫不到子類
                        '不知為何類別不能直接繼承某個物件集合(物件集合內有n個物件)
                        '就算類別屬性 Shared 也呼叫不到 ...
                        '最後是因為發現物件內的物件並非物件集合所以只有一筆資料所以跳過
                        '參考 : https://docs.microsoft.com/zh-tw/office/vba/language/reference/user-interface-help/dictionary-object
                        .shipType = New YahooShoppingSCM.Shiptype
                        .shipType.id = "1"
                    End With

                    obj.product.models() = New List(Of YahooShoppingSCM.Model)
                    '★★★★★ 第一層 ★★★★★ models內有幾個model
                    '利用 YahooshoppingSCM_API_list_attributes 欄位 model 底線後數字判別有幾組
                    '欄位值命名方式為ProductNo+_+modelitemNo從1開始，用於區分modelitem，如果type為attr則此值為null
                    Dim mcdtcount = EC.DB.ExecuteDataTable("SELECT top 1 model FROM YahooshoppingSCM_API_list_attributes with(NOLOCK) WHERE ProductNo ='" + ProductNo + "' AND type ='modelitem' group by model ORDER BY model desc")

                    For modelnum As Integer = 1 To Int(mcdtcount.Select().FirstOrDefault.Item("model").ToString) Step 1

                        '這邊不延續上面串字串方式因為會出現異常的空白
                        Dim mcdtsql = String.Format("SELECT *  FROM YahooshoppingSCM_API_list_attributes with(NOLOCK) WHERE ProductNo ='{0}' AND type ='modelitem' AND model = '{1}'", ProductNo, modelnum)
                        '查表 YahooshoppingSCM_API_list_attributes
                        Dim mcdt = EC.DB.ExecuteDataTable(mcdtsql)

                        With obj.product.models()

                            Dim model As YahooShoppingSCM.Model = New YahooShoppingSCM.Model
                            model.displayName = mcdt.Select("level ='1'").FirstOrDefault.Item("displayName").ToString

                            '★★★★★★★★★★ items start ★★★★★★★★★★

                            '★★★★★ 第二層 ★★★★★
                            '這裡的迴圈跟著modelnum
                            model.items = New List(Of YahooShoppingSCM.Item)

                            '★★★★★ 第三層 ★★★★★ items內有幾個item
                            '查表 YahooshoppingSCM_API_list_attributes 
                            Dim itemcountsql = String.Format("SELECT top 1 item FROM YahooshoppingSCM_API_list_attributes with(NOLOCK) WHERE ProductNo ='{0}' AND type ='modelitem' AND [level]='2' AND model ='{1}' group by item ORDER BY item desc", ProductNo, modelnum)
                            Dim itemcount = EC.DB.ExecuteDataTable(itemcountsql)
                            For itemnum As Integer = 1 To Int(itemcount.Select().FirstOrDefault.Item("item").ToString) Step 1
                                Dim item As YahooShoppingSCM.Item = New YahooShoppingSCM.Item
                                Dim spec2sql = String.Format("ProductNo ='{0}' AND type ='modelitem' AND [level]='2' AND model ='{1}' AND item ='{2}'", ProductNo, modelnum, itemnum)

                                item.displayName = mcdt.Select(spec2sql).FirstOrDefault.Item("displayName").ToString
                                item.stock = "" '測試"reviewStatus": "draft"時必須為空故預設都為空

                                item.spec = New YahooShoppingSCM.Spec1
                                item.spec.name = mcdt.Select(spec2sql).FirstOrDefault.Item("Attrname").ToString

                                'spec裡的values有幾個value  陣列方式存到 value 內
                                item.spec.values = New List(Of String)
                                'values陣列迴圈
                                Dim values() = Split(mcdt.Select(spec2sql).FirstOrDefault.Item("Attrvalues").ToString, ",")

                                For Number As Integer = 0 To values.Count - 1 Step 1

                                    Dim feedback = values(Number).ToString()
                                    item.spec.values.Add(mcdt.Select(spec2sql).FirstOrDefault.Item("Attrvalues").ToString)

                                Next

                                model.items.Add(item)
                            Next

                            '★★★★★★★★★★ items end ★★★★★★★★★★

                            '★★★★★★★★★★ videos start ★★★★★★★★★★
                            '不傳影片
                            'Dim Video As YahooShoppingSCM.Video = New YahooShoppingSCM.Video
                            'Video.order = "1" '定為常數: 排序寫死只傳一部影片或不傳
                            'Video.url = dt.Select().FirstOrDefault.Item("Videos").ToString
                            'model.videos = New List(Of YahooShoppingSCM.Video)
                            'model.videos.Add(Video)
                            '★★★★★★★★★★ videos end ★★★★★★★★★★

                            '★★★★★★★★★★ Images start ★★★★★★★★★★
                            '只傳一份所以不寫迴圈
                            Dim Image As YahooShoppingSCM.Image = New YahooShoppingSCM.Image
                            Image.order = "1" '定為常數: 排序寫死只傳一部影片或不傳
                            Image.url = dt.Select().FirstOrDefault.Item("Images").ToString
                            model.images = New List(Of YahooShoppingSCM.Image)
                            model.images.Add(Image)
                            '必填兩張圖，不確定要傳哪個先傳重複的上去
                            Dim Image2 As YahooShoppingSCM.Image = New YahooShoppingSCM.Image
                            Image2.order = "2" '定為常數: 排序寫死只傳一部影片或不傳
                            Image2.url = dt.Select().FirstOrDefault.Item("Images").ToString
                            model.images.Add(Image2)
                            '★★★★★★★★★★ Images end ★★★★★★★★★★

                            '★★★★★★★★★★ spec start ★★★★★★★★★★
                            Dim Spec As YahooShoppingSCM.Spec = New YahooShoppingSCM.Spec
                            Dim spec1sql = String.Format("ProductNo ='{0}' AND type ='modelitem' AND [level]='1' AND model ='{1}' ", ProductNo, modelnum)

                            Spec.name = mcdt.Select(spec1sql).FirstOrDefault.Item("Attrname").ToString()
                            Spec.values = New List(Of String)

                            Dim spec1values() = Split(mcdt.Select(spec1sql).FirstOrDefault.Item("Attrvalues").ToString, ",")

                            For Number As Integer = 0 To spec1values.Count - 1 Step 1

                                Dim feedback = spec1values(Number).ToString()
                                feedback = Trim(feedback)
                                Spec.values.Add(feedback)

                            Next

                            model.spec = New YahooShoppingSCM.Spec
                            model.spec = Spec
                            '★★★★★★★★★★ spec end ★★★★★★★★★★

                            .Add(model)
                        End With

                    Next




                    'shortDescription 簡短說明
                    Dim sd1 = dt.Select().FirstOrDefault.Item("shortDescription_1").ToString()
                    Dim sd2 = dt.Select().FirstOrDefault.Item("shortDescription_2").ToString()
                    Dim sd3 = dt.Select().FirstOrDefault.Item("shortDescription_3").ToString()
                    Dim sd4 = dt.Select().FirstOrDefault.Item("shortDescription_4").ToString()
                    Dim sd5 = dt.Select().FirstOrDefault.Item("shortDescription_5").ToString()

                    obj.product.shortDescription = New List(Of String)
                    '幾筆值就add幾次這要寫迴圈
                    If sd1 <> Nothing Or sd1 <> "" Then
                        obj.product.shortDescription.Add(sd1)
                        If sd2 <> Nothing Or sd2 <> "" Then
                            obj.product.shortDescription.Add(sd2)
                            If sd3 <> Nothing Or sd3 <> "" Then
                                obj.product.shortDescription.Add(sd3)
                                If sd4 <> Nothing Or sd4 <> "" Then
                                    obj.product.shortDescription.Add(sd4)
                                    If sd5 <> Nothing Or sd5 <> "" Then
                                        obj.product.shortDescription.Add(sd5)
                                    End If
                                End If
                            End If
                        End If
                    End If

                    obj.product.specs = New List(Of YahooShoppingSCM.Spec2)
                    '擷取選擇之level 1 & 2
                    Dim lvsql = "SELECT Attrname,level FROM YahooshoppingSCM_API_list_attributes WITH(NOLOCK) WHERE ProductNo = '{0}' AND Type='modelitem'"
                    lvsql = String.Format(lvsql, ProductNo)
                    Dim lvdt = EC.DB.ExecuteDataTable(lvsql)

                    Dim Spec2 As YahooShoppingSCM.Spec2 = New YahooShoppingSCM.Spec2
                    Spec2.name = lvdt.Select("level='1'").FirstOrDefault.Item("Attrname").ToString()
                    Spec2.level = "1"
                    obj.product.specs.Add(Spec2)

                    Dim Spec3 As YahooShoppingSCM.Spec2 = New YahooShoppingSCM.Spec2
                    Spec3.name = lvdt.Select("level='2'").FirstOrDefault.Item("Attrname").ToString()
                    Spec3.level = "2"
                    obj.product.specs.Add(Spec3)



                    '完成上述結構化迴圈後將obj序列化為api所需json
                    Dim JSONobj As String = Newtonsoft.Json.JsonConvert.SerializeObject(obj)

                    '★★★★★★★★★★★★★★★★★★★★ "呼叫API並上傳資料" ★★★★★★★★★★★★★★★★★★★★

                    '雅虎API URL TEST查詢值在Yahoo Shopping SCM API 文件上
                    Dim v1 As String = "https://tw.supplier.yahoo.com/api/spa/v1/"

                    'POST Yahoo Shopping SCM API
                    '創建提案
                    Dim H As String = v1 + "proposals" 'ok
                    '範例json格式 
                    'Request POST / spa / v1 / proposals body
                    'Response Status HTTP/1.1 201

                    '新增一般賣場(二階層屬性)
                    'Request
                    'https://docs.google.com/document/d/1-ROog-SSxvCIKkWRfcjadtFmP_gnL4PkqbR7TfvR5Cg/edit?pli=1
                    'Response
                    'https://docs.google.com/document/d/1Xw3Rf_LirWAizvjJU9QV44Nye206AEwKTpyzj70iDEY/edit 

                    Try

                        Dim Z As String = Request_YahooSCM_API("POST", H, JSONobj)

                        If doAction = "verifyReportGoods" Then '正式提報
                            '將資料上傳時的時間從新塞入資料表
                            Dim sqlposteddate = "UPDATE YahooshoppingSCM_API_list Set posteddate ='{0}' WHERE ProductNo='{1}'"
                            sqlposteddate = String.Format(sqlposteddate, Now, ProductNo)
                            EC.DB.ExecuteScalar(sqlposteddate)
                        End If
                        successCnt += 1


                        'successmsg += "<font style='color:blue'>ProductNo:" + ProductNo + "上傳成功</font></br>"
                        'Return successmsg
                    Catch ex As WebException '失敗
                        failCnt += 1
                        Dim ErrorResponse = ex.Response
                        Dim StackTrace = ex.StackTrace
                        Dim TargetSite = ex.TargetSite
                        '回傳資料解碼
                        Dim ErrorReader = New System.IO.StreamReader(ErrorResponse.GetResponseStream(), ASCIIEncoding.UTF8)
                        Dim ErrorText As String = ErrorReader.ReadToEnd()
                        ErrorResponse.Dispose()
                        'json轉物件
                        Dim errorObj As Newtonsoft.Json.Linq.JContainer = Newtonsoft.Json.JsonConvert.DeserializeObject(ErrorText)
                        Dim error_Count = errorObj.Item("error").Item("detail").Count

                        For num As Integer = 0 To error_Count - 1 Step 1
                            error_html += "商品編號:" + ProductNo + ":錯誤訊息如下</br>"

                            Dim type = ""
                            If errorObj.Item("error").Item("detail")(num).Item("type") IsNot Nothing Then
                                type = "類型:" + errorObj.Item("error").Item("detail")(num).Item("type").ToString() + "</br>"
                            End If
                            Dim invalidValue = ""
                            invalidValue = "無效值:" + errorObj.Item("error").Item("detail")(num).Item("invalidValue").ToString() + "</br>"
                            Dim message = ""
                            message = "訊息:" + errorObj.Item("error").Item("detail")(num).Item("message").ToString() + "</br>"
                            Dim path = ""
                            If errorObj.Item("error").Item("detail")(num).Item("path") IsNot Nothing Then
                                path = "錯誤欄位:" + errorObj.Item("error").Item("detail")(num).Item("path").ToString() + "</br>"
                            End If
                            error_html += type + invalidValue + message + path
                            error_html += "</br></br></br>"
                        Next

                        '紀錄最後一次的反傳
                        Dim failListsql As String = <sql>UPDATE YahooshoppingSCM_API_list 
                                       SET last_failList = '{0}'
                                       WHERE ProductNo = '{1}'
                                  </sql>
                        failListsql = String.Format(failListsql, error_html, ProductNo)
                        EC.DB.ExecuteScalar(failListsql)
                    End Try

                    ProductNo = "" '防止333行迴圈跑空
                Loop

                Dim doActionName = ""
                If doAction = "tempReportGoods" Then
                    doActionName = "測試暫存"
                ElseIf doAction = "verifyReportGoods" Then
                    doActionName = "正式提報"
                End If

                postback = <html>&lt;/br&gt;
                                               &lt;table style='display: table;
                                                   border-collapse: separate;
                                                   border-spacing: 2px;
                                                   border-color: black;
                                                   border: 1px solid black;'&gt;
                                                  &lt;tr style='border: 1px solid black;'&gt;
                                                       &lt;td style='border: 1px solid black;'&gt;
                                                            總筆數:{0} 。 成功筆數:{1} 。 失敗筆數:{2}&lt;/br&gt;
                                                            &lt;/br&gt;
                                                       &lt;/td&gt;
                                                  &lt;/tr&gt;
                                                  &lt;tr style='border: 1px solid black;'&gt;
                                                       &lt;td style='border: 1px solid black;'&gt;
                                                            &lt;h4&gt;{5}:錯誤商品名稱與清單:&lt;/h4&gt;
                                                                      {3}{4}
                                                       &lt;/td&gt;
                                                  &lt;/tr&gt;
                                               &lt;/table&gt;
                                     </html>
                postback = String.Format(postback, totalCnt, successCnt, failCnt, ProductNames, error_html, doActionName)

                If successCnt = 0 And failCnt = 0 Then

                    Return "<p>資料皆已暫存，請提報API!!</p>"

                End If

                Return postback



            End If


            Return "<p>暫無新資料需提報API!!</p>"
        Catch ex As Exception

            Return ex.Message

        End Try

    End Function
#End Region

#Region "YahooShoppingSCM_proposals 提報架構類別"

    ''' <summary>
    ''' YahooShoppingSCM_proposals 提報架構
    ''' (註:部分欄位指定值須比照線上文件)
    ''' 現階段非必填欄位註解已利後續修改時打開
    ''' https://docs.google.com/document/d/1-ROog-SSxvCIKkWRfcjadtFmP_gnL4PkqbR7TfvR5Cg/edit?pli=1
    ''' </summary>
    ''' <remarks></remarks>
    Public Class YahooShoppingSCM
#Region "object"
        ''' <summary>
        ''' YahooShoppingSCM_proposals 提報架構類別
        ''' 參考 : https://stackoverflow.com/questions/8118019/vb-net-json-deserialize
        ''' </summary>

        Public Class JSONobj

            ''' <summary>
            ''' 提案人，限 10 個中文字 (必填●
            ''' 
            ''' ex : 孫協志
            ''' </summary>
            Public Property applicant As String

            ''' <summary>
            ''' 商品資訊 (審核狀態 reviewStatus 非 composing 時 (必填●
            ''' </summary>
            Public Property product As Product

            ''' <summary>
            ''' 審核狀態
            ''' composing: 尚未提案 (預設)
            '''	draft: 儲存暫不提案
            '''	pendingReview: 待審核
            '''	approved: 審核已通過
            '''	declined: 審核不通過
            '''	expired: 已過審核期限
            '''	
            ''' ex : draft
            ''' </summary>
            Public Property reviewStatus As String

            ''' <summary>
            ''' 提案當下的提案站別 ID, e.g. sub1只允許供應商有簽約的子站 (必填●
            ''' 
            ''' ex : sub1
            ''' </summary>
            Public Property subStationId As String

            ''' <summary>
            ''' 提案單類型 (必填●
            ''' newProduct: 新增贈品/配件/屬性
            '''	newListing: 新增一般賣場
            '''	newListingByApi: 透過 SCM API 新增一般賣場 (不可用於新增)
            '''	updateCopy: 修改賣場/商品詳情
            '''	updateVideo: 修改賣場影片
            '''	
            ''' ex : newListing
            ''' </summary>
            Public Property type As String
            '''' <summary>
            '''' 備註，限 200 個字
            '''' 
            '''' ex : 我是備註
            '''' </summary>
            'Public Property note As String
            ''' <summary>
            ''' 賣場資訊 type 為 newListing 且 reviewStatus 非 composing 時必填 (必填●
            ''' </summary>
            Public Property listing As Listing

        End Class
#End Region



#Region "object(Product)"
        ''' <summary>
        ''' 商品資訊 (審核狀態 reviewStatus 非 composing 時必填●
        ''' </summary>
        Public Class Product
            ''' <summary>
            ''' 商品規格顯示方式 (必填●
            ''' table
            ''' list
            ''' 
            ''' ex : table
            ''' </summary>
            Public Property attributeDisplayMode As String

            ''' <summary>
            ''' 商品規格表 (必填●
            '''被 ProposalModel 中被選中的 Attribute，不可再填入此欄位
            '''若規格為 structured data 中的選項
            '''	values 需符合 structured data constraint 限制 (radiobox/checkbox)
            '''	只能有一個 `自訂其他屬性`
            '''若規格為 `自訂`
            '''	values 只能有一個值
            '''	不允許重複 `自訂規格` 名稱
            '''	`自訂規格` 名稱不允許與 structured data 中的選項重複
            '''	`自訂規格` 名稱不允許與 `自訂屬性` 名稱重複
            ''' </summary>
            <JsonProperty(PropertyName:="attributes")>
            Public Property attributes() As List(Of Attribute) = New List(Of Attribute)

            ''' <summary>
            ''' 商品目前的分類子類 往上追溯的子站要跟供應商有簽約的子站有交集 (必填●
            ''' 
            ''' ex : catItem10070
            ''' </summary>
            Public Property catItemId As String

            ''' <summary>
            ''' 內容級別 (必填●
            ''' None: 無級別 
            ''' G: 普級
            ''' PG: 保護級
            ''' PG12: 輔導級 12歲+
            ''' PG15: 輔導級 15歲+
            ''' R: 限制級
            ''' NC18: 情趣商品 (若為未滿18歲青少年不能購買商品，請選擇限制級)
            ''' 
            ''' 若 subStationId = 28(電玩 / 遊戲)， 只允許 G, PG, PG12, PG15 And R
            ''' 若 subStationId = 566， 只允許 R And NC18
            ''' 其他子站不可選 PG15
            ''' 
            ''' ex : G
            ''' </summary>
            Public Property contentRating As String

            ''' <summary>
            ''' 成本(含稅+運費)，到小數點兩位 (必填●
            ''' range: [0-9999999]
            ''' 成本(含稅+運費) = 購物中心售價 = 廠商建議價
            ''' 
            ''' ex : 50.00
            ''' </summary>
            Public Property cost As String

            '''' <summary>
            '''' 是否為即期品 (必填●
            '''' 配送方式為`快速到貨`且有填寫商品保存期限時方可為 true
            '''' 預設為 false
            '''' 
            '''' ex : true
            '''' </summary>
            'Public Property isExpiringItem As Boolean

            '''' <summary>
            '''' 是否需要安裝
            '''' 配送方式為`快速到貨`且附約資訊包含 appliance 時方可填寫
            '''' 預設為 false
            '''' 
            '''' ex : true
            '''' </summary>
            'Public Property isInstallRequired As Boolean

            '''' <summary>
            '''' 是否為大材積商品 (即將棄用)
            '''' (舊) 配送方式為`快速到貨`且附約資訊包含 appliance 時方可為 true
            '''' (新) POST/PUT 皆應為 null
            '''' 預設為 false
            '''' 
            '''' ex : true
            '''' </summary>
            'Public Property isLargeVolume As Boolean

            '''' <summary>
            '''' 是否為大型商品附屬贈品
            '''' 配送方式為`快速到貨`且附約資訊包含 appliance 時方可填寫
            '''' 預設為 false
            '''' 
            '''' ex : true
            '''' </summary>
            'Public Property isLargeVolumnProductGift As Boolean

            '''' <summary>
            '''' 是否屬於廢四機
            '''' 供應商啟用功能包含 recycleAppliance 時可選
            '''' 
            '''' ex : true
            '''' </summary>
            'Public Property isNeedRecycle As Boolean

            '''' <summary>
            '''' 是否為買斷商品
            '''' 配送方式為`快速到貨`且附約資訊包含 outrightPurchase 時方可填寫
            '''' 預設為 false
            '''' 
            '''' ex : true
            '''' </summary>
            'Public Property isOutrightPurchase As Boolean

            '''' <summary>
            '''' 最小包裝數
            '''' range: [1-32767]
            '''' 配送方式為`快速到貨`時才能更改，其餘只能為 1
            '''' 預設為 1
            '''' 
            '''' ex : 10
            '''' </summary>
            'Public Property minPackingCount As Integer

            ''' <summary>
            ''' 屬性商品型號 (不一定要給
            ''' proposal type = 14 (updateVideo) 且非無屬性賣場時須提供 (必填●
            ''' </summary>
            Public Property models() As New List(Of Model)

            ''' <summary>
            ''' 廠商建議價，到小數點兩位 (必填●
            ''' range: [0-9999999]
            ''' 成本(含稅+運費) = 購物中心售價= 廠商建議價
            ''' 
            ''' ex : 100.00
            ''' </summary>
            Public Property msrp As String

            ''' <summary>
            ''' 商品名稱，最長 45 個字 (必填●
            ''' 若為配送方式為`快速到貨`且為即期品，必須加上前綴"(即期品)"
            ''' 
            ''' (即期品)我是商品名稱
            ''' </summary>
            Public Property name As String

            '''' <summary>
            '''' 主件商品供應商商品料號，最長 40 個字
            '''' 
            '''' 12345
            '''' </summary>
            'Public Property partNo As String

            ''' <summary>
            ''' 配送方式 (必填●
            ''' 1	Home	    預設可選
            ''' 61	Express24HR	附約資訊包含 warehouse 時可選        ''' 200	ESD	        分類進階功能包含 ESD 時可選        ''' 400	EDelievry	分類進階功能包含 eCoupon 且提案單類型為 newProduct 時可選
            ''' 800	HomeStore	供應商啟用功能包含 deliveryByCvs 時可選        ''' </summary>
            Public Property shipType As Shiptype

            ''' <summary>
            ''' 賣場簡短說明 (必填●
            ''' 最多 5 條，每條最長 15 個字
            ''' 至少需填 1 條        '''         ''' ex : [我是簡短說明,我是簡短說明2]        ''' </summary>
            Public Property shortDescription() As List(Of String)

            ''' <summary>
            ''' 結構化資料屬性集 ID (必填●
            ''' 
            ''' ex : 000003326689
            ''' </summary>
            Public Property struDataAttrClusterId As String

            '''' <summary>
            '''' 商品保證 (必填●
            '''' </summary>
            'Public Property warranty As Warranty

            ''' <summary>
            ''' 商品詳情 (必填●
            ''' 為 HTML 內容
            ''' 	iframe URL 只允許 Youtube
            ''' 	image URL 只允許 yimg.com
            ''' 	image 不會再進行縮圖
            ''' 	proposal.reviewStatus 非 composing 時，為必填
            ''' 	
            ''' ex : table html /table
            ''' </summary>
            Public Property copy As String

            '''' <summary>
            '''' 品牌
            '''' 最長 20 個字
            '''' 
            '''' ex : 我是品牌
            '''' </summary>
            'Public Property brand As String

            '''' <summary>
            '''' 包裝完成後的商品高度
            '''' 單位為 cm，需為正整數
            '''' 若為配送方式為`快速到貨`或`直店配`時必填 (必填●
            '''' max 9999
            '''' 
            '''' ex : 77
            '''' </summary>
            'Public Property height As Integer

            '''' <summary>
            '''' 包裝完成後的商品長度
            '''' 單位為 cm，需為正整數
            '''' 若為配送方式為`快速到貨`或`直店配`時必填 (必填●
            '''' max 9999
            '''' 
            '''' ex : 55
            '''' </summary>
            'Public Property length As Integer

            '''' <summary>
            '''' 商品型號
            '''' 最長 20 個字
            '''' 
            '''' ex : 我是商品型號
            '''' </summary>
            'Public Property model As String
            '''' <summary>
            '''' 商品保存期限
            '''' 單位為天
            '''' range: [1-32767]
            '''' 配送方式shiptype為`快速到貨`可填，否則應為 null
            '''' 
            '''' ex : 56
            '''' </summary>
            'Public Property preserveDays As Integer
            '''' <summary>
            '''' 包裝完成後的商品寬度
            '''' 單位為 cm，需為正整數
            '''' 若為配送方式為`快速到貨`或`直店配`時必填
            '''' max 9999
            '''' 
            '''' ex : 88
            '''' </summary>
            'Public Property weight As Integer
            '''' <summary>
            '''' 包裝完成後的商品重量
            '''' 單位為 g，需為正整數
            '''' 若為配送方式為`快速到貨`或`直店配時`必填
            '''' max 2147483647
            '''' 
            '''' ex : 66
            '''' </summary>
            'Public Property width As Integer
            ''' <summary>
            ''' 商品屬性 (必填●        ''' </summary>
            Public Property specs() As List(Of Spec2) = New List(Of Spec2)
        End Class
#End Region

#Region "object(Product)(Attribute)"
        ''' <summary>
        ''' 商品規格表 (必填●
        '''被 ProposalModel 中被選中的 Attribute，不可再填入此欄位
        '''若規格為 structured data 中的選項
        '''	values 需符合 structured data constraint 限制 (radiobox/checkbox)
        '''	只能有一個 `自訂其他屬性`
        '''若規格為 `自訂`
        '''	values 只能有一個值
        '''	不允許重複 `自訂規格` 名稱
        '''	`自訂規格` 名稱不允許與 structured data 中的選項重複
        '''	`自訂規格` 名稱不允許與 `自訂屬性` 名稱重複
        ''' </summary>
        Public Class Attribute
            ''' <summary>
            ''' 屬性名稱 (必填●
            ''' 
            ''' ex : 中央處理器品牌
            ''' ex : 中央處理器型號
            ''' ex : 型號
            ''' ex : 晶片組
            ''' ex : 重量(kg)
            ''' </summary>
            <JsonProperty(PropertyName:="name")>
            Public Property name As String
            ''' <summary>
            ''' 屬性值 (必填●
            ''' 
            ''' ex : Intel
            ''' ex : G870
            ''' ex : Trey-Super-PC
            ''' ex : B75
            ''' ex : 27t
            ''' </summary>
            <JsonProperty(PropertyName:="values")>
            Public Property values() As List(Of String)
        End Class
#End Region

#Region "object(Product)(Models)"

        ''' <summary>
        ''' 屬性商品型號 (不一定要給
        ''' proposal type = 14 (updateVideo) 且非無屬性賣場時須提供 (必填●
        ''' </summary>
        Public Class Model
            ''' <summary>
            ''' 屬性值 (必填●
            ''' </summary>
            <JsonProperty(PropertyName:="items")>
            Public Property items() As List(Of Item)
            '''' <summary>
            '''' 商品影片 (必填●
            '''' </summary>
            '<JsonProperty(PropertyName:="videos")>
            'Public Property videos() As List(Of Video)
            ''' <summary>
            ''' 商品圖 (必填●
            ''' newListing/newProduct
            ''' 最多 10 張，需要為正方形，尺寸大於 1000^2 pixels 會被縮至 
            ''' 1000^2, 400^2, 250^2, 135^2 及 80^2 pixels、
            ''' 大於 400^2 pixels 會被縮至 400^2, 250^2, 135^2 及 80^2 pixels
            ''' 至少需要兩張主圖 (1000^2 pixels)，可增加 8 張副圖 (1000^2 Or 400^2 pixels)
            '''	updateImage
            '''	用於 proposal.listing.models 且 order = 1 時，可於各尺寸指定小圖 URL，未指定寬高的維度會是 1000x1000 圖檔自動縮圖
            '''	用於 proposal.listing.[additionalPurchase|complimentaries|selectComplimentaries]，規則同 newListing / newProduct
            ''' </summary>
            <JsonProperty(PropertyName:="images")>
            Public Property images() As List(Of Image)
            ''' <summary>
            ''' 第 1 層屬性的 spec
            ''' 需為 ProposalProductSpec 中 level=1 的 spec
            ''' 若為無屬性商品時為空, 有 1 層及以上屬性時必填 (必填●
            ''' 若 spec name 是從 structured data 中挑選，且 constraint type 為
            ''' radiobox 或 checkbox 時
            ''' 	values 一定要包含在 constraint 選項中
            '''		`自訂項目` 時，values 為 `其他`，自訂內容存放於 displayName
            '''	若 spec name 為自訂時
            '''	    `自訂項目` 的值存放於 spec.values
            '''	    displayName 需為必填，但不需與 `自訂項目` 的值一致
            ''' </summary>
            <JsonProperty(PropertyName:="spec")>
            Public Property spec As Spec
            ''' <summary>
            ''' 賣場顯示名稱，預設為規格名稱 (必填●
            ''' 若為無屬性商品時為空, 有 1 層及以上屬性時可填
            ''' 
            ''' ex : 極致簡約Dell2019
            ''' </summary>
            <JsonProperty(PropertyName:="displayName")>
            Public Property displayName As String
        End Class
#End Region
#Region "object(Product)(Models)(spec)"

        ''' <summary>
        ''' 第 1 層屬性的 spec
        ''' 需為 ProposalProductSpec 中 level=1 的 spec
        ''' 若為無屬性商品時為空, 有 1 層及以上屬性時必填 (必填●
        ''' 若 spec name 是從 structured(結構化大中小類) data 中挑選，且 constraint type 為
        ''' radiobox 或 checkbox 時
        ''' 	values 一定要包含在 constraint 選項中
        '''		`自訂項目` 時，values 為 `其他`，自訂內容存放於 displayName
        '''	若 spec name 為自訂時
        '''	    `自訂項目` 的值存放於 spec.values
        '''	    displayName 需為必填，但不需與 `自訂項目` 的值一致
        ''' </summary>
        Public Class Spec
            ''' <summary>
            ''' ex : 品牌
            ''' ex : 品牌
            ''' </summary> 
            <JsonProperty(PropertyName:="name")>
            Public Property name As String
            ''' <summary>
            ''' ex : Dell戴爾
            ''' ex : hp惠普
            ''' </summary>
            <JsonProperty(PropertyName:="values")>
            Public Property values() As List(Of String)
        End Class

#End Region
#Region "object(Product)(Models)(Items)"
        ''' <summary>
        ''' 屬性值 (必填●
        ''' </summary>
        Public Class Item
            ''' <summary>
            ''' 備貨數量
            '''若配送方式 (shipType) 為 61 (集宅配)，則只能為 0
            '''若配送方式 (shipType) 為 61 (集宅配)，default 0
            '''range:
            '''●	成本>= 5萬 0-15
            '''●	成本>= 3萬: 0-50
            '''●	成本>= 1萬: 0-100
            '''●	成本>= 5千: 0-300
            '''●	成本>= 3千: 0-500
            '''●	成本>= 0: 0-1000
            ''' </summary>
            Public Property stock As String
            '''' <summary>
            '''' 屬性商品供應商商品料號
            '''' 最長 40 個字
            '''' 
            '''' ex : 5566
            '''' </summary>
            'Public Property partNo As String
            '''' <summary>
            '''' 實際國際條碼
            '''' 限定為13碼或12碼 (12碼由 API 在前面補 0 )
            '''' 若為下列配送方式 (shipType)時則可填寫
            '''' 	1 (宅配)
            ''''     61 (集宅配)
            ''''     800 (直店配)
            '''' </summary>
            'Public Property barcode As String
            '''' <summary>
            '''' 進倉用國際條碼
            '''' 限定為13碼或12碼 (12碼由 API 在前面補 0 )
            '''' 
            '''' ex : 725272730706
            '''' </summary>
            'Public Property warehouseBarcode As String
            ''' <summary>
            ''' 第 2 層屬性的 spec
            ''' 需為 ProposalProductSpec 中 level=2 的 spec
            ''' 若為無屬性與 1 層屬性商品時為空, 為 2 層屬性商品時為必填
            ''' 
            ''' ex : 9785109946480
            ''' </summary>
            <JsonProperty(PropertyName:="spec")>
            Public Property spec As Spec1
            ''' <summary>
            ''' 賣場顯示名稱
            ''' 預設為規格名稱
            ''' 若為無屬性與 1 層屬性商品時為空, 為 2 層屬性商品時可填
            ''' 
            ''' ex : 卡其色
            ''' </summary>
            <JsonProperty(PropertyName:="displayName")>
            Public Property displayName As String
        End Class

#End Region
#Region "object(Product)(Models)(Item)(Spec)"
        ''' <summary>
        '''第 2 層屬性的 spec
        '''需為 ProposalProductSpec 中 level=2 的 spec
        '''若為無屬性與 1 層屬性商品時為空, 為 2 層屬性商品時為必填
        '''ex : 9785109946480
        ''' </summary>
        Public Class Spec1
            ''' <summary>
            ''' ex : 顏色        ''' </summary>
            <JsonProperty(PropertyName:="name")>
            Public Property name As String
            ''' <summary>
            ''' ex : 卡其        ''' </summary>
            <JsonProperty(PropertyName:="values")>
            Public Property values() As List(Of String)
        End Class
#End Region
        '#Region "object(Product)(Models)(Video)"
        '    ''' <summary>
        '    ''' 商品影片 (必填●
        '    ''' </summary>
        '    Public Class Video
        '        ''' <summary>
        '        ''' 影片 url
        '        ''' 若 FQDN 為 {s|ct}.yimg.com 或 edgecast-vod.yahoo.net，必需為 https 協定
        '        ''' 目前支援的 Video Codecs 請見列表 
        '        ''' https://docs.aws.amazon.com/en_us/mediaconvert/latest/ug/reference-codecs-containers-input.html
        '        ''' 
        '        ''' ex :
        '        ''' https://s.yimg.com/bp/Files/374d9974ab2cbce382e42724fede7aa07313cae6.qt
        '        ''' </summary>
        '        <JsonProperty(PropertyName:="url")>
        '        Public Property url As String
        '        ''' <summary>
        '        ''' 排列
        '        ''' 
        '        ''' ex : 1
        '        ''' </summary>
        '        <JsonProperty(PropertyName:="order")>
        '        Public Property order As Integer
        '    End Class

        '#End Region
#Region "object(Product)(Models)(Image)"
        Public Class Image
            ''' <summary>
            ''' 影片 url
            ''' 若 FQDN 為 {s|ct}.yimg.com 或 edgecast-vod.yahoo.net，必需為 https 協定
            ''' 目前支援的 Video Codecs 請見列表 
            ''' https://docs.aws.amazon.com/en_us/mediaconvert/latest/ug/reference-codecs-containers-input.html
            ''' 
            ''' ex : https://s.yimg.com/bp/Files/ba0b8bf005bab4e7cc8821afea217e342a1dfca3.png
            ''' 
            ''' </summary>
            <JsonProperty(PropertyName:="url")>
            Public Property url As String
            ''' <summary>
            ''' 排列
            ''' starts from 1
            ''' 
            ''' ex : 1
            ''' </summary>
            <JsonProperty(PropertyName:="order")>
            Public Property order As Integer
        End Class
#End Region

#Region "object(Product)(Shiptype)"

        ''' <summary>
        ''' 配送方式 (必填●
        ''' 1	Home	    預設可選
        ''' 61	Express24HR	附約資訊包含 warehouse 時可選    ''' 200	ESD	        分類進階功能包含 ESD 時可選    ''' 400	EDelievry	分類進階功能包含 eCoupon 且提案單類型為 newProduct 時可選
        ''' 800	HomeStore	供應商啟用功能包含 deliveryByCvs 時可選    ''' </summary>
        Public Class Shiptype
            ''' <summary>
            ''' 配送方式 (必填●
            ''' 1	Home	    預設可選 宅配(轉單/自出)
            ''' 61	Express24HR	附約資訊包含 warehouse 時可選 快速到貨(進倉)        ''' 200	ESD	        分類進階功能包含 ESD 時可選        ''' 400	EDelievry	分類進階功能包含 eCoupon 且提案單類型為 newProduct 時可選 電子禮券
            ''' 800	HomeStore	供應商啟用功能包含 deliveryByCvs 時可選 直店配(宅配+超取)        '''         ''' ex : 61        ''' </summary>
            <JsonProperty(PropertyName:="id")>
            Public Property id As Integer
        End Class

#End Region

        '#Region "object(Product)(Warranty)"

        '    ''' <summary>
        '    ''' 商品保證 (必填●
        '    ''' </summary>
        '    Public Class Warranty

        '        ''' <summary>
        '        ''' 保固期限 (必填●
        '        ''' 如果內容值為`無保固`則保固範圍及保固來源可不填，反之若為其他值則必填
        '        ''' 最長 20 個字
        '        ''' 
        '        ''' ex : 一個月
        '        ''' </summary>
        '        Public Property period As String

        '        ''' <summary>
        '        ''' 說明訊息整段描述
        '        ''' 最長 800 個字
        '        ''' 
        '        ''' ex : 保固說明文字
        '        ''' </summary>
        '        Public Property description As String

        '        ''' <summary>
        '        ''' 保固來源
        '        ''' none: 無保固
        '        ''' official: 原廠保固
        '        ''' retailer: 經銷保固
        '        ''' 若保固期限為`無保固`，只能為`none` (若有輸入會被取代)
        '        ''' 若保固期限不為`無保固`，預設為`official`
        '        ''' 
        '        ''' ex : official
        '        ''' </summary>
        '        Public Property handler As String
        '        ''' <summary>
        '        ''' 保固範圍
        '        ''' 最長 20 個字
        '        ''' 若保固期限為`無保固`，自動填入`無保固`
        '        ''' 
        '        ''' ex : 新品瑕疵
        '        ''' </summary>
        '        Public Property scope As String
        '    End Class
        '#End Region

#Region "object(Product)(Specs)"
        ''' <summary>
        ''' 商品屬性 (必填●    '''     ''' 此值可決定 1層 2層 屬性商品    ''' </summary>
        Public Class Spec2
            ''' <summary>
            ''' 屬性層級 (必填●
            ''' range: [1-2]
            ''' 
            ''' exe : 1
            ''' exe : 2
            ''' </summary>
            <JsonProperty(PropertyName:="level")>
            Public Property level As Integer
            ''' <summary>
            ''' 屬性名稱最長 30 個字 (必填●
            ''' 
            ''' exe : 品牌
            ''' exe : 顏色
            ''' </summary>
            <JsonProperty(PropertyName:="name")>
            Public Property name As String
        End Class
#End Region



#Region "object(Listing)"

        ''' <summary>
        ''' 賣場資訊 type 為 newListing 且 reviewStatus 非 composing 時必填 (必填●
        ''' </summary>
        Public Class Listing
            ''' <summary>
            ''' 商品目前的分類子類，往上追溯的子站要跟供應商有簽約的子站有交集 (必填●
            ''' ex : catItem10070
            ''' </summary>
            <JsonProperty(PropertyName:="catItemId")>
            Public Property catItemId As String
            ''' <summary>
            ''' 交貨期限 
            ''' 預設 normal
            ''' normal 正常交貨期
            ''' preOrder: 預購型商品
            ''' customized: 客製化商品
            ''' appointment: 客約送貨日
            ''' 
            ''' shipTypeId = 61
            ''' (集宅配)	if (product.isInstallRequired = true)
            ''' ? appointment :  normal
            ''' shipTypeId != 61
            ''' (非集宅配)	if category.functions contains
            ''' preOrderDelivery: normal, preOrder
            ''' customizedDelivery: normal, customized
            ''' appointmentDelivery: normal, appointment
            ''' 
            ''' 提案一般賣場且 reviewStatus 為 draft草案 時以上必填 (必填●
            ''' 
            ''' ex : appointment
            ''' </summary>
            <JsonProperty(PropertyName:="deliveryType")>
            Public Property deliveryType As String
            '''' <summary>
            '''' 特色標題，最長 20 個字
            '''' 
            '''' ex : 我是特色標題
            '''' </summary>
            'Public Property featureTitle As String
            ''' <summary>
            ''' 購物中心售價，到小數點兩位
            ''' range: [0-9999999]
            ''' rules:
            ''' 1.	cost (成本(含稅+運費)) 小於等於 price (購物中心售價) 小於等於 msrp (廠商建議價)
            ''' 2.	shipTypeId=800 (直店配) 時需 小於等於 20000
            ''' 提案一般賣場且 reviewStatus 為 draft 以上時必填 (必填●
            ''' 
            ''' 若"賣場毛利率 ((購物中心售價 - 成本) / 購物中心售價) 小於 子站毛利率"時，則
            ''' 1.	若有自動過審 (user.profile.toggles contains autoApproveListingOnOffShelve)時，API 會阻擋該 request
            ''' 2.	若無自動過審，僅檢查賣場毛利率需>=0
            ''' 
            ''' ex : 100.00
            ''' </summary>
            <JsonProperty(PropertyName:="price")>
            Public Property price As String
            ''' <summary>
            ''' 賣場網址，最長 25 個字
            ''' 只接受正/簡體中文、英文、數字、`-`，多個 `-` 會合併成一個，開頭不能是 `-`
            ''' 提案一般賣場且 reviewStatus 為 draft 以上時必填(必填●
            ''' 
            ''' ex : 我是商品名稱
            ''' </summary>
            <JsonProperty(PropertyName:="seoUrl")>
            Public Property seoUrl As String
        End Class
#End Region

    End Class
#End Region

    ''' <summary>
    ''' Yahoo欄位查詢API+資料更新
    ''' </summary>
    Class Field_Update
#Region "GET YahooShoppingSCM_categories API (取回類別架構)"
        ''' <summary>
        ''' 查詢類別並存入
        ''' 表名稱:"YahooshoppingSCM_API_Category" 、 位址:10.0.11.1 (LS3C_V2_2005)
        ''' (註:無做錯誤處理與反饋)
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub GET_categories_API()

            '雅虎API URL TEST查詢值在Yahoo Shopping SCM API 文件上
            Dim v1 As String = "https://tw.supplier.yahoo.com/api/spa/v1/"
            '精技提案類別項目 Max 50個超過不知道會怎樣文件沒寫 ，頂層有一個大類 z1，但呼叫了回傳空值
            Dim subs As String = "sub1,sub2,sub15,sub17,sub22,sub24,sub26,sub27,sub34,sub36,sub38,sub51,sub52,sub55,sub107,sub112,sub121,sub168,sub197,sub215,sub408,sub431,sub436,sub453,sub454,sub455,sub469,sub583,sub617,sub681,sub682,sub693,sub695,sub696,sub704,sub788,sub792,sub793,sub794,sub810,sub812"
            '查詢類別
            Dim E As String = v1 + "categories?categoryId=" + subs + "&fields=children,parents" 'ok
            '呼叫API
            Dim Z As String = Request_YahooSCM_API("Get", E, "")
            '轉為JOBJECT
            Dim Obj As Newtonsoft.Json.Linq.JObject = Newtonsoft.Json.JsonConvert.DeserializeObject(Z)

            '需要將sub與categoryId存入sql後，因為直接呼叫sub無categoryId名稱
            '必須再取出categoryId整理為一包(有max50限制)呼叫api才取的到categoryId的中文名稱
            '將jsonobj資料存入mssql可能有兩種方式 
            '1.程式內將json整理為 datatable class 類別檔，在整包datatable存入 mssql
            '參考 https://dotblogs.com.tw/justforgood/2014/05/29/145303
            '參考 http://www.infolight.com.tw/WebClient/FAQResponse.aspx?QuestionID=NTM2
            '參考 https://blog.xuite.net/beary2k2/twblog/567089844-MS+SQL+Server+%E5%AD%98%E5%8F%96+Datatable+for+C%23
            '參考 https://stackoverflow.com/questions/21820198/accessed-jarray-values-with-invalid-key-value-fields-array-position-index-ex
            '2.跑回圈整理成insert tsql string 整包進入 sql 儲存
            'Insert Query vs SqlBulkCopy 效能比較 :
            '參考 https://stackoverflow.com/questions/33774163/insert-query-vs-sqlbulkcopy-which-one-is-best-as-performance-wise-for-insert-r

            '嘗試第一種方法 但不特別建置 類別檔
            Dim dt As DataTable = New DataTable
            Dim newRow As DataRow = Nothing
            dt.Columns.Add("subId")
            dt.Columns.Add("subname")
            dt.Columns.Add("subVisible")
            dt.Columns.Add("categoryId")
            dt.Columns.Add("categoryIdname")
            dt.Columns.Add("categoryVisible")
            dt.Columns.Add("catItemId")
            dt.Columns.Add("catItemIdname")
            dt.Columns.Add("catItemVisible")
            '一開始不清楚rows.add新增機制卡在第一層迴圈引入 0 1，全部改至第二層迴圈即可
            For number As Int32 = 0 To Obj("categories").Count - 1 Step 1
                For number2 As Int32 = 0 To Obj("categories")(number).Item("childrenIdList").Count - 1 Step 1

                    '接著必須要將 categoryID 查詢出來再對 GET api/spa/v1/categories 要回 categoryIdname
                    '但 categoryId 查詢參數似乎有限制 max50 筆，依寫 code 當下約8百多筆資料
                    '採取呼叫一次寫入一次方案 ... 總共幾筆就呼叫幾次
                    '直接利用第一次呼叫api之回傳整理過後的 datatable dt

                    Dim categoryId As String = Obj("categories")(number).Item("childrenIdList")(number2).ToString() 'categoryId 
                    '查詢類別
                    Dim X As String = v1 + "categories?categoryId=" + categoryId + "&fields=children,parents" 'ok
                    '呼叫API
                    Dim XX As String = Request_YahooSCM_API("Get", X, "")
                    '轉為JOBJECT
                    Dim ObjX As Newtonsoft.Json.Linq.JObject = Newtonsoft.Json.JsonConvert.DeserializeObject(XX)

                    '無值須填空
                    '第三層catItem
                    For number3 As Int32 = 0 To ObjX("children").Count - 1 Step 1

                        newRow = dt.NewRow
                        '看似重複應該可以抓出來寫成內函式

                        '第一次呼叫API
                        Dim subId As String = Obj("categories")(number).Item("categoryId").ToString()

                        If subId = "" Or Nothing Then
                            newRow(0) = ""
                        Else
                            newRow(0) = subId
                        End If

                        Dim subname As String = Obj("categories")(number).Item("name").ToString()

                        If subname = "" Or Nothing Then
                            newRow(1) = ""
                        Else
                            newRow(1) = subname
                        End If


                        Dim subVisible As String = Obj("categories")(number).Item("visible").ToString()

                        If subVisible = "" Or Nothing Then
                            newRow(2) = ""
                        Else
                            newRow(2) = subVisible
                        End If


                        Dim categoryIdz As String = Obj("categories")(number).Item("childrenIdList")(number2).ToString()

                        If categoryIdz = "" Or Nothing Then
                            newRow(3) = ""
                        Else
                            newRow(3) = categoryIdz
                        End If


                        '第二次呼叫API
                        Dim categoryIdname As String = ObjX("categories")(0).Item("name").ToString()

                        If categoryIdname = "" Or Nothing Then
                            newRow(4) = ""
                        Else
                            newRow(4) = categoryIdname
                        End If

                        Dim categoryVisible As String = ObjX("categories")(0).Item("visible").ToString()

                        If categoryVisible = "" Or Nothing Then
                            newRow(5) = ""
                        Else
                            newRow(5) = categoryVisible
                        End If



                        '第三層catItem
                        Dim catItemId As String = ObjX("children")(number3).Item("categoryId").ToString()

                        If catItemId = "" Or Nothing Then
                            newRow(6) = ""
                        Else
                            newRow(6) = catItemId
                        End If

                        Dim catItemIdname As String = ObjX("children")(number3).Item("name").ToString()

                        If catItemIdname = "" Or Nothing Then
                            newRow(7) = ""
                        Else
                            newRow(7) = catItemIdname
                        End If

                        Dim catItemVisible As String = ObjX("children")(number3).Item("visible").ToString()

                        If catItemVisible = "" Or Nothing Then
                            newRow(8) = ""
                        Else
                            newRow(8) = catItemVisible
                        End If

                        dt.Rows.Add(newRow)
                    Next

                Next
            Next

            If dt.Rows.Count > 0 Then
                '必須先清空一次整個table避免重複寫入
                Dim sql As String = "DELETE FROM YahooshoppingSCM_API_Category"
                '利用SqlBulkCopy將datatable存入db
                If EC.DB.ExecuteDataTable(sql).Rows.Count = Nothing Then
                    Using bulkcopy As System.Data.SqlClient.SqlBulkCopy = New System.Data.SqlClient.SqlBulkCopy(EC.DB.GetConnString())
                        bulkcopy.DestinationTableName = "YahooshoppingSCM_API_Category" '表名稱 位址:10.0.11.1 (LS3C_V2_2005)
                        If bulkcopy.DestinationTableName <> Nothing Or bulkcopy.DestinationTableName <> "" Then
                            bulkcopy.BatchSize = dt.Rows.Count
                            bulkcopy.BulkCopyTimeout = 120
                            bulkcopy.WriteToServer(dt)
                        End If
                        bulkcopy.Close()
                    End Using
                End If
            End If

        End Sub

#End Region

#Region "GET YahooShoppingSCM_struDataAttrClusters API (取回結構化商品標籤)"
        ''' <summary>
        ''' 查詢類別並存入
        ''' 表名稱:"YahooshoppingSCM_API_Category" 、 位址:10.0.11.1 (LS3C_V2_2005)
        ''' (註:無做錯誤處理與反饋)
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Sub GET_struDataAttrClusters_API()

            '雅虎API URL TEST查詢值在Yahoo Shopping SCM API 文件上
            Dim v1 As String = "https://tw.supplier.yahoo.com/api/spa/v1/"
            '查詢資料庫內抓回之catItem作為查詢參數
            'Category ID which prefixed with its level, allow level 'catItem' only, e.g. 'catItem5566'
            'Must be a pair parameter of proposalType.
            '一次只能一筆catItemID所以可以將查詢回來的DataReader分row呼叫api
            Dim catItems As IDataReader = EC.DB.ExecuteReader("SELECT catItemID FROM [LS3C_V2_2005].[dbo].[YahooshoppingSCM_API_Category] WITH(NOLOCK)")

            '逐筆存成datable 再利用 SqlBulkCopy 存入資料庫
            '參考 : GET_categories_API() 就不再多做註解
            Dim dt As DataTable = New DataTable
            Dim newRow As DataRow = Nothing
            dt.Columns.Add("catItems")
            dt.Columns.Add("struDataAttrClusterId")
            dt.Columns.Add("struDataAttrClusterName")
            dt.Columns.Add("Attrtype")
            dt.Columns.Add("Attrname")
            dt.Columns.Add("Attrrequired")
            dt.Columns.Add("Attrvalues")
            '錯誤處理
            Dim ErrorcatItem As String

            '查出來後利用dataread逐筆呼叫api
            Do While catItems.Read()

                '查詢結構化屬性 
                Dim catItem As String = catItems.GetString(0)
                Dim E As String = v1 + "struDataAttrClusters?categoryId=" + catItem + "&proposalType=newListing" 'ok

                Try
                    '呼叫API
                    Dim Z As String = Request_YahooSCM_API("Get", E, "")

                    '轉為JOBJECT
                    Dim Obj As Newtonsoft.Json.Linq.JObject = Newtonsoft.Json.JsonConvert.DeserializeObject(Z)

                    For number As Int32 = 0 To Obj("struDataAttrClusters").Count - 1 Step 1
                        For number2 As Int32 = 0 To Obj("struDataAttrClusters")(number).Item("attributes").Count - 1 Step 1

                            newRow = dt.NewRow

                            '20191125 捕塞 catItems

                            newRow(0) = catItems.Item("catItemID").ToString


                            '看似重複應該可以抓出來寫成內函式

                            Dim struDataAttrClusterId As String = Obj("struDataAttrClusters")(number).Item("id").ToString()
                            If struDataAttrClusterId = "" Or Nothing Then
                                newRow(1) = ""
                            Else
                                newRow(1) = struDataAttrClusterId
                            End If

                            Dim struDataAttrClusterName As String = Obj("struDataAttrClusters")(number).Item("name").ToString()
                            If struDataAttrClusterName = "" Or Nothing Then
                                newRow(2) = ""
                            Else
                                newRow(2) = struDataAttrClusterName
                            End If

                            Dim Attrtype As String = Obj("struDataAttrClusters")(number).Item("attributes")(number2).Item("constraints").Item("type").ToString()
                            If Attrtype = "" Or Nothing Then
                                newRow(3) = ""
                            Else
                                newRow(3) = Attrtype
                            End If

                            Dim Attrname As String = Obj("struDataAttrClusters")(number).Item("attributes")(number2).Item("name").ToString()
                            If Attrname = "" Or Nothing Then
                                newRow(4) = ""
                            Else
                                newRow(4) = Attrname
                            End If

                            Dim Attrrequired As String = Obj("struDataAttrClusters")(number).Item("attributes")(number2).Item("required").ToString()
                            If Attrrequired = "" Or Nothing Then
                                newRow(5) = ""
                            Else
                                newRow(5) = Attrrequired
                            End If

                            Dim Attrvalues As String = Obj("struDataAttrClusters")(number).Item("attributes")(number2).Item("values").ToString()
                            If Attrvalues = "" Or Nothing Then
                                newRow(6) = ""
                            Else
                                newRow(6) = Attrvalues
                            End If


                            dt.Rows.Add(newRow)

                        Next
                    Next

                Catch ex As Exception
                    '不知道為何會有些catItem會呼叫不到結構化標籤，嘗試過與Visible也無關係
                    ErrorcatItem += "," + catItem

                End Try

                Dim dtc As String = dt.Rows.Count

            Loop

            If dt.Rows.Count > 0 Then
                '必須先清空一次整個table避免重複寫入
                Dim sql As String = "DELETE FROM YahooshoppingSCM_API_struDataAttrClusters"
                '利用SqlBulkCopy將datatable存入db
                If EC.DB.ExecuteDataTable(sql).Rows.Count = Nothing Then
                    Using bulkcopy As System.Data.SqlClient.SqlBulkCopy = New System.Data.SqlClient.SqlBulkCopy(EC.DB.GetConnString())
                        bulkcopy.DestinationTableName = "YahooshoppingSCM_API_struDataAttrClusters" '表名稱 位址:10.0.11.1 (LS3C_V2_2005)
                        If bulkcopy.DestinationTableName <> Nothing Or bulkcopy.DestinationTableName <> "" Then
                            bulkcopy.BatchSize = dt.Rows.Count
                            bulkcopy.BulkCopyTimeout = 120
                            bulkcopy.WriteToServer(dt)
                        End If
                        bulkcopy.Close()
                    End Using
                End If
            End If


        End Sub

#End Region
    End Class

#Region "YahooShoppingSCM_API_request (URL TEST)"
    ''' <summary>
    ''' 因精技需求分類優先串接之API
    ''' (註:無做錯誤處理與反饋)
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared Sub API_URL_TEST()

            '雅虎API URL TEST查詢值在Yahoo Shopping SCM API 文件上
            Dim v1 As String = "https://tw.supplier.yahoo.com/api/spa/v1/"

            'GET Yahoo Shopping SCM API
            '查詢使用者資訊
            Dim B As String = v1 + "user" '暫不使用
            Dim C As String = v1 + "user?fields=-profile" '暫不使用
            '查詢雅虎服務窗口
            Dim D As String = v1 + "serviceDesks?type=proposal" '暫不使用
            '查詢類別
            Dim E As String = v1 + "categories?categoryId=catItem33908&fields=children,parents" 'ok
            '查詢結構化數據屬性集群
            '固定 proposalType = newListing 只幫精技做一次性新增一般賣場其餘修改接透過雅虎購物中心後台
            Dim F As String = v1 + "struDataAttrClusters?categoryId=catItem95356&proposalType=newListing" 'ok

            '查詢提案 有例外詳於文件
            Dim G As String = v1 + "proposals" '暫不使用
            '查詢登錄供應商列表
            Dim I As String = v1 + "listing/3367399?fields=" '暫不使用
            '範例json格式
            'Request GET / spa / v1 / listing
            'Response Status HTTP/1.1 201
            '單層屬性賣場
            'https://docs.google.com/document/d/1Zuw1Ps0Xh5DhiY97cP_x1QzRjgJezppIIqzkJnWvIj8/edit
            '雙層屬性賣場
            'https://docs.google.com/document/d/1yKZHkZa4QvgKYUxOWlWM30JBIXYOLwPVQcHLJ3FG7sk/edit
            '無屬性賣場
            'https://docs.google.com/document/d/1YeCaOPA51Brzw5nVLEcSYewlqtEBPxuEZ40h9zQgZmU/edit



            'POST Yahoo Shopping SCM API
            '創建提案
            Dim H As String = v1 + "proposals" 'ok
            '範例json格式 
            'Request POST / spa / v1 / proposals body
            'Response Status HTTP/1.1 201




            '新增一般賣場(二階層屬性)
            'Request
            'https://docs.google.com/document/d/1-ROog-SSxvCIKkWRfcjadtFmP_gnL4PkqbR7TfvR5Cg/edit?pli=1
            'Response
            'https://docs.google.com/document/d/1Xw3Rf_LirWAizvjJU9QV44Nye206AEwKTpyzj70iDEY/edit 

            '修改賣場/商品詳情提案
            'Request
            'https://docs.google.com/document/d/1lpaxTgZsCequXiyj7GO0XcWXyzDe6yAFfmNQiUbT7tI/edit?pli=1
            'Response
            'https://docs.google.com/document/d/1HqPLzfY9XbgnqlGpnhKe04kWoKE0KDUXd4xdNykDI2s/edit

            '修改賣場影片(有屬性)
            'Request
            'https://docs.google.com/document/d/13xU9qoNWb07MDTgyiFBmr-fTJqby0eMsK3oWmW0GPpI/edit?pli=1
            'Response
            'https://docs.google.com/document/d/1eekPRUkLJe6bROHcAF0WqsWAB0ETEwiMfZ48Oah03w0/edit

            '修改賣場影片(無屬性)
            'Request
            'https://docs.google.com/document/d/1EhX2ekywSBG16FCGE4meyPE1jDv4Z8ksIJMBMJlMz84/edit?pli=1
            'Response
            'https://docs.google.com/document/d/15rp85S6iSyUn106okz87ULoIt2AxQr51fS1P2PklLlo/edit

            '檔案上傳流程 取得臨時憑證獲取token將檔案上傳至雅虎購物中心提供之aws
            'https://aws.amazon.com/getting-started/tools-sdks/
            '上傳成功後由response header 取得ETag 透過GET /fileObhects/{ETag} 取得該檔案URL
            Dim J As String = "https://tw.buy.yahoo.com/api/fileUploader/v1/credentials" '暫不使用
            '查詢檔案上傳臨時憑證token
            Dim K As String = "https://tw.buy.yahoo.com/api/fileUploader/v1/fileObjects/{ETag}" '暫不使用
            '查詢上傳檔案url

            '''''''測試''''''
            Dim Z As String = Request_YahooSCM_API("GET", B, "")


            '序列化JSONTEXT
            '參考 https://stackoverflow.com/questions/8118019/vb-net-json-deserialize
            'Dim testsss As YahooShoppingSCM_proposals.Rootobject = New YahooShoppingSCM_proposals.Rootobject{applicant="abc"}
            ' Dim testobj As Newtonsoft.Json.Linq.JObject = JsonSerializer.Serialize(testsss)

        End Sub
#End Region
#Region "YahooShoppingSCM_proposals_TEST 測試結果"

        ''' <summary>
        ''' <para>YahooShoppingSCM_proposals_TEST 測試結果</para>
        ''' <para>測試提報JSON目前已知如下 :</para>
        ''' <para>(product)(attributeDisplayMode)必要 屬性名稱與值</para>
        ''' <para>(product)(specs) 指定leve 與 屬性名稱</para>
        ''' <para>(product)(models)(spec)leve 1 屬性名稱與值</para>
        ''' <para>(product)(models)(items)(spec)leve 2 屬性名稱與值</para>
        ''' <para>struDataAttrclusters有必填屬性且除了可自訂欄位值外必須選擇雅虎所提供的屬性值</para>
        ''' <para>struDataAttrclusters只能用catItemId查詢</para>
        ''' <para>spec1 2 必須符合 level 1 2 所指定屬性與屬性值</para>
        ''' <para>影片與圖片包含html裡鑲嵌的必須通過驗證否則皆會報錯</para>
        ''' <para>以下TESTJSONPROPOSALS已刪除不必要條件(皆由手動人工後台調整)</para>
        ''' <para>★★★★★顏色與品牌是幾乎都會有的屬性可以設level以這兩個為定值★★★★★</para>
        ''' </summary>
        ''' <remarks></remarks>
        Private Class YahooTESTpost

            Public Shared Function Post_proposals_API_TEST()
                '★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★
                '物件轉JSON
                '參考 : https://stackoverflow.com/questions/33213265/serializing-nested-json-with-json-net-vb-net
                '物件或陣列裡面如果有物件需給定參考值

                '測試塞值後要上傳的JSON檔格式

                '問題 : 
                '類別檔雖然宣告為 List 或 物件型態 但還是必須要，
                '利用現有基類把值做出來再塞回去不太確定為何，
                '照理來說必須要從頭到尾類別ok直接塞值才對。

                '範例 :
                '另一種方式但無法在裡面再塞一個list
                'Dim items As New Dictionary(Of String, Object)
                'With items
                '    .Add("name", "")
                '    .Add("values", "")
                'End With
                '★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★

                '幾筆值就add幾次這要寫迴圈
                Dim obj = New YahooShoppingSCM.JSONobj
                obj.applicant = "孫協志"
                obj.reviewStatus = "draft"
                obj.subStationId = "sub1"
                obj.type = "newListing"

                obj.listing = New YahooShoppingSCM.Listing
                With obj.listing
                    .catItemId = "catItem10070"
                    .deliveryType = "normal"
                    .price = "100.00"
                    .seoUrl = "我是商品名稱"
                End With

                obj.product = New YahooShoppingSCM.Product
                With obj.product
                    .attributeDisplayMode = "table"
                    .catItemId = "catItem10070"
                    .contentRating = "G"
                    .cost = "50.00"
                    .msrp = "100.00"
                    .name = "(即期品)我是商品名稱"
                    .struDataAttrClusterId = "000002836363"
                    .copy = "<p>內容圖片與影片需經過驗證為有效url才可提交</p>"
                End With

                '幾筆值就add幾次這要寫迴圈
                obj.product.attributes() = New List(Of YahooShoppingSCM.Attribute)
                With obj.product.attributes()

                    Dim items As YahooShoppingSCM.Attribute = New YahooShoppingSCM.Attribute
                    items.name = "記憶體插槽"
                    items.values = New List(Of String)
                    '幾筆值就add幾次這要寫迴圈
                    items.values.Add(4)
                    obj.product.attributes().Add(items)

                End With

                With obj.product.attributes()

                    Dim items As YahooShoppingSCM.Attribute = New YahooShoppingSCM.Attribute
                    items.name = "CPU類型"
                    items.values = New List(Of String)
                    '幾筆值就add幾次這要寫迴圈
                    items.values.Add("i5")
                    items.values.Add(2)
                    obj.product.attributes().Add(items)

                End With

                With obj.product
                    '很詭異的寫法做完一包後在塞進類別內，原來那一包類別檔呼叫不到子類
                    '不知為何類別不能直接繼承某個物件集合(物件集合內有n個物件)
                    '就算類別屬性 Shared 也呼叫不到 ...
                    '最後是因為發現物件內的物件並非物件集合所以只有一筆資料所以跳過
                    '參考 : https://docs.microsoft.com/zh-tw/office/vba/language/reference/user-interface-help/dictionary-object
                    .shipType = New YahooShoppingSCM.Shiptype
                    .shipType.id = "1"
                End With

                '★★★★★ 第一層 ★★★★★

                obj.product.models() = New List(Of YahooShoppingSCM.Model)
                '幾筆值就add幾次這要寫迴圈
                With obj.product.models()

                    Dim model As YahooShoppingSCM.Model = New YahooShoppingSCM.Model
                    model.displayName = "極致簡約Dell2019"

                    '★★★★★★★★★★ items start ★★★★★★★★★★

                    '★★★★★ 第二層 ★★★★★
                    '幾筆值就add幾次這要寫迴圈
                    model.items = New List(Of YahooShoppingSCM.Item)
                    Dim item As YahooShoppingSCM.Item = New YahooShoppingSCM.Item
                    item.displayName = "可以自定義"

                    '★★★★★ 第三層 ★★★★★
                    '幾筆值就add幾次這要寫迴圈
                    item.spec = New YahooShoppingSCM.Spec1
                    item.spec.name = "顏色"
                    item.spec.values = New List(Of String)
                    '幾筆值就add幾次這要寫迴圈
                    item.spec.values.Add("灰色")
                    model.items.Add(item)

                    '★★★★★ 第二層 ★★★★★
                    '幾筆值就add幾次這要寫迴圈
                    Dim item2 As YahooShoppingSCM.Item = New YahooShoppingSCM.Item
                    item2.displayName = "可以自定義2"

                    '★★★★★ 第三層 ★★★★★
                    '幾筆值就add幾次這要寫迴圈
                    item2.spec = New YahooShoppingSCM.Spec1
                    item2.spec.name = "顏色2"
                    item2.spec.values = New List(Of String)
                    '幾筆值就add幾次這要寫迴圈
                    item2.spec.values.Add("灰色2")
                    model.items.Add(item2)

                    '★★★★★★★★★★ items end ★★★★★★★★★★

                    '★★★★★★★★★★ videos start ★★★★★★★★★★
                    '不傳影像
                    'Dim Video As YahooShoppingSCM.Video = New YahooShoppingSCM.Video
                    'Video.order = "1"
                    'Video.url = ""
                    'model.videos = New List(Of YahooShoppingSCM.Video)
                    'model.videos.Add(Video)
                    '★★★★★★★★★★ videos end ★★★★★★★★★★

                    '★★★★★★★★★★ Images start ★★★★★★★★★★
                    '只傳一份所以不寫迴圈
                    Dim Image As YahooShoppingSCM.Image = New YahooShoppingSCM.Image
                    Image.order = "1"
                    Image.url = ""
                    model.images = New List(Of YahooShoppingSCM.Image)
                    model.images.Add(Image)
                    '必填兩張圖，不確定要傳哪個先傳重複的上去
                    model.images.Add(Image)
                    '★★★★★★★★★★ Images end ★★★★★★★★★★

                    '★★★★★★★★★★ spec start ★★★★★★★★★★
                    Dim Spec As YahooShoppingSCM.Spec = New YahooShoppingSCM.Spec
                    Spec.name = "品牌"
                    Spec.values = New List(Of String)
                    '幾筆值就add幾次這要寫迴圈
                    Spec.values.Add("Dell戴爾")
                    Spec.values.Add("Msi微星")
                    model.spec = New YahooShoppingSCM.Spec
                    model.spec = Spec
                    '★★★★★★★★★★ spec end ★★★★★★★★★★

                    .Add(model)
                End With

                obj.product.shortDescription = New List(Of String)
                '幾筆值就add幾次這要寫迴圈
                obj.product.shortDescription.Add("我是簡短說明")
                obj.product.shortDescription.Add("我是簡短說明2")


                obj.product.specs = New List(Of YahooShoppingSCM.Spec2)
                '幾筆值就add幾次這要寫迴圈
                Dim Spec2 As YahooShoppingSCM.Spec2 = New YahooShoppingSCM.Spec2
                Spec2.name = "品牌"
                Spec2.level = "1"
                obj.product.specs.Add(Spec2)

                Dim Spec3 As YahooShoppingSCM.Spec2 = New YahooShoppingSCM.Spec2
                Spec3.name = "顏色"
                Spec3.level = "2"
                obj.product.specs.Add(Spec3)


                '完成上述結構化迴圈後將obj序列化為api所需json
                Dim test_JSONobj As String = Newtonsoft.Json.JsonConvert.SerializeObject(obj)

                '測試test與ex差異

                Dim ex_JSONobj As String =
            <A>{
  "applicant": "孫協志",
  "product": {
    "attributeDisplayMode": "table",
    "attributes": [
    	{"name": "記憶體插槽",
        "values": [
          "4"
        ]},
        {"name": "光碟機",
        "values": [
          "DVD燒錄機"
        ]},
        {"name": "記憶體類型",
        "values": [
          "DDR3"
        ]},
        {"name": "網路攝影機",
        "values": [
          "自訂"
        ]},
        {"name": "無線網路",
        "values": [
          "自訂"
        ]},
        {"name": "藍牙",
        "values": [
          "3.0"
        ]},
        {"name": "I/O連接埠",
        "values": [
          "自訂"
        ]},
        {"name": "顯示卡記憶體容量(GB)",
        "values": [
          "1G"
        ]},
        {"name": "定位",
        "values": [
          "文書機"
        ]},
        {"name": "型號",
        "values": [
          "自訂"
        ]},
        {"name": "記憶體最高支援容量",
        "values": [
          "4G"
        ]},
         {"name": "電池容量(cell/mAh.)",
        "values": [
          "自訂"
        ]},
        {"name": "保固",
        "values": [
          "6個月"
        ]},
        {"name": "指紋辨識",
        "values": [
          "有指紋辨識"
        ]},
        {"name": "螢幕解析度",
        "values": [
          "HD+"
        ]},
         {"name": "螢幕尺寸(吋)",
        "values": [
          "10.1吋"
        ]},
         {"name": "記憶體容量",
        "values": [
          "4G"
        ]},
        {"name": "螢幕類型",
        "values": [
          "鏡面"
        ]},
        {"name": "固態硬碟 SSD/eMMC",
        "values": [
          "8GB"
        ]},
        {"name": "螢幕觸控",
        "values": [
          "有螢幕觸控"
        ]},
          {"name": "中央處理器型號",
        "values": [
          "AMD"
        ]},
        {"name": "重量(kg)",
        "values": [
          "自定義"
        ]},
        {"name": "螢幕解析度類型",
        "values": [
          "HD+"
        ]},
         {"name": "標準配件",
        "values": [
          "滑鼠"
        ]},
        {"name": "顯示晶片卡型號",
        "values": [
          "P4200"
        ]},
        {"name": "2.5吋傳統硬碟轉數",
        "values": [
          "無" 
        ]},
        {"name": "作業系統",
        "values": [
          "Win7 home"
        ]},
        {"name": "其他連接埠",
        "values": [
          "自定義"
        ]},
        {"name": "中央處理器品牌",
        "values": [
          "Intel"
        ]},
        {"name": "CPU類型",
        "values": [
          "i5"
        ]},
        {"name": "硬碟類型",
        "values": [
          "固態硬碟"
        ]},
        {"name": "顯示卡記憶體類型",
        "values": [
          "DDR5"
        ]},
        {"name": "顯示卡類型",
        "values": [
          "內顯"
        ]},
        {"name": "2.5吋傳統硬碟容量",
        "values": [
          "無"
        ]},
        {"name": "讀卡機",
        "values": [
          "自定義"
        ]}
    ],
    "catItemId": "catItem10070",
    "contentRating": "G",
    "cost": "1450",
    "models": [
      {
        "items": [
          {
            "spec": {
              "name": "顏色",
              "values": [
                "金色系"
              ]
            },
            "displayName": "可以自定義"
          },
          {
            "spec": {
              "name": "顏色",
              "values": [
                "灰色系"
              ]
            },
            "displayName": "灰色"
          }
        ],
        "videos": [],
        "images": [],
        "spec": {
          "name": "品牌",
          "values": [
            "Dell戴爾"
          ]
        },
        "displayName": "極致簡約Dell2019"
      },
      {
        "spec": {
          "name": "品牌",
          "values": [
            "AVITA 美國品牌"
          ]
        },
        "items": [
          {
            "spec": {
              "name": "顏色",
              "values": [
                "金色系"
              ]
            },
            "displayName": "自定義"
          },
          {
            "spec": {
              "name": "顏色",
              "values": [
                "灰色系"
              ]
            },
            "displayName": "灰色"
          }
        ],
        "videos": [],
        "images": [],
        "displayName": "低調奢華hp讚"
      }
    ],
    "msrp": "100.00",
    "name": "(即期品)我是商品名稱",

    "shipType": {
      "id": 1
    },
    "shortDescription": [
      "我是簡短說明",
      "我是簡短說明2"
    ],
    "struDataAttrClusterId": "000002836363",
    "copy": "<p>內容圖片與影片需經過驗證為有效url才可提交</p>",

    "specs": [
      {
        "level": 1,
        "name": "品牌"
      },
      {
        "level": 2,
        "name": "顏色"
      }
    ]
  },
  "reviewStatus": "draft",
  "subStationId": "sub1",
  "type": "newListing",
  "listing": {
    "catItemId": "catItem10070",
    "deliveryType": "normal",
    "price": "100.00",
    "seoUrl": "我是商品名稱"
  }
}
</A>

                Dim Match As Boolean = ex_JSONobj Like test_JSONobj

            End Function

        End Class
#End Region





    '依雅虎購物中心api_sign in_規格需求加密
    ''' <summary>
    ''' 取得PostMan測試資料{timestamp, signature}
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Function Get_temporarily_PostManData()

        'HTTP_request需求包含4個 header (api-token、api-version、api-timesta、api-signture)與 Credential密文(加密後的cookie)
        '雅虎購物中心API_KEY查詢 : https://scm.monday.com.tw/ApprovalForm/Query/ApiKeyQuery.aspx

        'request實際欄位名稱為 api-timestamp 為Request發送當下的UnixTimestamp，例如1548225833，此Timestamp的有效期限為90秒
        Dim timestamp As String = Int((DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds + 0.5) '1573455721 
        'request實際欄位名稱為 api-token 為API Key查詢頁提供之Token
        Dim token As String = "Supplier_478"
        '供應商編號
        Dim supplierId As Integer = 478
        '為API Key查詢頁提供之KeyValue
        Dim secretKey As String = "yrsW8ukCXsEZOkKmegF7T2WrK0MTq9U2jH3DmSIod24="
        '為API Key查詢頁提供之KeyIV
        Dim iv As String = "MmThDVTcmRpN5jFgzxb+nQ=="
        '為API Key查詢頁提供之SaltKey
        Dim saltKey As String = "doTzTL6Ev0ttm2OF3kxpzloGTtxky1eI"
        'request實際欄位名稱為 api-Version 為API Key查詢頁提供之Version
        Dim Version As String = "1"

        '***** checked *****

        '***** Credential Plaintext 憑證文本 *****

        Dim cookie As JObject = New Newtonsoft.Json.Linq.JObject
        cookie.Add("supplierId", supplierId)
        'Dim plaintext As String = cookie.ToString
        Dim plaintext As String = "{" + Chr(34) + "supplierId" + Chr(34) + ":478}"

        '***** checked *****

        '***** Credential Signature 憑證密文 *****
        '參考文章 : https://stackoverflow.com/questions/5987186/aes-encrypt-string-in-vb-net
        '參考文章 : https://dotblogs.com.tw/yc421206/archive/2012/04/18/71609.aspx
        '參考文章 : https://stackoverflow.com/questions/723129/using-aescryptoserviceprovider-in-vb-net
        '參考文章 : https://stackoverflow.com/questions/3962900/how-do-i-encrypt-a-string-in-vb-net-using-rijndaelmanaged-and-using-pkcs5-paddi
        'AES / CBC / PKCS5Padding 加密方式
        '此步驟使用到 supplierId / secretKey / iv

        Dim aes As AesCryptoServiceProvider = New AesCryptoServiceProvider()

        aes.Key = Convert.FromBase64String(secretKey)
        aes.IV = Convert.FromBase64String(iv)
        aes.Mode = CipherMode.CBC
        aes.Padding = PaddingMode.PKCS7

        'Dim encryptor = aes.CreateEncryptor()
        'Dim encrypted = encryptor.TransformFinalBlock(Encoding.UTF8.GetBytes(plaintext), 0, Encoding.UTF8.GetBytes(plaintext).Length)
        'aes.Clear()
        Dim ms As MemoryStream = New MemoryStream()
        Dim cs As CryptoStream = New CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write)
        cs.Write(Encoding.UTF8.GetBytes(plaintext), 0, Encoding.UTF8.GetBytes(plaintext).Length)
        cs.FlushFinalBlock()
        Dim encrypted = ms.ToArray()

        Dim ciphertext As String = ""
        ciphertext = Convert.ToBase64String(encrypted)

        '***** checked *****

        '***** Credential Signature 憑證簽章 *****
        '參考文章 : https://kknews.cc/zh-tw/code/33bzv9y.html
        'hmacsha512 加密方式
        '此步驟使用到 timestamp / token / saltKey / secretKey

        Dim signatureSource As String = String.Format("{0}{1}{2}{3}", timestamp, token, saltKey, ciphertext)

        Dim utf8 As UTF8Encoding = New UTF8Encoding()
        Dim keyByte As Byte() = utf8.GetBytes(secretKey)
        Dim HMACSHA512 As HMACSHA512 = New HMACSHA512(keyByte)
        Dim messageBytes As Byte() = utf8.GetBytes(signatureSource)
        Dim hashmessage As Byte() = HMACSHA512.ComputeHash(messageBytes)

        Dim builder As New StringBuilder(hashmessage.Length * 2)
        For Each data As Byte In hashmessage
            builder.Append(Convert.ToString(data, 16).PadLeft(2, "0"c).PadRight(2, " "c))
        Next

        Dim metadata As Byte() = Encoding.UTF8.GetBytes(builder.ToString)

        Dim signaturebyte As Byte() = Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding("iso-8859-1"), metadata)
        Dim signature As String = System.Text.Encoding.Default.GetString(signaturebyte)



        Return {timestamp, signature}

    End Function

























#End Region
End Class



