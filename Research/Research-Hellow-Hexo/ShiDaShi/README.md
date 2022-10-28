# HEXO 操作手冊

1. `npm install -g hexo-cli` 安裝全域 HEXO 命令列介面
2. `hexo init <專案名稱>` 初始化 HEXO 專案 => 如果 <專案名稱> 沒有則會在 cd 位置初始化
3. `hexo generate` 生成靜態檔案 => 會以 themes、source 內沒有 _ 的檔案生成，所以首頁也會被重新生成
4. `hexo server` 開啟 HEXO LOCAL 伺服器
5. `hexo new <模板> <文章標題>` 如果沒有 <模板> 指令會以 _config.yml 內 default_layout 去 scaffolds 找
6. [官方文檔](https://hexo.io/zh-tw/docs/) 開源文檔相對於其他感覺有點兩光
7. [Mike Dane Tutorial](https://www.youtube.com/watch?v=Kt7u5kr_P5o&list=PLLAZ4kZ9dFpOMJR6D25ishrSedvsguVSm&ab_channel=MikeDane)
8. [從0開始themes](https://github.com/lizehongss/hexo-theme-zhl)
