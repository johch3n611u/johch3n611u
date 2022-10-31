# 20191112

昨日更改報表內容

精技購物中心反傳

## 使用 POST 要求與使用 Cookie 進行傳送與接收 呼叫 Web API

HttpClient 類別 才能實作

WebClient 類別 無法所以 nuget 引入 system.net.http

以上太麻煩 轉回使用 cookiecontainer

終於第一步登入完成

下班前 cookie 取 token 與 其他api也成功

[http://storynsong01.pixnet.net/blog/post/116435414-%E3%80%90vba%E3%80%91%E5%B8%B8%E7%94%A8vb%E5%AD%97%E4%B8%B2%E8%99%95%E7%90%86%E5%87%BD%E6%95%B8](http://storynsong01.pixnet.net/blog/post/116435414-%E3%80%90vba%E3%80%91%E5%B8%B8%E7%94%A8vb%E5%AD%97%E4%B8%B2%E8%99%95%E7%90%86%E5%87%BD%E6%95%B8)

```text
#Region "YahooShoppingSCM_API_request_header_加密方式"
    '依雅虎購物商城api_sign in_WSSID規格需求加密
    Public Function YSSCM_header_forcookic()

        'HTTP_request需求包含4個 header (api-token、api-version、api-timesta、api-signture)與 Credential密文(加密後的cookie)
        '雅虎購物中心API_KEY查詢 : https://scm.monday.com.tw/ApprovalForm/Query/ApiKeyQuery.aspx

        'request實際欄位名稱為 api-timestamp 為Request發送當下的UnixTimestamp，例如1548225833，此Timestamp的有效期限為90秒
        Dim timestamp As String = Int((DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds) '1573455721 
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
        '擷取response cookie
        Dim _sp As String = response.Cookies.Item("_sp").ToString
        _sp = Microsoft.VisualBasic.Mid(_sp, 5, _sp.Length)


    End Function

#End Region

End Class
```

{% embed url="https://dotblogs.com.tw/joysdw12/archive/2012/12/04/85380.aspx" caption="" %}

{% embed url="https://docs.microsoft.com/zh-tw/dotnet/api/system.net.httpwebrequest.cookiecontainer?view=netframework-4.8" caption="" %}

{% embed url="https://dotblogs.com.tw/shadow/2017/12/06/223813" caption="" %}

{% embed url="https://csharpkh.blogspot.com/2017/10/c-httpclient-webapi-11-post-cookie-web.html" caption="" %}

{% embed url="https://docs.microsoft.com/zh-tw/dotnet/api/system.net.http.httpclient?view=netframework-4.8" caption="" %}

![](https://github.com/johch3n611u/EC_Web-AP_Developer/tree/095f673ceb3c1661899447a7223f2f55012c6b3d/.gitbook/assets/image%20%28122%29.png)

```text
USE LS3C_V2_2005; --使用DB -- 10.0.11.1
GO

--迴圈設定
DECLARE @RunNum INT, --執行次數
@NowNum INT, --目前次數
@DatebyLiu DATETIME; --當日時間

SET @RunNum = 1; --執行至幾天前
SET @NowNum = 1; --迴圈初始值

--SET @YorDbyLiu = DateDiff( dd , ListOrder_Main.PostDate ,getdate()) ;
--當時間首購人數

WHILE @NowNum <= @RunNum
    BEGIN
        SET @DatebyLiu = @NowNum;
        SELECT COUNT(*) AS 當時間首購人數, 
               CONVERT(VARCHAR(100), GETDATE() - @NowNum, 23) AS 日期
        FROM
        (
            SELECT ListOrder_Main.memberno, 
                   m.Name
            FROM ListOrder_Main
                 LEFT JOIN Member m ON m.cno = ListOrder_Main.MemberNo
            WHERE m.Name IS NOT NULL
                  AND (SerialNo LIKE '0%'
                       OR SerialNo LIKE '5%')
                  AND DATEDIFF(dd, ListOrder_Main.PostDate, GETDATE()) = @DatebyLiu
            GROUP BY ListOrder_Main.memberno, 
                     m.Name, 
                     ListOrder_Main.[Status]
            HAVING COUNT(ListOrder_Main.memberno) = 1
                   AND ListOrder_Main.[Status] IN(0,1, 2, 3)
            --當時間每筆首購訂單明細
            --ORDER BY ListOrder_Main.memberno;
        ) AS DAYBUYCOUNT;
        SET @NowNum = @NowNum + 1;
    END;


--迴圈設定
--DECLARE @RunNum INT, --執行次數
--@NowNum INT, --目前次數
--@DatebyLiu DATETIME; --當日時間

--SET @RunNum = 74; --執行至幾天前
SET @NowNum = 1; --迴圈初始值
--當時間訂單筆數
WHILE @NowNum <= @RunNum
    BEGIN
        SET @DatebyLiu = @NowNum;
        SELECT COUNT(cno) AS 當時間訂單筆數, 
               CONVERT(VARCHAR(100), GETDATE() - @NowNum, 23) AS 日期
        FROM ListOrder_Main
        WHERE Site = 'eclife'
              AND (SerialNo LIKE '0%'
                   OR SerialNo LIKE '5%')
              AND DATEDIFF(dd, ListOrder_Main.PostDate, GETDATE()) = @DatebyLiu
        SET @NowNum = @NowNum + 1;
    END;

--迴圈設定
--DECLARE @RunNum INT, --執行次數
--@NowNum INT, --目前次數
--@DatebyLiu DATETIME; --當日時間

--SET @RunNum = 74; --執行至幾天前
SET @NowNum = 1; --迴圈初始值
--當時間查詢當下發送總數減掉回購訂單數 = 放入購物車”當日”未結帳人數
WHILE @NowNum <= @RunNum
    BEGIN
        SET @DatebyLiu = @NowNum;
        SELECT(mp.success - mp.orderCount) AS 當時間放入購物車當日未結帳人數, 
              CONVERT(VARCHAR(100), GETDATE() - @NowNum, 23) AS 日期
        FROM CRM.dbo.mailSend_REPORT mp WITH(NOLOCK)
             JOIN CRM.dbo.mailSend_REC mr WITH(NOLOCK) ON mp.mRID = mr.ID
             JOIN CRM.dbo.mailCategory_list mcl WITH(NOLOCK) ON mr.mListNO = mcl.NO
        WHERE mListNO LIKE '%W1D%'
              AND DATEDIFF(dd, sendDate, GETDATE()) = @DatebyLiu;
        SET @NowNum = @NowNum + 1;
    END;


--迴圈設定
--DECLARE @RunNum INT, --執行次數
--@NowNum INT, --目前次數
--@DatebyLiu DATETIME; --當日時間

--SET @RunNum = 74; --執行至幾天前
SET @NowNum = 1; --迴圈初始值
----當時間刷卡失敗筆數
WHILE @NowNum <= @RunNum
    BEGIN
        SET @DatebyLiu = @NowNum;
        SELECT COUNT(cno) AS 當時間刷卡失敗筆數, 
               CONVERT(VARCHAR(100), GETDATE() - @NowNum, 23) AS 日期
        FROM ListOrder_Main
        WHERE Site = 'eclife'
              AND DATEDIFF(dd, ListOrder_Main.PostDate, GETDATE()) = @DatebyLiu
              AND STATUS = 8
              AND TradeMode = 0
              AND memberno != '3003490' -- 秉霖測試
              AND memberno != '3007210'-- 筆數扣除
        SET @NowNum = @NowNum + 1;
    END;


--迴圈設定
--DECLARE @RunNum INT, --執行次數
--@NowNum INT, --目前次數
--@DatebyLiu DATETIME; --當日時間

--SET @RunNum = 74; --執行至幾天前
SET @NowNum = 1; --迴圈初始值
----當時間刷卡筆數
WHILE @NowNum <= @RunNum
    BEGIN
        SET @DatebyLiu = @NowNum;
        SELECT COUNT(cno) AS 當時間刷卡筆數, 
               CONVERT(VARCHAR(100), GETDATE() - @NowNum, 23) AS 日期
        FROM ListOrder_Main
        WHERE Site = 'eclife'
              AND DATEDIFF(dd, ListOrder_Main.PostDate, GETDATE()) = @DatebyLiu
              AND TradeMode = 0
              AND memberno != '3003490' -- 秉霖測試
              AND memberno != '3007210';-- 筆數扣除
        SET @NowNum = @NowNum + 1;
    END;


    --迴圈設定
--DECLARE @RunNum INT, --執行次數
--@NowNum INT, --目前次數
--@DatebyLiu DATETIME; --當日時間

--SET @RunNum = 74; --執行至幾天前
SET @NowNum = 1; --迴圈初始值
----當時間Web刷卡失敗筆數
WHILE @NowNum <= @RunNum
    BEGIN
        SET @DatebyLiu = @NowNum;
        SELECT COUNT(cno) AS 當時間Web刷卡失敗筆數, 
               CONVERT(VARCHAR(100), GETDATE() - @NowNum, 23) AS 日期
        FROM ListOrder_Main
        WHERE Site = 'eclife'
              AND DATEDIFF(dd, ListOrder_Main.PostDate, GETDATE()) = @DatebyLiu
              AND STATUS = 8
              AND TradeMode = 0
              AND memberno != '3003490' -- 秉霖測試
              AND memberno != '3007210'-- 筆數扣除
              AND Left(SerialNo,2) ='09'
        SET @NowNum = @NowNum + 1;
    END;

--迴圈設定
--DECLARE @RunNum INT, --執行次數
--@NowNum INT, --目前次數
--@DatebyLiu DATETIME; --當日時間

--SET @RunNum = 74; --執行至幾天前
SET @NowNum = 1; --迴圈初始值
----當時間APP刷卡失敗筆數
WHILE @NowNum <= @RunNum
    BEGIN
        SET @DatebyLiu = @NowNum;
        SELECT COUNT(cno) AS 當時間APP刷卡失敗筆數, 
               CONVERT(VARCHAR(100), GETDATE() - @NowNum, 23) AS 日期
        FROM ListOrder_Main
        WHERE Site = 'eclife'
              AND DATEDIFF(dd, ListOrder_Main.PostDate, GETDATE()) = @DatebyLiu
              AND STATUS = 8
              AND TradeMode = 0
              AND memberno != '3003490' -- 秉霖測試
              AND memberno != '3007210'-- 筆數扣除
              AND Left(SerialNo,2) ='59'
               AND Cooperation_Name = 'APP'
        SET @NowNum = @NowNum + 1;
    END;

--    --迴圈設定
----DECLARE @RunNum INT, --執行次數
----@NowNum INT, --目前次數
----@DatebyLiu DATETIME; --當日時間

----SET @RunNum = 74; --執行至幾天前
--SET @NowNum = 1; --迴圈初始值
----當時間WEB訂單筆數
--WHILE @NowNum <= @RunNum
--    BEGIN
--        SET @DatebyLiu = @NowNum;
--        SELECT COUNT(cno) AS 當時間WEB訂單筆數, 
--               CONVERT(VARCHAR(100), GETDATE() - @NowNum, 23) AS 日期
--        FROM ListOrder_Main
--        WHERE Site = 'eclife'
--              AND (SerialNo LIKE '0%'
--                   OR SerialNo LIKE '5%')
--              AND DATEDIFF(dd, ListOrder_Main.PostDate, GETDATE()) = @DatebyLiu
--              AND Left(SerialNo,2) ='09'
--        SET @NowNum = @NowNum + 1;
--    END;

    --迴圈設定
--DECLARE @RunNum INT, --執行次數
--@NowNum INT, --目前次數
--@DatebyLiu DATETIME; --當日時間

--SET @RunNum = 74; --執行至幾天前
SET @NowNum = 1; --迴圈初始值
--當時間APP訂單筆數
WHILE @NowNum <= @RunNum
    BEGIN
        SET @DatebyLiu = @NowNum;
        SELECT COUNT(cno) AS 當時間APP訂單筆數, 
               CONVERT(VARCHAR(100), GETDATE() - @NowNum, 23) AS 日期
        FROM ListOrder_Main
        WHERE Site = 'eclife'
              AND (SerialNo LIKE '0%'
                   OR SerialNo LIKE '5%')
              AND DATEDIFF(dd, ListOrder_Main.PostDate, GETDATE()) = @DatebyLiu
              AND Left(SerialNo,2) ='59'
              AND Cooperation_Name = 'APP'
        SET @NowNum = @NowNum + 1;
    END;
```

