# React Basis Version 16.13.1

## 總結

相對於 Vue 來說需要記憶的點確實蠻多的，但相對於 ng 來說又少一些，

但基本上都大同小異都是對於 databinding 、狀態響應、 ui component 的設計。

而因為很多既有的程式碼結構或資料結構會重複出現，所以就看 framework & cli 支援到哪種程度，又需要與其他多少東西相結合應用。

覺得最大的差異是 state 管理與資料都建議由上而下，而 vue 與 ng 則是自動響應與可以互傳。

## [Redux](https://zh.wikipedia.org/wiki/Redux_(JavaScript%E5%87%BD%E5%BC%8F%E5%BA%AB))

Redux一個用於應用程式狀態管理的開源JavaScript庫。Redux經常與React搭配運用，但其也可以獨立使用。

## [Next.js](https://ithelp.ithome.com.tw/articles/10190581)

## [Gatsby.js](https://ithelp.ithome.com.tw/articles/10201610)

## [安裝](https://zh-hant.reactjs.org/docs/getting-started.html)

* [Re-introduction JS](https://developer.mozilla.org/zh-TW/docs/Web/JavaScript/A_re-introduction_to_JavaScript)

[example](https://raw.githubusercontent.com/reactjs/reactjs.org/master/static/html/single-file-example.html)

JSX 部分需要用 babel 進行預處理，感覺有點像 angular ，

### [Pre JS Vs JSX](https://zh-hant.reactjs.org/docs/add-react-to-a-website.html#optional-try-react-with-jsx)

展示一個「Like」&lt;button>

* 純 JavaScript function call

```JavaScript
const e = React.createElement;

return e(
  'button',
  { onClick: () => this.setState({ liked: true }) },
  'Like'
);
```

* JSX

```JSX
<script src="https://unpkg.com/babel-standalone@6/babel.min.js"></script>

return (
  <button onClick={() => this.setState({ liked: true })}>
    Like
  </button>
);
```

## 小結1

感覺類似 Vue 也是[漸進式](https://medium.com/@gotraveltoworld/vue-js-%E4%BD%95%E8%AC%82%E6%BC%B8%E9%80%B2%E5%BC%8F%E6%A1%86%E6%9E%B6-7d0281a7efa9)的庫

## [整合的 toolchain](https://zh-hant.reactjs.org/docs/create-a-new-react-app.html)

* 擴大文件和 component 的規模。
* 使用來至 npm 的第三方函式庫。
* 偵測早期常見的錯誤。
* 實時在開發環境裡編輯 CSS 和 JS。
* 最佳化線上環境輸出。

### React 團隊主要推薦以下的方案：

* 如果你正在學習 React 或建立全新的 single-page 應用程式，請使用 [Create React App](https://zh-hant.reactjs.org/docs/create-a-new-react-app.html#create-react-app)。
* 如果你正在建立一個使用 Node.js 的 server-rendered 網頁，請使用 [Next.js](https://zh-hant.reactjs.org/docs/create-a-new-react-app.html#nextjs)。
* 如果你正在建立一個靜態內容的網頁，請使用 [Gatsby](https://zh-hant.reactjs.org/docs/create-a-new-react-app.html#gatsby)。
* 如果你正在建立一個 component 函式庫或與現存程式碼倉庫進行接軌，請使用[更靈活的 Toolchain](https://zh-hant.reactjs.org/docs/create-a-new-react-app.html#more-flexible-toolchains)。

### [Toolchain 通常包含](https://zh-hant.reactjs.org/docs/create-a-new-react-app.html#creating-a-toolchain-from-scratch)

一個 package 管理員，例如 Yarn 或 npm。它能讓你充分利用數量龐大的第三方 package，並且輕鬆的安裝或更新它們。

一個 bundler，例如 webpack 或 Parcel。它能讓你編寫模組化的程式碼，並將它們組合成小小的 package 以最佳化載入時間。

一個 compiler，例如 Babel。它能讓你編寫現代 JavaScript 程式碼，並可以在舊版本的瀏覽器裡使用。

### [為什麼要使用 crossorigin Attribute?](https://zh-hant.reactjs.org/docs/cdn-links.html#why-the-crossorigin-attribute)

```JavaScript
<script crossorigin src="..."></script>

//Headers
//Access-Control-Allow-Origin: *
```

## [主要概念](https://zh-hant.reactjs.org/docs/hello-world.html)

### Hellow World

[重新介紹 JavaScript](https://developer.mozilla.org/zh-TW/docs/Web/JavaScript/A_re-introduction_to_JavaScript)

## [介紹 JSX](https://zh-hant.reactjs.org/docs/introducing-jsx.html)

JSX : JavaScript 的語法擴充。

```JavaScript
const element = <h1>你好，世界！</h1>;
```

React 關注點分離的方法是將標籤語法寫在 JS 裡，其拆分為很多同時包含 UI 與邏輯的 component，而彼此之間很少互相依賴。

可以在 JSX 的大括號中寫入任何合法的 [JavaScript expression](https://developer.mozilla.org/en-US/docs/Web/JavaScript/Guide/Expressions_and_Operators#Expressions)

```JavaScript
const name = 'Josh Perez';
const element = <h1>Hello, {name}</h1>;

ReactDOM.render(
  element,
  document.getElementById('root')
);
```

建議將多行 JSX 包在括號中來避免遇到自動分號補足的麻煩

```JavaScript
function formatName(user) {
  return user.firstName+ ' ' + user.lastName;
}

const user = {
  firstName: 'Harper',
  lastName: 'Perez'
};

const element = (
  <h1>
    Hello, {formatName(user)}!
  </h1>
);

ReactDOM.render(
  element,
  document.getElementById('root')
);
```

編譯之後，JSX expressions 變成了一般的 JavaScript function 呼叫並回傳 JavaScript 物件。

可以在 if 跟 for 迴圈中使用 JSX，將其指定到一個變數，使用 JSX 作為參數並由 function 中回傳。

```JavaScript
function getGreeting(user) {
  if (user) {
    return <h1>Hello, {formatName(user)}!</h1>;
  }
  return <h1>Hello, Stranger.</h1>;
}

```

在 JSX 中使用引號將字串設定為屬性

```JavaScript
const element = <div tabIndex="0"></div>;
```

你也可以在屬性中使用大括號來嵌入一個 JavaScript expression：

```JavaScript
const element = <img src={user.avatarUrl}></img>;
```

在 JSX 中指定 Children

```JavaScript
const element = (
  <div>
    <h1>Hello!</h1>
    <h2>Good to see you here.</h2>
  </div>
);
```

### [JSX 防範注入攻擊](https://zh-hant.reactjs.org/docs/introducing-jsx.html#jsx-prevents-injection-attacks)

React DOM 預設會在 render 之前 escape 所有嵌入在 JSX 中的變數。

### JSX 表示物件

Babel 將 JSX 編譯為呼叫 React.createElement() 的程式，會進行一些檢查以幫助你寫出沒有 bug 的程式。

```JavaScript
const element = (
  <h1 className="greeting">
    Hello, World!
  </h1>
);

const element = React.createElement(
  'h1',
  {className: 'greeting'},
  'Hello, World!'
);
```

會產生類似下面的 React element 物件，React 會讀取，這些描述來產生 DOM 並保持他們在最新狀態。

```JavaScript
// 注意：這是簡化過的結構
const element = {
  type: 'h1',
  props: {
    className: 'greeting',
    children: 'Hello, world!'
  }
};
```

## [Render Element](https://zh-hant.reactjs.org/docs/rendering-elements.html)

建立 React 應用程式最小的單位是 element。

```JavaScript
const element = <h1>Hello, world</h1>;
```

與瀏覽器的 DOM element 不同，React element 是單純的 object，而且很容易被建立。React DOM 負責更新 DOM 來符合 React element。

```JavaScript
<div id="root"></div>
```

「root」DOM node 所有在內的 element 都會透過 React DOM 做管理。根據你的需求獨立出多個 root DOM node。

```JavaScript
const element = <h1>Hello, world</h1>;
ReactDOM.render(element, document.getElementById('root'));
```

### 如果要 render 一個 React element 到 root DOM node，傳入兩者到 ReactDOM.render()

### 更新被 Render 的 Element

React element 是 immutable 的。一旦你建立一個 element，你不能改變它的 children 或是 attribute。Element 就像是電影中的一個幀：它代表特定時間點的 UI。

更新 UI 唯一的方式是建立一個新的 element，並且將它傳入到 ReactDOM.render()。

```JavaScript
function tick() {
  const element = (
    <div>
      <h1>Hello, world!</h1>
      <h2>It is {new Date().toLocaleTimeString()}.</h2>
    </div>
  );
  ReactDOM.render(element, document.getElementById('root'));
}

setInterval(tick, 1000);
```

### React 只更新必要的 Element

React DOM 會將 element 和它的 children 與先前的狀態做比較，並且只更新必要的 DOM 達到理想的狀態。

## [Components 與 Props](https://zh-hant.reactjs.org/docs/components-and-props.html)

概念上來說，React component 就是 JavaScript function，接收任意的參數（稱之為「props」）並且回傳描述畫面的 React element。

也可以使用 ES6 Class 來定義 component：

```JavaScript
function Welcome(props) {
  return <h1>Hello, {props.name}</h1>;
}
////// Function Component 與 Class Component //////
class Welcome extends React.Component {
  render() {
    return <h1>Hello, {this.props.name}</h1>;
  }
}
```

### Render 一個 Component

```JavaScript
//<div id="root"></div>

function Welcome(props) {
  return <h1>Hello, {props.name}</h1>;
}

const element = <Welcome name="Sara" />;
ReactDOM.render(
  element,
  document.getElementById('root')
);
```

* 對 &lt;Welcome name="Sara" /> 這個 element 呼叫了 ReactDOM.render()。
* React 以 {name: 'Sara'} 作為 props 傳入 Welcome component 並呼叫。
* Welcome component 回傳了 &lt;h1>Hello, Sara&lt;/h1> 這個 element 作為返回值。
* React DOM 有效的將 DOM 更新為 &lt;h1>Hello, Sara&lt;/h1>。

當 React 看到由使用者定義 component 的 element 時，它將 JSX 屬性和 children 作為 single object 傳遞給該 component。我們稱這個 object 為「props」。

#### 注意： Component 的字首須為大寫字母

React 會將小寫字母開頭的組件視為原始 DOM 標籤，舉例來說，&lt;div /> 就會被視為是 HTML 的 div 標籤，但是 &lt;Welcome /> 則是一個 component，而且需要在作用域中使用 Welcome。

### 組合 Component

Component 可以在輸出中引用其他 component。我們可以在任何層次中抽象化相同的 component，按鈕、表單、對話框、甚至是整個畫面，在 React 應用程式中都將以 component 的方式呈現。

```JavaScript
//<div id="root"></div>

function Welcome(props) {
  return <h1>Hello, {props.name}</h1>;
}

function App() {
  return (
    <div>
      <Welcome name="Sara" />
      <Welcome name="Cahal" />
      <Welcome name="Edite" />
    </div>
  );
}

ReactDOM.render(
  <App />,
  document.getElementById('root')
);
```

通常來說，每個 React 應用程式都有一個最高層級的 App component。然而，如果你將 React 結合至現存的應用程式中，你可能需要使用像 Button 這樣的小型 component，並由下往上，逐步應用到畫面的最高層級。

### 抽離 Component

```JavaScript
function Comment(props) {
  return (
    <div className="Comment">
      <div className="UserInfo">
        <img className="Avatar"
          src={props.author.avatarUrl}
          alt={props.author.name}
        />
        <div className="UserInfo-name">
          {props.author.name}
        </div>
      </div>
      <div className="Comment-text">
        {props.text}
      </div>
      <div className="Comment-date">
        {formatDate(props.date)}
      </div>
    </div>
  );
}
```

它接受 author (一個物件)、text (一個字串)、還有 date (一個日期) 作為它的 props。它的作用是在一個社交網站上 render 一則評論。

這個 component 可能因為太多的巢狀關係而難以更動，而且也難以複用獨立的部分。讓我們把一些 component 從中分離吧。

建議從 component 的角度為 props 命名，而不是它的使用情境。

抽離後 :

```JavaScript
function formatDate(date) {
  return date.toLocaleDateString();
}

function Avatar(props) {
  return (
    <img
      className="Avatar"
      src={props.user.avatarUrl}
      alt={props.user.name}
    />
  );
}

function UserInfo(props) {
  return (
    <div className="UserInfo">
      <Avatar user={props.user} />
      <div className="UserInfo-name">{props.user.name}</div>
    </div>
  );
}

function Comment(props) {
  return (
    <div className="Comment">
      <UserInfo user={props.author} />
      <div className="Comment-text">{props.text}</div>
      <div className="Comment-date">
        {formatDate(props.date)}
      </div>
    </div>
  );
}

const comment = {
  date: new Date(),
  text: 'I hope you enjoy learning React!',
  author: {
    name: 'Hello Kitty',
    avatarUrl: 'https://placekitten.com/g/64/64',
  },
};
ReactDOM.render(
  <Comment
    date={comment.date}
    text={comment.text}
    author={comment.author}
  />,
  document.getElementById('root')
);
```

### [Props 是唯讀的](https://zh-hant.reactjs.org/docs/components-and-props.html#props-are-read-only)

不管你使用 function 或是 class 來宣告 component，都絕不能修改自己的 props。

所有的 React component 都必須像 Pure function 一般保護他的 props

```JavaScript
//Pure function
function sum(a, b) {
  return a + b;
}
//這個 withdraw function 並非 Pure function，因為它更改了它的參數：
function withdraw(account, amount) {
  account.total -= amount;
}
```

應用程式的 UI 是動態的，而且總是隨著時間改變。在下個章節，我們會介紹一個新的概念「state」。State 可以在不違反上述規則的前提下，讓 React component 隨使用者操作、網路回應、或是其他方式改變輸出內容。

## 小結2

總的來說 Props 唯獨不可修改 這裡跟 vue 與 ng 就有蠻大的差別性，

在猜應該是因為 react 是透過 ReactDOM.render() 去響應 DOM 與 Data 在與 state 去做變化，

而 ng 與 vue 似乎自動將這裡做在 core 了 ?

## [State 和生命週期](https://zh-hant.reactjs.org/docs/state-and-lifecycle.html)

ReactDOM.render();

```JavaScript
function tick() {
  const element = (
    <div>
      <h1>Hello, world!</h1>
      <h2>It is {new Date().toLocaleTimeString()}.</h2>
    </div>
  );
  ReactDOM.render(
    element,
    document.getElementById('root')
  );
}

setInterval(tick, 1000);
```

封裝 Clock component

```JavaScript
function Clock(props) {
  return (
    <div>
      <h1>Hello, world!</h1>
      <h2>It is {props.date.toLocaleTimeString()}.</h2>
    </div>
  );
}

function tick() {
  ReactDOM.render(
    <Clock date={new Date()} />,
    document.getElementById('root')
  );
}

setInterval(tick, 1000);
```

它缺少了一個重要的需求：Clock 設定 timer 並在每秒更新 UI 應該是 Clock 實作的細節的事實。

理想情況下，它會自己更新而不須注入新資料：

```JavaScript
ReactDOM.render(
  <Clock />,
  document.getElementById('root')
);
```

需要加入「state」到 Clock component。

### State 類似於 prop，但它是私有且由 component 完全控制的。

component 被定義為 class 有一些額外的特性。Local state 就是 class 其中的一個特性。

### 轉換 Function 成 Class

1. 建立一個相同名稱並且繼承 React.Component 的 ES6 class。
1. 加入一個 render() 的空方法。
1. 將 function 的內容搬到 render() 方法。
1. 將 render() 內的 props 替換成 this.props。
1. 刪除剩下空的 function 宣告。

```JavaScript
class Clock extends React.Component {
  render() {
    return (
      <div>
        <h1>Hello, world!</h1>
        <h2>It is {this.props.date.toLocaleTimeString()}.</h2>
      </div>
    );
  }
}
```

在每次發生更新時，render 方法都會被呼叫，但我們只要 render &lt;Clock /> 到相同的 DOM node 中，只有 Clock class 這個實例會被用到。這讓我們可以使用像是 local state 和生命週期方法這些額外的特性。

### 加入 Local State 到 Class

將 date 從搬移到 state：

```JavaScript
//將 render() 方法內的 this.props.date 替換成 this.state.date：
class Clock extends React.Component {
  render() {
    return (
      <div>
        <h1>Hello, world!</h1>
        <h2>It is {this.state.date.toLocaleTimeString()}.</h2>
      </div>
    );
  }
}
//加入一個 class constructor 並分配初始的 this.state：
class Clock extends React.Component {
  //注意，我們將傳送 props 到基礎 constructor：
  //Class component 應該總是要呼叫基礎 constructor 和 props。
  constructor(props) {
    super(props);
    this.state = {date: new Date()};
  }

  render() {
    return (
      <div>
        <h1>Hello, world!</h1>
        <h2>It is {this.state.date.toLocaleTimeString()}.</h2>
      </div>
    );
  }
}
//從 <Clock /> element 中移除 date prop：
```

把 timer 的程式碼加入到 component 本身。

```JavaScript
class Clock extends React.Component {
  constructor(props) {
    super(props);
    this.state = {date: new Date()};
  }

  render() {
    return (
      <div>
        <h1>Hello, world!</h1>
        <h2>It is {this.state.date.toLocaleTimeString()}.</h2>
      </div>
    );
  }
}

ReactDOM.render(
  <Clock />,
  document.getElementById('root')
);
```

但這樣雖然資料改為綁定 state 但渲染並不會自動響應。

必須

### 加入生命週期方法到 Class

讓 Clock component 設定它本身的 timer 並且每秒更新一次。

在具有許多 component 的應用程式中，當 component 被 destroy 時，釋放所佔用的資源是非常重要的。

每當 Clock render 到 DOM 的時候，我們想要設定一個 timer。在 React 中稱為「mount」。

每當產生的 Clock DOM 被移除時，我們想要清除 timer。在 React 中稱為「unmount」。

每當 component 在 mount 或是 unmount 的時候，我們可以在 component class 上宣告一些特別的方法來執行一些程式碼：

這些方法被稱為「生命週期方法」。

```JavaScript
class Clock extends React.Component {
  constructor(props) {
    super(props);
    this.state = {date: new Date()};
  }

  componentDidMount() {
  }

  componentWillUnmount() {
  }

  render() {
    return (
      <div>
        <h1>Hello, world!</h1>
        <h2>It is {this.state.date.toLocaleTimeString()}.</h2>
      </div>
    );
  }
}
```

componentDidMount() 方法會在 component 被 render 到 DOM 之後才會執行。這是設定 timer 的好地方：

```JavaScript
//雖然 this.props 是由 React 本身設定的，而且 this.state 具有特殊的意義，如果你需要儲存一些不相關於資料流的內容（像是 timer ID），你可以自由的手動加入。
componentDidMount() {
    this.timerID = setInterval(
      () => this.tick(),
      1000
    );
  }
//在 componentWillUnmount() 生命週期方法內移除 timer：
 componentWillUnmount() {
    clearInterval(this.timerID);
  }
```

實作一個 tick() 的方法，Clock component 將會在每秒執行它。

它將會使用 this.setState() 來安排 component local state 的更新：

```JavaScript
class Clock extends React.Component {
  constructor(props) {
    super(props);
    this.state = {date: new Date()};
  }

  componentDidMount() {
    this.timerID = setInterval(
      () => this.tick(),
      1000
    );
  }

  componentWillUnmount() {
    clearInterval(this.timerID);
  }

  tick() {
    this.setState({
      date: new Date()
    });
  }

  render() {
    return (
      <div>
        <h1>Hello, world!</h1>
        <h2>It is {this.state.date.toLocaleTimeString()}.</h2>
      </div>
    );
  }
}

ReactDOM.render(
  <Clock />,
  document.getElementById('root')
);
```

1. 當 &lt;Clock /> 被傳入到 ReactDOM.render() 時，React 會呼叫 Clock component 的constructor。由於 Clock 需要顯示目前的時間，它使用包含目前時間的 object 初始化 this.state。我們會在之後更新這個 state。
2. React 接著呼叫 Clock component 的 render() 方法。這就是 React 如何了解應該要在螢幕上顯示什麼內容。React 接著更新 DOM 來符合 Clock 的 render 輸出。
3. 每當 Clock 輸出被插入到 DOM 時，React 會呼叫 componentDidMount() 生命週期方法。在 Clock component 生命週期方法內，會要求瀏覽器設定 timer 每秒去呼叫 component 的 tick() 方法。
4. 瀏覽器每秒呼叫 tick() 方法。其中，Clock component 透過包含目前時間的 object 呼叫 setState() 來調度 UI 更新。感謝 setState()，React 現在知道 state 有所改變，並且再一次呼叫 render() 方法來了解哪些內容該呈現在螢幕上。這時候，在 render() 方法內的 this.state.date 將會有所不同，因此 render 輸出將會是更新的時間。React 相應地更新 DOM。
5. 如果 Clock component 從 DOM 被移除了，React 會呼叫 componentWillUnmount() 生命週期方法，所以 timer 會被停止

### [正確的使用 State](https://zh-hant.reactjs.org/docs/state-and-lifecycle.html#using-state-correctly)

```JavaScript
//請不要直接修改 State 這將不會重新 render component：
//唯一可以指定 this.state 值的地方是在 constructor。
// 錯誤
this.state.comment = 'Hello';
// 正確
this.setState({comment: 'Hello'});

//State 的更新可能是非同步的
//React 可以將多個 setState() 呼叫批次處理為單一的更新，以提高效能。
//因為 this.props 和 this.state 可能是非同步的被更新，你不應該依賴它們的值來計算新的 state。
//要修正這個問題，使用第二種形式的 setState()，它接受一個 function 而不是一個 object。Function 將接收先前的 state 作為第一個參數，並且將更新的 props 作為第二個參數
// 錯誤
this.setState({
  counter: this.state.counter + this.props.increment,
});
// 正確
this.setState((state, props) => ({
  counter: state.counter + props.increment
}));
// 正確
this.setState(function(state, props) {
  return {
    counter: state.counter + props.increment
  };
});

//State 的更新將會被 Merge
//當你呼叫 setState() 時，React 會 merge 你提供的 object 到目前的 state。
  constructor(props) {
    super(props);
    this.state = {
      posts: [],
      comments: []
    };
  }
//可以單獨的呼叫 setState() 更新它們：
//這個 merge 是 淺拷貝 的，所以 this.setState({comments}) 保持 this.state.posts 的完整，但它完全取代了 this.state.comments。
 componentDidMount() {
    fetchPosts().then(response => {
      this.setState({
        posts: response.posts
      });
    });

    fetchComments().then(response => {
      this.setState({
        comments: response.comments
      });
    });
  }
```

### [向下資料流](https://zh-hant.reactjs.org/docs/state-and-lifecycle.html#the-data-flows-down)

Parent 和 child component 不會知道某個 component 是 stateful 或 stateless 的 component，而且它們不在意它是透過 function 或是 class 被定義的。

這就是 state 通常被稱為 local state 或被封裝的原因。除了擁有和可以設定它之外的任何 component 都不能訪問它。

#### Component 可以選擇將它的 state 做為 props 往下傳遞到它的 child component：

```JavaScript
<h2>It is {this.state.date.toLocaleTimeString()}.</h2>

<FormattedDate date={this.state.date} />
```

FormattedDate component 會在它的 props 接收到 date，但他不知道它是從 Clock 的 state 傳遞過來的，從 Clock 的 props 或者是透過手動輸入：

```JavaScript
function FormattedDate(props) {
  return <h2>It is {props.date.toLocaleTimeString()}.</h2>;
}
```

這通常被稱作為「上至下」或「單向」的資料流。任何 state 總是由某個特地的 component 所擁有，任何從 state 得到的資料或 UI，state 只能影響在 tree「以下」的 component。

如果你想像一個 component tree 是一個 props 的瀑布，每個 component 的 state 像是一個額外的水流源頭，它在任意的某個地方而且往下流。

為了表示所有 component 真的都是被獨立的，我們可以建立一個 App component 來 render 三個 <Clock>：

```JavaScript
function App() {
  return (
    <div>
      <Clock />
      <Clock />
      <Clock />
    </div>
  );
}

ReactDOM.render(
  <App />,
  document.getElementById('root')
);
```

每個 Clock 設定它本身的 timer 並獨立的更新。

## 小結3

從這裡來看就有點清楚了，大概是原先不是用 es6 class 方法所以資料有點像是在 react dom 之外的感覺，而多了 state 的特性，

就像是 ng ts 檔內的 export class 內的 data，就像是 vue 內的 methods 的感覺。

而生命週期的部分 ng 與 vue 似乎是自動偵測資料是否有變動有的話則自動更新渲染。

但 `如果 Clock component 從 DOM 被移除了，React 會呼叫 componentWillUnmount() 生命週期方法，所以 timer 會被停止` 這裡有點不太懂何時 DOM 會被移除...也不講清楚

這句也講不太清楚 `這個 merge 是 淺拷貝 的，所以 this.setState({comments}) 保持 this.state.posts 的完整，但它完全取代了 this.state.comments。`

目前是認定 [一個週期內的多次更新會被批量一起更新](https://codertw.com/ios/20359/)。

重點之一 React Component 可以選擇將它的 state 做為 props 往下傳遞到它的 child component：

React 不像 ng 用 databinding 或是 @inputOuput 之類的來在 Component 上下傳遞資料，

也不像 vue 是用 $event 之類的傳遞資料。

## [事件處理](https://zh-hant.reactjs.org/docs/handling-events.html)

事件的名稱在 React 中都是 camelCase，而在 HTML DOM 中則是小寫。

事件的值在 JSX 中是一個 function，而在 HTML DOM 中則是一個 string。

```JavaScript
//在 HTML 中的語法：
<button onclick="activateLasers()">
  Activate Lasers
</button>
//和在 React 中的語法有些微的不同：
<button onClick={activateLasers}>
  Activate Lasers
</button>
```

在 React 中，你不能夠在像在 HTML DOM 中使用 return false 來避免瀏覽器預設行為。

必須明確地呼叫 preventDefault。

```JavaScript
//在 HTML 中的語法：
<a href="#" onclick="console.log('The link was clicked.'); return false">
  Click me
</a>
//在 React 中，你則可以這樣寫：
function ActionLink() {
  function handleClick(e) {
    e.preventDefault();
    console.log('The link was clicked.');
  }

  return (
    <a href="#" onClick={handleClick}>
      Click me
    </a>
  );
}
```

在這裡，e 是一個綜合事件（synthetic event）。React 根據 W3C 規範來定義這些綜合事件，所以，你不需要煩惱跨瀏覽器相容性（cross-browser compatibility）的問題。若想了解更多這方面的資訊，請參考 SyntheticEvent。

當使用 React 時，你不需要在建立一個 DOM element 後再使用 addEventListener 來加上 listener。你只需要在這個 element 剛開始被 render 時就提供一個 listener。

當你使用 ES6 class 來定義 Component 時，常見的慣例是把 event handler 當成那個 class 的方法。例如，這個 Toggle Component 會 render 一個按鈕，讓使用者可以轉換 state 中的「開」與「關」：

```JavaScript
class Toggle extends React.Component {
  constructor(props) {
    super(props);
    this.state = {isToggleOn: true};

    // 為了讓 `this` 能在 callback 中被使用，這裡的綁定是必要的：
    this.handleClick = this.handleClick.bind(this);
  }

  handleClick() {
    this.setState(state => ({
      isToggleOn: !state.isToggleOn
    }));
  }

  render() {
    return (
      <button onClick={this.handleClick}>
        {this.state.isToggleOn ? 'ON' : 'OFF'}
      </button>
    );
  }
}

ReactDOM.render(
  <Toggle />,
  document.getElementById('root')
);
```

### [callback](https://developer.mozilla.org/zh-TW/docs/Glossary/Callback_function)

回呼函式（callback function）是指能藉由參數（argument）通往另一個函式的函式。它會在外部函式內調用、以完成某些事情。

### [Function.prototype.bind()](https://developer.mozilla.org/zh-TW/docs/Web/JavaScript/Reference/Global_Objects/Function/bind)

bind() 方法，會建立一個新函式。該函式被呼叫時，會將 this 關鍵字設為給定的參數，並在呼叫時，帶有提供之前，給定順序的參數。

---------------------------------------------------

請特別注意 this 在 JSX callback 中的意義。在 JavaScript 中，class 的方法在預設上是沒有被綁定（bound）的。如果你忘了綁定 this.handleClick 並把它傳遞給 onClick 的話，this 的值將會在該 function 被呼叫時變成 undefined。

這並非是 React 才有的行為，而是 function 在 JavaScript 中的運作模式。總之，當你使用一個方法，卻沒有在後面加上 () 之時（例如當你使用 onClick={this.handleClick} 時），你應該要綁定這個方法。

如果呼叫 bind 對你來說很麻煩的話，你可以用別的方式。如果你使用了還在測試中的 class fields 語法的話，你可以用 class field 正確的綁定 callback：

```JavaScript
class LoggingButton extends React.Component {
  // 這個語法確保 `this` 是在 handleClick 中被綁定：
  // 警告：這是一個還在*測試中*的語法：
  handleClick = () => {
    console.log('this is:', this);
  }

  render() {
    return (
      <button onClick={this.handleClick}>
        Click me
      </button>
    );
  }
}
```

如果你並沒有使用 class field 的語法的話，你則可以在 callback 中使用 arrow function：

```JavaScript
class LoggingButton extends React.Component {
  handleClick() {
    console.log('this is:', this);
  }

  render() {
    // 這個語法確保 `this` 是在 handleClick 中被綁定：
    return (
      <button onClick={() => this.handleClick()}>
        Click me
      </button>
    );
  }
}
```

這個語法的問題是每一次 LoggingButton render 的時候，就會建立一個不同的 callback。大多時候，這是無所謂的。然而，如果這個 callback 被當作一個 prop 傳給下層的 component 的話，其他的 component 也許會做些多餘的 re-render。原則上來說，我們建議在 constructor 內綁定，或使用 class field 語法，以避免這類的性能問題。

### 將參數傳給 Event Handler

在一個迴圈中，我們常常會需要傳遞一個額外的參數給 event handler。例如，如果 id 是每一行的 ID 的話，下面兩種語法都可行：

```JavaScript
<button onClick={(e) => this.deleteRow(id, e)}>Delete Row</button>
<button onClick={this.deleteRow.bind(this, id)}>Delete Row</button>
```

以這兩個例子來說，e 這個參數所代表的 React 事件將會被當作 ID 之後的第二個參數被傳遞下去。在使用 arrow function 時，我們必須明確地將它傳遞下去，但若使用 bind 語法，未來任何的參數都將會自動被傳遞下去。

## [條件 Render](https://zh-hant.reactjs.org/docs/conditional-rendering.html)

React 中的條件 rendering 跟 JavaScript 一致。使用 JavaScript 中的運算子如 if 或者 三元運算子 來建立表示目前 state 的 element，然後讓 React 根據它們來更新 UI。

```JavaScript
function UserGreeting(props) {
  return <h1>Welcome back!</h1>;
}

function GuestGreeting(props) {
  return <h1>Please sign up.</h1>;
}

function Greeting(props) {
  const isLoggedIn = props.isLoggedIn;
  if (isLoggedIn) {
    return <UserGreeting />;
  }
  return <GuestGreeting />;
}

ReactDOM.render(
  // Try changing to isLoggedIn={true}:
  <Greeting isLoggedIn={false} />,
  document.getElementById('root')
);
```

stateful component

```JavaScript
class LoginControl extends React.Component {
  constructor(props) {
    super(props);
    this.handleLoginClick = this.handleLoginClick.bind(this);
    this.handleLogoutClick = this.handleLogoutClick.bind(this);
    this.state = {isLoggedIn: false};
  }

  handleLoginClick() {
    this.setState({isLoggedIn: true});
  }

  handleLogoutClick() {
    this.setState({isLoggedIn: false});
  }

  render() {
    const isLoggedIn = this.state.isLoggedIn;
    let button;

    if (isLoggedIn) {
      button = <LogoutButton onClick={this.handleLogoutClick} />;
    } else {
      button = <LoginButton onClick={this.handleLoginClick} />;
    }

    return (
      <div>
        <Greeting isLoggedIn={isLoggedIn} />
        {button}
      </div>
    );
  }
}

function UserGreeting(props) {
  return <h1>Welcome back!</h1>;
}

function GuestGreeting(props) {
  return <h1>Please sign up.</h1>;
}

function Greeting(props) {
  const isLoggedIn = props.isLoggedIn;
  if (isLoggedIn) {
    return <UserGreeting />;
  }
  return <GuestGreeting />;
}

function LoginButton(props) {
  return (
    <button onClick={props.onClick}>
      Login
    </button>
  );
}

function LogoutButton(props) {
  return (
    <button onClick={props.onClick}>
      Logout
    </button>
  );
}

ReactDOM.render(
  <LoginControl />,
  document.getElementById('root')
);

```

### [Inline If 與 && 邏輯運算子](https://zh-hant.reactjs.org/docs/conditional-rendering.html#inline-if-with-logical--operator)

```JavaScript
function Mailbox(props) {
  const unreadMessages = props.unreadMessages;
  return (
    <div>
      <h1>Hello!</h1>
      {unreadMessages.length > 0 &&
        <h2>
          You have {unreadMessages.length} unread messages.
        </h2>
      }
    </div>
  );
}

const messages = ['React', 'Re: React', 'Re:Re: React'];
ReactDOM.render(
  <Mailbox unreadMessages={messages} />,
  document.getElementById('root')
);

```

能夠這樣做是因為在 JavaScript 中，true && expression 總是回傳 expression ，而 false && expression 總是回傳 false。

所以，當條件為 true 時，&& 右側的 element 會出現在輸出中，如果是 false，React 會忽略並跳過它。

### Inline If-Else 與三元運算子

```JavaScript
render() {
  const isLoggedIn = this.state.isLoggedIn;
  return (
    <div>
      The user is <b>{isLoggedIn ? 'currently' : 'not'}</b> logged in.
    </div>
  );
}

render() {
  const isLoggedIn = this.state.isLoggedIn;
  return (
    <div>
      {isLoggedIn
        ? <LogoutButton onClick={this.handleLogoutClick} />
        : <LoginButton onClick={this.handleLoginClick} />
      }
    </div>
  );
}
```

### 防止 Component Render

在少數的情況下，你可能希望 component 隱藏自己本身，即便它是由另一個 component 被 render。可以透過回傳 null 而不是它的 render 輸出。

```JavaScript
function WarningBanner(props) {
  if (!props.warn) {
    return null;
  }

  return (
    <div className="warning">
      Warning!
    </div>
  );
}

class Page extends React.Component {
  constructor(props) {
    super(props);
    this.state = {showWarning: true}
    this.handleToggleClick = this.handleToggleClick.bind(this);
  }

  handleToggleClick() {
    this.setState(prevState => ({
      showWarning: !prevState.showWarning
    }));
  }

  render() {
    return (
      <div>
        <WarningBanner warn={this.state.showWarning} />
        <button onClick={this.handleToggleClick}>
          {this.state.showWarning ? 'Hide' : 'Show'}
        </button>
      </div>
    );
  }
}

ReactDOM.render(
  <Page />,
  document.getElementById('root')
);
```

## [列表與 Key](https://zh-hant.reactjs.org/docs/lists-and-keys.html)

使用 map() function 來接收 numbers array，並將其中的每個值乘以兩倍。我們將 map() 回傳的新 array 設定為變數 doubled 的值並印出：

在 React 中，將 array 轉變成 element 列表幾乎是一樣的方式。

### Render 多個 Component

```JavaScript
const numbers = [1, 2, 3, 4, 5];
const doubled = numbers.map((number) => number * 2);
console.log(doubled);
//[2, 4, 6, 8, 10]

//當你執行這段程式碼時，你會收到一個關於你應該提供 key 給每一個列表項目的警告。「key」是當你在建立一個 element 列表時必須使用的特殊的 string attribute。
function NumberList(props) {
  const numbers = props.numbers;
  const listItems = numbers.map((number) =>
    <li key={number.toString()}>
      {number}
    </li>
  );
  return (
    <ul>{listItems}</ul>
  );
}

const numbers = [1, 2, 3, 4, 5];
ReactDOM.render(
  <NumberList numbers={numbers} />,
  document.getElementById('root')
);

```

### Key

Key 幫助 React 分辨哪些項目被改變、增加或刪除。在 array 裡面的每個 element 都應該要有一個 key，如此才能給予每個 element 一個固定的身份：

```JavaScript
//選擇 key 最佳的方法是在列表中使用唯一識別字串來區別 sibling 項目。通常，你會使用資料的 ID 作為 key：
const todoItems = todos.map((todo) =>
  <li key={todo.id}>
    {todo.text}
  </li>
);
//當你 render 的項目沒有固定的 ID 且你也沒有更好的辦法時，你可以使用項目的索引做為 key：
const todoItems = todos.map((todo, index) =>
  // 請在項目沒有固定的 ID 時才這樣做
  <li key={index}>
    {todo.text}
  </li>
);
```

我們並不建議你使用索引作為 key，尤其如果項目的順序會改變的話。這會對效能產生不好的影響，也可能會讓 component state 產生問題。

### [用 Key 抽離 Component](https://zh-hant.reactjs.org/docs/lists-and-keys.html#extracting-components-with-keys)

Key 只有在周遭有 array 的情境中才有意義。

例如，如果你要抽離一個 ListItem component 的話，你應該把 key 放在 array 裡的 &lt;ListItem /> element 上，而不是把它放在 ListItem 裡面的 &lt;li> element 上。

```JavaScript
//範例：Key 的錯誤使用方式
function ListItem(props) {
  const value = props.value;
  return (
    // 錯！你不需要在這裡指出 key：
    <li key={value.toString()}>
      {value}
    </li>
  );
}

function NumberList(props) {
  const numbers = props.numbers;
  const listItems = numbers.map((number) =>
    // 錯！你應該要在這裡指出 key：
    <ListItem value={number} />
  );
  return (
    <ul>
      {listItems}
    </ul>
  );
}

const numbers = [1, 2, 3, 4, 5];
ReactDOM.render(
  <NumberList numbers={numbers} />,
  document.getElementById('root')
);
//範例：Key 的正確使用方式
function ListItem(props) {
  // 正確！你不需要在這裡指出 key：
  return <li>{props.value}</li>;
}

function NumberList(props) {
  const numbers = props.numbers;
  const listItems = numbers.map((number) =>
    // 正確！你需要在 array 裡指出 key：
    <ListItem key={number.toString()}              value={number} />

  );
  return (
    <ul>
      {listItems}
    </ul>
  );
}

const numbers = [1, 2, 3, 4, 5];
ReactDOM.render(
  <NumberList numbers={numbers} />,
  document.getElementById('root')
);
```

一個好的經驗法則是，在 map() 呼叫中的每個 element 都會需要 key。

### Key 必須在 Sibling 中是唯一的

在 array 中使用的 key 應該要是唯一的值。然而，它們不必在全域中唯一。當我們產生兩個不同的 array 時，我們仍然可以使用相同的 key

```JavaScript
function Blog(props) {
  const sidebar = (
    <ul>
      {props.posts.map((post) =>
        <li key={post.id}>
          {post.title}
        </li>
      )}
    </ul>
  );
  const content = props.posts.map((post) =>
    <div key={post.id}>
      <h3>{post.title}</h3>
      <p>{post.content}</p>
    </div>
  );
  return (
    <div>
      {sidebar}
      <hr />
      {content}
    </div>
  );
}

const posts = [
  {id: 1, title: 'Hello World', content: 'Welcome to learning React!'},
  {id: 2, title: 'Installation', content: 'You can install React from npm.'}
];
ReactDOM.render(
  <Blog posts={posts} />,
  document.getElementById('root')
);
```

Key 的功能是提示 React，但它們不會被傳遞到你的 component。如果你在 component 中需要同樣的值，你可以直接把這個值用一個不同的名稱作為 prop 傳下去：

```JavaScript
const content = posts.map((post) =>
  <Post
    key={post.id}
    id={post.id}
    title={post.title} />
);
//在上面的例子中，Post component 可以讀取 props.id，但不能讀取 props.key。
```

### 在 JSX 中嵌入 map()

```JavaScript
function NumberList(props) {
  const numbers = props.numbers;
  const listItems = numbers.map((number) =>
    <ListItem key={number.toString()}
              value={number} />
  );
  return (
    <ul>
      {listItems}
    </ul>
  );
}
//JSX 讓你在大括號中嵌入任何表達式，所以我們能夠 inline map() 的結果：
function NumberList(props) {
  const numbers = props.numbers;
  return (
    <ul>
      {numbers.map((number) =>
        <ListItem key={number.toString()}
                  value={number} />
      )}
    </ul>
  );
}
```

## 小結4

以往 ng 與 vue 都是在 element 上用特殊屬性做條件與迴圈渲染，

react 條件與迴圈渲染比較像在，良興時接觸到的類似字串的 innerHTML 渲染，但又不像組文字 element 串那樣直覺，而是要接觸陣列與 js map。

key 的部分蠻像 Vue 的 id key 的應該是有借鑑...

## [表單](https://zh-hant.reactjs.org/docs/forms.html)

HTML 表單的 element 和 React 中其他的 DOM element 不太一樣，因為表單的 element 很自然地有一些內部的 state。

```JavaScript
<form>
  <label>
    Name:
    <input type="text" name="name" />
  </label>
  <input type="submit" value="Submit" />
</form>
```

當使用者提交表單時，此表單具有瀏覽到新頁面的預設 HTML 表單行為。如果你想要在 React 中也有這樣的行為的話，直接用 HTML 是可行的。但是在大多數的情況中，有一個 JavaScript function 來處理提交表單的功能並讀取使用者在表單中填入的資料是很方便的。要做到這樣，標準的方法是使用「controlled component」：

### Controlled Component

在 HTML 中，表單的 element 像是 &lt;input>、&lt;textarea> 和 &lt;select> 通常會維持它們自身的 state，並根據使用者的輸入來更新 state。在 React 中，可變的 state 通常是被維持在 component 中的 state property，並只能以 setState() 來更新。

我們可以透過將 React 的 state 變成「唯一真相來源」來將這兩者結合。如此，render 表單的 React component 同時也掌握了後續使用者的輸入對表單帶來的改變。像這樣一個輸入表單的 element，被 React 用這樣的方式來控制它的值，就被稱為「controlled component」。

```JavaScript
class NameForm extends React.Component {
  constructor(props) {
    super(props);
    this.state = {value: ''};

    this.handleChange = this.handleChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  handleChange(event) {
    this.setState({value: event.target.value});
  }

  handleSubmit(event) {
    alert('A name was submitted: ' + this.state.value);
    event.preventDefault();
  }

  render() {
    return (
      <form onSubmit={this.handleSubmit}>
        <label>
          Name:
          <input type="text" value={this.state.value} onChange={this.handleChange} />
        </label>
        <input type="submit" value="Submit" />
      </form>
    );
  }
}
```

由於 value attribute 是被設定在我們的表單 element 上，顯示的 value 會永遠是 this.state.value，這使得 React 的 state 成為了資料來源。由於 handleChange 在每一次鍵盤被敲擊時都會被執行，並更新 React 的 state，因此被顯示的 value 將會在使用者打字的同時被更新。

在這樣的 controlled component 中，顯示的 value 始終由 React 的 state 驅動，雖然這意味著你必須寫更多的 code，但現在你同時可以將 value 傳遞給其他的 UI element，或是從其他 event handler 重置。

### Textarea 標籤

```JavaScript
<textarea>
  Hello there, this is some text in a text area
</textarea>
//在 React 中，<textarea> 則是使用一個 value 的 attribute。如此一來，一個使用 <textarea> 的表單可以使用非常類似單行的 input 方法來寫成：
class EssayForm extends React.Component {
  //請注意 this.state.value 是在 constructor 內被初始化的，所以上述的 text area 一開始會有一些文字。
  constructor(props) {
    super(props);
    this.state = {
      value: 'Please write an essay about your favorite DOM element.'
    };

    this.handleChange = this.handleChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  handleChange(event) {
    this.setState({value: event.target.value});
  }

  handleSubmit(event) {
    alert('An essay was submitted: ' + this.state.value);
    event.preventDefault();
  }

  render() {
    return (
      <form onSubmit={this.handleSubmit}>
        <label>
          Essay:
          <textarea value={this.state.value} onChange={this.handleChange} />
        </label>
        <input type="submit" value="Submit" />
      </form>
    );
  }
}
```

### Select 標籤

```JavaScript
<select>
  <option value="grapefruit">Grapefruit</option>
  <option value="lime">Lime</option>
  <option selected value="coconut">Coconut</option>
  <option value="mango">Mango</option>
</select>
//請注意在這裡，椰子的選項是一開始就被選定的，因為它有一個 selected attribute。但是在 React 中並不是用 selected attribute，而是在 select 的標籤上用一個 value attribute。對一個 controlled component 來說這是比較方便的，因為你只需要在一個地方更新它。例如：
class FlavorForm extends React.Component {
  constructor(props) {
    super(props);
    this.state = {value: 'coconut'};

    this.handleChange = this.handleChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  handleChange(event) {
    this.setState({value: event.target.value});
  }

  handleSubmit(event) {
    alert('Your favorite flavor is: ' + this.state.value);
    event.preventDefault();
  }

  render() {
    return (
      <form onSubmit={this.handleSubmit}>
        <label>
          Pick your favorite flavor:
          <select value={this.state.value} onChange={this.handleChange}>
            <option value="grapefruit">Grapefruit</option>
            <option value="lime">Lime</option>
            <option value="coconut">Coconut</option>
            <option value="mango">Mango</option>
          </select>
        </label>
        <input type="submit" value="Submit" />
      </form>
    );
  }
}
//你可以將一個 array 傳給 value 這個 attribute，這使得你可以在一個 select 中選取多個選項：
```

### 檔案 input 標籤

```JavaScript
//在 HTML 中，<input type="file"> 讓使用者從它們的儲存裝置中選擇一個至多個檔案，並把它們上傳到伺服器或透過 File API 被 JavaScript 處理。

<input type="file" />

//由於它的值是唯讀，它在 React 中是一個 uncontrolled component。在稍後的文件中有其他關於它和其他 uncontrolled component 的討論。
```

### 處理多個輸入

```JavaScript
//當你需要處理多個 controlled input element，你可以在每個 element 中加入一個 name attribute，並讓 handler function 選擇基於 event.target.name 的值該怎麼做：
class Reservation extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      isGoing: true,
      numberOfGuests: 2
    };

    this.handleInputChange = this.handleInputChange.bind(this);
  }

  handleInputChange(event) {
    const target = event.target;
    const value = target.name === 'isGoing' ? target.checked : target.value;
    const name = target.name;
//注意我們使用了 ES6 的 computed property name 語法來更新與輸入中的 name 相對應的 state key：
    this.setState({
      [name]: value
    });
//這和下面的 ES5 程式碼是ㄧ樣的：
//var partialState = {};
//partialState[name] = value;
//this.setState(partialState);
  }

  render() {
    return (
      <form>
        <label>
          Is going:
          <input
            name="isGoing"
            type="checkbox"
            checked={this.state.isGoing}
            onChange={this.handleInputChange} />
        </label>
        <br />
        <label>
          Number of guests:
          <input
            name="numberOfGuests"
            type="number"
            value={this.state.numberOfGuests}
            onChange={this.handleInputChange} />
        </label>
      </form>
    );
  }
}
```

### Controlled 輸入值為 Null

在一個 controlled component 上指明 value prop 可避免使用者改變輸入，除非你希望使用者這樣做。如果你已經指明了 value 但輸入仍然是可以被修改的，你很可能是不小心將 value 的值設定為 undefined 或 null。

```JavaScript
ReactDOM.render(<input value="hi" />, mountNode);

setTimeout(function() {
  ReactDOM.render(<input value={null} />, mountNode);
}, 1000);
```

### Controlled component 的替代方案

有時候使用 controlled component 是很乏味的，因為你需要為每一個資訊可以改變的方式寫一個 event handler，並將所有的輸入 state 透過一個 React component 來傳遞。這在你將一個舊的 codebase 改寫成 React 時或將一個 React 的應用程式與一個非 React 的函式庫整合時會變得特別麻煩。在這種情況中，你也許會想參考 uncontrolled component，也就是另一種取代輸入表格的方式。

### 成熟的解決方案

如果你想找出一個完整的、包含驗證、可追蹤拜訪欄位並能處理提交表單等功能的解決方案，Formik 是一個很熱門的選擇。然而，它是在與 controlled component 和維持 state 相同的原則上所建立的，所以別忘了學習它。

## 小結5

看到這裡覺得 react 編寫時較複雜，也會有不同於原js或其他框架的實作概念，也有更多 es6 7 8 9 10 之類的編譯語法，

不太確定是為了秀技術還是...，像人家 ng 至少 ts 就好寫很多，

但總體都是以 component 為核心思想下去思考，

所以應該是編寫一次元件後續就只剩 css 上的變化，或是 component 的變形之類。

是屬於寫一次用很多次的感覺的框架。

## [提升 State](https://zh-hant.reactjs.org/docs/lifting-state-up.html)

通常來說，有一些 component 需要反映相同的資料變化。我們建議將共享的 state 提升到最靠近它們的共同 ancestor。

```JavaScript
const scaleNames = {
  c: 'Celsius',
  f: 'Fahrenheit'
};

class TemperatureInput extends React.Component {
  constructor(props) {
    super(props);
    this.handleChange = this.handleChange.bind(this);
    this.state = {temperature: ''};
  }

  handleChange(e) {
    this.setState({temperature: e.target.value});
  }

  render() {
    const temperature = this.state.temperature;
    const scale = this.props.scale;
    return (
      <fieldset>
        <legend>Enter temperature in {scaleNames[scale]}:</legend>
        <input value={temperature}
               onChange={this.handleChange} />
      </fieldset>
    );
  }
}

class Calculator extends React.Component {
  render() {
    return (
      <div>
        <TemperatureInput scale="c" />
        <TemperatureInput scale="f" />
      </div>
    );
  }
}

ReactDOM.render(
  <Calculator />,
  document.getElementById('root')
);
```

現在我們有兩個輸入，但是當你輸入其中一個溫度輸入時，另外一個輸入並沒有更新。這和我們的需求產生了矛盾：我們希望它們可以保持同步。

我們也無法從 Calculator 顯示 BoilingVerdict。Calculator 並不知道目前的溫度，因為它被隱藏在 TemperatureInput 內。

### [撰寫轉換 Function](https://zh-hant.reactjs.org/docs/lifting-state-up.html#writing-conversion-functions)

### 提升 Lifting State Up

在 React 中，共享 state 是透過將 state 搬移到需要它的 component 共同最近的 ancestor 來完成的。這被稱為「提升 state」。我們將從 TemperatureInput 移除 local state 並且搬移它到 Calculator。

如果 Calculator 擁有共享 state，它將成為目前兩個溫度輸入的「真相來源」。這可以說明它們兩者具有一致的值。由於這兩個 TemperatureInput component 的 prop 都是來自相同的 Calculator parent component，所以這兩個輸入會彼此同步。

首先，我們將會把 TemperatureInput component 的 this.state.temperature 替換為 this.props.temperature。現在，讓我們假設 this.props.temperature 已經存在，雖然我們之後需要從 Calculator 傳遞它：

我們知道 prop 是唯讀的。當 temperature 在 local state 時，TemperatureInput 可以呼叫 this.setState() 來改變它。然而，現在 temperature prop 是來自它的 parent，TemperatureInput 無法控制它。

在 React 中，這通常透過讓 component「被控制」來解決。就像 DOM &lt;input> 同時接受 value 和 onChange prop，所以可以自訂 TemperatureInput 同時接受來自 Calculator parent component 的 temperature 和 onTemperatureChange prop。

```JavaScript
const scaleNames = {
  c: 'Celsius',
  f: 'Fahrenheit'
};

function toCelsius(fahrenheit) {
  return (fahrenheit - 32) * 5 / 9;
}

function toFahrenheit(celsius) {
  return (celsius * 9 / 5) + 32;
}

function tryConvert(temperature, convert) {
  const input = parseFloat(temperature);
  if (Number.isNaN(input)) {
    return '';
  }
  const output = convert(input);
  const rounded = Math.round(output * 1000) / 1000;
  return rounded.toString();
}

function BoilingVerdict(props) {
  if (props.celsius >= 100) {
    return <p>The water would boil.</p>;
  }
  return <p>The water would not boil.</p>;
}

class TemperatureInput extends React.Component {
  constructor(props) {
    super(props);
    this.handleChange = this.handleChange.bind(this);
  }

  handleChange(e) {
    this.props.onTemperatureChange(e.target.value);
  }

  render() {
    const temperature = this.props.temperature;
    const scale = this.props.scale;
    return (
      <fieldset>
        <legend>Enter temperature in {scaleNames[scale]}:</legend>
        <input value={temperature}
               onChange={this.handleChange} />
      </fieldset>
    );
  }
}

class Calculator extends React.Component {
  constructor(props) {
    super(props);
    this.handleCelsiusChange = this.handleCelsiusChange.bind(this);
    this.handleFahrenheitChange = this.handleFahrenheitChange.bind(this);
    this.state = {temperature: '', scale: 'c'};
  }

  handleCelsiusChange(temperature) {
    this.setState({scale: 'c', temperature});
  }

  handleFahrenheitChange(temperature) {
    this.setState({scale: 'f', temperature});
  }

  render() {
    const scale = this.state.scale;
    const temperature = this.state.temperature;
    const celsius = scale === 'f' ? tryConvert(temperature, toCelsius) : temperature;
    const fahrenheit = scale === 'c' ? tryConvert(temperature, toFahrenheit) : temperature;

    return (
      <div>
        <TemperatureInput
          scale="c"
          temperature={celsius}
          onTemperatureChange={this.handleCelsiusChange} />
        <TemperatureInput
          scale="f"
          temperature={fahrenheit}
          onTemperatureChange={this.handleFahrenheitChange} />
        <BoilingVerdict
          celsius={parseFloat(celsius)} />
      </div>
    );
  }
}

ReactDOM.render(
  <Calculator />,
  document.getElementById('root')
);

```

* React 在 DOM &lt;input> 上呼叫被指定為 onChange 的函式。在我們的範例中，這是在 TemperatureInput component 內的 handleChange 方法。
* 在 TemperatureInput component 的 handleChange 方法呼叫 this.props.onTemperatureChange() 與新的期望值。它的 prop 包含 onTemperatureChange，是由 Calculator parent component 所提供的。
* 當它被 render 之前，Calculator 指定攝氏 TemperatureInput 的 onTemperatureChange 是 Calculator 的 handleCelsiusChange 方法，而華氏溫度的 TemperatureInput 的 onTemperatureChange 方法是 Calculator 的 handleFahrenheitChange 方法。因此根據我們編輯的輸入呼叫這兩個 Calculator 方法中的其中一個。
* 在這些方法中，Calculator component 要求 React 根據我們編輯的新輸入值和目前的溫度單位的輸入呼叫 this.setState() 來重新 render 本身。
* React 呼叫 Calculator component 的 render 方法來了解 UI 應該是怎麼樣子。根據目前溫度和溫度單位重新計算兩個輸入的值。溫度轉換會在這裡執行。
* 透過 Calculator 指定新的 prop，React 呼叫各個 TemperatureInput component 的 render 方法，它們應該了解 UI 是什麼樣子。
* React 呼叫 BoilingVerdict component 的 render 方法，以攝氏溫度做為 prop。
* React DOM 使用沸騰判定更新 DOM 並匹配所需的輸入值。我們剛剛編輯的輸入它接收目前的值，而另一個輸入被更新成轉換後的溫度。

### [經驗學習](https://zh-hant.reactjs.org/docs/lifting-state-up.html#lessons-learned)

在 React 應用程式中，對於資料的變化只能有一個唯一的「真相來源」。通常來說，state 會優先被加入到需要 render 的 component。接著，如果其他的 component 也需要的話，你可以提升 state 到共同最靠近的 ancestor。你應該依賴上至下的資料流，而不是嘗試在不同 component 之間同步 state。

提升 state 涉及撰寫更多的「boilerplate」程式碼，而不是雙向綁定的方法，但它對於隔離和尋找 bug 時更加容易。由於任何 state「存活」在一些 component 中，而且 component 本身可以改變它，bug 的產生大幅的減少。此外，你也可以實作任何自訂的邏輯來拒絕或轉換使用者的輸入。

如果某樣東西可以從 prop 或 state 被取得，它可能不應該在 state。例如，我們只 store 最後編輯的 temperature 和它的 scale，而不是 store celsiusValue 和 fahrenheitValue。其他輸入的值總是可以從它們的 render() 方法被計算出來。這讓我們可以清除或將四捨五入應用於另一個欄位, 而不會在使用者輸入中失去任何精度。

當你在 UI 上看到一些錯誤時，你可以使用 React Developer Tools 來檢查 prop 並往 tree 的上方尋找，直到找到負責更新 state 的 component。這讓你可以追蹤到錯誤的來源：

## 小結6

感覺類似向上傳遞 ng @output 、 vue $event 的感覺，

但講解寫得很複雜又不圖解不知道是在沖三小感覺 ng component 之間傳遞資料 都比這好懂了...

大概看程式碼就是將 setState 與 State 都交給上層 component 處理，下層只負責吃 props 做 render

## [Composition vs 繼承](https://zh-hant.reactjs.org/docs/composition-vs-inheritance.html)

React 具有強大的 composition 模型，我們建議你在 component 之間使用 composition 來複用你的程式碼，而不是使用繼承。

### 包含

有些 component 不會提早知道它們的 children 有些什麼。對於像是 Sidebar 或 Dialog 這類通用的「box」component 特別常見。

我們建議這些 component 使用特殊的 children prop 將 children element 直接傳入到它們的輸出：

```JavaScript
function FancyBorder(props) {
  return (
    <div className={'FancyBorder FancyBorder-' + props.color}>
      {props.children}
    </div>
  );
}

function WelcomeDialog() {
  return (
    <FancyBorder color="blue">
      <h1 className="Dialog-title">
        Welcome
      </h1>
      <p className="Dialog-message">
        Thank you for visiting our spacecraft!
      </p>
    </FancyBorder>
  );
}

ReactDOM.render(
  <WelcomeDialog />,
  document.getElementById('root')
);

```

有時候你可能需要在 component 中使用多個「hole」。在這種情況下，你可以使用你慣用的方法，而不是使用 children：

```JavaScript
function Contacts() {
  return <div className="Contacts" />;
}

function Chat() {
  return <div className="Chat" />;
}

function SplitPane(props) {
  return (
    <div className="SplitPane">
      <div className="SplitPane-left">
        {props.left}
      </div>
      <div className="SplitPane-right">
        {props.right}
      </div>
    </div>
  );
}

function App() {
  return (
    <SplitPane
      left={
        <Contacts />
      }
      right={
        <Chat />
      } />
  );
}

ReactDOM.render(
  <App />,
  document.getElementById('root')
);

```

### 特別化

有時候，我們需要考慮 component 會不會是其他 component 的「特別情況」。例如，我們可能會說 WelcomeDialog 是 Dialog 的一個特定情況。

在 React 中，這也可以透過 composition 被實現，其中更「特別」的 component render 更多「通用」的 component，並使用 prop 對其進行設定：

```JavaScript
function FancyBorder(props) {
  return (
    <div className={'FancyBorder FancyBorder-' + props.color}>
      {props.children}
    </div>
  );
}

function Dialog(props) {
  return (
    <FancyBorder color="blue">
      <h1 className="Dialog-title">
        {props.title}
      </h1>
      <p className="Dialog-message">
        {props.message}
      </p>
    </FancyBorder>
  );
}

function WelcomeDialog() {
  return (
    <Dialog
      title="Welcome"
      message="Thank you for visiting our spacecraft!" />
  );
}

ReactDOM.render(
  <WelcomeDialog />,
  document.getElementById('root')
);

// 對於使用 class 定義的 component，composition 一樣有效：

function FancyBorder(props) {
  return (
    <div className={'FancyBorder FancyBorder-' + props.color}>
      {props.children}
    </div>
  );
}

function Dialog(props) {
  return (
    <FancyBorder color="blue">
      <h1 className="Dialog-title">
        {props.title}
      </h1>
      <p className="Dialog-message">
        {props.message}
      </p>
      {props.children}
    </FancyBorder>
  );
}

class SignUpDialog extends React.Component {
  constructor(props) {
    super(props);
    this.handleChange = this.handleChange.bind(this);
    this.handleSignUp = this.handleSignUp.bind(this);
    this.state = {login: ''};
  }

  render() {
    return (
      <Dialog title="Mars Exploration Program"
              message="How should we refer to you?">
        <input value={this.state.login}
               onChange={this.handleChange} />
        <button onClick={this.handleSignUp}>
          Sign Me Up!
        </button>
      </Dialog>
    );
  }

  handleChange(e) {
    this.setState({login: e.target.value});
  }

  handleSignUp() {
    alert(`Welcome aboard, ${this.state.login}!`);
  }
}

ReactDOM.render(
  <SignUpDialog />,
  document.getElementById('root')
);


```

### 那麼關於繼承呢？

在 Facebook 中，我們使用 React 在成千上萬個 component，我們找不到任何使用案例來推薦你建立繼承結構的 component。

Prop 和 composition 提供你明確和安全的方式來自訂 component 的外觀和行為所需的靈活性。請記得，component 可以接受任意的 prop，包含 primitive value、React element，或者是 function。

如果你想要在 component 之間複用非 UI 的功能，我們建議抽離它到一個獨立的 JavaScript 模組。Component 可以 import 並使用它的 function、object，或者是 class，而不需要繼承它。

## 小結7

這裡我覺得主要是為了類似 SOLID 內的，開閉原則的感覺，不像 NG 有一堆裝飾器，而是全部集中在 React 框架下使用，但不知道是效能考量還是怎樣...

整體來說 NG 是要花時間記裝飾器用法，而 React 是要了解組件方法。

## [用 React 思考](https://zh-hant.reactjs.org/docs/thinking-in-react.html)

在我們的意見中，React 是用 JavaScript 建立大型、快速的網路應用程式最首要的方式。它對於在 Facebook 和 Instagram 的我們來說能很有效的增加規模。

React 眾多的優點之ㄧ是它讓你能在寫程式的同時去思考你的應用程式。在這個章節中，我們會帶領你走過一遍用 React 來建立一個可搜尋的產品資料表格的思考過程。

![IMAGE](https://zh-hant.reactjs.org/static/1071fbcc9eed01fddc115b41e193ec11/d4770/thinking-in-react-mock.png)

```JSON
[
  {category: "Sporting Goods", price: "$49.99", stocked: true, name: "Football"},
  {category: "Sporting Goods", price: "$9.99", stocked: true, name: "Baseball"},
  {category: "Sporting Goods", price: "$29.99", stocked: false, name: "Basketball"},
  {category: "Electronics", price: "$99.99", stocked: true, name: "iPod Touch"},
  {category: "Electronics", price: "$399.99", stocked: false, name: "iPhone 5"},
  {category: "Electronics", price: "$199.99", stocked: true, name: "Nexus 7"}
];
```

### 第一步：將 UI 拆解成 component 層級

首先，你要做的是將視覺稿中每一個 component （及 subcomponent）都圈起來，並幫它們命名。如果你在跟設計師合作的話，他們可能已經幫你做好這一步了，所以跟他們聊聊吧！他們在 Photoshop 中所用的圖層的名字可能可以作為你的 React component 的名字！

但是你要怎麼知道哪一個東西應該是自己獨立一個 component 呢？使用和你決定建立一個新的 function 或 object 一樣的準則即可。其中一個技巧是單一職責原則，它的意思是：在我們的理想中，一個 component應該只負責做一件事情。如果這個 component 最後變大了，你就需要再將它分成數個更小的 subcomponent 。

由於你常常會展示 JSON 的資料模型給使用者，你會發現，如果你的模式是正確地被建立的話，你的 UI（以及你的 component 結構）會很好的相互對應。這是因為 UI 和資料模型通常是遵守同樣的資訊架構，這意味著將你的 UI 拆成 component 通常是相當容易的。將 UI 分解成數個 component，每一個都明確代表著你的資料模型中的某一部份即可。

![IMAGE](https://zh-hant.reactjs.org/static/eb8bda25806a89ebdc838813bdfa3601/6b2ea/thinking-in-react-components.png)

你會看到在這裡我們應用程式中有五個 component。我們把每個 component 所代表的資料都斜體化了。

* FilterableProductTable（橘色）： 包含整個範例
* SearchBar（藍色）： 接收所有 使用者的輸入
* ProductTable（綠色）： 展示並過濾根據使用者輸入的資料集
* ProductCategoryRow（土耳其藍色）： 為每個列別展示標題
* ProductRow（紅色）： 為每個產品展示一列

如果你看看 ProductTable，你會發現表格的標題列（內含「Name」和「Price」標籤 ）並非獨立的 component。要不要把它們變成 component 這個議題完全是個人的喜好，正反意見都有。在這邊的例子裡面，我們把它當作 ProductTable 的一部分，因為它是 rendering 資料集 的一部分，而這正是 ProductTable 這個 component 的責任。然而，如果標題欄之後變得越來越複雜（假如我們要加上可以分類的直觀功能的話），那麼建立一個獨立的 ProductTableHeader component 就非常合理。

既然我們已經找出視覺稿中的 component 了，讓我們來安排它們的層級。在視覺稿中，在另一個 component 中出現的 component 就應該是 child：

* FilterableProductTable
  * SearchBar
  * ProductTable
    * ProductCategoryRow
    * ProductRow

### 第二步：在 React 中建立一個靜態版本

在你有了 component 層級後，就可以開始實作你的應用程式了。最簡單的方式是為你的應用程式建立一個接收資料模型、render UI 且沒有互動性的版本。建立一個靜態版本需要打很多字，但不需要想很多，而加上互動性則相反，需要做很多的思考，很少的打字，所以最好的方式是把這幾個過程都分開來。接下來，我們會知道為什麼是如此。

為你的應用程式建立一個 render 資料模型的版本，你會想要建立可以重複使用其他 component 的 component，並使用 props 傳遞資料。Props 是將資料從 parent 傳給 child 的方式。如果你對於 state 的概念很熟悉的話，請完全不要使用 state 來建立這個靜態版本。State 是保留給互動性的，也就是會隨時間改變的資料。既然我們目前要做的是這應用程式的靜態版本，你就不需要 state。

你可以從最上層開始，或從最下層開始。也就是說，你可以先從層級較高的 component 開始做起（也就是從 FilterableProductTable 開始），或者你也可以從比它低層級的（ProductRow）開始。在比較簡單的例子中，通常從上往下是比較簡單的。但在較為大型的專案中，從下往上、邊寫邊測試則比較容易。

在這一步的最後，你會有一個函式庫的可重複使用的 component 來 render 你的資料模型。這些 component 只會有 render() 方法，因為這是你應用程式的靜態版本。最高層級的 component (FilterableProductTable) 會接收你的資料模型作為 prop。如果你改變底層的資料模型並再次呼叫 ReactDOM.render() 的話，那麼 UI 就會被更新。你可以看到 UI 的更新方式以及更改的位置。React 的 單向資料流（也可稱為單向綁定）確保所有 component 都是模塊化且快速的。

如果你需要幫助來執行這一步的話，請參考這份 React 文件。

```JavaScript
class ProductCategoryRow extends React.Component {
  render() {
    const category = this.props.category;
    return (
      <tr>
        <th colSpan="2">
          {category}
        </th>
      </tr>
    );
  }
}

class ProductRow extends React.Component {
  render() {
    const product = this.props.product;
    const name = product.stocked ?
      product.name :
      <span style={{color: 'red'}}>
        {product.name}
      </span>;

    return (
      <tr>
        <td>{name}</td>
        <td>{product.price}</td>
      </tr>
    );
  }
}

class ProductTable extends React.Component {
  render() {
    const rows = [];
    let lastCategory = null;

    this.props.products.forEach((product) => {
      if (product.category !== lastCategory) {
        rows.push(
          <ProductCategoryRow
            category={product.category}
            key={product.category} />
        );
      }
      rows.push(
        <ProductRow
          product={product}
          key={product.name} />
      );
      lastCategory = product.category;
    });

    return (
      <table>
        <thead>
          <tr>
            <th>Name</th>
            <th>Price</th>
          </tr>
        </thead>
        <tbody>{rows}</tbody>
      </table>
    );
  }
}

class SearchBar extends React.Component {
  render() {
    return (
      <form>
        <input type="text" placeholder="Search..." />
        <p>
          <input type="checkbox" />
          {' '}
          Only show products in stock
        </p>
      </form>
    );
  }
}

class FilterableProductTable extends React.Component {
  render() {
    return (
      <div>
        <SearchBar />
        <ProductTable products={this.props.products} />
      </div>
    );
  }
}


const PRODUCTS = [
  {category: 'Sporting Goods', price: '$49.99', stocked: true, name: 'Football'},
  {category: 'Sporting Goods', price: '$9.99', stocked: true, name: 'Baseball'},
  {category: 'Sporting Goods', price: '$29.99', stocked: false, name: 'Basketball'},
  {category: 'Electronics', price: '$99.99', stocked: true, name: 'iPod Touch'},
  {category: 'Electronics', price: '$399.99', stocked: false, name: 'iPhone 5'},
  {category: 'Electronics', price: '$199.99', stocked: true, name: 'Nexus 7'}
];

ReactDOM.render(
  <FilterableProductTable products={PRODUCTS} />,
  document.getElementById('container')
);
```

#### 簡短的插曲：Props 和 State

### 第三步：找出最少（但完整）的 UI State 的代表

為了將你的 UI 變成有互動性，你需要有辦法觸發底層的資料模型做出改變。React 使用 state 把這件事實現了。

為了正確地建立你的應用程式，你首先需要思考你的應用程式最少需要哪些可變的 state。這裡的關鍵是 DRY：避免重複代碼原則。請找出你的應用程式所需的最少的呈現方式，並在你遇到其他東西時再計算它們。例如，如果你在建立一個待辦清單，使用一個可以用來代表待辦事項的 array。不要另外用一個獨立的 state 變數來追蹤數量。當你要 render 代辦事項的數量時，讀取待辦事項 array 的長度即可。

思考我們範例中應用程式的所有資料。我們現在有：

* 原本的產品列表
* 使用者輸入的搜尋關鍵字
* checkbox 的值
* 篩選過後的產品列表

讓我們來看一下每一個資料，並找出哪一個是 state。對於每一個資料，問你自己這三個問題：

1. 這個資料是從 parent 透過 props 傳下來的嗎？如果是的話，那它很可能不是 state。
2. 這個資料是否一直保持不變呢？如果是的話，那它很可能不是 state。
3. 你是否可以根據你的 component 中其他的 state 或 prop 來計算這個資料呢？如果是的話，那它一定不是 state。

原本的產品列表是被當作 prop 往下傳的，所以它不是 state。搜尋關鍵字和 checkbox 看起來可能是 state，因為它們會隨時間而改變，也不能從其他東西中被計算出來。最後，篩選過後的產品列表不是 state，因為它能透過結合原本的產品列表、搜尋關鍵字和checkbox 的值被計算出來。

所以，我們的 state 是：

* 使用者輸入的搜尋關鍵字
* checkbox 的值

### 第四步：找出你的 State 應該在哪裡

```JavaScript
class ProductCategoryRow extends React.Component {
  render() {
    const category = this.props.category;
    return (
      <tr>
        <th colSpan="2">
          {category}
        </th>
      </tr>
    );
  }
}

class ProductRow extends React.Component {
  render() {
    const product = this.props.product;
    const name = product.stocked ?
      product.name :
      <span style={{color: 'red'}}>
        {product.name}
      </span>;

    return (
      <tr>
        <td>{name}</td>
        <td>{product.price}</td>
      </tr>
    );
  }
}

class ProductTable extends React.Component {
  render() {
    const filterText = this.props.filterText;
    const inStockOnly = this.props.inStockOnly;

    const rows = [];
    let lastCategory = null;

    this.props.products.forEach((product) => {
      if (product.name.indexOf(filterText) === -1) {
        return;
      }
      if (inStockOnly && !product.stocked) {
        return;
      }
      if (product.category !== lastCategory) {
        rows.push(
          <ProductCategoryRow
            category={product.category}
            key={product.category} />
        );
      }
      rows.push(
        <ProductRow
          product={product}
          key={product.name}
        />
      );
      lastCategory = product.category;
    });

    return (
      <table>
        <thead>
          <tr>
            <th>Name</th>
            <th>Price</th>
          </tr>
        </thead>
        <tbody>{rows}</tbody>
      </table>
    );
  }
}

class SearchBar extends React.Component {
  render() {
    const filterText = this.props.filterText;
    const inStockOnly = this.props.inStockOnly;

    return (
      <form>
        <input
          type="text"
          placeholder="Search..."
          value={filterText} />
        <p>
          <input
            type="checkbox"
            checked={inStockOnly} />
          {' '}
          Only show products in stock
        </p>
      </form>
    );
  }
}

class FilterableProductTable extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      filterText: '',
      inStockOnly: false
    };
  }

  render() {
    return (
      <div>
        <SearchBar
          filterText={this.state.filterText}
          inStockOnly={this.state.inStockOnly}
        />
        <ProductTable
          products={this.props.products}
          filterText={this.state.filterText}
          inStockOnly={this.state.inStockOnly}
        />
      </div>
    );
  }
}


const PRODUCTS = [
  {category: 'Sporting Goods', price: '$49.99', stocked: true, name: 'Football'},
  {category: 'Sporting Goods', price: '$9.99', stocked: true, name: 'Baseball'},
  {category: 'Sporting Goods', price: '$29.99', stocked: false, name: 'Basketball'},
  {category: 'Electronics', price: '$99.99', stocked: true, name: 'iPod Touch'},
  {category: 'Electronics', price: '$399.99', stocked: false, name: 'iPhone 5'},
  {category: 'Electronics', price: '$199.99', stocked: true, name: 'Nexus 7'}
];

ReactDOM.render(
  <FilterableProductTable products={PRODUCTS} />,
  document.getElementById('container')
);
```

OK，所以我們已經找出這個應用程式最少的 state 是哪些了。下一步，我們需要找出哪幾個 component 會 mutate，或者擁有，這個 state。

請記得，React 的核心精神是單向資料流，從 component 的層級從高往下流。也許哪個 component 該擁有 state 在一開始並不是很明顯。對新手來說，這往往是最難理解的概念，所以請跟著以下的步驟來思考：

在你的應用程式中的每個 state：

* 指出每個根據 state 來 render 某些東西的 component。
* 找出一個共同擁有者 component（在層級中單一一個需要 state 的、在所有的 component 之上的 component）。
* 應該擁有 state 的會是共同擁有者 component 或另一個更高層級的 component。
* 如果你找不出一個應該擁有 state 的 component 的話，那就建立一個新的 component 來保持 state，並把它加到層級中共同擁有者 component 之上的某處。

讓我們來討論一下我們應用程式的這個策略：

* ProductTable 需要根據 state 來篩選產品列表，而 SearchBar 需要展示搜尋關鍵字和 checkbox 的 state。
* 這兩個 component 的共同擁有者 component 是 FilterableProductTable。
* 概念上來說，篩選過的文字和復選框的值存在於 FilterableProductTable 中是合理的。

很好，所以我們現在已經決定我們的 state 會存在於 FilterableProductTable 之中。首先，把這個實例的 property this.state = {filterText: '', inStockOnly: false} 加到 FilterableProductTable 的 constructor 裡面以反映你的應用程式的初始 state。接著，把 filterText 和 inStockOnly 作為 prop 傳給 ProductTable 和 SearchBar。最後，用這些 prop 來篩選 ProductTable 中的列，並設定 SearchBar 中的表格欄的值。

你可以開始看到你的應用程式會如何運作：將 filterText 設定為 「ball」 並更新你的程式。你會看到資料表被正確地更新了。

### 第五步：加入相反的資料流

```JavaScript
class ProductCategoryRow extends React.Component {
  render() {
    const category = this.props.category;
    return (
      <tr>
        <th colSpan="2">
          {category}
        </th>
      </tr>
    );
  }
}

class ProductRow extends React.Component {
  render() {
    const product = this.props.product;
    const name = product.stocked ?
      product.name :
      <span style={{color: 'red'}}>
        {product.name}
      </span>;

    return (
      <tr>
        <td>{name}</td>
        <td>{product.price}</td>
      </tr>
    );
  }
}

class ProductTable extends React.Component {
  render() {
    const filterText = this.props.filterText;
    const inStockOnly = this.props.inStockOnly;

    const rows = [];
    let lastCategory = null;

    this.props.products.forEach((product) => {
      if (product.name.indexOf(filterText) === -1) {
        return;
      }
      if (inStockOnly && !product.stocked) {
        return;
      }
      if (product.category !== lastCategory) {
        rows.push(
          <ProductCategoryRow
            category={product.category}
            key={product.category} />
        );
      }
      rows.push(
        <ProductRow
          product={product}
          key={product.name}
        />
      );
      lastCategory = product.category;
    });

    return (
      <table>
        <thead>
          <tr>
            <th>Name</th>
            <th>Price</th>
          </tr>
        </thead>
        <tbody>{rows}</tbody>
      </table>
    );
  }
}

class SearchBar extends React.Component {
  constructor(props) {
    super(props);
    this.handleFilterTextChange = this.handleFilterTextChange.bind(this);
    this.handleInStockChange = this.handleInStockChange.bind(this);
  }

  handleFilterTextChange(e) {
    this.props.onFilterTextChange(e.target.value);
  }

  handleInStockChange(e) {
    this.props.onInStockChange(e.target.checked);
  }

  render() {
    return (
      <form>
        <input
          type="text"
          placeholder="Search..."
          value={this.props.filterText}
          onChange={this.handleFilterTextChange}
        />
        <p>
          <input
            type="checkbox"
            checked={this.props.inStockOnly}
            onChange={this.handleInStockChange}
          />
          {' '}
          Only show products in stock
        </p>
      </form>
    );
  }
}

class FilterableProductTable extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      filterText: '',
      inStockOnly: false
    };

    this.handleFilterTextChange = this.handleFilterTextChange.bind(this);
    this.handleInStockChange = this.handleInStockChange.bind(this);
  }

  handleFilterTextChange(filterText) {
    this.setState({
      filterText: filterText
    });
  }

  handleInStockChange(inStockOnly) {
    this.setState({
      inStockOnly: inStockOnly
    })
  }

  render() {
    return (
      <div>
        <SearchBar
          filterText={this.state.filterText}
          inStockOnly={this.state.inStockOnly}
          onFilterTextChange={this.handleFilterTextChange}
          onInStockChange={this.handleInStockChange}
        />
        <ProductTable
          products={this.props.products}
          filterText={this.state.filterText}
          inStockOnly={this.state.inStockOnly}
        />
      </div>
    );
  }
}


const PRODUCTS = [
  {category: 'Sporting Goods', price: '$49.99', stocked: true, name: 'Football'},
  {category: 'Sporting Goods', price: '$9.99', stocked: true, name: 'Baseball'},
  {category: 'Sporting Goods', price: '$29.99', stocked: false, name: 'Basketball'},
  {category: 'Electronics', price: '$99.99', stocked: true, name: 'iPod Touch'},
  {category: 'Electronics', price: '$399.99', stocked: false, name: 'iPhone 5'},
  {category: 'Electronics', price: '$199.99', stocked: true, name: 'Nexus 7'}
];

ReactDOM.render(
  <FilterableProductTable products={PRODUCTS} />,
  document.getElementById('container')
);
```

到目前為止，我們已經建立了一個作為含有從層級由上往下傳 props 和 state 的、且可以正確 render 的 function 的應用程式。現在是時候支援另一種資料流的方向了：在層級深處的表格 component 需要更新 FilterableProductTable 的 state。

React 將這種資料流明確表示出來，以便讓你能更容易理解你的程式如何運作，但是這的確比傳統的雙向資料綁定需要打多一點字。

如果你試著在範例目前的版本中印出或勾選 checkbox，你會看到 React 無視你的輸入。這是刻意的，因為我們把 input 的 value prop 設定為永遠和從 FilterableProductTable 傳下來的 state ㄧ樣。

讓我們思考一下我們想要做些什麼。我們想確保當使用者改變這個表格時，我們會更新 state 以反映使用者的輸入。既然 component 只應該更新它自己本身的 state， FilterableProductTable 將會把 callback 傳給 SearchBar，而它們則會在 state 該被更新的時候被觸發。我們可以在輸入上使用 onChange 這個 event 來 接收通知。被 FilterableProductTable 傳下來的 callback 則會呼叫 setState()，之後應用程式就會被更新。

完成

希望這幫助你理解如何用 React 建立 component 和應用程式。雖然這可能需要你比你習慣的多打一些程式碼，請記得閱讀程式碼比起寫程式碼更常發生，而閱讀這種模組化、清晰明確的程式碼是非常容易的。當你開始建立大型的 component 函式庫時，你會很感激有這樣的明確性和模組性，而當你開始重複使用程式碼時，你的程式的行數會開始減少。:)

