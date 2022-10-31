# Try some React.js

## Demo

![IMAGE](https://github.com/johch3n611u/Side-Project-Hellow-React.js/blob/master/IMG/vuetodolistdemo.gif?raw=true)

## 序

既然碰過了 Angular 也碰過了 Vue 那乾脆找回在大學時期的初心，

什麼都抱持著嘗試的可能性，且 React 至少目前是主流，那就嘗試看看吧，

要記得是解決目標問題而選擇框架，而不是選擇框架後才找尋目標問題，

目前面臨人生涯的重大時刻，人生還漫長花一點時間碰碰又何嘗不可呢 ?

且還做了許多規劃了，抱持著不放棄任何可能性，明日會有重大轉淚點也說不定呢，

## 過程

* [Babel](https://zh.wikipedia.org/wiki/Babel_(%E7%B7%A8%E8%AD%AF%E5%99%A8)) 偏好的程式語言或風格來寫作原始碼，並將其利用 Babel 翻譯成 JavaScript

官方提供做中學直接學習 React 但感覺這樣對內容可能會存在不了解，所以還是從閱讀[文件](https://zh-hant.reactjs.org/docs/getting-started.html)開始，並記錄[筆記與心得](https://github.com/johch3n611u/Side-Project-Hellow-React.js/tree/master/StudyProject/React%20Basis)

看完基礎後也做了總結，本來是想說照著教學實作，但最好理解又能複習的方式，

我想大概就是用不同的框架，寫同樣的功能與樣式了吧，來嘗試看看　todolist。

## 問題

* [vscode auto format 似乎不支援在 html內使用 jsx ... 變很麻煩](https://stackoverflow.com/questions/40498622/how-to-auto-indent-jsx-in-vscode)
* [檔案分離後又讀取不到，必須添加 type="text/babel"...](https://stackoverflow.com/questions/28100644/reactjs-uncaught-syntaxerror-unexpected-token)
* [迴圈渲染思考方式又跟其他框架或ssl框架不太一樣...](https://zh-hant.reactjs.org/docs/lists-and-keys.html)
* [實作過程中遇到最麻煩的大概是 state 的提升與 state 互動問題，研究後發現其實蠻像 ng 的 service 的。](https://zh-hant.reactjs.org/docs/lifting-state-up.html#lifting-state-up)
* [感覺一個按鈕就要寫一個單一事件 ... 跟 ng 跟 vue 比感覺頗麻煩。](https://stackoverflow.com/questions/27827234/how-to-handle-the-onkeypress-event-in-reactjs)
* [而且其實也是很多預設屬性．．．](https://stackoverflow.com/questions/43556212/failed-form-proptype-you-provided-a-value-prop-to-a-form-field-without-an-on)
* [需要用到一堆高階函數解決問題．．．但確實　ｊｓ　會學很快　？？](https://developer.mozilla.org/zh-TW/docs/Web/JavaScript/Reference/Global_Objects/Array/filter)
* 所有功能都完成了但就卡在最後一步，整個 list 必須要 sort 會有三種資料型態 1.全部 2.已完成 3.未完成，但又只有單一狀態能夠管控的時候感覺頗怪的，不知道是否要用到 redux 目前想到的方式只有 state 一種目的為渲染，然後再額外加上三種 list 至 fakedata 內 1. show_all todos 2. show_completed todos2 3. show_incomplete todos3...
* [似乎又能用閉包的方式處理... 但不熟練不會用 ...](https://www.youtube.com/watch?v=TLIQUloXLec)
* 大致功能大概完成，目前存在一些小缺陷，就等日後有空或實力更強時再補。 1. li 識別問題 影響到相關功能 2. sort 分類問題無狀態管理

## Todo-List Demo

### Branch

Now

* Version 1. todolist-simple (master)

Expected

*  ̶V̶e̶r̶s̶i̶o̶n̶ ̶2̶.̶ ̶R̶e̶d̶u̶x̶ ̶S̶t̶a̶t̶e̶ ̶M̶a̶n̶a̶g̶e̶m̶e̶n̶t̶

## 參考

* <https://zh-hant.reactjs.org/>
* <https://ithelp.ithome.com.tw/articles/10201610>
* <https://ithelp.ithome.com.tw/articles/10190581>
* <https://cythilya.github.io/2017/04/01/todo-list-react-and-redux-example/>