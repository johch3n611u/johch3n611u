# 總結

整個官方網站都瀏覽過了，剩下的就是實作的熟練度了，再來是 plugins 的問題，

因為不像 ng 是包起來一整包，有點類似 react ，所以說實話感覺學習曲線還是在那並沒有比較容易的感覺...

不太確定是否是因為我 react 與 ng 都沒有碰得很熟練與深度的關係...

總之接下來應該會繼續從 router　之後接續是 vuex 再來是 Quasar

--------------------------------------------------------------

# [Vue Advanced](https://cn.vuejs.org/v2/guide/components-registration.html) Version 2.X

## 组件名

建議 "字母全小写且必须包含一个连字符"。这会帮助你避免和当前以及未来的 HTML 元素相冲突。

my-component-name

## 全域 vs 局部 聲明

### 全域

```JavaScript
Vue.component(
    'my-component-name',
    {/*...*/}
    )

new Vue({el:'#App'})
```

### 局部

```JavaScript
var ComponentName = {/*...*/}
new Vue({
    el:'#App',
    components:{
        'my-component-name' : ComponentName
    }
})
```

### ES5+ Module

```JavaScript
import Component from './Component'

export default {
    components: {
    Component,
  },
  // ...
}
```

### [自动化全局注册 Vue CLI 3+ 實例](https://github.com/chrisvfritz/vue-enterprise-boilerplate/blob/master/src/components/_globals.js)

基礎組件 Vue Cli 3+

入口文件 src/main.js 導入

自動化全域聲明

import Vue from 'vue'

import upperFirst from './upperFirst'

import camelCase from './camelCase'

require.context

類似反應式設計、ng基礎組件載入，透過 wepbpack，靜態分析使用內容作最小打包

## [Prop](https://cn.vuejs.org/v2/guide/components-props.html)

### 大小寫

### Prop 类型

数字 布尔值 数组 对象

### 传递静态或动态 Prop

即便 `false` 是静态的，我们仍然需要 `v-bind` 来告诉 Vue 这是一个 JavaScript 表达式而不是一个字符串。

```JavaScript
post: {
  id: 1,
  title: 'My Journey with Vue'
}

<blog-post v-bind="post"></blog-post>
//等价于：
<blog-post
  v-bind:id="post.id"
  v-bind:title="post.title"
></blog-post>
```

### 单向数据流

類似 React 只支援從上到下的資料流

### [Prop 验证检查、类型检查](https://cn.vuejs.org/v2/guide/components-props.html#Prop-%E9%AA%8C%E8%AF%81)

一些類似 MVC 內　Module 的模型屬性，並顯示於控制台警告方便開發。　<https://docs.microsoft.com/zh-tw/aspnet/core/mvc/models/validation?view=aspnetcore-3.1#validation-attributes>

### 非 Prop 的 Attribute

e.g. 通过一个 Bootstrap 插件使用了一个第三方的 &lt;bootstrap-date-input> 组件，这个插件需要在其 &lt;input> 上用到一个 data-date-picker attribute。

### 替换/合并已有的 Attribute

对于绝大多数 attribute 来说，从外部提供给组件的值会替换掉组件内部设置好的值。 type="text" 就会替换掉 type="date"

庆幸的是，class 和 style attribute 会稍微智能一些，即两边的值会被合并起来，从而得到最终的值：form-control date-picker-theme-dark。

#### 禁用 Attribute 继承

不希望组件的根元素继承 attribute，你可以在组件的选项中设置 inheritAttrs: false

配合实例的 $attrs property 使用，该 property 包含了传递给一个组件的 attribute 名和 attribute 值

就可以手动决定这些 attribute 会被赋予哪个元素。

```JavaScript
Vue.component('base-input', {
  inheritAttrs: false,
  props: ['label', 'value'],
  template: `
    <label>
      {{ label }}
      <input
        v-bind="$attrs"
        v-bind:value="value"
        v-on:input="$emit('input', $event.target.value)"
      >
    </label>
  `
})
```

## [自定义事件](https://cn.vuejs.org/v2/guide/components-custom-events.html)

### 事件名

不同于组件和 prop，事件名不存在任何自动化的大小写转换。而是触发的事件名需要完全匹配监听这个事件所用的名称。

因此，我们推荐你始终使用 kebab-case 的事件名。

Kebeb Case(Kebeb-Case)

Kebeb本身是烤肉串的意思，變數就像烤肉串一樣串在一起。

命名方式：在單字間加入破折號hyphen。

例子：good-to-eat, cart-item。

常用場景：通常會用在網址。如本篇的naming-convention

### 自定义组件的 v-model

一个组件上的 v-model 默认会利用名为 value 的 prop 和名为 input 的事件，但单选框、复选框等类型的输入控件可能会将 value attribute 用于[不同的目的](https://developer.mozilla.org/en-US/docs/Web/HTML/Element/input/checkbox#Value)。model 选项可以用来避免这样的冲突：

```JavaScript
Vue.component('base-checkbox', {
  model: {
    prop: 'checked',
    event: 'change'
  },
  props: {
    checked: Boolean
  },
  template: `
    <input
      type="checkbox"
      v-bind:checked="checked"
      v-on:change="$emit('change', $event.target.checked)"
    >
  `
})

//<base-checkbox v-model="lovingVue"></base-checkbox>
```

### 将原生事件绑定到组件

使用 v-on 的 .native 修饰符

```JavaScript
<base-input v-on:focus.native="onFocus"></base-input>
// 可能的失敗案例　<base-input> 组件可能做了如下重构，所以根元素实际上是一个 <label> 元素
<label>
  {{ label }}
  <input
    v-bind="$attrs"
    v-bind:value="value"
    v-on:input="$emit('input', $event.target.value)"
  >
</label>
// 为了解决这个问题，Vue 提供了一个 $listeners property，它是一个对象，里面包含了作用在这个组件上的所有监听器。
{
  focus: function (event) { /* ... */ }
  input: function (value) { /* ... */ },
}
// 现在 <base-input> 组件是一个完全透明的包裹器了，也就是说它可以完全像一个普通的 <input> 元素一样使用了：所有跟它相同的 attribute 和监听器都可以工作。
Vue.component('base-input', {
  inheritAttrs: false,
  props: ['label', 'value'],
  computed: {
    inputListeners: function () {
      var vm = this
      // `Object.assign` 将所有的对象合并为一个新对象
      return Object.assign({},
        // 我们从父级添加所有的监听器
        this.$listeners,
        // 然后我们添加自定义监听器，
        // 或覆写一些监听器的行为
        {
          // 这里确保组件配合 `v-model` 的工作
          input: function (event) {
            vm.$emit('input', event.target.value)
          }
        }
      )
    }
  },
  template: `
    <label>
      {{ label }}
      <input
        v-bind="$attrs"
        v-bind:value="value"
        v-on="inputListeners"
      >
    </label>
  `
})
```

### .sync 修饰符

在有些情况下，我们可能需要对一个 prop 进行'双向绑定'。不幸的是，真正的双向绑定会带来维护上的问题，因为子组件可以变更父组件，且在父组件和子组件都没有明显的变更来源。

* 这也是为什么我们推荐以 update:myPropName 的模式触发事件取而代之。

```JavaScript
this.$emit('update:title', newTitle)

<text-document
  v-bind:title="doc.title"
  v-on:update:title="doc.title = $event"
></text-document>
// 等價於
<text-document v-bind:title.sync="doc.title"></text-document>
```

＊　注意带有 .sync 修饰符的 v-bind 不能和表达式一起使用 (例如 v-bind:title.sync=’doc.title + ‘!’’ 是无效的)。取而代之的是，你只能提供你想要绑定的 property 名，类似 v-mode＊

# 小結1

vue 相對於 ng、react 發展較晚所以吸收容納了許多類似的架構還有融入像 ts 的 es5+ 內容，理解容易但就要看實作熟練度了。

## [插槽](https://cn.vuejs.org/v2/guide/components-slots.html)

Vue 实现了一套内容分发的 API，这套 API 的设计灵感源自 Web Components 规范草案，将 &lt;slot> 元素作为承载分发内容的出口

```JavaScript
<navigation-link url="/profile">
  Your Profile
</navigation-link>

<a
  v-bind:href="url"
  class="nav-link"
>
  <slot></slot>
</a>
```

当组件渲染的时候，&lt;slot>&lt;/slot> 将会被替换为'Your Profile'。插槽内可以包含任何模板代码，包括 HTML 甚至其它的组件

如果 &lt;navigation-link> 没有包含一个 &lt;slot> 元素，则该组件起始标签和结束标签之间的任何内容都会被抛弃。

### 编译作用域

* 父级模板里的所有内容都是在父级作用域中编译的；子模板里的所有内容都是在子作用域中编译的。

想在一个插槽中使用数据时

```JavaScript
<navigation-link url="/profile">
  Logged in as {{ user.name }}
</navigation-link>
// 该插槽跟模板的其它地方一样可以访问相同的实例 property (也就是相同的'作用域')，而不能访问 <navigation-link> 的作用域。例如 url 是访问不到的
<navigation-link url="/profile">
  Clicking here will send you to: {{ url }}
  <!--
  这里的 `url` 会是 undefined，因为其 (指该插槽的) 内容是
  _传递给_ <navigation-link> 的而不是
  在 <navigation-link> 组件*内部*定义的。
  -->
</navigation-link>
```

### 后备内容

有时为一个插槽设置具体的后备 (也就是默认的) 内容是很有用的，它只会在没有提供内容的时候被渲染。

```JavaScript
<button type="submit">
  <slot>Submit</slot>
</button>
```

### 具名插槽

* 一个不带 name 的 &lt;slot> 出口会带有隐含的名字'default'。

```JavaScript
<div class="container">
  <header>
    <slot name="header"></slot>
  </header>
  <main>
    <slot></slot>
  </main>
  <footer>
    <slot name="footer"></slot>
  </footer>
</div>

<base-layout>
  <template v-slot:header>
    <h1>Here might be a page title</h1>
  </template>

  <p>A paragraph for the main content.</p>
  <p>And another one.</p>

  <template v-slot:footer>
    <p>Here's some contact info</p>
  </template>
</base-layout>
```

* 注意 v-slot 只能添加在 &lt;template> 上 (只有一种例外情况)，这一点和已经废弃的 slot attribute 不同。

### 作用域插槽 插槽 prop

有时让插槽内容能够访问子组件中才有的数据是很有用的。

```JavaScript
<current-user>
  <template v-slot:default="slotProps">
    {{ slotProps.user.firstName }}
  </template>
</current-user>

<span>
  <slot v-bind:user="user">
    {{ user.lastName }}
  </slot>
</span>
```

独占默认插槽的缩写语法

```JavaScript
<current-user>
  <template v-slot:default="slotProps">
    {{ slotProps.user.firstName }}
  </template>

  <template v-slot:other="otherSlotProps">
    ...
  </template>
</current-user>
```

### 解构插槽 Prop

作用域插槽的内部工作原理是将你的插槽内容包括在一个传入单个参数的函数里：

```JavaScript
function (slotProps) {
  // 插槽内容
}

// 这意味着 v-slot 的值实际上可以是任何能够作为函数定义中的参数的 JavaScript 表达式。所以在支持的环境下 (单文件组件或现代浏览器)，你也可以使用 ES2015 解构来传入具体的插槽 prop，

<current-user v-slot="{ user: person }">
  {{ person.firstName }}
</current-user>
```

### 动态插槽名

```JavaScript
<base-layout>
  <template v-slot:[dynamicSlotName]>
    ...
  </template>
</base-layout>
```

### 具名插槽的缩写

跟 v-on 和 v-bind 一样，v-slot 也有缩写，即把参数之前的所有内容 (v-slot:) 替换为字符 #。

```JavaScript
<base-layout>
  <template #header>
    <h1>Here might be a page title</h1>
  </template>

  <p>A paragraph for the main content.</p>
  <p>And another one.</p>

  <template #footer>
    <p>Here's some contact info</p>
  </template>
</base-layout>
```

### [其它示例](https://cn.vuejs.org/v2/guide/components-slots.html#%E5%85%B6%E5%AE%83%E7%A4%BA%E4%BE%8B)

### [废弃了的语法](https://cn.vuejs.org/v2/guide/components-slots.html#%E5%85%B6%E5%AE%83%E7%A4%BA%E4%BE%8B)

插槽 prop 允许我们将插槽转换为可复用的模板，这些模板可以基于输入的 prop 渲染出不同的内容。这在设计封装数据逻辑同时允许父级组件自定义部分布局的可复用组件时是最有用的。

# 小結2

插槽感覺很像 ng 的 app-root，但因為 ng 也學個完整但使用不深入，

這裡更多的是　vue slot 的相對應深入的技術，

但實務上如何使用可能就要看 bilibili 的那篇教學了。

## [动态组件 & 异步组件](https://cn.vuejs.org/v2/guide/components-dynamic-async.html)

### 在动态组件上使用 keep-alive

之前曾经在一个多标签的界面中使用 is attribute 来切换不同的组件：

```JavaScript
<component v-bind:is="currentTabComponent"></component>
```

当在这些组件之间切换的时候，你有时会想保持这些组件的状态，以避免反复重渲染导致的性能问题。

* 重新创建动态组件的行为通常是非常有用的，但是在这个案例中，我们更希望那些标签的组件实例能够被在它们第一次被创建的时候缓存下来。为了解决这个问题，我们可以用一个 &lt;keep-alive> 元素将其动态组件包裹起来。

```JavaScript
<!-- 失活的组件将会被缓存！-->
<keep-alive>
  <component v-bind:is="currentTabComponent"></component>
</keep-alive>
```

### 异步组件

* 在大型应用中，我们可能需要将应用分割成小一些的代码块，并且只在需要的时候才从服务器加载一个模块。为了简化，Vue 允许你以一个工厂函数的方式定义你的组件，这个工厂函数会异步解析你的组件定义。Vue 只有在这个组件需要被渲染的时候才会触发该工厂函数，且会把结果缓存起来供未来重渲染。

```JavaScript
Vue.component('async-example', function (resolve, reject) {
  setTimeout(function () {
    // 向 `resolve` 回调传递组件定义
    resolve({
      template: '<div>I am async!</div>'
    })
  }, 1000)
})
//  webpack 的 code-splitting 功能
Vue.component('async-webpack-example', function (resolve) {
  // 这个特殊的 `require` 语法将会告诉 webpack
  // 自动将你的构建代码切割成多个包，这些包
  // 会通过 Ajax 请求加载
  require(['./my-async-component'], resolve)
})
// es5+ Promise
Vue.component(
  'async-webpack-example',
  // 这个 `import` 函数会返回一个 `Promise` 对象。
  () => import('./my-async-component')
)
// 局部注册
new Vue({
  // ...
  components: {
    'my-component': () => import('./my-async-component')
  }
})
```

* Browserify 是一個開源JavaScript工具，允許開發人員編寫可在瀏覽器中使用的Node.js樣式的模塊。

#### 处理加载状态

```JavaScript
const AsyncComponent = () => ({
  // 需要加载的组件 (应该是一个 `Promise` 对象)
  component: import('./MyComponent.vue'),
  // 异步组件加载时使用的组件
  loading: LoadingComponent,
  // 加载失败时使用的组件
  error: ErrorComponent,
  // 展示加载时组件的延时时间。默认值是 200 (毫秒)
  delay: 200,
  // 如果提供了超时时间且组件加载也超时了，
  // 则使用加载失败时使用的组件。默认值是：`Infinity`
  timeout: 3000
})
```

* 注意如果你希望在 Vue Router 的路由组件中使用上述语法的话，你必须使用 Vue Router 2.4.0+ 版本。

## [处理边界情况](https://cn.vuejs.org/v2/guide/components-edge-cases.html)

* 这里记录的都是和处理边界情况有关的功能，即一些需要对 Vue 的规则做一些小调整的特殊情况。不过注意这些功能都是有劣势或危险的场景的。我们会在每个案例中注明，所以当你使用每个功能的时候请稍加留意。

### 访问元素 & 组件

在绝大多数情况下，我们最好不要触达另一个组件实例内部或手动操作 DOM 元素。不过也确实在一些情况下做这些事情是合适的。

#### [访问根实例](https://cn.vuejs.org/v2/guide/components-edge-cases.html#%E8%AE%BF%E9%97%AE%E6%A0%B9%E5%AE%9E%E4%BE%8B)

* 对于 demo 或非常小型的有少量组件的应用来说这是很方便的。不过这个模式扩展到中大型应用来说就不然了。因此在绝大多数情况下，我们强烈推荐使用 Vuex 来管理应用的状态。

#### [访问父级组件实例](https://cn.vuejs.org/v2/guide/components-edge-cases.html#%E8%AE%BF%E9%97%AE%E7%88%B6%E7%BA%A7%E7%BB%84%E4%BB%B6%E5%AE%9E%E4%BE%8B)

* 在绝大多数情况下，触达父级组件会使得你的应用更难调试和理解，尤其是当你变更了父级组件的数据的时候。当我们稍后回看那个组件的时候，很难找出那个变更是从哪里发起的。

* 很快它就会失控。这也是我们针对需要向任意更深层级的组件提供上下文信息时推荐依赖注入的原因。

#### [访问子组件实例或子元素](https://cn.vuejs.org/v2/guide/components-edge-cases.html#%E8%AE%BF%E9%97%AE%E5%AD%90%E7%BB%84%E4%BB%B6%E5%AE%9E%E4%BE%8B%E6%88%96%E5%AD%90%E5%85%83%E7%B4%A0)

尽管存在 prop 和事件，有的时候你仍可能需要在 JavaScript 里直接访问一个子组件。为了达到这个目的，你可以通过 ref 这个 attribute 为子组件赋予一个 ID 引用。

* $refs 只会在组件渲染完成之后生效，并且它们不是响应式的。这仅作为一个用于直接操作子组件的 逃生舱 ——你应该避免在模板或计算属性中访问 $refs。

#### [依赖注入](https://cn.vuejs.org/v2/guide/components-edge-cases.html#%E4%BE%9D%E8%B5%96%E6%B3%A8%E5%85%A5)

* 然而，依赖注入还是有负面影响的。它将你应用程序中的组件与它们当前的组织方式耦合起来，使重构变得更加困难。同时所提供的 property 是非响应式的。这是出于设计的考虑，因为使用它们来创建一个中心化规模化的数据跟使用 $root做这件事都是不够好的。如果你想要共享的这个 property 是你的应用特有的，而不是通用化的，或者如果你想在祖先组件中更新所提供的数据，那么这意味着你可能需要换用一个像 Vuex 这样真正的状态管理方案了。

#### [程序化的事件侦听器](https://cn.vuejs.org/v2/guide/components-edge-cases.html#%E7%A8%8B%E5%BA%8F%E5%8C%96%E7%9A%84%E4%BA%8B%E4%BB%B6%E4%BE%A6%E5%90%AC%E5%99%A8)

现在，你已经知道了 $emit 的用法，它可以被 v-on 侦听，但是 Vue 实例同时在其事件接口中提供了其它的方法。我们可以：

* 通过 $on(eventName, eventHandler) 侦听一个事件
* 通过 $once(eventName, eventHandler) 一次性侦听一个事件
* 通过 $off(eventName, eventHandler) 停止侦听一个事件

```JavaScript
// 一次性将这个日期选择器附加到一个输入框上
// 它会被挂载到 DOM 上。
mounted: function () {
  // Pikaday 是一个第三方日期选择器的库
  this.picker = new Pikaday({
    field: this.$refs.input,
    format: 'YYYY-MM-DD'
  })
},
// 在组件被销毁之前，
// 也销毁这个日期选择器。
beforeDestroy: function () {
  this.picker.destroy()
}
// 程序化的侦听器
mounted: function () {
  this.attachDatepicker('startDateInput')
  this.attachDatepicker('endDateInput')
},
methods: {
  attachDatepicker: function (refName) {
    var picker = new Pikaday({
      field: this.$refs[refName],
      format: 'YYYY-MM-DD'
    })

    this.$once('hook:beforeDestroy', function () {
      picker.destroy()
    })
  }
}
```

* 注意 Vue 的事件系统不同于浏览器的 EventTarget API。尽管它们工作起来是相似的，但是 $emit、$on, 和 $off 并不是 dispatchEvent、addEventListener 和 removeEventListener 的别名。

#### [循环引用](https://cn.vuejs.org/v2/guide/components-edge-cases.html#%E5%BE%AA%E7%8E%AF%E5%BC%95%E7%94%A8)

组件是可以在它们自己的模板中调用自身的。不过它们只能通过 name 选项来做这件事：

稍有不慎，递归组件就可能导致无限循环：

类似上述的组件将会导致 max stack size exceeded 错误，所以请确保递归调用是条件性的 (例如使用一个最终会得到 false 的 v-if)。

### [组件之间的循环引用](https://cn.vuejs.org/v2/guide/components-edge-cases.html#%E7%BB%84%E4%BB%B6%E4%B9%8B%E9%97%B4%E7%9A%84%E5%BE%AA%E7%8E%AF%E5%BC%95%E7%94%A8)

### [模板定义的替代品](https://cn.vuejs.org/v2/guide/components-edge-cases.html#%E7%BB%84%E4%BB%B6%E4%B9%8B%E9%97%B4%E7%9A%84%E5%BE%AA%E7%8E%AF%E5%BC%95%E7%94%A8)

＊ 当 inline-template 这个特殊的 attribute 出现在一个子组件上时，这个组件将会使用其里面的内容作为模板，而不是将其作为被分发的内容。这使得模板的撰写工作更加灵活。

不过，inline-template 会让模板的作用域变得更加难以理解。所以作为最佳实践，请在组件内优先选择 template 选项或 .vue 文件里的一个 &lt;template> 元素来定义模板。

* X-Template 另一个定义模板的方式是在一个 &lt;script> 元素中，并为其带上 text/x-template 的类型，然后通过一个 id 将模板引用过去。

* 这些可以用于模板特别大的 demo 或极小型的应用，但是其它情况下请避免使用，因为这会将模板和该组件的其它定义分离开。

### [控制更新](https://cn.vuejs.org/v2/guide/components-edge-cases.html#%E6%8E%A7%E5%88%B6%E6%9B%B4%E6%96%B0)

#### 强制更新 $forceUpdate

如果你发现你自己需要在 Vue 中做一次强制更新，99.9% 的情况，是你在某个地方做错了事。

* 通过 v-once 创建低开销的静态组件

# 小結3

這裡提及很多進階應用，但本身前端並不是那麼地在行，很多都沒接觸過，可能 bilibili 影片內會有實例會比較深刻了解，

內容已經涉及到更多深層的 javascript 而不是類似 jq 的基礎應用了...

大概理解前端那麼多錢大概要會甚麼了，感覺以上這些至少是基礎，而哪時能用在實作上且是看到東西就想到，大概就能領那樣的薪水了。

# 过渡 & 动画

## [进入/离开 & 列表过渡](https://cn.vuejs.org/v2/guide/transitions.html)

## [状态过渡](https://cn.vuejs.org/v2/guide/transitioning-state.html)

# 小結4

這個層級面對的是更多動畫的應用，感覺應該是要使用到或者前面都上手後再深入會比較好，接近於數字選染圖 animation 了而不是純粹的 transform 。

# 可复用性 & 组合

## [混入](https://cn.vuejs.org/v2/guide/mixins.html)

混入 (mixin) 提供了一种非常灵活的方式，来分发 Vue 组件中的可复用功能。一个混入对象可以包含任意组件选项。当组件使用混入对象时，所有混入对象的选项将被 混合 进入该组件本身的选项。

```JavaScript
// 定义一个混入对象
var myMixin = {
  created: function () {
    this.hello()
  },
  methods: {
    hello: function () {
      console.log('hello from mixin!')
    }
  }
}

// 定义一个使用混入对象的组件
var Component = Vue.extend({
  mixins: [myMixin]
})

var component = new Component() // => "hello from mixin!"
```

## 选项合并

当组件和混入对象含有同名选项时，这些选项将以恰当的方式进行 合并 。

比如，数据对象在内部会进行递归合并，并在发生冲突时以组件数据优先。

同名钩子函数将合并为一个数组，因此都将被调用。另外，混入对象的钩子将在组件自身钩子之前调用。

值为对象的选项，例如 methods、components 和 directives，将被合并为同一个对象。两个对象键名冲突时，取组件对象的键值对。

## 全局混入

* 混入也可以进行全局注册。使用时格外小心！一旦使用全局混入，它将影响每一个之后创建的 Vue 实例。使用恰当时，这可以用来为自定义选项注入处理逻辑。

* 请谨慎使用全局混入，因为它会影响每个单独创建的 Vue 实例 (包括第三方组件)。大多数情况下，只应当应用于自定义选项，就像上面示例一样。推荐将其作为插件发布，以避免重复应用混入。

## 自定义选项合并策略

自定义选项将使用默认策略，即简单地覆盖已有值。如果想让自定义选项以自定义逻辑合并，可以向 Vue.config.optionMergeStrategies 添加一个函数

## [自定义指令](https://cn.vuejs.org/v2/guide/custom-directive.html)

Vue2.0 中，代码复用和抽象的主要形式是组件。然而，有的情况下，你仍然需要对普通 DOM 元素进行底层操作，这时候就会用到自定义指令。

```JavaScript
// 注册一个全局自定义指令 `v-focus`
Vue.directive('focus', {
  // 当被绑定的元素插入到 DOM 中时……
  inserted: function (el) {
    // 聚焦元素
    el.focus()
  }
})
// 局部指令
directives: {
  focus: {
    // 指令的定义
    inserted: function (el) {
      el.focus()
    }
  }
}

// <input v-focus>
```

### 钩子函数

一个指令定义对象可以提供如下几个钩子函数 (均为可选)：

* bind：只调用一次，指令第一次绑定到元素时调用。在这里可以进行一次性的初始化设置。

* inserted：被绑定元素插入父节点时调用 (仅保证父节点存在，但不一定已被插入文档中)。

* update：所在组件的 VNode 更新时调用，但是可能发生在其子 VNode 更新之前。指令的值可能发生了改变，也可能没有。但是你可以通过比较更新前后的值来忽略不必要的模板更新 (详细的钩子函数参数见下)。

我们会在稍后讨论渲染函数时介绍更多 VNodes 的细节。

* componentUpdated：指令所在组件的 VNode 及其子 VNode 全部更新后调用。

* unbind：只调用一次，指令与元素解绑时调用。

### [钩子函数参数](https://cn.vuejs.org/v2/guide/custom-directive.html#%E9%92%A9%E5%AD%90%E5%87%BD%E6%95%B0%E5%8F%82%E6%95%B0)

### [动态指令参数](https://cn.vuejs.org/v2/guide/custom-directive.html#%E5%8A%A8%E6%80%81%E6%8C%87%E4%BB%A4%E5%8F%82%E6%95%B0)

### 函数简写

### [对象字面量](https://cn.vuejs.org/v2/guide/custom-directive.html#%E5%AF%B9%E8%B1%A1%E5%AD%97%E9%9D%A2%E9%87%8F)

## [渲染函数 & JSX](https://cn.vuejs.org/v2/guide/render-function.html)

Vue 推荐在绝大多数情况下使用模板来创建你的 HTML。然而在一些场景中，你真的需要 JavaScript 的完全编程的能力。这时你可以用渲染函数，它比模板更接近编译器。

### [节点、树以及虚拟 DOM](https://cn.vuejs.org/v2/guide/render-function.html#%E8%8A%82%E7%82%B9%E3%80%81%E6%A0%91%E4%BB%A5%E5%8F%8A%E8%99%9A%E6%8B%9F-DOM)

### [虚拟 DOM](https://cn.vuejs.org/v2/guide/render-function.html#%E8%99%9A%E6%8B%9F-DOM)

Vue 通过建立一个虚拟 DOM 来追踪自己要如何改变真实 DOM。

### [深入数据对象](https://cn.vuejs.org/v2/guide/render-function.html#%E6%B7%B1%E5%85%A5%E6%95%B0%E6%8D%AE%E5%AF%B9%E8%B1%A1)

### [完整示例](https://cn.vuejs.org/v2/guide/render-function.html#%E5%AE%8C%E6%95%B4%E7%A4%BA%E4%BE%8B)

### [约束](https://cn.vuejs.org/v2/guide/render-function.html#%E7%BA%A6%E6%9D%9F)

* VNode 必须唯一 组件树中的所有 VNode 必须是唯一的。

### [使用 JavaScript 代替模板功能](https://cn.vuejs.org/v2/guide/render-function.html#%E4%BD%BF%E7%94%A8-JavaScript-%E4%BB%A3%E6%9B%BF%E6%A8%A1%E6%9D%BF%E5%8A%9F%E8%83%BD)

### [事件 & 按键修饰符](https://cn.vuejs.org/v2/guide/render-function.html#%E4%BA%8B%E4%BB%B6-amp-%E6%8C%89%E9%94%AE%E4%BF%AE%E9%A5%B0%E7%AC%A6)

### [插槽](https://cn.vuejs.org/v2/guide/render-function.html#%E6%8F%92%E6%A7%BD)

* 你可以通过 this.$slots 访问静态插槽的内容，每个插槽都是一个 VNode 数组：

### [JSX](https://cn.vuejs.org/v2/guide/render-function.html#JSX)

### [函数式组件](https://cn.vuejs.org/v2/guide/render-function.html#%E5%87%BD%E6%95%B0%E5%BC%8F%E7%BB%84%E4%BB%B6)

之前创建的锚点标题组件是比较简单，没有管理任何状态，也没有监听任何传递给它的状态，也没有生命周期方法。实际上，它只是一个接受一些 prop 的函数。在这样的场景下，我们可以将组件标记为 functional，这意味它无状态 (没有响应式数据)，也没有实例 (没有 this 上下文)

```JavaScript
Vue.component('my-component', {
  functional: true,
  // Props 是可选的
  props: {
    // ...
  },
  // 为了弥补缺少的实例
  // 提供第二个参数作为上下文
  render: function (createElement, context) {
    // ...
  }
})
```

* 注意：在 2.3.0 之前的版本中，如果一个函数式组件想要接收 prop，则 props 选项是必须的。在 2.3.0 或以上的版本中，你可以省略 props 选项，所有组件上的 attribute 都会被自动隐式解析为 prop。

当使用函数式组件时，该引用将会是 HTMLElement，因为他们是无状态的也是无实例的。

* props：提供所有 prop 的对象
* children：VNode 子节点的数组
* slots：一个函数，返回了包含所有插槽的对象
* scopedSlots：(2.6.0+) 一个暴露传入的作用域插槽的对象。也以函数形式暴露普通插槽。
* data：传递给组件的整个数据对象，作为 createElement 的第二个参数传入组件
* parent：对父组件的引用
* listeners：(2.3.0+) 一个包含了所有父组件为当前组件注册的事件监听器的对象。这是 data.on 的一个别名。
* injections：(2.3.0+) 如果使用了 inject 选项，则该对象包含了应当被注入的 property。

### [向子元素或子组件传递 attribute 和事件](https://cn.vuejs.org/v2/guide/render-function.html#%E5%90%91%E5%AD%90%E5%85%83%E7%B4%A0%E6%88%96%E5%AD%90%E7%BB%84%E4%BB%B6%E4%BC%A0%E9%80%92-attribute-%E5%92%8C%E4%BA%8B%E4%BB%B6)

### [slots() 和 children 对比](https://cn.vuejs.org/v2/guide/render-function.html#slots-%E5%92%8C-children-%E5%AF%B9%E6%AF%94)

### [模板编译](https://cn.vuejs.org/v2/guide/render-function.html#%E6%A8%A1%E6%9D%BF%E7%BC%96%E8%AF%91)

Vue 的模板实际上被编译成了渲染函数。这是一个实现细节，通常不需要关心。但如果你想看看模板的功能具体是怎样被编译的，可能会发现会非常有意思。

# 小結5

深入数据对象 這裡都是些更深入的應用，類似 ng 的各種不同裝飾器，react 有沒有這個部份我就不知道了。

使用 JavaScript 代替模板功能 這裡很像 react 的原理，但 vue 最後都要　new 了不知道為何還需要

其餘的部分跟之前的結論一樣，感覺都是要藉由實例去了解，就要看教學影片了。

## [插件](https://cn.vuejs.org/v2/guide/plugins.html)

插件通常用来为 Vue 添加全局功能。插件的功能范围没有严格的限制

1. 添加全局方法或者 property。如：vue-custom-element
1. 添加全局资源：指令/过滤器/过渡等。如 vue-touch
1. 通过全局混入来添加一些组件选项。如 vue-router
1. 添加 Vue 实例方法，通过把它们添加到 Vue.prototype 上实现。
1. 一个库，提供自己的 API，同时提供上面提到的一个或多个功能。如 vue-router

### [使用插件](https://cn.vuejs.org/v2/guide/plugins.html#%E4%BD%BF%E7%94%A8%E6%8F%92%E4%BB%B6)

通过全局方法 Vue.use() 使用插件。它需要在你调用 new Vue() 启动应用之前完成

也可以传入一个可选的选项对象 Vue.use(MyPlugin, { someOption: true })

Vue.use 会自动阻止多次注册相同插件，届时即使多次调用也只会注册一次该插件。

Vue.js 官方提供的一些插件 (例如 vue-router) 在检测到 Vue 是可访问的全局变量时会自动调用 Vue.use()。然而在像 CommonJS 这样的模块环境中，你应该始终显式地调用 Vue.use()

* CommonJS是一個專案，其目標是為JavaScript在網頁瀏覽器之外建立模組約定。建立這個專案的主要原因是當時缺乏普遍可接受形式的JavaScript指令碼模組單元

# [awesome-vue](https://github.com/vuejs/awesome-vue#components--libraries) 集合了大量由社区贡献的插件和库。

## [开发插件](https://cn.vuejs.org/v2/guide/plugins.html#%E5%BC%80%E5%8F%91%E6%8F%92%E4%BB%B6)

## [过滤器](https://cn.vuejs.org/v2/guide/filters.html)

Vue.js 允许你自定义过滤器，可被用于一些常见的文本格式化。过滤器可以用在两个地方：双花括号插值和 v-bind 表达式 (后者从 2.1.0+ 开始支持)。过滤器应该被添加在 JavaScript 表达式的尾部，由 管道 符号指示

# 小結6

插件這邊指的應該是 vue plugins 而不是普通的 js libraries ，

libraries 似乎要藉由 cli 結構類似 ng module 方式用 es5+ export npm install 引入

[好像也可以不用 cli 引入](https://www.itread01.com/p/978556.html) <https://codesandbox.io/s/github/vuejs/vuejs.org/tree/master/src/v2/examples/vue-20-wrapper-component?from-embed>

<https://cn.vuejs.org/v2/examples/>

过滤器 其實就跟 ng pipe 相同原理

# 工具

## [单文件组件](https://cn.vuejs.org/v2/guide/single-file-components.html)

在很多 Vue 项目中，我们使用 Vue.component 来定义全局组件，紧接着用 new Vue({ el: '#container '}) 在每个页面内指定一个容器元素。

缺點

* 全局定义 (Global definitions) 强制要求每个 component 中的命名不得重复
* 字符串模板 (String templates) 缺乏语法高亮，在 HTML 有多行的时候，需要用到丑陋的 \
* 不支持 CSS (No CSS support) 意味着当 HTML 和 JavaScript 组件化时，CSS 明显被遗漏
* 没有构建步骤 (No build step) 限制只能使用 HTML 和 ES5 JavaScript，而不能使用预处理器，如 Pug (formerly Jade) 和 Babel

文件扩展名为 .vue 的 single-file components (单文件组件) 为以上所有问题提供了解决方法，并且还可以使用 webpack 或 Browserify 等构建工具。

<https://cli.vuejs.org/zh/>

# 小結7

单文件组件　跟 react 很類似但不知道需不需要引入　Babel.js 再來是可能涉及像是 ng cli 的 vue cli

果真　稍微看了一下　vue cli 也像　ng cli 必須部屬 dist 才是真的靜態網頁的內容...

## [单元测试](https://cn.vuejs.org/v2/guide/unit-testing.html)

Vue CLI 拥有开箱即用的通过 Jest 或 Mocha 进行单元测试的内置选项。我们还有官方的 Vue Test Utils 提供更多详细的指引和自定义设置。

## [TypeScript 支持](https://cn.vuejs.org/v2/guide/typescript.html)

Vue CLI 提供了内建的 TypeScript 工具支持。

## [生产环境部署](https://cn.vuejs.org/v2/guide/deployment.html)

以下大多数内容在你使用 Vue CLI 时都是默认开启的。该章节仅跟你自定义的构建设置有关。

# 規模化

## [路由](https://cn.vuejs.org/v2/guide/routing.html)

<https://router.vuejs.org/zh/>

如果你只需要非常简单的路由而不想引入一个功能完整的路由库，可以像这样动态渲染一个页面级的组件

结合 HTML5 History API，你可以建立一个麻雀虽小五脏俱全的客户端路由器。可以直接看实例应用

```JavaScript
const NotFound = { template: '<p>Page not found</p>' }
const Home = { template: '<p>home page</p>' }
const About = { template: '<p>about page</p>' }

const routes = {
  '/': Home,
  '/about': About
}

new Vue({
  el: '#app',
  data: {
    currentRoute: window.location.pathname
  },
  computed: {
    ViewComponent () {
      return routes[this.currentRoute] || NotFound
    }
  },
  render (h) { return h(this.ViewComponent) }
})
```

# 小結8

router 這寫得太．．．　可能要去插件官網研究了，如果像 ng 的多簡單阿...單組件外連就能在那處理路由邏輯不知道 vue router　會是什麼狀況

## [状态管理](https://cn.vuejs.org/v2/guide/state-management.html)

<https://vuex.vuejs.org/zh/>

配套調適工具

<https://chrome.google.com/webstore/detail/vuejs-devtools/nhdogjmejiglipccpnnnanhbledajbpd>

经常被忽略的是，Vue 应用中原始 data 对象的实际来源——当访问数据对象时，一个 Vue 实例只是简单的代理访问。所以，如果你有一处需要被多个实例间共享的状态，可以简单地通过维护一份数据来实现共享

现在当 sourceOfTruth 发生变更，vmA 和 vmB 都将自动地更新它们的视图。子组件们的每个实例也会通过 this.$root.$data 去访问。现在我们有了唯一的数据来源，但是，调试将会变为噩梦。任何时间，我们应用中的任何部分，在任何数据改变后，都不会留下变更过的记录。

为了解决这个问题，我们采用一个简单的 store 模式：

接着我们继续延伸约定，组件不允许直接变更属于 store 实例的 state，而应执行 action 来分发 (dispatch) 事件通知 store 去改变，我们最终达成了 Flux 架构。这样约定的好处是，我们能够记录所有 store 中发生的 state 变更，同时实现能做到记录变更、保存状态快照、历史回滚/时光旅行的先进的调试工具。

说了一圈其实又回到了 Vuex，如果你已经读到这儿，或许可以去尝试一下！

# 小結9

總而言之這裡還是說服你要去看類似 redux 的狀態管理框架 flux 而你不看還是不知道如何應用...

## [服务端渲染](https://cn.vuejs.org/v2/guide/ssr.html)

<https://ssr.vuejs.org/zh/> like Angular Universal

# 小結10

這裡直接推薦兩種框架去實作 ssr 這件事情...那其實跟 ng 還不是一樣，還有跟最近了解到一個成大大神的 react 的　canner　類似...

1. Nuxt.js
2. Quasar Framework

## [安全](https://cn.vuejs.org/v2/guide/security.html)

后端协作

HTTP 安全漏洞，诸如伪造跨站请求 (CSRF/XSRF) 和跨站脚本注入 (XSSI)，都是后端重点关注的方向，因此并不是 Vue 所担心的。尽管如此，和后端团队交流学习如何和他们的 API 最好地进行交互，例如在表单提交时提交 CSRF token，永远是件好事。

# [内在 深入响应式原理](https://cn.vuejs.org/v2/guide/reactivity.html)

# 小結11

這裡感覺跟 ng 很類似較偏自動響應式，而 react 則是要主動觸發，其餘的大概是在講　vue 的設計構想原理之類，在還沒玩轉前看這個好像沒實質意義。

# [迁移](https://cn.vuejs.org/v2/guide/migration.html)

# [更多](https://cn.vuejs.org/v2/guide/comparison.html)
