:: 參考
:: https://blog.miniasp.com/post/2009/11/03/How-to-get-system-date-time-in-batch-file-part-III
:: https://edisonx.pixnet.net/blog/post/57090736
:: https://davidhu0903ex3.pixnet.net/blog/post/443069270-%5B%E6%95%99%E5%AD%B8%5D-dos%E6%89%B9%E6%AC%A1%E6%AA%94%E8%A3%BD%E4%BD%9C---bat%E6%AA%94%E8%AA%9E%E6%B3%95
:: https://stackoverflow.com/questions/4252176/exclude-in-xcopy-just-for-a-file-type
REM 取得今天的年、月、日 (自動補零)
SET TodayYear=%date:~0,4%
SET TodayMonthP0=%date:~5,2%
SET TodayDayP0=%date:~8,2%

REM 取得今天的年、月、日 (純數字)
REM 2010/08/03 更新：以下是為了修正 Batch 遇到 08, 09 會視為八進位的問題
IF %TodayMonthP0:~0,1% == 0 (
	SET /A TodayMonth=%TodayMonthP0:~1,1%+0
) ELSE (
	SET /A TodayMonth=TodayMonthP0+0
)

IF %TodayMonthP0:~0,1% == 0 (
	SET /A TodayDay=%TodayDayP0:~1,1%+0
) ELSE (
	SET /A TodayDay=TodayDayP0+0
)

md %TodayYear%%TodayMonthP0%%TodayDayP0%\新增文件夾
xcopy 被複製的檔案路徑 %TodayYear%%TodayMonthP0%%TodayDayP0%\要複製的文件夾名稱 /S /exclude:uncopy.txt 排除該TXT內比對到的檔案

cmd