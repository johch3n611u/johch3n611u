# Alex 宅幹嘛 影片筆記 從 jQuery 入門到認識 JavaScript

{% embed url="https://www.youtube.com/watch?v=xeIbgAdEcUA" %}

## 多載、鍊式與入門觀念

$ = queryselect

第一步 scope 封裝

IIFES 立即函式

\(funtion\(\){}\)\(\) = \(funtion\(\){}\(\)\)

jQ 只露出兩個東西可以用 1 $ 2 jQ



#### 選擇

document.getElementByID\('alex'\)

document.querySelector\('\#alex'\)

$\('alex'\)



$\('body'\).on\('click mouseenter' , '\#alex' ,funtion\(\){console.log\('click!'\)} \)  

支援所有js事件與偵聽兩個以上事件

$\('alex'\).click\(funtion\(\){console.log\('click!'\)}\)   

但有些新事件不能用



\(funtion\($\){

}\)\($\)



$\(funtion\(\){

}\)



document.ready     \(  jQ 組合事件 DOMContentLoaded

### 多載多型overload 

#### 不同的參數傳入進行不同的功能



$\('\#alex'\)

$\('&lt;div&gt;&lt;/div&gt;'\)

$\(this\)

$\(funtion \(\){}\)



funtion overloadFun\(a\){

console.log\(typeof a\)

if\(!a\){ console.log\('nothing'\)

}else if\(typeof a === 'string' \){

console.log\('string'\)

}else if\(typeof a === 'number' \){

console.log\('number'\)

}else if\(typeof a === 'function' \){

console.log\('function'\)}

}





ready 其實是在處理 \( 

document 的 DOMContentLoaded 事件

結構完成但不包含樣式與圖片的讀取

、

window load  事件

沒有css的情況會直接渲染圖片



阻塞 ?  chrome 其實現在是會對圖片做優化渲染



script 生命週期 \( 效能探討 \( 開發者工具 performance



document.readyState  描述文件讀取狀態







### 鍊式

animate \({樣式物件} ,{設定物件} \)

animate \({樣式物件} ,動畫時間參數 ,加減數參數 , 完成後的funtion\)

好幾種屬性 要看文件才會比較知道



$\('\#alex'\)

.stop\(\)

.animate\(\)

.animate\(\)

.animate\(\)

.animate\(\)

...





funtion base



jQ 所有 funtion 都會 return this 

所以才能鍊式 純 js 要 return this



$\(\) 其實有幫你 new



重複利用請宣告變數







{% embed url="https://www.youtube.com/watch?v=bTw0CKumCsI" %}

## DOM 選取器與遍歷的使用解析

who object selector

when event

what funtion



getElement ... 選擇器

querySelector







### jQ 管理資料



nodelist   類似 array  長得很像陣列的物件



object : { } scope

key : value     \(    數字可以當 key   \( 也有 index 自由度比較高

Array : \[ \]

index : vale   \(    索引



不是物件包陣列而是繼承陣列的方法



length

prototype

splice

push







### 選擇器

選ID  \#     選class  .       選 tag li

{% embed url="https://www.w3school.com.cn/jquery/jquery\_ref\_selectors.asp" %}





位址 跟 值 要搞清楚

$ 每次都會有新物件 所以  === 才會 false





jQ 最外層是 document &gt; html &gt; body &gt; dom

parentNode





prerObject  物件會留下來記錄起來 鍊式 所以 jQ比 JS 佔記憶體





eq\(0\)  jq 物件

\[0\]  原生 dom 物件



dom排序   \(   預設排序    index

選取排序    eq 選取排序

jq 手上有多少東西 就每個都做一次那種事情 類似內建迴圈





後面直接開 jq 講裡面的選擇器構造 感覺有點太硬了 ...





















{% embed url="https://www.youtube.com/watch?v=y2MK4mE1RkM" %}

## 事件綁定觸發與擴充功能

### 













{% embed url="https://www.youtube.com/watch?v=gAh60TfQ51w" %}



## 動畫原理、樣式功能操作與原始碼探索







{% embed url="https://www.youtube.com/watch?v=z-hN7GY5K7g" %}

## Ajax 與非同步功能操作與原始碼探索









{% embed url="https://www.youtube.com/watch?v=XLegPUzpLrU" %}





{% embed url="https://www.youtube.com/watch?v=f2ttaeDHzwE&list=PLEfh-m\_KG4dYbxVoYDyT\_fmXZHnuKg2Fq" %}

##  深入淺出 Javascript30









{% embed url="https://www.youtube.com/watch?v=mzuKtTuimEE" %}

## 從 CSS 到 SASS \(SCSS\) 超入門觀念引導







