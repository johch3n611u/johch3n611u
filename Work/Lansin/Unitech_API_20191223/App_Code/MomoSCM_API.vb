'/////////////////////////////////////////////////////////////////////////////////
' 主程式表單
'
' 建檔人員: 育誠
' 版   本: version 1
' 建檔日期: 2019-11-15
' 參考   : App_Code/YahooShoppingSCM_API.vb ( 原檔案註解較詳細
' 修改記錄: 
' 關連程式:
' 呼叫來源:
'/////////////////////////////////////////////////////////////////////////////////
Imports System.Data
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.IO.Compression
Imports System.Net

Public Class MomoSCM_API
    ''' <summary>
    ''' Momo共用參數
    ''' </summary>
    Private Class CommonParameter
        ''' <summary>
        ''' 登入驗證參數
        ''' </summary>
        Public Class loginInfo
            ''' <summary>
            ''' 精技統一編號
            ''' </summary>
            Public Const VATnumber As String = "12215548"
            ''' <summary>
            ''' 精技MomoSCM密碼
            ''' </summary>
            Public Const SCMpassword As String = "1110193"
            ''' <summary>
            ''' 寄售: 貨權在精技，讓momo銷售
            ''' </summary>
            Public Class Consignment
                ''' <summary>
                ''' 銷售方式 : 寄售
                ''' </summary>
                Public Const salesMethods As String = "Consignment"
                ''' <summary>
                ''' 寄售密碼
                ''' </summary>
                Public Const ConsignmentCode As String = "011560"
                ''' <summary>
                ''' 寄售momoOTP序號V000004118後3碼
                ''' </summary>
                Public Const ConsignmentOTPcode As String = "118"
            End Class
            ''' <summary>
            ''' 買斷: 貨權在momo，在momo銷售
            ''' </summary>
            Public Class Buyout
                ''' <summary>
                ''' 銷售方式 : 買斷
                ''' </summary>
                Public Const salesMethods As String = "Buyout"
                ''' <summary>
                ''' 買斷密碼
                ''' </summary>
                Public Const BuyoutCode As String = "014105"
                ''' <summary>
                ''' 買斷momoOTP序號V000006357後3碼
                ''' </summary>
                Public Const BuyoutOTPcode As String = "357"
            End Class

        End Class
        ''' <summary>
        ''' 步驟參數
        ''' </summary>
        Public Class doAction
            ''' <summary>
            ''' 暫存
            ''' </summary>
            Public Const tempReportGoods As String = "tempReportGoods"
            ''' <summary>
            ''' 送審
            ''' </summary>
            Public Const verifyReportGoods As String = "verifyReportGoods"
        End Class

        ''' <summary>
        ''' 精技固定產地編碼
        ''' </summary>
        Public Shared originCode As String = "0086"

        Public Class Message
            ''' <summary>
            ''' 成功字串
            ''' </summary>
            Public Shared successmsg As String
            ''' <summary>
            ''' 錯誤字串
            ''' </summary>
            Public Shared errormsg As String
            ''' <summary>
            ''' 圖檔大小
            ''' </summary>
            Public Shared imglength As String
            ''' <summary>
            '''壓縮檔大小
            ''' </summary>
            Public Shared ziplength As String
        End Class
    End Class

    ''' <summary>
    ''' 資料傳遞至MomoSCM_WebAPI。
    ''' </summary>
    ''' <param name="Method">傳遞方法</param>
    ''' <param name="URL">網址</param>
    ''' <param name="JSONString">JSON傳遞資料</param>
    ''' <returns></returns>
    Public Shared Function Request_MomoSCM_API(Method As String, URL As String, JSONString As String) As String

        '取得雅虎購物中心驗證cookie與wssid
        '***** 將cookie與wssid塞入封包 *****
        '實作 HttpWebRequest
        Dim request As HttpWebRequest = WebRequest.Create(URL) '丟入函數的URL參數
        request.CookieContainer = New CookieContainer()
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

    ''' <summary>
    ''' Momo商品提報。
    ''' <para>相關資料表為MomoshoppingSCM_API_list</para>
    ''' <para>單筆請輸入ProductNo</para>
    ''' <para>多筆請以半形逗號分隔Ex(ProductNo,ProductNo,...</para>
    ''' <para>全部ProductNo請傳空字串或Nothing</para>
    ''' <paramref name="doAction"/> tempReportGoods API暫存 , verifyReportGoods API送審
    ''' </summary> 
    Public Shared Function POST_proposals_API(ByVal ProductNo As String, ByVal doAction As String) As String

        'WebAPI反傳錯誤處理
        Dim ProductNames As String = "" '錯誤商品名稱
        Dim failLists As String = "" '錯誤清單
        Dim totalCnt As Integer  '總筆數
        Dim successCnt As Integer  '成功筆數
        Dim failCnt As Integer '失敗筆數
        Dim dt As New DataTable
        Dim postback = ""

        '查詢提報筆數
        If ProductNo = Nothing Or ProductNo = "" Then
            Dim sql = <sql>
                      SELECT * FROM MomoshoppingSCM_API_list WITH(NOLOCK) WHERE posteddate IS NULL
                  </sql>
            dt = EC.DB.ExecuteDataTable(sql)
        Else
            Dim sql As String = <sql>SELECT * FROM MomoshoppingSCM_API_list with(NOLOCK) WHERE ProductNo ='{0}' AND posteddate IS NULL</sql>
            sql = String.Format(sql, ProductNo)
            dt = EC.DB.ExecuteDataTable(sql)
        End If

        If dt.Rows.Count <> 0 Then

            For num As Integer = 0 To dt.Rows.Count - 1 Step 1

                If ProductNo = Nothing Or ProductNo = "" Then
                    ProductNo = dt.Rows.Item(num).Item("ProductNo").ToString
                End If

                '所需資料表
                Dim ecsql As String = <sql>
                      SELECT * FROM list WITH(NOLOCK) WHERE ProductNo = '{0}'
                  </sql>
                ecsql = String.Format(ecsql, ProductNo)
                Dim ecdt = EC.DB.ExecuteDataTable(ecsql)

                Dim attrsql As String = <sql>
                      SELECT * FROM MomoshoppingSCM_API_list_attributes WITH(NOLOCK) WHERE ProductNo = '{0}'
                  </sql>
                attrsql = String.Format(attrsql, ProductNo)
                Dim attrdt = EC.DB.ExecuteDataTable(attrsql)

                '取值塞值

                Dim obj As MomoSCM.JSONobj = New MomoSCM.JSONobj
                If doAction = "tempReportGoods" Then
                    obj.doAction = CommonParameter.doAction.tempReportGoods '暫存
                ElseIf doAction = "verifyReportGoods" Then
                    obj.doAction = CommonParameter.doAction.verifyReportGoods '提報
                End If

                'Dim feedback = MomoOldIMG_Process(ProductNo)
                'If feedback(0) = "" Then
                '    obj.zipFileData = ZipFileData_Process(ProductNo, "old", "")
                'ElseIf Convert.IsDBNull(dt.Select().FirstOrDefault.Item("NewImg_Url")) = False Then
                '    Dim newURL = dt.Select().FirstOrDefault.Item("NewImg_Url").ToString()
                '    obj.zipFileData = ZipFileData_Process(ProductNo, "new", newURL)
                'End If

                '20191219 育誠 一律改為使用目錄內的 10001_B1.jpg ， 再由後台人員去MomoSCM更改商品圖片
                obj.zipFileData = ZipFileData_Process(ProductNo)

                Dim salesMethods As String = dt.Select().FirstOrDefault.Item("salesMethods").ToString
                If salesMethods = "買斷" Then

                    obj.loginInfo = New MomoSCM.Logininfo
                    With obj.loginInfo
                        .entpCode = CommonParameter.loginInfo.Buyout.BuyoutCode
                        .entpID = CommonParameter.loginInfo.VATnumber
                        .entpPwd = CommonParameter.loginInfo.SCMpassword
                        .otpBackNo = CommonParameter.loginInfo.Buyout.BuyoutOTPcode
                    End With

                ElseIf salesMethods = "寄賣" Then

                    obj.loginInfo = New MomoSCM.Logininfo
                    With obj.loginInfo
                        .entpCode = CommonParameter.loginInfo.Consignment.ConsignmentCode
                        .entpID = CommonParameter.loginInfo.VATnumber
                        .entpPwd = CommonParameter.loginInfo.SCMpassword
                        .otpBackNo = CommonParameter.loginInfo.Consignment.ConsignmentOTPcode
                    End With
                End If

                obj.sendInfoList() = New List(Of MomoSCM.Sendinfolist)
                Dim sendInfoListValue = New MomoSCM.Sendinfolist
                With sendInfoListValue
                    .batchSupNo = ecdt.Select().FirstOrDefault.Item("ProductNo").ToString 'ProductNo
                    .supGoodsName_brand = ecdt.Select().FirstOrDefault.Item("brand").ToString 'brand
                    .supGoodsName_salePoint = dt.Select().FirstOrDefault.Item("NewProductName").ToString 'ProductName
                    .supGoodsName_serial = ecdt.Select().FirstOrDefault.Item("classics").ToString 'classics
                    .isPrompt = dt.Select().FirstOrDefault.Item("isPrompt").ToString
                    .isGift = dt.Select().FirstOrDefault.Item("isGift").ToString
                    .mainEcCategoryCode = dt.Select().FirstOrDefault.Item("mainEcCategoryCode").ToString
                    .webBrandNo = dt.Select().FirstOrDefault.Item("webBrandNo").ToString
                    .buyPrice = ecdt.Select().FirstOrDefault.Item("SpicalPrice").ToString 'SpicalPrice

                    '如為贈品售價為零
                    If dt.Select().FirstOrDefault.Item("isGift").ToString = "是" Then
                        .salePrice = "0"
                    Else
                        .salePrice = ecdt.Select().FirstOrDefault.Item("SpicalPrice").ToString 'SpicalPrice
                    End If

                    .custPrice = ecdt.Select().FirstOrDefault.Item("SpicalPrice").ToString 'SpicalPrice
                    .goodsType = dt.Select().FirstOrDefault.Item("goodsType").ToString '01:原廠 02:經銷商  03:平行輸入
                    .temperatureType = dt.Select().FirstOrDefault.Item("temperatureType").ToString '(常溫、冷凍、冷藏)
                    .originCode = CommonParameter.originCode '0086
                    .width = dt.Select().FirstOrDefault.Item("width").ToString
                    .length = dt.Select().FirstOrDefault.Item("length").ToString
                    .height = dt.Select().FirstOrDefault.Item("height").ToString
                    .weight = dt.Select().FirstOrDefault.Item("weight").ToString
                    .hasAs = dt.Select().FirstOrDefault.Item("hasAs").ToString '[售後保固]無EC欄位且為必填 (有/無)
                    .asDays = dt.Select().FirstOrDefault.Item("asDays").ToString '[保固天數]無EC欄位且為必填 (有/無) 
                    .asNote = dt.Select().FirstOrDefault.Item("asNote").ToString '[保固內容]無EC欄位且為必填 (有/無) 

                    '買斷供應商需提報寄倉商品[是否入EC倉]欄位值不合法或未填寫
                    '不入Momo倉時，[首次備貨量]欄位不可填寫
                    .isECWarehouse = dt.Select().FirstOrDefault.Item("isECWarehouse").ToString

                    .saleUnit = "件" '銷售單位 依照精技提供欄位寫死
                    .isPointReachDate = dt.Select().FirstOrDefault.Item("isPointReachDate").ToString
                    '.routeMainGroup = ""
                    '.routeSubGroup = ""
                    '.routeBranchName = ""
                    '.drugGoodsCode = "ABC-1234"
                    .isCommission = dt.Select().FirstOrDefault.Item("isCommission").ToString '[代收代付]無EC欄位且為必填 (是/否) 
                    .isAcceptTravelCard = dt.Select().FirstOrDefault.Item("isAcceptTravelCard").ToString '[售後保固]無EC欄位且為必填 (是/否) 
                    .isIncludeInstall = dt.Select().FirstOrDefault.Item("isIncludeInstall").ToString '[售後保固]無EC欄位且為必填 (是/否) 
                    '.recycleItem = ""
                    '.comments = "廠商與MOMO的意見交流"
                    .expDays = dt.Select().FirstOrDefault.Item("expDays").ToString  '保存期限(天) 無EC欄位且為必填 若商品無效期請輸入99999
                    '.etkPromoSdate = ""
                    '.etkPromoEdate = ""
                    '.etkOfflineDate = ""
                    '.trustBankCode = ""
                    '.outplaceSeq = ""
                    '.outplaceSeqRtn = ""
                    '.goodsSpec = "產品內容..."
                    '.saleNotice = "銷售注意事項…"
                    '.accessories = "3C配件"
                    '.giftDesc = "有什麼贈品…"
                    '.headline = "2012秋冬新品上市"
                    '.content = "質感展現的裝飾領口"
                    '.detailInfo = "輕熟主打珍珠領斜接雪紡棉上衣-優雅黑"
                    '.ecEntpReturnSeq = ""
                    .main_achievement = dt.Select().FirstOrDefault.Item("main_achievement").ToString
                    '.youtube_url = ""
                    .agreed_delivery_yn = dt.Select().FirstOrDefault.Item("agreed_delivery_yn").ToString
                    .tax_yn = dt.Select().FirstOrDefault.Item("tax_yn").ToString
                    .disc_mach_yn = dt.Select().FirstOrDefault.Item("disc_mach_yn").ToString
                    .gov_subsidize_yn = dt.Select().FirstOrDefault.Item("gov_subsidize_yn").ToString
                    Dim col1 = Split(attrdt.Select().FirstOrDefault.Item("colSeq1").ToString, ",")
                    .colSeq1 = col1(0)
                    Dim col2 = Split(attrdt.Select().FirstOrDefault.Item("colSeq2").ToString, ",")
                    .colSeq2 = col2(0)
                    Dim colseq = <規格API>[
                                            {
                                                   "COL_SEQ": "001",
                                                    "COL_NAME": "尺寸"
                                                },
                                                {
                                                    "COL_SEQ": "002" 
                                                    "COL_NAME": "容量"
                                                },
                                                {
                                                    "COL_SEQ": "003",
                                                    "COL_NAME": "重量"
                                                },
                                                {
                                                    "COL_SEQ": "004",
                                                    "COL_NAME": "顏色"
                                                },
                                                {
                                                    "COL_SEQ": "005",
                                                    "COL_NAME": "口味"
                                                },
                                                {
                                                    "COL_SEQ": "006",
                                                    "COL_NAME": "規格"
                                                }
                                    ]</規格API>
                End With

                sendInfoListValue.indexList() = New List(Of MomoSCM.Indexlist)
                For num2 As Integer = 0 To attrdt.Rows.Count - 1 Step 1

                    Dim indexListValue = New MomoSCM.Indexlist
                    With indexListValue
                        .indexNo = attrdt.Rows.Item(num2).Item("indexNo").ToString
                        .chosenItemNo = attrdt.Rows.Item(num2).Item("chosenItemNos").ToString
                    End With
                    sendInfoListValue.indexList().Add(indexListValue)
                Next
                'Dim indexListValue2 = New MomoSCM.Indexlist
                'With indexListValue2
                '    .indexNo = "20160128115258992"
                '    .chosenItemNo = "1\t2\t3\t"
                'End With
                'sendInfoListValue.indexList().Add(indexListValue2)

                sendInfoListValue.singleItemList() = New List(Of MomoSCM.Singleitemlist)
                Dim singleitemlistValue = New MomoSCM.Singleitemlist
                With singleitemlistValue
                    '詳細規格1 類似雅虎階層屬性 但值為自訂
                    .colDetail1 = attrdt.Select().FirstOrDefault.Item("colDetail1").ToString
                    '詳細規格2 類似雅虎階層屬性 但值為自訂
                    .colDetail2 = attrdt.Select().FirstOrDefault.Item("colDetail2").ToString


                    '.entpGoodsNo = "def11"
                    .internationalNo = ecdt.Select().FirstOrDefault.Item("barCode").ToString 'barCode
                    '.prepareQty = "0" '[可備貨量] 無EC欄位且為必填 ★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★★
                    .ecFirstQty = dt.Select().FirstOrDefault.Item("ecFirstQty").ToString '[備貨數量] 無EC欄位且為必填 
                    .ecMinQty = dt.Select().FirstOrDefault.Item("ecMinQty").ToString '最低採購量 無EC欄位且為必填 
                    .ecLeadTime = dt.Select().FirstOrDefault.Item("ecLeadTime").ToString '備貨週期 無EC欄位且為必填 
                    '.specifiedDate = ""
                    '.lastSaleDate = ""
                    '.branchNameSingle = ""
                End With


                'singleitemlistValue.branchSnList() = New List(Of MomoSCM.Branchsnlist)
                'Dim branchSnListValue = New MomoSCM.Branchsnlist
                'branchSnListValue.branchSn = ""
                'singleitemlistValue.branchSnList().Add(branchSnListValue)
                'Dim branchSnListValue2 = New MomoSCM.Branchsnlist
                'branchSnListValue2.branchSn = ""
                'singleitemlistValue.branchSnList().Add(branchSnListValue2)
                sendInfoListValue.singleItemList().Add(singleitemlistValue)

                'sendInfoListValue.clothData = New MomoSCM.Clothdata
                'With sendInfoListValue.clothData
                '    ._type = "1"
                '    ._unit = "01"
                '    .sizeItemCounts = "1,2,6"
                '    .tryItemCounts = "1,2,3,4,5,6,7,8,9,10"
                'End With

                'sendInfoListValue.clothData.sizeIndexList() = New List(Of MomoSCM.Sizeindexlist)
                'Dim sizeIndexListValue = New MomoSCM.Sizeindexlist
                'sizeIndexListValue.sizeIndex = "M,25,B"
                'sendInfoListValue.clothData.sizeIndexList().Add(sizeIndexListValue)

                'Dim sizeIndexListValue2 = New MomoSCM.Sizeindexlist
                'sizeIndexListValue2.sizeIndex = "L,28,C"
                'sendInfoListValue.clothData.sizeIndexList().Add(sizeIndexListValue2)

                'Dim sizeIndexListValue3 = New MomoSCM.Sizeindexlist
                'sizeIndexListValue3.sizeIndex = "XL,30,D"
                'sendInfoListValue.clothData.sizeIndexList().Add(sizeIndexListValue3)



                'sendInfoListValue.clothData.tryIndexList() = New List(Of MomoSCM.Tryindexlist)
                'Dim tryIndexListValue = New MomoSCM.Tryindexlist
                'tryIndexListValue.tryIndex = "泱泱,M,23,30,26,50,162,45,34B,很舒服"
                'sendInfoListValue.clothData.tryIndexList().Add(tryIndexListValue)

                'Dim tryIndexListValue2 = New MomoSCM.Tryindexlist
                'tryIndexListValue2.tryIndex = "老蕭,M,23,30,26,55,162,55,34C,有點緊，L可能會比較好"
                'sendInfoListValue.clothData.tryIndexList().Add(tryIndexListValue2)


                'sendInfoListValue.mobileDetailInfo = New MomoSCM.Mobiledetailinfo
                'With sendInfoListValue.mobileDetailInfo
                '    .youtubeUrl() = New List(Of String)
                '    .youtubeUrl().Add("12345678901")
                '    .youtubeUrl().Add("1234567902")


                '    .title() = New List(Of String)
                '    .title().Add("標題一")
                '    .title().Add("標題二")
                '    .title().Add("標題三")
                '    .title().Add("標題四")

                '    .content() = New List(Of String)
                '    .content().Add("內容一")
                '    .content().Add("內容二")
                '    .content().Add("內容三")
                '    .content().Add("內容四")
                'End With

                obj.sendInfoList().Add(sendInfoListValue)



                '完成上述結構化迴圈後將obj序列化為api所需json
                Dim JSONobj As String = Newtonsoft.Json.JsonConvert.SerializeObject(obj)

                '★★★★★★★★★★★★★★★★★★★★ "呼叫API並上傳資料" ★★★★★★★★★★★★★★★★★★★★

                Dim URL As String = "https://scmapi.momoshop.com.tw/GoodsServlet.do"

                Try
                    Dim Z As String = MomoSCM_API.Request_MomoSCM_API("POST", URL, JSONobj)


                    '反饋200錯誤處理
                    Dim responseJSON = Newtonsoft.Json.JsonConvert.DeserializeObject(Z)
                    'netJArray(0).Item("MAIN_ACHIEVEMENT_LIST")(num2).Item("ID").ToString()

                    totalCnt += CInt(responseJSON.item("resultInfo").item("totalCnt").ToString) ' 總筆數
                    successCnt += CInt(responseJSON.item("resultInfo").item("successCnt").ToString) ' 成功筆數

                    If CInt(responseJSON.item("resultInfo").item("successCnt").ToString) > 0 Then
                        '將資料上傳時的時間從新塞入資料表
                        Dim sqlposteddate = "UPDATE MomoshoppingSCM_API_list Set posteddate ='{0}' WHERE ProductNo='{1}'"
                        sqlposteddate = String.Format(sqlposteddate, Now, ProductNo)
                        EC.DB.ExecuteScalar(sqlposteddate)
                    End If

                    failCnt += CInt(responseJSON.item("resultInfo").item("failCnt").ToString) ' 失敗筆數
                    Dim failList As Newtonsoft.Json.Linq.JArray = responseJSON.item("resultInfo").item("failList") '錯誤清單

                    '清單資料整理 ( 因為都是一筆資料一筆資料丟所以不做防呆 )
                    For numfailList As Integer = 0 To failList.Count - 1 Step 1
                        Dim lispsplit() = Split(failList(numfailList).ToString, Convert.ToChar(9)) 'vbtab分割
                        ProductNames = lispsplit(2)
                        failLists += ((numfailList + 1).ToString) + ". " + lispsplit(3) + "</br>"
                    Next
                    failLists = failLists.Replace("'", "")
                    ProductNames += " : </br></br>" + failLists
                    '紀錄最後一次的反傳
                    Dim failListsql As String = <sql>UPDATE MomoshoppingSCM_API_list 
                                       SET last_failList = '{0}'
                                       WHERE ProductNo = '{1}'
                                  </sql>
                    failListsql = String.Format(failListsql, failLists, ProductNo)
                    EC.DB.ExecuteScalar(failListsql)


                Catch ex As Exception

                    Dim Z As String = MomoSCM_API.Request_MomoSCM_API("POST", URL, JSONobj)

                    CommonParameter.Message.errormsg += "<font style='color:red'>失敗，錯誤訊息 : " + ex.Message + "</font></br>" + "MomoAPI反饋訊息如下" + Z
                    Return CommonParameter.Message.errormsg
                End Try
                'CommonParameter.Message.successmsg += "<font style='color:blue'>ProductNo:" + "ProductNo" + "上傳成功</font></br>"
                'Return CommonParameter.Message.successmsg





            Next

            Dim doActionName = ""
            If doAction = "tempReportGoods" Then
                doActionName = "測試暫存"
            ElseIf doAction = "verifyReportGoods" Then
                doActionName = "正式提報"
            End If

            postback = <html>&lt;table style='display: table;
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
                                                        &lt;h6&gt;壓縮檔大小{5}(kb)、解壓縮後圖檔大小{4}(kb)&lt;/h6&gt;
                                                        &lt;h4&gt;{6}:錯誤商品名稱與清單:&lt;/h4&gt;
                                                                  {3}
                                                   &lt;/td&gt;
                                              &lt;/tr&gt;
                                           &lt;/table&gt;
                                     </html>
            postback = String.Format(postback, totalCnt, successCnt, failCnt, ProductNames, CommonParameter.Message.imglength, CommonParameter.Message.ziplength, doActionName)



            If successCnt = 0 And failCnt = 0 Then

                Return "<p>資料皆已暫存，請提報API!!</p>"

            End If


            Return postback

        End If

        Return "<p>暫無新資料需提報API!!</p>"

        Dim ex_JSONobj = <a>{
    "doAction":"tempReportGoods",
"zipFileData":"UE…(節略)",
    "loginInfo":{
        "entpCode":"001005",
        "entpID":"11223344",
        "entpPwd":"ab1234",
"otpBackNo":"123"
    },
"sendInfoList":[
{
    "batchSupNo":"10001",
    "supGoodsName_brand":"大量品牌",
    "supGoodsName_salePoint":"大量賣點",
    "supGoodsName_serial":"大量系列",
    "isPrompt":"否",
    "isGift":"否",
    "mainEcCategoryCode":"1000600002",
    " webBrandNo":"1702400051",
    "buyPrice":"1200",
    "salePrice":"1583",
    "custPrice":"1800",
    "goodsType":"01",
    "temperatureType":"常溫",
    "originCode":"0886",
    "width":"13",
    "length":"29",
    "height":"15",
    "weight":"8",
    "hasAs":"無",
    "asDays":"",
    "asNote":"",
    "isECWarehouse":"否",
    "saleUnit":"無",
    "isPointReachDate":"否",
    "routeMainGroup":"",
    "routeSubGroup":"",
    "routeBranchName":"",
    "drugGoodsCode":"ABC-1234",
    "isCommission":"否",
    "isAcceptTravelCard":"否",
    "isIncludeInstall":"否",
    "recycleItem":"",
    "comments":"廠商與MOMO的意見交流",
    "expDays":"",
    "etkPromoSdate":"",
    "etkPromoEdate":"",
    "etkOfflineDate":"",
    "trustBankCode":"",
    "outplaceSeq":"",
    "outplaceSeqRtn":"",
    "goodsSpec":"產品內容…",
    "saleNotice":"銷售注意事項…",
	"accessories":"3C配件",
    "giftDesc":"有什麼贈品…",
    "headline":"2012秋冬新品上市",
    "content":"質感展現的裝飾領口",
    "detailInfo":"輕熟主打珍珠領斜接雪紡棉上衣-優雅黑",
    "ecEntpReturnSeq":"",
    "main_achievement":"A0001G0001C0001",
    "youtube_url":"",
    "agreed_delivery_yn":"", 
    "tax_yn":"是", 
    "disc_mach_yn":"", 
    "gov_subsidize_yn":"", 
    "colSeq1":"001",
    "colSeq2":"002",
    "indexList": [
           {
"indexNo": "20160108103334151",
		        "chosenItemNo": "1"
		    },
           {
		        "indexNo": "20160128115258992",
		        "chosenItemNo": "1\t2\t3\t" 
		    }
            
    ],
"singleItemList":[
         {
           "colDetail1":"紅",
           "colDetail2":"斑馬紋",
           "entpGoodsNo":"def11",
           "internationalNo":"1234567890123",
           "prepareQty":"50",
           "ecFirstQty":"",
           "ecMinQty":"",
           "ecLeadTime":"",
           "specifiedDate":"",
           "lastSaleDate":"",
           "branchNameSingle":"",
           "branchSnList":[{"branchSn":""},{"branchSn":""}
           ]
         }
    ],"clothData": {
"_type": "1",
		        "_unit": "01",
		        "sizeItemCounts": "1,2,6",
		        "sizeIndexList": [
{"sizeIndex":"M,25,B"},
{"sizeIndex":"L,28,C"},
{"sizeIndex":"XL,30,D"}
],
		        "tryItemCounts": "1,2,3,4,5,6,7,8,9,10",
		        "tryIndexList": [
{"tryIndex":"泱泱,M,23,30,26,50,162,45,34B,很舒服"},
{"tryIndex":"老蕭,M,23,30,26,55,162,55,34C,有點緊，L可能會比較好"}
]
		},
"mobileDetailInfo": {
			"youtubeUrl": ["12345678901", "1234567902"],
			"title": ["標題一", "標題二", "標題三", "標題四"],
			"content": ["內容一", "內容二", "內容三", "內容四"]
		}
  }
]
}
</a>

    End Function

    ''' <summary>
    ''' 既有圖檔符合MomoSCM規格時，URL取圖檔、改規格、改檔名、轉ZIP、塞串流、轉base64。
    ''' <para>使用時機 : 既有圖檔符合MomoSCM規格時，MomoshoppingSCM_API_list資料表、NewImg_Url欄位為空時。</para>
    ''' <paramref name="ProductNo"/> 參數:上傳商品之產品編號。
    ''' </summary>
    Private Shared Function ZipFileData_Process(ByVal ProductNo As String) As String
        ' <paramref name="IMGstatus"/> 參數:區分是商品既有或新上傳網址 old / new 。
        ', ByVal IMGstatus As String, ByVal newURL As String
        '參考 : http://www.componentace.com/add-stream-to-zip-in-vb.net.htm
        '參考 : https://blog.darkthread.net/blog/zip-byte-array/
        '參考 : https://docs.microsoft.com/zh-tw/dotnet/api/system.io.compression.zipfile?view=netframework-4.8
        '參考 : https://www.cnblogs.com/Mr_JinRui/archive/2010/07/05/1771184.html
        '參考 : https://dotblogs.com.tw/atowngit/2010/01/12/12972
        '參考 : https://codeday.me/bug/20190203/607864.html
        '參考 : https://stackoverflow.com/questions/13053739/when-is-getbuffer-on-memorystream-ever-useful

        '20191219 育誠 一律改為使用目錄內的 10001_B1.jpg ， 再由後台人員去MomoSCM更改商品圖片

        'Dim imgurl = ""
        ''圖檔路徑
        'If IMGstatus = "old" Then
        '    Dim sql As String = <sql>SELECT TOP 1 CategoryA FROM list with(NOLOCK) WHERE ProductNo = '{0}'</sql>
        '    sql = String.Format(sql, ProductNo)
        '    Dim dt = EC.DB.ExecuteDataTable(sql)
        '    imgurl = EC.mng.Info.Eclife_HomeURL & dt.Select().FirstOrDefault.Item("CategoryA").ToString
        'ElseIf IMGstatus = "new" Then
        '    imgurl = newURL
        '    '正確範例 http://img.eclife.com.tw/photo2011/prod_2019/11/G1260008_forAWS.jpg
        '    '測試 http://img.eclife.com.tw/photo2011/prod_2019/MomoSCM/G9040006_B1.jpg
        '    '部分網域有鎖不是所有地方都可以外聯故需要存死位子
        'End If

        '20191219 育誠 一律改為使用目錄內的 10001_B1.jpg ， 再由後台人員去MomoSCM更改商品圖片 C:\Web_backup\Site\EC_Mngx FileStream
        Dim WC As System.Net.WebClient = New System.Net.WebClient()
        Dim imgurl = "https://ecmngx.eclife.com.tw/mng/product/Unitech/momoshoppingscm/10001_B1.jpg"
        Dim networkStream As Stream = WC.OpenRead(imgurl)
        Dim MemoryStream As New MemoryStream()
        networkStream.CopyTo(MemoryStream)
        networkStream.Dispose()
        Dim bytes(MemoryStream.Length) As Byte
        MemoryStream.Seek(0, SeekOrigin.Begin)
        MemoryStream.Read(bytes, 0, MemoryStream.Length)
        MemoryStream.Dispose()
        ''★★★★★★★★★★ URL取圖檔、改規格、改檔名 ★★★★★★★★★★
        'Dim WC As System.Net.WebClient = New System.Net.WebClient()

        ''20191218 改為統一使用資料夾內 10001_B1.jpg檔案
        'Dim oldimg As System.Drawing.Image = System.Drawing.Image.FromStream(stream)
        'stream.Dispose()
        ''oldimg.Save("C:\dasdasd\test.jpg") 檔案不變
        'Dim newimg = New System.Drawing.Bitmap(oldimg, New System.Drawing.Size(1000, 1000))
        ''newimg.Save("C:\dasdasd\test.jpg") 檔案變大
        'Dim graphic As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(newimg)
        'WC.Dispose()
        'graphic.InterpolationMode = Drawing.Drawing2D.InterpolationMode.HighQualityBicubic
        'graphic.SmoothingMode = Drawing.Drawing2D.SmoothingMode.HighQuality
        'graphic.PixelOffsetMode = Drawing.Drawing2D.PixelOffsetMode.HighQuality
        'graphic.CompositingQuality = Drawing.Drawing2D.CompositingQuality.HighQuality
        'graphic.DrawImageUnscaledAndClipped(newimg, New Rectangle(0, 0, 1000, 1000))
        'newimg.SetResolution(1000, 1000)
        ''newimg.Save("C:\dasdasd\test.jpg") 檔案不變
        'oldimg.Dispose()
        ''圖檔改規格後存為串流 詳細請參照MomoSCM上的文件
        'Dim imgStream = New MemoryStream()
        'newimg.Save(imgStream, System.Drawing.Imaging.ImageFormat.Jpeg)
        'newimg.Dispose()

        ''固定檔名為(batchSupNo + _B1~...)　詳細請參照MomoSCM上的文件
        Dim newimgfilename = ProductNo + "_B1.jpg"

            ''★★★★★★★★★★ Stream 轉 Bytes ★★★★★★★★★★

            ''imgStream.Seek(0, SeekOrigin.Begin)
            ''Dim buf() As Byte
            ''Dim br As BinaryReader = New BinaryReader(imgStream)
            ''Dim len As Integer = imgStream.Length
            ''buf = br.ReadBytes(len)
            ''br.Close()

            'Dim bytes(imgStream.Length) As Byte
            'imgStream.Seek(0, SeekOrigin.Begin)
            'imgStream.Read(bytes, 0, imgStream.Length)

            '★★★★★★★★★★ 轉ZIP、塞串流 ★★★★★★★★★★ 原圖檔不會失真但會影響原始圖檔大小非常大。

            'Dim zipStream = New MemoryStream()
            'Dim zipArchive = New ZipArchive(zipStream, ZipArchiveMode.Update)
            'Dim entry = zipArchive.CreateEntry(newimgfilename)
            'Dim entryStream = entry.Open()
            'entryStream.Seek(0, SeekOrigin.Begin)
            'entryStream.Write(bytes, 0, bytes.Length)

            Dim imgzip
            Dim zipStream = New MemoryStream()
            Using zipStream
                Dim zipArchive = New ZipArchive(zipStream, ZipArchiveMode.Update)
                Using zipArchive
                    Dim entry = zipArchive.CreateEntry(newimgfilename)
                    Dim entryStream = entry.Open()
                    Using entryStream
                        entryStream.Write(bytes, 0, bytes.Length)
                    End Using
                End Using
                imgzip = zipStream.ToArray
            End Using
            'System.IO.File.WriteAllBytes("C:\dasdasd\test.zip", imgzip) 變非常小 3mb -> 200kb
            '★★★★★★★★★★ bytes轉Base64 ★★★★★★★★★★
            Dim base64imgzip = Convert.ToBase64String(imgzip)
            '符合MomoSCM B圖需求大小 1000*1000 (200kb~1000kb)
            CommonParameter.Message.imglength = CInt(bytes.Length / 1024) '壓縮過後的檔案大小
            CommonParameter.Message.ziplength = CInt(imgzip.Length / 1024) '壓縮過後的檔案大小
            Return base64imgzip
            '解碼測試網址 :　https://cloud.magiclen.org/tw/base64/decoder


    End Function

    ''' <summary>
    ''' 驗證舊圖檔符合是否Momo需求
    ''' </summary>
    Public Shared Function MomoOldIMG_Process(ByVal ProductNo As String) As String()

        Dim feedback(2) As String
        Dim newIMGsql As String = <sql>
                                      SELECT NewImg_Url FROM MomoshoppingSCM_API_list WITH(Nolock) WHERE ProductNo = '{0}'
                                  </sql>
        newIMGsql = String.Format(newIMGsql, ProductNo)
        Dim newimgdt = EC.DB.ExecuteDataTable(newIMGsql)
        If newimgdt.Rows.Count > 0 Then
            If Convert.IsDBNull(newimgdt.Select().FirstOrDefault.Item("NewImg_Url")) Then
                feedback(0) = ""
                '有資料且有新圖
                Return feedback
            End If
        End If

        '圖檔路徑
        Dim sql As String = <sql>SELECT TOP 1 CategoryA FROM list with(NOLOCK) WHERE ProductNo = '{0}'</sql>
        sql = String.Format(sql, ProductNo)
        Dim dt = EC.DB.ExecuteDataTable(sql)
        Dim imgurl = EC.mng.Info.Eclife_HomeURL & dt.Select().FirstOrDefault.Item("CategoryA").ToString

        '★★★★★★★★★★ URL取圖檔、改規格、改檔名 ★★★★★★★★★★
        Dim WC As System.Net.WebClient = New System.Net.WebClient()
        Dim oldimg As System.Drawing.Image = System.Drawing.Image.FromStream(WC.OpenRead(imgurl))
        Dim newimg = New System.Drawing.Bitmap(oldimg, New System.Drawing.Size(1000, 1000))
        WC.Dispose()
        oldimg.Dispose()
        '圖檔改規格後存為串流 詳細請參照MomoSCM上的文件
        Dim imgStream = New MemoryStream()
        newimg.Save(imgStream, System.Drawing.Imaging.ImageFormat.Jpeg)
        newimg.Dispose()

        '圖檔處理
        '固定檔名為(batchSupNo + _B1~...)　詳細請參照MomoSCM上的文件
        Dim newimgfilename = ProductNo + "_B1.jpg"

        '★★★★★★★★★★ Stream 轉 Bytes ★★★★★★★★★★

        Dim bytes(imgStream.Length) As Byte
        imgStream.Seek(0, SeekOrigin.Begin)
        imgStream.Read(bytes, 0, imgStream.Length)

        '★★★★★★★★★★ 轉ZIP、塞串流 ★★★★★★★★★★ 原圖檔不會失真但會影響原始圖檔大小非常大。

        Dim imgzip
        Dim zipStream = New MemoryStream()
        Using zipStream
            Dim zipArchive = New ZipArchive(zipStream, ZipArchiveMode.Update)
            Using zipArchive
                Dim entry = zipArchive.CreateEntry(newimgfilename)
                Dim entryStream = entry.Open()
                Using entryStream
                    entryStream.Write(bytes, 0, bytes.Length)
                End Using
            End Using
            imgzip = zipStream.ToArray
        End Using
        '符合MomoSCM B圖需求大小 1000*1000 (200kb~1000kb)
        Dim imglength = CInt(imgzip.Length)
        '200kb * 1024 = 204,800 bytes
        If imglength < 250000 Then
            imglength = imglength / 1024
            feedback(0) = "此圖檔太小請重新上傳，符合MomoSCM規格(200kb~1000kb)。"
            feedback(1) = "原圖檔案轉為1000*1000後大小為:" + CStr(imglength) + "kb"
            '無資料且舊圖太小
            Return feedback
        ElseIf imglength > 1000000 Then
            imglength = imglength / 1024
            feedback(0) = "此圖檔太大請重新上傳，符合MomoSCM規格(200kb~1000kb)。"
            feedback(1) = "原圖檔案轉為1000*1000後大小為:" + CStr(imglength) + "kb"
            '無資料且舊圖太大
            Return feedback
        End If

        '無資料但舊圖符合規格
        feedback(0) = ""
        Return feedback

    End Function

    ''' <summary>
    ''' Momo商品上傳格式類別檔案
    ''' </summary>
    Public Class MomoSCM
        Public Class JSONobj
            Public Property doAction As String
            Public Property zipFileData As String
            Public Property loginInfo As Logininfo
            Public Property sendInfoList() As List(Of Sendinfolist)
        End Class

        Public Class Logininfo
            Public Property entpCode As String
            Public Property entpID As String
            Public Property entpPwd As String
            Public Property otpBackNo As String
        End Class

        Public Class Sendinfolist
            Public Property batchSupNo As String
            Public Property supGoodsName_brand As String
            Public Property supGoodsName_salePoint As String
            Public Property supGoodsName_serial As String
            Public Property isPrompt As String
            Public Property isGift As String
            Public Property mainEcCategoryCode As String
            Public Property webBrandNo As String
            Public Property buyPrice As String
            Public Property salePrice As String
            Public Property custPrice As String
            Public Property goodsType As String
            Public Property temperatureType As String
            Public Property originCode As String
            Public Property width As String
            Public Property length As String
            Public Property height As String
            Public Property weight As String
            Public Property hasAs As String
            Public Property asDays As String
            Public Property asNote As String
            Public Property isECWarehouse As String
            Public Property saleUnit As String
            Public Property isPointReachDate As String
            'Public Property routeMainGroup As String
            'Public Property routeSubGroup As String
            'Public Property routeBranchName As String
            'Public Property drugGoodsCode As String
            Public Property isCommission As String
            Public Property isAcceptTravelCard As String
            Public Property isIncludeInstall As String
            'Public Property recycleItem As String
            'Public Property comments As String
            Public Property expDays As String
            'Public Property etkPromoSdate As String
            'Public Property etkPromoEdate As String
            'Public Property etkOfflineDate As String
            'Public Property trustBankCode As String
            'Public Property outplaceSeq As String
            'Public Property outplaceSeqRtn As String
            'Public Property goodsSpec As String
            'Public Property saleNotice As String
            'Public Property accessories As String
            'Public Property giftDesc As String
            'Public Property headline As String
            'Public Property content As String
            'Public Property detailInfo As String
            'Public Property ecEntpReturnSeq As String
            Public Property main_achievement As String '業績屬性
            'Public Property youtube_url As String
            Public Property agreed_delivery_yn As String
            Public Property tax_yn As String
            Public Property disc_mach_yn As String
            Public Property gov_subsidize_yn As String
            Public Property colSeq1 As String
            Public Property colSeq2 As String
            Public Property indexList() As List(Of Indexlist)
            Public Property singleItemList() As List(Of Singleitemlist)
            'Public Property clothData As Clothdata
            'Public Property mobileDetailInfo As Mobiledetailinfo
        End Class

        'Public Class Clothdata
        'Public Property _type As String
        'Public Property _unit As String
        'Public Property sizeItemCounts As String
        'Public Property sizeIndexList() As List(Of Sizeindexlist)
        'Public Property tryItemCounts As String
        'Public Property tryIndexList() As List(Of Tryindexlist)
        'End Class

        Public Class Sizeindexlist
            Public Property sizeIndex As String
        End Class

        Public Class Tryindexlist
            Public Property tryIndex As String
        End Class

        'Public Class Mobiledetailinfo
        'Public Property youtubeUrl() As List(Of String)
        'Public Property title() As List(Of String)
        'Public Property content() As List(Of String)
        'End Class

        Public Class Indexlist
            Public Property indexNo As String
            Public Property chosenItemNo As String
        End Class

        Public Class Singleitemlist
            Public Property colDetail1 As String
            Public Property colDetail2 As String
            'Public Property entpGoodsNo As String
            Public Property internationalNo As String
            'Public Property prepareQty As String
            Public Property ecFirstQty As String
            Public Property ecMinQty As String
            Public Property ecLeadTime As String
            'Public Property specifiedDate As String
            'Public Property lastSaleDate As String
            'Public Property branchNameSingle As String
            'Public Property branchSnList() As List(Of Branchsnlist)
        End Class

        'Public Class Branchsnlist
        'Public Property branchSn As String
        'End Class

    End Class

    ''' <summary>
    ''' Momo欄位查詢API+資料更新
    ''' </summary>
    Class Field_Update
        ''' <summary>
        ''' Momo分類資料更新。
        ''' <para>儲存至MomoshoppingSCM_API_Category</para>
        ''' <para>供連動選項使用。</para>
        ''' </summary>
        Public Shared Function POST_Category_API() As String

            Dim SQL = <SQL>SELECT * FROM MomoshoppingSCM_API_Category WITH(NOLOCK)</SQL>
            Dim DT = EC.DB.ExecuteDataTable(SQL)

            For num As Integer = 0 To DT.Rows.Count - 1 Step 1

                Dim exrequest = <a>{
                              "loginInfo": {
                                      "entpID": "97275166",
                                      "entpCode": "001005",
                                      "entpPwd": "12345678",
                                      "otpBackNo": "111"
                                },
                              "ecCategoryCode": "1300500196"
                           }
                        </a>

                Dim salesMethods() = {CommonParameter.loginInfo.Buyout.salesMethods, CommonParameter.loginInfo.Consignment.salesMethods}
                For Each salesMethod As String In salesMethods

                    Dim requestdatalv1 As New Dictionary(Of String, Object)
                    Dim requestdatalv2 As New Dictionary(Of String, Object)

                    If salesMethod.ToString = "Buyout" Then

                        With requestdatalv2
                            .Add("entpID", CommonParameter.loginInfo.VATnumber)
                            .Add("entpCode", CommonParameter.loginInfo.Buyout.BuyoutCode)
                            .Add("entpPwd", CommonParameter.loginInfo.SCMpassword)
                            .Add("otpBackNo", CommonParameter.loginInfo.Buyout.BuyoutOTPcode)
                        End With

                    ElseIf salesMethod.ToString = "Consignment" Then

                        With requestdatalv2
                            .Add("entpID", CommonParameter.loginInfo.VATnumber)
                            .Add("entpCode", CommonParameter.loginInfo.Consignment.ConsignmentCode)
                            .Add("entpPwd", CommonParameter.loginInfo.SCMpassword)
                            .Add("otpBackNo", CommonParameter.loginInfo.Consignment.ConsignmentOTPcode)
                        End With

                    End If

                    With requestdatalv1
                        .Add("loginInfo", requestdatalv2)
                        .Add("ecCategoryCode", DT.Rows.Item(num).Item("mainEcCategoryCode").ToString)
                    End With

                    '完成上述結構化迴圈後將obj序列化為api所需json
                    Dim JSONobj As String = Newtonsoft.Json.JsonConvert.SerializeObject(requestdatalv1)

                    '★★★★★★★★★★★★★★★★★★★★ "呼叫API並上傳資料" ★★★★★★★★★★★★★★★★★★★★

                    Dim URL = "https://scmapi.momoshop.com.tw/api/v1/goods/basic_code/ecCategory/D1102.scm"
                    Dim QA = <A>HTTP status code：
                    200　[GET]：OK，返回請求資訊。
                    201　[POST]：CREATED，新增或修改成功。
                    400 ：Bad Request，用戶發出的請求有誤，或Server尚未提供此服務。
                    401 ：Unauthorized，表示無權限。
                    422 ：Unprocesable entity，驗證錯誤。
                    500 ：INTERNAL SERVER ERROR，無預期錯誤。
                </A>
                    Try
                        Dim Z As String = MomoSCM_API.Request_MomoSCM_API("POST", URL, JSONobj)
                        Dim netJArray As Newtonsoft.Json.Linq.JArray = Newtonsoft.Json.JsonConvert.DeserializeObject(Z)
                        Dim EXJSON = <EXJSON>
                                     {
                                       "CATEGORY_CODE": "1000100005",
                                       "LEVEL1":  "服飾",
                                       "LEVEL2":  "韓國空運",
                                       "LEVEL3":  "女裝",
	                                   "LEVEL4":  "帽T",
	                                   "IS_ AGREED_DELIVERY_YN":  "0",
	                                   "IS_TAX_YN":  "1",
	                                   "IS_DISC_MACH_YN":  "0",
	                                   "IS_GOV_SUBSIDIZE_YN":  "0",
	                                   "IS_MAIN_ACHIEVEMENT_YN":  "1",
	                                   "MAIN_ACHIEVEMENT_NAME":  "商品組合",
	                                   "MAIN_ACHIEVEMENT_LIST": [	
                                               {"ID":"A0003G0002C0002","NAME":"超值禮盒"},
                                               {"ID":"A0003G0002C0003","NAME":"特惠組"},
                                               {"ID":"A0003G0002C0004","NAME":"單品"},
                                               {"ID":"A0003G0002C0005","NAME":"超值禮盒"}
                                                 , {…}
                                                ]
                                     }
                                 </EXJSON>
                        Dim sqlupdata As String =
                                        <sql>UPDATE MomoshoppingSCM_API_Category SET 
                                         IS_AGREED_DELIVERY_YN ='{0}',
                                         IS_TAX_YN ='{1}',
                                         IS_DISC_MACH_YN ='{2}',
                                         IS_GOV_SUBSIDIZE_YN ='{3}',
                                         IS_MAIN_ACHIEVEMENT_YN ='{4}',
                                         MAIN_ACHIEVEMENT_ID = '{5}',
                                         MAIN_ACHIEVEMENT_NAME = '{6}',
                                         posteddate ='{7}'
                                         WHERE mainEcCategoryCode = '{8}'
                                    </sql>
                        '0: 否、1: 是 (提報商品所需)
                        Dim A = netJArray(0).Item("IS_AGREED_DELIVERY_YN").ToString
                        Dim B = netJArray(0).Item("IS_TAX_YN").ToString
                        Dim C = netJArray(0).Item("IS_DISC_MACH_YN").ToString
                        Dim D = netJArray(0).Item("IS_GOV_SUBSIDIZE_YN").ToString
                        Dim E = ""
                        '不確定為何範例有IS_MAIN_ACHIEVEMENT_YN欄位但真實WebAPI回傳欄位無此欄位
                        'Dim E = netJArray(0).Item("IS_MAIN_ACHIEVEMENT_YN").ToString
                        If CStr(netJArray(0).Item("IS_MAIN_ACHIEVEMENT_YN")) <> Nothing Then
                            E = netJArray(0).Item("IS_MAIN_ACHIEVEMENT_YN").ToString
                        End If

                        Dim F As String = ""
                        Dim G As String = ""

                        If E = "" Then
                            For num2 As Integer = 0 To netJArray(0).Item("MAIN_ACHIEVEMENT_LIST").Count - 1 Step 1
                                F += netJArray(0).Item("MAIN_ACHIEVEMENT_LIST")(num2).Item("ID").ToString() + ","
                                G += netJArray(0).Item("MAIN_ACHIEVEMENT_LIST")(num2).Item("NAME").ToString() + ","
                            Next
                        End If

                        Dim H = DT.Rows.Item(num).Item("mainEcCategoryCode").ToString

                        sqlupdata = String.Format(sqlupdata, A, B, C, D, E, F, G, Now, H)

                        EC.DB.ExecuteScalar(sqlupdata)

                    Catch ex As Exception
                        CommonParameter.Message.errormsg += "<font style='color:red'>mainEcCategoryCode:" + "ecCategoryCode" + "失敗，錯誤訊息 : " + ex.Message + "</font></br>"
                        'Return CommonParameter.Message.errormsg
                    End Try
                    CommonParameter.Message.successmsg += "<font style='color:blue'>mainEcCategoryCode:" + "ecCategoryCode" + "上傳成功</font></br>"
                    'Return CommonParameter.Message.successmsg

                Next
                'Return "暫無新資料需分類屬性查詢API"
            Next
        End Function

        ''' <summary>
        ''' Momo分類屬性資料更新。
        ''' <para>儲存至MomoshoppingSCM_API_AttrIndex</para>
        ''' <para>供連動選項使用。</para>
        ''' </summary>
        Public Shared Function POST_AttrIndex_API() As String
            Dim exrequest = <a>{
  "loginInfo": {
        "entpID": "97275166",
        "entpCode": "001005",
        "entpPwd": "12345678",
        "otpBackNo": "111"
  },
  "ecCategoryCode": "1300500196"
}

                        </a>
            Dim exresponse = <a>[
{
    "ITEM_CONTENT": "紅\t澄\t黃\t綠",
    "CHECK_YN": "0",
    "INDEX_ITEM_NO": "A001G001C001\tA001G001C002\tA001G001C003\tA001G001C004",
    "INDEX_NAME": "帽子顏色",
    "INDEX_NO": "A001G001"
},
{
    "ITEM_CONTENT": "XS\tS\tM\tL\tXL",
    "CHECK_YN": "1",
    "INDEX_ITEM_NO": "A001G002C001\tA001G002C002\tA001G002C003\tA001G002C004\tA001G002C005",
    "INDEX_NAME":  "帽子尺寸",
    "INDEX_NO": "A001G002"
  },
{},…
]
                        </a>

            Dim sql1 = <sql>
                       SELECT * FROM MomoshoppingSCM_API_Category WITH(NOLOCK)
                   </sql>
            Dim Categorytb = EC.DB.ExecuteDataTable(sql1)


            EC.DB.ExecuteScalar("DELETE FROM MomoshoppingSCM_API_AttrIndex")
            For num As Integer = 0 To Categorytb.Rows.Count - 1 Step 1

                Dim salesMethods() = {"寄賣"} '先只做寄賣

                For num2 As Integer = 0 To salesMethods.Count - 1 Step 1

                    Dim obj As New Dictionary(Of String, Object)
                    obj.Add("loginInfo", New MomoSCM.Logininfo)

                    Dim ecCategoryCode = Categorytb.Rows.Item(num).Item("mainEcCategoryCode").ToString

                    obj.Add("ecCategoryCode", ecCategoryCode)

                    If salesMethods(num2) = "買斷" Then

                        obj.Item("loginInfo") = New MomoSCM.Logininfo
                        With obj.Item("loginInfo")
                            .entpCode = CommonParameter.loginInfo.Buyout.BuyoutCode
                            .entpID = CommonParameter.loginInfo.VATnumber
                            .entpPwd = CommonParameter.loginInfo.SCMpassword
                            .otpBackNo = CommonParameter.loginInfo.Buyout.BuyoutOTPcode
                        End With

                    ElseIf salesMethods(num2) = "寄賣" Then

                        obj.Item("loginInfo") = New MomoSCM.Logininfo
                        With obj.Item("loginInfo")
                            .entpCode = CommonParameter.loginInfo.Consignment.ConsignmentCode
                            .entpID = CommonParameter.loginInfo.VATnumber
                            .entpPwd = CommonParameter.loginInfo.SCMpassword
                            .otpBackNo = CommonParameter.loginInfo.Consignment.ConsignmentOTPcode
                        End With
                    End If

                    '完成上述結構化迴圈後將obj序列化為api所需json
                    Dim JSONobj As String = Newtonsoft.Json.JsonConvert.SerializeObject(obj)

                    '★★★★★★★★★★★★★★★★★★★★ "呼叫API並上傳資料" ★★★★★★★★★★★★★★★★★★★★

                    Dim URL As String = "https://scmapi.momoshop.com.tw/api/v1/goods/basic_code/ecIndex/D1102.scm"

                    Try
                        Dim Z As String = MomoSCM_API.Request_MomoSCM_API("POST", URL, JSONobj)

                        '反饋處理
                        Dim responseJSON As Newtonsoft.Json.Linq.JContainer = Newtonsoft.Json.JsonConvert.DeserializeObject(Z)


                        For num3 As Integer = 0 To responseJSON.Count - 1 Step 1

                            Dim attrName = responseJSON(num3).Item("INDEX_NAME").ToString()
                            Dim indexNo = responseJSON(num3).Item("INDEX_NO").ToString()
                            Dim chosenItemNo = responseJSON(num3).Item("INDEX_ITEM_NO").ToString()
                            chosenItemNo = chosenItemNo.Replace(Convert.ToChar(9), ",")
                            Dim chosenItemName = responseJSON(num3).Item("ITEM_CONTENT").ToString()
                            chosenItemName = chosenItemName.Replace(Convert.ToChar(9), ",")
                            Dim CHECK_YN = responseJSON(num3).Item("CHECK_YN").ToString()



                            Dim sqlupdate As String = <sql>
                                                      INSERT INTO MomoshoppingSCM_API_AttrIndex (mainEcCategoryCode,attrName, indexNo, chosenItemNo,chosenItemName,CHECK_YN)
                                                      VALUES ('{0}','{1}', '{2}','{3}','{4}',{5})
                                                  </sql>
                            sqlupdate = String.Format(sqlupdate, ecCategoryCode, attrName, indexNo, chosenItemNo, chosenItemName, CHECK_YN)
                            EC.DB.ExecuteScalar(sqlupdate)
                        Next

                    Catch ex As Exception
                        CommonParameter.Message.errormsg += "<font style='color:red'>失敗，錯誤訊息 : " + ex.Message + "</font></br>"
                        Return CommonParameter.Message.errormsg
                    End Try

                Next

            Next

            Return "分類屬性資料更新成功"

        End Function

        '精技固定產地編碼為0086故不使用此API
        ''' <summary>
        ''' Momo產地資料更新。
        ''' <para>精技固定產地編碼為0086故不使用此API</para>
        ''' </summary>
        Public Shared Function POST_originCode_API() As String
            Dim exrequest = <a>{
                              "loginInfo": {
                                    "entpID": "97275166",
                                    "entpCode": "001005",
                                    "entpPwd": "12345678",
                                    "otpBackNo": "111"
                              },
                              "originName": "亞"
                            }
                        </a>


            Dim salesMethods() = {CommonParameter.loginInfo.Buyout.salesMethods, CommonParameter.loginInfo.Consignment.salesMethods}
            For Each salesMethod As String In salesMethods

                Dim requestdatalv1 As New Dictionary(Of String, Object)
                Dim requestdatalv2 As New Dictionary(Of String, Object)

                If salesMethod.ToString = "Buyout" Then

                    With requestdatalv2
                        .Add("entpID", CommonParameter.loginInfo.VATnumber)
                        .Add("entpCode", CommonParameter.loginInfo.Buyout.BuyoutCode)
                        .Add("entpPwd", CommonParameter.loginInfo.SCMpassword)
                        .Add("otpBackNo", CommonParameter.loginInfo.Buyout.BuyoutOTPcode)
                    End With

                ElseIf salesMethod.ToString = "Consignment" Then

                    With requestdatalv2
                        .Add("entpID", CommonParameter.loginInfo.VATnumber)
                        .Add("entpCode", CommonParameter.loginInfo.Consignment.ConsignmentCode)
                        .Add("entpPwd", CommonParameter.loginInfo.SCMpassword)
                        .Add("otpBackNo", CommonParameter.loginInfo.Consignment.ConsignmentOTPcode)
                    End With

                End If

                With requestdatalv1
                    .Add("loginInfo", requestdatalv2)
                    .Add("originName", "亞")
                End With


                '完成上述結構化迴圈後將obj序列化為api所需json
                Dim JSONobj As String = Newtonsoft.Json.JsonConvert.SerializeObject(requestdatalv1)

                '★★★★★★★★★★★★★★★★★★★★ "呼叫API並上傳資料" ★★★★★★★★★★★★★★★★★★★★

                Dim URL = "https://scmapi.momoshop.com.tw/api/v1/goods/basic_code/origin/D1102.scm"
                Dim QA = <A>HTTP status code：
                    200　[GET]：OK，返回請求資訊。
                    201　[POST]：CREATED，新增或修改成功。
                    400 ：Bad Request，用戶發出的請求有誤，或Server尚未提供此服務。
                    401 ：Unauthorized，表示無權限。
                    422 ：Unprocesable entity，驗證錯誤。
                    500 ：INTERNAL SERVER ERROR，無預期錯誤。
                </A>
                Try
                    Dim Z As String = MomoSCM_API.Request_MomoSCM_API("POST", URL, JSONobj)
                    '將資料上傳時的時間從新塞入資料表
                    Dim sqlposteddate = "UPDATE MomoshoppingSCM_API_AttrIndex Set posteddate ='{0}' WHERE mainEcCategoryCode='{1}'"
                    sqlposteddate = String.Format(sqlposteddate, Now, "ecCategoryCode")
                    EC.DB.ExecuteScalar(sqlposteddate)
                Catch ex As Exception
                    CommonParameter.Message.errormsg += "<font style='color:red'>mainEcCategoryCode:" + "ecCategoryCode" + "失敗，錯誤訊息 : " + ex.Message + "</font></br>"
                    'Return CommonParameter.Message.errormsg
                End Try
                CommonParameter.Message.successmsg += "<font style='color:blue'>mainEcCategoryCode:" + "ecCategoryCode" + "上傳成功</font></br>"
                'Return CommonParameter.Message.successmsg






            Next
            'Return "暫無新資料需分類屬性查詢API"
        End Function

        ''' <summary>
        ''' Momo網路品牌資料更新。
        ''' <para>儲存至MomoshoppingSCM_API_webBrand</para>
        ''' <para>供連動選項使用。</para>
        ''' </summary>
        Public Shared Function POST_webbrand_API() As String
            Dim exrequest = <a>{
                              "loginInfo": {
                                      "entpID": "97275166",
                                      "entpCode": "001005",
                                      "entpPwd": "12345678",
                                      "otpBackNo": "111"
                                },
                                "brandEng": "test"
                            }
                        </a>
            Dim exresponse = <EX>
                            [
                                  {
                                       "QC_YN":"1",
                                       "BRAND_ENG":"Test",
                                       "BRAND_CHI":"歐蕾水凝保濕系列 0"
                                      ,"WEB_BRAND_NO":"B3"
                                   }
                                   ,{
                                       "QC_YN": "0",
                                       "BRAND_ENG": "testhwchu",
                                       "BRAND_CHI": "",
                                       "WEB_BRAND_NO": "20160620093053950"
                                    },{}…
                                   ]
                                           OR 
                                   {
                                     "ERROR": "webBrandNo、brandEng、brandChi至少填寫一樣參數。"
                                   }
                        </EX>

            Dim sql1 = <sql>
                       SELECT * FROM MomoshoppingSCM_API_webBrand WITH(NOLOCK)
                   </sql>
            Dim webBrandtb = EC.DB.ExecuteDataTable(sql1)

            For num As Integer = 0 To webBrandtb.Rows.Count - 1 Step 1

                Dim salesMethods() = {"寄賣"} '優先處理寄賣

                For num2 As Integer = 0 To salesMethods.Count - 1 Step 1

                    Dim obj As New Dictionary(Of String, Object)
                    obj.Add("loginInfo", New MomoSCM.Logininfo)

                    Dim BrandNames()
                    BrandNames = Split(webBrandtb.Rows.Item(num).Item("BrandName").ToString(), ",")

                    obj.Add("brandEng", BrandNames(0))

                    If salesMethods(num2) = "買斷" Then

                        obj.Item("loginInfo") = New MomoSCM.Logininfo
                        With obj.Item("loginInfo")
                            .entpCode = CommonParameter.loginInfo.Buyout.BuyoutCode
                            .entpID = CommonParameter.loginInfo.VATnumber
                            .entpPwd = CommonParameter.loginInfo.SCMpassword
                            .otpBackNo = CommonParameter.loginInfo.Buyout.BuyoutOTPcode
                        End With

                    ElseIf salesMethods(num2) = "寄賣" Then

                        obj.Item("loginInfo") = New MomoSCM.Logininfo
                        With obj.Item("loginInfo")
                            .entpCode = CommonParameter.loginInfo.Consignment.ConsignmentCode
                            .entpID = CommonParameter.loginInfo.VATnumber
                            .entpPwd = CommonParameter.loginInfo.SCMpassword
                            .otpBackNo = CommonParameter.loginInfo.Consignment.ConsignmentOTPcode
                        End With
                    End If

                    '完成上述結構化迴圈後將obj序列化為api所需json
                    Dim JSONobj As String = Newtonsoft.Json.JsonConvert.SerializeObject(obj)

                    '★★★★★★★★★★★★★★★★★★★★ "呼叫API並上傳資料" ★★★★★★★★★★★★★★★★★★★★

                    Dim URL As String = "https://scmapi.momoshop.com.tw/api/v1/goods/basic_code/web_brand/D1102.scm"

                    Try
                        Dim Z As String = MomoSCM_API.Request_MomoSCM_API("POST", URL, JSONobj)


                        '反饋處理
                        Dim responseJSON As Newtonsoft.Json.Linq.JContainer = Newtonsoft.Json.JsonConvert.DeserializeObject(Z)
                        For num3 As Integer = 0 To responseJSON.Count - 1 Step 1

                            If responseJSON.Item(num3).Item("BRAND_ENG").ToString = BrandNames(0) Then

                                Dim updatasql As String = <sql>
                                                          UPDATE MomoshoppingSCM_API_webBrand 
                                                          Set webBrandNo ='{0}' ,
                                                          BrandName ='{1}'
                                                          WHERE webBrandNo='{2}'
                                                      </sql>



                                Dim newwebBrandNo = responseJSON.Item(num3).Item("WEB_BRAND_NO").ToString
                                Dim newBrandName = responseJSON.Item(num3).Item("BRAND_ENG").ToString + "," + responseJSON.Item(num3).Item("BRAND_CHI").ToString
                                Dim oldwebBrandNo = webBrandtb.Rows.Item(num).Item("webBrandNo").ToString
                                oldwebBrandNo = oldwebBrandNo.Replace(vbLf, "")
                                updatasql = String.Format(updatasql, newwebBrandNo, newBrandName, oldwebBrandNo)

                                EC.DB.ExecuteScalar(updatasql)

                                '將資料上傳時的時間從新塞入資料表
                                Dim sqlposteddate = "UPDATE MomoshoppingSCM_API_webBrand Set posteddate ='{0}' WHERE webBrandNo='{1}'"
                                sqlposteddate = String.Format(sqlposteddate, Now, newwebBrandNo)
                                EC.DB.ExecuteScalar(sqlposteddate)

                            End If

                        Next



                    Catch ex As Exception
                        CommonParameter.Message.errormsg += "<font style='color:red'>失敗，錯誤訊息 : " + ex.Message + "</font></br>"
                        Return CommonParameter.Message.errormsg
                    End Try

                Next

            Next

            Return "網路品牌資料更新成功"

        End Function
    End Class
End Class

