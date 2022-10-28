# 版本資訊

v1.

* 單機版本 <https://github.com/johch3n611u/Side-Project-Try-Some-Electron.net>
* Sheet.js <https://github.com/SheetJS/sheetjs>
* Datatable.js <https://datatables.net/>
* Electron.NET <https://github.com/ElectronNET/Electron.NET>
* 串接一般 .xlsx 檔案，基礎功能滿足報表查詢過濾，雖然靈活性較不足，但符合高度資安要求，無網路也可使用。

v2.

* 網路版本 <https://github.com/johch3n611u/Side-Project-GoogleSheets-to-WebDatabase>
* GoogleSheets API <https://developers.google.com/sheets/api>
* Datatables.js
* 串接 GoogleSheets，源碼放置於 Github Page，受限於前二開源，高度靈活性，滿足電腦與手機版本。

v3.

* Index / Login / GoogleSheets select book
* 由靜態串接改為動態串接，GoogleSheets 有多少 book 則有多少報表。

v4.

* User Feedback Issue
  * Show entries 100 default <https://stackoverflow.com/questions/10630853/change-values-of-select-box-of-show-10-entries-of-jquery-datatable>
  * Header Less Data More
    * Title Show Book Name
    * Input Label Turn To Placeholder
  * Info Change <https://stackoverflow.com/questions/45085026/how-to-change-datatables-text-showing-page-1-of-5-of-x-entries/45085239>
* W3.CSS Progress Bars <https://www.w3schools.com/w3css/w3css_progressbar.asp>
  * Progress Bars 因為 while loop 與 jq attr 問題無解，改由別種方式完成此功能。

v5.

* 以下內容目前尚未實作，等後續有需求再繼續。
* Export <https://datatables.net/extensions/buttons/examples/initialisation/export.html>
* RWD Table <https://www.ucamc.com/e-learning/css/420-table-rwd>
  * 抓取篩選資料，渲染成以下 RWD 格式 <https://stackoverflow.com/questions/33169649/how-to-get-filtered-data-result-set-from-jquery-datatable>
* RWD Example <https://www.sinyi.com.tw/buy/list>
* Inside Book Select Other Books
* IMG Support <https://stackoverflow.com/questions/46063474/how-to-display-image-in-datatable>
* Clear Search Button
  * 卡在 while loop 與 jq attr 問題無解，等後續有需求再繼續。

v6.

* 為了追蹤使用者資訊分析所以優先實作 GA <https://github.com/johch3n611u/Side-Project-GoogleSheets-to-WebDatabase/blob/main/HowToGoogleAnalytics.md>

vFuture.

* Pick Selected Export or DoSomeThing... <https://datatables.net/extensions/select/examples/api/get.html>
* Chart.js 仿 BI 報表分析
* Line API 相關應用
