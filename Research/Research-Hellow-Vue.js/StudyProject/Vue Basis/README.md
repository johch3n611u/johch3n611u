# [Vue Basis](https://cn.vuejs.org/v2/guide/#%E8%B5%B7%E6%AD%A5) Version 2.X

## [声明式渲染](https://cn.vuejs.org/v2/guide/#%E5%A3%B0%E6%98%8E%E5%BC%8F%E6%B8%B2%E6%9F%93)

文本插值

> {{ app.message }}

数据和 DOM 被建立了关联都是响应式的，修改 JavaScript 控制台 app.message 的值，你将看到相应地更新。

> app.message = 123

* 注意不再和 HTML 直接交互了。一个 Vue 应用会将其挂载到一个 DOM 元素上 (对于这个例子是 #app) 然后对其进行完全控制。那个 HTML 是我们的入口，但其余都会发生在新创建的 Vue 实例内部。

绑定元素 attribute

> v-bind

指令带有前缀 `v-`  Vue 提供的特殊 attribute，在渲染的 DOM 上应用特殊的响应式行为。

> &lt;span v-bind:title="message">

将这个元素节点的 title attribute 和 Vue 实例的 message property 保持一致。

> app2.message = 123

## [条件与循环](https://cn.vuejs.org/v2/guide/#%E6%9D%A1%E4%BB%B6%E4%B8%8E%E5%BE%AA%E7%8E%AF)

> v-if="boolean"

條件判斷渲染

> v-for="todo in todos"

這裡其實感覺像後端的 foreach 但不知為何是 for 像是 python 也淘汰 for 不知為何?

> app4.todos.push({ text: '新项目' })

不仅可以把数据绑定到 DOM 文本或 attribute，还可以绑定到 DOM 结构。

* 还有其它很多指令，每个都有特殊的功能，進階的更可以在 Vue 插入/更新/移除元素时自动应用过渡效果。

## [处理用户输入](https://cn.vuejs.org/v2/guide/#%E5%A4%84%E7%90%86%E7%94%A8%E6%88%B7%E8%BE%93%E5%85%A5)

> v-on + 事件监听器

讓用户和应用进行交互

> v-on:click="reverseMessage"

```JavaScript
var app5 = new Vue({
  el: '#app-5',
  data: {
    message: 'Hello Vue.js!'
  },
  methods: {
    reverseMessage: function () {
      this.message = this.message.split('').reverse().join('')
    }
  }
})
```

#### 小結1

跟 Angular 應用上無差別就是綁定，但細節使用上有所差別，且因 NG 強制使用 TS 有些地方會有些許不一樣。

* 更新了应用的状态，但没有触碰 DOM——所有的 DOM 操作都由 Vue 来处理，你编写的代码只需要关注逻辑层面即可。

這裡應該是講說並沒有像 jQ 那樣去抓 DOM 並對 DOM 做操作，而是指對資料做操作。

> v-model

轻松实现表单输入和应用状态之间的双向绑定。

## [组件化应用构建](https://cn.vuejs.org/v2/guide/#%E7%BB%84%E4%BB%B6%E5%8C%96%E5%BA%94%E7%94%A8%E6%9E%84%E5%BB%BA)

一个组件本质上是一个拥有预定义选项的一个 Vue 实例。

類似 Angular 的裝飾器 ?? 不太確定是否是這個意思

> 在 Vue 中注册组件

```JavaScript
// 定义名为 todo-item 的新组件
Vue.component('todo-item', {
  template: '<li>这是个待办项</li>'
})

var app = new Vue(...)
```

> 在 Vue 中使用组件

```HTML
<ol>
  <!-- 创建一个 todo-item 组件的实例 -->
  <todo-item></todo-item>
</ol>
```

> 父作用域将数据传到子组件 - prop

```JavaScript
Vue.component('todo-item', {
  // todo-item 组件现在接受一个
  // "prop"，类似于一个自定义 attribute。
  // 这个 prop 名为 todo。
  props: ['todo'],
  template: '<li>{{ todo.text }}</li>'
})
```

> 使用 v-bind 指令将待办项传到循环输出的每个组件中

```JavaScript
<div id="app-7">
  <ol>
    <!--
      现在我们为每个 todo-item 提供 todo 对象
      todo 对象是变量，即其内容可以是动态的。
      我们也需要为每个组件提供一个 key ，稍后再
      作详细解释。
    -->
    <todo-item
      v-for="item in groceryList"
      v-bind:todo="item"
      v-bind:key="item.id"
    ></todo-item>
  </ol>
</div>
```

```JavaScript
Vue.component('todo-item', {
  props: ['todo'],
  template: '<li>{{ todo.text }}</li>'
})

var app7 = new Vue({
  el: '#app-7',
  data: {
    groceryList: [
      { id: 0, text: '蔬菜' },
      { id: 1, text: '奶酪' },
      { id: 2, text: '随便其它什么人吃的东西' }
    ]
  }
})
```

> 大型組件 example

```HTML
<div id="app">
  <app-nav></app-nav>
  <app-view>
    <app-sidebar></app-sidebar>
    <app-content></app-content>
  </app-view>
</div>
```

#### 小結2

這裡與 Angular 也所差異，使用 ng 時多是使用 ng cli 所以生成文件與 component 就由 cli 處理，但 vue 這邊還沒使用過 cli 不太確定差異性。

本質上似乎都是藉由 es6 export 讓組件 interface、class 化的感覺，不然就是要藉由 JS 引入檔的排序做變化。

## [与自定义元素的关系](https://cn.vuejs.org/v2/guide/#%E4%B8%8E%E8%87%AA%E5%AE%9A%E4%B9%89%E5%85%83%E7%B4%A0%E7%9A%84%E5%85%B3%E7%B3%BB)

## [数据与方法](https://cn.vuejs.org/v2/guide/instance.html#%E6%95%B0%E6%8D%AE%E4%B8%8E%E6%96%B9%E6%B3%95)

* Vue 实例被创建时，它将 data 对象中的所有的 property 加入到 Vue 的响应式系统中。当这些 property 的值发生改变时，视图将会产生 响应 ，即匹配更新为新的值。

```JavaScript
// 我们的数据对象
var data = { a: 1 }

// 该对象被加入到一个 Vue 实例中
var vm = new Vue({
  data: data
})

// 获得这个实例上的 property
// 返回源数据中对应的字段
vm.a == data.a // => true

// 设置 property 也会影响到原始数据
vm.a = 2
data.a // => 2

// ……反之亦然
data.a = 3
vm.a // => 3
```

> vm.b = 'hi'

* 只有当实例被创建时就已经存在于 data 中的 property 才是响应式的。对 b 的改动将不会触发任何视图的更新。

```JavaScript
data: {
  newTodoText: '',
  visitCount: 0,
  hideCompletedTodos: false,
  todos: [],
  error: null,
  b:''
}
```

> Object.freeze()

阻止修改现有的 property，响应系统无法再追踪变化。

## [Vue 保留 property 与方法，与用户定义的 property 区分](https://cn.vuejs.org/v2/api/#%E5%AE%9E%E4%BE%8B-property)

```JavaScript
var data = { a: 1 }
var vm = new Vue({
  el: '#example',
  data: data
})

vm.$data === data // => true
vm.$el === document.getElementById('example') // => true

// $watch 是一个实例方法
vm.$watch('a', function (newValue, oldValue) {
  // 这个回调将在 `vm.a` 改变后调用
})
```

## [实例生命周期钩子](https://cn.vuejs.org/v2/guide/instance.html#%E7%94%9F%E5%91%BD%E5%91%A8%E6%9C%9F%E5%9B%BE%E7%A4%BA)

* 不要在选项 property 或回调上使用箭头函数，比如 created: () => console.log(this.a) 或 vm.$watch('a', newValue => this.myMethod())。因为箭头函数并没有 this，this 会作为变量一直向上级词法作用域查找，直至找到为止，经常导致 Uncaught TypeError: Cannot read property of undefined 或 Uncaught TypeError: this.myMethod is not a function 之类的错误。

#### 小結3

確實跟 ng 原理蠻像的，但感覺架構較小些，大概也是實際編輯時遇到問題在解決會熟悉較快，畢竟 ng 時就在這樣學習生命週期的...

![IMAGE](https://github.com/johch3n611u/Side-Project-Hellow-Vue.js/blob/master/IMG/lifecycle.png?raw=true)

## 模板语法

Vue.js 使用了基于 HTML 的模板语法，允许开发者声明式地将 DOM 绑定至底层 Vue 实例的数据。[所有 Vue.js 的模板都是合法的 HTML](#)，所以能被遵循规范的浏览器和 HTML 解析器解析。

在底层的实现上，Vue 将[模板编译成虚拟 DOM 渲染函数。结合响应系统](#)，Vue 能够智能地计算出最少需要重新渲染多少组件，并把 DOM 操作次数减到最少。

如果你熟悉虚拟 DOM 并且偏爱 JavaScript 的原始力量，你也可以不用模板，直接写渲染 (render) 函数，使用可选的 JSX 语法。

### 純文本插值

Mustache

> {{ msg }}

执行一次性指令，当数据改变时，插值处的内容不会更新。

> v-once

* 这会影响到该节点上的其它数据绑定

### [输出真正的 HTML，你需要使用](https://cn.vuejs.org/v2/guide/syntax.html#%E5%8E%9F%E5%A7%8B-HTML)

> v-html="HtmlTag"

这个 Tag 的内容将会被替换成为 property 值 rawHtml，直接作为 HTML——会忽略解析 property 值中的数据绑定。

* [注意，你不能使用 v-html 来复合局部模板，因为 Vue 不是基于字符串的模板引擎。反之，对于用户界面 (UI)，组件更适合作为可重用和可组合的基本单位。](https://segmentfault.com/q/1010000011336626)

### Attributes

* Vue在进行进行插值处理和 绑定表达式时使用了一种叫[Mustache](https://www.jianshu.com/p/5b16cd0c1ca5)模版引擎。是一个logic-less(轻逻辑)模板解析引擎,它的优势在于可以应用在Javascript、PHP、Python、Perl等多种编程语言中。

Mustache 語法不能作用在 [HTML 特性(Property not always like Attribute 屬性)](https://openhome.cc/Gossip/JavaScript/AttributeProperty.html)上，遇到這種情況應該使用 [v-bind 指令](https://ithelp.ithome.com.tw/m/articles/10191722)

```HTML
<div id="app">
  <div v-bind:id="dynamicId">dynamicId</div>
  <button v-bind:disabled="isButtonDisabled">Button</button>
</div>
```

```JavaScript
var vm = new Vue({
  el: '#app',
  data: {
    dynamicId: 'colorRed',
    isButtonDisabled: true
  }
})
```

### [使用 JavaScript 表达式](https://cn.vuejs.org/v2/guide/syntax.html#%E4%BD%BF%E7%94%A8-JavaScript-%E8%A1%A8%E8%BE%BE%E5%BC%8F)

有效

```JavaScript
{{ number + 1 }}

{{ ok ? 'YES' : 'NO' }}

{{ message.split('').reverse().join('') }}

<div v-bind:id="'list-' + id"></div>
```

無效

```JavaScript
<!-- 这是语句，不是表达式 -->
{{ var a = 1 }}

<!-- 流控制也不会生效，请使用三元表达式 -->
{{ if (ok) { return message } }}
```

### [Directives(指令)](https://ithelp.ithome.com.tw/m/articles/10191722)

指令 (Directives) 是带有 v- 前缀的特殊属性。指令的职责是，当表达式的值改变时，将其产生的连带影响，响应式地作用于 DOM。

### [Arguments(參數)](https://ithelp.ithome.com.tw/m/articles/10191722)

有些指令可以接受一個參數，在指令名稱之後以冒號表示。

```JavaScript
<a v-bind:href="url"> ... </a>
<a v-on:click="doSomething"> ... </a>
```

### [动态参数](https://cn.vuejs.org/v2/guide/syntax.html#%E5%8A%A8%E6%80%81%E5%8F%82%E6%95%B0)

从 2.6.0 开始，可以用方括号括起来的 JavaScript 表达式作为一个指令的参数：

```JavaScript
<!--
注意，参数表达式的写法存在一些约束，如之后的 对动态参数表达式的约束 章节所述。
-->
<a v-bind:[attributeName]="url"> ... </a>
<a v-on:[eventName]="doSomething"> ... </a>
```

* 这里的 attributeName 会被作为一个 JavaScript 表达式进行动态求值，求得的值将会作为最终的参数来使用。例如，如果你的 Vue 实例有一个 data property attributeName，其值为 "href"，那么这个绑定将等价于 v-bind:href。同样地，你可以使用动态参数为一个动态的事件名绑定处理函数：在这个示例中，当 eventName 的值为 "focus" 时，v-on:[eventName] 将等价于 v-on:focus。

#### 对动态参数的值的约束

动态参数预期会求出一个字符串，异常情况下值为 null。这个特殊的 null 值可以被显性地用于移除绑定。任何其它非字符串类型的值都将会触发一个警告。

#### 对动态参数表达式的约束

动态参数表达式有一些语法约束，因为某些字符，如空格和引号，放在 HTML attribute 名里是无效的。例如：

```JavaScript
<!-- 这会触发一个编译警告 -->
<a v-bind:['foo' + bar]="value"> ... </a>
```

变通的办法是使用没有空格或引号的表达式，或用计算属性替代这种复杂表达式。

* 在 DOM 中使用模板时 (直接在一个 HTML 文件里撰写模板)，还需要避免使用大写字符来命名键名，因为浏览器会把 attribute 名全部强制转为小写：

```JavaScript
<!--
在 DOM 中使用模板时这段代码会被转换为 `v-bind:[someattr]`。
除非在实例中有一个名为 someattr 的 property，否则代码不会工作。
-->
<a v-bind:[someAttr]="value"> ... </a>
```

### [Modifilers(修飾符)](https://ithelp.ithome.com.tw/m/articles/10191722)

修飾符是以.指定特殊的後綴。例如，.prevent修飾符為告訴v-on指令對於觸發的事件調用event.preventDefault()：

```JavaScript
<form v-on:submit.prevent="onSubmit"> ... </form>
```

### Shorthands(縮寫)

因為v-bind和v-on這兩個指令太常用到，Vue 提供縮寫。

> v-bind Shorthand

```JavaScript
<!-- 完整语法 -->
<a v-bind:href="url">...</a>

<!-- 缩写 -->
<a :href="url">...</a>
```

> v-on Shorthand

```JavaScript
<!-- 完整语法 -->
<a v-on:click="doSomething">...</a>

<!-- 缩写 -->
<a @click="doSomething">...</a>
```

## 计算属性和侦听器

### 计算属性

模板内的表达式非常便利，但是设计它们的初衷是用于简单运算的。在模板中放入太多的逻辑会让模板过重且难以维护。例如：

```JavaScript
<div id="example">
  {{ message.split('').reverse().join('') }}
</div>
```

所以，对于任何复杂逻辑，你都应当使用计算属性。

```JavaScript
<div id="example">
  <p>Original message: "{{ message }}"</p>
  <p>Computed reversed message: "{{ reversedMessage }}"</p>
</div>

var vm = new Vue({
  el: '#example',
  data: {
    message: 'Hello'
  },
  computed: {
    // 计算属性的 getter
    reversedMessage: function () {
      // `this` 指向 vm 实例
      return this.message.split('').reverse().join('')
    }
  }
})
```

```JavaScript
console.log(vm.reversedMessage) // => 'olleH'
vm.message = 'Goodbye'
console.log(vm.reversedMessage) // => 'eybdooG'
```

### [计算属性缓存(computed) vs 方法(methods)](https://cn.vuejs.org/v2/guide/computed.html)

```JavaScript
<p>Reversed message: "{{ reversedMessage() }}"</p>

// 在组件中
methods: {
  reversedMessage: function () {
    return this.message.split('').reverse().join('')
  }
}
```

* 两种方式的最终结果确实是完全相同的。然而，不同的是计算属性是基于它们的响应式依赖进行缓存的。只在相关响应式依赖发生改变时它们才会重新求值。这就意味着只要 message 还没有发生改变，多次访问 reversedMessage 计算属性会立即返回之前的计算结果，而不必再次执行函数。

这也同样意味着下面的计算属性将不再更新，因为 Date.now() 不是响应式依赖：

```JavaScript
computed: {
  now: function () {
    return Date.now()
  }
}
```

* 每当触发重新渲染时，调用方法(methods)将总会再次执行函数。

* 我们为什么需要缓存？假设我们有一个性能开销比较大的计算属性 A，它需要遍历一个巨大的数组并做大量的计算。然后我们可能有其他的计算属性依赖于 A。如果没有缓存，我们将不可避免的多次执行 A 的 getter！如果你不希望有缓存，请用方法来替代。

#### [差異](https://ithelp.ithome.com.tw/m/articles/10191808)

當定義 computed 之後，其相依的 data 或是 component 中的 props 改變，computed 也會隨之更新；methods 則是不管資料是否相依都會計算。

下例的 computed 中沒有相依的 data，因此在 message 被修改時，now 沒有被更新，但 methods 會重新計算更新 getNow 的值。

```JavaScript
<div id="app">
  <p>message：{{ message }}</p>
  <p>now (computed)：{{ now }}</p>
  <p>getNow (method)：{{ getNow() }}</p>
</div>

var vm = new Vue({
  el: '#app',
  data: {
    message: 'Hello Vue!'
  },
  computed: {
    now: function() {
      return Date.now();
    }
  },
  methods: {
    getNow: function() {
      return Date.now();
    }
  }
});

vm.$data.message = 'message changed';
```

#### 小結5

> [插入文本之外的过滤器移除](https://cn.vuejs.org/v2/guide/migration.html#%E6%8F%92%E5%85%A5%E6%96%87%E6%9C%AC%E4%B9%8B%E5%A4%96%E7%9A%84%E8%BF%87%E6%BB%A4%E5%99%A8%E7%A7%BB%E9%99%A4)

看來還是要依照官網的，版本有變動有些東西還直接拿掉 ? 跟 ng 很像... 但 ng 有向下兼容，更新上也可以查到蠻多資料的，不知道 Vue 又是如何...

### 计算属性 vs 侦听属性

* 当你有一些数据需要随着其它数据变动而变动时，你很容易滥用 watch——特别是如果你之前使用过 AngularJS。然而，通常更好的做法是使用计算属性而不是命令式的 watch 回调。

> watch

```JavaScript
<div id="demo">{{ fullName }}</div>

var vm = new Vue({
  el: '#demo',
  data: {
    firstName: 'Foo',
    lastName: 'Bar',
    fullName: 'Foo Bar'
  },
  watch: {
    firstName: function (val) {
      this.fullName = val + ' ' + this.lastName
    },
    lastName: function (val) {
      this.fullName = this.firstName + ' ' + val
    }
  }
})
```

> computed

```JavaScript
<div id="demo">{{ fullName }}</div>

var vm = new Vue({
  el: '#demo',
  data: {
    firstName: 'Foo',
    lastName: 'Bar'
  },
  computed: {
    fullName: function () {
      return this.firstName + ' ' + this.lastName
    }
  }
})
```

### 计算属性的 setter

计算属性默认只有 getter，不过在需要时你也可以提供一个 setter：

```JavaScript
// ...
computed: {
  fullName: {
    // getter
    get: function () {
      return this.firstName + ' ' + this.lastName
    },
    // setter
    set: function (newValue) {
      var names = newValue.split(' ')
      this.firstName = names[0]
      this.lastName = names[names.length - 1]
    }
  }
}
// ...
```

现在再运行 vm.fullName = 'John Doe' 时，setter 会被调用，vm.firstName 和 vm.lastName 也会相应地被更新。

### [自定义的侦听器](https://cn.vuejs.org/v2/guide/computed.html#%E4%BE%A6%E5%90%AC%E5%99%A8)

 Vue 通过 watch 选项提供了一个更通用的方法，来响应数据的变化。当需要在数据变化时执行异步或开销较大的操作时，这个方式是最有用的。

```JavaScript
<div id="watch-example">
  <p>
    Ask a yes/no question:
    <input v-model="question">
  </p>
  <p>{{ answer }}</p>
</div>

<!-- 因为 AJAX 库和通用工具的生态已经相当丰富，Vue 核心代码没有重复 -->
<!-- 提供这些功能以保持精简。这也可以让你自由选择自己更熟悉的工具。 -->
<script src="https://cdn.jsdelivr.net/npm/axios@0.12.0/dist/axios.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/lodash@4.13.1/lodash.min.js"></script>
<script>
var watchExampleVM = new Vue({
  el: '#watch-example',
  data: {
    question: '',
    answer: 'I cannot give you an answer until you ask a question!'
  },
  watch: {
    // 如果 `question` 发生改变，这个函数就会运行
    question: function (newQuestion, oldQuestion) {
      this.answer = 'Waiting for you to stop typing...'
      this.debouncedGetAnswer()
    }
  },
  created: function () {
    // `_.debounce` 是一个通过 Lodash 限制操作频率的函数。
    // 在这个例子中，我们希望限制访问 yesno.wtf/api 的频率
    // AJAX 请求直到用户输入完毕才会发出。想要了解更多关于
    // `_.debounce` 函数 (及其近亲 `_.throttle`) 的知识，
    // 请参考：https://lodash.com/docs#debounce
    this.debouncedGetAnswer = _.debounce(this.getAnswer, 500)
  },
  methods: {
    getAnswer: function () {
      if (this.question.indexOf('?') === -1) {
        this.answer = 'Questions usually contain a question mark. ;-)'
        return
      }
      this.answer = 'Thinking...'
      var vm = this
      axios.get('https://yesno.wtf/api')
        .then(function (response) {
          vm.answer = _.capitalize(response.data.answer)
        })
        .catch(function (error) {
          vm.answer = 'Error! Could not reach the API. ' + error
        })
    }
  }
})
</script>
```

* [Axios](https://www.kancloud.cn/yunye/axios/234845) 是一个基于 promise 的 HTTP 库，可以用在浏览器和 node.js 中。

* [Lodash](https://www.lodashjs.com/) 是一个一致性、模块化、高性能的 JavaScript 实用工具库。

## [Class 与 Style 绑定](https://cn.vuejs.org/v2/guide/class-and-style.html)

字符串拼接麻烦且易错。因此，在将 v-bind 用于 class 和 style 时，Vue.js 做了专门的增强。表达式结果的类型除了字符串之外，还可以是对象或数组。

### [绑定 HTML Class](https://cn.vuejs.org/v2/guide/class-and-style.html#%E7%BB%91%E5%AE%9A-HTML-Class)

#### 对象语法

```JavaScript
<div v-bind:class="{ active: isActive }"></div>

<div
  class="static"
  v-bind:class="{ active: isActive, 'text-danger': hasError }"
></div>

data: {
  isActive: true,
  hasError: false
}
```

上面的语法表示 active 这个 class 存在与否将取决于数据 property isActive 的 truthiness。也可以在对象中传入更多字段来动态切换多个 class。此外，v-bind:class 指令也可以与普通的 class attribute 共存。

结果渲染为：

```JavaScript
<div class="static active"></div>
```

* 当 isActive 或者 hasError 变化时，class 列表将相应地更新。例如，如果 hasError 的值为 true，class 列表将变为 "static active text-danger"。

绑定的数据对象不必内联定义在模板里：

```JavaScript
<div v-bind:class="classObject"></div>

data: {
  classObject: {
    active: true,
    'text-danger': false
  }
}
```

* 進階應用 - 計算屬性

```JavaScript
<div v-bind:class="classObject"></div>

data: {
  isActive: true,
  error: null
},
computed: {
  classObject: function () {
    return {
      active: this.isActive && !this.error,
      'text-danger': this.error && this.error.type === 'fatal'
    }
  }
}
```

#### 数组语法

可以把一个数组传给 v-bind:class，以应用一个 class 列表：

```JavaScript
<div v-bind:class="[activeClass, errorClass]"></div>

data: {
  activeClass: 'active',
  errorClass: 'text-danger'
}
```

條件渲染

```JavaScript
<div v-bind:class="[isActive ? activeClass : '', errorClass]"></div>
```

多个条件 class 时这样写有些繁琐。所以在数组语法中也可以使用对象语法：

```JavaScript
<div v-bind:class="[{ active: isActive }, errorClass]"></div>
```

#### [用在组件上](https://cn.vuejs.org/v2/guide/class-and-style.html#%E7%94%A8%E5%9C%A8%E7%BB%84%E4%BB%B6%E4%B8%8A)

* 当在一个自定义组件上使用 class property 时，这些 class 将被添加到该组件的根元素上面。这个元素上已经存在的 class 不会被覆盖。

```JavaScript
Vue.component('my-component', {
  template: '<p class="foo bar">Hi</p>'
})

<my-component class="baz boo"></my-component>

//<p class="foo bar baz boo">Hi</p>

<my-component v-bind:class="{ active: isActive }"></my-component>

//<p class="foo bar active">Hi</p>
```

### [绑定内联样式](https://cn.vuejs.org/v2/guide/class-and-style.html#%E7%BB%91%E5%AE%9A%E5%86%85%E8%81%94%E6%A0%B7%E5%BC%8F)

#### 对象语法2

v-bind:style 。 CSS property 名可以用驼峰式 (camelCase) 或短横线分隔 (kebab-case，记得用引号括起来) 来命名：直接绑定到一个样式对象通常更好，这会让模板更清晰：

```JavaScript
<div v-bind:style="{ color: activeColor, fontSize: fontSize + 'px' }"></div>

data: {
  activeColor: 'red',
  fontSize: 30
}


<div v-bind:style="styleObject"></div>

data: {
  styleObject: {
    color: 'red',
    fontSize: '13px'
  }
}
```

同样的，对象语法常常结合返回对象的计算属性使用。

```JavaScript
<div v-bind:style="[baseStyles, overridingStyles]"></div>
```

#### 自动添加前缀

当 v-bind:style 使用需要添加浏览器引擎前缀的 CSS property 时，如 transform，Vue.js 会自动侦测并添加相应的前缀。

#### 多重值

从 2.3.0 起你可以为 style 绑定中的 property 提供一个包含多个值的数组，常用于提供多个带前缀的值，例如：

```JavaScript
<div :style="{ display: ['-webkit-box', '-ms-flexbox', 'flex'] }"></div>

//display: flex。
```

## [条件渲染](https://cn.vuejs.org/v2/guide/conditional.html)

> v-else

```JavaScript
<h1 v-if="awesome">Vue is awesome!</h1>
<h1 v-else>Oh no 😢</h1>

<div v-if="Math.random() > 0.5">
  Now you see me
</div>
<div v-else>
  Now you don't
</div>
```

* v-else 元素必须紧跟在带 v-if 或者 v-else-if 的元素的后面，否则它将不会被识别。

### 在 &lt;template> 元素上使用 v-if 条件渲染分组

* 因为 v-if 是一个指令，所以必须将它添加到一个元素上。但是如果想切换多个元素呢？此时可以把一个 &lt;template> 元素当做不可见的包裹元素，并在上面使用 v-if。最终的渲染结果将不包含 &lt;template> 元素。

```JavaScript
<template v-if="ok">
  <h1>Title</h1>
  <p>Paragraph 1</p>
  <p>Paragraph 2</p>
</template>
```

> v-else-if

```JavaScript
<div v-if="type === 'A'">
  A
</div>
<div v-else-if="type === 'B'">
  B
</div>
<div v-else-if="type === 'C'">
  C
</div>
<div v-else>
  Not A/B/C
</div>
```

### [用 key 管理可复用的元素](https://cn.vuejs.org/v2/guide/conditional.html#%E7%94%A8-key-%E7%AE%A1%E7%90%86%E5%8F%AF%E5%A4%8D%E7%94%A8%E7%9A%84%E5%85%83%E7%B4%A0)

Vue 会尽可能高效地渲染元素，通常会复用已有元素而不是从头开始渲染。

* 那么在下面的代码中切换 loginType 将不会清除用户已经输入的内容。因为两个模板使用了相同的元素，&lt;input> 不会被替换掉——仅仅是替换了它的 placeholder。

```JavaScript
<template v-if="loginType === 'username'">
  <label>Username</label>
  <input placeholder="Enter your username">
</template>
<template v-else>
  <label>Email</label>
  <input placeholder="Enter your email address">
</template>
```

* 这样也不总是符合实际需求，所以 Vue 为你提供了一种方式来表达 这两个元素是完全独立的，不要复用它们 。

> key attribute

```JavaScript
<template v-if="loginType === 'username'">
  <label>Username</label>
  <input placeholder="Enter your username" key="username-input">
</template>
<template v-else>
  <label>Email</label>
  <input placeholder="Enter your email address" key="email-input">
</template>
```

> v-show

```JavaScript
<h1 v-show="ok">Hello!</h1>
```

* 不同的是带有 v-show 的元素始终会被渲染并保留在 DOM 中。v-show 只是简单地切换元素的 CSS property display。注意，v-show 不支持 &lt;template> 元素，也不支持 v-else。

### [v-if vs v-show](https://cn.vuejs.org/v2/guide/conditional.html#v-if-vs-v-show)

v-if 是 真正 的条件渲染，因为它会确保在切换过程中条件块内的事件监听器和子组件适当地被销毁和重建。也是惰性的：如果在初始渲染时条件为假，则什么也不做——直到条件第一次变为真时，才会开始渲染条件块。

v-show 不管初始条件是什么，元素总是会被渲染，并且只是简单地基于 CSS 进行切换。

如果需要非常频繁地切换，则使用 v-show 较好；如果在运行时条件很少改变，则使用 v-if 较好。

### v-if 与 v-for 一起使用

> 永远不要把 v-if 和 v-for 同时用在同一个元素上。请将 v-if 移动至容器元素上 (比如 ul、ol)。

* [不推荐同时使用 v-if 和 v-for。请查阅风格指南以获取更多信息。](https://cn.vuejs.org/v2/style-guide/#%E9%81%BF%E5%85%8D-v-if-%E5%92%8C-v-for-%E7%94%A8%E5%9C%A8%E4%B8%80%E8%B5%B7%E5%BF%85%E8%A6%81)

* [当 v-if 与 v-for 一起使用时，v-for 具有比 v-if 更高的优先级。请查阅列表渲染指南以获取详细信息。](https://cn.vuejs.org/v2/guide/list.html#v-for-object)

## [列表渲染](https://cn.vuejs.org/v2/guide/list.html)

### 用 v-for 把一个数组对应为一组元素

v-for 指令需要使用 item in items 形式的特殊语法，其中 items 是源数据数组，而 item 则是被迭代的数组元素的别名。

```JavaScript
<ul id="example-1">
  <li v-for="item in items" :key="item.message">
    {{ item.message }}
  </li>
</ul>

var example1 = new Vue({
  el: '#example-1',
  data: {
    items: [
      { message: 'Foo' },
      { message: 'Bar' }
    ]
  }
})

// * Foo
// * Bar
```

在 v-for 块中，我们可以访问所有父作用域的 property。v-for 还支持一个可选的第二个参数，即当前项的索引。

```JavaScript
<ul id="example-2">
  <li v-for="(item, index) in items">
    {{ parentMessage }} - {{ index }} - {{ item.message }}
  </li>
</ul>

var example2 = new Vue({
  el: '#example-2',
  data: {
    parentMessage: 'Parent',
    items: [
      { message: 'Foo' },
      { message: 'Bar' }
    ]
  }
})

// * Parent - 0 - Foo
// * Parent - 1 - Bar
```

* 你也可以用 of 替代 in 作为分隔符，因为它更接近 JavaScript 迭代器的语法：

```JavaScript
<div v-for="item of items"></div>
```

### [在 v-for 里使用对象](https://cn.vuejs.org/v2/guide/list.html#%E5%9C%A8-v-for-%E9%87%8C%E4%BD%BF%E7%94%A8%E5%AF%B9%E8%B1%A1)

v-for 来遍历一个对象的 property。

```JavaScript
<ul id="v-for-object" class="demo">
  <li v-for="value in object">
    {{ value }}
  </li>
</ul>

new Vue({
  el: '#v-for-object',
  data: {
    object: {
      title: 'How to do lists in Vue',
      author: 'Jane Doe',
      publishedAt: '2016-04-10'
    }
  }
})

// * How to do lists in Vue
// * Jane Doe
// * 2016-04-10
```

提供第二个的参数为 property 名称 (也就是键名)：

```JavaScript
<div v-for="(value, name) in object">
  {{ name }}: {{ value }}
</div>

// title: How to do lists in Vue
// author: Jane Doe
// publishedAt: 2016-04-10
```

可以用第三个参数作为索引：

```JavaScript
<div v-for="(value, name, index) in object">
  {{ index }}. {{ name }}: {{ value }}
</div>

// 0. title: How to do lists in Vue
// 1. author: Jane Doe
// 2. publishedAt: 2016-04-10
```

### [维护状态](https://cn.vuejs.org/v2/guide/list.html#%E7%BB%B4%E6%8A%A4%E7%8A%B6%E6%80%81)

如果数据项的顺序被改变，Vue 将不会移动 DOM 元素来匹配数据项的顺序，而是就地更新每个元素，并且确保它们在每个索引位置正确渲染。

* 只适用于不依赖子组件状态或临时 DOM 状态 (例如：表单输入值) 的列表渲染输出。

* 为了给 Vue 一个提示，以便它能跟踪每个节点的身份，从而重用和重新排序现有元素，你需要为每项提供一个唯一 key attribute：

```JavaScript
<div v-for="item in items" v-bind:key="item.id">
  <!-- 内容 -->
</div>
```

* 不要使用对象或数组之类的非基本类型值作为 v-for 的 key。请用字符串或数值类型的值。

### 数组更新检测

#### 变更方法

Vue 将被侦听的数组的变更方法进行了包裹，所以它们也将会触发视图更新。这些被包裹过的方法包括：

* push()
* pop()
* shift()
* unshift()
* splice()
* sort()
* reverse()

> example1.items.push({ message: 'Baz' })

#### 替换数组

filter()、concat() 和 slice()。它们不会变更原始数组，而总是返回一个新数组。当使用非变更方法时，可以用新数组替换旧数组：

```JavaScript
example1.items = example1.items.filter(function (item) {
  return item.message.match(/Foo/)
})
```

* 你可能认为这将导致 Vue 丢弃现有 DOM 并重新渲染整个列表。幸运的是，事实并非如此。Vue 为了使得 DOM 元素得到最大范围的重用而实现了一些智能的启发式方法，所以用一个含有相同元素的数组去替换原来的数组是非常高效的操作。

### [显示过滤/排序后的结果](https://cn.vuejs.org/v2/guide/list.html#%E6%98%BE%E7%A4%BA%E8%BF%87%E6%BB%A4-%E6%8E%92%E5%BA%8F%E5%90%8E%E7%9A%84%E7%BB%93%E6%9E%9C)

```JavaScript
<li v-for="n in evenNumbers">{{ n }}</li>

data: {
  numbers: [ 1, 2, 3, 4, 5 ]
},
computed: {
  evenNumbers: function () {
    return this.numbers.filter(function (number) {
      return number % 2 === 0
    })
  }
}
```

在计算属性不适用的情况下 (例如，在[嵌套 v-for](#) 循环中) 你可以使用一个方法：

```JavaScript
<ul v-for="set in sets">
  <li v-for="n in even(set)">{{ n }}</li>
</ul>

data: {
  sets: [[ 1, 2, 3, 4, 5 ], [6, 7, 8, 9, 10]]
},
methods: {
  even: function (numbers) {
    return numbers.filter(function (number) {
      return number % 2 === 0
    })
  }
}
```

### 在 v-for 里使用值范围

```JavaScript
<div>
  <span v-for="n in 10">{{ n }} </span>
</div>

// 1 2 3 4 5 6 7 8 9 10
```

### 在 &lt;template> 上使用 v-for

```JavaScript
<ul>
  <template v-for="item in items">
    <li>{{ item.msg }}</li>
    <li class="divider" role="presentation"></li>
  </template>
</ul>
```

### [v-for 与 v-if 一同使用](https://cn.vuejs.org/v2/guide/list.html#v-for-%E4%B8%8E-v-if-%E4%B8%80%E5%90%8C%E4%BD%BF%E7%94%A8)

当它们处于同一节点，v-for 的优先级比 v-if 更高，这意味着 v-if 将分别重复运行于每个 v-for 循环中。当你只想为部分项渲染节点时，这种优先级的机制会十分有用，如下：

```JavaScript
<li v-for="todo in todos" v-if="!todo.isComplete">
  {{ todo }}
</li>

<ul v-if="todos.length">
  <li v-for="todo in todos">
    {{ todo }}
  </li>
</ul>
<p v-else>No todos left!</p>
```

### 在组件上使用 v-for

* 2.2.0+ 的版本里，当在组件上使用 v-for 时，key 现在是必须的。

```JavaScript
<my-component v-for="item in items" :key="item.id"></my-component>
```

* 任何数据都不会被自动传递到组件里，因为组件有自己独立的作用域。为了把迭代数据传递到组件里，我们要使用 prop：

```JavaScript
<my-component
  v-for="(item, index) in items"
  v-bind:item="item"
  v-bind:index="index"
  v-bind:key="item.id"
></my-component>
```

＊　不自动将 item 注入到组件里的原因是，这会使得组件与 v-for 的运作紧密耦合。明确组件数据的来源能够使组件在其他场合重复使用。

＊ [is attribute](https://stackoverflow.com/questions/27434431/what-is-html-is-attribute)

### todo-list demo

```JavaScript
<div id="todo-list-example">
  <form v-on:submit.prevent="addNewTodo">
    <label for="new-todo">Add a todo</label>
    <input
      v-model="newTodoText"
      id="new-todo"
      placeholder="E.g. Feed the cat"
    >
    <button>Add</button>
  </form>
  <ul>
    <li
      is="todo-item"
      v-for="(todo, index) in todos"
      v-bind:key="todo.id"
      v-bind:title="todo.title"
      v-on:remove="todos.splice(index, 1)"
    ></li>
  </ul>
</div>
```

```JavaScript
Vue.component('todo-item', {
  template: '\
    <li>\
      {{ title }}\
      <button v-on:click="$emit(\'remove\')">Remove</button>\
    </li>\
  ',
  props: ['title']
})

new Vue({
  el: '#todo-list-example',
  data: {
    newTodoText: '',
    todos: [
      {
        id: 1,
        title: 'Do the dishes',
      },
      {
        id: 2,
        title: 'Take out the trash',
      },
      {
        id: 3,
        title: 'Mow the lawn'
      }
    ],
    nextTodoId: 4
  },
  methods: {
    addNewTodo: function () {
      this.todos.push({
        id: this.nextTodoId++,
        title: this.newTodoText
      })
      this.newTodoText = ''
    }
  }
})
```

* 注意这里的 is="todo-item" attribute。这种做法在使用 DOM 模板时是十分必要的，因为在 &lt;ul> 元素内只有 &lt;li> 元素会被看作有效内容。这样做实现的效果与 &lt;todo-item> 相同，但是可以避开一些潜在的浏览器解析错误。查看 DOM 模板解析说明 来了解更多信息。

#### 小結6

這裡感覺非常重要類似 ng 的 @input @output 一樣，在　component 之間傳遞數據是一件非常重要的應用。

## [事件处理](https://cn.vuejs.org/v2/guide/events.html)

### 监听事件

可以用 v-on 指令监听 DOM 事件，并在触发时运行一些 JavaScript 代码。

```JavaScript
<div id="example-1">
  <button v-on:click="counter += 1">Add 1</button>
  <p>The button above has been clicked {{ counter }} times.</p>
</div>

var example1 = new Vue({
  el: '#example-1',
  data: {
    counter: 0
  }
})
```

### 事件处理方法

```JavaScript
<div id="example-2">
  <!-- `greet` 是在下面定义的方法名 -->
  <button v-on:click="greet">Greet</button>
</div>

var example2 = new Vue({
  el: '#example-2',
  data: {
    name: 'Vue.js'
  },
  // 在 `methods` 对象中定义方法
  methods: {
    greet: function (event) {
      // `this` 在方法里指向当前 Vue 实例
      alert('Hello ' + this.name + '!')
      // `event` 是原生 DOM 事件
      if (event) {
        alert(event.target.tagName)
      }
    }
  }
})

// 也可以用 JavaScript 直接调用方法
example2.greet() // => 'Hello Vue.js!'
```

### [内联处理器中的方法](https://cn.vuejs.org/v2/guide/events.html#%E5%86%85%E8%81%94%E5%A4%84%E7%90%86%E5%99%A8%E4%B8%AD%E7%9A%84%E6%96%B9%E6%B3%95)

```JavaScript
<div id="example-3">
  <button v-on:click="say('hi')">Say hi</button>
  <button v-on:click="say('what')">Say what</button>
</div>

new Vue({
  el: '#example-3',
  methods: {
    say: function (message) {
      alert(message)
    }
  }
})
```

有时也需要在内联语句处理器中访问原始的 DOM 事件。可以用特殊变量 $event 把它传入方法：

```JavaScript
<button v-on:click="warn('Form cannot be submitted yet.', $event)">
  Submit
</button>

// ...
methods: {
  warn: function (message, event) {
    // 现在我们可以访问原生事件对象
    if (event) {
      event.preventDefault()
    }
    alert(message)
  }
}
```

### 事件修饰符

* 在事件处理程序中调用 event.preventDefault() 或 event.stopPropagation() 是非常常见的需求。尽管我们可以在方法中轻松实现这点，但更好的方式是：方法只有纯粹的数据逻辑，而不是去处理 DOM 事件细节。

* .stop
* .prevent
* .capture
* .self
* .once
* .passive

```JavaScript
<!-- 阻止单击事件继续传播 -->
<a v-on:click.stop="doThis"></a>

<!-- 提交事件不再重载页面 -->
<form v-on:submit.prevent="onSubmit"></form>

<!-- 修饰符可以串联 -->
<a v-on:click.stop.prevent="doThat"></a>

<!-- 只有修饰符 -->
<form v-on:submit.prevent></form>

<!-- 添加事件监听器时使用事件捕获模式 -->
<!-- 即内部元素触发的事件先在此处理，然后才交由内部元素进行处理 -->
<div v-on:click.capture="doThis">...</div>

<!-- 只当在 event.target 是当前元素自身时触发处理函数 -->
<!-- 即事件不是从内部元素触发的 -->
<div v-on:click.self="doThat">...</div>

<!-- 点击事件将只会触发一次 -->
<a v-on:click.once="doThis"></a>

<!-- 滚动事件的默认行为 (即滚动行为) 将会立即触发 -->
<!-- 而不会等待 `onScroll` 完成  -->
<!-- 这其中包含 `event.preventDefault()` 的情况 -->
<div v-on:scroll.passive="onScroll">...</div>
```

* 使用修饰符时，顺序很重要；相应的代码会以同样的顺序产生。因此，用 v-on:click.prevent.self 会阻止所有的点击，而 v-on:click.self.prevent 只会阻止对元素自身的点击。

* 不像其它只能对原生的 DOM 事件起作用的修饰符，.once 修饰符还能被用到自定义的组件事件上。如果你还没有阅读关于组件的文档，现在大可不必担心。

* Vue 还对应 addEventListener 中的 passive 选项提供了 .passive 修饰符。

### [按键修饰符](https://cn.vuejs.org/v2/guide/events.html#%E6%8C%89%E9%94%AE%E4%BF%AE%E9%A5%B0%E7%AC%A6)

```JavaScript
<!-- 只有在 `key` 是 `Enter` 时调用 `vm.submit()` -->
<input v-on:keyup.enter="submit">
```

#### 小結7

有些地方真的看不懂也不知道怎麼查...

```JavaScript
你可以直接将 KeyboardEvent.key 暴露的任意有效按键名转换为 kebab-case 来作为修饰符。

<input v-on:keyup.page-down="onPageDown">
在上述示例中，处理函数只会在 $event.key 等于 PageDown 时被调用。
```

### [按键码](https://cn.vuejs.org/v2/guide/events.html#%E6%8C%89%E9%94%AE%E7%A0%81)

keyCode 的事件用法已经被废弃了并可能不会被最新的浏览器支持。为了在必要的情况下支持旧浏览器，Vue 提供了绝大多数常用的按键码的别名：

### 系统修饰键

可以用如下修饰符来实现仅在按下相应按键时才触发鼠标或键盘事件的监听器。

* .ctrl
* .alt
* .shift
* .meta

### .exact 修饰符

修饰符允许你控制由精确的系统修饰符组合触发的事件。

```JavaScript
<!-- 即使 Alt 或 Shift 被一同按下时也会触发 -->
<button v-on:click.ctrl="onClick">A</button>

<!-- 有且只有 Ctrl 被按下的时候才触发 -->
<button v-on:click.ctrl.exact="onCtrlClick">A</button>

<!-- 没有任何系统修饰符被按下的时候才触发 -->
<button v-on:click.exact="onClick">A</button>
```

### 鼠标按钮修饰符

* .left
* .right
* .middle

### [为什么在 HTML 中监听事件？](https://cn.vuejs.org/v2/guide/events.html#%E4%B8%BA%E4%BB%80%E4%B9%88%E5%9C%A8-HTML-%E4%B8%AD%E7%9B%91%E5%90%AC%E4%BA%8B%E4%BB%B6%EF%BC%9F)

## [表单输入绑定](https://cn.vuejs.org/v2/guide/forms.html)

### 基础用法

你可以用 v-model 指令在表单 &lt;input>、&lt;textarea> 及 &lt;select> 元素上创建双向数据绑定。它会根据控件类型自动选取正确的方法来更新元素。尽管有些神奇，但 v-model 本质上不过是语法糖。它负责监听用户的输入事件以更新数据，并对一些极端场景进行一些特殊处理。

* v-model 会忽略所有表单元素的 value、checked、selected attribute 的初始值而总是将 Vue 实例的数据作为数据来源。你应该通过 JavaScript 在组件的 data 选项中声明初始值。v-model 在内部为不同的输入元素使用不同的 property 并抛出不同的事件：

text 和 textarea 元素使用 value property 和 input 事件；

checkbox 和 radio 使用 checked property 和 change 事件；

select 字段将 value 作为 prop 并将 change 作为事件。

-------------------------------------------------

### 文本

```JavaScript
<input v-model="message" placeholder="edit me">
<p>Message is: {{ message }}</p>
```

### 多行文本

```JavaScript
<span>Multiline message is:</span>
<p style="white-space: pre-line;">{{ message }}</p>
<br>
<textarea v-model="message" placeholder="add multiple lines"></textarea>
```

### 复选框

```JavaScript
<input type="checkbox" id="checkbox" v-model="checked">
<label for="checkbox">{{ checked }}</label>

<div id='example-3'>
  <input type="checkbox" id="jack" value="Jack" v-model="checkedNames">
  <label for="jack">Jack</label>
  <input type="checkbox" id="john" value="John" v-model="checkedNames">
  <label for="john">John</label>
  <input type="checkbox" id="mike" value="Mike" v-model="checkedNames">
  <label for="mike">Mike</label>
  <br>
  <span>Checked names: {{ checkedNames }}</span>
</div>

new Vue({
  el: '#example-3',
  data: {
    checkedNames: []
  }
})
```

### 单选按钮

```JavaScript
<div id="example-4">
  <input type="radio" id="one" value="One" v-model="picked">
  <label for="one">One</label>
  <br>
  <input type="radio" id="two" value="Two" v-model="picked">
  <label for="two">Two</label>
  <br>
  <span>Picked: {{ picked }}</span>
</div>

new Vue({
  el: '#example-4',
  data: {
    picked: ''
  }
})
```

### 选择框

* 如果 v-model 表达式的初始值未能匹配任何选项，&lt;select> 元素将被渲染为 未选中 状态。在 iOS 中，这会使用户无法选择第一个选项。因为这样的情况下，iOS 不会触发 change 事件。因此，更推荐像上面这样提供一个值为空的禁用选项。

```JavaScript
<div id="example-5">
  <select v-model="selected">
    <option disabled value="">请选择</option>
    <option>A</option>
    <option>B</option>
    <option>C</option>
  </select>
  <span>Selected: {{ selected }}</span>
</div>

new Vue({
  el: '...',
  data: {
    selected: ''
  }
})

<div id="example-6">
  <select v-model="selected" multiple style="width: 50px;">
    <option>A</option>
    <option>B</option>
    <option>C</option>
  </select>
  <br>
  <span>Selected: {{ selected }}</span>
</div>

new Vue({
  el: '#example-6',
  data: {
    selected: []
  }
})

<select v-model="selected">
  <option v-for="option in options" v-bind:value="option.value">
    {{ option.text }}
  </option>
</select>
<span>Selected: {{ selected }}</span>

new Vue({
  el: '...',
  data: {
    selected: 'A',
    options: [
      { text: 'One', value: 'A' },
      { text: 'Two', value: 'B' },
      { text: 'Three', value: 'C' }
    ]
  }
})
```

### [值绑定](https://cn.vuejs.org/v2/guide/forms.html#%E5%80%BC%E7%BB%91%E5%AE%9A)

* 但是有时我们可能想把值绑定到 Vue 实例的一个动态 property 上，这时可以用 v-bind 实现，并且这个 property 的值可以不是字符串。

```JavaScript
<!-- 当选中时，`picked` 为字符串 "a" -->
<input type="radio" v-model="picked" value="a">

<!-- `toggle` 为 true 或 false -->
<input type="checkbox" v-model="toggle">

<!-- 当选中第一个选项时，`selected` 为字符串 "abc" -->
<select v-model="selected">
  <option value="abc">ABC</option>
</select>
```

### 复选框-

```JavaScript
<input
  type="checkbox"
  v-model="toggle"
  true-value="yes"
  false-value="no"
>
// 当选中时
vm.toggle === 'yes'
// 当没有选中时
vm.toggle === 'no'
```

* 这里的 true-value 和 false-value attribute 并不会影响输入控件的 value attribute，因为浏览器在提交表单时并不会包含未被选中的复选框。如果要确保表单中这两个值中的一个能够被提交，(即 yes 或 no )，请换用单选按钮。

### 单选按钮-

```JavaScript
<input type="radio" v-model="pick" v-bind:value="a">
// 当选中时
vm.pick === vm.a
```

### 选择框的选项

```JavaScript
<select v-model="selected">
    <!-- 内联对象字面量 -->
  <option v-bind:value="{ number: 123 }">123</option>
</select>

// 当选中时
typeof vm.selected // => 'object'
vm.selected.number // => 123
```

### [修饰符](https://cn.vuejs.org/v2/guide/forms.html#%E4%BF%AE%E9%A5%B0%E7%AC%A6)

* .lazy
* .number
* .trim

```JavaScript
<!-- 在 change 时而非 input 时更新 -->
<input v-model.lazy="msg">

<!-- 自动将用户的输入值转为数值类型 -->
<input v-model.number="age" type="number">

<!-- 自动过滤用户输入的首尾空白字符 -->
<input v-model.trim="msg">
```

### 在组件上使用 v-model

HTML 原生的输入元素类型并不总能满足需求。幸好，Vue 的组件系统允许你创建具有完全自定义行为且可复用的输入组件。这些输入组件甚至可以和 v-model 一起使用！

要了解更多，请参阅组件指南中的[自定义输入组件](https://cn.vuejs.org/v2/guide/components-custom-events.html#%E8%87%AA%E5%AE%9A%E4%B9%89%E7%BB%84%E4%BB%B6%E7%9A%84-v-model)。

## [组件基础](https://cn.vuejs.org/v2/guide/components.html)

### 基本示例

```JavaScript
// 定义一个名为 button-counter 的新组件
Vue.component('button-counter', {
  data: function () {
    return {
      count: 0
    }
  },
  template: '<button v-on:click="count++">You clicked me {{ count }} times.</button>'
})
```

组件是可复用的 Vue 实例，且带有一个名字：在这个例子中是 &lt;button-counter>。我们可以在一个通过 new Vue 创建的 Vue 根实例中，把这个组件作为自定义元素来使用：

```JavaScript
<div id="components-demo">
  <button-counter></button-counter>
</div>

new Vue({ el: '#components-demo' })
```

* 因为组件是可复用的 Vue 实例，所以它们与 new Vue 接收相同的选项，例如 data、computed、watch、methods 以及生命周期钩子等。仅有的例外是像 el 这样根实例特有的选项。

### [组件的复用](https://cn.vuejs.org/v2/guide/components.html#%E7%BB%84%E4%BB%B6%E7%9A%84%E5%A4%8D%E7%94%A8)

每个组件都会各自独立维护它的 count。因为你每用一次组件，就会有一个它的新实例被创建。

#### data 必须是一个函数

因此每个实例可以维护一份被返回对象的独立的拷贝：

```JavaScript
//data: {
// count: 0
//}

data: function () {
  return {
    count: 0
  }
}
```

### [组件的组织](https://cn.vuejs.org/v2/guide/components.html#%E7%BB%84%E4%BB%B6%E7%9A%84%E7%BB%84%E7%BB%87)

为了能在模板中使用，这些组件必须先注册以便 Vue 能够识别。Vue 有两种组件的注册类型：全局注册和局部注册。至此，我们的组件都只是通过 Vue.component 全局注册的：

```JavaScript
Vue.component('my-component-name', {
  // ... options ...
})
```

[全局注册的组件可以用在其被注册之后的任何 (通过 new Vue) 新创建的 Vue 根实例，也包括其组件树中的所有子组件的模板中。](https://cn.vuejs.org/v2/guide/components-registration.html)

### [通过 Prop 向子组件传递数据](https://cn.vuejs.org/v2/guide/components.html#%E9%80%9A%E8%BF%87-Prop-%E5%90%91%E5%AD%90%E7%BB%84%E4%BB%B6%E4%BC%A0%E9%80%92%E6%95%B0%E6%8D%AE)

Prop 是你可以在组件上注册的一些自定义 attribute。当一个值传递给一个 prop attribute 的时候，它就变成了那个组件实例的一个 property。为了给博文组件传递一个标题，我们可以用一个 props 选项将其包含在该组件可接受的 prop 列表中：

```JavaScript
Vue.component('blog-post', {
  props: ['title'],
  template: '<h3>{{ title }}</h3>'
})

<blog-post title="My journey with Vue"></blog-post>
<blog-post title="Blogging with Vue"></blog-post>
<blog-post title="Why Vue is so fun"></blog-post>
```

```JavaScript
new Vue({
  el: '#blog-post-demo',
  data: {
    posts: [
      { id: 1, title: 'My journey with Vue' },
      { id: 2, title: 'Blogging with Vue' },
      { id: 3, title: 'Why Vue is so fun' }
    ]
  }
})

<blog-post
  v-for="post in posts"
  v-bind:key="post.id"
  v-bind:title="post.title"
></blog-post>
```

### [单个根元素](https://cn.vuejs.org/v2/guide/components.html#%E5%8D%95%E4%B8%AA%E6%A0%B9%E5%85%83%E7%B4%A0)

然而如果你在模板中尝试这样写，Vue 会显示一个错误，并解释道 every component must have a single root element (每个组件必须只有一个根元素)。你可以将模板的内容包裹在一个父元素内，来修复这个问题，例如：

```JavaScript
//<h3>{{ title }}</h3>
//<div v-html="content"></div>

<div class="blog-post">
  <h3>{{ title }}</h3>
  <div v-html="content"></div>
</div>
```

```JavaScript
//<blog-post
//  v-for="post in posts"
//  v-bind:key="post.id"
//  v-bind:title="post.title"
//  v-bind:content="post.content"
//  v-bind:publishedAt="post.publishedAt"
//  v-bind:comments="post.comments"
//></blog-post>

<blog-post
  v-for="post in posts"
  v-bind:key="post.id"
  v-bind:post="post"
></blog-post>

Vue.component('blog-post', {
  props: ['post'],
  template: `
    <div class="blog-post">
      <h3>{{ post.title }}</h3>
      <div v-html="post.content"></div>
    </div>
  `
})

// 现在，不论何时为 post 对象添加一个新的 property，它都会自动地在 <blog-post> 内可用。
```

### [监听子组件事件](https://cn.vuejs.org/v2/guide/components.html#%E7%9B%91%E5%90%AC%E5%AD%90%E7%BB%84%E4%BB%B6%E4%BA%8B%E4%BB%B6)

当点击这个按钮时，我们需要告诉父级组件放大所有博文的文本。

* 父

```JavaScript
new Vue({
  el: '#blog-posts-events-demo',
  data: {
    posts: [/* ... */],
    postFontSize: 1
  }
})

<div id="blog-posts-events-demo">
  <div :style="{ fontSize: postFontSize + 'em' }">
    <blog-post
      v-for="post in posts"
      v-bind:key="post.id"
      v-bind:post="post"
      v-on:enlarge-text="postFontSize += 0.1"
    ></blog-post>
  </div>
</div>
```

> v-on:enlarge-text="postFontSize += 0.1"

父级组件可以像处理 native DOM 事件一样通过 v-on 监听子组件实例的任意事件：

* 子

```JavaScript
Vue.component('blog-post', {
  props: ['post'],
  template: `
    <div class="blog-post">
      <h3>{{ post.title }}</h3>
      <button v-on:click="$emit('enlarge-text')">
        Enlarge text
      </button>
      <div v-html="post.content"></div>
    </div>
  `
})
```

> v-on:click="$emit('enlarge-text')"

子组件可以通过调用内建的 [$emit](https://cn.vuejs.org/v2/api/#vm-emit) 方法并传入事件名称来触发一个事件：

* vm.$emit( eventName, […args] ) 触发当前实例上的事件。附加参数都会传给监听器回调。

### [使用事件抛出一个值](https://cn.vuejs.org/v2/guide/components.html#%E4%BD%BF%E7%94%A8%E4%BA%8B%E4%BB%B6%E6%8A%9B%E5%87%BA%E4%B8%80%E4%B8%AA%E5%80%BC)

让 &lt;blog-post> 组件决定它的文本要放大多少。可以使用 $emit 的第二个参数来提供这个值：

```JavaScript
<button v-on:click="$emit('enlarge-text', 0.1)">
  Enlarge text
</button>
```

然后当在父级组件监听这个事件的时候，我们可以通过 $event 访问到被抛出的这个值：

```JavaScript
<blog-post
  ...
  v-on:enlarge-text="postFontSize += $event"
></blog-post>
```

或者，如果这个事件处理函数是一个方法：那么这个值将会作为第一个参数传入这个方法：

```JavaScript
<blog-post
  ...
  v-on:enlarge-text="onEnlargeText"
></blog-post>

methods: {
  onEnlargeText: function (enlargeAmount) {
    this.postFontSize += enlargeAmount
  }
}
```

### [在组件上使用 v-model](https://cn.vuejs.org/v2/guide/components.html#%E5%9C%A8%E7%BB%84%E4%BB%B6%E4%B8%8A%E4%BD%BF%E7%94%A8-v-model)

自定义事件也可以用于创建支持 v-model 的自定义输入组件。记住：

```JavaScript
<input v-model="searchText">

//等价于：

<input
  v-bind:value="searchText"
  v-on:input="searchText = $event.target.value"
>
```

当用在组件上时，v-model 则会这样：

为了让它正常工作，这个组件内的 &lt;input> 必须：

将其 value attribute 绑定到一个名叫 value 的 prop 上
在其 input 事件被触发时，将新的值通过自定义的 input 事件抛出

```JavaScript
//<custom-input
//  v-bind:value="searchText"
//  v-on:input="searchText = $event"
//></custom-input>

<custom-input v-model="searchText"></custom-input>

Vue.component('custom-input', {
  props: ['value'],
  template: `
    <input
      v-bind:value="value"
      v-on:input="$emit('input', $event.target.value)"
    >
  `
})
```

### [通过插槽分发内容](https://cn.vuejs.org/v2/guide/components.html#%E9%80%9A%E8%BF%87%E6%8F%92%E6%A7%BD%E5%88%86%E5%8F%91%E5%86%85%E5%AE%B9)

> &lt;slot>

### [动态组件](https://cn.vuejs.org/v2/guide/components.html#%E5%8A%A8%E6%80%81%E7%BB%84%E4%BB%B6)

> &lt;component>

有的时候，在不同组件之间进行动态切换是非常有用的，比如在一个多标签的界面里：

```JavaScript
<!-- 组件会在 `currentTabComponent` 改变时改变 -->
<component v-bind:is="currentTabComponent"></component>
```

在上述示例中，currentTabComponent 可以包括

* 已注册组件的名字，或
* 一个组件的选项对象

#### 小結8

這裡我猜就是跟 React 學的...，變成新的 tag 的感覺。

### [解析 DOM 模板时的注意事项](https://cn.vuejs.org/v2/guide/components.html#%E8%A7%A3%E6%9E%90-DOM-%E6%A8%A1%E6%9D%BF%E6%97%B6%E7%9A%84%E6%B3%A8%E6%84%8F%E4%BA%8B%E9%A1%B9)

* 有些 HTML 元素，诸如 &lt;ul>、&lt;ol>、&lt;table> 和 &lt;select>，对于哪些元素可以出现在其内部是有严格限制的。而有些元素，诸如 &lt;li>、&lt;tr> 和 &lt;option>，只能出现在其它某些特定的元素内部。

```JavaScript
//<table>
//  <blog-post-row></blog-post-row>
//</table>

<table>
  <tr is="blog-post-row"></tr>
</table>
```

需要注意的是如果我们从以下来源使用模板的话，这条限制是不存在的：

* 字符串 (例如：template: '...')
* 单文件组件 (.vue)
* &lt;script type="text/x-template">

## 總結

看完　Basis 可以感受到確實比 ng 輕，但這也只是基本應用而已，延伸應用加下去感覺跟 ng 比也是不相上下，

且許多應用如 ng 一樣，可能要邊做遇到問題時在了解才會學得快。