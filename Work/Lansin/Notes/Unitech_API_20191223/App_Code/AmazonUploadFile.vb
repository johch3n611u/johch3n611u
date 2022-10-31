Imports System.IO
Imports System.Net
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports YahooSCM_API
Imports System.Linq
Imports System.Threading.Tasks

Public Class AmazonUploadFile

    Private Const _credentialUrl As String = "https://tw.buy.yahoo.com/api/fileUploader/v1/credentials"
    Private Const _putObjectUrl As String = "http://v3.eclifeapi.com.tw/api/Bucket/PutObject"
    Private Const _fileObjectUrl As String = "https://tw.buy.yahoo.com/api/fileUploader/v1/fileObjects/"

    ''' <summary>
    ''' yahoo 要求的加密格式
    ''' </summary>
    Enum ServerSideEncryptionList
        None
        AES256
        AWSKMS
    End Enum

    ''' <summary>
    ''' AWS伺服器位址
    ''' </summary>
    Enum RegionEndpointsList
        'The US East (Virginia) endpoint.
        USEast1
        '///
        '/// Summary:
        '///     The Middle East (Bahrain) endpoint.
        MESouth1
        '///
        '/// Summary:
        '///     The Canada (Central) endpoint.
        CACentral1
        '//
        '/// Summary:
        '//     The China (Ningxia) endpoint.
        CNNorthWest1
        '//
        '/// Summary:
        '//     The China (Beijing) endpoint.
        CNNorth1
        '//
        '/// Summary:
        '//     The US GovCloud West (Oregon) endpoint.
        USGovCloudWest1
        '//
        '/// Summary:
        '//     The US GovCloud East (Virginia) endpoint.
        USGovCloudEast1
        '//
        '/// Summary:
        '//     The South America (Sao Paulo) endpoint.
        SAEast1
        '//
        '/// Summary:
        '//     The Asia Pacific (Singapore) endpoint.
        APSoutheast1
        '//
        '/// Summary:
        '//     The Asia Pacific (Mumbai) endpoint.
        APSouth1
        '//
        '/// Summary:
        '//     The Asia Pacific (Osaka-Local) endpoint.
        APNortheast3
        '//
        '/// Summary:
        '//     The Asia Pacific (Sydney) endpoint.
        APSoutheast2
        '//
        '/// Summary:
        '//     The Asia Pacific (Tokyo) endpoint.
        APNortheast1
        '//
        '/// Summary:
        '//     The US East (Ohio) endpoint.
        USEast2
        '//
        '/// Summary:
        '//     The Asia Pacific (Seoul) endpoint.
        APNortheast2
        '//
        '/// Summary:
        '//     The US West (Oregon) endpoint.
        USWest2
        '//
        '/// Summary:
        '//     The EU North (Stockholm) endpoint.
        EUNorth1
        '//
        '/// Summary:
        '//     The EU West (Ireland) endpoint.
        EUWest1
        '//
        '/// Summary:
        '//     The US West (N. California) endpoint.
        USWest1
        '//
        '/// Summary:
        '//     The EU West (Paris) endpoint.
        EUWest3
        '//
        '/// Summary:
        '//     The EU Central (Frankfurt) endpoint.
        EUCentral1
        '//
        '/// Summary:
        '//     The Asia Pacific (Hong Kong) endpoint.
        APEast1
        '//
        '/// Summary:
        '//     The EU West (London) endpoint.
        EUWest2
    End Enum

    ''' <summary>
    ''' 上傳AWS API資料格式
    ''' </summary>
    Public Class PutObjectModel
        Public Property AwsAccessKeyId As String
        Public Property AwsSecretAccessKey As String
        Public Property AwsSessionToken As String
        Public Property BucketName As String
        Public Property FileUrl As String
        Public Property FileUploadPath As String
        Public Property Metadata As Dictionary(Of String, String) = New Dictionary(Of String, String)
        Public Property SideEncryption As ServerSideEncryptionList
        Public Property RegionEndpoint As RegionEndpointsList
    End Class

    ''' <summary>
    ''' AWS 檔案位址
    ''' </summary>
    Public Class FileObjectsModel
        <Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification:="<Pending>")>
        Public Property id As String
        <Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification:="<Pending>")>
        Public Property url As String
        <Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification:="<Pending>")>
        Public Property expiredTs As DateTime
    End Class

    ''' <summary>
    ''' Yahoo登入後的憑證
    ''' </summary>
    Public Class YahooRequestData
        Public Property Cookies As CookieCollection
        Public Property WSSID As String
    End Class

    Private Shared _YahooResquestData As YahooRequestData = Nothing
    Public Shared Property YahooResquestData As YahooRequestData
        Get
            If _YahooResquestData Is Nothing Then
                _YahooResquestData = New YahooRequestData With {
                    .Cookies = Get_YahooSCM_CookieCollection()
                }
                _YahooResquestData.WSSID = Get_YahooSCM_WSSID_token(_YahooResquestData.Cookies)
            End If

            Return _YahooResquestData
        End Get
        Set
        End Set
    End Property

    ''' <summary>
    ''' 上傳檔案至AWS並取回檔案Url
    ''' </summary>
    ''' <param name="url"></param>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    Public Function GenAWSUrlByUrl(url As String, fileName As String, wssid As String, cookie As CookieCollection) As FileObjectsModel
        Dim cookieCollection As CookieCollection = cookie
        Dim mWSSID As String = wssid
        Dim headerDic = New Dictionary(Of String, String) From {
            {"X-YahooWSSID-Authorization", mWSSID}
        }
        Dim credentials As String = SendRequest("GET", _credentialUrl, "", cookieCollection, headerDic)
        Dim Obj As JObject = JsonConvert.DeserializeObject(credentials)
        Dim id As String = Obj.Item("id").ToString
        Dim key As String = Obj.Item("key").ToString
        Dim token As String = Obj.Item("token").ToString
        Dim putObject As PutObjectModel = New PutObjectModel With {
            .AwsAccessKeyId = id,
            .AwsSecretAccessKey = key,
            .AwsSessionToken = token,
            .BucketName = "ytw-shp-file-uploader",
            .FileUrl = url,
            .FileUploadPath = "FileUploader/" + fileName,
            .SideEncryption = ServerSideEncryptionList.AES256,
            .RegionEndpoint = RegionEndpointsList.APNortheast1
        }
        putObject.Metadata.Add("YahooWSSID-Authorization", mWSSID)
        Dim etag As String = UploadFile(putObject)
        Dim fileObject As FileObjectsModel = GetFileObje(etag, cookieCollection, headerDic)
        Return fileObject
    End Function

    ''' <summary>
    ''' 上傳檔案至 Amazon S3 回傳 etag
    ''' </summary>
    ''' <param name="putObject"></param>
    ''' <returns></returns>
    Public Function UploadFile(putObject As PutObjectModel) As String
        Dim request As HttpWebRequest = WebRequest.Create(_putObjectUrl)
        request.Method = "POST"
        request.ContentType = "application/json"
        '禁止轉址
        request.AllowAutoRedirect = False
        Dim JSONobj As String = JsonConvert.SerializeObject(putObject)
        '內容轉為資料流
        If JSONobj <> "" Or JSONobj <> Nothing Then
            Dim st As Stream = request.GetRequestStream()
            Dim byteArray As Byte() = Encoding.Default.GetBytes(JSONobj)
            st.Write(byteArray, 0, byteArray.Length)
        End If

        Try
            Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                '回傳資料解碼
                Using reader = New StreamReader(response.GetResponseStream(), Encoding.UTF8)
                    Dim responseText As String = reader.ReadToEnd()
                    '回傳API JObject
                    Dim etag = JsonConvert.DeserializeObject(responseText).Item("data").Item("eTag").ToString
                    Return etag
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' 取回Amazon S3上的檔案資訊
    ''' </summary>
    ''' <param name="eTag"></param>
    ''' <param name="cookieCollection"></param>
    ''' <returns></returns>
    Public Function GetFileObje(eTag As String, cookieCollection As CookieCollection, headers As Dictionary(Of String, String)) As FileObjectsModel
        If Not String.IsNullOrWhiteSpace(eTag) Then
            'etag要清除"符號
            Dim objUrl As String = _fileObjectUrl + eTag.Trim(Chr(34))
            '查詢檔案url如果取得失敗最多重複10次，因為檔案上傳後，雖然可以取得etag但是還無法立即搜尋到
            Dim max As Integer = 10
            For index As Integer = 1 To max
                Try
                    Dim awsfileObjectStr As String = SendRequest("GET", objUrl, "", cookieCollection, headers)
                    Dim fileObjectData As FileObjectsModel = JsonConvert.DeserializeObject(Of FileObjectsModel)(awsfileObjectStr)
                    Return fileObjectData
                Catch ex As Exception
                    If index <> max Then
                        Task.Delay(500)
                    Else
                        Throw New Exception(ex.Message)
                    End If
                End Try
            Next
            Throw New Exception("eTag無效")
        Else
            Throw New Exception("eTag無效")
        End If
    End Function
    ''' <summary>
    ''' 上傳HTML內的圖檔案至AWS並取回檔案Url塞入HTML反傳
    ''' </summary>
    ''' <param name="text"></param>
    ''' <param name="wssid"></param>
    ''' <param name="cookie"></param>
    ''' <returns></returns>
    Public Function ModifyHtmlImgUrl(text As String, wssid As String, cookie As CookieCollection) As String
        Dim instance As New Regex("(?<=<img.+?src=(""|'))(?<imgsrc>.+?)(?=(""|'))")
        Dim match As MatchCollection = instance.Matches(text)
        For Each item As Match In match
            Try
                Dim res As FileObjectsModel = GenAWSUrlByUrl(item.Value, Path.GetFileName(item.Value), wssid, cookie)
                If res IsNot Nothing And Not String.IsNullOrWhiteSpace(res.url) Then
                    text = Regex.Replace(text, item.Value, res.url)
                Else
                    text = Regex.Replace(text, item.Value, "無法取得檔案，請確認檔案是否可以使用。")
                End If
            Catch ex As Exception
                text = Regex.Replace(text, item.Value, "無法取得檔案，請確認檔案是否可以使用。")
            End Try
        Next
        Return text
    End Function

    ''' <summary>
    ''' 發送Api請求
    ''' </summary>
    ''' <param name="Method"></param>
    ''' <param name="URL"></param>
    ''' <param name="JSONString"></param>
    ''' <param name="cookieCollection"></param>
    ''' <returns></returns>
    Public Function SendRequest(Method As String, URL As String, JSONString As String, cookieCollection As CookieCollection, headers As Dictionary(Of String, String)) As String
        '取得雅虎購物中心驗證cookie與wssid
        '***** 將cookie與wssid塞入封包 *****
        '實作 HttpWebRequest
        Dim request As HttpWebRequest = WebRequest.Create(URL) '丟入函數的URL參數
        request.CookieContainer = New CookieContainer()
        request.CookieContainer.Add(cookieCollection)

        If headers IsNot Nothing Then
            For Each header As KeyValuePair(Of String, String) In headers
                request.Headers.Add(header.Key, header.Value)
            Next
        End If
        'request.Headers.Add("X-YahooWSSID-Authorization", wssid)
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
            Using response As HttpWebResponse = CType(request.GetResponse(), HttpWebResponse)
                '回傳資料解碼
                Using reader = New StreamReader(response.GetResponseStream(), Encoding.UTF8)
                    Dim responseText As String = reader.ReadToEnd()
                    '回傳API JObject
                    Return responseText
                End Using
            End Using
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function
End Class
