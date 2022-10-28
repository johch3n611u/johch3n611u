# JavaScript 筆記 2020_0620

## 應用層面

<https://ithelp.ithome.com.tw/m/users/20112093/ironman/1854>

## 參考

<https://ithelp.ithome.com.tw/articles/10200406>

---

## 運算子、運算式、值與型別、變數、條件式、迴圈

<https://coggle.it/diagram/XJdj8UA5tjeIugda/t/%E5%85%AD%E8%A7%92%E5%89%8D%E7%AB%AF%E5%A4%A7%E8%A3%9C%E5%B8%96>

### 前言

這部分跟程式基本邏輯應該沒差多少，只紀錄不懂的，與自己理解的。

---

* 字面值（Literal Value） e.g. `a = b + 1` 中的 1
* 直譯器與編譯器可將程式碼由上到下逐行轉為電腦可懂的命令。其差別在於時機點

> 直譯（interpret）：在程式執行「時」做轉換。
>
> 編譯（compile）：在程式執行「前」做轉換，然後會產出編譯後的指令，因此之後執行的是這個編譯後的結果。

* [注意，在`瀏覽器`中 JavaScript 會透過`瀏覽器引擎`會在每次執行前即時`編譯`程式碼，`接著立刻執行編譯後的指令`。](https://yu-jack.github.io/2020/03/16/javascript-is-compiler-or-interpreter-language/)
* 物件特性的存取運算子（object property access:）利用 `.`（點記號法，dot notation） 或 `[ ]`（方括號記號法，bracket notation） 的方式存取物件的特性，例如：obj.a 或 obj['a']，. 因為簡單便利較常使用，但 [ ] 卻可在索引值是變數或有特殊字元時能保證完成值的存取，例如：obj['h e l l o']（有空白）、obj['#$%^&']（特殊字元）、obj['123']（開頭為數字）。若想了解命名規則，待變數命名的部份會再詳述。
* 字串運算子（string）：+ 可串接兩字元，並回傳結果，通常用於連接變數與字串。不過目前都改用 ES6 的字串模板（string template）了，使用 ${ variable_name } 即可代入變數，而不需再用 + 與雙/單引號拼湊字串，方便許多，範例如下。

```JavaScript
const name = 'Summer';

// 使用字串運算子
const greetings_1 = 'Hello ' + name + '!'; // "Hello Summer!"

// 使用字串模板
const greetings_2 = `Hello ${name}!`; // "Hello Summer!"
```
