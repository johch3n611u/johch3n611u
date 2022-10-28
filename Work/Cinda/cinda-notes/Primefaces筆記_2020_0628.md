# Primefaces

* Prime
  * 動詞
    * 灌注, 填裝
  * 形容詞
    * 根本, 最初的
  * 名詞
    * 青春, 初期

[譯自英文- PrimeFaces 是土耳其公司 PrimeTek Informatics 創建的用於基於 JavaServer Faces 的應用程序的開源用戶界面組件庫。](https://en.wikipedia.org/wiki/PrimeFaces)

[![IMG](https://camo.githubusercontent.com/672700033e038c7e9c627d1bc6168da132996a81/68747470733a2f2f7777772e7072696d6566616365732e6f72672f77702d636f6e74656e742f75706c6f6164732f323031362f31302f7072696d655f6c6f676f5f6e65772e706e67)Github](https://github.com/primefaces/primefaces)

## [PrimeNG 0.1.0 - Since 1 Feb 2016](https://github.com/primefaces/primeng/releases?after=v0.3.0)

Now 2020.0628 PrimeNG 9.1.0

PrimeNG 是 Angular 的豐富 UI 組件的集合。同時還有 PrimeVue 與 PrimeReact 版本，算是蠻有力的開源 UI 組件庫。

使用方法類似之前開發 SideProject 時所使用的 <https://material.angular.io/>，但因為對於前端組件應用較不熟悉，所以有很多內容還要更加強，

相對於比較有使用經驗的 BS 組件來說 <https://getbootstrap.com/docs/4.5/components/alerts/> 多了更多的操作方式，

官方文件 <https://www.primefaces.org/primeng/showcase/#/>

## 使用

如同 Angular 其他組件一樣，都要透過 npm 引入至本地的 node_modules packages file 才能使用，

並且利用 TypeScript / ES6 的 import、export 模組概念導入，

![alt](https://qph.fs.quoracdn.net/main-qimg-1bb226be271cbb969e55513384f2401d.webp)

## 依存關係

```HTML
<link rel="stylesheet" type="text/css" href="/node_modules/primeicons/primeicons.css" />
<!-- Prime 圖標 -->
<link rel="stylesheet" type="text/css" href="/node_modules/primeng/resources/themes/nova-light/theme.css" />
<!-- 主題 CSS -->
<link rel="stylesheet" type="text/css" href="/node_modules/primeng/resources/primeng.min.css" />
<!-- 組件 CSS -->
Component
* Schedule - FullCalendar 4.1.0
* Editor - Quill Editor
* GMap - Google Maps
* Charts - Charts.js 2.7.x
* Captcha - Google Recaptcha
```

所以也是套用蠻多其他輪子組成汽車的...

> 從 Angular 4 開始 animations 有自己的模塊，因此您需要將 BrowserAnimationsModule 導入到您的應用程序中。如果您希望全局禁用動畫，請導入 NoopAnimationsModule。
