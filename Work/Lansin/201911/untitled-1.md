# 20191115

精技雅虎購物中心 api 欄位確認 後台input功能easyui頁面新增

商品提報json object 建置類別檔  確認與ec list 差異



和秉霖大講了

其實最主要的問題應該就是

公司走向與生涯規劃不相符

雖然環境ok但是上述就最重要的不相符那也無可奈何 ...

![](../.gitbook/assets/image%20%2818%29.png)

```text
#Region "YahooShoppingSCM_API_request (header_加密取得_sp cookie)"
    '依雅虎購物中心api_sign in_規格需求加密
    Public Function Get_YSSCM_CookieCollection()

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
    Public Function Get_YSSCM_WSSID_token(CookieCollection As CookieCollection)

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
    Public Function Get_YSSCM_API_Response(Method As String, URL As String, JSONString As String)

        '取得雅虎購物中心驗證cookie與wssid
        Dim CookieCollection As CookieCollection = Get_YSSCM_CookieCollection()
        Dim WSSID As String = Get_YSSCM_WSSID_token(CookieCollection)
        '***** 將cookie與wssid塞入封包 *****
        '實作 HttpWebRequest
        Dim request As HttpWebRequest = WebRequest.Create(URL) '丟入函數的URL參數
        request.CookieContainer = New CookieContainer()
        request.CookieContainer.Add(CookieCollection)
        request.Headers.Add("X-YahooWSSID-Authorization", WSSID)
        request.Method = Method '丟入函數的Method參數
        '禁止轉址
        request.AllowAutoRedirect = False
        '內容轉為資料流
        If JSONString <> "" Or JSONString <> Nothing Then
            Dim st As Stream = request.GetRequestStream()
            Dim byteArray As Byte() = Encoding.Default.GetBytes(JSONString)
            st.Write(byteArray, 0, byteArray.Length)
        End If
        '執行HttpWebRequest
        Try

            Dim response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
            '回傳資料解碼
            Dim reader = New System.IO.StreamReader(response.GetResponseStream(), ASCIIEncoding.UTF8)
            Dim responseText As String = reader.ReadToEnd()
            'Dim Obj As Newtonsoft.Json.Linq.JObject = Newtonsoft.Json.JsonConvert.DeserializeObject(responseText)
            '釋放response
            response.Close()
            '回傳API JObject
            Return responseText

        Catch ex As Exception
            Return ex.ToString
        End Try


    End Function

#End Region

#Region "YahooShoppingSCM_API_request (URL TEST)"
    ''' <summary>
    ''' 因精技需求分類優先串接之API
    ''' (註:無做錯誤處理與反饋)
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub API_URL_TEST()

        '雅虎API URL TEST查詢值在Yahoo Shopping SCM API 文件上
        Dim v1 As String = "https://tw.supplier.yahoo.com/api/spa/v1/"

        'GET Yahoo Shopping SCM API
        '查詢使用者資訊
        Dim B As String = v1 + "user" '暫不使用
        Dim C As String = v1 + "user?fields=-profile" '暫不使用
        '查詢雅虎服務窗口
        Dim D As String = v1 + "serviceDesks?type=proposal" '暫不使用
        '查詢類別
        Dim E As String = v1 + "categories?categoryId=cat13886&fields=children,parents" 'ok
        '查詢結構化數據屬性集群
        Dim F As String = v1 + "struDataAttrClusters?categoryId=catItem5566&proposalType=newListing" '暫不使用
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
        Dim Z As String = Get_YSSCM_API_Response("Get", E, "")

    End Sub
#End Region

#Region "隱藏其他頁籤"
    '因精技需求改寫yahooapi2為串yahoo購物商城，隱藏部分未改寫功能。
    Public Sub HideTabPage()

        Me.TabPage_OnlineOffline.Parent = Nothing
        Me.TabPage_UpdatePrice.Parent = Nothing
        Me.TabPage_UpdateMain.Parent = Nothing
        Me.TabPage_stock.Parent = Nothing
        Me.TabPage_ReUploadImage.Parent = Nothing
        Me.TabPage_RecProcStatus.Parent = Nothing
        'Me.TabPage_DBSetting.Parent = Nothing
        'Me.TabPage_TestAPI.Parent = Nothing

        GET_categories_API()
        'API_URL_TEST()



    End Sub
#End Region

#Region "GET YahooShoppingSCM_categories API"
    ''' <summary>
    ''' 查詢類別並存入
    ''' 表名稱:"YahooshoppingSCM_API_Category" 、 位址:10.0.11.1 (LS3C_V2_2005)
    ''' (註:無做錯誤處理與反饋)
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GET_categories_API()

        '雅虎API URL TEST查詢值在Yahoo Shopping SCM API 文件上
        Dim v1 As String = "https://tw.supplier.yahoo.com/api/spa/v1/"
        '精技提案類別項目 Max 50個超過不知道會怎樣文件沒寫 ，頂層有一個大類 z1，但呼叫了回傳空值
        Dim subs As String = "sub1,sub2,sub15,sub17,sub22,sub24,sub26,sub27,sub34,sub36,sub38,sub51,sub52,sub55,sub107,sub112,sub121,sub168,sub197,sub215,sub408,sub431,sub436,sub453,sub454,sub455,sub469,sub583,sub617,sub681,sub682,sub693,sub695,sub696,sub704,sub788,sub792,sub793,sub794,sub810,sub812"
        '查詢類別
        Dim E As String = v1 + "categories?categoryId=" + subs + "&fields=children,parents" 'ok
        '呼叫API
        Dim Z As String = Get_YSSCM_API_Response("Get", E, "")
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
        dt.Columns.Add("categoryId")
        dt.Columns.Add("categoryIdname")
        dt.Columns.Add("catItemId")
        dt.Columns.Add("catItemIdname")
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
                Dim XX As String = Get_YSSCM_API_Response("Get", X, "")
                '轉為JOBJECT
                Dim ObjX As Newtonsoft.Json.Linq.JObject = Newtonsoft.Json.JsonConvert.DeserializeObject(XX)


                '第三層catItem
                For number3 As Int32 = 0 To ObjX("children").Count - 1 Step 1

                    '第一次呼叫API
                    newRow = dt.NewRow
                    newRow(0) = Obj("categories")(number).Item("categoryId").ToString() 'subs
                    newRow(1) = Obj("categories")(number).Item("name").ToString() 'subname
                    newRow(2) = Obj("categories")(number).Item("childrenIdList")(number2).ToString() 'categoryId 

                    '第二次呼叫API
                    newRow(3) = ObjX("categories")(0).Item("name").ToString() 'categoryIdname 
                    '第三層catItem
                    newRow(4) = ObjX("children")(number3).Item("categoryId").ToString()
                    newRow(5) = ObjX("children")(number3).Item("name").ToString()
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

End Class

#Region "YahooShoppingSCM_proposals 提報架構類別"

''' <summary>
''' YahooShoppingSCM_proposals 提報架構
''' (註:部分欄位指定值須比照線上文件)
''' https://docs.google.com/document/d/1-ROog-SSxvCIKkWRfcjadtFmP_gnL4PkqbR7TfvR5Cg/edit?pli=1
''' </summary>
''' <remarks></remarks>
Public Class YahooShoppingSCM_proposals
#Region "object"
    ''' <summary>
    ''' YahooShoppingSCM_proposals 提報架構類別
    ''' </summary>
    Public Class Rootobject

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
        ''' <summary>
        ''' 備註，限 200 個字
        ''' 
        ''' ex : 我是備註
        ''' </summary>
        Public Property note As String
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
        Public Property attributes() As Attribute

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

        ''' <summary>
        ''' 是否為即期品 (必填●
        ''' 配送方式為`快速到貨`且有填寫商品保存期限時方可為 true
        ''' 預設為 false
        ''' 
        ''' ex : true
        ''' </summary>
        Public Property isExpiringItem As Boolean

        ''' <summary>
        ''' 是否需要安裝
        ''' 配送方式為`快速到貨`且附約資訊包含 appliance 時方可填寫
        ''' 預設為 false
        ''' 
        ''' ex : true
        ''' </summary>
        Public Property isInstallRequired As Boolean

        ''' <summary>
        ''' 是否為大材積商品 (即將棄用)
        ''' (舊) 配送方式為`快速到貨`且附約資訊包含 appliance 時方可為 true
        ''' (新) POST/PUT 皆應為 null
        ''' 預設為 false
        ''' 
        ''' ex : true
        ''' </summary>
        Public Property isLargeVolume As Boolean

        ''' <summary>
        ''' 是否為大型商品附屬贈品
        ''' 配送方式為`快速到貨`且附約資訊包含 appliance 時方可填寫
        ''' 預設為 false
        ''' 
        ''' ex : true
        ''' </summary>
        Public Property isLargeVolumnProductGift As Boolean

        ''' <summary>
        ''' 是否屬於廢四機
        ''' 供應商啟用功能包含 recycleAppliance 時可選
        ''' 
        ''' ex : true
        ''' </summary>
        Public Property isNeedRecycle As Boolean

        ''' <summary>
        ''' 是否為買斷商品
        ''' 配送方式為`快速到貨`且附約資訊包含 outrightPurchase 時方可填寫
        ''' 預設為 false
        ''' 
        ''' ex : true
        ''' </summary>
        Public Property isOutrightPurchase As Boolean

        ''' <summary>
        ''' 最小包裝數
        ''' range: [1-32767]
        ''' 配送方式為`快速到貨`時才能更改，其餘只能為 1
        ''' 預設為 1
        ''' 
        ''' ex : 10
        ''' </summary>
        Public Property minPackingCount As Integer

        ''' <summary>
        ''' 屬性商品型號 (不一定要給
        ''' proposal type = 14 (updateVideo) 且非無屬性賣場時須提供 (必填●
        ''' </summary>
        Public Property models() As Model

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

        ''' <summary>
        ''' 主件商品供應商商品料號，最長 40 個字
        ''' 
        ''' 12345
        ''' </summary>
        Public Property partNo As String

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
        Public Property shortDescription() As String

        ''' <summary>
        ''' 結構化資料屬性集 ID (必填●
        ''' 
        ''' ex : 000003326689
        ''' </summary>
        Public Property struDataAttrClusterId As String


        ''' <summary>
        ''' 商品保證 (必填●
        ''' </summary>
        Public Property warranty As Warranty

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

        ''' <summary>
        ''' 品牌
        ''' 最長 20 個字
        ''' 
        ''' ex : 我是品牌
        ''' </summary>
        Public Property brand As String

        ''' <summary>
        ''' 包裝完成後的商品高度
        ''' 單位為 cm，需為正整數
        ''' 若為配送方式為`快速到貨`或`直店配`時必填 (必填●
        ''' max 9999
        ''' 
        ''' ex : 77
        ''' </summary>
        Public Property height As Integer

        ''' <summary>
        ''' 包裝完成後的商品長度
        ''' 單位為 cm，需為正整數
        ''' 若為配送方式為`快速到貨`或`直店配`時必填 (必填●
        ''' max 9999
        ''' 
        ''' ex : 55
        ''' </summary>
        Public Property length As Integer

        ''' <summary>
        ''' 商品型號
        ''' 最長 20 個字
        ''' 
        ''' ex : 我是商品型號
        ''' </summary>
        Public Property model As String
        ''' <summary>
        ''' 商品保存期限
        ''' 單位為天
        ''' range: [1-32767]
        ''' 配送方式shiptype為`快速到貨`可填，否則應為 null
        ''' 
        ''' ex : 56
        ''' </summary>
        Public Property preserveDays As Integer
        ''' <summary>
        ''' 包裝完成後的商品寬度
        ''' 單位為 cm，需為正整數
        ''' 若為配送方式為`快速到貨`或`直店配`時必填
        ''' max 9999
        ''' 
        ''' ex : 88
        ''' </summary>
        Public Property weight As Integer
        ''' <summary>
        ''' 包裝完成後的商品重量
        ''' 單位為 g，需為正整數
        ''' 若為配送方式為`快速到貨`或`直店配時`必填
        ''' max 2147483647
        ''' 
        ''' ex : 66
        ''' </summary>
        Public Property width As Integer
        ''' <summary>
        ''' 商品屬性 (必填●        ''' </summary>
        Public Property specs() As Spec2
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
        Public Property values() As String
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
        Public Property items() As Item
        ''' <summary>
        ''' 商品影片 (必填●
        ''' </summary>
        Public Property videos() As Video
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
        Public Property images() As Image
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
        Public Property spec As Spec
        ''' <summary>
        ''' 賣場顯示名稱，預設為規格名稱 (必填●
        ''' 若為無屬性商品時為空, 有 1 層及以上屬性時可填
        ''' 
        ''' ex : 極致簡約Dell2019
        ''' </summary>
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
        Public Property name As String
        ''' <summary>
        ''' ex : Dell戴爾
        ''' ex : hp惠普
        ''' </summary>
        Public Property values() As String
    End Class

#End Region
#Region "object(Product)(Models)(Items)"
    ''' <summary>
    ''' 屬性值 (必填●
    ''' </summary>
    Public Class Item
        ''' <summary>
        ''' 屬性商品供應商商品料號
        ''' 最長 40 個字
        ''' 
        ''' ex : 5566
        ''' </summary>
        Public Property partNo As String
        ''' <summary>
        ''' 實際國際條碼
        ''' 限定為13碼或12碼 (12碼由 API 在前面補 0 )
        ''' 若為下列配送方式 (shipType)時則可填寫
        ''' 	1 (宅配)
        '''     61 (集宅配)
        '''     800 (直店配)
        ''' </summary>
        Public Property barcode As String
        ''' <summary>
        ''' 進倉用國際條碼
        ''' 限定為13碼或12碼 (12碼由 API 在前面補 0 )
        ''' 
        ''' ex : 725272730706
        ''' </summary>
        Public Property warehouseBarcode As String
        ''' <summary>
        ''' 第 2 層屬性的 spec
        ''' 需為 ProposalProductSpec 中 level=2 的 spec
        ''' 若為無屬性與 1 層屬性商品時為空, 為 2 層屬性商品時為必填
        ''' 
        ''' ex : 9785109946480
        ''' </summary>
        Public Property spec As Spec1
        ''' <summary>
        ''' 賣場顯示名稱
        ''' 預設為規格名稱
        ''' 若為無屬性與 1 層屬性商品時為空, 為 2 層屬性商品時可填
        ''' 
        ''' ex : 卡其色
        ''' </summary>
        Public Property displayName As String
    End Class

#End Region
#Region "object(Product)(Models)(Item)(Spec)"
    Public Class Spec1
        ''' <summary>
        ''' ex : 顏色        ''' </summary>
        Public Property name As String
        ''' <summary>
        ''' ex : 卡其        ''' </summary>
        Public Property values() As String
    End Class
#End Region
#Region "object(Product)(Models)(Video)"
    ''' <summary>
    ''' 商品影片 (必填●
    ''' </summary>
    Public Class Video
        ''' <summary>
        ''' 影片 url
        ''' 若 FQDN 為 {s|ct}.yimg.com 或 edgecast-vod.yahoo.net，必需為 https 協定
        ''' 目前支援的 Video Codecs 請見列表 
        ''' https://docs.aws.amazon.com/en_us/mediaconvert/latest/ug/reference-codecs-containers-input.html
        ''' 
        ''' ex :
        ''' https://s.yimg.com/bp/Files/374d9974ab2cbce382e42724fede7aa07313cae6.qt
        ''' </summary>
        Public Property url As String
        ''' <summary>
        ''' 排列
        ''' 
        ''' ex : 1
        ''' </summary>
        Public Property order As Integer
    End Class

#End Region
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
        Public Property url As String
        ''' <summary>
        ''' 排列
        ''' starts from 1
        ''' 
        ''' ex : 1
        ''' </summary>
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
        Public Property id As Integer
    End Class

#End Region

#Region "object(Product)(Warranty)"

    ''' <summary>
    ''' 商品保證 (必填●
    ''' </summary>
    Public Class Warranty

        ''' <summary>
        ''' 保固期限 (必填●
        ''' 如果內容值為`無保固`則保固範圍及保固來源可不填，反之若為其他值則必填
        ''' 最長 20 個字
        ''' 
        ''' ex : 一個月
        ''' </summary>
        Public Property period As String

        ''' <summary>
        ''' 說明訊息整段描述
        ''' 最長 800 個字
        ''' 
        ''' ex : 保固說明文字
        ''' </summary>
        Public Property description As String

        ''' <summary>
        ''' 保固來源
        ''' none: 無保固
        ''' official: 原廠保固
        ''' retailer: 經銷保固
        ''' 若保固期限為`無保固`，只能為`none` (若有輸入會被取代)
        ''' 若保固期限不為`無保固`，預設為`official`
        ''' 
        ''' ex : official
        ''' </summary>
        Public Property handler As String
        ''' <summary>
        ''' 保固範圍
        ''' 最長 20 個字
        ''' 若保固期限為`無保固`，自動填入`無保固`
        ''' 
        ''' ex : 新品瑕疵
        ''' </summary>
        Public Property scope As String
    End Class
#End Region

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
        Public Property level As Integer
        ''' <summary>
        ''' 屬性名稱最長 30 個字 (必填●
        ''' 
        ''' exe : 品牌
        ''' exe : 顏色
        ''' </summary>
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
        Public Property deliveryType As String
        ''' <summary>
        ''' 特色標題，最長 20 個字
        ''' 
        ''' ex : 我是特色標題
        ''' </summary>
        Public Property featureTitle As String '特色標題，最長 20 個字
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
        Public Property price As String
        ''' <summary>
        ''' 賣場網址，最長 25 個字
        ''' 只接受正/簡體中文、英文、數字、`-`，多個 `-` 會合併成一個，開頭不能是 `-`
        ''' 提案一般賣場且 reviewStatus 為 draft 以上時必填(必填●
        ''' 
        ''' ex : 我是商品名稱
        ''' </summary>
        Public Property seoUrl As String
    End Class
#End Region

End Class
#End Region
```

```text
#Region "YahooShoppingSCM_API_request (URL TEST)"
    ''' <summary>
    ''' 因精技需求分類優先串接之API
    ''' (註:無做錯誤處理與反饋)
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub API_URL_TEST()

        '雅虎API URL TEST查詢值在Yahoo Shopping SCM API 文件上
        Dim v1 As String = "https://tw.supplier.yahoo.com/api/spa/v1/"

        'GET Yahoo Shopping SCM API
        '查詢使用者資訊
        Dim B As String = v1 + "user" '暫不使用
        Dim C As String = v1 + "user?fields=-profile" '暫不使用
        '查詢雅虎服務窗口
        Dim D As String = v1 + "serviceDesks?type=proposal" '暫不使用
        '查詢類別
        Dim E As String = v1 + "categories?categoryId=cat13886&fields=children,parents" 'ok
        '查詢結構化數據屬性集群
        Dim F As String = v1 + "struDataAttrClusters?categoryId=catItem5566&proposalType=newListing" '暫不使用
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
        'https://docs.google.com/document/d/1HqPLzfY9XbgnqlGpnhKe04kWoKE0KDUXd4xdNykDI2s/edit
        '修改賣場影片(有屬性)
        'https://docs.google.com/document/d/1eekPRUkLJe6bROHcAF0WqsWAB0ETEwiMfZ48Oah03w0/edit
        '修改賣場影片(無屬性)
        'https://docs.google.com/document/d/15rp85S6iSyUn106okz87ULoIt2AxQr51fS1P2PklLlo/edit

        '檔案上傳流程 取得臨時憑證獲取token將檔案上傳至雅虎購物中心提供之aws
        'https://aws.amazon.com/getting-started/tools-sdks/
        '上傳成功後由response header 取得ETag 透過GET /fileObhects/{ETag} 取得該檔案URL
        Dim J As String = "https://tw.buy.yahoo.com/api/fileUploader/v1/credentials" '暫不使用
        '查詢檔案上傳臨時憑證token
        Dim K As String = "https://tw.buy.yahoo.com/api/fileUploader/v1/fileObjects/{ETag}" '暫不使用
        '查詢上傳檔案url

        '''''''測試''''''
        Dim Z As String = Get_YSSCM_API_Response("Get", E, "")

    End Sub
#End Region

#Region "隱藏其他頁籤"
    '因精技需求改寫yahooapi2為串yahoo購物商城，隱藏部分未改寫功能。
    Public Sub HideTabPage()

        Me.TabPage_OnlineOffline.Parent = Nothing
        Me.TabPage_UpdatePrice.Parent = Nothing
        Me.TabPage_UpdateMain.Parent = Nothing
        Me.TabPage_stock.Parent = Nothing
        Me.TabPage_ReUploadImage.Parent = Nothing
        Me.TabPage_RecProcStatus.Parent = Nothing
        'Me.TabPage_DBSetting.Parent = Nothing
        'Me.TabPage_TestAPI.Parent = Nothing

        GET_categories_API()
        'API_URL_TEST()



    End Sub
#End Region

#Region "GET YahooShoppingSCM_categories API"
    ''' <summary>
    ''' 查詢類別並存入
    ''' 表名稱:"YahooshoppingSCM_API_Category" 、 位址:10.0.11.1 (LS3C_V2_2005)
    ''' (註:無做錯誤處理與反饋)
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GET_categories_API()

        '雅虎API URL TEST查詢值在Yahoo Shopping SCM API 文件上
        Dim v1 As String = "https://tw.supplier.yahoo.com/api/spa/v1/"
        '精技提案類別項目 Max 50個超過不知道會怎樣文件沒寫 ，頂層有一個大類 z1，但呼叫了回傳空值
        Dim subs As String = "sub1,sub2,sub15,sub17,sub22,sub24,sub26,sub27,sub34,sub36,sub38,sub51,sub52,sub55,sub107,sub112,sub121,sub168,sub197,sub215,sub408,sub431,sub436,sub453,sub454,sub455,sub469,sub583,sub617,sub681,sub682,sub693,sub695,sub696,sub704,sub788,sub792,sub793,sub794,sub810,sub812"
        '查詢類別
        Dim E As String = v1 + "categories?categoryId=" + subs + "&fields=children,parents" 'ok
        '呼叫API
        Dim Z As String = Get_YSSCM_API_Response("Get", E, "")
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
        dt.Columns.Add("categoryId")
        dt.Columns.Add("categoryIdname")
        dt.Columns.Add("catItemId")
        dt.Columns.Add("catItemIdname")
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
                Dim XX As String = Get_YSSCM_API_Response("Get", X, "")
                '轉為JOBJECT
                Dim ObjX As Newtonsoft.Json.Linq.JObject = Newtonsoft.Json.JsonConvert.DeserializeObject(XX)


                '第三層catItem
                For number3 As Int32 = 0 To ObjX("children").Count - 1 Step 1

                    '第一次呼叫API
                    newRow = dt.NewRow
                    newRow(0) = Obj("categories")(number).Item("categoryId").ToString() 'subs
                    newRow(1) = Obj("categories")(number).Item("name").ToString() 'subname
                    newRow(2) = Obj("categories")(number).Item("childrenIdList")(number2).ToString() 'categoryId 

                    '第二次呼叫API
                    newRow(3) = ObjX("categories")(0).Item("name").ToString() 'categoryIdname 
                    '第三層catItem
                    newRow(4) = ObjX("children")(number3).Item("categoryId").ToString()
                    newRow(5) = ObjX("children")(number3).Item("name").ToString()
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

End Class

#Region "YahooShoppingSCM_proposals 提報架構類別"

''' <summary>
''' YahooShoppingSCM_proposals 提報架構
''' (註:部分欄位指定值須比照線上文件)
''' https://drive.google.com/file/d/1-LtPORLx3x_qRt6rDWiWwlW4Cgg5Grfi/view
''' </summary>
''' <remarks></remarks>
Public Class YahooShoppingSCM_proposals

    Public Class Rootobject '最外層 '''''''''''''''''''''''''' 待確認 哪些是正確需求必填欄位，哪些欄位有固定值
        Public Property applicant As String '申請人 ex : "孫協志"
        Public Property contactWindow As String '聯繫窗口 ex : "kellyyeh"
        Public Property createdTs As Date '創建時間 ex : "2019-03-05T15:34:58+08:00" '''''''''''''''''''''''''' 待確認
        Public Property creator As String '創建人 ex : "mwu02"
        Public Property executeStatus As String '執行狀態 ex : "idle"
        Public Property expiredTs As Date '到期時間 ex : "2019-03-20T00:00:00+08:00" '''''''''''''''''''''''''' 待確認
        Public Property id As Integer 'id ex : 76527
        Public Property listing As Listing '商品清單
        Public Property modifiedTimes As Integer '改動次數 ex : 1 
        Public Property modifiedTs As Date '改動時間 ex : "2019-03-05T15:34:58+08:00" '''''''''''''''''''''''''' 待確認
        Public Property modifier As String '改動人 ex : "mwu02"
        Public Property note As String '備註 ex : "我是備註"
        Public Property product As Product '商品屬性 
        Public Property reviewStatus As String '審核狀態 ex : "draft"
        Public Property subStationId As String '大類ID ex : "sub1"
        Public Property subStationName As String '大類名稱 ex : "筆記型電腦超過十一個字test"
        Public Property supplierId As Integer '供應商ID ex : 4866 '''''''''''''''''''''''''' 待確認
        Public Property supplierName As String '供應商名稱 ex : "興奇雅虎測試"
        Public Property type As String '提報狀態 ex : "newListing"
    End Class

    Public Class Listing '商品清單
        Public Property applyLowGpm As Boolean '?? ex : false '''''''''''''''''''''''''' 待確認
        Public Property catId As String '中類ID ex : "cat430"
        Public Property catItemId As String '小類ID ex : "catItem10070"
        Public Property catItemName As String '小類名稱 ex : "新迅馳Sonoma"
        Public Property catName As String '中類名稱 ex : "加值功能NB推薦"
        Public Property cvsPurchaseQtyLimit As Integer 'csv採購數量限制 ex : 1
        Public Property deliveryType As String '交貨類型 ex : "appointment"   '''''''''''''''''''''''''' 待確認
        Public Property featureTitle As String 'ex : 
        Public Property isDisplay As Boolean 'ex : 
        Public Property offShelvedTs As Date 'ex : 
        Public Property onShelvedTs As Date 'ex : 
        Public Property price As String 'ex : 
        Public Property purchaseQtyLimit As Integer 'ex : 
        Public Property seoUrl As String 'ex : 
        Public Property subStationId As String 'ex : 
        Public Property subStationName As String 'ex : 
        Public Property zoneId As String 'ex : 
        Public Property zoneName As String 'ex : 
    End Class

    Public Class Product '商品屬性
        Public Property attributeDisplayMode As String 'ex : 
        Public Property attributes() As Attribute 'ex : 
        Public Property brand As String 'ex : 
        Public Property catId As String 'ex : 
        Public Property catItemId As String 'ex : 
        Public Property catItemName As String 'ex : 
        Public Property catName As String 'ex : 
        Public Property contentRating As String 'ex : 
        Public Property copy As String 'ex : 
        Public Property cost As String 'ex : 
        Public Property height As Integer 'ex : 
        Public Property isExpiringItem As Boolean 'ex : 
        Public Property isInstallRequired As Boolean 'ex : 
        Public Property isLargeVolume As Boolean 'ex : 
        Public Property isLargeVolumnProductGift As Boolean 'ex : 
        Public Property isNeedRecycle As Boolean 'ex : 
        Public Property isOutrightPurchase As Boolean 'ex : 
        Public Property length As Integer 'ex : 
        Public Property minPackingCount As Integer 'ex : 
        Public Property model As String 'ex : 
        Public Property models() As Model 'ex : 
        Public Property msrp As String 'ex : 
        Public Property name As String 'ex : 
        Public Property partNo As String 'ex : 
        Public Property preserveDays As Integer 'ex : 
        Public Property safeStockQty As Integer 'ex : 
        Public Property shareMediaBetweenModels As Boolean 'ex : 
        Public Property shipType As Shiptype 'ex : 
        Public Property shortDescription() As String 'ex : 
        Public Property specs() As Spec2 'ex : 
        Public Property struDataAttrClusterId As String 'ex : 
        Public Property struDataAttrClusterName As String 'ex : 
        Public Property subStationId As String 'ex : 
        Public Property subStationName As String 'ex : 
        Public Property warranty As Warranty 'ex : 
        Public Property weight As Integer 'ex : 
        Public Property width As Integer 'ex : 
        Public Property zoneId As String 'ex : 
        Public Property zoneName As String 'ex : 
    End Class

    Public Class Shiptype 'ex : 
        Public Property id As Integer 'ex : 
        Public Property name As String 'ex : 
    End Class

    Public Class Warranty 'ex : 
        Public Property description As String 'ex : 
        Public Property handler As String 'ex : 
        Public Property period As String 'ex : 
        Public Property scope As String 'ex : 
    End Class

    Public Class Attribute 'ex : 
        Public Property name As String 'ex : 
        Public Property values() As String 'ex : 
    End Class

    Public Class Model 'ex : 
        Public Property displayName As String 'ex : 
        Public Property images() As Image 'ex : 
        Public Property items() As Item 'ex : 
        Public Property spec As Spec 'ex : 
        Public Property videos() As Video 'ex : 
    End Class

    Public Class Spec 'ex : 
        Public Property name As String 'ex : 
        Public Property values() As String 'ex : 
    End Class

    Public Class Image 'ex : 
        Public Property height As Integer 'ex : 
        Public Property order As Integer 'ex : 
        Public Property url As String 'ex : 
        Public Property width As Integer 'ex : 
    End Class

    Public Class Item 'ex : 
        Public Property barcode As String 'ex : 
        Public Property displayName As String 'ex : 
        Public Property partNo As String 'ex : 
        Public Property spec As Spec1 'ex : 
        Public Property stock As Integer 'ex : 
        Public Property warehouseBarcode As String 'ex : 
    End Class

    Public Class Spec1 'ex : 
        Public Property name As String 'ex : 
        Public Property values() As String 'ex : 
    End Class

    Public Class Video 'ex : 
        Public Property order As Integer 'ex : 
        Public Property url As String 'ex : 
    End Class

    Public Class Spec2 'ex : 
        Public Property level As Integer 'ex : 
        Public Property name As String 'ex : 
    End Class
End Class
#End Region
```

