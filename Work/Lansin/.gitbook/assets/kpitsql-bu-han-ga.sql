USE LS3C_V2_2005; --�ϥ�DB -- 10.0.11.1
GO

--�j��]�w
DECLARE @RunNum INT, --���榸��
@NowNum INT, --�ثe����
@DatebyLiu DATETIME; --���ɶ�

SET @RunNum = 1; --����ܴX�ѫe
SET @NowNum = 1; --�j���l��

--SET @YorDbyLiu = DateDiff( dd , ListOrder_Main.PostDate ,getdate()) ;
--��ɶ����ʤH��

WHILE @NowNum <= @RunNum
    BEGIN
        SET @DatebyLiu = @NowNum;
        SELECT COUNT(*) AS ��ɶ����ʤH��, 
               CONVERT(VARCHAR(100), GETDATE() - @NowNum, 23) AS ���
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
            --��ɶ��C�����ʭq�����
            --ORDER BY ListOrder_Main.memberno;
        ) AS DAYBUYCOUNT;
        SET @NowNum = @NowNum + 1;
    END;


--�j��]�w
--DECLARE @RunNum INT, --���榸��
--@NowNum INT, --�ثe����
--@DatebyLiu DATETIME; --���ɶ�

--SET @RunNum = 74; --����ܴX�ѫe
SET @NowNum = 1; --�j���l��
--��ɶ��q�浧��
WHILE @NowNum <= @RunNum
    BEGIN
        SET @DatebyLiu = @NowNum;
        SELECT COUNT(cno) AS ��ɶ��q�浧��, 
               CONVERT(VARCHAR(100), GETDATE() - @NowNum, 23) AS ���
        FROM ListOrder_Main
        WHERE Site = 'eclife'
              AND (SerialNo LIKE '0%'
                   OR SerialNo LIKE '5%')
              AND DATEDIFF(dd, ListOrder_Main.PostDate, GETDATE()) = @DatebyLiu
        SET @NowNum = @NowNum + 1;
    END;

--�j��]�w
--DECLARE @RunNum INT, --���榸��
--@NowNum INT, --�ثe����
--@DatebyLiu DATETIME; --���ɶ�

--SET @RunNum = 74; --����ܴX�ѫe
SET @NowNum = 1; --�j���l��
--��ɶ��d�߷�U�o�e�`�ƴ�^�ʭq��� = ��J�ʪ�������顨�����b�H��
WHILE @NowNum <= @RunNum
    BEGIN
        SET @DatebyLiu = @NowNum;
        SELECT(mp.success - mp.orderCount) AS ��ɶ���J�ʪ�����饼���b�H��, 
              CONVERT(VARCHAR(100), GETDATE() - @NowNum, 23) AS ���
        FROM CRM.dbo.mailSend_REPORT mp WITH(NOLOCK)
             JOIN CRM.dbo.mailSend_REC mr WITH(NOLOCK) ON mp.mRID = mr.ID
             JOIN CRM.dbo.mailCategory_list mcl WITH(NOLOCK) ON mr.mListNO = mcl.NO
        WHERE mListNO LIKE '%W1D%'
              AND DATEDIFF(dd, sendDate, GETDATE()) = @DatebyLiu;
        SET @NowNum = @NowNum + 1;
    END;
	

--�j��]�w
--DECLARE @RunNum INT, --���榸��
--@NowNum INT, --�ثe����
--@DatebyLiu DATETIME; --���ɶ�

--SET @RunNum = 74; --����ܴX�ѫe
SET @NowNum = 1; --�j���l��
----��ɶ���d���ѵ���
WHILE @NowNum <= @RunNum
    BEGIN
        SET @DatebyLiu = @NowNum;
        SELECT COUNT(cno) AS ��ɶ���d���ѵ���, 
               CONVERT(VARCHAR(100), GETDATE() - @NowNum, 23) AS ���
        FROM ListOrder_Main
        WHERE Site = 'eclife'
              AND DATEDIFF(dd, ListOrder_Main.PostDate, GETDATE()) = @DatebyLiu
              AND STATUS = 8
              AND TradeMode = 0
              AND memberno != '3003490' -- ���M����
              AND memberno != '3007210'-- ���Ʀ���
        SET @NowNum = @NowNum + 1;
    END;


--�j��]�w
--DECLARE @RunNum INT, --���榸��
--@NowNum INT, --�ثe����
--@DatebyLiu DATETIME; --���ɶ�

--SET @RunNum = 74; --����ܴX�ѫe
SET @NowNum = 1; --�j���l��
----��ɶ���d����
WHILE @NowNum <= @RunNum
    BEGIN
        SET @DatebyLiu = @NowNum;
        SELECT COUNT(cno) AS ��ɶ���d����, 
               CONVERT(VARCHAR(100), GETDATE() - @NowNum, 23) AS ���
        FROM ListOrder_Main
        WHERE Site = 'eclife'
              AND DATEDIFF(dd, ListOrder_Main.PostDate, GETDATE()) = @DatebyLiu
              AND TradeMode = 0
              AND memberno != '3003490' -- ���M����
              AND memberno != '3007210';-- ���Ʀ���
        SET @NowNum = @NowNum + 1;
    END;


	--�j��]�w
--DECLARE @RunNum INT, --���榸��
--@NowNum INT, --�ثe����
--@DatebyLiu DATETIME; --���ɶ�

--SET @RunNum = 74; --����ܴX�ѫe
SET @NowNum = 1; --�j���l��
----��ɶ�Web��d���ѵ���
WHILE @NowNum <= @RunNum
    BEGIN
        SET @DatebyLiu = @NowNum;
        SELECT COUNT(cno) AS ��ɶ�Web��d���ѵ���, 
               CONVERT(VARCHAR(100), GETDATE() - @NowNum, 23) AS ���
        FROM ListOrder_Main
        WHERE Site = 'eclife'
              AND DATEDIFF(dd, ListOrder_Main.PostDate, GETDATE()) = @DatebyLiu
              AND STATUS = 8
              AND TradeMode = 0
              AND memberno != '3003490' -- ���M����
              AND memberno != '3007210'-- ���Ʀ���
			  AND Left(SerialNo,2) ='09'
        SET @NowNum = @NowNum + 1;
    END;

--�j��]�w
--DECLARE @RunNum INT, --���榸��
--@NowNum INT, --�ثe����
--@DatebyLiu DATETIME; --���ɶ�

--SET @RunNum = 74; --����ܴX�ѫe
SET @NowNum = 1; --�j���l��
----��ɶ�APP��d���ѵ���
WHILE @NowNum <= @RunNum
    BEGIN
        SET @DatebyLiu = @NowNum;
        SELECT COUNT(cno) AS ��ɶ�APP��d���ѵ���, 
               CONVERT(VARCHAR(100), GETDATE() - @NowNum, 23) AS ���
        FROM ListOrder_Main
        WHERE Site = 'eclife'
              AND DATEDIFF(dd, ListOrder_Main.PostDate, GETDATE()) = @DatebyLiu
              AND STATUS = 8
              AND TradeMode = 0
              AND memberno != '3003490' -- ���M����
              AND memberno != '3007210'-- ���Ʀ���
			  AND Left(SerialNo,2) ='59'
			   AND Cooperation_Name = 'APP'
        SET @NowNum = @NowNum + 1;
    END;

--	--�j��]�w
----DECLARE @RunNum INT, --���榸��
----@NowNum INT, --�ثe����
----@DatebyLiu DATETIME; --���ɶ�

----SET @RunNum = 74; --����ܴX�ѫe
--SET @NowNum = 1; --�j���l��
----��ɶ�WEB�q�浧��
--WHILE @NowNum <= @RunNum
--    BEGIN
--        SET @DatebyLiu = @NowNum;
--        SELECT COUNT(cno) AS ��ɶ�WEB�q�浧��, 
--               CONVERT(VARCHAR(100), GETDATE() - @NowNum, 23) AS ���
--        FROM ListOrder_Main
--        WHERE Site = 'eclife'
--              AND (SerialNo LIKE '0%'
--                   OR SerialNo LIKE '5%')
--              AND DATEDIFF(dd, ListOrder_Main.PostDate, GETDATE()) = @DatebyLiu
--			  AND Left(SerialNo,2) ='09'
--        SET @NowNum = @NowNum + 1;
--    END;

	--�j��]�w
--DECLARE @RunNum INT, --���榸��
--@NowNum INT, --�ثe����
--@DatebyLiu DATETIME; --���ɶ�

--SET @RunNum = 74; --����ܴX�ѫe
SET @NowNum = 1; --�j���l��
--��ɶ�APP�q�浧��
WHILE @NowNum <= @RunNum
    BEGIN
        SET @DatebyLiu = @NowNum;
        SELECT COUNT(cno) AS ��ɶ�APP�q�浧��, 
               CONVERT(VARCHAR(100), GETDATE() - @NowNum, 23) AS ���
        FROM ListOrder_Main
        WHERE Site = 'eclife'
              AND (SerialNo LIKE '0%'
                   OR SerialNo LIKE '5%')
              AND DATEDIFF(dd, ListOrder_Main.PostDate, GETDATE()) = @DatebyLiu
			  AND Left(SerialNo,2) ='59'
			  AND Cooperation_Name = 'APP'
        SET @NowNum = @NowNum + 1;
    END;