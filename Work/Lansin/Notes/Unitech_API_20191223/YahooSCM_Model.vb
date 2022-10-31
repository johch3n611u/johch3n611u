#Region "YahooShoppingSCM_proposals 提報架構類別"

''' <summary>
''' YahooShoppingSCM_proposals 提報架構
''' (註:部分欄位指定值須比照線上文件)
''' 現階段非必填欄位註解已利後續修改時打開
''' https://docs.google.com/document/d/1-ROog-SSxvCIKkWRfcjadtFmP_gnL4PkqbR7TfvR5Cg/edit?pli=1
''' </summary>
''' <remarks></remarks>
Public Class YahooShoppingSCM_Data
#Region "object"
    ''' <summary>
    ''' YahooShoppingSCM_proposals 提報架構類別
    ''' 參考 : https://stackoverflow.com/questions/8118019/vb-net-json-deserialize
    ''' </summary>

    Public Class JSONobj

        ''' <summary>
        ''' 提案人，限 10 個中文字 (必填●
        ''' 
        ''' ex : 孫協志
        ''' </summary>
        Public Property applicant As String

        ''' <summary>
        ''' 商品資訊 (審核狀態 reviewStatus 非 composing 時 (必填●
        ''' </summary>
        Public Property product As Product

        ''' <summary>
        ''' 審核狀態
        ''' composing: 尚未提案 (預設)
        '''    draft: 儲存暫不提案
        '''    pendingReview: 待審核
        '''    approved: 審核已通過
        '''    declined: 審核不通過
        '''    expired: 已過審核期限
        '''    
        ''' ex : draft
        ''' </summary>
        Public Property reviewStatus As String

        ''' <summary>
        ''' 提案當下的提案站別 ID, e.g. sub1只允許供應商有簽約的子站 (必填●
        ''' 
        ''' ex : sub1
        ''' </summary>
        Public Property subStationId As String

        ''' <summary>
        ''' 提案單類型 (必填●
        ''' newProduct: 新增贈品/配件/屬性
        '''    newListing: 新增一般賣場
        '''    newListingByApi: 透過 SCM API 新增一般賣場 (不可用於新增)
        '''    updateCopy: 修改賣場/商品詳情
        '''    updateVideo: 修改賣場影片
        '''    
        ''' ex : newListing
        ''' </summary>
        Public Property type As String
        '''' <summary>
        '''' 備註，限 200 個字
        '''' 
        '''' ex : 我是備註
        '''' </summary>
        'Public Property note As String
        ''' <summary>
        ''' 賣場資訊 type 為 newListing 且 reviewStatus 非 composing 時必填 (必填●
        ''' </summary>
        Public Property listing As Listing

    End Class
#End Region



#Region "object(Product)"
    ''' <summary>
    ''' 商品資訊 (審核狀態 reviewStatus 非 composing 時必填●
    ''' </summary>
    Public Class Product
        ''' <summary>
        ''' 商品規格顯示方式 (必填●
        ''' table
        ''' list
        ''' 
        ''' ex : table
        ''' </summary>
        Public Property attributeDisplayMode As String

        ''' <summary>
        ''' 商品規格表 (必填●
        '''被 ProposalModel 中被選中的 Attribute，不可再填入此欄位
        '''若規格為 structured data 中的選項
        '''    values 需符合 structured data constraint 限制 (radiobox/checkbox)
        '''    只能有一個 `自訂其他屬性`
        '''若規格為 `自訂`
        '''    values 只能有一個值
        '''    不允許重複 `自訂規格` 名稱
        '''    `自訂規格` 名稱不允許與 structured data 中的選項重複
        '''    `自訂規格` 名稱不允許與 `自訂屬性` 名稱重複
        ''' </summary>
        <JsonProperty(PropertyName:="attributes")>
        Public Property attributes() As List(Of Attribute) = New List(Of Attribute)

        ''' <summary>
        ''' 商品目前的分類子類 往上追溯的子站要跟供應商有簽約的子站有交集 (必填●
        ''' 
        ''' ex : catItem10070
        ''' </summary>
        Public Property catItemId As String

        ''' <summary>
        ''' 內容級別 (必填●
        ''' None: 無級別 
        ''' G: 普級
        ''' PG: 保護級
        ''' PG12: 輔導級 12歲+
        ''' PG15: 輔導級 15歲+
        ''' R: 限制級
        ''' NC18: 情趣商品 (若為未滿18歲青少年不能購買商品，請選擇限制級)
        ''' 
        ''' 若 subStationId = 28(電玩 / 遊戲)， 只允許 G, PG, PG12, PG15 And R
        ''' 若 subStationId = 566， 只允許 R And NC18
        ''' 其他子站不可選 PG15
        ''' 
        ''' ex : G
        ''' </summary>
        Public Property contentRating As String

        ''' <summary>
        ''' 成本(含稅+運費)，到小數點兩位 (必填●
        ''' range: [0-9999999]
        ''' 成本(含稅+運費) = 購物中心售價 = 廠商建議價
        ''' 
        ''' ex : 50.00
        ''' </summary>
        Public Property cost As String

        '''' <summary>
        '''' 是否為即期品 (必填●
        '''' 配送方式為`快速到貨`且有填寫商品保存期限時方可為 true
        '''' 預設為 false
        '''' 
        '''' ex : true
        '''' </summary>
        'Public Property isExpiringItem As Boolean

        '''' <summary>
        '''' 是否需要安裝
        '''' 配送方式為`快速到貨`且附約資訊包含 appliance 時方可填寫
        '''' 預設為 false
        '''' 
        '''' ex : true
        '''' </summary>
        'Public Property isInstallRequired As Boolean

        '''' <summary>
        '''' 是否為大材積商品 (即將棄用)
        '''' (舊) 配送方式為`快速到貨`且附約資訊包含 appliance 時方可為 true
        '''' (新) POST/PUT 皆應為 null
        '''' 預設為 false
        '''' 
        '''' ex : true
        '''' </summary>
        'Public Property isLargeVolume As Boolean

        '''' <summary>
        '''' 是否為大型商品附屬贈品
        '''' 配送方式為`快速到貨`且附約資訊包含 appliance 時方可填寫
        '''' 預設為 false
        '''' 
        '''' ex : true
        '''' </summary>
        'Public Property isLargeVolumnProductGift As Boolean

        '''' <summary>
        '''' 是否屬於廢四機
        '''' 供應商啟用功能包含 recycleAppliance 時可選
        '''' 
        '''' ex : true
        '''' </summary>
        'Public Property isNeedRecycle As Boolean

        '''' <summary>
        '''' 是否為買斷商品
        '''' 配送方式為`快速到貨`且附約資訊包含 outrightPurchase 時方可填寫
        '''' 預設為 false
        '''' 
        '''' ex : true
        '''' </summary>
        'Public Property isOutrightPurchase As Boolean

        '''' <summary>
        '''' 最小包裝數
        '''' range: [1-32767]
        '''' 配送方式為`快速到貨`時才能更改，其餘只能為 1
        '''' 預設為 1
        '''' 
        '''' ex : 10
        '''' </summary>
        'Public Property minPackingCount As Integer

        ''' <summary>
        ''' 屬性商品型號 (不一定要給
        ''' proposal type = 14 (updateVideo) 且非無屬性賣場時須提供 (必填●
        ''' </summary>
        Public Property models() As New List(Of Model)

        ''' <summary>
        ''' 廠商建議價，到小數點兩位 (必填●
        ''' range: [0-9999999]
        ''' 成本(含稅+運費) = 購物中心售價= 廠商建議價
        ''' 
        ''' ex : 100.00
        ''' </summary>
        Public Property msrp As String

        ''' <summary>
        ''' 商品名稱，最長 45 個字 (必填●
        ''' 若為配送方式為`快速到貨`且為即期品，必須加上前綴"(即期品)"
        ''' 
        ''' (即期品)我是商品名稱
        ''' </summary>
        Public Property name As String

        '''' <summary>
        '''' 主件商品供應商商品料號，最長 40 個字
        '''' 
        '''' 12345
        '''' </summary>
        'Public Property partNo As String

        ''' <summary>
        ''' 配送方式 (必填●
        ''' 1    Home        預設可選
        ''' 61    Express24HR    附約資訊包含 warehouse 時可選
        ''' 200    ESD            分類進階功能包含 ESD 時可選
        ''' 400    EDelievry    分類進階功能包含 eCoupon 且提案單類型為 newProduct 時可選
        ''' 800    HomeStore    供應商啟用功能包含 deliveryByCvs 時可選
        ''' </summary>
        Public Property shipType As Shiptype

        ''' <summary>
        ''' 賣場簡短說明 (必填●
        ''' 最多 5 條，每條最長 15 個字
        ''' 至少需填 1 條
        ''' 
        ''' ex : [我是簡短說明,我是簡短說明2]
        ''' </summary>
        Public Property shortDescription() As List(Of String)

        ''' <summary>
        ''' 結構化資料屬性集 ID (必填●
        ''' 
        ''' ex : 000003326689
        ''' </summary>
        Public Property struDataAttrClusterId As String

        '''' <summary>
        '''' 商品保證 (必填●
        '''' </summary>
        'Public Property warranty As Warranty

        ''' <summary>
        ''' 商品詳情 (必填●
        ''' 為 HTML 內容
        '''     iframe URL 只允許 Youtube
        '''     image URL 只允許 yimg.com
        '''     image 不會再進行縮圖
        '''     proposal.reviewStatus 非 composing 時，為必填
        '''     
        ''' ex : table html /table
        ''' </summary>
        Public Property copy As String

        '''' <summary>
        '''' 品牌
        '''' 最長 20 個字
        '''' 
        '''' ex : 我是品牌
        '''' </summary>
        'Public Property brand As String

        '''' <summary>
        '''' 包裝完成後的商品高度
        '''' 單位為 cm，需為正整數
        '''' 若為配送方式為`快速到貨`或`直店配`時必填 (必填●
        '''' max 9999
        '''' 
        '''' ex : 77
        '''' </summary>
        'Public Property height As Integer

        '''' <summary>
        '''' 包裝完成後的商品長度
        '''' 單位為 cm，需為正整數
        '''' 若為配送方式為`快速到貨`或`直店配`時必填 (必填●
        '''' max 9999
        '''' 
        '''' ex : 55
        '''' </summary>
        'Public Property length As Integer

        '''' <summary>
        '''' 商品型號
        '''' 最長 20 個字
        '''' 
        '''' ex : 我是商品型號
        '''' </summary>
        'Public Property model As String
        '''' <summary>
        '''' 商品保存期限
        '''' 單位為天
        '''' range: [1-32767]
        '''' 配送方式shiptype為`快速到貨`可填，否則應為 null
        '''' 
        '''' ex : 56
        '''' </summary>
        'Public Property preserveDays As Integer
        '''' <summary>
        '''' 包裝完成後的商品寬度
        '''' 單位為 cm，需為正整數
        '''' 若為配送方式為`快速到貨`或`直店配`時必填
        '''' max 9999
        '''' 
        '''' ex : 88
        '''' </summary>
        'Public Property weight As Integer
        '''' <summary>
        '''' 包裝完成後的商品重量
        '''' 單位為 g，需為正整數
        '''' 若為配送方式為`快速到貨`或`直店配時`必填
        '''' max 2147483647
        '''' 
        '''' ex : 66
        '''' </summary>
        'Public Property width As Integer
        ''' <summary>
        ''' 商品屬性 (必填●
        ''' </summary>
        Public Property specs() As List(Of Spec2) = New List(Of Spec2)
    End Class
#End Region

#Region "object(Product)(Attribute)"
    ''' <summary>
    ''' 商品規格表 (必填●
    '''被 ProposalModel 中被選中的 Attribute，不可再填入此欄位
    '''若規格為 structured data 中的選項
    '''    values 需符合 structured data constraint 限制 (radiobox/checkbox)
    '''    只能有一個 `自訂其他屬性`
    '''若規格為 `自訂`
    '''    values 只能有一個值
    '''    不允許重複 `自訂規格` 名稱
    '''    `自訂規格` 名稱不允許與 structured data 中的選項重複
    '''    `自訂規格` 名稱不允許與 `自訂屬性` 名稱重複
    ''' </summary>
    Public Class Attribute
        ''' <summary>
        ''' 屬性名稱 (必填●
        ''' 
        ''' ex : 中央處理器品牌
        ''' ex : 中央處理器型號
        ''' ex : 型號
        ''' ex : 晶片組
        ''' ex : 重量(kg)
        ''' </summary>
        <JsonProperty(PropertyName:="name")>
        Public Property name As String
        ''' <summary>
        ''' 屬性值 (必填●
        ''' 
        ''' ex : Intel
        ''' ex : G870
        ''' ex : Trey-Super-PC
        ''' ex : B75
        ''' ex : 27t
        ''' </summary>
        <JsonProperty(PropertyName:="values")>
        Public Property values() As List(Of String)
    End Class
#End Region

#Region "object(Product)(Models)"

    ''' <summary>
    ''' 屬性商品型號 (不一定要給
    ''' proposal type = 14 (updateVideo) 且非無屬性賣場時須提供 (必填●
    ''' </summary>
    Public Class Model
        ''' <summary>
        ''' 屬性值 (必填●
        ''' </summary>
        <JsonProperty(PropertyName:="items")>
        Public Property items() As List(Of Item)
        ''' <summary>
        ''' 商品影片 (必填●
        ''' </summary>
        <JsonProperty(PropertyName:="videos")>
        Public Property videos() As List(Of Video)
        ''' <summary>
        ''' 商品圖 (必填●
        ''' newListing/newProduct
        ''' 最多 10 張，需要為正方形，尺寸大於 1000^2 pixels 會被縮至 
        ''' 1000^2, 400^2, 250^2, 135^2 及 80^2 pixels、
        ''' 大於 400^2 pixels 會被縮至 400^2, 250^2, 135^2 及 80^2 pixels
        ''' 至少需要兩張主圖 (1000^2 pixels)，可增加 8 張副圖 (1000^2 Or 400^2 pixels)
        '''    updateImage
        '''    用於 proposal.listing.models 且 order = 1 時，可於各尺寸指定小圖 URL，未指定寬高的維度會是 1000x1000 圖檔自動縮圖
        '''    用於 proposal.listing.[additionalPurchase|complimentaries|selectComplimentaries]，規則同 newListing / newProduct
        ''' </summary>
        <JsonProperty(PropertyName:="images")>
        Public Property images() As List(Of Image)
        ''' <summary>
        ''' 第 1 層屬性的 spec
        ''' 需為 ProposalProductSpec 中 level=1 的 spec
        ''' 若為無屬性商品時為空, 有 1 層及以上屬性時必填 (必填●
        ''' 若 spec name 是從 structured data 中挑選，且 constraint type 為
        ''' radiobox 或 checkbox 時
        '''     values 一定要包含在 constraint 選項中
        '''        `自訂項目` 時，values 為 `其他`，自訂內容存放於 displayName
        '''    若 spec name 為自訂時
        '''        `自訂項目` 的值存放於 spec.values
        '''        displayName 需為必填，但不需與 `自訂項目` 的值一致
        ''' </summary>
        <JsonProperty(PropertyName:="spec")>
        Public Property spec As Spec
        ''' <summary>
        ''' 賣場顯示名稱，預設為規格名稱 (必填●
        ''' 若為無屬性商品時為空, 有 1 層及以上屬性時可填
        ''' 
        ''' ex : 極致簡約Dell2019
        ''' </summary>
        <JsonProperty(PropertyName:="displayName")>
        Public Property displayName As String
    End Class
#End Region
#Region "object(Product)(Models)(spec)"

    ''' <summary>
    ''' 第 1 層屬性的 spec
    ''' 需為 ProposalProductSpec 中 level=1 的 spec
    ''' 若為無屬性商品時為空, 有 1 層及以上屬性時必填 (必填●
    ''' 若 spec name 是從 structured(結構化大中小類) data 中挑選，且 constraint type 為
    ''' radiobox 或 checkbox 時
    '''     values 一定要包含在 constraint 選項中
    '''        `自訂項目` 時，values 為 `其他`，自訂內容存放於 displayName
    '''    若 spec name 為自訂時
    '''        `自訂項目` 的值存放於 spec.values
    '''        displayName 需為必填，但不需與 `自訂項目` 的值一致
    ''' </summary>
    Public Class Spec
        ''' <summary>
        ''' ex : 品牌
        ''' ex : 品牌
        ''' </summary> 
        <JsonProperty(PropertyName:="name")>
        Public Property name As String
        ''' <summary>
        ''' ex : Dell戴爾
        ''' ex : hp惠普
        ''' </summary>
        <JsonProperty(PropertyName:="values")>
        Public Property values() As List(Of String)
    End Class

#End Region
#Region "object(Product)(Models)(Items)"
    ''' <summary>
    ''' 屬性值 (必填●
    ''' </summary>
    Public Class Item
        '''' <summary>
        '''' 屬性商品供應商商品料號
        '''' 最長 40 個字
        '''' 
        '''' ex : 5566
        '''' </summary>
        'Public Property partNo As String
        '''' <summary>
        '''' 實際國際條碼
        '''' 限定為13碼或12碼 (12碼由 API 在前面補 0 )
        '''' 若為下列配送方式 (shipType)時則可填寫
        ''''     1 (宅配)
        ''''     61 (集宅配)
        ''''     800 (直店配)
        '''' </summary>
        'Public Property barcode As String
        '''' <summary>
        '''' 進倉用國際條碼
        '''' 限定為13碼或12碼 (12碼由 API 在前面補 0 )
        '''' 
        '''' ex : 725272730706
        '''' </summary>
        'Public Property warehouseBarcode As String
        ''' <summary>
        ''' 第 2 層屬性的 spec
        ''' 需為 ProposalProductSpec 中 level=2 的 spec
        ''' 若為無屬性與 1 層屬性商品時為空, 為 2 層屬性商品時為必填
        ''' 
        ''' ex : 9785109946480
        ''' </summary>
        <JsonProperty(PropertyName:="spec")>
        Public Property spec As Spec1
        ''' <summary>
        ''' 賣場顯示名稱
        ''' 預設為規格名稱
        ''' 若為無屬性與 1 層屬性商品時為空, 為 2 層屬性商品時可填
        ''' 
        ''' ex : 卡其色
        ''' </summary>
        <JsonProperty(PropertyName:="displayName")>
        Public Property displayName As String
    End Class

#End Region
#Region "object(Product)(Models)(Item)(Spec)"
    ''' <summary>
    '''第 2 層屬性的 spec
    '''需為 ProposalProductSpec 中 level=2 的 spec
    '''若為無屬性與 1 層屬性商品時為空, 為 2 層屬性商品時為必填
    '''ex : 9785109946480
    ''' </summary>
    Public Class Spec1
        ''' <summary>
        ''' ex : 顏色
        ''' </summary>
        <JsonProperty(PropertyName:="name")>
        Public Property name As String
        ''' <summary>
        ''' ex : 卡其
        ''' </summary>
        <JsonProperty(PropertyName:="values")>
        Public Property values() As List(Of String)
    End Class
#End Region
#Region "object(Product)(Models)(Video)"
    ''' <summary>
    ''' 商品影片 (必填●
    ''' </summary>
    Public Class Video
        ''' <summary>
        ''' 影片 url
        ''' 若 FQDN 為 {s|ct}.yimg.com 或 edgecast-vod.yahoo.net，必需為 https 協定
        ''' 目前支援的 Video Codecs 請見列表 
        ''' https://docs.aws.amazon.com/en_us/mediaconvert/latest/ug/reference-codecs-containers-input.html
        ''' 
        ''' ex :
        ''' https://s.yimg.com/bp/Files/374d9974ab2cbce382e42724fede7aa07313cae6.qt
        ''' </summary>
        <JsonProperty(PropertyName:="url")>
        Public Property url As String
        ''' <summary>
        ''' 排列
        ''' 
        ''' ex : 1
        ''' </summary>
        <JsonProperty(PropertyName:="order")>
        Public Property order As Integer
    End Class

#End Region
#Region "object(Product)(Models)(Image)"
    Public Class Image
        ''' <summary>
        ''' 影片 url
        ''' 若 FQDN 為 {s|ct}.yimg.com 或 edgecast-vod.yahoo.net，必需為 https 協定
        ''' 目前支援的 Video Codecs 請見列表 
        ''' https://docs.aws.amazon.com/en_us/mediaconvert/latest/ug/reference-codecs-containers-input.html
        ''' 
        ''' ex : https://s.yimg.com/bp/Files/ba0b8bf005bab4e7cc8821afea217e342a1dfca3.png
        ''' 
        ''' </summary>
        <JsonProperty(PropertyName:="url")>
        Public Property url As String
        ''' <summary>
        ''' 排列
        ''' starts from 1
        ''' 
        ''' ex : 1
        ''' </summary>
        <JsonProperty(PropertyName:="order")>
        Public Property order As Integer
    End Class
#End Region

#Region "object(Product)(Shiptype)"

    ''' <summary>
    ''' 配送方式 (必填●
    ''' 1    Home        預設可選
    ''' 61    Express24HR    附約資訊包含 warehouse 時可選
    ''' 200    ESD            分類進階功能包含 ESD 時可選
    ''' 400    EDelievry    分類進階功能包含 eCoupon 且提案單類型為 newProduct 時可選
    ''' 800    HomeStore    供應商啟用功能包含 deliveryByCvs 時可選
    ''' </summary>
    Public Class Shiptype
        ''' <summary>
        ''' 配送方式 (必填●
        ''' 1    Home        預設可選 宅配(轉單/自出)
        ''' 61    Express24HR    附約資訊包含 warehouse 時可選 快速到貨(進倉)
        ''' 200    ESD            分類進階功能包含 ESD 時可選
        ''' 400    EDelievry    分類進階功能包含 eCoupon 且提案單類型為 newProduct 時可選 電子禮券
        ''' 800    HomeStore    供應商啟用功能包含 deliveryByCvs 時可選 直店配(宅配+超取)
        ''' 
        ''' ex : 61
        ''' </summary>
        <JsonProperty(PropertyName:="id")>
        Public Property id As Integer
    End Class

#End Region

    '#Region "object(Product)(Warranty)"

    '    ''' <summary>
    '    ''' 商品保證 (必填●
    '    ''' </summary>
    '    Public Class Warranty

    '        ''' <summary>
    '        ''' 保固期限 (必填●
    '        ''' 如果內容值為`無保固`則保固範圍及保固來源可不填，反之若為其他值則必填
    '        ''' 最長 20 個字
    '        ''' 
    '        ''' ex : 一個月
    '        ''' </summary>
    '        Public Property period As String

    '        ''' <summary>
    '        ''' 說明訊息整段描述
    '        ''' 最長 800 個字
    '        ''' 
    '        ''' ex : 保固說明文字
    '        ''' </summary>
    '        Public Property description As String

    '        ''' <summary>
    '        ''' 保固來源
    '        ''' none: 無保固
    '        ''' official: 原廠保固
    '        ''' retailer: 經銷保固
    '        ''' 若保固期限為`無保固`，只能為`none` (若有輸入會被取代)
    '        ''' 若保固期限不為`無保固`，預設為`official`
    '        ''' 
    '        ''' ex : official
    '        ''' </summary>
    '        Public Property handler As String
    '        ''' <summary>
    '        ''' 保固範圍
    '        ''' 最長 20 個字
    '        ''' 若保固期限為`無保固`，自動填入`無保固`
    '        ''' 
    '        ''' ex : 新品瑕疵
    '        ''' </summary>
    '        Public Property scope As String
    '    End Class
    '#End Region

#Region "object(Product)(Specs)"
    ''' <summary>
    ''' 商品屬性 (必填●
    ''' 
    ''' 此值可決定 1層 2層 屬性商品
    ''' </summary>
    Public Class Spec2
        ''' <summary>
        ''' 屬性層級 (必填●
        ''' range: [1-2]
        ''' 
        ''' exe : 1
        ''' exe : 2
        ''' </summary>
        <JsonProperty(PropertyName:="level")>
        Public Property level As Integer
        ''' <summary>
        ''' 屬性名稱最長 30 個字 (必填●
        ''' 
        ''' exe : 品牌
        ''' exe : 顏色
        ''' </summary>
        <JsonProperty(PropertyName:="name")>
        Public Property name As String
    End Class
#End Region



#Region "object(Listing)"

    ''' <summary>
    ''' 賣場資訊 type 為 newListing 且 reviewStatus 非 composing 時必填 (必填●
    ''' </summary>
    Public Class Listing
        ''' <summary>
        ''' 商品目前的分類子類，往上追溯的子站要跟供應商有簽約的子站有交集 (必填●
        ''' ex : catItem10070
        ''' </summary>
        <JsonProperty(PropertyName:="catItemId")>
        Public Property catItemId As String
        ''' <summary>
        ''' 交貨期限 
        ''' 預設 normal
        ''' normal 正常交貨期
        ''' preOrder: 預購型商品
        ''' customized: 客製化商品
        ''' appointment: 客約送貨日
        ''' 
        ''' shipTypeId = 61
        ''' (集宅配)    if (product.isInstallRequired = true)
        ''' ? appointment :  normal
        ''' shipTypeId != 61
        ''' (非集宅配)    if category.functions contains
        ''' preOrderDelivery: normal, preOrder
        ''' customizedDelivery: normal, customized
        ''' appointmentDelivery: normal, appointment
        ''' 
        ''' 提案一般賣場且 reviewStatus 為 draft草案 時以上必填 (必填●
        ''' 
        ''' ex : appointment
        ''' </summary>
        <JsonProperty(PropertyName:="deliveryType")>
        Public Property deliveryType As String
        '''' <summary>
        '''' 特色標題，最長 20 個字
        '''' 
        '''' ex : 我是特色標題
        '''' </summary>
        'Public Property featureTitle As String
        ''' <summary>
        ''' 購物中心售價，到小數點兩位
        ''' range: [0-9999999]
        ''' rules:
        ''' 1.    cost (成本(含稅+運費)) 小於等於 price (購物中心售價) 小於等於 msrp (廠商建議價)
        ''' 2.    shipTypeId=800 (直店配) 時需 小於等於 20000
        ''' 提案一般賣場且 reviewStatus 為 draft 以上時必填 (必填●
        ''' 
        ''' 若"賣場毛利率 ((購物中心售價 - 成本) / 購物中心售價) 小於 子站毛利率"時，則
        ''' 1.    若有自動過審 (user.profile.toggles contains autoApproveListingOnOffShelve)時，API 會阻擋該 request
        ''' 2.    若無自動過審，僅檢查賣場毛利率需>=0
        ''' 
        ''' ex : 100.00
        ''' </summary>
        <JsonProperty(PropertyName:="price")>
        Public Property price As String
        ''' <summary>
        ''' 賣場網址，最長 25 個字
        ''' 只接受正/簡體中文、英文、數字、`-`，多個 `-` 會合併成一個，開頭不能是 `-`
        ''' 提案一般賣場且 reviewStatus 為 draft 以上時必填(必填●
        ''' 
        ''' ex : 我是商品名稱
        ''' </summary>
        <JsonProperty(PropertyName:="seoUrl")>
        Public Property seoUrl As String
    End Class
#End Region

End Class
#End Region