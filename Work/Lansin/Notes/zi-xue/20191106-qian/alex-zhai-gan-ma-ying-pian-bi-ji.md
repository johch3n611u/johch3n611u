# Alex 宅幹嘛 影片筆記 1&2

{% embed url="https://www.youtube.com/watch?v=hCy-eHwjhXc" %}

### 1.&lt;script&gt;位址   /  變數使用時機 / 基礎 funtion 與 property

### 2.who when what如何寫程式  \( vs 提示字

### 3.hoisting提升 \( var v.s let  

### 4.scope block 作用域  $\(funtion\(\){}\)  // \(funtion \(\){}\)\(\)IIFE //ES6 { }    封裝

### 5.primitive type V.S object type   //  位址 / 值  \( 獨立運作時必須完全 deep clone

### 6.array v.s object // 序列編號 / 屬性名稱

### 7.hoisting 順序 funtion &gt; var 、 相同funtion 後&gt;前 \(\( funtion &gt; var &gt;  參數 ?

### 8.先宣告後使用 / 盡量不要重複宣告



### [https://www.youtube.com/watch?v=5552ZlZvDiU](https://www.youtube.com/watch?v=5552ZlZvDiU)

### 

### count 使用時機 判斷式種類 ? 迴圈種類 ? ++-- 運算式 表達式 ? vs 建立片段

























Emmet HTML 初始化

Scritp tag位置 前因後果 &lt;=&gt; 直譯器 載入先後順序

bundle程式 webpack

stylesheet render 要放 head 是為了避免繪製兩次畫面



 _DOM 操作_

window 視窗  document 內容

操作 who 誰id 。 when  follow line number 或事件。 what 修改內容 

用 funtion method

getelementbyid \( id \)   不直覺

queryselector \( \#id \)  直覺一些  類似 jQ 的 $

單雙引號 習慣   程式使用單引號 ， 非程式使用 雙引號

ex \('&lt;div id=" " &gt;&lt;/dib&gt;'\)

es6 變數資料使用反引號

```text
` `
```

innerHTML   \( 感覺像 return ?

property 屬性 變數    寫入或讀出內容

\'

document.queryselector\(\#id\).innerHTML = ' ' 



別複製貼上 要去看別人講甚麼，然後更進階到改成自己講的方式



client render 

通常第一階是method 第二階是property

jQ .click

JS .addEventListener\('click', funtion \( \){ } \) 事件監聽者

funtion \( \){ } 包裝一個可以被重複使用的指令功能 \(匿名或有名字\)



$\('\#A'\).click\(funtion\(\){}\)

$\('\#A'\).on\(click , funtion\(\){}\) 監聽概念



常用常改時取變數   \(  可以節省效能  \( es5 var        /       es6 let const    ie11     但 safari 是新坑

var 很容易有坑而且不好殺蟲   let 只能宣告一次不可重複



hoisting \( 程式提升 宣告變數/參數/函式   先於指定  \(  不宣告就使用   \( 先宣告後使用

not defined undefine 差別

let 逼迫一定要先宣告

變數名稱必須有意義

funtion xx\(\){} 與  var xx = funtion\(\){}  宣告後的意義不同

var xx = funtion \(\){} 其實等於   var xx = undefine 



var 跟 funtion 誰先 hoisting ?







純資料 /    傳值   

基本primitive  型別 6種   es6有多一種  number string boolear null undefined

物件 / 陣列     // 傳址

物件型別  object array funtion date regx正規表達式





純資料是轉值    \( 會很多 位址記憶體 值  / 複製一份

object 物件與陣列是傳址   \(   會是同一個位址

可以用記憶體大小來思考



所以物件 必須要用物件的複製方式 deep clone 或 JSON.parse\(JSON.stringify\(xxx\)\)

JavaScript Object Notation

JSON funtion會不見 ? 因為是傳資料?

let aaa = bbb // 傳址

JSON.parse\(JSON.stringify\(xxx\)\)  // 拷貝資料 部拷貝 funtion



jQ extend

lodash 資料處理的函式庫

object.assign\({},xxx\)









=&gt; ES6 箭頭函式



物件 屬性名稱 值 但不用顧慮排序

陣列 序列編號



外洩問題 閉包 ? 匿名函式

\(funtion\(\){ let xx = aa }\)\(\)

es6 { let xx = aa } 包住後 let 不會外洩也取用不了 塊狀區塊 





=== 型態根值都要符合



Microsoft Edge





錯誤訊息 殺蟲





先宣告後使用



var alex

console.log\(alex\)

alex='man'



hositing

funtion &gt; var 

funtion後的會 &gt; funtion先的







## 第二part

const 常數 永不變





funtion basic

class 

oo







who

let xxx = document.querySelector\('xxx'\)

what

let xxx2 = function \(\){xxx.innerHTML = 'zzz'}

when

xxx.addEventListener\('click',xxx2\)





自製funtion



funtion xxx \(\) {

}

let xxx = funtion \(\){

}

let obj = {

xxx: funtion\(\){

}

}



參數位置 執行區域 執行內容 反傳參數

callback funtion

return funtion 

funtion aaa\(z,x\){

}



return false



return 中斷











this 執行環境

呼叫這個 funtion的對象   who   when   what  的 who

scope 結構 如果 包了 兩層 三層 ... this 對象會改變



es6 =&gt; 箭頭函式  \( 簡化版的 funtion \( 不擁有自己的 this

es5 var self = this  記住 this 用於參數

#### **執行環境物件**









判斷式 

雙even 單odd

%取餘數

加減乘除餘數百分比

if  

if else 

else



if

if

if



? = if 

: = else

: ? = else if



var eat = today == 3? 'aa' : today % 2 == 0 ? 'bb' : 'ccc'









迴圈 

for

while \(單一條件\){}        無窮迴圈  條件參數必須設定結尾 或 return ?



switch case

break 中斷











同步非同步  \( call api 要回 json 看









片段 snippet

preferences

$1$2 focus



 縮排  空白好於tab 因為跨系統會有問題





## 閉包 prototype





