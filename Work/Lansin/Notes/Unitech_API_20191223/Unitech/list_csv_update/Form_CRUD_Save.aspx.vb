'/////////////////////////////////////////////////////////////////////////////////
' 儲存 (新增/修改/刪除)
'
' 建檔人員: 阿友
' 建檔日期: 2013-08-28
' 修改記錄: 範例--> 日期 記錄人員 簡述
' 關連程式:
' 呼叫來源: 
'/////////////////////////////////////////////////////////////////////////////////
Imports System.IO
Imports EC.Library.Security
Partial Class mng_ShoppingCash_SC_UserList_Form_CRUD_Save
    Inherits System.Web.UI.Page

    Public prglimit As EC.mng.Limit       '讀取程式的權限
    Dim csvfile As System.Web.HttpPostedFile = HttpContext.Current.Request.Files("file1")
    Dim csv As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Response.CacheControl = "no-cache"             '避免被 Cache 住
        EC.mng.Login.LoginCheck()                      '未登入則導到登入頁

        Dim action As String = RequestString("action", RequestActMode.None, RequestMode.XSS)    '空值 or del

        Dim errStatus As String = ""
        Dim errMessage As String = ""

        prglimit = EC.mng.Limit.CheckLimit(ViewState)    '檢查使用者權限
        If prglimit.errStatus = "error" Then   '權限不符
            errStatus = prglimit.errStatus
            errMessage = prglimit.errMessage
        Else  '儲存
            If csvfile.ContentLength > 0 Then
                Try

                    '=======================================================================================
                    '將 CSV檔 HttpPostedFile 轉成 Byte() -> Stream -> TextReader --> String
                    '---------------------------------------------------------------------------------------
                    Dim _FileByte As Byte() = New Byte(csvfile.ContentLength - 1) {}
                    csvfile.InputStream.Read(_FileByte, 0, csvfile.ContentLength)   '將CSV檔案資料放入 byte()
                    Dim stream As Stream = New MemoryStream(_FileByte)
                    Dim tr As TextReader = New StreamReader(stream, Encoding.GetEncoding("BIG5"))
                    csv = tr.ReadToEnd
                    Dim data() As String = {}
                    '依CrLf分行
                    data = csv.Split(ControlChars.CrLf.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                    Dim head() As String = {}
                    head = data(0).Split(",")

                    If data(0) = "ProductNo,Yahoo_Report,Momo_Report" Then
                        '檔案存入資料庫
                        For num As Integer = 1 To data.Count - 1 Step 1
                            Dim content() As String = {}
                            content = data(num).Split(",")
                            'head等於csv第一行
                            'content為值
                            Dim repeatsearch As String = "SELECT COUNT(*) FROM Unitech_list WHERE ProductNo='{0}'"
                            repeatsearch = String.Format(repeatsearch, content(0))
                            '重複delete後
                            If EC.DB.ExecuteScalar(repeatsearch) > 0 Then
                                Dim sqldelete As String = "DELETE FROM Unitech_list WHERE {0} = '{1}'"
                                sqldelete = String.Format(sqldelete, head(0), content(0))
                                EC.DB.ExecuteScalar(sqldelete)
                            End If
                            '再insert
                            Dim sqlinsert = "INSERT INTO Unitech_list ({0},{1},{2}) VALUES('{3}','{4}','{5}')"
                            sqlinsert = String.Format(sqlinsert, head(0), head(1), head(2), content(0), content(1), content(2))
                            EC.DB.ExecuteScalar(sqlinsert)
                        Next

                        errStatus = "ok"
                        errMessage = "上傳檔案成功"
                    Else
                        errStatus = "錯誤"
                        errMessage = "檔案格式錯誤"

                    End If

                Catch ex As Exception
                    errStatus = "錯誤"
                    errMessage = "檔案格式錯誤"
                End Try

            End If

        End If

        Response.Write("[{status: '" & errStatus & "', message:'" & errMessage & "'}]")

    End Sub

End Class
