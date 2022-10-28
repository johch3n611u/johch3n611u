---

<iframe src="https://docs.google.com/presentation/d/e/2PACX-1vRnl-hlXmR_3SW928Il9WuKBYRVJIyFcKbswdCohITfJUDavNNur35DEAb7u7i1opspL6GnboPRPSZc/embed?start=true&loop=true&delayms=3000" frameborder="0" width="960" height="569" allowfullscreen="true" mozallowfullscreen="true" webkitallowfullscreen="true"></iframe>

<https://stackoverflow.com/questions/1365407/c-sharp-code-to-validate-email-address>

<https://rileycoder.wordpress.com/2020/01/20/net-gmail%E7%99%BC%E9%80%81%E4%BF%A1%E4%BB%B6%E5%A4%B1%E6%95%97%E3%80%8Csmtp-%E4%BC%BA%E6%9C%8D%E5%99%A8%E9%9C%80%E8%A6%81%E5%AE%89%E5%85%A8%E9%80%A3%E6%8E%A5%EF%BC%8C%E6%88%96%E7%94%A8%E6%88%B6/>

<https://yukaii.tw/blog/2018/04/13/use-gmail-smtp-without-your-own-smtp-server/>

<https://docs.google.com/presentation/d/1IEZ3Ow6EekNYDxqzI9toHEgPiagYbZTiffCKjdfXha0/edit#slide=id.ga4518fb1b2_2_7>

<https://blog.turn.tw/?p=2821>

---

# Meeting.SoftwareTesting

<https://www.google.com/search?sxsrf=ALeKk02q7Z6HFIfP8SbvtRlLexhwJM_McA%3A1602689099601&ei=SxiHX9SXJLSWr7wP9_GL0A8&q=%E5%96%AE%E5%85%83%E6%B8%AC%E8%A9%A6+c%23+%E7%AF%84%E4%BE%8B&oq=%E5%96%AE%E5%85%83%E6%B8%AC%E8%A9%A6+c%23&gs_lcp=CgZwc3ktYWIQAxgBMgIIADIECAAQHjIECAAQHjIICAAQCBAHEB4yBggAEAgQHjIGCAAQCBAeMgYIABAIEB4yBggAEAgQHjIGCAAQCBAeOgYIABAHEB46BAgAEENQp1dY2G5glnhoAHAAeACAAcsBiAHXBpIBBjEyLjAuMZgBAKABAaoBB2d3cy13aXrAAQE&sclient=psy-ab>

19:16 請問一下，我確定看看我理解的對不對，

我們目前 unit test 實際應用就是，我們單子會進到 PAMAI 關卡 ， 這時 透過 PAMAI Console 程式 調用測試 目的是要通過，第三方 IDM 系統 ， 當這個測試是成功的時候，才會調用 PAM 去跑這張單子的 Approve 與 Close

看了一下網路資料，AG有單元測試，.net也有測試專案，我們實際上應該是只有用到像上上面那樣

但demo應該沒辦法，在想是不是大綱可以是

概念講完，
講實際應用，
在寫一個簡單的 AG測試或 .net 測試專案

但這兩個好像啟動方式都是透過 debug 然後產結果，好像沒辦法用在實際應用上，這樣有關係嗎？

20:33 實際應用很簡單
你可以寫起單 call create的測試
20:33  我們起單成功會回傳什麼
20:34  Status code=0就算成功

20:35  等於是也寫一支 console ?

20:35  不是
20:35  測試專案不等於console
20:37  Pamai那邊按執行那個consile不是測試專案喔
20:37  那是應用程式

20:37 像 PAMAI 是 Console ? 測試專案好像要用別種方式啟動，好像還是要打開 Visual Studio

20:38 測試專案就是要用vs跑
20:38 你先找別人的測試專案怎麼做的

20:38 ok 我再多看一些資料

20:39 應該說先找看看有沒有別人實作.net unittest的影片 你看一遍別人怎麼操作的會比較知道

20:41 好，目前只有看 AG 單元測試，好像比較簡單一點 都單獨在 spec 檔案內寫，但是也沒說實際應用可能會是怎樣，我再去看看 .net unittes ，在理一下大綱看 demo 要怎實作

20:42 先處理.net的
20:42 Ag的放後面

<https://www.itread01.com/content/1548807503.html>

UAT SIT

## 大綱

1. 理論概念簡介
2. 單元測試 Angular
3. 單元測試 CSharp
4. 可能嘗試 端對端測試或是整合測試

## 理論概念簡介

軟體測試：在規定的條件下對程式進行操作，以發現程式錯誤，衡量軟體品質，並對其是否能滿足設計要求進行評估的過程。

有許多方法

### 進程分類

#### Alpha 驗證測試

通常是在開發單位由開發人員與測試的測試人員，以類比或實際操作性的方式進行驗證測試。

#### Beta 確認測試 ( Close、Open )

由公眾參與的測試，真實的環境中以實際的資料來執行測試，以確認效能，系統執行有效率，系統復原與備份作業正常。

### 方法分類

#### 黑箱測試

測試應用程式的功能，而不是其內部結構或運作。測試者不需具備應用程式的程式碼、內部結構和程式語言的專門知識。測試者只需知道什麼是系統應該做的事，即當鍵入一個特定的輸入，可得到一定的輸出。

#### 白盒測試

測試應用程式的內部結構或運作，而不是測試應用程式的功能（即黑箱測試）。在白盒測試時，以程式語言的角度來設計測試案例。可以應用於單元測試（unit testing）、整合測試（integration testing）和系統的軟體測試流程，測試者輸入資料驗證資料流在程式中的流動路徑，並確定適當的輸出，類似測試電路中的節點。

### 類型分類

#### 功能.測試

按照測試軟體的各個功能劃分進行有條理的測試，在功能測試部分要保證測試項覆蓋所有功能和各種功能條件組合。

#### 系統.測試

對一個完整的軟體以用戶的角度來進行測試，系統測試和功能測試的區別是，系統測試利用的所有測試資料和測試的方法都要類比成和用戶的實際使用環境完全一樣，測試的軟體也是經過系統整合以後的完整軟體系統，而不是在功能測試階段利用的每個功能模組單獨編譯後生成的可執行程式。

#### 極限值.測試

對軟體在各種特殊條件，特殊環境下能否正常執行和軟體的效能進行測試。
特殊條件一般指的是軟體規定的最大值，最小值，以及在超過最大、最小值條件下的測試。
特殊環境一般指的是軟體執行的機器處於CPU高負荷，或是網路高負荷狀態下的測試，根據軟體的不同，特殊環境也有過不同。

#### 效能.測試

效能測試是對軟體效能的評價。簡單的說，軟體效能衡量的是軟體具有的回應及時度能力。因此，效能測試是採用測試手段對軟體的回應及時性進行評價的一種方式。根據軟體的不同類型，效能測試的側重點也不同。

#### 壓力.測試

壓力測試要求進行超過規定效能指標的測試。例如一個網站設計容量是100個人同時點擊，壓力測試就要是採用120個同時點擊的條件測試。壓力測試的通常判斷準則：1. 系統能夠恢復。2. 壓力過程中不要有明顯效能下降。

### 階段分類

#### 單元測試

單元測試是對軟體組成單元進行測試，其目的是檢驗軟體基本組成單位的正確性，測試的物件是軟體設計的最小單位：函式。

#### 整合測試

整合測試也稱綜合測試、組裝測試、聯合測試，將程式模組採用適當的整合策略組裝起來，對系統的介面及整合後的功能進行正確性檢測的測試工作。其主要目的是檢查軟體單位之間的介面是否正確，整合測試的物件是已經經過單元測試的模組。

#### 系統測試

系統測試主要包括功能測試、介面測試、可靠性測試、易用性測試、效能測試。 功能測試主要針對包括功能可用性、功能實現程度（功能流程&業務流程、資料處理&業務資料處理）方面測試。

#### 回歸測試

回歸測試指在軟體維護階段，為了檢測代碼修改而引入的錯誤所進行的測試活動。回歸測試是軟體維護階段的重要工作，有研究表明，回歸測試帶來的耗費占軟體生命周期的1/3總費用以上。

與普通的測試不同，在回歸測試過程開始的時候，測試者有一個完整的測試用例集可供使用，因此，如何根據代碼的修改情況對已有測試用例集進行有效的復用是回歸測試研究的重要方向，此外，回歸測試的研究方向還涉及自動化工具，物件導向回歸測試，測試用例優先級，回歸測試用例補充生成等。

## 參考

<https://zh.wikipedia.org/wiki/%E8%BD%AF%E4%BB%B6%E6%B5%8B%E8%AF%95>

<https://blog.miniasp.com/post/2019/02/18/Unit-testing-Integration-testing-e2e-testing>

<https://www.google.com/search?q=angular+%E5%96%AE%E5%85%83%E6%B8%AC%E8%A9%A6&oq=angu&aqs=chrome.0.69i59l2j69i60j69i65l2.3749j0j4&client=ms-android-xiaomi-rev1&sourceid=chrome-mobile&ie=UTF-8>

<https://blog.johnwu.cc/article/angular-unit-test-jasmine-karma-webpack.html>

<https://medium.com/@hsien.w.wei/ut-angular-%E5%8F%AC%E5%96%9A-karma-jasmine-%E4%BE%86%E5%81%9A%E5%80%8B%E5%96%AE%E5%85%83%E6%B8%AC%E8%A9%A6%E5%90%A7-i-5890debc8053>

<https://tw.alphacamp.co/blog/tdd-test-driven-development-example>

<https://rickhw.github.io/2019/10/30/SQA/Problems-In-Software-Testing/>

<http://teddy-chen-tw.blogspot.com/2013/04/blog-post.html?m=1>

<https://medium.com/@ji3g4kami/unit-test-%E6%95%99%E5%AD%B8-ba39e54fcbc5>
