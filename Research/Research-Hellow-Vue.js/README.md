# Try some Vue.js

## Demo

* CodePen <https://codepen.io/johch3n611u/full/MWaGrgw>

* [此 Windows 98 樣式為開源 98.css 庫，並非是用 Winform 所撰寫。](https://github.com/jdan/98.css?fbclid=IwAR1rCsmipzAA5RBq3RInac7Pz4pCKIRp8Y0OzBRi-LsdSGlNplcgw2o4NMQ)

![IMAGE](https://github.com/johch3n611u/Side-Project-Hellow-Vue.js/blob/master/IMG/vuetodolistdemo.gif)

## 序

以學過 Angular 的角度來看 Vue 上手速度算是蠻快的，基本上熟悉文件配置，使用方式與組成都很類似，

差別似乎在於 ng 每次都需要 Ahead-of-Time 或 Just-in-Time 將 TS 編譯為瀏覽器能讀懂的程式碼，而 Vue 則是像函式庫一樣的引用，

在這簡易做一個 todolist demo ，有時間在上到能夠在 firebase 儲存狀態。

## 過程

在看過官方文件與一些參考後，寫了[筆記](https://github.com/johch3n611u/Side-Project-Hellow-Vue.js/tree/master/StudyProject/Vue%20Basis)，

總結來說看完 Basis 可以感受到確實比 ng 輕，但這也只是基本應用而已，延伸應用加下去感覺跟 ng 比也是不相上下，

而 Vue 進階使用也有如 ng .ts 檔案的 [单文件组件 .vue](https://cn.vuejs.org/v2/guide/single-file-components.html)，

也就是說也必須用 webpack 或 Browserify 等构建工具進行編譯，且許多應用如 ng 一樣，可能要邊做遇到問題時在了解才會學得快。

## Todo-List Demo

### Branch

#### Now

* Version 1. todolist-simple (master)

#### Expected

*  ̶V̶e̶r̶s̶i̶o̶n̶ ̶2̶.̶ ̶S̶C̶S̶S̶ ̶/̶ ̶W̶e̶b̶p̶a̶c̶k̶ ̶U̶s̶e̶d̶

* ̶V̶e̶r̶s̶i̶o̶n̶ ̶3̶.̶ ̶c̶l̶i̶ ̶F̶i̶l̶e̶ ̶C̶o̶m̶p̶o̶n̶e̶n̶t̶s̶

* ̶V̶e̶r̶s̶i̶o̶n̶ ̶4̶.̶ ̶F̶i̶r̶e̶b̶a̶s̶e̶ ̶I̶n̶t̶e̶r̶a̶c̶t̶i̶v̶e̶

## 參考

* <https://cn.vuejs.org/>
* <https://dotblogs.com.tw/shadow/2019/01/16/082531>
* <https://ithelp.ithome.com.tw/m/users/20107789/ironman/1710>
* <https://cythilya.github.io/2017/03/07/todolist-vue-example/>
* <https://dotblogs.com.tw/brian90191/2019/07/03/121640>
* <https://ithelp.ithome.com.tw/articles/10187642>
* <https://developer.mozilla.org/zh-TW/docs/Web/JavaScript/Reference/Global_Objects/Object/keys>

### 後續

* [A re-introduction to JavaScript (JS tutorial)](https://developer.mozilla.org/zh-TW/docs/Web/JavaScript/A_re-introduction_to_JavaScript)

<details>
<summary><a href='https://kknews.cc/zh-tw/code/kbejqlb.html'>Vue 插件總匯</a></summary>

### [一、UI組件及框架](#)

element - 餓了麼出品的Vue2的web UI工具套件

mint-ui - Vue 2的移動UI元素

iview - 基於 Vuejs 的開源 UI 組件庫

Keen-UI - 輕量級的基本UI組件合集

vue-material - 通過Vue Material和Vue 2建立精美的app應用

muse-ui - 三端樣式一致的響應式 UI 庫

vuetify - 為移動而生的Vue JS 2組件框架

vonic - 快速構建移動端單頁應用

vue-blu - 幫助你輕鬆創建web應用

vue-multiselect - Vue.js選擇框解決方案

VueCircleMenu - 漂亮的vue圓環菜單

vue-chat - vuejs和vuex及webpack的聊天示例

radon-ui - 快速開發產品的Vue組件庫

vue-waterfall - Vue.js的瀑布布局組件

vue-carbon - 基於 vue 開發MD風格的移動端

vue-beauty - 由vue和ant design創建的優美UI組件

bootstrap-vue - 應用於Vuejs2的Twitter的Bootstrap 4組件

vueAdmin - 基於vuejs2和element的簡單的管理員模板

vue-ztree - 用 vue 寫的樹層級組件

vue-tree - vue樹視圖組件

vue-tabs - 多tab頁輕型框架

### [二、滾動scroll組件](#)

vue-scroller - Vonic UI的功能性組件

vue-mugen-scroll - 無限滾動組件

vue-infinite-loading - VueJS的無限滾動插件

vue-virtual-scroller - 帶任意數目數據的順暢的滾動

vue-infinite-scroll - VueJS的無限滾動指令

vue-scrollbar - 最簡單的滾動區域組件

vue-scroll - vue滾動

vue-pull-to-refresh - Vue2的上拉下拉

mint-loadmore - VueJS的雙向下拉刷新組件

vue-smoothscroll - smoothscroll的VueJS版本

### [三、slider組件](#)

vue-awesome-swiper - vue.js觸摸滑動組件

vue-slick - 實現流暢輪播框的vue組件

vue-swipe - VueJS觸摸滑塊

vue-swiper - 易於使用的滑塊組件

vue-images - 顯示一組圖片的lightbox組件

vue-carousel-3d - VueJS的3D輪播組件

vue-slide - vue輕量級滑動組件

vue-slider - vue 滑動組件

vue-m-carousel - vue 移動端輪播組件

dd-vue-component - 訂單來了的公共組件庫

vue-easy-slider - Vue 2.x的滑塊組件

### [四、編輯器](#)

markcook - 好看的markdown編輯器

eme - 優雅的Markdown編輯器

vue-syntax-highlight - Sublime Text語法高亮

vue-quill-editor - 基於Quill適用於Vue2的富文本編輯器

Vueditor - 所見即所得的編輯器

vue-html5-editor - html5所見即所得編輯器

vue2-editor - HTML編輯器

vue-simplemde - VueJS的Markdown編輯器組件

vue-quill - vue組件構建quill編輯器

### [五、圖表](#)

vue-table - 簡化數據表格

vue-chartjs - vue中的Chartjs的封裝

vue-charts - 輕鬆渲染一個圖表

vue-chart - 強大的高速的vue圖表解析

vue-highcharts - HighCharts組件

chartjs - Vue Bulma的chartjs組件

vue-chartkick - VueJS一行代碼實現優美圖表

### [六、日曆](#)

vue-calendar - 日期選擇插件

vue-datepicker - 日曆和日期選擇組件

vue-datetime-picker - 日期時間選擇控制項

vue2-calendar - 支持lunar和日期事件的日期選擇器

vue-fullcalendar - 基於vue.js的全日曆組件

vue-datepicker - 漂亮的Vue日期選擇器組件

datepicker - 基於flatpickr的時間選擇組件

vue2-timepicker - 下拉時間選擇器

vue-date-picker - VueJS日期選擇器組件

vue-datepicker-simple - 基於vue的日期選擇器

### [七、地址選擇](#)

vue-city - 城市選擇器

vue-region-picker - 選擇中國的省份市和地區

### [八、地圖](#)

vue-amap - 基於Vue 2和高德地圖的地圖組件

vue-google-maps - 帶有雙向數據綁定Google地圖組件

vue-baidu-map- 基於 Vue 2的百度地圖組件庫

vue-cmap - Vue China map可視化組件

### [九、播放器](#)

vue-video-player - VueJS視頻及直播播放器

vue-video - Vue.js的HTML5視頻播放器

vue-music-master - vue手機端網頁音樂播放器

### [十、文件上傳](#)

vue-upload-component - Vuejs文件上傳組件

vue-core-image-upload - 輕量級的vue上傳插件

vue-dropzone - 用於文件上傳的Vue組件

### [十一、圖片處理](#)

vue-lazyload-img - 移動優化的vue圖片懶加載插件

vue-image-crop-upload - vue圖片剪裁上傳組件

vue-svgicon - 創建svg圖標組件的工具

vue-img-loader - 圖片加載UI組件

vue-image-clip- 基於vue的圖像剪輯組件

vue-progressive-image - Vue的漸進圖像加載插件

### [十二、提示](#)

vue-toast-mobile - VueJS的toast插件

vue-msgbox - vuejs的消息框

vue-tooltip - 帶綁定信息提示的提示工具

vue-verify-pop - 帶氣泡提示的vue校驗插件

### [十三、進度條](#)

vue-radial-progress - Vue.js放射性進度條組件

vue-progressbar - vue輕量級進度條

vue2-loading-bar - 最簡單的仿Youtube加載條視圖

### [十四、開發框架匯總](#)

vue-admin - Vue管理面板框架

electron-vue - Electron及VueJS快速啟動樣板

vue-2.0-boilerplate - Vue2單頁應用樣板

vue-webgulp - 仿VueJS Vue loader示例

vue-bulma - 輕量級高性能MVVM Admin UI框架

vue-spa-template - 前後端分離後的單頁應用開發

Framework7-Vue - VueJS與Framework7結合

vue-element-starter - vue啟動頁

### [十五、實用庫匯總](#)

vuelidate - 簡單輕量級的基於模塊的Vue.js驗證

qingcheng - qingcheng主題

vuex - 專為 Vue.js 應用程式開發的狀態管理模式

vue-axios - 將axios整合到VueJS的封裝

vue-desktop - 創建管理面板網站的UI庫

vue-meta - 管理app的meta信息

avoriaz - VueJS測試實用工具庫

vue-framework7 - 結合VueJS使用的Framework7組件

vue-lazy-render - 用於Vue組件的延遲渲染

vue-svg-icon - vue2的可變彩色svg圖標方案

vue-online - reactive的在線和離線組件

vue-password-strength-meter - 交互式密碼強度計

vuep - 用實時編輯和預覽來渲染Vue組件

vue-bootstrap-modal - vue的Bootstrap樣式組件

element-admin - 支持 vuecli 的 Element UI 的後台模板

vue-shortkey - 應用於Vue.js的Vue-ShortKey 插件

cleave - 基於cleave.js的Cleave組件

vue-events - 簡化事件的VueJS插件

http-vue-loader - 從html及js環境加載vue文件

vue-electron - 將選擇的API封裝到Vue對象中的插件

vue-router-transition - 頁面過渡插件

vuemit - 處理VueJS事件

vue-cordova - Cordova的VueJS插件

vue-qart - 用於qartjs的Vue2指令

vue-websocket - VueJS的Websocket插件

vue-gesture - VueJS的手勢事件插件

vue-local-storage - 具有類型支持的Vuejs本地儲存插件

lazy-vue - 懶加載圖片

vue-lazyloadImg - 圖片懶加載插件

vue-bus - VueJS的事件總線

vue-observe-visibility - 當元素在頁面上可見或隱藏時檢測

vue-notifications - 非阻塞通知庫

v-media-query - vue中添加用於配合媒體查詢的方法

vuex-shared-mutations - 分享某種Vuex mutations

vue-lazy-component - 懶加載組件或者元素的Vue指令

vue-reactive-storage - vue插件的Reactive層

vue-ts-loader - 在Vue裝載機檢查腳本

vue-pagination-2 - 簡單通用的分頁組件

vuex-i18n - 定位插件

Vue.resize - 檢測HTML調整大小事件的vue指令

vue-zoombox - 一個高級zoombox

leo-vue-validator - 異步的表單驗證組件

modal - Vue Bulma的modal組件

Famous-Vue - Famous庫的vue組件

vue-input-autosize - 基於內容自動調整文本輸入的大小

vue-file-base64 - 將文件轉換為Base64的vue組件

Vue-Easy-Validator - 簡單的表單驗證

vue-truncate-filter - 截斷字符串的VueJS過濾器

### [十六、服務端](#)

vue-ssr - 結合Express使用Vue2服務端渲染

nuxt.js - 用於伺服器渲染Vue app的最小化框架

vue-ssr - 非常簡單的VueJS伺服器端渲染模板

vue-easy-renderer - Nodejs服務端渲染

express-vue - 簡單的使用伺服器端渲染vue.js

### [十七、輔助工具](#)

DejaVue - Vuejs可視化及壓力測試

vue-generate-component - 輕鬆生成Vue js組件的CLI工具

vscode-VueHelper - 目前vscode最好的vue代碼提示插件

vue-play - 展示Vue組件的最小化框架

VuejsStarterKit - vuejs starter套件

vue-multipage-cli - 簡單的多頁CLI

### [十八、應用實例](#)

pagekit - 輕量級的CMS建站系統

vuedo - 博客平台

koel - 基於網絡的個人音頻流媒體服務

CMS-of-Blog - 博客內容管理器

vue-cnode - 重寫vue版cnode社區

vue-ghpages-blog - 依賴GitHub Pages無需本地生成的靜態博客

swoole-vue-webim - Web版的聊天應用

fewords - 功能極其簡單的筆記本

jackblog-vue - 個人博客系統

vue-blog - 使用Vue2.0 和Vuex的vue-blog

vue-dashing-js - nuvo-dashing-js的fork

rss-reader - 簡單的rss閱讀器

### [十九、Demo示例](#)

eleme - 高仿餓了麼app商家詳情

NeteaseCloudWebApp - 高仿網易雲音樂的webapp

vue-zhihu-daily - 知乎日報 with Vuejs

Vue-cnodejs - 基於vue重寫Cnodejs.org的webapp

vue2-demo - 從零構建vue2 + vue-router + vuex 開發環境

vue-wechat - vue.js開發微信app介面

vue-music - Vue 音樂搜索播放

maizuo - vue/vuex/redux仿賣座網

vue-demo - vue簡易留言板

spa-starter-kit - 單頁應用啟動套件

zhihudaily-vue - 知乎日報web版

douban - 模仿豆瓣前端

vue-Meizi - vue最新實戰項目

vue-demo-kugou - vuejs仿寫酷狗音樂webapp

vue2.0-taopiaopiao - vue2.0與express構建淘票票頁面

node-vue-server-webpack - Node.js+Vue.js+webpack快速開發框架

VueDemo_Sell_Eleme - Vue2高仿餓了麼外賣平台

vue-leancloud-blog - 一個前後端完全分離的單頁應用

vue-fis3 - 流行開源工具集成demo

mi-by-vue - VueJS仿小米官網

vue-demo-maizuo - 使用Vue2全家桶仿製賣座電影

vue2.x-douban - Vue2實現簡易豆瓣電影webApp

vue-adminLte-vue-router - vue和adminLte整合應用

vue-zhihudaily - 知乎日報 Web 版本

Zhihu-Daily-Vue.js - Vuejs單頁網頁應用

vue-axios-github - 登錄攔截登出功能

vue2.x-Cnode - 基於vue全家桶的Cnode社區

hello-vue-django - 使用帶有Django的vuejs的樣板項目

websocket_chat - 基於vue和websocket的多人在線聊天室

x-blog - 開源的個人blog項目

vue-cnode - vue單頁應用demo

vue-express-mongodb - 簡單的前後端分離案例

photoShare - 基於圖片分享的社交平台

notepad - 本地存儲的記事本

vue-zhihudaily-2.0 - 使用Vue2.0+vue-router+vuex創建的zhihudaily

vueBlog - 前後端分離博客

Zhihu_Daily - 基於Vue和Nodejs的Web單頁應用

vue-ruby-china - VueJS框架搭建的rubychina平台

vue-koa-demo - 使用Vue2和Koa1的全棧demo

life-app-vue - 使用vue2完成多功能集合到小webapp

vue-trip - vue2做的出行webapp

github-explorer - 尋找最有趣的GitHub庫

vue-ssr-boilerplate - 精簡版的ofvue-hackernews-2

vue-bushishiren - 不是詩人應用

houtai - 基於vue和Element的後台管理系統

ios7-vue - 使用vue2.0 vue-router vuex模擬ios7

Framework7-VueJS - 使用移動框架的示例

cnode-vue - 基於vue和vue-router構建的cnodejs web網站SPA

vue-cli-multipage-bootstrap - 將vue官方在線示例整合到組件中

vue-cnode - 用 Vue 做的 CNode 官網

seeMusic - 跨平台雲音樂播放器

HyaReader - 移動友好的閱讀器

zhihu-daily - 輕鬆查看知乎日報內容

vue-cnode - 使用cNode社區提供的接口

zhihu-daily-vue - 知乎日報

vue-dropload - 用以測試下拉加載與簡單路由

vue-cnode-mobile - 搭建cnode社區

Vuejs-SalePlatform - vuejs搭建的售賣平台demo

vue-memo - 用 vue寫的記事本應用

sls-vuex2-demo - vuex2商城購物車demo

v-notes - 簡單美觀的記事本

vue-starter - VueJs項目的簡單啟動頁

### [二十、其他實用插件匯總](#)

vue-dragging- 使元素可以拖拽

Vue.Draggable- 實現拖放和視圖模型數組同步

vue-picture-input- 移動友好的圖片文件輸入組件

rubik- 基於Vuejs2的開源 UI 組件庫

VueStar- 帶星星動畫的vue點讚按鈕

vue-tables-2- 顯示數據的bootstrap樣式網格

DataVisualization- 數據可視化

vue-drag-and-drop-list- 創建排序列表的Vue指令

vuwe- 基於微信WeUI所開發的專用於Vue2的組件庫

vue-typer- 模擬用戶輸入選擇和刪除文本的Vue組件

vue-impression- 移動Vuejs2 UI元素

vue-datatable- 使用Vuejs創建的DataTableView

vue-instant- 輕鬆創建自動提示的自定義搜索控制項

vue-slider-component- 在vue1和vue2中使用滑塊

vue-touch-ripple- vuejs的觸摸ripple組件

coffeebreak- 實時編輯CSS組件工具

vue-datasource- 創建VueJS動態表格

handsontable- 網頁表格組件

vue-bootstrap-table- 可排序可檢索的表格

vue-google-signin-button- 導入谷歌登錄按鈕

vue-float-label- VueJS浮動標籤模式

vue-tagsinput- 基於VueJS的標籤組件

vue-social-sharing- 社交分享組件

vue-popup-mixin- 用於管理彈出框的遮蓋層

cubeex- 包含一套完整的移動UI

vue-fullcalendar- vue FullCalendar封裝

vue-material-design- Vue MD風格組件

vue-morris- Vuejs組件封裝Morrisjs庫

we-vue- Vue2及weui1開發的組件

vue-form-2- 全面的HTML表單管理的解決方案

vue-side-nav- 響應式的側邊導航

mint-indicator- VueJS移動加載指示器插件

vue-ripple- 製作谷歌MD風格漣漪效果的Vue組件

vue-touch-keyboard- VueJS虛擬鍵盤組件

vue-parallax- 整潔的視覺效果

vue-typewriter- vue組件類型

vue-ios-alertview- iOS7+ 風格的alertview服務

paco-ui-vue- PACOUI的vue組件

vue-button- Vue按鈕組件

原文網址：https://kknews.cc/code/kbejqlb.html


</details>