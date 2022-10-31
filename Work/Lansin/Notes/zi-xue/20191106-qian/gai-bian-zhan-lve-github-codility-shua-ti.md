---
description: >-
  經過昨日試看影片與查找資料後覺得 ， 現階段剩下的時間內 ， 如果繼續看影片雖然可以增強js功力 ，但顯然對於 codility 是沒有幫助的 ，
  所以改為刷以上看到的題目 ， 首先是將 英文都翻為中文 ， 後為看懂 ，這樣如果考的時候有類似題目 想參考資料查找也會快許多 ， 依照進度如果星期四進度太慢
  ， 可能星期五會請假來看 ... 畢竟真的有點不想在這繼續待 道不同 ... 想走互動阿
---

# 改變戰略 Github Codility 刷提

{% embed url="https://gist.github.com/lalkmim/e04845eb9d1c5936622a" caption="" %}

整體看了一遍後感覺有點複雜雖然都有JS範例可以看，但這感覺不太像請假一天就能起來的 ...

感覺只能先看影片了，可能看完影片後會試著寫L1-5 ...

## L1

### Iterations 迭代

反覆運算

### Binary Gap（二進位制空白）

[https://www.itread01.com/content/1545609902.html](https://www.itread01.com/content/1545609902.html)

[https://medium.com/@bob800530/codility-ch1-binarygap-9408cc8f1a73](https://medium.com/@bob800530/codility-ch1-binarygap-9408cc8f1a73)

二進位數組中一與一之間的零的出現最大值

在整數的二進製表示中查找最長的零序列。

digit 數字

binary 二禁制

counter 計數器

max 最大值

parseInt\( 字串 , 指定字串是哪種進位2-36 \) 函数可解析一个字符串，并返回一个整数。

因為 js 特性 如不指定字串 _進位_制 會依照字串內容去自動判斷

[https://www.w3school.com.cn/jsref/jsref\_parseInt.asp](https://www.w3school.com.cn/jsref/jsref_parseInt.asp)

```text
function solution(N) {
    // write your code in JavaScript (Node.js 4.0.0)

    var number = N;
    var binary = '';
    var counter = -1;
    var max = 0;
    while(number > 0) {
        var digit = number % 2;

        if(digit === 1) {
            if(counter > max) {
                max = counter;
            }
            counter = 0;
        } else if(counter >= 0) {
            counter++;
        }

        binary = '' + digit + binary;
        number = parseInt(number / 2);
    }

    //console.log('number, binary:', number, binary);

    return max;
}
```

進來函式的數被假設為一定為正整數N ， 但大小不知道

目的是求最大 正整數N的 [BinaryGap](https://app.codility.com/demo/results/trainingU2FQPQ-7Y4/#) 在整數的二進製表示中查找最長的零序列。

所以一定要將正整數 轉為二進制才有辦法 計算，

計算方式為 除與 二 取餘數

接著 利用 計數器 與 最大值 兩變數來進行比對 計算 最大 [BinaryGap](https://app.codility.com/demo/results/trainingU2FQPQ-7Y4/#)

首先要先計算出 2進制為多少

```text
// 在計算出完整二進制前不會停止

while(number > 0) {
var digit = number % 2;
binary = '' + digit + binary;
number = parseInt(number / 2);
 }
```

接著排除掉如 100010000 右邊四個零 不能計算

所以 如果是那種情況的話 第一次遇到一時才將 計數器 歸0

```text
var counter = -1;

if(digit === 1) {


//直到計數器大於最大BinaryGap時將計數器的值轉為新的BinaryGap最大值
//如不然當下一次遇到一時則將計數器歸零重算

           if(counter > max) {
                max = counter;
            }
            counter = 0;
        } 

        //開始計算 當 digit 為零 且不是 第一次遇到一後 每遇到零一次 counter++ 就+1次
        else if(counter >= 0) {
            counter++;
        }
```

## L2

### OddOccurrencesInArray 陣列中的基數出現次數

[http://cain19811028.blogspot.com/2017/02/codility-in-python-lession-2.html](http://cain19811028.blogspot.com/2017/02/codility-in-python-lession-2.html)

[https://www.jianshu.com/p/904bb11cf358](https://www.jianshu.com/p/904bb11cf358)

查找奇數個元素中出現的值。

### 網路上看到三種方法可以解

1.排序比對

由题目要求可知，元素个数为奇数个，只有一个元素是未配对的数；

先将数组进行排序（使用泛型算法中的方法）；

遍历数组，每次前进2个位置，如果第i位置与第i+1位置的元素不相等，则，第i位置的元素就是我们要求的数；

2.groupby

好像是類似去計算相同資料筆數如為一則返回答案

3.第三種為 XOR

[https://zh.wikipedia.org/wiki/%E9%80%BB%E8%BE%91%E5%BC%82%E6%88%96](https://zh.wikipedia.org/wiki/%E9%80%BB%E8%BE%91%E5%BC%82%E6%88%96)

感覺易懂又好解決範例也是用此解決。

與一般的[邏輯或](https://zh.wikipedia.org/wiki/%E9%80%BB%E8%BE%91%E6%88%96)OR不同，當兩兩數值相同為否，而數值不同時為真。

#### 按位异或赋值（Bitwise XOR assignment）[节](https://developer.mozilla.org/zh-CN/docs/Web/JavaScript/Reference/Operators/Assignment_Operators#%E6%8C%89%E4%BD%8D%E5%BC%82%E6%88%96%E8%B5%8B%E5%80%BC%EF%BC%88Bitwise_XOR_assignment%EF%BC%89) <a id="&#x6309;&#x4F4D;&#x5F02;&#x6216;&#x8D4B;&#x503C;&#xFF08;Bitwise_XOR_assignment&#xFF09;"></a>

按位异或赋值运算符使用两个操作值的二进制表示，执行二进制异或运算，并把结果赋给变量。

![](../../.gitbook/assets/image%20%2842%29.png)

```text
Operator: x ^= y 
Meaning:  x  = x ^ y
```

```text
// you can write to stdout for debugging purposes, e.g.
// console.log('this is a debug message');

function solution(A) {
    // write your code in JavaScript (Node.js 4.0.0)

    var agg = 0;

    for(var i=0; i<A.length; i++) {
        agg ^= A[i];
    }

    return agg;
}

// For example, for the input [2, 2, 3, 3, 4] the solution terminated unexpectedly.
```

很簡單最一開始時 agg 為零 所以肯定不會跟 A\[i\] 相同 所以 XOR 為真

所以 A\[i\] 就會被指向 agg

```text
Operator: x ^= y 
Meaning:  x  = x ^ y
0为false，非0为true
```

^= 比較複雜 所以先討論 ^

比對第二筆至第A.length時有四種情況

![](../../.gitbook/assets/image%20%2854%29.png)

但加上 = 後 其實只有兩種情況 那就是 T 或 F

可以看到上圖 很明顯 TT 與 FF 都是 F

得到一個簡單的結論

相同則不取代，不相同則取代

然後迴圈的次數與比較方式則是從位址0到資料筆數最後一筆，所以不會有無窮迴圈的狀況

最後筆對就會是唯一一筆不重複的

### CyclicRotation 陣列旋轉循環 位址調換

[https://medium.com/@bob800530/codility-ch2-cyclicrotation%E8%A7%A3%E6%B3%95-110f5659ad87](https://medium.com/@bob800530/codility-ch2-cyclicrotation%E8%A7%A3%E6%B3%95-110f5659ad87)

[https://www.jianshu.com/p/f4e67794d237](https://www.jianshu.com/p/f4e67794d237)

題目 A為一個長度為N的整數陣列 N和K皆為介於0~100的整數 A內的元素皆介於−1000~1000

求A陣列輪轉K次後的值。

範例:

A = \[3, 8, 9, 7, 6\] K = 3

0 1 2 3 4

A輪轉1次 \[6,3,8,9,7\]

A輪轉2次 \[7,6,3,8,9\]

A輪轉3次 \[9,7,6,3,8\]

return \[9,7,6,3,8\]

0 -&gt; 4

由上可看出 輪轉幾次 原本位址就會+上輪轉次數+1位址 \( i+k \)

但如果K輪轉次數超過陣列大小時呢?會再從第一個陣列開始繼續下去 % A.length;

result\[newPos\] = A\[i\]; 至換

但總有特例那就是當 陣列長度為1時

```text
|| 或 or
```

輪轉次數為零時

```text
 if(A.length === 1 || K === 0) {
        return A;
    }
```

這行應該能減少時間複雜度之類的

```text
// you can write to stdout for debugging purposes, e.g.
// console.log('this is a debug message');

function solution(A, K) {
    // write your code in JavaScript (Node.js 4.0.0)

    var result = [];

    if(A.length === 1 || K === 0) {
        return A;
    }

    for(var i=0; i<A.length; i++) {


        var newPos = (i+K) % A.length;

        result[newPos] = A[i];
    }

    return result;
}
```

## L3

### Time complexity 時間複雜度

演算法的是一個函式，它定性描述該演算法的執行時間。

這是一個代表演算法輸入值的字串的長度的函式。

### FrogJmp 青蛙跳 計算從位置X到Y的最小跳躍次數。

X跳到Y一次跳D可以跳幾次

[https://medium.com/@bob800530/codility-ch3-frogjmp%E8%A7%A3%E6%B3%95-416c1866cd41](https://medium.com/@bob800530/codility-ch3-frogjmp%E8%A7%A3%E6%B3%95-416c1866cd41)

題目 一隻青蛙須從X點跳到Y點，一次可以跳D格的距離，

試求青蛙最少要跳幾次?

X, Y, D 皆為介於1~1,000,000,000間的整數 X ≤ Y

範例 X = 10 Y = 85 D = 30 從10跳到85最少需要跳3次，

因為\(85-10\)/3 = 2..15，只要餘數不是0皆須多跳一步

```text
// you can write to stdout for debugging purposes, e.g.
// console.log('this is a debug message');

function solution(X, Y, D) {
    // write your code in JavaScript (Node.js 4.0.0)
    if(X === Y) {
        return 0;
    } else if(D >= (Y-X)) {
        return 1;
    } else {
        var jumps = parseInt((Y-X)/D);
        jumps += ((Y-X)%D > 0) ? 1 : 0;

        return jumps;
    }
}
```

### PermMissingElem 吐出缺值 找到給定排列中缺少的元素。

給一個含缺值的連續數列 A，尋找缺值的數字為多少，使用連續數列的總和減去原數列即可

[https://siansiansu.github.io/posts/2019-02-09-%E7%AD%86%E8%A8%98-codility-permmissingelem/](https://siansiansu.github.io/posts/2019-02-09-%E7%AD%86%E8%A8%98-codility-permmissingelem/)

A \[0\] = 2 A \[1\] = 3 A \[2\] = 1 A \[3\] = 5

return 4

```text
// you can write to stdout for debugging purposes, e.g.
// console.log('this is a debug message');

function solution(A) {
    // write your code in JavaScript (Node.js 4.0.0)
    A.sort(function(a, b) {
        return a - b;
    });

    var next = 1;
    var i=0;
    while(next === A[i]) {
        next++;
        i++;
    }

    return next;
}
```

.sort 方法會原地（in place）對一個陣列的所有元素進行排序，並回傳此陣列。

[https://developer.mozilla.org/zh-TW/docs/Web/JavaScript/Reference/Global\_Objects/Array/sort](https://developer.mozilla.org/zh-TW/docs/Web/JavaScript/Reference/Global_Objects/Array/sort)

為了比較數字而不是字串，比較函式可以僅僅利用 a 減 b。以下函式將會升冪排序陣列：

function compareNumbers\(a, b\) { return a - b; }

```text
    var next = 1;
    var i=0;
    while(next === A[i]) {
        next++;
        i++;
    }
```

A \[0\] = 2

A \[1\] = 3

A \[2\] = 1

A \[3\] = 5

sort

A \[0\] = 1

A \[1\] = 2

A \[2\] = 3

A \[3\] = 5

給出了由N個不同整數組成的陣列A. 該數組包含\[1 ..（N + 1）\]範圍內的整數，這意味著只缺少一個元素。

所以一定是 從1 開始 ?

N是\[ 0 ... 100,000 \] 範圍內的整數;

A的元素都是截然不同的;

數組A的每個元素是\[1 ..（N + 1）\]範圍內的整數。

### TapeEquilibrium 膠帶平衡 最小化值\|（A \[0\] + ... + A \[P-1\]） - （A \[P\] + ... + A \[N-1\]）\|。

依序從數列左邊的加總減掉數列右邊的加總，然後取絕對值，找出最小的差，

[https://siansiansu.github.io/posts/2019-02-09-%E7%AD%86%E8%A8%98-codility-tapeequilibrium/](https://siansiansu.github.io/posts/2019-02-09-%E7%AD%86%E8%A8%98-codility-tapeequilibrium/)

```text
// you can write to stdout for debugging purposes, e.g.
// console.log('this is a debug message');

function solution(A) {
    // write your code in JavaScript (Node.js 4.0.0)
    var lower = [A.length];
    var upper = [A.length];

    lower.push(0);
    //https://www.w3school.com.cn/jsref/jsref_push.asp
    //push() 方法可把它的参数顺序添加到 arrayObject 的尾部。它直接修改 arrayObject，而不是创建一个新的数组。push() 方法和 pop() 方法使用数组提供的先进后出栈的功能。

    for(var i=0; i<A.length; i++) {
        var iRev = A.length - i - 1;

        if(i === 0) {
            lower[i] = 0;
        } else {
            lower[i] = lower[i-1] + A[i-1];
        }       

        if(iRev === A.length - 1) {
            upper[iRev] = A[iRev];
        } else {
            upper[iRev] = upper[iRev+1] + A[iRev];
        }        
    }

    var result = Math.abs(lower[1] - upper[1]);
    //https://developer.mozilla.org/zh-TW/docs/Web/JavaScript/Reference/Global_Objects/Math

    for(var i=2; i<lower.length; i++) {
        var diff = Math.abs(lower[i] - upper[i]);
        if(diff < result) {
            result = diff;
        }
    }

    return result;
}
```

依序從數列左邊的加總減掉數列右邊的加總，然後取絕對值，找出最小的差，

舉個例子好了：

假設一個數列 A = \[3, 1, 2, 4, 3\]

當步驟為 1 的時候，從數列左邊數過來第一個，減掉從右邊數到左邊第二的絕對值為 7，也就是 \|3 − \(1 + 2 + 4 + 3\)\| = 7

接下來指針往右移動

當步驟為 2 的時候，他們之間的絕對值差為 \|\(3 + 1\) − \(2 + 4 + 3\)\| = 5

當步驟為 3 的時候，他們之間的絕對值差為 \|\(3 + 1 + 2\) − \(4 + 3\)\| = 1

當步驟為 4 的時候，他們之間的絕對值差為 \|\(3 + 1 + 2 + 4\) − 3\| = 7

從步驟 1 ~ 4，選出等號右邊最小的，所以正確答案為 1 。

題目的意思大概是這樣，開始解題吧！

## L4

### Couting Elements 計數元素

### MissingInteger 失蹤整數 找到給定序列中不存在的最小正整數。

給一個含有 N 個整數的陣列 A，返回最小的正整數 \(必須大於 0\) 且不含在 A 陣列裡面  
舉例來說 A = \[1, 3, 6, 4, 1, 2\] ，則 function 應該要返回 5。

[https://fightwennote.blogspot.com/2018/05/codility-missinginteger.html](https://fightwennote.blogspot.com/2018/05/codility-missinginteger.html)

這是一個演示任務。

寫一個函數：

功能解決方案（A）;

在給定N個整數的數組A的情況下，返回A中不存在的最小正整數（大於0）。

例如，給定A = \[1,3,6,4,1,2\]，函數應返回5。

給定A = \[1,2,3\]，函數應返回4。

給定A = \[-1，-3\]，函數應返回1。

為以下假設編寫有效的算法：

N是\[ 1 ... 100,000 \] 範圍內的整數; 數組A的每個元素都是\[ -1,000,000 ... 1,000,000 \] 範圍內的整數。

### FrogRiverOne 青蛙過河 找到青蛙可以跳到河的另一邊的最早時間。

一隻青蛙由河岸的左側跳到右側時，需要藉由葉子作為墊腳石前進，青蛙由左側跳到右側，一共需要跳X下，因此這些葉子須坐落於1~X的位子時，青蛙才有可能跳到右側。

[https://medium.com/@bob800530/codility-ch4-frogriverone%E8%A7%A3%E7%AD%94-f81e9fcf99e9](https://medium.com/@bob800530/codility-ch4-frogriverone%E8%A7%A3%E7%AD%94-f81e9fcf99e9)

### PermCheck 檢查陣列 檢查數組A是否是排列。

[https://medium.com/@bob800530/codility-ch4-permcheck解答-f43ced27c116](https://medium.com/@bob800530/codility-ch4-permcheck%E8%A7%A3%E7%AD%94-f43ced27c116)

檢查所接收到的A\[\]是否為數列，是的話return 1，否則return0。

### MaxCounters 最大計數器

應用所有交替操作後計算計數器的值：將計數器增加1; 將所有計數器的值設置為當前最大值。

[https://blog.csdn.net/l769255844/article/details/76966091](https://blog.csdn.net/l769255844/article/details/76966091)

[https://www.google.com/search?biw=1280&bih=577&ei=ifRlXfDtHqCXr7wP65WB0Aw&q=MaxCounters+%E4%B8%AD&oq=MaxCounters+%E4%B8%AD&gs\_l=psy-ab.3..33i160.9017.11600..12050...2.2..0.85.291.7......0....1..gws-wiz.......0i71j0j0i67j0i30j0i19j0i30i19j0i8i30i19j0i5i30i19.YI5Z6uL6CIg&ved=0ahUKEwiw1ty0z6TkAhWgy4sBHetKAMo4ChDh1QMICw&uact=5](https://www.google.com/search?biw=1280&bih=577&ei=ifRlXfDtHqCXr7wP65WB0Aw&q=MaxCounters+%E4%B8%AD&oq=MaxCounters+%E4%B8%AD&gs_l=psy-ab.3..33i160.9017.11600..12050...2.2..0.85.291.7......0....1..gws-wiz.......0i71j0j0i67j0i30j0i19j0i30i19j0i8i30i19j0i5i30i19.YI5Z6uL6CIg&ved=0ahUKEwiw1ty0z6TkAhWgy4sBHetKAMo4ChDh1QMICw&uact=5)

## L5

### Prefix Sums 前綴總和

[https://rust-algo.club/sorting/counting\_sort/](https://rust-algo.club/sorting/counting_sort/)

[https://blog.csdn.net/baimafujinji/article/details/6477724](https://blog.csdn.net/baimafujinji/article/details/6477724)

### CountDiv 計數DIV 計算在\[a..b\]範圍內可被k整除的整數數。

[http://ju.outofmemory.cn/entry/223830](http://ju.outofmemory.cn/entry/223830)

對於A = 6，B = 11和K = 2，你的函數應該返回3，因為在\[6..11\]範圍內有三個可被2整除的數字，即6,8和10.假設：A和 B是

### PassingCars 車牌檢驗 計算路上過往車輛的數量。

[https://www.google.com/search?q=PassingCars&oq=PassingCars&aqs=chrome..69i57j69i60&sourceid=chrome&ie=UTF-8](https://www.google.com/search?q=PassingCars&oq=PassingCars&aqs=chrome..69i57j69i60&sourceid=chrome&ie=UTF-8)

在一條雙向道上，每台車上都有車號為A\[\]的index，向東行的車其值為0，向西行的車其值為1，**向東行的車號需&lt;向西行的車號**，在這條雙向道上，有幾種會車的可能?

### GenomicRangeQuery   從一系列序列DNA中找到最小的核苷酸。

#### 快速計算矩陣內單位總和的方式

[http://myjavawar.blogspot.com/2018/10/genomicrangequery.html](http://myjavawar.blogspot.com/2018/10/genomicrangequery.html)

### MinAvgTwoSlice 找到包含至少兩個元素的任何切片的最小平均值。

[https://blog.csdn.net/dear0607/article/details/42581149](https://blog.csdn.net/dear0607/article/details/42581149)

## L6

### Sorting 排序

### Triangle

三角

確定是否可以從給定的邊緣集構建三角形。

### Distinct

區別

計算數組中不同值的數量。

### MaxProductOfThree

最大產品

對任何三重態（P，Q，R）最大化A \[P\] \* A \[Q\] \* A \[R\]。

### NumberOfDiscIntersections

數

的

圓盤

交叉口

計算一系列光盤中的交叉點數量。

## L7

### Stacks and Queues 堆疊與佇列

### Brackets

括號

確定給定的括號字符串（多個類型）是否已正確嵌套。

### Fish

N貪婪的魚正沿著河流移動。計算有多少魚活著。

### StoneWall

石牆

使用最小數量的矩形覆蓋“曼哈頓天際線”。

### Nesting

嵌套

確定給定的圓括號（單一類型）是否已正確嵌套。

## L8

### Leader 索引

### [EquiLeader](https://app.codility.com/demo/results/trainingV5AZPJ-J9X/#)

球菌 領導

找到索引S使得序列A \[0\]，A \[1\]，...，A \[S\]和A \[S + 1\]，A \[S + 2\]，...，A \[\[\]的領導者N - 1\]是一樣的。

### Dominator

統治者

查找數組的索引，使其值出現在數組中超過一半的索引處。

## L9

### Maximum slice problem [最大子數列問題](http://shubo.io/maximum-subarray-problem-kadane-algorithm/)

### MaxDoubleSliceSum

找到任何雙切片的最大總和。

### MaxProfit

最大利潤

給定股票價格記錄計算最大可能收益。

### MaxSliceSum

最大的切片和

找到數組元素的緊湊子序列的最大總和。

## L10

### prime and composite numbers 质数 和数

[https://zhidao.baidu.com/question/311766608](https://zhidao.baidu.com/question/311766608)

### MinPerimeterRectangle

最小周長矩形

找到面積等於N的任何矩形的最小周長。

### CountFactors

計數 因素

計算給定數量n的因子

### Flags

[旗幟](https://app.codility.com/demo/results/training97DBAE-GKV/#)

找到可以在山峰上設置的最大標誌數。

### Peaks

[峰值](https://app.codility.com/demo/results/training7GBTZ2-BNT/#)將數組劃分為相同大小的塊的最大數量，每個塊應包含索引P，使得A \[P-1\] &lt;A \[P\]&gt; A \[P + 1\]

## L11

### _sieve of Eratosthenes_  埃拉托斯特尼篩法

這是一種簡單且歷史悠久的[篩法](https://zh.wikipedia.org/wiki/%E7%AD%9B%E6%B3%95)，用來找出一定範圍內所有的[質數](https://zh.wikipedia.org/wiki/%E8%B3%AA%E6%95%B8)。

### CountSemiprimes

計數

半素數

計算給定範圍內的半素數\[a..b\]

### CountNonDivisible

計數

非

整除

計算不是每個元素的除數的數組的元素數。

## L12

### Euclidean algorithm [輾轉相除法](https://zh.wikipedia.org/zh-tw/%E8%BC%BE%E8%BD%89%E7%9B%B8%E9%99%A4%E6%B3%95)

是求最大公因數的算法

### ChocolatesByNumber

[巧克力比例](https://app.codility.com/demo/results/trainingSXZ3KT-MV4/#)

一圈中有N個巧克力。計算你要吃的巧克力數量。

### CommonPrimeDivisors

共同 主要 除數

檢查兩個數字是否具有相同的除數。

## L13

### [Fibonacci number](https://en.wikipedia.org/wiki/Fibonacci_number) 斐波那契數列

又譯為**菲波拿契數列**、**菲波那西數列**、**斐氏數列**、**黃金分割數列**。

在[數學](https://zh.wikipedia.org/wiki/%E6%95%B8%E5%AD%B8)上，**費氏數列**是以[遞迴](https://zh.wikipedia.org/wiki/%E9%80%92%E5%BD%92)的方法來定義

[https://zh.wikipedia.org/wiki/%E6%96%90%E6%B3%A2%E9%82%A3%E5%A5%91%E6%95%B0%E5%88%97](https://zh.wikipedia.org/wiki/%E6%96%90%E6%B3%A2%E9%82%A3%E5%A5%91%E6%95%B0%E5%88%97)

### FibFrog

計算青蛙到達河流另一側所需的最小跳躍次數。

### Ladder

[梯子](https://app.codility.com/demo/results/trainingZXRA9N-33G/#)計算攀爬到梯子頂部的不同攀爬方式的數量。

## L14

### Binary search algorithm [二分搜尋演算法](https://zh.wikipedia.org/zh-tw/%E4%BA%8C%E5%88%86%E6%90%9C%E7%B4%A2%E7%AE%97%E6%B3%95)

[MinMaxDivision](https://app.codility.com/demo/results/trainingHA5ZGD-H6G/#)將數組A劃分為K個塊並最小化任何塊的最大總和。

### NailingPlanks

釘

木板

[NailingPlanks](https://app.codility.com/demo/results/trainingEW83KS-F3M/#)計算允許釘入一系列木板的​​最小釘子數量。

## L15

### Caterpillar method

[http://shubo.io/caterpillar-method/](http://shubo.io/caterpillar-method/)

### AbsDistinct

阿布斯

不同

計算已排序數組元素的不同絕對值的數量。

### CountDistinctSlices

計數

不同

片

計算不同切片的數量（僅包含唯一數字）。

### CountTriangles

計數

三角形

[CountTriangles](https://app.codility.com/demo/results/trainingSREAJE-B4Q/#)計算可以從給定邊集構建的三角形的數量。

### MinAbsSumOfTwo

[MinAbsSumOfTwo](https://app.codility.com/demo/results/trainingPWSPFF-2X9/#)找到兩個元素之和的最小絕對值。

## L16

### Greedy algorithms 貪婪演算法

又稱貪心演算法，是一種在每一步選擇中都採取在目前狀態下最好或最佳（即最有利）的選擇，從而希望導致結果是最好或最佳的 ...

[https://zh.wikipedia.org/wiki/%E8%B4%AA%E5%BF%83%E7%AE%97%E6%B3%95](https://zh.wikipedia.org/wiki/%E8%B4%AA%E5%BF%83%E7%AE%97%E6%B3%95)

### MaxNonoverlappingSegments

找到一組最大的非重疊段。

### TieRopes

領帶 羅普斯

[TieRopes](https://app.codility.com/demo/results/trainingG9UHPH-8EX/#)綁定相鄰的繩索以獲得長度&gt; = K的最大繩索數量。

## L17

### Dynamic programming 動態編成

[https://zh.wikipedia.org/wiki/%E5%8A%A8%E6%80%81%E8%A7%84%E5%88%92](https://zh.wikipedia.org/wiki/%E5%8A%A8%E6%80%81%E8%A7%84%E5%88%92)

### NumberSolitaire

數

接龍

[NumberSolitaire](https://app.codility.com/demo/results/trainingXKAYAB-KGV/#)在給定的數組中，找到最大和的子集，其中連續元素之間的距離最多為6。

### MinAbsSum

阿布斯

和

[MinAbsSum](https://app.codility.com/demo/results/trainingQ7CF2B-9XJ/#)給定整數數組，找到元素的最低絕對和。

## ...

