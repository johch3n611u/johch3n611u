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

--	--迴圈設定
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
--			  AND Left(SerialNo,2) ='09'
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