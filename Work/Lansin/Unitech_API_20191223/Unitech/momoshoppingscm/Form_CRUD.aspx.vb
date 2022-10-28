'/////////////////////////////////////////////////////////////////////////////////
' 表單 (新增/修改)
'
' 建檔人員: 育誠
' 建檔日期: 2019-11-27
' 修改記錄: 範例--> 日期 記錄人員 簡述
' 關連程式: 
' 呼叫來源: 
'/////////////////////////////////////////////////////////////////////////////////
Imports EC.Library.Security
Imports System.Data

Partial Class mng_product_list_Form
    Inherits System.Web.UI.Page

    Public PrgID As String = ""
    Public prglimit As EC.mng.Limit       '讀取程式的權限
    Public _ID As Integer = 0
    Public tb As New Ls3c_v2_2005.list
    Public ProductNo As String = ""

    Public dt As DataTable = New DataTable
    Public MomowebBrand As String = ""
    Public Dn As New Dictionary(Of String, Object)
    Public MomomainEcCategoryCode As String = ""
    Public MomomAttrIndexCode As String = ""
    Public Momocol As New Dictionary(Of String, Object)
    Public MomoOldIMG As String()
    Public MomoIMGUrl As String = ""
    Public NewProductName As String = ""

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Response.CacheControl = "no-cache"             '避免被 Cache 住
        EC.mng.Login.LoginCheck()                      '未登入則導到登入頁
        prglimit = New EC.mng.Limit(ViewState)         '讀取程式的權限

        PrgID = SQLIJ(Request("PrgID"))                '主選單的MENUID,判斷權限使用.
        _ID = RequestNumeric("ID", RequestActMode.None, RequestMode.SQLInjection)

        '正常讀取頁面

        If Not IsNumeric(_ID) Then _ID = 0

        '取資料
        If _ID > 0 Then   'Modify

            tb = Ls3c_v2_2005.list.Load2012(_ID)
            tb.image = tb.image & ""
            ProductNo = String.Format("SELECT ProductNo FROM list WITH(NOLOCK) WHERE cno='{0}'", _ID)
            ProductNo = EC.DB.ExecuteDataTable(ProductNo).Select().FirstOrDefault.Item("ProductNo").ToString

            Dim SQL As String = <a>
                                    SELECT * FROM MomoshoppingSCM_API_list WITH(NOLOCK) WHERE ProductNo='{0}'
                                </a>
            SQL = String.Format(SQL, ProductNo)
            dt = EC.DB.ExecuteDataTable(SQL)

            '動態webBrand選項
            Dim sql2 = <sql>SELECT * FROM MomoshoppingSCM_API_webBrand WITH(NOLOCK)</sql>
            Dim dt2 = EC.DB.ExecuteDataTable(sql2)

            If dt.Rows.Count <> 0 Then
                Dim sql3 As String = "webBrandNo = {0}"
                Dim aa As String = CStr(dt.Select().FirstOrDefault.Item("webBrandNo"))
                sql3 = String.Format(sql3, dt.Select().FirstOrDefault.Item("webBrandNo").ToString)
                '奇怪的bug上面至換取值後會造成值多一個空白
                sql3 = sql3.Replace(" ", "")

                Dim webBrandNo = dt.Select().FirstOrDefault.Item("webBrandNo").ToString()
                Dim BrandName = dt2.Select(sql3).FirstOrDefault.Item("BrandName").ToString()

                MomowebBrand += "<option selected='true' value='" + webBrandNo + "'>" + BrandName + "</option>"
            End If
            For num As Integer = 0 To dt2.Rows.Count - 1 Step 1
                MomowebBrand += "<option value='" + dt2.Rows.Item(num).Item("webBrandNo").ToString + "'>" + dt2.Rows.Item(num).Item("BrandName").ToString + "</option>"
            Next

            '動態mainEcCategoryCode選項
            Dim sql4 = <sql>SELECT * FROM MomoshoppingSCM_API_Category WITH(NOLOCK)</sql>
            Dim dt3 = EC.DB.ExecuteDataTable(sql4)
            If dt.Rows.Count <> 0 Then
                Dim sql5 = "mainEcCategoryCode = '" + dt.Select().FirstOrDefault.Item("mainEcCategoryCode").ToString + "'"
                MomomainEcCategoryCode += "<option selected='true' value='" + dt.Select().FirstOrDefault.Item("mainEcCategoryCode").ToString + "'>" + dt3.Select(sql5).FirstOrDefault.Item("CategoryName").ToString + "</option>"
            End If

            For num As Integer = 0 To dt3.Rows.Count - 1 Step 1
                MomomainEcCategoryCode += "<option value='" + dt3.Rows.Item(num).Item("mainEcCategoryCode").ToString + "'>" + dt3.Rows.Item(num).Item("CategoryName").ToString + "</option>"
            Next


            '既有資料

            Dim attrsql As String = <sql>
                                        SELECT * FROM MomoshoppingSCM_API_list_attributes WITH(NOLOCK) WHERE ProductNo ='{0}'
                                    </sql>

            attrsql = String.Format(attrsql, ProductNo)
            Dim attrdt = EC.DB.ExecuteDataTable(attrsql)
            If attrdt.Rows.Count > 0 Then
                'MomomAttrIndex商品屬性連動選項
                For num2 As Integer = 0 To attrdt.Rows.Count - 1 Step 1

                    Dim tag = "<input style ='display:none;' id='indexNo_{0}' name='indexNo_{0}' value='{1}' required='required'/>" +
                       "<span>{2}</span> : <select id='select_{0}' name='select_{0}' required='required'><option selected='selected' value='{3}'>{4}</option></select></br>"

                    Dim indexNo = attrdt.Rows.Item(num2).Item("indexNo").ToString
                    Dim chosenItemNo = attrdt.Rows.Item(num2).Item("chosenItemNos").ToString

                    Dim Namesql As String = <sql>
                                  SELECT * FROM MomoshoppingSCM_API_AttrIndex WITH(NOLOCK) WHERE indexNo ='{0}'
                              </sql>
                    Namesql = String.Format(Namesql, indexNo)
                    Dim namedt = EC.DB.ExecuteDataTable(Namesql)
                    If namedt.Rows.Count > 0 Then


                        Dim attrName = namedt.Select().FirstOrDefault.Item("attrName").ToString

                        Dim newnamedt As New DataTable
                        Dim newRow As DataRow = Nothing
                        newnamedt.Columns.Add("chosenItemNo")
                        newnamedt.Columns.Add("chosenItemName")


                        Dim chosenItemNos() = Split((namedt.Rows.Item(0).Item("chosenItemNo").ToString), ",")
                        Dim chosenItemNames() = Split((namedt.Rows.Item(0).Item("chosenItemName").ToString), ",")

                        For num3 As Integer = 0 To chosenItemNos.Count - 1 Step 1
                            newRow = newnamedt.NewRow
                            newRow(0) = chosenItemNos(num3)
                            newRow(1) = chosenItemNames(num3)
                            newnamedt.Rows.Add(newRow)
                        Next

                        Dim selectsql = "chosenItemNo='{0}'"
                        selectsql = String.Format(selectsql, chosenItemNo)

                        Dim chosenItemName = newnamedt.Select(selectsql).FirstOrDefault.Item("chosenItemName").ToString

                        tag = String.Format(tag, num2, indexNo, attrName, chosenItemNo, chosenItemName)

                        MomomAttrIndexCode += tag


                    End If
                Next

                'col商品規格選項 類似雅虎Lv1,Lv2 但無固定值只有固定選項
                Dim colSeq1 = Split(attrdt.Select().FirstOrDefault.Item("colSeq1").ToString(), ",")
                Dim html1 As String = "<option value='{0}' selected='selected'>{1}</option>"
                html1 = String.Format(html1, colSeq1(0), colSeq1(1))
                Momocol.Add("colSeq1", html1 + "")

                Dim colDetail1 = attrdt.Select().FirstOrDefault.Item("colDetail1").ToString()
                Momocol.Add("colDetail1", colDetail1 + "")



                Dim colSeq2 = Split(attrdt.Select().FirstOrDefault.Item("colSeq2").ToString(), ",")
                Dim html2 As String = "<option value='{0}' selected='selected'>{1}</option>"
                html2 = String.Format(html2, colSeq2(0), colSeq2(1))
                Momocol.Add("colSeq2", html2 + "")

                Dim colDetail2 = attrdt.Select().FirstOrDefault.Item("colDetail2").ToString()
                Momocol.Add("colDetail2", colDetail2 + "")

            Else
                MomomAttrIndexCode = "<select required = 'required' <option value=''>未填寫</option></select>"
                Momocol.Add("colSeq1", "")
                Momocol.Add("colDetail1", "")
                Momocol.Add("colSeq2", "")
                Momocol.Add("colDetail2", "")
            End If

            '初始項目
            If dt.Rows.Count <> 0 Then
                Dn.Add("isPrompt", dt.Select().FirstOrDefault.Item("isPrompt").ToString)
                Dn.Add("isGift", dt.Select().FirstOrDefault.Item("isGift").ToString)
                Dn.Add("mainEcCategoryCode", dt.Select().FirstOrDefault.Item("mainEcCategoryCode").ToString)
                Dn.Add("webBrandNo", dt.Select().FirstOrDefault.Item("webBrandNo").ToString)
                Dn.Add("goodsType", dt.Select().FirstOrDefault.Item("goodsType").ToString)
                Dn.Add("temperatureType", dt.Select().FirstOrDefault.Item("temperatureType").ToString)
                Dn.Add("width", dt.Select().FirstOrDefault.Item("width").ToString)
                Dn.Add("length", dt.Select().FirstOrDefault.Item("length").ToString)
                Dn.Add("height", dt.Select().FirstOrDefault.Item("height").ToString)
                Dn.Add("weight", dt.Select().FirstOrDefault.Item("weight").ToString)
                Dn.Add("isECWarehouse", dt.Select().FirstOrDefault.Item("isECWarehouse").ToString)
                Dn.Add("isPointReachDate", dt.Select().FirstOrDefault.Item("isPointReachDate").ToString)
                Dn.Add("main_achievement", dt.Select().FirstOrDefault.Item("main_achievement").ToString)
                Dn.Add("agreed_delivery_yn", dt.Select().FirstOrDefault.Item("agreed_delivery_yn").ToString)
                Dn.Add("tax_yn", dt.Select().FirstOrDefault.Item("tax_yn").ToString)
                Dn.Add("disc_mach_yn", dt.Select().FirstOrDefault.Item("disc_mach_yn").ToString)
                Dn.Add("gov_subsidize_yn", dt.Select().FirstOrDefault.Item("gov_subsidize_yn").ToString)

                Dn.Add("hasAs", dt.Select().FirstOrDefault.Item("hasAs").ToString)
                Dn.Add("asDays", dt.Select().FirstOrDefault.Item("asDays").ToString)
                Dn.Add("asNote", dt.Select().FirstOrDefault.Item("asNote").ToString)
                Dn.Add("ecFirstQty", dt.Select().FirstOrDefault.Item("ecFirstQty").ToString)
                Dn.Add("ecMinQty", dt.Select().FirstOrDefault.Item("ecMinQty").ToString)
                Dn.Add("ecLeadTime", dt.Select().FirstOrDefault.Item("ecLeadTime").ToString)
                Dn.Add("isCommission", dt.Select().FirstOrDefault.Item("isCommission").ToString)
                Dn.Add("saleUnit", dt.Select().FirstOrDefault.Item("saleUnit").ToString)
                Dn.Add("expDays", dt.Select().FirstOrDefault.Item("expDays").ToString)
                Dn.Add("isAcceptTravelCard", dt.Select().FirstOrDefault.Item("isAcceptTravelCard").ToString)
                Dn.Add("isIncludeInstall", dt.Select().FirstOrDefault.Item("isIncludeInstall").ToString)
            Else
                Dn.Add("isPrompt", "")
                Dn.Add("isGift", "")
                Dn.Add("mainEcCategoryCode", "")
                Dn.Add("webBrandNo", "")
                Dn.Add("goodsType", "")
                Dn.Add("temperatureType", "")
                Dn.Add("width", "")
                Dn.Add("length", "")
                Dn.Add("height", "")
                Dn.Add("weight", "")
                Dn.Add("isECWarehouse", "")
                Dn.Add("isPointReachDate", "")
                Dn.Add("main_achievement", "")
                Dn.Add("agreed_delivery_yn", "")
                Dn.Add("tax_yn", "")
                Dn.Add("disc_mach_yn", "")
                Dn.Add("gov_subsidize_yn", "")

                Dn.Add("hasAs", "")
                Dn.Add("asDays", "")
                Dn.Add("asNote", "")
                Dn.Add("ecFirstQty", "")
                Dn.Add("ecMinQty", "")
                Dn.Add("ecLeadTime", "")
                Dn.Add("isCommission", "")
                Dn.Add("saleUnit", "")
                Dn.Add("expDays", "")
                Dn.Add("isAcceptTravelCard", "")
                Dn.Add("isIncludeInstall", "")
            End If

            '圖片驗證
            MomoOldIMG = MomoSCM_API.MomoOldIMG_Process(ProductNo)
            If dt.Rows.Count > 0 Then
                Dim imgsql As String = "SELECT NewImg_Url FROM MomoshoppingSCM_API_list With(NOLOCK) WHERE ProductNo ='{0}'"
                imgsql = String.Format(imgsql, ProductNo)
                Dim imgdt = EC.DB.ExecuteDataTable(imgsql)
                If imgdt.Rows.Count > 0 Then
                    Dim NewImg_Url = imgdt.Select().FirstOrDefault.Item("NewImg_Url")
                    If Convert.IsDBNull(NewImg_Url) = False Then
                        MomoIMGUrl = NewImg_Url
                    End If
                End If
            End If

            '新產品名稱
            If dt.Rows.Count <> 0 Then
                If Convert.IsDBNull(dt.Select().FirstOrDefault.Item("NewProductName")) Then
                    NewProductName = Left(tb.ProductName.ToString(), 25)
                Else
                    NewProductName = dt.Select().FirstOrDefault.Item("NewProductName").ToString
                End If
            ElseIf dt.Rows.Count = 0 Then
                NewProductName = Left(tb.ProductName.ToString(), 25)
            End If

        End If

    End Sub

End Class
