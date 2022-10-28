---
description: 在閱讀時接觸到共筆這個名詞發現，蠻認同這種方式的所以拿來應用在學習程式領域試試看。
---

# 克服JS奇怪的部分1-30\_共筆

{% embed url="https://nightwing975.github.io/simonallenblog/" %}

內容由此處提出，原作者為Simon Allen。

此共筆為學習使用，如有侵犯版權請告知感謝。



### D1 前言 :

F2E = Front End Engineering



johch3n611u :

有些內容之前上JS課程時並沒有細講，剛好可以補上。



### D2 幾個名詞小觀念 :

#### 語法解析器\(Syntax Parser\) 

直譯器



#### 詞彙環境\(Lexical Environment\) 

程式碼在程式中的實際所在位置 \( 如變數、函數、物件、陣列中 \)，

影響執行階段對應的記憶體位置，影響它和其他變數函式的互動。



#### 執行環境\(Execution Context\) 

johch3n611u :

讀取時會逐行逐字讀Code，並依照宣告的詞彙環境，以不同順序被解析轉換 \( 伏筆:JS特性:提升 \)，

創造與擺入記憶體，最後瀏覽器才逐行執行。\( 建立時會經歷創造階段與執行階段 \)

執行完全域執行環境\( 會位於堆疊的最底層 \)後執行階段若遇到funtion，

就會新增一個執行環境並在一次進行創造與執行並進行執行推疊\(Execution stack\)，

直到code出現return則結束執行環境，並從堆疊中被移除，

因為JS預設為同步\(synchronous\)模式，最上最新一層則為**現行環境 \(Active context\)。**

{% embed url="https://medium.com/@hyWang/%E9%9D%9E%E5%90%8C%E6%AD%A5-asynchronous-%E8%88%87%E5%90%8C%E6%AD%A5-synchronous-%E7%9A%84%E5%B7%AE%E7%95%B0-c7f99b9a298a" %}

\*\*\*\*

#### 名稱/值的配對\(Name/Value pair\)

一個名稱會對應到一個值或組合。\( 伏筆:undefined、null \)



#### 全域環境與全域物件\(Global execution context\)

執行時產生全域環境\(基礎執行環境ex: browser = window ， node.js = global ， .net = system ...\)與

全域物件 global object ****或許應該要稱為 執行環境物件 \(Execution context object\)。

#### 進階解析

{% embed url="https://medium.com/%E9%AD%94%E9%AC%BC%E8%97%8F%E5%9C%A8%E7%A8%8B%E5%BC%8F%E7%B4%B0%E7%AF%80%E8%A3%A1/%E6%B7%BA%E8%AB%87-javascript-%E5%9F%B7%E8%A1%8C%E7%92%B0%E5%A2%83-2976b3eaf248" %}

#### 補充

```text
前言
所有程式碼都必須在執行環境 (Execution context)中執行，
你可以把 執行環境 想像成一間又一間的工作室，
而每間工作室都會配有一個 
執行環境物件，
負責紀錄該環境中需要用到的各種資料，
這個傢伙比較複雜，我們下一章節才會聊到它。

執行階段 (Execution Phase) 顧名思義，就是一行一行地執行程式碼，
這沒什麼困難的。不過在那之前，還有一個相當重要的階段，
稱作創造階段！傳說中的 提升 (Hoisting) 就是在這個階段發生的。
那究竟創造階段(Creation Phase) 是在創造什麼呢？

每個執行環境都會配有一個執行環境物件 (Execution context object)，
負責紀錄該環境中需要用到的各種資料。
由於需要紀錄的資料相當得多，
所以 執行環境物件 就必須好好地為這些資料分類，
讓 執行環境 能更快速、更明確地取得它需要的資料。

因此，每個 執行環境物件 都有 3 個屬性 (Property)，分別是：
變數物件 (Variable Object)
作用域鏈 (Scope Chain)
“This” 變數 (“This” Variable)
```

####  **執行環境物件 \(Execution context object\)**

每個執行環境都會配有一個執行環境物件 \(Execution context object\)， 

負責紀錄該環境中需要用到的各種資料。

#### 每個 執行環境物件 都有 3 個屬性 \(Property\)

#### 變數物件 **\(Variable Object\)** 

#### **作用域鏈 \(Scope Chain\) 待補 原作者停更**

#### **“This” 變數 \(“This” Variable\) 似乎就是造成子類能夠使用 this 的原因所在**











### D3 執行環境:創造與提升



johch3n611u : 

編輯器/IDE上的程式碼順序並不等於電腦執行JS程式碼之順序，

而是將整個檔案的程式碼包裹於全域執行環境，

並由上而下進行創造階段、執行階段，並在過程中跳過函式內的程式碼，

直到函式被呼叫時新增一個區域執行環境並繼續創造與執行階段，

而提升就是來自於執行環境物件的屬性所造成，

在執行環境創造階段時，

參數值會被創造為物件存放於記憶體中、

變數會被定義值為undefined於記憶體中、

而函式會被完整定義內容於記憶體中，

並被提升至程式碼的上層後，

進行執行階段，此時才開始賦予值給變數與呼叫函式新增執行環境。



#### 

 





將這兩份文章合為一份則為上述我認為的結果。

#### 創造與提升 :

```text
A文章

全域執行環境 (Global execution context)，又稱為 默認執行環境。
執行環境 在建立時，會經歷兩個階段，分別是 ：
Creation Phase 創造階段
Execution Phase 執行階段
一旦 全域執行環境 結束 創造階段、進入 執行階段，
它就會開始由上到下、一行一行地執行程式，並自動跳過函式裡的程式碼，
這其實蠻合理的，畢竟你只是進行函式宣告，並沒有打算當下執行它。
如果你的專案非常的陽春，完全沒有任何的函式呼叫 (Function Call)，
那麼全域執行環境 是你唯一會遇到的 執行環境。
你可能會問，如果有函式呼叫呢？
一旦 全域執行環境 在執行程式碼的過程中，判讀到函式呼叫，
全域執行環境 就會馬上為它建立一個全新的執行環境，
專門供這個函式裏頭的程式碼運行，如果我們不斷地在一個函式中又呼叫另一個函式，
可想而知，就會建立很多個執行環境，而這些工作室會相互堆疊，變成一棟 工作大樓。
```

```text
B文章

創造(creation)：直譯器將我們程式所寫的變數與函式創造並設定到電腦的記憶體裡。
實際上語法解析器將我們的程式碼轉換給電腦時，
它會知道我們在程式的哪裡宣告變數與函式，
在創造(給電腦)階段會先將變數與函式的定義按順序設定在記憶體裡，然後才會執行我們其他的程式。

boy();
var boy = function() {
    var a = 'Yoo!';
	console.log(a);
}
執行時我們可以這樣想：

var boy先被提升到最上面(創造變數boy並設定進記憶體)
接著呼叫執行boy函式
給變數boy賦值(指向)函式

呼叫執行boy函式時報錯，是因為變數boy這個時候根本沒有被賦值，
何況是指向函式呼叫呢？所以自然報錯boy is not a function，可以想像成是這樣：

var boy
boy();
boy = function() {
    var a = 'Yoo!';
	console.log(a);
}
結果會是一樣的
　
當然這不代表我們寫在編輯器或IDE上的JS程式被竄寫成這樣，
而是電腦執行JS的順序變成這樣，這種乍看變數與函式被拉到作用域最前面的現象稱為Hoisting
```



#### 進階解析

{% embed url="https://medium.com/%E9%AD%94%E9%AC%BC%E8%97%8F%E5%9C%A8%E7%A8%8B%E5%BC%8F%E7%B4%B0%E7%AF%80%E8%A3%A1/%E4%BA%94%E5%88%86%E9%90%98%E8%BC%95%E9%AC%86%E6%9A%B8%E8%A7%A3-%E6%8F%90%E5%8D%87-hoisting-82e960964b3e" %}

#### **補充**

#### 1.Variable Object 變數物件 

1.1.建立參數物件Argument Object，存放所有我們打算送進函式的引數 \(Argument\)。 

{% embed url="https://thisworldmyworld.blogspot.com/2009/12/blog-post\_08.html" %}



1.2.掃描程式碼中是否有函式宣告 \(Function Declaration\)： 如果有，就為每一個函式建立一個新屬性，

其值指向該函式在記憶體中的位址。 



1.3.掃描程式碼中是否有變數宣告 \(Variable Declaration\)： 如果有，就為每一個變數建立一個新屬性，

並將該屬性的值初始為 undefined。 



#### 最後兩項在程式界中，有一個響叮噹的名號：

#### Hoisting（提升）。

變數與函式，在進入執行階段前，其實就已經完成宣告。

這種「將變數宣告與函式宣告的動作，提升到程式碼最頂端」的行為，就是 Hoisting（提升）。

#### 

johch3n611u : 

#### 結果這位文章作者居然斷更新的內容 ...







### D4 undefined與not defined

es5 var a = 100; es6語法為let a = 100;



變數在建立執行環境時被賦予值為 undefined，同時也是型別。 \( like null \)

而未宣告建立定義的東西 console 才會是 not defined



### D5 變數與函式環境、外部參照

johch3n611u : 

一樣在思考前幾張節關於程式碼底層運作觸發的先後順序。

並介紹了  es6 的 let 宣告，

沒宣告就會報錯，而不是外部查找。

區分了，全域、區域、外部環境的 差別 \( 全域不等於外部 \)。

似乎就是所謂的 **作用域鏈 \(Scope Chain\)** 





let 具有區塊範圍\(block scoping\)特性，

用 let 宣告的相同變數名稱，在全域、區域、不同執行環境彼此互不影響。



#### 小結 :

今天我們知道全域執行環境就有全域變數、區域執行環境就有區域變數，

並且知道當區域內找不到變數時會向外查找，

向外查找的外部環境是指 外部定義的環境，不能直接認定就是全域環境與呼叫的環境。





執行JavaScript時，接收到翻譯的電腦會先創造一個全域執行環境。

當程式呼叫函式，就會在全域環境中創造該函式的\(區域\)執行環境。

而每個執行環境不論全域或區域，都有屬於自己的變數作用域存在。



當函式中使用到沒有定義的變數時，

JS會接著到 外部環境尋找\(外部參照\)，

注意這裡的外部環境不是指呼叫這個函式的執行環境，

而是指定義宣告這個函式的外部環境。

#### 範例

{% embed url="https://nightwing975.github.io/simonallenblog/2018/07/28/2018ITMAN/2018ITMAN-JS-Day05/" %}





### D6 JS是同步還是非同步?

瀏覽器有很多 引擎 ，包括 js 、 渲染 、 處理http請求 、 處理互動，瀏覽器是非同步的。\(但js是同步的\)

事件發生時 js 會將 事件 放置於 事件佇列 Event Queue ， 如 滑鼠/鍵盤點擊 . . .



當執行環境完成後 ， 才開始 注意 事件序列 ， 當有事件發生創造執行環境給對應函式執行。

在waitThreeSeconds\(\)執行完成、全域的console.log\('finished execution'\);也執行完成，

點擊事件才執行其對應函式clickHandler\(\)，因為JS會先把執行時期的同步程式都執行完，

才處理事件。 也就是說執行時間太長的同步程式可以干擾非同步事件的處理時間，在開發時得注意這點。



#### 小結 

同步與非同步是JS中極為重要的一環，當初學習AJAX時，

常在書上或聽人說JS會註冊非同步事件，向誰註冊呢?就是事件佇列Event Queue啦。





JS在瀏覽器的非同步行為，

其實是指 將非同步事件放到事件佇列，

而自己執行同步程式結束後，才處理事件。如果事件觸發函式，

就創造執行環境給對應函式，

所以說，當瀏覽器的其他引擎或處理器有事件、狀態發生並與JS引擎互動時，

這些事件就會被註冊進事件佇列裡。









### D7 型別與運算子

JavaScript是動態型別Dynamic Typing語言，

相較於 C\#、JAVA之類的靜態型別語言，

JS的變數不用在編輯時特意宣告型別\(例如布林值、字串、數值..等等\)，

在執行時它就會自動判別。



型別，JS有6種純值Primitive Types，\(又稱原始型別、基本型別\)，「純值」是什麼意思呢？

純值是指一種資料的型別\(型態\)，

換句話說，純值\(基本型別\)不是物件，因為物件就是名稱/值配對的組合物，

為避免與口語上的’值’搞混，下面純值改稱呼原始型別或基本型別。



undefined、null、boolean、number、string、



symbol es6 新增 可以賦予變數獨特性、獨一性。



運算子 可以想成是一個內建預設函式。





### D8 運算子的優先性

執行順序  :  運算子的兩個特性，優先性與相依性來決定。



優先性 表示哪一個運算子被優先運算，

當同一行程式有不只一個運算子時，

具高優先性的運算子會先計算，然後依序算到排序等級低的運算子。



相依性 表示運算子被計算的順序，

若是左到右計算的運算子就稱為左相依性，右到左則稱為右相依性。



若運算子的優先性都相同，那就是依據相依性來判斷順序，決定是左到右還是右到左運算。



### D9 強制型轉與比較運算子

johch3n611u :

因為是動態型別有許多例子可判斷比較時會出現的狀況。



typeof\(\) 可查詢型別



要判斷哪些東西會被轉型成數字可以用Number\(\)這個內建函式：



NaN在JS中表示 Not a Number 表示 不是數值 它也不是原始型別Primitive Types



使用三個=是嚴格比較，===運算子不只比對左右兩邊的值，連型別也會一起比較



運算子優先性相同，就依據相依性來判斷是左到右執行還是右到左執行。

#### 範例

{% embed url="https://nightwing975.github.io/simonallenblog/2018/07/29/2018ITMAN/2018ITMAN-JS-Day09/" %}

### 

### 

### 



### D10 存在與布林

存在existence與 布林boolean的關係



Boolean\(\)這個內件函式，來判斷型別轉為boolean的結果。



#### 範例

{% embed url="https://nightwing975.github.io/simonallenblog/2018/07/29/2018ITMAN/2018ITMAN-JS-Day10/" %}





### D11 設定函式內的預設值

#### 範例

{% embed url="https://nightwing975.github.io/simonallenblog/2018/07/29/2018ITMAN/2018ITMAN-JS-Day11/" %}





### Da12 物件與點

物件是一群名稱/值的組合

其值可以是另一個名稱/值的組合，也可以是數值、字串、布林、物件、函式….等等。

物件可以有個原始的設定，又稱屬性Properties， 

物件可以連結至另一個物件\(物件裡包含著物件\)，

這個被連結的物件也是屬性。 

物件裡可以有函式，當函式在物件裡，

稱為方法Methods，它既是函式，又與物件相關。



\[ \] 是運算子「計算取用成員運算子」。

. 運算子也是個函式，它叫做成員取用運算子，可以取用物件中的屬性與方法

不管是 \[ \] 運算子還是 . 運算子，

基本上都是透過查找左邊物件\(記憶體\)，去參照尋找右邊屬性\(記憶體\)位置







### D13 物件與物件實體

new Object\(\)來建立物件，但開發時相對少見這種寫法。

這是因為JS還有另一種更快建立物件的方法，就是 物件實體語法object literals。

#### 範例

{% embed url="https://nightwing975.github.io/simonallenblog/2018/07/29/2018ITMAN/2018ITMAN-JS-Day13/" %}



### D14 JSON與物件實體

JSON，全名JavaScript Object Notation

是受到JavaScript物件實體語法啟發的傳輸格式，

比起使用XML傳輸資料，JSON格式在檔案大小上更為輕量，也是現在主流的傳輸格式。



物件的屬性名name，在JSON中被引號包起來變成'name'，

也就是說原本的屬性名到了JSON變成字串，JavaScript利用物件實體語法時，

物件屬性名稱也可以是字串\(對物件實體語法，屬性的引號'或"可加可不加\)



相較之下，JSON格式的屬性名與值一定要加引號'成為 字串，

這裡要有個認知，雖然JSON是受到JavaScript啟發，

但 JSON和JavaScript是不一樣的東西。



JSON格式再傳輸時\(前端到後端、後端到前端\)，有時也會以 字串的形式處理。

#### JSON.stringify\(\)

將JS物件轉為JSON字串

#### JSON.parse\(\);

將JSON字串轉為JS物件



#### 資源

擴充套件JSONView

JSON Editor Online



### D15 函式就是物件

在JS這個物件導向語言裡，其函式的特性被稱為一級函式。First Class Functions

開發者對別的基礎型別做的事，也可以對函式去做，

因為 函式就是物件，而物件就是 名稱/值的組合。

開發者可以指派函式給變數，將函式傳入另一個函式，

也可以用實體語法建立函數。 函式可以有屬性和方法，

因為他就是物件，所以它可以連結到物件、屬性、其他函式\(Methood\)



而JS的函式還有隱藏的物件屬性，其中最重要的有兩個：

#### NAME

即名子屬性，然而名子不是必須的，函式也可以沒有名子\(匿名函式\)

#### CODE

即程式屬性code property，這個屬性的內容，就是開發者在編輯器上開發時，寫在函式內的程式碼。 我們寫的程式碼會成為這個屬性的值，而CODE屬性的特性是，它的內容是可以呼叫執行的。



```text
function greet(){
	console.log('運動');
}
greet.talk = 'hi';
console.log(greet.talk);
```

那這個函式，它的名子、物件屬性NAME是什麼呢？ 

就是greet 當我們欲呼叫執行函式，在編輯器寫上greet\(\)，

其實就是透過函式屬性名子greet去呼叫這個函式物件的定義，

而\(\)就會創造執行環境，執行這個函式物件程式屬性裡的內容，

也就是console.log\('hi'\);這段程式碼。



### D16 函式陳述句與函式表示式

#### Function Statement\(函式陳述句\)

程式碼的單位，這段程式碼不會產生一個值

#### Function Expression\(函式表達式、表示式\)

程式碼的單位，這段程式碼最終會產生\(回傳\)一個值，而這個值不一定會被開發者賦予變數。



#### 範例

{% embed url="https://nightwing975.github.io/simonallenblog/2018/07/29/2018ITMAN/2018ITMAN-JS-Day16/" %}



### D17 傳值by value與傳址by reference

johch3n611u :

\( 很邏輯觀念 必須要思考前面單元的運算優先考量與 傳值 傳址 的相互關係 \)





JS同時具有call by value和call by reference的特性，這種傳遞特性稱作call by sharing。



#### call by value傳值

當我們創造變數並給值時，變數會指向值在電腦記憶體中的位置，

若我們以這個值為參照，指定另一個變數指向這個值時，

電腦會在記憶體中新增\(複製\)一個新值，讓後來的這個變數指向新的值。

#### 在JavaScript裡，布林值、字串、數值、null、undefined都是call by value。



#### by reference傳址

當我們創造變數並給值\(物件\)時，變數會指向物件在電腦記憶體中的位置，

若我們以這個物件為參照，指定另一個變數指向這物件，

這個變數就會指向電腦記憶體中同樣的物件，不會有新的物件在記憶體中被創造出來。

#### 在JavaScript裡，物件、陣列、函式都是call by reference。



#### 範例 \( 很邏輯觀念 必須要思考前面單元的運算優先考量與 傳值 傳址 的相互關係 \)

{% embed url="https://nightwing975.github.io/simonallenblog/2018/07/29/2018ITMAN/2018ITMAN-JS-Day17/" %}







### D18 物件、函式與「this」

johch3n611u :

理解是 this 屬性 是在 創造階段時被創造，而 funtion 也是在此時被創造，

故 this 都會被指向 全域物件，而不是看身處的執行環境 。

用變數保存 this 則是因為 變數在 創造階段時是被宣告為 undefined，

在執行階段時才將值指入變數，

故此時的this就會是執行環境物件裡面的this屬性，而不是全域執行環境物件裡的this屬性。



ES6的箭頭函式，其this的狀況又不一樣，很常會需要先在外部環境用變數保存this，在箭頭函式內再調用



JavaScript在建立執行環境時，不論是全域、區域執行環境，

在創造時會一併建立一個變數 this。\( 執行環境物件 屬性 \)

而this會指向呼叫函式的執行環境，

更進一步的說，this會指向函式目前所在物件。



所以使用this必須要小心，得清楚它指向的物件對象是誰，否則可能會產生一些開發上的bug。

log方法裡的函式表達式otherLog，其this指向了全域物件！

在函式內定義的函式，裏頭函式的this會指向全域物件。

通常開發者會用個方法避免這種bug，那就是 用個變數保存this

#### 範例

{% embed url="https://nightwing975.github.io/simonallenblog/2018/07/29/2018ITMAN/2018ITMAN-JS-Day18/" %}









### D19 陣列、arguments、spread與分號

#### 陣列

開發時可以用 var arr = new Array\(\); 也可使用

陣列實體語法 var arr =  \[ \] ;

JS的陣列與物件很像，可以放各種資料，例如：布林值、物件和函數..等等。



#### arguments 物件

與this一樣，當一個函式的執行環境被創造出來時，\(執行環境物件的三屬性之一\)

也會一併創造出arguments這個關鍵字，它會保存所有傳進函式\(當參數\)的值。



#### 以下複習

#### 執行環境物件三屬性

1.變數物件 \(Variable Object\) 

1.1.建立參數物件Argument Object 並賦予內容\(值或物件或函式...\)。

1.2.函式建立新屬性指向記憶體位址並賦予內容\(值或物件或函式...\)。 

1.3.變數建立新屬性值初始為 undefined。 

2.作用域鏈 \(Scope Chain\) 、 3.“This” 變數 \(“This” Variable\)





#### spread operator展開運算子和rest operator其餘運算子都是...符號

ES6以後才新增的內容，這兩者根據使用的狀況和情境有很大的差別。

可能是因為作者錄製影片時，...運算子的規範還不齊全，所以影片快速帶過，這邊有興趣可以參考：

對照ES5中的相容語法，則是用apply函式

#### 展開運算子

```text
const params = [ "hello", true, 7 ]
const other = [ 1, 2, ...params ] // [ 1, 2, "hello", true, 7 ]





const aString = "foo"
const chars = [ ...aString ] // [ "f", "o", "o" ]





function sum(a, b, c) {
  return a + b + c
}
const args = [1, 2, 3]
sum(…args) // 6




function sum(a, b, c) {
  return a + b + c;
}

var args = [1, 2, 3];
sum.apply(undefined, args) ;// 6


```

#### 其餘運算子

```text
function sum(…numbers) {
  const result = 0

  numbers.forEach(function (number) {
    result += number
  })

  return result
}

sum(1) // 1
sum(1, 2, 3, 4, 5) // 15
```

#### 補充

{% embed url="https://eyesofkids.gitbooks.io/javascript-start-from-es6/content/part4/rest\_spread.html" %}











#### 重載函式 = 多載 \( JS裡面沒有多載 \)

其他程式語言如JAVA、C\#有重載函式的特性，可以創建名稱一樣，功能卻略有差別的函式。

JS並沒有這種特性，只能在函式裡寫判斷，並用複數函式互相呼叫，來達成類似的效果。







#### 分號

JS會幫我們自動加上分號，更確切的說是語法解析器幫我們加上的。

只是自動加上分號的特性，偶爾會造成bug。

```text
最佳寫法
return {
錯誤寫法
return
{
// return; 會自動解析為此
```

#### 範例

{% embed url="https://nightwing975.github.io/simonallenblog/2018/07/29/2018ITMAN/2018ITMAN-JS-Day19/" %}







### D20 立即呼叫的函式表示式\(IIFE\)



IIFE全名為Immediately Invoked Functions Expressions 

指的是可以立即執行的Functions Expressions函式表示式，中文多譯為立即\(執行\)函式。

```text
var hello = function(name){
	console.log('Hello ' + name);
}('Simon');
```

不少JS框架、套件的開頭與結尾被\(\)包住，程式碼被立即函式包著，

其目的是怕污染到使用者\(開發者\)的全域環境。

若全域和IIFE內都有重複的變數名，那這些框架、套件該如何取用全域的變數呢？

```text
var food = '水牛城雞翅';
console.log(food);
(function(global){
    var food = '雞塊';
	global.food = '麥脆雞';
    console.log(food);
})(window);
console.log(food);
```

為避免無意義的外部查找，將window當參數傳入，使其成為這個IIFE的區域物件，

確保在IIFE內的程式能故意取用到全域的特定變數。

#### 補充

{% embed url="https://nightwing975.github.io/simonallenblog/2018/07/29/2018ITMAN/2018ITMAN-JS-Day20/\#more" %}











### D21 閉包

closure閉包

MDN

閉包是一種特殊的物件，其中結合了兩樣東西︰函數，和函數所建立的環境。

環境由任意的局域變數所組成，這些變數是由在閉包建立的時間點上存在於作用域裡的所有變數。

使用閉包模擬私有

```text
var Counter = (function() {
  var privateCounter = 0;
  function changeBy(val) {
    privateCounter += val;
  }
  return {
    increment: function() {
      changeBy(1);
    },
    decrement: function() {
      changeBy(-1);
    },
    value: function() {
      return privateCounter;
    }
  }   
})();

alert(Counter.value()); /* 顯示 0 */
Counter.increment();
Counter.increment();
alert(Counter.value()); /* 顯示 2 */
Counter.decrement();
alert(Counter.value()); /* 顯示 1 */
```

{% embed url="https://developer.mozilla.org/zh-TW/docs/Web/JavaScript/Obsolete\_Pages/Obsolete\_Pages/Obsolete\_Pages/%E9%96%89%E5%8C%85%E7%9A%84%E9%81%8B%E7%94%A8" %}





{% embed url="https://nightwing975.github.io/simonallenblog/2018/07/29/2018ITMAN/2018ITMAN-JS-Day21/" %}

原作者的筆記中有點不好理解所以

#### 補充

{% embed url="https://pjchender.blogspot.com/2017/05/javascript-closure.html" %}





### D22 Function Factories、閉包與回呼

johch3n611u :

language = language \|\| 'en'; 





JS並沒有 重載函式的特性，但是可以用函式傳入的參數，在函式裡用if判斷達成類似的效果。

```text
function hello(firstname, lastname, language){
	language = language || 'en';

	if(language === 'en'){
		console.log('Hello ' + firstname + ' ' + lastname);
	}else if(language === 'es'){
		console.log('Hola ' + firstname + ' ' + lastname);
	}
}

hello('Simon' , 'Duke', 'en')
hello('Simon' , 'Duke', 'es')
```

這種作法的缺點是，若函式裡的判斷變多、傳進去的參數變多，

那呼叫時的 易讀性會降低不少，開發者可以用閉包的特性，修改其內容。

```text
function makeHello(language){

	return function(firstname , lastname){

		if(language === 'en'){
			console.log('Hello ' + firstname + ' ' + lastname);
		}else if(language === 'es'){
			console.log('Hola ' + firstname + ' ' + lastname);
		}
	}
}

//閉包特性
var englishHello = makeHello('en');
var spanishHello = makeHello('es');

englishHello('Simon', 'Duke')
spanishHello('Simon', 'Duke')
```







我們常用的setTimeout，既是非同步，也用到了 函式表示式和 閉包的特性。

```text
function sayHi(){

	var hi = '你好';

	setTimeout(function(){
		console.log(hi);
	}, 3000);
}
sayHi();
```

呼叫執行sayHi函式，setTimeout註冊事件，3秒後執行傳給setTimeout當參數的匿名函式，

印出變數hi的值你好。 但3秒後sayHi函式的執行環境早就結束了，

這個hi變數理應隨著sayHi執行環境結束而從記憶體清除，

只是因為 閉包的特性，hi仍存在記憶體，所以才可以正常顯示出來。





回乎函式 當我們呼叫、使用了一個函式，可以當成「call」一個函式，

當這個函式執行到一定程度，在裏頭呼叫「call」另一個函式，

這個被叫來的函式就是callback function回乎函式。

```text
function tellMeWhenDone(callback){

	console.log('已完成!');

	callback();
}

tellMeWhenDone(function(){
	console.log('全部完成囉!');
});
```



### D23 函式內建方法：bind\(\)、call\(\)與apply\(\)

當函式執行環境被創造出來，會一併創造arguments關鍵字，

保存帶入自己的參數; 也會一併創造出this關鍵字，指向函數目前所處的物件，

關於this可以參考這天的筆記。

若我們希望修改this指向的對象，有辦法達到這個目的嗎？

我們可以利用bind\(\)、call\(\)、apply\(\)這些函式內建方法來達成目的



#### bind\(\)

可以創造函式的拷貝版，並且可以透過傳入物件來綁定\(指定\)它的this是誰



#### call\(\)

和bind\(\)有點類似，可以透過傳入物件來綁定\(指定\)它的this是誰，

但call\(\)會馬上執行該函式，所以在\(\)傳入物件當參數綁定時，

也可以一併傳入這個函式執行所需的其他參數。



#### apply\(\)

也可以透過傳入物件來綁定\(指定\)它的this是誰，

並且和call\(\)一樣會直接執行呼叫它的函式，使用時也可以帶入函式所需參數，

只是這個參數必須是陣列形式。



#### function borrowing　

可以跟別的物件借方法來操作



#### function currying

建立一個函式的拷貝，並設定要傳入的參數預設值，有許多數學運算需處理時很有妙用

bind\(\)除了可以帶入欲綁定this的物件，也可以帶入函式的參數，

但bind帶入的參數會成為這個函式參數的預設值



#### 範例

{% embed url="https://nightwing975.github.io/simonallenblog/2018/07/29/2018ITMAN/2018ITMAN-JS-Day23/" %}





### D24 函數程式設計

小結 今天影片這樣看下來，使用函數程式設計的優點有

簡潔 

速度快 

維護性好 

可重複利用

#### 範例

{% embed url="https://nightwing975.github.io/simonallenblog/2018/07/29/2018ITMAN/2018ITMAN-JS-Day24/\#more" %}















### D25 古典與原型繼承、瞭解原型

#### Classical Inheritance 古典繼承

C\#、JAVA常用到的物件繼承方式，有一些專有名詞\(語法\)如：

private私用、protected保護、friend夥伴、 interface介面…等術語。

古典繼承很流行，也解決了很多問題，但樹狀結構物件的互動模式，

一但數量增加，很容易產生複雜、龐大的集合。



#### Prototypal Inheritance 原型繼承

JavaScript的物件繼承種類，相對古典繼承簡易、彈性，

既然是所謂Prototypal「原型」，就代表有相對於目前物件的原型存在。

JS的所有物件\(包含函式\)都有個隱藏屬性proto，

這個屬性會參考、指向另一個\(原型\)物件，而這個被指向的原始物件，就是我們當下這個物件的原型。

一個物件可以參照一個原型\(物件\)，這個原型物件可以再參照另一個原型\(物件\)。 

當我們使用的屬性、方法不存在當下操作的物件中，JS就會往該物件的原型去查找，

有就存取它，沒有就在往原型的原型物件查找，直到盡頭。



原型鏈查找與 範圍鏈\(外部參照\)是 不一樣的東西，

前者是去物件的原型對象找屬性和方法，後者是往外部執行環境找變數，這兩者不可搞混。



影片警告：這裡範例使用proto只是為了方便說明，

在現實生活中非常不建議使用proto，因為瀏覽器會真的存取物件原型，導致程式的執行速度變慢。



物件使用proto改變原型\(指向其他物件），操作其他物件的函式方法，

函式的this仍會指向存取、執行它的物件，而不是函式方法原本所屬的物件。



每個物件都有原型物件對象、範圍練和原型練是不一樣的東西、

proto是物件函式的內建屬性，實際開發不要使用它。



#### 範例

{% embed url="https://nightwing975.github.io/simonallenblog/2018/07/29/2018ITMAN/2018ITMAN-JS-Day25/" %}





### D26 物件型別、Reflection and Extend

Primitive Types 基本型別

#### Object Type 物件型別

JavaScript除了Primitive Types以外的東西，全都是物件型別！

JS物件的原型仍會是物件，而原型鏈的終點，就是null。



#### Reflection

一個物件列出自己的屬性，然後改變自己的屬性和方法，

我們可以藉Reflection，實現extend\(擴展、繼承\)這個模式來幫我們達成目的。



extend

可以將其他物件的屬性、方法新增給我們的目標物件。

extend\(s\)是JS物件導向很重要的觀念、語法，可以讓JS物件獲得其他物件的屬性和方法，

尤其現在使用Vue、React開發，很常看到ES6的extends存在。









#### 補充

forin 目標內的東西全都遍歷、loop個一輪 每個屬性\(值\)都取出來

superman\[prop\]的\[\]不是陣列，而是可以存取物件屬性的計算取用成員運算子運算子

```text
for (var prop in superman) {
  console.log(prop + ':' + superman[prop])
}
```

內建方法hasOwnProperty 篩選 自身屬性

hasOwnProperty可以檢查屬性是不是該物件本身的成員，

若有非物件本身的屬性存在\(包含物件原型的屬性\)，就回傳false

```text
for (var prop in superman) {
    if(superman.hasOwnProperty(prop)){
        console.log(prop + ':' + superman[prop])
    }
}
```

```text

```

{% embed url="https://pjchender.blogspot.com/2016/06/javascript-for-in-function.html" %}





### D27 函式建構子與new

function constructor 函式建構式\(或譯函式建構子\)

能用來新建物件的一種函式，透過與new運算子一起使用，能創建出新物件並設定該物件的屬性與方法



函式的內建屬性prototype不是用來取用函式自己的原型物件，而是用來取用建構子創造出來的物件原型。



#### 小結

透過function constructor與new建立物件，是JS很常見的用法，尤其是使用某些plugins，

很常看到類似的寫法。







#### 範例

{% embed url="https://nightwing975.github.io/simonallenblog/2018/07/30/2018ITMAN/2018ITMAN-JS-Day27/" %}





#### D28 內建的函式建構子

什麼是內建的函式建構子？

其實是將JS的內建的Number\(\)、String\(\)、Date\(\)….等其他內建函式，與new函式建構子一起使用。



prototype不是用來取用函式自己的原型，而是用來取用new建構子創造出來的物件原型，

重點在於，我們可以透過prototype去取用方法、新增方法，

透過原型鏈的查找，我們可以讓new建構子創造出來的物件原型使用我們定義的函式，

這等於我們可以改造JS程式，添加一些自己的函式進原型中。



透過new建立像 基本型別Primitive Types的物件型別Object Types，

如今天講的字串、數值，可能導致我們開發時搞混，因而造成一些bug出現，

畢竟new建立建立出來的是物件型別Object Types



#### 小結 

使用內建的函式建構子\(JS內建函式 + new函式建構子\)需要非常小心。 

開發者往往覺得new Number\(\)、new String\(\)、new Date\(\)…等寫法

和Number\(\)、String\(\)、Date\(\)的結果一樣：只是多了一個new建構子，

一樣會建立新的 基本型別Primitive Types，但只要使用new在函式前面，

就會創建一個新的 物件\(型別\)Object Types出來，

而我們往往不會意識到…自己操作的其實是物件。



#### 範例

{% embed url="https://nightwing975.github.io/simonallenblog/2018/07/30/2018ITMAN/2018ITMAN-JS-Day28/" %}











#### D29 Object.create與class

除了new建構子和物件實體語法，JS還有別種建立物件的方法，

那就是ES5新增的Object.create和ES6新增的類別class。

```text
var Flash = {
	firstname: '預設值',
	lastname: '預設值',
	run: function(){
		return 'Run,' + this.firstname + ',Run!';
	}
}
var Barry = Object.create(Flash);
console.log(Barry);
```

####  **polyfill**

有些舊版引擎會缺少開發者要用的程式語法，

開發者可以用程式檢驗JS引擎\(瀏覽器\)是否有目標程式語法，

如果沒有就由開發者撰寫程式添加進去。



#### ES6新增的class類別

```text
class TheFlash{

	constructor(firstname, lastname){
		this.firstname = firstname;
		this.lastname = lastname;
	}

	run(){
		return 'Run,' + this.firstname + ',Run!';
	}
}

var Barry = new TheFlash('Barry', 'Allen');
console.log(Barry);
```

我們設定一個class類別TheFlash，在裏頭有constructor建構子，

和函式建構子一樣，我們可以預先設定物件值，也可以設定函式方法在class裏頭。

設定好class，要創建新物件仍然使用new並帶入參數，

參數會對應到constructor的設定，以此創建物件



在其他的語言，類別是創建物件的藍圖，但在ES6，

類別本身是被建立出來的物件，class TheFlash是一個實際被建立的物件，

開發者是從物件建立新物件，也就是說，ES6的JS，本質仍然是原型繼承，而非變成古典繼承。



那class要怎麼設定原型呢？可以使用ES6新增的extends來處理。

\(注意：不是第26天介紹的underscore.js函式庫extend，是extends，字尾多了一個s\)



設定類別SpeedRunner並extends TheFlash\(extends會 繼承類別TheFlash\)，

在constructor內，透過super方法去獲得父類別TheFlash的建構式參數設定。

RedRunner此時的父類別是類別TheFlash，它能透過原型鏈取用方法run\(\)嗎？



#### 小結

class語法本質上還是JS原型繼承，也就是所謂語法糖，

extend\(擴展、繼承\)的概念也很容易在現今JS框架中看到，

例如Vue.js容易見到extend、React容易見到extends，

可以說JS的框架都是以JS原型繼承的概念為基礎並加以擴展的。





#### 範例

{% embed url="https://nightwing975.github.io/simonallenblog/2018/07/30/2018ITMAN/2018ITMAN-JS-Day29/" %}









### D30 心得-持續學習、持續成長

{% embed url="https://nightwing975.github.io/simonallenblog/2018/07/30/2018ITMAN/2018ITMAN-JS-Day30/" %}





johch3n611u :

總共花了兩天約 8-10小時看完，

共筆感覺稱不上比較多的還是去看原作者整理過後的心得感想，

作者最後只講到原影片內容的第六章節，並開始檢討他三十天賽程的狀況

個人認為看影片或許能獲得蠻多的知識，

但在這個資訊爆炸的時代，

在上完了七個月的密集課程後才發現真的還有很多要學習的，

不只是實務上，還有理論，理論的應用等等...

而且看別人整理過後的文章在自我理解，

似乎能夠省去不少純看影片的時間，

但短期目標還是想試試看有沒有機會轉到前端，

感覺必須要針對目標做更多努力與嘗試。











