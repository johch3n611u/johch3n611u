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

    '初始清單
    Public yahoolist As New Dictionary(Of String, String)
    Public yahooattr As New Dictionary(Of String, String)

    '初始類別
    Public yahooCategorysubs As String
    Public yahooCategorycats As String
    Public yahooCategorycatstext As String
    Public yahooCategorycatitems As String
    Public contentRatings As String
    '初始屬性
    Public level1 As String = ""
    Public level2 As String = ""
    Public attrnoncustom As String = ""
    Public attrcustom As String = ""
    '屬性ID
    Public struDataAttrClusterId As String = ""
    'model displayname
    Public lv1displayname As String = ""
    Public lv2displayname As String = ""
    '限制長度25新產品名稱
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

            '擷取Yahooshoppingscm list新增欄位
            Dim APIlistdtSql = String.Format("SELECT * FROM YahooshoppingSCM_API_list WITH(NOLOCK) WHERE ProductNo='{0}'", ProductNo)
            Dim APIlistdt = EC.DB.ExecuteDataTable(APIlistdtSql)
            If APIlistdt.Rows.Count > 0 Then

                With yahoolist

                    .Add("applicant", APIlistdt.Select().FirstOrDefault.Item("applicant").ToString())
                    .Add("contentRating", APIlistdt.Select().FirstOrDefault.Item("contentRating").ToString())
                    .Add("videos", APIlistdt.Select().FirstOrDefault.Item("videos").ToString())
                    .Add("images", APIlistdt.Select().FirstOrDefault.Item("images").ToString())
                    .Add("copy", APIlistdt.Select().FirstOrDefault.Item("copy").ToString())
                    .Add("displayName", APIlistdt.Select().FirstOrDefault.Item("displayName").ToString())
                    .Add("shortDescription_1", APIlistdt.Select().FirstOrDefault.Item("shortDescription_1").ToString())
                    .Add("shortDescription_2", APIlistdt.Select().FirstOrDefault.Item("shortDescription_2").ToString())
                    .Add("shortDescription_3", APIlistdt.Select().FirstOrDefault.Item("shortDescription_3").ToString())
                    .Add("shortDescription_4", APIlistdt.Select().FirstOrDefault.Item("shortDescription_4").ToString())
                    .Add("shortDescription_5", APIlistdt.Select().FirstOrDefault.Item("shortDescription_5").ToString())
                    .Add("subStationId", APIlistdt.Select().FirstOrDefault.Item("subStationId").ToString())
                    .Add("catItemId", APIlistdt.Select().FirstOrDefault.Item("catItemId").ToString())

                End With

            Else

                With yahoolist

                    .Add("applicant", "")
                    .Add("contentRating", "")
                    .Add("videos", "")
                    .Add("images", "")
                    .Add("copy", "")
                    .Add("displayName", "")
                    .Add("shortDescription_1", "")
                    .Add("shortDescription_2", "")
                    .Add("shortDescription_3", "")
                    .Add("shortDescription_4", "")
                    .Add("shortDescription_5", "")
                    .Add("subStationId", "")
                    .Add("catItemId", "")

                End With

            End If

            '擷取Yahooshoppingscm attr新增欄位
            Dim APIlistattrdtSql = String.Format("SELECT * FROM YahooshoppingSCM_API_list_attributes WITH(NOLOCK) WHERE ProductNo='{0}'", ProductNo)
            Dim APIlistattrdt = EC.DB.ExecuteDataTable(APIlistattrdtSql)


            '雅虎初始大類 ( 應該在迴圈內判斷並添加 selected )

            Dim yahooCategorysubdt = EC.DB.ExecuteDataTable("SELECT subID,subname FROM YahooshoppingSCM_API_Category WITH(NOLOCK) GROUP BY subID,subname")

            If yahoolist.Item("subStationId") <> "" Or yahoolist.Item("subStationId") <> Nothing Then

                Dim selectsql = String.Format("subId='{0}'", yahoolist.Item("subStationId"))
                yahooCategorysubs += "<option value='" + yahoolist.Item("subStationId") + "'>" + yahooCategorysubdt.Select(selectsql).FirstOrDefault.Item(1).ToString + "</option>"

            Else
                yahooCategorysubs += "<option value=''>未選擇</option>"
            End If

            For num As Integer = 0 To yahooCategorysubdt.Rows.Count - 1 Step 1
                yahooCategorysubs += "<option value='" + yahooCategorysubdt.Rows.Item(num).Item(0) + "'>" + yahooCategorysubdt.Rows.Item(num).Item(1) + "</option>"
            Next

            '雅虎初始中類 (因雅虎上架不需要中類故不做存取預設值)
            If yahoolist.Item("catItemId") <> "" Or yahoolist.Item("catItemId") <> Nothing Then
                yahooCategorycatstext = "<font style='color:red'>因雅虎上架資料不需存中類，故不記錄中類屬性。</font>"
            End If

            yahooCategorycats += "<option value='中類'>未選擇</option>"

            '雅虎初始小類 ( 應該在迴圈內判斷並添加 selected )

            Dim yahooCategorycatitemdt = EC.DB.ExecuteDataTable("SELECT catItemId,catItemIdname FROM YahooshoppingSCM_API_Category WITH(NOLOCK) GROUP BY catItemId,catItemIdname")

            If yahoolist.Item("catItemId") <> "" Or yahoolist.Item("catItemId") <> Nothing Then

                Dim selectsql = String.Format("catItemId='{0}'", yahoolist.Item("catItemId"))
                yahooCategorycatitems += "<option value='" + yahoolist.Item("catItemId") + "'>" + yahooCategorycatitemdt.Select(selectsql).FirstOrDefault.Item(1).ToString + "</option>"

            Else
                yahooCategorycatitems += "<option value=''>未選擇</option>"
            End If

            '雅虎初始內容級別

            If yahoolist.Item("contentRating") <> "" Or yahoolist.Item("contentRating") <> Nothing Then

                Dim contentRatingcontent
                Select Case yahoolist.Item("contentRating")
                    Case "G"
                        contentRatingcontent = "G:普級"
                    Case "PG"
                        contentRatingcontent = "PG:保護級"
                    Case "PG12"
                        contentRatingcontent = "PG12:輔導級12歲+"
                    Case "R"
                        contentRatingcontent = ">R:限制級"
                End Select

                contentRatings += String.Format("<option value='{0}'>{1}</option>", yahoolist.Item("contentRating"), contentRatingcontent)
            Else
                contentRatings += "<option value=''>未選擇</option>"
            End If

            contentRatings += "<option value ='G'>G:普級</option><option value='PG'>PG:保護級</option><option value='PG12'>PG12:輔導級12歲+</option><option value='R'>R:限制級</option>"




        '結構化屬性查詢

        'level 1 屬性

        Dim attrlevel1sql = "SELECT Top 1 * FROM YahooshoppingSCM_API_list_attributes WITH (NOLOCK) WHERE ProductNo ='{0}' AND [level] ='1'"
        attrlevel1sql = String.Format(attrlevel1sql, ProductNo)
        Dim level1dt = EC.DB.ExecuteDataTable(attrlevel1sql)

        If level1dt.Rows.Count > 0 Then
            level1 = level1dt.Select().FirstOrDefault.Item("Attrname").ToString()
        End If

        'level 2 屬性
        Dim attrlevel2sql = "SELECT Top 1 * FROM YahooshoppingSCM_API_list_attributes WITH (NOLOCK) WHERE ProductNo ='{0}' AND [level] ='2'"
        attrlevel2sql = String.Format(attrlevel2sql, ProductNo)
        Dim level2dt = EC.DB.ExecuteDataTable(attrlevel2sql)

        If level2dt.Rows.Count > 0 Then
            level2 = level2dt.Select().FirstOrDefault.Item("Attrname").ToString()
        End If

        '必填非自定義屬性
        Dim attrsql1 = "SELECT * FROM YahooshoppingSCM_API_list_attributes WITH (NOLOCK) WHERE ProductNo ='{0}' AND contenttype = 'attrnoncustom'"
        attrsql1 = String.Format(attrsql1, ProductNo)
        Dim dt = EC.DB.ExecuteDataTable(attrsql1)

        For num As Integer = 0 To dt.Rows.Count - 1 Step 1

                attrnoncustom += "<input style='border: 0' id='attrname{0}' name='attrname{0}' readonly='readonly' value='{1}' required='required'/> : <select id='attrnoncustom{0}' name='attrnoncustom{0}' required='required'>"

                attrnoncustom = String.Format(attrnoncustom, num, dt.Rows.Item(num).Item("Attrname").ToString())

                Dim Attrvalues = dt.Rows.Item(num).Item("Attrvalues").ToString()

                attrnoncustom += "<option value ='" + Attrvalues + "'>" + Attrvalues + "</option></select></br>"

        Next





        '必填自定義屬性
        Dim attrsql2 = "SELECT * FROM YahooshoppingSCM_API_list_attributes WITH (NOLOCK) WHERE ProductNo ='{0}' AND contenttype = 'attrcustom'"
        attrsql2 = String.Format(attrsql2, ProductNo)
        Dim dt2 = EC.DB.ExecuteDataTable(attrsql2)

        For num As Integer = 0 To dt2.Rows.Count - 1 Step 1

            Dim Attrvalues2 = dt2.Rows.Item(num).Item("Attrvalues").ToString()
                attrcustom += "<input style='border: 0' id='textattrname{0}' name='textattrname{0}' readonly='readonly' value='{1}' required='required'/> : <input id='attrcustom{0}' name='attrcustom{0}' value='{2}' required='required' />"

                attrcustom = String.Format(attrcustom, num, dt2.Rows.Item(num).Item("Attrname").ToString(), Attrvalues2)
                attrcustom += "</br>"

        Next

        '屬性ID
        Dim struDataAttrClusterIdsql = "SELECT struDataAttrClusterId FROM YahooshoppingSCM_API_list_attributes WITH (NOLOCK) WHERE ProductNo ='{0}'"
        struDataAttrClusterIdsql = String.Format(struDataAttrClusterIdsql, ProductNo)
        Dim struDataAttrClusterIddt = EC.DB.ExecuteDataTable(struDataAttrClusterIdsql)
        If struDataAttrClusterIddt.Rows.Count > 0 Then
            struDataAttrClusterId = struDataAttrClusterIddt.Select().FirstOrDefault.Item("struDataAttrClusterId").ToString
        End If

        '強制只有一組 model 所以 依照 level 1 與 2  必須再補兩組 lv1displayname lv2displayname
        Dim lv1displaynamesql = "SELECT TOP 1 displayName FROM YahooshoppingSCM_API_list_attributes WITH (NOLOCK) WHERE ProductNo ='{0}' AND level ='1'"
        lv1displaynamesql = String.Format(lv1displaynamesql, ProductNo)
        Dim lv1displaynamedt = EC.DB.ExecuteDataTable(lv1displaynamesql)
        If lv1displaynamedt.Rows.Count > 0 Then
            lv1displayname = lv1displaynamedt.Select().FirstOrDefault.Item("displayName").ToString
        End If

        Dim lv2displaynamesql = "SELECT TOP 1 displayName FROM YahooshoppingSCM_API_list_attributes WITH (NOLOCK) WHERE ProductNo ='{0}' AND level ='2'"
        lv2displaynamesql = String.Format(lv2displaynamesql, ProductNo)
        Dim lv2displaynamedt = EC.DB.ExecuteDataTable(lv2displaynamesql)
            If lv2displaynamedt.Rows.Count > 0 Then
                lv2displayname = lv2displaynamedt.Select().FirstOrDefault.Item("displayName").ToString
            End If

            '新產品名稱
            If APIlistdt.Rows.Count <> 0 Then

                If Convert.IsDBNull(APIlistdt.Select().FirstOrDefault.Item("NewProductName")) Then
                    NewProductName = Left(tb.ProductName.ToString(), 25)
                Else
                    NewProductName = APIlistdt.Select().FirstOrDefault.Item("NewProductName").ToString
                End If

            ElseIf dt.Rows.Count = 0 Then

                NewProductName = Left(tb.ProductName.ToString(), 25)

            End If

        End If

    End Sub

End Class
