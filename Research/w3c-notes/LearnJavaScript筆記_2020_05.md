# [JavaScript Tutorial by w3schools](https://www.w3schools.com/js/default.asp)

## 重新熟悉

<details>
<summary><h2 style="display:inline">JS Introduction</h2></summary>

---

1. Node.js、MongoDB、CouchDB 都是用 JavaScript 作為編程語言。
2. ECMAScript 是該語言的正式名稱，由 Brendan Eich 於 1995 年發明。
3. JavaScript 接受雙引號和單引號
4. 直接對 document 屬性操作：

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_intro_inner_html_quotes>改動html文本
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_intro_lightbulb> 改動img的來源位置
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_intro_style>改動css文字大小
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_intro_hide>改動css display屬性
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_intro_show>改動css display屬性

5. JavaScript 已 [預設](#) 在計算機，平板電腦和智能手機上的 [瀏覽器](#) 中運行。免費供所有人使用。
6. 舊的 JavaScript 示例可能使用 [type 屬性](#)：`<script type =" text / javascript">`。
7. type 屬性不是必需的。 JavaScript 是 HTML 中的默認腳本語言。
8. JavaScript [function](#) 是 JavaScript 代碼塊，可以在被"調用"時執行。
9. 腳本可以放置在`<body>`，或在`<head>`一個HTML頁面的部分，或者在兩者。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_whereto_head>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_whereto_body>

10. 將腳本放在`<body>`元素的底部可以提高顯示速度，因為 script 解析會減慢顯示速度。
11. 要使用外部腳本，可以將腳本文件的名稱放在標籤的src（源）屬性中`<script src="myScript.js"></script>`。
12. 外部腳本不能包含`<script>`標籤。
13. 要將多個腳本文件添加到一頁中，請使用多個腳本標籤。
14. 外部腳本的引入放式分別有:
```JavaScript
<script src="myScript1.js"></script> 當文本與腳本在本地端的同資料夾時的引入方式
<script src="myScript2.js"></script> 當文本與腳本在本地端的同資料夾時的引入方式
<script src="https://www.w3schools.com/js/myScript1.js"></script> 當腳本在網址上時用超連結引入方式
<script src="/js/myScript1.js"></script>當文本與腳本在本地端的特定資料夾時的引入方式
```
15. 引入外部腳本的優勢:分開html文本和程式碼, 讓html文本和程式碼更好閱讀與維護, 網頁載入更快當腳本檔案被緩存。

</details> <!-- JS Introduction 結束 -->

<br>

<details>
<summary><h2 style="display:inline">JS Basics</h2></summary>

---

<details>
<summary><h3 style="display:inline">JS Output</h3></summary>

1. JavaScript 可以通過不同的方式"顯示"數據。

```JavaScript
innerHTML //使用寫入HTML元素
document.write() //使用寫入HTML輸出
window.alert() //使用寫入警報框
console.log() //使用寫入瀏覽器控制台,打開開發著工具時可以抓到裡面的值(value)
```

2. 更改 HTML 元素的 innerHTML 屬性是在 HTML 中顯示數據的常用方法。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_output_dom>

3. 加載 HTML 文檔後使用 document.write（），將刪除所有現有的 HTML
4. document.write（） 方法應僅用於測試。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_output_write_over>

5. 您可以忽略加上視窗(window)關鍵字。在 JavaScript 中，視窗(window)物件是全域物件，這意味著[變量，屬性和方法都默屬於視窗物件](#)。這也意味著沒有一定 要加上(window)關鍵字在指令中。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_output_alert>

6. window.print() 在瀏覽器中調用該方法以打印當前窗口的內容。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_output_print>

</details>

---

<details>
<summary><h3 style="display:inline">JS Statements</h3></summary>

```Text
A computer program is a list of "instructions" to be "executed" by a computer.
電腦程序是電腦要執行的一份指令列表。

In a programming language, these programming instructions are called statements.
編程語言中這些指令被稱為陳述句。

A JavaScript program is a list of programming statements.

In HTML, JavaScript programs are executed by the web browser.
```

<https://www.w3schools.com/js/tryit.asp?filename=tryjs_statements>

7. JavaScript 語句由以下組成：值，運算符，表達式，關鍵字和註釋。
8. 分號分隔 JavaScript 語句。當用分號分隔時，允許在一行上使用多個語句。
9. 在網絡上，您可能會看到沒有分號的示例。不需要以分號結尾的語句，但強烈建議使用。
10.  JavaScript會忽略多個空格。您可以在腳本中添加空格以使其更具可讀性。

```JavaScript
var person = "Hege";
var person="Hege";
var x = y + z;
```

11. 為了獲得最佳可讀性，程序員通常喜歡避免使用超過80個字符的代碼行。
12. 如果JavaScript語句不適合一行，則打破它的最佳位置是在運算符之後。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_statements_linebreak>

13. JavaScript 語句可以在大括號 [{...}](#) 中的 JS 代碼塊 Code Blocks 中分組在一起。
14. [JS 代碼塊的目的是定義要一起執行的語句](#)。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_statements_blocks>

15. JavaScript 語句通常以關鍵字開頭， 以標識要執行的 JavaScript 操作。
16. JavaScript 關鍵字是保留字。保留字不能用作變量的名稱。

```Text
關鍵詞 //描述

break //終止開關或迴路
continue //跳出循環並從頂部開始
debugger //停止執行JavaScript，並調用調試功能（如果有）
do ... while //在條件為真時執行語句塊並重複執行該塊
for //只要條件為真，就標記要執行的語句塊
function //聲明一個功能
if ... else //根據條件標記要執行的語句塊
return //退出功能
switch //根據不同的情況標記要執行的語句塊
try ... catch //對語句塊實施錯誤處理
var //聲明一個變量
```

</details>

---

<details>
<summary><h3 style="display:inline">JS Syntax</h3></summary>

17. JavaScript 語法是規則集，以及如何構造 JavaScript 程序。

```JavaScript
var x, y, z;       // 如何宣告變數
x = 5; y = 6;      // 變數如何分配值
z = x + y;         // 如何計算值
```

18. JavaScript語法定義了兩種類型的值：固定值稱為 literals。變量值稱為 variables。
19. 固定值：數字沒有小數 `10.50 只會顯示 10.5`。
20. 固定值：字符串是文本，用雙引號或單引號引起來。
21. JavaScript 使用 var 關鍵字聲明變量。賦值運算符等號 = 用於分配值給變量。
22. JavaScript 使用算術運算符（+ - * /） 計算值。
23. [表達式是值，變量和運算符的組合，可以計算出一個值。](#)

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_syntax_expressions_strings>

24. 後雙斜線碼 //之間或/*與*/被視為註釋。
25. 在 JavaScript 中，第一個字符必須是字母，下劃線（_）或美元符號（$）。
26. 後續字符可以是字母，數字，下劃線或美元符號。
27. 不允許數字作為第一個字符。這樣，JavaScript 可以輕鬆區分標識符和數字。
28. [所有 JavaScript 標識符均 區分大小寫。變量 lastName 和 lastname，是兩個不同的變量](#)
29. JavaScript 中不允許使用連字符(-)。它們保留用於減法。
30. JavaScript 程序員傾向於使用以小寫字母開頭的駝峰式大小寫。

```JavaScript
變數命名方式

//Hyphens:
//first-name, last-name, master-card, inter-city.

Underscore:
first_name, last_name, master_card, inter_city.

Upper Camel Case (Pascal Case):
FirstName, LastName, MasterCard, InterCity.

Lower Camel Case:
firstName, lastName, masterCard, interCity.
```

31. JavaScript 使用 Unicode 字符集。

</details>

---

<details>
<summary><h3 style="display:inline">JS Comments</h3></summary>

32. JavaScript 註釋可用於解釋 JavaScript 代碼並使其更具可讀性。
33. 測試替代代碼時，JavaScript 註釋還可用於阻止執行。

</details>

---

<details>
<summary><h3 style="display:inline">JS Variables</h3></summary>

34. JavaScript 變量是用於存儲數據值的容器。
35. 所有 JavaScript 變量必須 使用唯一的名稱標識。
36. 字符串用雙引號或單引號引起來。數字不帶引號。
37. 如果在引號中加上數字，它將被視為文本字符串。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_variables_types>

38. 聲明後，變量沒有值（從技術上講，其值為 undefined ）。 `var carName;`。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_variables_undefined>

39. 在聲明變量時為其賦值：`var carName = "Volvo";`。
40. 在 script 開始時聲明所有變量是一種好的編程習慣。
41. 一個陳述，多個變量 `var person = "John Doe", carName = "Volvo", price = 200;`。
42. 聲明可以跨越多行。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_variables_multiline>

43. [重新聲明一個JavaScript變量，它也不會丟失其值。](#)

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_variables_redefine>

44. 如果將數字用引號引起來，則其餘數字將被視為字符串並連接在一起。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_variables_add_string_number>

45. 由於JavaScript將美元符號與下劃線（_）視為字母，因此包含標識符是有效的變量名
46. 在JavaScript庫jQuery中，main函數 $用於選擇HTML元素。在jQuery中，$("p");意思是"選擇所有 p 個元素"。

<https://dotblogs.com.tw/maplenote/2014/07/21/146024>

47. 下劃線在JavaScript中不是很常見，但是專業程序員之間的約定是將其用作"私有 private （隱藏）"變量的別名。

</details>

---

<details>
<summary><h3 style="display:inline">JS Operators</h3></summary>

48. 當在字符串上使用時，+運算符稱為串聯運算符。
49. 相加兩個數字將返回總和，但相加一個數字和一個字符串將返回一個字符串。
50. 運算子：算術、賦值、比較、邏輯、類型、按位

</details>

---

<details>
<summary><h3 style="display:inline">JS Arithmetic</h3></summary>

51. 求冪(次方)：所述冪運算符（**）引發第一操作數到第二操作數的功率，產生與以下結果相同的結果Math.pow(x,y)。

* <https://www.w3schools.com/js/js_arithmetic.asp>

</details>

---

<details>
<summary><h3 style="display:inline">JS Assignment</h3></summary>

* <https://www.w3schools.com/js/js_assignment.asp>

</details>

---

<details>
<summary><h3 style="display:inline">JS Data Types</h3></summary>

52. [JavaScript 具有動態類型。這意味著可以使用同一變量來保存不同的數據類型。](#)
53. JavaScript 只有一種數字。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_datatypes_numbers>

54. JavaScript 陣列用方括號括起來。陣列項目用逗號分隔。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_datatypes_array>

55. JavaScript 物件用花括號括起來 {}。物件項目用逗號分隔。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_datatypes_object>

56. 使用 JavaScript typeof 運算符查找 JavaScript 變量的類型。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_datatypes_typeof_number>

57. 在 JavaScript 中，沒有值的變量具有 value undefined。類型也是 undefined。
58. 空值(Empty Values)與無關 undefined。空字符串具有合法值和類型。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_datatypes_empty>

59. 空(Null)它應該是不存在的東西。不幸的是，在 JavaScript 中，的數據類型 null 是一個對象。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_datatypes_null>

60. 未定義和空之間的區別。

```JavaScript
typeof undefined           // undefined
typeof null                // object

null === undefined         // false
null == undefined          // true
```

61. 複雜數據。

```JavaScript
typeof {name:'John', age:34} // Returns "object"
typeof [1,2,3,4]             // Returns "object" (not "array", see note below)
typeof null                  // Returns "object"
typeof function myFunc(){}   // Returns "function"
```

62. JavaScript 數組中的 陣列都是物件。

</details>

---

<details>
<summary><h3 style="display:inline">JS Functions</h3></summary>

63. 在函數內部，參數為局部變量 local variables (全域變數(global variable)和區域變數(local variable))。
64. 當"某物" 調用（call）該函數時，將執行該函數內的代碼。

* 事件發生時（用戶單擊按鈕時）
* 從JavaScript代碼調用（調用）時
* 自動（自行調用 callback）

65. 當 JavaScript 到達一條 return 語句時，該函數將停止執行。[返回值被"返回"到"調用者"](#)

```JavaScript
var x = myFunction(4, 3);   // Function is called, return value will end up in x

function myFunction(a, b) {
  return a * b;             // Function returns the product of a and b
}
```

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_function_return>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_function_variable>

66. 局部變量只能從函數內部訪問。

```JavaScript
// code here can NOT use carName

function myFunction() {
  var carName = "Volvo";
  // code here CAN use carName
}

// code here can NOT use carName
typeof carName;
// undefined
```

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_function_scope>

</details>

---

<details>
<summary><h3 style="display:inline">JS Objects</h3></summary>

<https://www.w3schools.com/js/js_objects.asp>

67. JavaScript 物件是 屬性或方法 的容器。調用的方式如下

```JavaScript
objectName.propertyName

objectName["propertyName"]
```

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_objects_create_1>

68. 方法是作為屬性存儲的函數。A method is a function stored as a property.

```JavaScript
var person = {
  firstName: "John",
  lastName : "Doe",
  id       : 5566,
  fullName : function() {
    return this.firstName + " " + this.lastName;
  }
};
```

69. [在函數定義中，this 指的是函數的"所有者"](#)。
70. Accessing Object Methods。使用 objectName.methodName()。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_objects_method>

80. 如果訪問不帶（）括號的方法，它將返回函數定義。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_objects_function>

90. 不要將字符串，數字和布爾值聲明為對象！當使用關鍵字"new" 聲明 JavaScript 變量時，該變量將作為對象創建。它們會使您的代碼複雜化，並降低執行速度。

```JavaScript
var x = new String();        // Declares x as a String object
var y = new Number();        // Declares y as a Number object
var z = new Boolean();       // Declares z as a Boolean object

// x=,
// y=0,
// z=false
```

</details>

---

<details>
<summary><h3 style="display:inline">JS Events</h3></summary>

91. HTML 事件可以是瀏覽器執行的操作，也可以是用戶執行的操作。

* HTML網頁已完成加載
* HTML輸入字段已更改
* 單擊了HTML按鈕

92. HTML 允許使用 JavaScript 代碼將事件處理程序屬性添加到 HTML 元素。`<element event="some JavaScript">`

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_event_onclick1>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_event_onclick>

93. JavaScript 代碼通常長幾行。看到事件屬性調用函數更常見。`<button onclick="displayDate()">The time is?</button>`

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_events1>

94. 瀏覽器事件。<https://www.w3schools.com/jsref/dom_obj_event.asp>

| 常見事件             | 描述                     |
| -------------------- | ------------------------ |
| onchange 不斷變化    | HTML元素已更改           |
| onclick 點擊         | 用戶單擊一個HTML元素     |
| onmouseover 鼠標懸停 | 用戶將鼠標移到HTML元素上 |
| onmouseout 暫停      | 用戶將鼠標從HTML元素移開 |
| onkeydown 按鍵       | 用戶按下鍵盤鍵           |
| onload 負載          | 瀏覽器已完成頁面加載     |

95. JS 可以做什麼？

* 每次頁面加載時應該做的事情
* 關閉頁面時應該做的事情
* 用戶單擊按鈕時應執行的操作
* 用戶輸入數據時應驗證的內容
* HTML 事件屬性可以直接執行 JavaScript 代碼
* HTML 事件屬性可以調用 JavaScript 函數
* 您可以將自己的事件處理函數分配給 HTML 元素
* 您可以防止事件被發送或處理
* 和更多 ...

</details>

---

<details>
<summary><h3 style="display:inline">JS Strings</h3></summary>

96. JavaScript 字符串用於存儲和處理文本。
97. 您可以在字符串內使用引號，只要它們與字符串周圍的引號不匹配即可。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_string_quotes_mixed>

98. 要查找字符串的長度，請使用內置 length 屬性：。

```JavaScript
var txt = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
var sln = txt.length;
```

99. 轉義符、避免關鍵字。

| Code | Result | Description  |
| ---- | ------ | ------------ |
| \ '  | '      | Single quote |
| \ "  | "      | Double quote |
| \ \  | \      | Backslash    |

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_string_escape_quotes2>

100. 如果JavaScript語句不適合一行，則打破它的最佳位置是在運算符之後。

```JavaScript
document.getElementById("demo").innerHTML =
"Hello Dolly!";
```

101. 您還可以使用單個反斜杠在文本字符串中拆分代碼行。該\方法不是首選方法。它可能沒有普遍支持。某些瀏覽器不允許在\字符後面留空格。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_string_break>

102. 分解字符串的一種更安全的方法是使用字符串加法。

```JavaScript
document.getElementById("demo").innerHTML = "Hello " +
"Dolly!";
```

103. 字符串可以是物件。不同的 type 具有不同的預設方法。

```JavaScript
var x = "John";
var y = new String("John");

// typeof x will return string
// typeof y will return object

// (x == y) is true because x and y have equal values
```

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_string_object1>

104. 使用 === 運算符時，相等的字符串不相等，因為 === 運算符期望類型和值相等。

`(x === y) is false because x and y have different types (string and object)`

```JavaScript
var x = new String("John");
var y = new String("John");

// (x == y) is false because x and y are different objects
// (x === y) is false because x and y are different objects
```

</details>

---

<details>
<summary><h3 style="display:inline">JS String Methods</h3></summary>

105. string.length 返回字符串的長度。
106. string.indexOf("key") 返回 first 字符串中指定文本的出現位置的索引（位置）。
107. lastIndexOf() 方法返回 字符串中最後一次出現指定文本的索引。
108. indexOf()，lastIndexOf() 如果找不到文本，則返回 -1 。
109. 兩種方法都接受第二個參數作為搜索的起始位置。
110. search() 方法在字符串中搜索指定的值，並返回匹配的位置。
111. 該 search() 方法不能使用第二個起始位置參數。
112. 該 indexOf() 方法不能採用強大的搜索值（正則表達式）。
113. 有三種方法可以提取字符串的一部分：

* slice(start, end) `提取字符串的一部分，並以新字符串返回提取的部分。如果參數為負，則從字符串的末尾開始計算位置。`
* substring(start, end) `與 slice() 相似，區別在於 substring() 不能接受負索引。如果省略第二個參數，substring() 將切出其餘字符串。`
* substr(start, length) `與 slice() 相似，區別在於第二個參數指定 了提取部分的長度。`

114. replace() 方法用字符串中的另一個值替換指定的值，不會更改其調用的字符串。它返回一個新字符串。默認情況下，該 replace() 方法僅替換第一個匹配項。
115. replace() 方法區分大小寫。編寫 MICROSOFT（大寫）將不起作用。
116. 要替換不區分大小寫的代碼，請使用帶有標誌（不敏感）的正則表達式/i：`var n = str.replace(/MICROSOFT/i, "W3Schools");`
117. 請注意，正則表達式不帶引號。
118. 要替換所有匹配項，請使用帶有標誌的正則表達式/g（全局匹配項）：`str = "Please visit Microsoft and Microsoft!";var n = str.replace(/Microsoft/g, "W3Schools");`
119. 字符串將轉換為大寫 toUpperCase()。
120. 字符串將轉換為小寫 toLowerCase()。
121. concat() 連接兩個或多個字符串。
122. 所有字符串方法都返回一個新字符串。他們不修改原始字符串。正式表示：字符串是不可變的：字符串不能更改，只能替換。
123. trim() 方法從字符串的兩側刪除空格 `str.replace(/^[\s\uFEFF\xA0]+|[\s\uFEFF\xA0]+$/g, '')` 可達到同樣效果。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_string_trim_polyfill>

124. charAt() 方法返回字符串中指定索引（位置）處的字符。
125. charCodeAt() 方法返回字符串中指定索引處的字符的unicode。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_string_prop2>

126. 如果要使用字符串作為數組，可以將其轉換為數組。
127. split() 方法將字符串轉換為數組。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_string_split_char>

</details>

---

<details>
<summary><h3 style="display:inline">JS Numbers</h3></summary>

128. JavaScript 只有一種數字。數字可以帶或不帶小數。
129. 可以使用科學（指數）符號來寫特大號或小號 `var x = 123e5;    // 12300000 ; var y = 123e-5;   // 0.00123`
130. JavaScript 數字始終是 64 位浮點，不會定義不同類型的數字，例如整數，短整數，長整數，浮點數等。始終存儲為雙精度浮點數。
131. 整數（不帶句點或指數表示法的數字）的精度最高為15位數字。

```JavaScript
var x = 999999999999999;   // x will be 999999999999999
var y = 9999999999999999;  // y will be 10000000000000000
```

132. 小數位數的最大值是17，但是浮點算術並不總是100％準確的。

```JavaScript
var x = 0.2 + 0.1;         // x will be 0.30000000000000004
var x = (0.2 * 10 + 0.1 * 10) / 10;       // x will be 0.3
```

133. 如果字符串和數字，結果將是字符串串聯。先計算遇到字符串聯則先字符串聯(+)。數字字符串互相計(/ * -)算是可行的

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_numbers_add_strings5>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_numbers_add_strings3>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_numbers_add_strings4>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_numbers_string2>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_numbers_string3>

134. `NaN` 是 JavaScript 保留字，表示數字不是合法數字。

`var x = 100 / "Apple";  // x will be NaN (Not a Number)`

135. 可以使用全局 JavaScript 函數 isNaN() 來確定值是否為數字

```JavaScript
var x = 100 / "Apple";
isNaN(x);               // returns true because x is Not a Number
```

136. 當心 NaN 。如果 NaN 在數學運算中使用，結果還將為 NaN。
137. NaN 是一個數字：typeof NaN 返回 number。
138. 無限 Infinity （或 -Infinity ）是如果您計算的數字超出最大可能數，JavaScript 將返回的值。
139. Infinity 是一個數字：typeof Infinityreturn number。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_numbers_infinity>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_numbers_infinity_zero>

140. 如果數字常量以 0x 開頭，則 JavaScript 會將其解釋為十六進制。
141. 默認情況下，JavaScript 將數字顯示為以十進制。
142. 可以使用該 toString() 方法將數字從 2 到 36 進制。

```JavaScript
var myNumber = 32;
myNumber.toString(10);  // returns 32
myNumber.toString(32);  // returns 10
myNumber.toString(16);  // returns 20
myNumber.toString(8);   // returns 40
myNumber.toString(2);   // returns 100000
```

143. 數字特性

```JavaScript
var x = 123;
var y = new Number(123);

// typeof x returns number
// typeof y returns object

// (x == y) is true because x and y have equal values
// (x === y) is false because x and y have different types

var x = new Number(500);
var y = new Number(500);

// (x == y) is false because objects cannot be compared
```

</details>

---

<details>
<summary><h3 style="display:inline">JS Number Methods</h3></summary>

144. 原始值（例如3.14或2014）不能具有屬性和方法（因為它們不是對象）。
145. 方法和屬性也可用於原始值，因為 JavaScript 在執行方法和屬性時會將原始值視為對象。
146. toString() 方法返回一個數字作為字符串。

```JavaScript
var x = 123;
x.toString();            // returns 123 from variable x
(123).toString();        // returns 123 from literal 123
(100 + 23).toString();   // returns 123 from expression 100 + 23
```

147. toExponential() 返回一個字符串，該字符串的數字使用指數表示法四捨五入並寫入。

```JavaScript
var x = 9.656;
x.toExponential(2);     // returns 9.66e+0
x.toExponential(4);     // returns 9.6560e+0
x.toExponential(6);     // returns 9.656000e+0
```

148. toFixed() 返回一個字符串，該數字用指定的小數位數書寫。

```JavaScript
var x = 9.656;
x.toFixed(0);           // returns 10
x.toFixed(2);           // returns 9.66
x.toFixed(4);           // returns 9.6560
x.toFixed(6);           // returns 9.656000
```

149. toPrecision() 返回一個字符串，該字符串帶有指定長度的數字。

```JavaScript
var x = 9.656;
x.toPrecision();        // returns 9.656
x.toPrecision(2);       // returns 9.7
x.toPrecision(4);       // returns 9.656
x.toPrecision(6);       // returns 9.65600
```

150. valueOf() 返回一個數字作為數字。
151. 在 JavaScript 中，數字可以是原始值（typeof = number）或對象（typeof = object）。
152. 該 valueOf() 方法在 JavaScript 內部使用，用於將 Number 對象轉換為原始值。

```JavaScript
var x = 123;
x.valueOf();            // returns 123 from variable x
(123).valueOf();        // returns 123 from literal 123
(100 + 23).valueOf();   // returns 123 from expression 100 + 23
```

153. JavaScript 全局方法可用於所有 JavaScript 數據類型。
154. Number() 可以用於將 JavaScript 變量轉換為數字。如果數字無法轉換，NaN則返回（非數字）。

```JavaScript
Number(true);          // returns 1
Number(false);         // returns 0
Number("10");          // returns 10
Number("  10");        // returns 10
Number("10  ");        // returns 10
Number(" 10  ");       // returns 10
Number("10.33");       // returns 10.33
Number("10,33");       // returns NaN
Number("10 33");       // returns NaN
Number("John");        // returns NaN
```

155. Number() 也可以將日期轉換為數字。`Number(new Date("2017-09-30"));    // returns 1506729600000`
156. parseInt() 解析字符串並返回整數。允許有空格。僅返回第一個數字。

```JavaScript
parseInt("10");         // returns 10
parseInt("10.33");      // returns 10
parseInt("10 20 30");   // returns 10
parseInt("10 years");   // returns 10
parseInt("years 10");   // returns NaN
```

157. parseFloat()解析一個字符串並返回一個數字。允許有空格。僅返回第一個數字。

```JavaScript
parseFloat("10");        // returns 10
parseFloat("10.33");     // returns 10.33
parseFloat("10 20 30");  // returns 10
parseFloat("10 years");  // returns 10
parseFloat("years 10");  // returns NaN
```

158. var x = Number.MAX_VALUE; 返回 JavaScript 中可能的最大數字。
159. var x = Number.MIN_VALUE; 返回JavaScript中可能的最低數字。
160. POSITIVE_INFINITY 在溢出時返回。NEGATIVE_INFINITY 在溢出時返回。
161. Number 屬性屬於 JavaScript 的 Number 對象包裝器，稱為 Number。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_number_max_undefined>

</details>

---

<details>
<summary><h3 style="display:inline">JS Arrays</h3></summary>

162. JavaScript 數組用於在單個變量中存儲多個值。`var cars = ["Saab", "Volvo", "BMW"];`
163. 空格和換行符並不重要。聲明可以跨越多行。通過引用索引號來訪問數組元素。數組索引從0開始。
164. 陣列是一種特殊的對象。typeof JavaScript 中的運算符為陣列返回"物件"。但是陣列最好描述為陣列。
165. 您可以在數組中包含對象。您可以在數組中具有函數。您可以在數組中包含數組。

```JavaScript
myArray[0] = Date.now;
myArray[1] = myFunction;
myArray[2] = myCars;
```

166. JavaScript 數組的真正優勢在於內置的數組屬性和方法。

```JavaScript
var x = cars.length;   // The length property returns the number of elements
var y = cars.sort();   // The sort() method sorts arrays

var first = fruits[0];
// 訪問第一個數組元素
var last = fruits[fruits.length - 1];
// 訪問最後一個數組元素
var fruits, text, fLen, i;
fruits = ["Banana", "Orange", "Apple", "Mango"];
fLen = fruits.length;

text = "<ul>";
for (i = 0; i < fLen; i++) {
  text += "<li>" + fruits[i] + "</li>";
}
text += "</ul>";
// 遍歷數組的最安全方法是使用for循環
var fruits, text;
fruits = ["Banana", "Orange", "Apple", "Mango"];

text = "<ul>";
fruits.forEach(myFunction);
text += "</ul>";

function myFunction(value) {
  text += "<li>" + value + "</li>";
}
// Array.forEach()
fruits.push("Lemon");    // adds a new element (Lemon) to fruits
// 向數組添加新元素的最簡單方法是使用push()方法
fruits[fruits.length] = "Lemon";    // adds a new element (Lemon) to fruits
// 使用length屬性將新元素添加到數組中
```

167. 該 length 屬性始終比最高數組索引大一。
168. 添加具有高索引的元素會在數組中創建未定義的"孔"

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_array_holes>

169. JavaScript 並沒有關聯數組支持數組名為索引。在 JavaScript 中，數組始終使用編號索引。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_array_associative_2>

170. 在 JavaScript 中，數組使用編號索引，對象使用命名索引。
171. JavaScript 不支持關聯數組。
172. 當您希望元素名稱為字符串（文本）時， 應使用對象。
173. 如果希望元素名稱為 數字，則應使用數組。

```JavaScript
var points = new Array();     // Bad
var points = [];              // Good
var points = new Array(40); // 內含40個空陣列項目 ,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,
```

174. ES5+ 如何識別數組 Array.isArray(fruits);   // returns true

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_array_isarray>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_array_instanceof>

</details>

---

<details>
<summary><h3 style="display:inline">JS Array Methods</h3></summary>

175. JavaScript 方法 toString() 將數組轉換為（逗號分隔）數組值的字符串。

```JavaScript
var fruits = ["Banana", "Orange", "Apple", "Mango"];
document.getElementById("demo").innerHTML = fruits.toString();

// Banana,Orange,Apple,Mango
```

176. join() 方法還將所有數組元素連接到一個字符串中。

```JavaScript
var fruits = ["Banana", "Orange", "Apple", "Mango"];
document.getElementById("demo").innerHTML = fruits.join(" * ");

// Banana * Orange * Apple * Mango
```

177. pop()方法從數組中刪除最後一個元素。pop() 方法返回"彈出"的值。

```JavaScript
var fruits = ["Banana", "Orange", "Apple", "Mango"];
fruits.pop();              // Removes the last element ("Mango") from fruits

var fruits = ["Banana", "Orange", "Apple", "Mango"];
var x = fruits.pop();      // the value of x is "Mango"
```

178. push() 方法將一個新元素添加到數組中（最後）。push() 方法返回新的數組長度。

```JavaScript
var fruits = ["Banana", "Orange", "Apple", "Mango"];
fruits.push("Kiwi");       //  Adds a new element ("Kiwi") to fruits

var fruits = ["Banana", "Orange", "Apple", "Mango"];
var x = fruits.push("Kiwi");   //  the value of x is 5
```

179. shift() 方法刪除第一個數組元素，並將所有其他元素"移位"到較低的索引。shift() 方法返回"移出"的字符串。

```JavaScript
var fruits = ["Banana", "Orange", "Apple", "Mango"];
fruits.shift();            // Removes the first element "Banana" from fruits

var fruits = ["Banana", "Orange", "Apple", "Mango"];
var x = fruits.shift();    // the value of x is "Banana"
```

180. unshift() 方法（在開始時）向數組添加一個新元素，並"取消移位"較舊的元素。unshift() 方法返回新的數組長度。

```JavaScript
var fruits = ["Banana", "Orange", "Apple", "Mango"];
fruits.unshift("Lemon");    // Adds a new element "Lemon" to fruits

var fruits = ["Banana", "Orange", "Apple", "Mango"];
fruits.unshift("Lemon");    // Returns 5
```

181. JavaScript 運算符刪除元素 delete。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_array_delete>

182. 使用 delete 可能會在數組中留下未定義的漏洞。使用 pop（） 或 shift（） 代替。

183. splice() 方法可用於將新項目添加到數組。

```JavaScript
var fruits = ["Banana", "Orange", "Apple", "Mango"];
fruits.splice(2, 0, "Lemon", "Kiwi");
```

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_array_splice_return>

184. splice() 刪除元素而不會在數組中留下"孔"。

```JavaScript
var fruits = ["Banana", "Orange", "Apple", "Mango"];
fruits.splice(0, 1);        // Removes the first element of fruits
```

185. concat() 方法通過合併（連接）現有數組來創建新數組。該concat()方法不會更改現有數組。它總是返回一個新的數組。

```JavaScript
var myGirls = ["Cecilie", "Lone"];
var myBoys = ["Emil", "Tobias", "Linus"];
var myChildren = myGirls.concat(myBoys);   // Concatenates (joins) myGirls and myBoys
var arr1 = ["Cecilie", "Lone"];
var arr2 = ["Emil", "Tobias", "Linus"];
var arr3 = ["Robin", "Morgan"];
var myChildren = arr1.concat(arr2, arr3);   // Concatenates arr1 with arr2 and arr3
var arr1 = ["Emil", "Tobias", "Linus"];
var myChildren = arr1.concat("Peter");
```

186. slice() 方法將一個數組切成一個新數組。

```JavaScript
var fruits = ["Banana", "Orange", "Lemon", "Apple", "Mango"];
var citrus = fruits.slice(1);
// citrus = ["Orange", "Lemon", "Apple", "Mango"];
```

187. 沒有內置函數可用於查找JavaScript數組中的最高或最低值。

</details>

---

<details>
<summary><h3 style='display:inline'>JS Sorting Arrays</h3></summary>

188. sort() 方法按字母順序對數組進行排序。

```JavaScript
var fruits = ["Banana", "Orange", "Apple", "Mango"];
fruits.sort();        // Sorts the elements of fruits

// Apple,Banana,Mango,Orange
```

189. reverse() 方法反轉數組中的元素。

```JavaScript
var fruits = ["Banana", "Orange", "Apple", "Mango"];
fruits.sort();        // First sort the elements of fruits
fruits.reverse();     // Then reverse the order of the elements

// Orange,Mango,Banana,Apple
```

190. sort() 方法在對數字進行排序時將產生錯誤的結果。通過 sort(compare function) 解決此問題。

* <https://developer.mozilla.org/zh-TW/docs/Web/JavaScript/Reference/Global_Objects/Array/sort>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_array_sort3>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_array_sort2>

> 例： var points = [40, 100, 1, 5, 25, 10];
>
> 比較函數比較數組中的所有值，一次比較兩個值(a, b)。
>
> 比較40和100時，該sort()方法將調用compare函數（40，100）。
>
> 該函數計算40-100 (a - b)，並且由於結果為負數（-60），因此sort函數會將40排序為小於100的值。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_array_sort_alpha>

191. Random 排序、Fisher Yates Method

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_array_sort_random>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_array_sort_random2>

192. 沒有內置函數可用於查找數組中的最大值或最小值。但是，對數組進行排序後，可以使用索引來獲取最高和最低值。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_array_sort_low>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_array_sort_high>

```JavaScript
var points = [40, 100, 1, 5, 25, 10];
points.sort(function(a, b){return a - b});
// now points[0] contains the lowest value
// and points[points.length-1] contains the highest value

var points = [40, 100, 1, 5, 25, 10];
points.sort(function(a, b){return b - a});
// now points[0] contains the highest value
// and points[points.length-1] contains the lowest value
```

193. 如果只想查找最高（或最低）值，則對整個數組進行排序是一種非常低效的方法。
194. Math.max.apply 用來查找數組中的最高編號。Math.max.apply(null, [1, 2, 3]) 等同於 apply(Math.max, arr) 也等同於 Math.max (1, 2, 3)。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_array_sort_math_max>
* <https://kknews.cc/zh-tw/code/mjmllp9.html>

195. Math.min.apply 用來查找數組中的最小數字。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_array_sort_math_min>

196. 最快的解決方案是使用"自製"方法。此函數循環遍歷一個數組，將每個值與找到的最大值進行比較。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_array_sort_max>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_array_sort_min>

197. 排序物件陣列。`cars.sort(function(a, b){return a.year - b.year});`

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_array_sort_object1>

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_array_sort_object2>

</details>

---

<details>
<summary><h3 style='display:inline'>JS Array Iteration Methods</h3></summary>

198. forEach() 方法為每個數組元素調用一次函數（回調函數）。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_array_foreach>
* 請注意，該 forEach() 函數帶有3個參數
* 物品價值 value
* 項目索引 index
* 數組本身 itself

```JavaScript
numbers.forEach(myFunction);

function myFunction(value,index,itself) {
  txt += "index="+ index +",value=" + value +",itself=" + itself + "<br>";
}

// index=0,value=45,itself=45,4,9,16,25
// index=1,value=4,itself=45,4,9,16,25
// index=2,value=9,itself=45,4,9,16,25
// index=3,value=16,itself=45,4,9,16,25
// index=4,value=25,itself=45,4,9,16,25
```

199. map() 方法通過對每個數組元素執行功能來創建新數組。
200. map() 方法不執行沒有值的數組元素的功能。
201. map() 方法不會更改原始數組。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_array_map>

202. map() 類似 forEach() 帶有3個參數。
203. filter() 方法創建一個具有通過測試的數組元素的新數組。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_array_filter>

204. filter() 類似 forEach() 帶有3個參數。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_array_filter>

205. reduce() 方法在每個數組元素上運行一個函數，以產生（減少到）單個值。方法在數組中從左到右起作用。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_array_reduce>

206. 請注意，該 reduce() 函數採用4個參數。

* 總計（初始值/先前返回的值） total
* 物品價值 value
* 項目索引 index
* 數組本身 itself

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_array_reduce_initial>

207. reduceRight() 從作品從右到左在數組中。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_array_reduce_right>

208. every() 方法檢查所有數組值是否都通過測試。反傳布林值

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_array_every>

209. some() 方法檢查某些數組值是否通過測試。反傳布林值

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_array_some>

210. indexOf() 方法在數組中搜索元素值並返回其位置。如果找不到該項目，則返回-1。如果該項目多次出現，它將返回第一次出現的位置。

<https://www.w3schools.com/js/tryit.asp?filename=tryjs_array_indexof>

211. Array.lastIndexOf() 與相同 Array.indexOf()，但返回指定元素最後一次出現的位置。
212. find() 方法返回通過測試函數的第一個數組元素的值。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_array_find>

213. findIndex() 方法返回通過測試函數的第一個數組元素的索引。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_array_find_index>

</details>

---

<details>
<summary><h3 style='display:inline'>JS Date Objects</h3></summary>

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_date_current>

```JavaScript
new Date()
new Date(year, month, day, hours, minutes, seconds, milliseconds)
new Date(milliseconds)
new Date(date string)
```

214. 日期對像是靜態的。計算機時間正在滴答作響，但日期對象卻沒有。
215. 年，月，日，小時，分鐘，秒和毫秒。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_date_new_all>

216. 您不能省略月份。如果僅提供一個參數，它將被視為毫秒。一天（24小時）為8,640萬毫秒。
217. 一位數和兩位數的年份將解釋為19xx。`var d = new Date(99, 11, 24);`
218. new Date(dateString) 從日期字符串創建一個新的日期對象。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_date_new_string>

219. JavaScript 將（默認）以全文字符串格式輸出日期。
220. 在 HTML 中顯示日期對象時，該方法會自動將其轉換為字符串toString()。Wed Jun 10 2020 00:45:36 GMT+0800 (台北標準時間)
221. toUTCString() 方法將日期轉換為UTC字符串（日期顯示標準）。Tue, 09 Jun 2020 16:45:52 GMT
222. toDateString() 方法將日期轉換為更易讀的格式。Wed Jun 10 2020

</details>

---

<details>
<summary><h3 style='display:inline'>JS Date Formats</h3></summary>

223. 通常有3種類型的JavaScript日期輸入格式。

| Type       | Example                                   |
| ---------- | ----------------------------------------- |
| ISO Date   | "2015-03-25" (The International Standard) |
| Short Date | "03/25/2015"                              |
| Long Date  | "Mar 25 2015" or "25 Mar 2015"            |

224. ISO格式遵循JavaScript中的嚴格標準。其他格式的定義不太好，可能與瀏覽器有關。
225. JavaScript（默認情況下）將以全文字符串格式輸出日期。
226. ISO 8601（YYYY-MM-DD）是表示日期和時間的國際標準。
227. 計算的日期將相對於您的時區。
228. ISO（YYYY-MM-DDTHH：MM：SSZ）。日期和時間用大寫字母T分隔。UTC時間以大寫字母Z定義。
229. UTC（世界標準時間）與GMT（格林威治標準時間）相同。
230. 在不同的瀏覽器中，在日期時間字符串中省略T或Z可以得出不同的結果。
231. JavaScript 短日期預設格式為。"MM/DD/YYYY"
232. 在某些瀏覽器中，幾個月或幾天沒有前導零可能會產生錯誤。"2015-3-25"
233. 未定義YYYY/MM/DD的行為。一些瀏覽器會嘗試猜測格式。有些會返回NaN。"2015/03/25"
234. DD-MM-YYYY的行為也未定義。一些瀏覽器會嘗試猜測格式。有些會返回NaN。"25-03-2015"
235. JavaScript 長日期預設格式為。MMM DD YYYY (月和日可以按任何順序排列)"25 Mar 2015"。月份可以用完整的（1月）或縮寫的（1月）來寫。"January 25 2015"。
236. 逗號被忽略。名稱不區分大小寫 "JANUARY, 25, 2015"
237. Date.parse() 返回日期與1970年1月1日之間的毫秒數

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_date_parse>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_date_convert>

</details>

---

<details>
<summary><h3 style='display:inline'>JS Get Date Methods</h3></summary>

| Method            | Description                                       |
| ----------------- | ------------------------------------------------- |
| getFullYear()     | Get the year as a four digit number (yyyy)        |
| getMonth()        | Get the month as a number (0-11)                  |
| getDate()         | Get the day as a number (1-31)                    |
| getHours()        | Get the hour (0-23)                               |
| getMinutes()      | Get the minute (0-59)                             |
| getSeconds()      | Get the second (0-59)                             |
| getMilliseconds() | Get the millisecond (0-999)                       |
| getTime()         | Get the time (milliseconds since January 1, 1970) |
| getDay()          | Get the weekday as a number (0-6)                 |
| Date.now()        | Get the time. ECMAScript 5.                       |

238. getTime() 方法返回自1970年1月1日以來的毫秒數。
239. UTC日期方法用於處理UTC日期（通用時區日期）。

| Method               | Description                                                 |
| -------------------- | ----------------------------------------------------------- |
| getUTCDate()         | Same as getDate(), but returns the UTC date                 |
| getUTCDay()          | Same as getDay(), but returns the UTC day                   |
| getUTCFullYear()     | Same as getFullYear(), but returns the UTC year             |
| getUTCHours()        | Same as getHours(), but returns the UTC hour                |
| getUTCMilliseconds() | Same as getMilliseconds(), but returns the UTC milliseconds |
| getUTCMinutes()      | Same as getMinutes(), but returns the UTC minutes           |
| getUTCMonth()        | Same as getMonth(), but returns the UTC month               |
| getUTCSeconds()      | Same as getSeconds(), but returns the UTC seconds           |

</details>

---

<details>
<summary><h3 style='display:inline'>JS Set Date Methods</h3></summary>

| Method            | Description                                       |
| ----------------- | ------------------------------------------------- |
| setDate()         | Set the day as a number (1-31)                    |
| setFullYear()     | Set the year (optionally month and day)           |
| setHours()        | Set the hour (0-23)                               |
| setMilliseconds() | Set the milliseconds (0-999)                      |
| setMinutes()      | Set the minutes (0-59)                            |
| setMonth()        | Set the month (0-11)                              |
| setSeconds()      | Set the seconds (0-59)                            |
| setTime()         | Set the time (milliseconds since January 1, 1970) |

240. 日期可以輕鬆比較。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_date_compare>

241. JavaScript 的月份從 0 到 11。一月是 0。12 月是 11。

</details>

---

<details>
<summary><h3 style='display:inline'>JS Math Object</h3></summary>

242. Math.PI;  // returns 3.141592653589793

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_math_pi>

243. Math.round(x) 返回四捨五入到最接近的整數的x的值。

```JavaScript
Math.round(4.7);    // returns 5
Math.round(4.4);    // returns 4
```

244. Math.pow(x, y) 將x的值返回為y的冪。`Math.pow(8, 2); // returns 64`
245. Math.sqrt(x) 返回x的平方根。`Math.sqrt(64); // returns 8`
246. Math.abs(x) 返回x的絕對（正）值。`Math.abs(-4.7); // returns 4.7`
247. Math.ceil(x)返回x的值舍入最多到其最接近的整數。`Math.ceil(4.4); // returns 5`
248. Math.floor(x)返回x的值四捨五入不進位。`Math.floor(4.7); // returns 4`
249. Math.sin(x) 返回角度x（以弧度為單位）的正弦值（介於-1和1之間的值）。要使用度數而不是弧度，則必須將度數轉換為弧度。以弧度為單位的角度=以度為單位的角度x PI / 180。

`Math.sin(90 * Math.PI / 180); // returns 1 (the sine of 90 degrees)`

250. Math.cos(x) 返回角度x（以弧度為單位）的餘弦值（介於-1和1之間的值）。

* <https://www.learnmode.net/upload/flip/book/9e/9ed843cb41ca54d0/ffd22aeed4d2.pdf>

251. Math.min() 並且Math.max() 可用於查找的參數列表中的最低或最高值。

```JavaScript
Math.min(0, 150, 30, 20, -8, -200);  // returns -200
Math.max(0, 150, 30, 20, -8, -200);  // returns 150
```

252. Math.random() 返回介於0（含）和1（不含）之間的隨機數。
253. JavaScript 提供了8個數學常數，可以使用 Math 對象進行訪問。

```JavaScript
Math.E        // returns Euler's number
Math.PI       // returns PI
Math.SQRT2    // returns the square root of 2
Math.SQRT1_2  // returns the square root of 1/2
Math.LN2      // returns the natural logarithm of 2
Math.LN10     // returns the natural logarithm of 10
Math.LOG2E    // returns base 2 logarithm of E
Math.LOG10E   // returns base 10 logarithm of E
```

254. 與其他全局對像不同，Math對像沒有構造函數。方法和屬性是靜態的。無需先創建Math對象即可使用所有方法和屬性（常量）。

| Method               | Description                                                                   |
| -------------------- | ----------------------------------------------------------------------------- |
| abs(x)               | Returns the absolute value of x                                               |
| acos(x)              | Returns the arccosine of x, in radians                                        |
| acosh(x)             | Returns the hyperbolic arccosine of x                                         |
| asin(x)              | Returns the arcsine of x, in radians                                          |
| asinh(x)             | Returns the hyperbolic arcsine of x                                           |
| atan(x)              | Returns the arctangent of x as a numeric value between -PI/2 and PI/2 radians |
| atan2(y, x)          | Returns the arctangent of the quotient of its arguments                       |
| atanh(x)             | Returns the hyperbolic arctangent of x                                        |
| cbrt(x)              | Returns the cubic root of x                                                   |
| ceil(x)              | Returns x, rounded upwards to the nearest integer                             |
| cos(x)               | Returns the cosine of x (x is in radians)                                     |
| cosh(x)              | Returns the hyperbolic cosine of x                                            |
| exp(x)               | Returns the value of Ex                                                       |
| floor(x)             | Returns x, rounded downwards to the nearest integer                           |
| log(x)               | Returns the natural logarithm (base E) of x                                   |
| max(x, y, z, ..., n) | Returns the number with the highest value                                     |
| min(x, y, z, ..., n) | Returns the number with the lowest value                                      |
| pow(x, y)            | Returns the value of x to the power of y                                      |
| random()             | Returns a random number between 0 and 1                                       |
| round(x)             | Rounds x to the nearest integer                                               |
| sin(x)               | Returns the sine of x (x is in radians)                                       |
| sinh(x)              | Returns the hyperbolic sine of x                                              |
| sqrt(x)              | Returns the square root of x                                                  |
| tan(x)               | Returns the tangent of an angle                                               |
| tanh(x)              | Returns the hyperbolic tangent of a number                                    |
| trunc(x)             | Returns the integer part of a number (x)                                      |

</details>

---

<details>
<summary><h3 style='display:inline'>JS Random</h3></summary>

255. Math.random()與with Math.floor()一起使用可用於返回隨機整數。

```JavaScript
Math.floor(Math.random() * 10);     // returns a random integer from 0 to 9
Math.floor(Math.random() * 11);      // returns a random integer from 0 to 10
Math.floor(Math.random() * 100);     // returns a random integer from 0 to 99
Math.floor(Math.random() * 101);     // returns a random integer from 0 to 100
Math.floor(Math.random() * 10) + 1;  // returns a random integer from 1 to 10
Math.floor(Math.random() * 100) + 1; // returns a random integer from 1 to 100
```

256. 創建一個適當的隨機函數以用於所有隨機整數目的可能是個好主意。

```JavaScript
// 此JavaScript函數始終返回介於min（包括）和max（排除）之間的隨機數
function getRndInteger(min, max) {
  return Math.floor(Math.random() * (max - min) ) + min;
}
// 此JavaScript函數始終返回介於min和max之間的隨機數（均包括在內）
function getRndInteger(min, max) {
  return Math.floor(Math.random() * (max - min + 1) ) + min;
}
```

</details>

---

<details>
<summary><h3 style='display:inline'>JS Booleans</h3></summary>

257. JavaScript 具有布爾數據類型。它只能採用 true 或 false 值。無法使用 YES / NO 、 ON / OFF
258. Boolean()函數來查找表達式（或變量）是否為真

```JavaScript
Boolean(10 > 9) // returns true
(10 > 9)        // also returns true
10 > 9          // also returns true
```

259. 一切帶有"Value"的東西都是 true。沒有"Value"的一切都是 false。

```JavaScript
Boolean(100) is true
Boolean(3.14) is true
Boolean(-15) is true
Any (not empty) string Boolean("Hello") is true
Even the string Boolean('false') is true
Any expression (except zero) Boolean(1 + 7 + 3.14) is true
---
var x = 0;
Boolean(x);       // returns false

var x = -0;
Boolean(x);       // returns false

var x = "";
Boolean(x);       // returns false

var x;
Boolean(x);       // returns false

var x = null;
Boolean(x);       // returns false

var x = false;
Boolean(x);       // returns false

var x = 10 / "H";
Boolean(x);       // returns false

var y = new Boolean(false) //布爾值可以是對象
```

</details>

---

<details>
<summary><h3 style='display:inline'>JS Comparison and Logical Operators</h3></summary>

260. 比較運算符號。

| Operator | Description                       | Comparing | Returns |
| -------- | --------------------------------- | --------- | ------- |
| ==       | equal to                          | x == 8    | false   |
|          |                                   | x == 5    | true    |
|          |                                   | x == "5"  | true    |
| ===      | equal value and equal type        | x === 5   | true    |
|          |                                   | x === "5" | false   |
| !=       | not equal                         | x != 8    | true    |
| !==      | not equal value or not equal type | x !== 5   | false   |
|          |                                   | x !== "5" | true    |
|          |                                   | x !== 8   | true    |
| >        | greater than                      | x > 8     | false   |
| <        | less than                         | x < 8     | true    |
| >=       | greater than or equal to          | x >= 8    | false   |
| <=       | less than or equal to             | x <= 8    | true    |

261. if (age < 18) text = "Too young";
262. 邏輯運算符。

| Operator | Description | Example                                        |
| -------- | ----------- | ---------------------------------------------- |
| &&       | and         | (x < 10 && y > 1) is true                      |
| \|\|     | or          | (x == 5                  \|\| y == 5) is false |
| !        | not         | !(x == y) is true                              |

263. 條件（三元）運算符 variablename = (condition) ? value1:value2 `var voteable = (age < 18) ? "Too young":"Old enough";`

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_comparison>

264. 在將字符串與數字進行比較時，JavaScript 會在進行比較時將字符串轉換為數字。空字符串將轉換為0。非數字字符串將NaN始終轉換為false。
265. 為了確保獲得正確的結果，在比較之前，應將變量轉換為正確的類型。

```JavaScript
age = Number(age);
if (isNaN(age)) {
  voteable = "Input is not a number";
} else {
  voteable = (age < 18) ? "Too young" : "Old enough";
}
```

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_comparison_12>

</details>

---

<details>
<summary><h3 style='display:inline'>JS if else and else if</h3></summary>

* 使用 if 指定的代碼塊將被執行，如果一個指定的條件是真
* 使用 else 指定的代碼塊將被執行，如果相同的條件為假
* 使用 else if 指定一個新的條件測試，如果第一個條件為假
* 使用 switch 指定的代碼許多替代塊被執行

266. 請注意，這 if 是小寫字母。大寫字母（If或IF）將生成 JavaScript 錯誤。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_ifthen>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_ifthenelse>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_elseif>

</details>

---

<details>
<summary><h3 style='display:inline'>JS Switch Statement</h3></summary>

* 開關表達式只計算一次。
* 將表達式的值與每種情況的值進行比較。
* 如果匹配，則執行相關的代碼塊。
* 如果不匹配，則執行默認代碼塊。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_switch>

267. 當 JavaScript 到達break 關鍵字時，它將脫離 switch 塊。
268. default 關鍵字指定的代碼運行，如果沒有匹配的情況下。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_switch2>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_switch4>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_switch3>

269. switch 案例使用嚴格的比較（===）。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_switch5>

</details>

---

<details>
<summary><h3 style='display:inline'>JS For Loop</h3></summary>

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_loop_for>

270. JavaScript 支持各種循環。

* for -遍歷代碼塊多次
* for/in -遍歷對象的屬性
* for/of -遍歷可迭代對象的值
* while -在指定條件為真時循環遍歷代碼塊
* do/while -在指定條件為true時也循環遍歷代碼塊
* for(語句１；語句２；語句３)｛｝

271. 語句1在執行代碼塊之前執行（一次）。語句2定義了執行代碼塊的條件。語句3在每次執行代碼塊後都會執行（每次）。
272. 可以在語句1中啟動許多值（用逗號分隔）

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_loop_for_om1>

273. 可以省略語句1（例如，在循環開始之前設置值）語句2也是可選的。如果省略語句2，則必須在循環內提供一個中斷。否則，循環將永遠不會結束。這將使您的瀏覽器崩潰。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_loop_for_om2>

274. 語句3通常會增加初始變量的值。語句3是可選的。語句3可以執行負增量（i--），正增量（i = i + 15）之類的任何操作。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_loop_for_om3>

275. for/in 語句遍歷對象的屬性。

```JavaScript
var person = {fname:"John", lname:"Doe", age:25};

var text = "";
var x;
for (x in person) {
  text += person[x];
}

// John Doe 25
```

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_object_for_in>

276. for/of 使您可以遍歷可迭代的數據結構，例如數組，字符串，映射，NodeList等。

```JavaScript
var cars = ['BMW', 'Volvo', 'Mini'];
var x;

for (x of cars) {
  document.write(x + "<br >");
}

// BMW
// Volvo
// Mini

var txt = 'JavaScript';
var x;

for (x of txt) {
  document.write(x + "<br >");
}

// J
// a
// v
// a
// S
// c
// r
// i
// p
// t
```

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_object_for_of>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_object_for_of2>

</details>

---

<details>
<summary><h3 style='display:inline'>JS While Loop</h3></summary>

277. while 只要指定條件為真，循環就循環遍歷代碼塊。如果忘記增加條件中使用的變量，循環將永遠不會結束。這將使您的瀏覽器崩潰。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_while>

278. do/while 循環是 while 循環的變體。在檢查條件是否為真之前，此循環將執行一次代碼塊，然後只要條件為真，它將重複該循環。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_dowhile>

```JavaScript
var cars = ["BMW", "Volvo", "Saab", "Ford"];
var i = 0;
var text = "";

for (;cars[i];) {
  text += cars[i] + "<br>";
  i++;
}

---

var cars = ["BMW", "Volvo", "Saab", "Ford"];
var i = 0;
var text = "";

while (cars[i]) {
  text += cars[i] + "<br>";
  i++;
}
```

</details>

---

<details>
<summary><h3 style='display:inline'>JS Break and Continue</h3></summary>

279. break 聲明中"跳出來"的循環中。

```JavaScript
for (i = 0; i < 10; i++) {
  if (i === 3) { break; }
  text += "The number is " + i + "<br>";
}
```

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_break>

280. continue 語句在循環中"跳過"一個迭代。

```JavaScript
for (i = 0; i < 10; i++) {
  if (i === 3) { continue; }
  text += "The number is " + i + "<br>";
}
```

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_continue>

281. [帶有標籤引用(在語句之前加上標籤名稱和冒號)，break、continue 語句可用於跳出任何代碼塊。代碼塊是{和}之間的代碼塊](#)。

```JavaScript
var cars = ["BMW", "Volvo", "Saab", "Ford"];
list: {
  text += cars[0] + "<br>";
  text += cars[1] + "<br>";
  break list;
  text += cars[2] + "<br>";
  text += cars[3] + "<br>";
}
```

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_break_list>

</details>

---

<details>
<summary><h3 style='display:inline'>JS Type Conversion</h3></summary>

282. Number（） 轉換為 Number，String（） 轉換為 String，Boolean（） 轉換為 Boolean。

## JavaScript數據類型

### 在JavaScript中，有 5 種不同的數據類型可以包含值：

* string
* number
* boolean
* object
* function

### 有 6 種類型的物件：

* Object
* Date
* Array
* String
* Number
* Boolean

### 還有 2 種不能包含值的數據類型：

* null
* undefined

```JavaScript
typeof "John"                 // Returns "string"
typeof 3.14                   // Returns "number"
typeof NaN                    // Returns "number"
typeof false                  // Returns "boolean"
typeof [1,2,3,4]              // Returns "object"
typeof {name:'John', age:34}  // Returns "object"
typeof new Date()             // Returns "object"
typeof function () {}         // Returns "function"
typeof myCar                  // Returns "undefined" *
typeof null                   // Returns "object"
```

283. NaN 的數據類型是數字。
284. 陣列的數據類型是物件。
285. 日期的數據類型是物件。
286. null 的數據類型是物件。
287. 未定義變量的數據類型是未定義 *。
288. 尚未分配值的變量的數據類型也未定義 *。
289. typeof　不能用來確定　JavaScript　對像是否為數組（或日期）。
290. constructor　建設子屬性返回所有　JavaScript　變量的構造函數。

```JavaScript
"John".constructor                // Returns function String()  {[native code]}
(3.14).constructor                // Returns function Number()  {[native code]}
false.constructor                 // Returns function Boolean() {[native code]}
[1,2,3,4].constructor             // Returns function Array()   {[native code]}
{name:'John',age:34}.constructor  // Returns function Object()  {[native code]}
new Date().constructor            // Returns function Date()    {[native code]}
function () {}.constructor        // Returns function Function(){[native code]}
```

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_array_isarray>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_type_isarray>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_date_isdate>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_type_isdate>

291. JavaScript 類型轉換，通過使用 JavaScript 函數，或 JavaScript 本身自動。
292. 全局方法 String() 可以將數字轉換為字符串。Number 方法的 toString() 作用相同。

| Method          | Description                                                    |
| --------------- | -------------------------------------------------------------- |
| toExponential() | 返回一個字符串，其字符串以指數表示法四捨五入並寫入。           |
| toFixed()       | 返回一個字符串，該字符串的數字四捨五入並以指定的小數位數書寫。 |
| toPrecision()   | 返回一個字符串，該字符串帶有指定長度的數字。                   |

293. 在"日期方法"一章中，您將找到更多可用於將日期轉換為字符串的方法。

| Method            | Description                                       |
| ----------------- | ------------------------------------------------- |
| getDate()         | Get the day as a number (1-31)                    |
| getDay()          | Get the weekday a number (0-6)                    |
| getFullYear()     | Get the four digit year (yyyy)                    |
| getHours()        | Get the hour (0-23)                               |
| getMilliseconds() | Get the milliseconds (0-999)                      |
| getMinutes()      | Get the minutes (0-59)                            |
| getMonth()        | Get the month (0-11)                              |
| getSeconds()      | Get the seconds (0-59)                            |
| getTime()         | Get the time (milliseconds since January 1, 1970) |

294. 全局方法 Number() 可以將字符串轉換為數字。

| Method       | Description                                         |
| ------------ | --------------------------------------------------- |
| parseFloat() | Parses a string and returns a floating point number |
| parseInt()   | Parses a string and returns an integer              |

295. 一元運算符+可用於一個變量轉換為一個數字。如果該變量不能轉換，它將仍然變成一個數字，但是帶有值NaN （不是數字）

```JavaScript
var y = "5";      // y is a string
var x = + y;      // x is a number

var y = "John";   // y is a string
var x = + y;      // x is a number (NaN)
```

296. 全局方法Number()還可以將布爾值轉換為數字。

```JavaScript
Number(false)     // returns 0
Number(true)      // returns 1
```

297. 全局方法 Number() 可用於將日期轉換為數字。

```JavaScript
d = new Date();
Number(d)          // returns 1404568027739
d.getTime()        // returns 1404568027739
```

298. [自動類型轉換。當JavaScript嘗試對"錯誤的"數據類型進行操作時，它將嘗試將值轉換為"正確的"類型。](#)

```JavaScript
5 + null    // returns 5         because null is converted to 0
"5" + null  // returns "5null"   because null is converted to "null"
"5" + 2     // returns "52"      because 2 is converted to "2"
"5" - 2     // returns 3         because "5" is converted to 5
"5" * "2"   // returns 10        because "5" and "2" are converted to 5 and 2
```

299. - * / 似乎都會運作，只有＋會是轉為字串相加
300. toString()　當您嘗試＂輸出＂對像或變量時，JavaScript　自動調用變量的函數

| Original　Value  | Converted　to Number | Converted　to String | Converted　to Boolean |
| ---------------- | -------------------- | -------------------- | --------------------- |
| false            | 0                    | "false"              | false                 |
| true             | 1                    | "true"               | true                  |
| 0                | 0                    | "0"                  | false                 |
| 1                | 1                    | "1"                  | true                  |
| "0"              | 0                    | "0"                  | ＄＄true              |
| "000"            | 0                    | "000"                | ＄＄true              |
| "1"              | 1                    | "1"                  | true                  |
| NaN              | NaN                  | "NaN"                | false                 |
| Infinity         | Infinity             | "Infinity"           | true                  |
| -Infinity        | -Infinity            | "-Infinity"          | true                  |
| ""               | ＄＄0                | ""                   | ＄＄false             |
| "20"             | 20                   | "20"                 | true                  |
| "twenty"         | NaN                  | "twenty"             | true                  |
| [ ]              | ＄＄0                | ""                   | true                  |
| [20]             | ＄＄20               | "20"                 | true                  |
| [10,20]          | NaN                  | "10,20"              | true                  |
| ["twenty"]       | NaN                  | "twenty"             | true                  |
| ["ten","twenty"] | NaN                  | "ten,twenty"         | true                  |
| function(){}     | NaN                  | "function(){}"       | true                  |
| { }              | NaN                  | "[object Object]"    | true                  |
| null             | ＄＄0                | "null"               | false                 |
| undefined        | NaN                  | "undefined"          | false                 |

引號中的值表示字符串值。

＄＄值表示程序員可能不會期望的值（某些）。

</details>

---

<details>
<summary><h3 style='display:inline'>JS Bitwise Operations</h3></summary>

* <https://www.w3schools.com/js/js_bitwise.asp>

301. JavaScript 按位運算符。

| Name                  | Description                                          | Operation    | Result | Same as     | Result |
| --------------------- | ---------------------------------------------------- | ------------ | ------ | ----------- | ------ |
| AND                   | 如果兩個位均為1，則將每個位設置為1                   | 5 & 1        | 1      | 0101 & 0001 | 0001   |
| OR                    | 如果兩位之一為1，則將每個位設置為1                   | 5       \| 1 | 5      | 0101        | 0001   | 0101 |
| XOR                   | 如果只有一位為1，則將每個位設置為1                   | ~ 5          | 10     | ~0101       | 1010   |
| NOT                   | 反轉所有位                                           | 5 << 1       | 10     | 0101 << 1   | 1010   |
| Zero fill left shift  | 通過從右向右推零向左移動，讓最左邊的位掉落           | 5 ^ 1        | 4      | 0101 ^ 0001 | 0100   |
| Signed right shift    | 通過從左側推入最左邊的位向右移動，並讓最右邊的位掉落 | 5 >> 1       | 2      | 0101 >> 1   | 0010   |
| Zero fill right shift | 向左移動零，向右移動，讓最右邊的位掉落               | 5 >>> 1      | 2      | 0101 >>> 1  | 0010   |

302. JavaScript 將數字存儲為 64 位浮點數，但是所有按位運算都是對 32 位二進制數執行的。
303. 感覺這在前端比較不會遇到，等遇到的時候再補看。

</details>

---

<details>
<summary><h3 style='display:inline'>JS Regular Expressions</h3></summary>

### 有很多範例，需要時再查看。

* <https://www.w3schools.com/js/js_regexp.asp>

304. 正則表達式是形成搜索模式的一系列字符。搜索模式可用於文本搜索和文本替換操作。 /pattern/modifiers; `var patt = /w3schools/i;`

* / w3schools / i   是一個正則表達式。
* w3schools   是一種模式（用於搜索）。
* i   是修飾符（將搜索修改為不區分大小寫）。

305. 則表達式通常與兩個字符串方法一起使用：search() 和 replace()。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_regexp_string_search>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_regexp_string_replace>

306. 正則表達式修飾符

| Modifier | Description                                            |
| -------- | ------------------------------------------------------ |
| i        | 執行不區分大小寫的匹配                                 |
| g        | 執行全局比賽（查找所有比賽，而不是在第一場比賽后停止） |
| m        | 執行多行匹配                                           |

307. 正則表達式模式。方括號用於查找一系列字符

| Expression       | Description                |
| ---------------- | -------------------------- |
| [abc]            | 查找方括號之間的任何字符   |
| [0-9]            | 查找方括號之間的任何數字   |
| (x         \| y) | 查找以\|分隔的任何替代方案 |

308. 元字符是具有特殊含義的字符

| Metacharacter | Description                                                                   |
| ------------- | ----------------------------------------------------------------------------- |
| \d            | 找一個數字                                                                    |
| \s            | 查找空格字符                                                                  |
| \b            | 在這樣的單詞開頭找到匹配項：\ bWORD，或者在這樣的單詞結尾找到匹配項：WORD \ b |
| \uxxxx        | 查找由十六進制數字xxxx指定的Unicode字符                                       |

309. 量詞定義數量

| Quantifier | Description                     |
| ---------- | ------------------------------- |
| n+         | 匹配任何包含至少一個n的字符串   |
| n*         | 匹配包含零個或多個n的任何字符串 |
| n?         | 匹配任何包含零或一出現n的字符串 |

310. JavaScript 中，RegExp 物件是具有預定義屬性和方法的正則表達式物件。
311. test()　方法是　RegExp　表達式方法。字符串中搜索模式，並根據結果返回true或false。

`var patt = /e/;　patt.test("The best things in life are free!");`

312. 不必先將正則表達式放在變量中。上面的兩行可以縮短為一

`/e/.test("The best things in life are free!");`

313. exec() 方法在字符串中搜索指定的模式，然後將找到的文本作為物件返回。如果找不到匹配項，它將返回一個空（空）物件。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_regexp_exec>

</details>

---

<details>
<summary><h3 style='display:inline'>JS Errors - Throw and Try to Catch</h3></summary>

* try 語句使您可以測試代碼塊是否存在錯誤。
* catch 語句使您可以處理錯誤。
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_try_catch>
* throw 語句使您可以創建自定義錯誤。
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_throw_error>
* finally 語句使您可以在嘗試並捕獲之後執行代碼，而不管結果如何。
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_finally_error>

314. 現代瀏覽器通常會結合使用 JavaScript 和內置 HTML 驗證，並使用在 HTML 屬性中定義的預定義驗證規則。

`<input id="demo" type="number" min="5" max="10" step="1">`

315. 錯誤值 err 物件。 Error Object - name 設置或返回錯誤名稱、message 設置或返回錯誤消息（字符串）

| name           | Description               |
| -------------- | ------------------------- |
| EvalError      | eval（）函數中發生錯誤    |
| RangeError     | 發生數字"超出範圍"        |
| ReferenceError | 發生非法引用              |
| SyntaxError    | 發生語法錯誤              |
| TypeError      | 發生類型錯誤              |
| URIError       | 發生encodeURI（）中的錯誤 |

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_error_rangeerror>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_error_referenceerror>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_error_syntaxerror>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_error_typeerror>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_error_urierror>

</details>

---

<details>
<summary><h3 style='display:inline'>JS Scope</h3></summary>

316. 範圍確定變量的可訪問性（可見性）。
317. 在JavaScript中，作用域有兩種類型：當地範圍、全球範圍
318. 每個函數都創建一個新作用域。從函數外部無法訪問（可見）在函數內部定義的變量。在 JavaScript 中，對象和函數也是變量。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_scope_local>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_scope_global>

319. 如果為尚未聲明的變量賦值，它將自動成為 GLOBAL 變量。
320. 自動全局，此代碼示例將聲明一個全局變量 carName，即使該值是在函數內部分配的也是如此。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_local_global>

321. [所有現代瀏覽器都支持在"嚴格模式"下運行JavaScript。在"嚴格模式"下，未聲明的變量不是自動全局的](#)。
322. 在 HTML 中，全局範圍是 window 對象。所有全局變量都屬於 window 對象。
323. 除非您打算，否則不要創建全局變量。您的全局變量（或函數）可以覆蓋窗口變量（或函數）。任何函數，包括窗口對象，都可以覆蓋全局變量和函數。
324. [JavaScript 變量的生命週期從聲明開始。函數完成後，將刪除局部變量並轉為函數參數（參數）在函數內部充當局部變量。在Web瀏覽器中，關閉瀏覽器窗口（或選項卡）時，全局變量將被刪除](#)。

</details>

---

<details>
<summary><h3 style='display:inline'>JS Hoisting</h3></summary>

325. 提升是 JavaScript 將所有聲明移到當前作用域的頂部（當前腳本或當前函數的頂部）。
326. 在 JavaScript 中，變量可以在使用後聲明。 變量可以在聲明之前使用。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_hoisting1>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_hoisting2>

327. [JavaScript 僅提升聲明，而不提升初始化。在初始化當下找不到變數則為 undefined](#)。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_hoisting3>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_hoisting5>

328. 對於許多開發人員而言，提升是 JavaScript 的未知或被忽略的行為。如果未聲明變量，則嚴格模式下的JavaScript不允許使用變量。

</details>

---

<details>
<summary><h3 style='display:inline'>JS Use Strict</h3></summary>

329. "use strict"; 定義JavaScript代碼應在"嚴格模式"下執行。 es5+ 新指令

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_strict_variable>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_strict_global>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_strict_local>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_strict_object>

330. 在普通 JavaScript 中，開發人員將不會收到將值分配給不可寫屬性的任何錯誤反饋。
331. 在嚴格模式下，對不可寫屬性，僅 getter 屬性，不存在屬性，不存在變量或不存在對象的任何賦值都會引發錯誤。
332. 不允許使用未聲明的對象、不允許刪除變量（或對象）、不允許重複參數名稱、不允許刪除功能、不允許使用八進制數字文字、不允許使用八進制轉義字符、不允許寫入只讀屬性、不允許寫入僅獲取屬性、不允許刪除不可刪除的屬性、該單詞 eval、arguments、implements、interface、let、package、private、protected、public、static、yield 不能用作變量、with 語句是不允許的、eval() 不允許在調用它的範圍內創建變量

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_strict_delete>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_strict_delete_function>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_strict_duplicate>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_strict_octal>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_strict_escape>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_strict_readonly>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_strict_getonly>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_strict_deleteprop>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_strict_eval>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_strict_width>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_strict_eval2>

333. this 關鍵字是指調用該函數的對象。如果未指定對象，則將返回嚴格模式下的 undefined 函數，而正常模式下的函數將返回全局對象（窗口）

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_strict_this>

</details>

---

<details>
<summary><h3 style='display:inline'>JS this Keyword</h3></summary>

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_this_method>

334. this 關鍵字引用其所屬的對象。

> * 在方法 method 中，this 指向所有者物件 owner object。
> * 單獨，this 指向全局物件 global object [object Window]。
> * 在函數 function 中，this 指向全局物件 global object [object Window]。
> * 在函數 function 中，在嚴格模式下 this 為 undefined。
> * 在事件 event 中，this 指向接收事件 event 的元素 element。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_this_method>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_this>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_this_alone>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_this_function>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_this_strict>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_this_event>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_this_object>

> * Methods 方法像是 call()，apply() 它們都可以用於調用以另一個物件作為參數的物件方法。指向 this 到任何物件。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_this_call>

</details>

---

<details>
<summary><h3 style='display:inline'>JS Let</h3></summary>

335. ES2015 引入了兩個重要的新 JavaScript 關鍵字：let 和 const。提供了塊作用域 Block Scope 變量（和常量）。
336. 在ES2015之前，JavaScript 僅具有兩種類型的範圍：Global Scope 和 Function Scope。可以從 JavaScript 程序中的任何位置訪問全局變量。局部變量只能從聲明它們的函數內部訪問。
337. JavaScript Block Scope 塊範圍

```JavaScript
{
  var x = 2;
}
// x CAN be used here

---

{
  let x = 2;
}
// x can NOT be used here
```

338. var 關鍵字重新聲明變量會帶來問題。let 在塊內重新聲明變量不會在塊外重新聲明變量

```JavaScript
var x = 10;
// Here x is 10
{
  var x = 2;
  // Here x is 2
}
// Here x is 2

---

var x = 10;
// Here x is 10
{
  let x = 2;
  // Here x is 2
}
// Here x is 10
```

339. loop 範圍：var，在循環中聲明的變量在循環外重新聲明了變量。let，在循環中聲明的變量不會在循環外重新聲明該變量。let 變量僅在循環內可見。

```JavaScript
var i = 5;
for (var i = 0; i < 10; i++) {
  // some statements
}
// Here i is 10

---

let i = 5;
for (let i = 0; i < 10; i++) {
  // some statements
}
// Here i is 5
```

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_let_for_loop1>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_let_for_loop2>

340. 函數內部聲明時，用var和聲明的變量let非常相似，都將具有 function 範圍。
341. var 和聲明的變量 let 在塊外聲明時非常相似。都將具有全球範圍。
342. JavaScript，全局範圍就是 JavaScript 環境。HTML中，全局範圍是window對象。let 關鍵字定義的全局變量不屬於 window 對象。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_let_scope>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_let_global>

343. var 在程序中的任何位置都可以重新宣告 JavaScript 變量。
344. 不允許在相同作用域或相同塊中使用相同聲明 var、let。

```JavaScript
var x = 2;       // Allowed
let x = 3;       // Not allowed

{
  var x = 4;   // Allowed
  let x = 5   // Not allowed
}

---

let x = 2;       // Allowed
let x = 3;       // Not allowed

{
  let x = 4;   // Allowed
  let x = 5;   // Not allowed
}

---

let x = 2;       // Allowed
var x = 3;       // Not allowed

{
  let x = 4;   // Allowed
  var x = 5;   // Not allowed
}

---

let x = 2;       // Allowed

{
  let x = 3;   // Allowed
}

{
  let x = 4;   // Allowed
}
```

345. 變量 let 未提升到頂部。 let 在聲明變量之前使用變量將導致 ReferenceError。

</details>

---

<details>
<summary><h3 style='display:inline'>JS Const</h3></summary>

346. const 行為類似於 let 變量，但不能重新分配它們。
347. const 與 let 涉及 Block Scope 相似。
348. const 在聲明 JavaScript 變量時，必須為其賦值。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_const_value>

```JavaScript
var x = 10;
// Here x is 10
{
  const x = 2;
  // Here x is 2
}
// Here x is 10

---

const PI; // This will give an error
PI = 3.14159265359;
```

349. 不能更改常量原始值，但是可以更改常量物件的屬性。但是不能重新分配一個常量物件。常數陣列可以改變，不能重新分配一個常量陣列。

```JavaScript
// You can create a const object:
const car = {type:"Fiat", model:"500", color:"white"};

// You can change a property:
car.color = "red";

// You can add a property:
car.owner = "Johnson";

---

const car = {type:"Fiat", model:"500", color:"white"};
car = {type:"Volvo", model:"EX60", color:"red"};    // ERROR

---

// You can create a constant array:
const cars = ["Saab", "Volvo", "BMW"];

// You can change an element:
cars[0] = "Toyota";

// You can add an element:
cars.push("Audi");

---

const cars = ["Saab", "Volvo", "BMW"];
cars = ["Toyota", "Volvo", "Audi"];    // ERROR
```

剩下的內容跟 let 相似 <https://www.w3schools.com/js/js_const.asp>

</details>

---

<details>
<summary><h3 style='display:inline'>JS Arrow Function</h3></summary>

350. 箭頭功能在ES6中引入。箭頭函數使我們可以編寫較短的函數語法。

```JavaScript
hello = function() {
  return "Hello World!";
}

---

hello = () => {
  return "Hello World!";
}

---

hello = () => "Hello World!";
```

351. 帶參數的箭頭，如果只有一個參數，則也可以跳過括號。

`hello = (val) => "Hello " + val;`

`hello = val => "Hello " + val;`

352. 箭頭功能沒有的綁定 this。使用箭頭功能時，this 始終代表定義了箭頭功能的物件(代表該函數的 所有者)。
353. 結果表明，第一個示例返回兩個不同的對象（窗口和按鈕），第二個示例返回兩次窗口對象，因為窗口對像是函數的"所有者"。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_arrow_function6>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_arrow_function7>

</details>

---

<details>
<summary><h3 style='display:inline'>JS Classes</h3></summary>

354. ES6 引入了類。類是函數的一種類型，而屬性是在 constructor() 方法內部分配的。每次初始化類對象時，都會調用構造函數方法。

```JavaScript
class Car {
  constructor(brand) {
    this.carname = brand;
  }
  present(x) {
    return x + ", I have a " + this.carname;
  }
}

mycar = new Car("Ford");
document.getElementById("demo").innerHTML = mycar.present("Hello");
```

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_classes_method2>

355. 靜態方法 是在類本身而非原型 prototype 上定義的。這意味著您不能在物件（mycar）上調用靜態方法，而在類（Car）上調用。

```JavaScript
class Car {
  constructor(brand) {
    this.carname = brand;
  }
  static hello(x) {
    return "Hello " + x.carname;
  }
}

mycar = new Car("Ford");

//Call 'hello()' on the class Car:
document.getElementById("demo").innerHTML = Car.hello(mycar);

//and NOT on the 'mycar' object:
//document.getElementById("demo").innerHTML = mycar.hello();
//this would raise an error.
```

356. Inheritance 繼承 extends 擴展。通過類繼承創建的類將從另一個類繼承所有方法。
357. super() 方法調用父方法的構造方法。並可以訪問父方法的屬性和方法。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_classes_inherit>

```JavaScript
class Car {
  constructor(brand) {
    this.carname = brand;
  }
  present() {
    return 'I have a ' + this.carname;
  }
}

class Model extends Car {
  constructor(brand, mod) {
    super(brand);
    this.model = mod;
  }
  show() {
    return this.present() + ', it is a ' + this.model;
  }
}

mycar = new Model("Ford", "Mustang");
document.getElementById("demo").innerHTML = mycar.show();
```

358. getter 和 setter 想在返回值或設置它們之前對值做一些特殊的事情。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_classes_getters>

359. getter / setter 方法的名稱不能與屬性的名稱相同，許多程序員在屬性名稱前使用下劃線字符 _ 與實際屬性分開。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_classes_getters2>

360. 要使用 setter，請使用與設置屬性值時相同的語法，不帶括號。

```JavaScript
class Car {
  constructor(brand) {
    this._carname = brand;
  }
  get carname() {
    return this._carname;
  }
  set carname(x) {
    this._carname = x;
  }
}

mycar = new Car("Ford");
mycar.carname = "Volvo";
document.getElementById("demo").innerHTML = mycar.carname;
```

361. 與函數和其他 JavaScript 聲明不同，類聲明不會被提升。對於其他聲明，例如函數，在聲明之前嘗試使用它時不會出錯，因為 JavaScript 聲明的默認行為正在提升（將聲明移到頂部）。
362. 類中的語法必須以"嚴格模式"編寫。如果您不遵循"嚴格模式"規則，則會收到錯誤消息。

</details>

---

<details>
<summary><h3 style='display:inline'>JS Debugging</h3></summary>

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_console>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_debugger>

363. 主流瀏覽器通常都有調試工具。

</details>

---

<details>
<summary><h3 style='display:inline'>JS Style Guide and Coding Conventions</h3></summary>

* <https://www.w3schools.com/js/js_conventions.asp>

364. 變量和函數的命名和聲明規則。空格，縮進和註釋的使用規則。編程實踐和原則。提高代碼可讀性，使代碼維護更加容易。

* camelCase
* 所有名稱字母開頭
* 在運算符（= +-* /）前後以及逗號後加空格
* 使用2個空格來縮進代碼塊
* 始終以分號結束簡單的語句
* 將開口支架放在第一行的末尾
* 在開口支架前留一個空間
* 將右括號放在新的一行上，不要有空格
* 不要以分號結束複雜的語句
* 將左括號放在與對象名稱相同的行上
* 在每個屬性及其值之間使用冒號加一個空格
* 請在字符串值（而不是數字值）兩邊使用引號
* 不要在最後一個屬性值對之後添加逗號
* 將右括號放在新行上，沒有前導空格
* 始終用分號結束對象定義
* 可以將短對象壓縮壓縮成一行，僅在屬性之間使用空格
* 避免使用超過80個字符的行
* 不適合一行，則打破它的最佳位置是在運算符或逗號之後
* 用大寫形式編寫的全局變量
* 用大寫形式編寫的常量
* 連字符可能被誤認為是減法嘗試。JavaScript名稱中不允許使用連字符
* 許多程序員更喜歡使用下劃線（date_of_birth），尤其是在SQL數據庫中，下劃線通常在PHP文檔中使用
* PascalCase 通常是 C 程序員首選的
* camelCase 由 JavaScript 本身，jQuery 和其他 JavaScript 庫使用
* 請勿以 $ 開頭名稱。這會使您與許多 JavaScript 庫名稱發生衝突

---

* 大多數Web服務器（Apache，Unix）對文件名區分大小寫
* 其他Web服務器（Microsoft，IIS）不區分大小寫
* 為避免這些問題，請始終使用小寫文件名（如果可能）。

</details>

---

<details>
<summary><h3 style='display:inline'>JS est Practices</h3></summary>

365. 避免全局變量，避免new，避免==，避免eval()
366. 盡量減少使用全局變量。這包括所有數據類型，對象和功能。全局變量和函數可以被其他腳本覆蓋。請改用局部變量，並學習如何使用 閉包。
367. 將所有聲明放在每個腳本或函數的頂部是一種很好的編碼做法。提供更乾淨的代碼、提供一個地方來查找局部變量、使避免不必要的（隱含的）全局變量更加容易、減少不必要的重新聲明的可能性。
368. 聲明變量時初始化變量是一種好的編碼習慣。提供更乾淨的代碼、提供一個位置來初始化變量、避免未定義的值。初始化變量提供了預期用途（和預期數據類型）的概念
369. 始終將數字，字符串或布爾值視為原始值。不作為對象。將這些類型聲明為對象，會降低執行速度並產生討厭的副作用
370. 不要使用new Object（）

* 使用{}代替new Object()
* 使用""代替new String()
* 使用0代替new Number()
* 使用false代替new Boolean()
* 使用[]代替new Array()
* 使用/()/代替new RegExp()
* 使用function (){}代替new Function()

371. 當心自動類型轉換，JavaScript 是鬆散類型的。變量可以包含不同的數據類型，並且變量可以更改其數據類型
372. 使用 === 比較
373. 如果使用缺少的參數調用函數，則將缺少的參數的值設置為 undefined。
374. ECMAScript 2015 允許在函數調用中使用默認參數 `function (a=1, b=1) { /*function code*/ }```JavaScript
375. 始終 switch 以 default。結尾。即使您認為沒有必要
376. eval()函數用於將文本作為代碼運行。在幾乎所有情況下，都沒有必要使用它。因為它允許運行任意代碼，所以它也表示一個安全問題。

</details>

---

<details>
<summary><h3 style='display:inline'>JS Common Mistakes</h3></summary>

* <https://www.w3schools.com/js/js_mistakes.asp>
377. JSON不允許尾隨逗號。

</details>

---

<details>
<summary><h3 style='display:inline'>JS Performance</h3></summary>

* <https://www.w3schools.com/js/js_performance.asp>
378. 循環中的每個語句（包括for語句）都針對循環的每次迭代執行。可以放在循環外部的語句或賦值將使循環運行更快。

```JavaScript
// BAD
var i;
for (i = 0; i < arr.length; i++) {
// BATTER
var i;
var l = arr.length;
for (i = 0; i < l; i++) {
```

379. 多次訪問DOM元素，請訪問一次，並將其用作局部變量

```JavaScript
var obj;
obj = document.getElementById("demo");
obj.innerHTML = "Hello";
```

380. 保持HTML DOM中的元素數量少。這將始終改善頁面加載，並加快渲染（頁面顯示），尤其是在較小的設備上。
381. 如果您不打算保存值，請不要創建新變量。

```JavaScript
// BAD
var fullName = firstName + " " + lastName;
document.getElementById("demo").innerHTML = fullName;
// BATTER
document.getElementById("demo").innerHTML = firstName + " " + lastName;
```

382. 將腳本放在頁面正文的底部，使瀏覽器可以首先加載頁面。在下載腳本時，瀏覽器將不會開始其他任何下載。此外，所有解析和渲染活動都可能被阻止。
383. 另一種方法是defer="true"在script標籤中使用。defer屬性指定腳本應在頁面解析完成後執行，但僅適用於外部腳本。
384. 如果可能，您可以在頁面加載後按代碼將腳本添加到頁面

```JavaScript
<script>
window.onload = function() {
  var element = document.createElement("script");
  element.src = "myScript.js";
  document.body.appendChild(element);
};
</script>
```

385. 避免使用with關鍵字。它對速度有負面影響。它還會弄亂JavaScript範圍。

</details>

---

<details>
<summary><h3 style='display:inline'>JS Reserved Words</h3></summary>

* <https://www.w3schools.com/js/js_reserved.asp>

</details>

---

<details>
<summary><h3 style='display:inline'>JS Versions</h3></summary>

* <https://www.w3schools.com/js/js_versions.asp>
386. ECMAScript 5 (2009) ES5
387. ECMAScript 6 (2015) ES6
388. 2018 Chrome 與 Opera 已支援到 (ES7 ECMAScript 2016)

</details>

---

<details>
<summary><h3 style='display:inline'>JS ECMAScript 5 - JavaScript 5</h3></summary>

* <https://www.w3schools.com/js/js_es5.asp>
387. 2009 ES5 功能

* "use strict"
* String.trim()
* Array.isArray()
* Array.forEach()
* Array.map()
* Array.filter()
* Array.reduce()
* Array.reduceRight()
* Array.every()
* Array.some()
* Array.indexOf()
* Array.lastIndexOf()
* JSON.parse()
* JSON.stringify()
* Date.now()
* 屬性獲取器和設置器 Property Getters and Setter
* 新對象屬性方法 New Object Property Methods

388. ES5 語法更改

* 字符串的屬性訪問[] Property access [ ] on strings
* 數組和對象文字中的尾隨逗號 Trailing commas in array and object literals
* 多行字符串文字 Multiline string literals
* 保留字作為屬性名稱 Reserved words as property names

</details>

---

<details>
<summary><h3 style='display:inline'>JS ECMAScript 6 - ECMAScript 2015</h3></summary>

* <https://www.w3schools.com/js/js_es6.asp>
389. 2015 ES6 功能

* let
* const
* 箭頭函數 Arrow Functions
* 類 Classes
* 默認參數值 Default parameter values
* Array.find()
* Array.findIndex()
* Exponentiation 指數（**）（EcmaScript 2016）

</details>

---

<details>
<summary><h3 style='display:inline'>JS JSON</h3></summary>

* <https://www.w3schools.com/js/js_json.asp>
390. JavaScript Object Notation

</details>

---

<details>
<summary><h3 style='display:inline'>JS Forms</h3></summary>

391. HTML表單驗證可以通過JavaScript完成。如果表單字段（fname）為空，則此函數將警告消息並返回false，以防止提交表單

```JavaScript
function validateForm() {
  var x = document.forms["myForm"]["fname"].value;
  if (x == "") {
    alert("Name must be filled out");
    return false;
  }
}

<form name="myForm" action="/action_page.php" onsubmit="return validateForm()" method="post">
Name: <input type="text" name="fname">
<input type="submit" value="Submit">
</form>
```

392. HTML表單驗證可以由瀏覽器自動執行：如果表單字段（fname）為空，則該 required 屬性將阻止提交此表單
393. 典型的驗證任務是：用戶是否填寫了所有必填字段？用戶輸入了有效日期嗎？用戶是否在數字字段中輸入了文字？
394. HTML5引入了新的HTML驗證概念，稱為約束驗證。約束驗證 HTML 輸入屬性。約束驗證 CSS 偽選擇器。約束驗證 DOM 屬性和方法

約束驗證 HTML 輸入屬性

| Attribute | Description                                        |
| --------- | -------------------------------------------------- |
| disabled  | 指定應禁用輸入元素                                 |
| max       | Specifies the maximum value of an input element    |
| min       | Specifies the minimum value of an input element    |
| pattern   | 指定輸入元素的值模式                               |
| required  | Specifies that the input field requires an element |
| type      | 指定輸入元素的類型                                 |

約束驗證 CSS 偽選擇器

| Selector  | Description                                                    |
| --------- | -------------------------------------------------------------- |
| :disabled | 選擇指定了"禁用"屬性的輸入元素                                 |
| :invalid  | 選擇具有無效值的輸入元素                                       |
| :optional | 選擇未指定"必需"屬性的輸入元素                                 |
| :required | Selects input elements with the "required" attribute specified |
| :valid    | 選擇具有有效值的輸入元素                                       |

</details>

---

<details>
<summary><h3 style='display:inline'>JS Validation API</h3></summary>

* <https://www.w3schools.com/js/js_validation_api.asp>

395. 約束驗證 DOM 方法

| Property            | Description                            |
| ------------------- | -------------------------------------- |
| checkValidity()     | 如果輸入元素包含有效數據，則返回true。 |
| setCustomValidity() | 設置輸入元素的validationMessage屬性。  |

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_validation_check>

396. 約束驗證 DOM 屬性

| Property          | Description                             |
| ----------------- | --------------------------------------- |
| validity          | 包含與輸入元素的有效性有關的布爾屬性。  |
| validationMessage | 包含有效性為false時瀏覽器將顯示的消息。 |
| willValidate      | 指示是否將驗證輸入元素。                |

397. 有效性屬性

| Property        | Description                                      |
| --------------- | ------------------------------------------------ |
| customError     | 如果設置了自定義有效性消息，則設置為true。       |
| patternMismatch | 如果元素的值與其模式屬性不匹配，則設置為true。   |
| rangeOverflow   | 如果元素的值大於其max屬性，則設置為true。        |
| rangeUnderflow  | 如果元素的值小於其min屬性，則為true。            |
| stepMismatch    | 如果元素的值對於其step屬性無效，則設置為true。   |
| tooLong         | 如果元素的值超過其maxLength屬性，則設置為true。  |
| typeMismatch    | 如果每個元素的type屬性的值無效，則設置為true。   |
| valueMissing    | 如果元素（具有必需的屬性）沒有值，則設置為true。 |
| valid           | 如果元素的值有效，則設置為true。                 |

> rangeOverflow

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_validation_rangeOverflow>

```JavaScript
<input id="id1" type="number" max="100">
<button onclick="myFunction()">OK</button>

<p id="demo"></p>

<script>
function myFunction() {
  var txt = "";
  if (document.getElementById("id1").validity.rangeOverflow) {
    txt = "Value too large";
  }
  document.getElementById("demo").innerHTML = txt;
}
</script>
```

> rangeUnderflow

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_validation_rangeUnderflow>

```JavaScript
<input id="id1" type="number" min="100">
<button onclick="myFunction()">OK</button>

<p id="demo"></p>

<script>
function myFunction() {
  var txt = "";
  if (document.getElementById("id1").validity.rangeUnderflow) {
    txt = "Value too small";
  }
  document.getElementById("demo").innerHTML = txt;
}
</script>
```

</details>

---

<details>
<summary><h3 style='display:inline'>JS Objects deeply</h3></summary>

398. 在JavaScript中，幾乎"一切"都是對象。

* 布爾值可以是對象（如果使用new關鍵字定義）
* 數字可以是對象（如果使用new關鍵字定義）
* 字符串可以是對象（如果使用new關鍵字定義）
* 日期始終是對象
* 數學永遠是對象
* 正則表達式始終是對象
* 數組始終是對象
* 功能始終是對象
* 對象永遠是對象
* 除原語 primitives 外，所有 JavaScript 值都是對象。

399. 原始值是不具有屬性或方法的值。原始數據類型為具有原始值的數據。JavaScript 定義了 5 種原始數據類型：

* string
* number
* boolean
* null
* undefined

> 基本值是不可變的（它們是硬編碼的，因此不能更改）。

* <https://www.w3schools.com/js/js_object_definition.asp>

400. 無需使用 new Object()。為了簡化，可讀性和執行速度，請使用第一個（對象文字方法）。

---

### Object Properties

* <https://www.w3schools.com/js/js_object_properties.asp>

401. for...in 循環內部的代碼塊將針對每個屬性執行一次。
402. 以通過簡單地給它賦一個值來向現有對象添加新屬性。`person.nationality = "English";`
403. delete 關鍵字刪除屬性的兩個值和屬性本身。刪除後，該屬性將無法使用，然後再重新添加。delete 不得在預定義的 JavaScript 對象屬性上使用該運算符。它可能會使您的應用程序崩潰。
404. JavaScript 中，可以讀取所有屬性，但是只能更改 value 屬性（並且僅在該屬性為可寫狀態時）。ECMAScript 5 具有獲取和設置所有屬性屬性的方法
405. JavaScript對象繼承其原型的屬性。

---

### Object Methods

406. 向對象添加方法

```JavaScript
person.name = function () {
  return this.firstName + " " + this.lastName;
};
```

---

### Display Objects

* 按名稱顯示對象屬性
* 循環顯示對象屬性
* 使用Object.values（）顯示對象
* 使用JSON.stringify（）顯示對象
* console.log() f12 dev loop display

406. 您必須在循環中使用person [x]。person.x將不起作用（因為x是變量）。
407. JSON.stringify 不會對函數進行字符串化
408. 如果在字符串化之前將函數轉換為字符串，則可以"修復"。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_object_display_stringify_function_tostring>

---

### Object Accessors

409. ECMAScript 5（2009）引入了 Getters and Setters

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_object_accessors_get>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_object_accessors_set>

410. fullName作為函數訪問：person.fullName（）。 fullName作為屬性訪問：person.fullName。第二個 提供了更簡單的語法。
411. 使用 getter 和 setter 時，JavaScript可以確保更好的數據質量。
412. 為什麼要使用Getter和Setter？它提供了更簡單的語法、它允許屬性和方法的語法相同、它可以確保更好的數據質量、對於幕後處理很有用
413. Object.defineProperty()方法還可以用於添加Getter和Setter

```JavaScript
// Define object
var obj = {counter : 0};

// Define setters
Object.defineProperty(obj, "reset", {
  get : function () {this.counter = 0;}
});
Object.defineProperty(obj, "increment", {
  get : function () {this.counter++;}
});
Object.defineProperty(obj, "decrement", {
  get : function () {this.counter--;}
});
Object.defineProperty(obj, "add", {
  set : function (value) {this.counter += value;}
});
Object.defineProperty(obj, "subtract", {
  set : function (value) {this.counter -= value;}
});

// Play with the counter:
obj.reset;
obj.add = 5;
obj.subtract = 1;
obj.increment;
obj.decrement;
```

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_object_accessors_set4>

---

### Object Constructors

414. 用大寫首字母命名構造函數是一種好的做法。
415. 有時我們需要一個"藍圖"來創建許多相同"類型"的對象。通過使用new關鍵字調用構造函數來創建相同類型的對象

```JavaScript
var myFather = new Person("John", "Doe", 50, "blue");
var myMother = new Person("Sally", "Rally", 48, "green");
```

416. this不是變量。它是一個關鍵字。您無法更改的值this。
417. 不能像向現有物件中添加新屬性一樣，向物件構造函數中添加新屬性。不能像向現有對像中添加新方法一樣，向對象構造函數中添加新方法。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_object_constructor4>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_object_constructor5>

> * <https://www.w3schools.com/js/js_object_constructors.asp>

</details>

---

<details>
<summary><h3 style='display:inline'>JS Object Prototypes</h3></summary>

418. 對象構造函數，不能將新屬性添加到現有的對象構造函數中，要將新屬性添加到構造函數，必須將其添加到構造函數
419. 所有 JavaScript 物件都從原型繼承屬性和方法。

* Object.prototype 是對原型繼承鏈的頂端。
* Date 對象繼承自 Date.prototype
* Array 對象繼承自 Array.prototype
* Person 對象繼承自 Person.prototype

420. prototype 屬性允許您向對象構造函數添加新屬性

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_object_prototype5>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_object_prototype6>

```JavaScript
function Person(first, last, age, eyecolor) {
  this.firstName = first;
  this.lastName = last;
  this.age = age;
  this.eyeColor = eyecolor;
}

Person.prototype.nationality = "English";

---

function Person(first, last, age, eyecolor) {
  this.firstName = first;
  this.lastName = last;
  this.age = age;
  this.eyeColor = eyecolor;
}

Person.prototype.name = function() {
  return this.firstName + " " + this.lastName;
};
```

421. 僅修改您自己的原型。切勿修改標準 JavaScript 對象的原型。

</details>

---

<details>
<summary><h3 style='display:inline'>JS ES5 Object Methods</h3></summary>

* <https://www.w3schools.com/js/js_object_es5.asp>

422. ES5 增加了許多 Obj 方法可以直接針對 Prototypes 原形、Property 屬性做操作。

</details>

---

<details>
<summary><h3 style='display:inline'>JS Function Definitions</h3></summary>

* <https://www.w3schools.com/js/js_function_definition.asp>

423. 分號用於分隔可執行的JavaScript語句。由於函數聲明不是可執行語句，因此以分號結尾並不常見。
424. 匿名函數（一個沒有名稱的函數）存儲在變量中的函數不需要函數名。始終使用變量名來調用（調用）它們。以分號結尾，因為它是可執行語句的一部分。

```JavaScript
var x = function (a, b) {return a * b};
var z = x(4, 3);
```

425. JavaScript函數是使用function關鍵字定義的。也可以使用稱為的內置JavaScript 函數構造函數來定義函數 Function()。實際上，您不必使用函數構造函數。大多數時候，您可以避免new在JavaScript中使用關鍵字。

`var myFunction = new Function("a", "b", "return a * b");`

426. 提升適用於變量聲明和函數聲明。因此，可以在聲明JavaScript函數之前對其進行調用。使用表達式定義的函數不會被提升。

```JavaScript
myFunction(5);

function myFunction(y) {
  return y * y;
}
```

427. 立即函式 (匿名的自調用函數)

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_function_expression_self>

```JavaScript
(function () {
  var x = "Hello!!";  // I will invoke myself
})();
```

428. typeof JavaScript 中的運算符為函數返回"function"。但是，JavaScript 函數最好被描述為對象。JavaScript 函數同時具有 屬性 和 方法。
429. 定義為物件屬性的函數稱為物件的方法。設計用來創建新物件的函數稱為物件構造函數。
430. 箭頭函數 允許使用簡短的語法編寫函數表達式。不需要 function 關鍵字，return 關鍵字和 大括號。

```JavaScript
// ES5
var x = function(x, y) {
  return x * y;
}

// ES6
const x = (x, y) => x * y;
```

431. 箭頭函數沒有自己的this。它們不適用於定義物件方法。
432. 箭頭功能未提升。必須先定義它們，然後再使用它們。
433. 使用const 比使用 var 更加安全，因為函數表達式始終為常數。
434. return 如果函數是單個語句，則只能省略關鍵字和大括號。因此，始終保留它們可能是一個好習慣 `const x = (x, y) => { return x * y };`

---

### Function Parameters

435. 函數參數是函數定義中列出的名稱。函數參數是 傳遞給函數（並由函數接收）的實際值
436. JavaScript函數定義未指定參數的數據類型。
437. JavaScript函數不對傳遞的參數執行類型檢查。
438. JavaScript函數不檢查接收到的參數數量。
439. 如果使用缺少的參數（小於聲明的值）調用函數，則將缺少的值設置為：undefined 有時這是可以接受的，但有時最好將默認值分配給參數
440. ECMAScript 2015允許在函數聲明中使用默認參數值 `function (a=1, b=1) `
441. JavaScript函數具有一個稱為arguments對象的內置對象。這樣，您可以簡單地使用一個函數來查找（例如）數字列表中的最大值

```JavaScript
x = findMax(1, 123, 500, 115, 44, 88);

function findMax() {
  var i;
  var max = -Infinity;
  for (i = 0; i < arguments.length; i++) {
    if (arguments[i] > max) {
      max = arguments[i];
    }
  }
  return max;
}
```

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_function_arguments>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_function_arguments_sum>

442. JavaScript參數由值傳遞：該函數僅知道值，而不是參數的位置。如果函數更改了參數的值，則不會更改參數的原始值。對參數的更改在函數外部不可見（反映）。
443. In JavaScript, object references are values.如果函數更改了物件屬性，它將更改原始值。對物件屬性的更改在函數外部可見（反映）。

---

### Function Invocation

* <https://www.w3schools.com/js/js_function_invocation.asp>

443. 定義函數時，不會執行函數內部的代碼。
444. 函數內部的代碼在調用函數時執行。
445. myFunction(); 或 window.myFunction(); 是調用JavaScript函數的常用方法，但不是很好的做法。全局變量，方法或函數可以輕鬆在全局對像中創建名稱衝突和錯誤。
446. 調用函數作為物件方法會使值this 變成物件本身。
447. 如果函數調用前面帶有new關鍵字，則它是構造函數調用。構造函數調用將創建一個新對象。新對像從其構造函數繼承屬性和方法。

---

### Function Call

* <https://www.w3schools.com/js/js_function_call.asp>

448. call() 方法，您可以編寫可在不同物件上況衝方法。

```JavaScript
var person = {
  fullName: function() {
    return this.firstName + " " + this.lastName;
  }
}
var person1 = {
  firstName:"John",
  lastName: "Doe"
}
var person2 = {
  firstName:"Mary",
  lastName: "Doe"
}
person.fullName.call(person2);  // Will return "Mary Doe"
```

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_function_call_call2>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_function_call_arguments>

449. [在JavaScript中，所有函數都是物件方法。如果函數不是JavaScript對象的方法，則它是全局物件的函數](#)。

---

### Function Apply

* <https://www.w3schools.com/js/js_function_apply.asp>

450. apply() 方法，您可以編寫可在不同對像上使用的方法。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_function_apply>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_function_apply_arguments>

```JavaScript
var person = {
  fullName: function() {
    return this.firstName + " " + this.lastName;
  }
}
var person1 = {
  firstName: "Mary",
  lastName: "Doe"
}
person.fullName.apply(person1);  // Will return "Mary Doe"
```

451. call（）和apply（）之間的區別

* call() 方法採用參數列表。
* apply() 方法將參數作為數組。

```JavaScript
person.fullName.apply(person1, ["Oslo", "Norway"]);

person.fullName.call(person1, "Oslo", "Norway");
```

452. 由於JavaScript 數組沒有max（）方法，因此您可以應用該 Math.max()方法。

```JavaScript
Math.max(1,2,3);  // Will return 3

---

Math.max.apply(null, [1,2,3]); // Will also return 3
Math.max.apply(Math, [1,2,3]); // Will also return 3
Math.max.apply(" ", [1,2,3]); // Will also return 3
Math.max.apply(0, [1,2,3]); // Will also return 3
```

</details>

---

<details>
<summary><h3 style='display:inline'>JS Closures</h3></summary>

* <https://www.w3schools.com/js/js_function_closures.asp>

453. 可以使用 閉包 將全局變量設為局部（私有）。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_function_closures1>

```JavaScript
// Initiate counter
var counter = 0;

// Function to increment counter
function add() {
  counter += 1;
}

// Call add() 3 times
add();
add();
add();

// The counter should now be 3
```

上面的解決方案存在一個問題：頁面上的任何代碼都可以更改計數器，而無需調用 add（）。

計數器應在add()函數本地，以防止其他代碼對其進行更改

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_function_closures2>

```JavaScript
// Initiate counter
var counter = 0;

// Function to increment counter
function add() {
  var counter = 0;
  counter += 1;
}

// Call add() 3 times
add();
add();
add();

//The counter should now be 3. But it is 0
```

它不起作用，因為我們顯示全局計數器而不是本地計數器。

我們可以通過讓函數返回它來刪除全局計數器並訪問本地計數器：

```JavaScript
// Function to increment counter
function add() {
  var counter = 0;
  counter += 1;
  return counter;
}

// Call add() 3 times
add(); // 完成調用後 js 會自動清除垃圾記憶體造成局部變數 counter 無法紀錄
add();
add();

//The counter should now be 3. But it is 1.
```

它沒有用，因為每次調用函數時都會重置本地計數器。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_function_closures3>

454. JavaScript 內部函數。嵌套函數

此種方法將計數功能移入內部，在 add() 內功能還沒結束前記憶體都會存在

但此種方法出現了問題

1. 原本設計是要呼叫 add() 一次就計數器+1
2. 假如依照第一種設計方式思考則 var counter = 0; 只能執行一次

```JavaScript
function add() {
  var counter = 0;
  function plus() {counter += 1;}
  // plus();
  // plus();
  plus();
  return counter;
}
// outside
add();
```

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_function_closures4>

455. 還記得自調用功能嗎？此功能有什麼作用？

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_function_closures5>

```JavaScript
var add = (function () {
  var counter = 0;
  return function () {counter += 1; return counter}
})();

add();
add();
add();

// the counter is now 3
```

為變量 add 分配了 自調用函數(會在讀到時自動執行一次) 的 返回值(function () {counter += 1; return counter})。

* <https://www.w3schools.com/js/js_function_definition.asp>

自調用功能僅運行一次。它將計數器設置為零（0），並返回函數表達式。

這樣添加就成為一個功能。"很棒"的部分是

1. 它可以訪問父作用域中的計數器。

這稱為JavaScript 閉包。它使函數具有" 私有 "變量成為可能。

2. 計數器受匿名函數作用域的保護，只能使用add函數進行更改。

閉包是即使父函數已關閉，也可以訪問父作用域的函數。

</details>

---

<details>
<summary><h3 style='display:inline'>JS HTML DOM</h3></summary>

## [Document Object Model](https://www.w3schools.com/js/js_htmldom.asp)

這一大章節基本上就是介紹如何利用 JS 操作 文件物件模型

### [DOM Methods](https://www.w3schools.com/js/js_htmldom_methods.asp)

### [DOM Document](https://www.w3schools.com/js/js_htmldom_document.asp)

### [DOM Elements](https://www.w3schools.com/js/js_htmldom_elements.asp)

### [DOM Changing HTML](https://www.w3schools.com/js/js_htmldom_html.asp)

### [DOM Changing CSS](https://www.w3schools.com/js/js_htmldom_css.asp)

### [DOM Animation](https://www.w3schools.com/js/js_htmldom_animate.asp)

### [DOM Events](https://www.w3schools.com/js/js_htmldom_events.asp)

### [DOM EventListener](https://www.w3schools.com/js/js_htmldom_eventlistener.asp)

### [DOM Navigation](https://www.w3schools.com/js/js_htmldom_navigation.asp)

### [DOM Nodes](https://www.w3schools.com/js/js_htmldom_nodes.asp)

### [DOM Collections](https://www.w3schools.com/js/js_htmldom_collections.asp)

### [DOM Node Lists](https://www.w3schools.com/js/js_htmldom_nodelist.asp)

</details>

---

<details>
<summary><h3 style='display:inline'>JS Browser BOM</h3></summary>

## [Browser Object Model](https://www.w3schools.com/js/js_window.asp)

允許JavaScript與瀏覽器"對話"。

```JavaScript
window.document.getElementById("header");
===
document.getElementById("header");
```

### [Window Screen](https://www.w3schools.com/js/js_window_screen.asp)

### [Window Location](https://www.w3schools.com/js/js_window_location.asp)

### [Window History](https://www.w3schools.com/js/js_window_history.asp)

### [Window Navigator](https://www.w3schools.com/js/js_window_navigator.asp)

### [Popup Boxes](https://www.w3schools.com/js/js_popup.asp)

456. 要在彈出框中顯示換行符，請使用反斜杠後跟字符n。`alert("Hello\nHow are you?");`

### [Timing Events](https://www.w3schools.com/js/js_timing.asp)

457. window.setTimeout(function, milliseconds); 等待指定的毫秒數後執行功能。
458. window.clearTimeout(timeoutVariable); 停止執行setTimeout（）中指定的函數。
459. window.setInterval(function, milliseconds); 在每個給定的時間間隔重複給定的功能。
460. window.clearInterval(timerVariable); 停止setInterval（）方法中指定的函數的執行。

### [Cookies](https://www.w3schools.com/js/js_cookies.asp)

461. Cookies 是存儲在計算機上的小型文本文件中的數據。Web服務器將網頁發送到瀏覽器後，連接將關閉，並且服務器會忘記有關用戶的所有信息。
462. 如果您的瀏覽器關閉了本地 Cookie 支持，則下面的示例均無效。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_cookie_username>

</details>

---

<details>
<summary><h3 style='display:inline'>JS AJAX</h3></summary>

### [Asynchronous JavaScript And XML](https://www.w3schools.com/js/js_ajax_intro.asp)

463. 由兩個部分組成 1. XMLHttpRequest obj。2. JS & HTML Data (JSON/Text/XML)

```JavaScript
function loadDoc() {
  var xhttp = new XMLHttpRequest();
  xhttp.onreadystatechange = function() {
    if (this.readyState == 4 && this.status == 200) {
     document.getElementById("demo").innerHTML = this.responseText;
    }
  };
  xhttp.open("GET", "ajax_info.txt", true);
  xhttp.send();
}
```

---

### [The XMLHttpRequest Object](https://www.w3schools.com/js/js_ajax_http.asp)

464. 出於安全原因，現代瀏覽器不允許跨域訪問。這意味著該網頁及其嘗試加載的XML文件必須位於同一服務器上。

XMLHttpRequest對象方法

| Method                              | Description                         |
| ----------------------------------- | ----------------------------------- |
| new XMLHttpRequest()                | Creates a new XMLHttpRequest object |
| abort()                             | 取消當前請求                        |
| getAllResponseHeaders()             | Returns header information          |
| getResponseHeader()                 | Returns 特定 header information     |
| open(method, url, async, user, psw) | 指定 the request                    |
|                                     | method：請求類型為GET或POST         |
|                                     | url：文件位置                       |
|                                     | async ：真（異步）或假（同步）      |
|                                     | user: 可選用戶名                    |
|                                     | psw  : 可選密碼                     |
| send()                              | 將請求發送到服務器 用於GET請求      |
| send(string)                        | 將請求發送到服務器。 用於POST請求   |
| setRequestHeader()                  | 將標籤/值對添加到要發送的標頭中     |

XMLHttpRequest對象屬性

| Method             | Description                                                                                               |
| ------------------ | --------------------------------------------------------------------------------------------------------- |
| onreadystatechange | 定義當 readyState 屬性更改時要調用的函數                                                                  |
| readyState         | 保留 XMLHttpRequest 的狀態。                                                                              |
|                    | 0: 請求未初始化                                                                                           |
|                    | 1：服務器連接建立                                                                                         |
|                    | 2：收到請求                                                                                               |
|                    | 3：處理要求                                                                                               |
|                    | 4：請求已完成且響應已準備就緒                                                                             |
| responseText       | 以字符串形式返迴響應數據                                                                                  |
| responseXML        | 以XML數據形式返迴響應數據                                                                                 |
| status             | 返回請求的狀態號                                                                                          |
|                    | 200: "OK"                                                                                                 |
|                    | 403："禁止"                                                                                               |
|                    | 404: "Not Found"                                                                                          |
|                    | For a complete list go to the Http Messages Reference https://www.w3schools.com/tags/ref_httpmessages.asp |
| statusText         | 返回狀態文本（例如"確定"或"未找到"）                                                                      |

### [Send a Request To a Server](https://www.w3schools.com/js/js_ajax_http_send.asp)

465. 不建議使用同步XMLHttpRequest（async = false），因為JavaScript將在服務器響應就緒之前停止執行。如果服務器忙或慢，則應用程序將掛起或停止。同步XMLHttpRequest正在從Web標準中刪除，但是此過程可能需要很多年。

### [Server Response]([Server Response](https://www.w3schools.com/js/js_ajax_http_response.asp))

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_ajax_callback>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_ajax_responsexml>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_ajax_header>

### [XML Example](https://www.w3schools.com/js/js_ajax_xmlfile.asp)

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_ajax_xml2>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_ajax_xml2>
* <https://www.w3schools.com/js/cd_catalog.xml>

### [PHP Example](https://www.w3schools.com/js/js_ajax_php.asp)

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_ajax_suggest_php>

### [ASP Example](https://www.w3schools.com/js/js_ajax_asp.asp)

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_ajax_suggest_asp>

### [Database Example](https://www.w3schools.com/js/js_ajax_database.asp)

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_ajax_database>

### [XML Applications](https://www.w3schools.com/js/js_ajax_applications.asp)

* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_ajax_display_table>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_ajax_app_first>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjs_ajax_app_navigate>

### [AJAX Examples](https://www.w3schools.com/js/js_ajax_examples.asp)

</details>

---

<details>
<summary><h3 style='display:inline'>JS JSONP</h3></summary>

* <https://www.w3schools.com/js/js_json_jsonp.asp>

466. JSONP是一種發送JSON數據而無需擔心跨域問題的方法。JSONP不使用該XMLHttpRequest對象。JSONP使用`<script>`標記代替。
467. 由於跨域策略，從另一個域請求文件可能會導致問題。從另一個域請求外部腳本不會出現此問題。JSONP利用了這一優勢，並使用腳本標籤而不是XMLHttpRequest對象來請求文件。

* <https://www.w3schools.com/js/showphp.asp?filename=demo_jsonp>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjson_jsonp>

468. 腳本標記僅應在需要時創建，通過將JSON發送到php文件來使示例動態化，並讓php文件根據獲取的信息返回JSON對象。

* <https://www.w3schools.com/js/tryit.asp?filename=tryjson_jsonp_create>
* <https://www.w3schools.com/js/tryit.asp?filename=tryjson_jsonp_php>

469. 當您無法控制服務器文件時，如何使服務器文件調用正確的功能？有時服務器文件提供回調函數作為參數，php文件將調用您作為回調參數傳遞的函數

* <https://www.w3schools.com/js/tryit.asp?filename=tryjson_jsonp_callback>

---

* JSONP
* <https://www.google.com/search?q=JSONP&oq=JSONP&aqs=chrome..69i57j69i59j69i60l2&sourceid=chrome&ie=UTF-8>
* 跨來源資源共享
* <https://www.google.com/search?q=%E8%B7%A8%E4%BE%86%E6%BA%90%E8%B3%87%E6%BA%90%E5%85%B1%E4%BA%AB&oq=%E8%B7%A8%E4%BE%86%E6%BA%90%E8%B3%87%E6%BA%90%E5%85%B1%E4%BA%AB&aqs=chrome..69i57&sourceid=chrome&ie=UTF-8>

---

## Other

### [Introduction](https://www.w3schools.com/js/js_json_intro.asp)

### [Syntax](https://www.w3schools.com/js/js_json_syntax.asp)

### [JSON vs XML](https://www.w3schools.com/js/js_json_xml.asp)

### [Data Types](a)

### [JSON.parse()](https://www.w3schools.com/js/js_json_parse.asp)

### [JSON.stringify()](https://www.w3schools.com/js/js_json_stringify.asp)

### [Objects](https://www.w3schools.com/js/js_json_objects.asp)

### [Arrays](https://www.w3schools.com/js/js_json_arrays.asp)

### [PHP](https://www.w3schools.com/js/js_json_php.asp)

### [HTML](https://www.w3schools.com/js/js_json_html.asp)

</details>

</details> <!-- JS Basics 結束 -->

## [JS Exercises](https://www.w3schools.com/js/exercise_js.asp?filename=exercise_js_variables1)
