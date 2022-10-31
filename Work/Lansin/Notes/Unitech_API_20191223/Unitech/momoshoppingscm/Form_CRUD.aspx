<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage.master" CodeFile="Form_CRUD.aspx.vb" Inherits="mng_product_list_Form" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="/lib/ckeditor_3.6.2/ckeditor.js" type="text/javascript"></script>
    <script src="/lib/ckeditor_3.6.2/config.js" type="text/javascript"></script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <form id="ff" action="Form_CRUD_Save.aspx?PrgID=<%=PrgID%>&RequestString=Temporary_proposals" enctype="multipart/form-data" method="post" style2="padding: 10px 20px 10px 40px;">
        <input type="hidden" name="id" id="id" value="<%=_ID%>" />
        <input type="hidden" name="cno" id="cno" value="<%=_ID%>" />
        <input type="hidden" name="action" id="action" value="" />
        <input type="hidden" name="cost" id="cost" value="<%=tb.cost%>" />
        <input type="hidden" name="online" id="online" value="<%=tb.OnLine%>" />
        <input type="hidden" name="spicalprice" id="spicalprice" value="<%=tb.spicalprice%>" />
        <input type="hidden" name="saleprice" id="saleprice" value="<%=tb.saleprice%>" />
        <input type="hidden" name="BonusPoint" id="BonusPoint" value="<%=tb.BonusPoint%>" />
        <input type="hidden" name="list_max_buy" id="list_max_buy" value="<%=tb.List_max_buy%>" />
        <input type="hidden" name="list_max_buy_tot" id="list_max_buy_tot" value="<%=tb.list_max_buy_tot%>" />
        <%--<%Response.Write(Server.MapPath(".") + "\10001_B1.jpg") %>--%>
        <%-- 新欄位 --%>
        <input type="hidden" name="ProductNo" id="ProductNo" value="<%=ProductNo%>" />
        <input type="hidden" name="list_max_buy_tot" id="list_max_buy_tot" value="<%=tb.list_max_buy_tot%>" />

        <table border="1" width="100%" class="doc-table">
            <%If _ID > 0 Then%>
            <tr>
                <td colspan="2" style="background-color: #faf5dd" align="right">
                    <a href="<%=tb.Moreinfo_Page_URL%>" target="_blank">開啟前台產品頁面</a>
                </td>
            </tr>
            <%End If%>
            <%--            <tr>
                <td colspan="2" style="background-color: #faf5dd">
                    <div class="cus_accordion" id="div_prod">
                        <div class="expanded" style="float: left"></div>
                        既有EC欄位 
                <div class="expanded" style="float: right"></div>
                    </div>
                </td>
            </tr>
            <tr class="div_prod">
                <td colspan="2">
                    <table border="0" width="100%">
                        <tr>
                            <td class="bg" width="80">購物中心售價:</td>
                            <td>
                                <input readonly="readonly" type="text" id="price" name="price" value="<%=tb.SpicalPrice%>" size="20" maxlength="20" />
                            </td>
                            <td class="bg">SEO商品名稱:</td>
                            <td>
                                <input readonly="readonly" type="text" id="seoUrl" name="seoUrl" value="<%=tb.ProductName%>"  />
                            </td>
                        </tr>

                        <tr>
                            <td class="bg">成本(含稅+運費):</td>
                            <td>
                                <input readonly="readonly" type="text" id="cost" name="cost" value="<%=tb.Cost%>" size="25" maxlength="20" />
                            </td>
                            <td class="bg" width="100">廠商建議價:</td>
                            <td>
                                <input readonly="readonly" type="text" id="msrp" name="msrp" value="<%=tb.SpicalPrice%>" size="25" maxlength="50" />
                            </td>
                        </tr>
                        <tr>
                            <td class="bg">商品名稱:</td>

                            <td>
                                <input readonly="readonly" type="text" id="name" name="name" value="<%=tb.ProductName%>" size="25" maxlength="50" />
                            </td>
                    </table>
                </td>
            </tr>--%>

            <style>
                .red {
                    color:red;
                }
            </style>

            <tr>
                <td colspan="2" style="background-color: #faf5dd">
                    <div class="cus_accordion" id="div_prod_info">
                        <div class="expanded" style="float: left"></div>
                        Momo購物中心新增欄位
                <div class="expanded" style="float: right"></div>
                    </div>
                </td>
            </tr>
            <tr class="div_prod_info">
                <td colspan="2">
                    <table border="0" width="100%">
                        <tr>
                            <td class="bg" width="130"><font class="red">(必填)</font>產品名稱</td>
                            <td>
                                <input type="text" id="NewProductName" name="NewProductName" size="20" maxlength="20" value="<%=NewProductName%>" required='required'/><font style="color:white">__</font><font style="color:red">最多25字(中文算2字，英文算1字)。自動擷取舊產品名稱前25字</font>
                            </td>
                        </tr>
                        <tr style="display:none"> 
                            <td class="bg" width="130">銷售方式 : ★★★★★ 只提供精技[[寄賣]]串接，此欄位會影響其餘必填欄位 ★★★★★</td>
                            <td>
                                <%if dt.Rows.Count <> 0%>
                                    <%If dt.Select().FirstOrDefault.Item("salesMethods").ToString = "寄賣" Then%>
                                       <input type="radio" name="salesMethods" value="寄賣" checked="checked" required="required">寄賣
                                       <input type="radio" name="salesMethods" value="買斷" required="required">買斷
                                    <%ElseIf dt.Select().FirstOrDefault.Item("salesMethods").ToString = "買斷" Then%>
                                       <input type="radio" name="salesMethods" value="寄賣" required="required">寄賣
                                       <input type="radio" name="salesMethods" value="買斷" checked="checked" required="required">買斷
                                    <%End If %>
                                <% Else %>
                                      <input type="radio" name="salesMethods" value="寄賣" checked="checked" required="required">寄賣
                                      <input type="radio" name="salesMethods" value="買斷" required="required">買斷
                                <%End If %>
                                <font style="color:red;"><font style="color:white;">____</font>影響提報時的MomoSCM後台帳號與後續提報欄位驗證</font>
                            </td>
                            
                        </tr>
                        <tr style="display:none">
                            <td class="bg">是否入Momo倉 :  ★★★★★ 只提供精技[[入倉]]串接，此欄位會影響其餘必填欄位 ★★★★★</td>
                             <td>
                                <%if dt.Rows.Count <> 0 Then%>
                                    <%If dt.Select().FirstOrDefault.Item("isECWarehouse").ToString = "是" Then%>
                                       <input type="radio" name="isECWarehouse" value="是" checked="checked" required="required">是
                                       <input type="radio" name="isECWarehouse" value="否" required="required">否
                                    <%ElseIf dt.Select().FirstOrDefault.Item("isECWarehouse").ToString = "否" Then%>
                                       <input type="radio" name="isECWarehouse" value="是" required="required">是
                                       <input type="radio" name="isECWarehouse" value="否" checked="checked" required="required">否
                                    <%End If %>
                                <% Else %>
                                      <input type="radio" name="isECWarehouse" value="是" checked="checked" required="required">是
                                      <input type="radio" name="isECWarehouse" value="否" required="required">否
                                <%End If %>
                            </td>
                        </tr>
                        <tr>
                            <td class="bg" width="130"><font class="red">(必填)</font>是否群組促銷 :</td>
                            <td>
                                <%if dt.Rows.Count <> 0%>
                                    <%If dt.Select().FirstOrDefault.Item("isPrompt").ToString = "是" Then%>
                                       <input type="radio" name="isPrompt" value="是" checked="checked" required="required">是
                                       <input type="radio" name="isPrompt" value="否" required="required">否
                                    <%ElseIf dt.Select().FirstOrDefault.Item("isPrompt").ToString = "否" Then%>
                                       <input type="radio" name="isPrompt" value="是" required="required">是
                                       <input type="radio" name="isPrompt" value="否" checked="checked" required="required">否
                                    <%End If %>
                                <% Else %>
                                      <input type="radio" name="isPrompt" value="是" required="required">是
                                      <input type="radio" name="isPrompt" value="否" required="required">否
                                <%End If %>
                                 <font style="color:red;"><font style="color:white;">____</font>贈品和群組促銷不可同時為"是"</font>
                            </td>
                            
                            </td>
                        </tr>
                        <tr>
                            <td class="bg" width="130"><font class="red">(必填)</font>是否為贈品 :</td>
                            <td>
                                <%if dt.Rows.Count <> 0%>
                                    <%If dt.Select().FirstOrDefault.Item("isGift").ToString = "是" Then%>
                                       <input type="radio" name="isGift" value="是" checked="checked" required="required">是
                                       <input type="radio" name="isGift" value="否" required="required">否
                                    <%ElseIf dt.Select().FirstOrDefault.Item("isGift").ToString = "否" Then%>
                                       <input type="radio" name="isGift" value="是" required="required">是
                                       <input type="radio" name="isGift" value="否" checked="checked" required="required">否
                                    <%End If %>
                                <% Else %>
                                      <input type="radio" name="isGift" value="是" required="required">是
                                      <input type="radio" name="isGift" value="否" required="required">否
                                <%End If %>
                                <font style="color:red;"><font style="color:white;">____</font>贈品和群組促銷不可同時為"是"</font>
                            </td>
                             
                        </tr>
                        
                        
                        <tr>
                            <td class="bg"><font class="red">(必填)</font>商品來源 :</td>
                            <td colspan="4">
                                <select name="goodsType" id="goodsType" required="required">
                                    <option value="">未選擇</option>
                                    <%If Dn.Item("goodsType").ToString <> "" Then%>
                                    <%Select Case Dn.Item("goodsType").ToString %>
                                    <%Case "01"%>
                                    <option selected="selected" value="<%=Dn.Item("goodsType").ToString %>">原廠</option>
                                    <%Case "02"%>
                                    <option selected="selected" value="<%=Dn.Item("goodsType").ToString %>">經銷商</option>
                                    <%Case "03"%>
                                    <option selected="selected" value="<%=Dn.Item("goodsType").ToString %>">平行輸入</option>
                                    <%End Select %>
                                    <%End if %>
                                    <option value="01">原廠</option>
                                    <option value="02">經銷商</option>
                                    <option value="03">平行輸入</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td class="bg"><font class="red">(必填)</font>配送溫層 :</td>
                            <td colspan="4">
                                      <select name="temperatureType" id="temperatureType" required="required">
                                    <option value="">未選擇</option>
                                          <%If Dn.Item("temperatureType").ToString <> "" Then%>
                                    <option selected="selected" value="<%=Dn.Item("temperatureType").ToString %>"><%=Dn.Item("temperatureType").ToString %></option>
                                    <%End if %>
                                    <option value="常溫">常溫</option>
                                    <option value="冷凍">冷凍</option>
                                    <option value="冷藏">冷藏</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td class="bg"><font class="red">(必填)</font>規格:</td>
                            <td colspan="4">
                                寬度(cm) :
                                <input type="text" name="width" id="width" value="<%=Dn.Item("width").ToString %>"" size="50" required="required"/>
                                <br />
                                長度(cm) :
                                <input type="text" name="length" id="length" value="<%=Dn.Item("width").ToString %>" size="50" required="required"/>
                                <br />
                                高度(cm) :
                                <input type="text" name="height" id="height" value="<%=Dn.Item("height").ToString %>" size="50" required="required"/>
                                <br />
                                重量(k g) :
                                <input type="text" name="weight" id="weight" value="<%=Dn.Item("weight").ToString %>" size="50" required="required"/>
                            </td>
                        </tr>
                        
                        <tr style="display:none;">
                            <td class="bg">銷售單位 :</td>
                            <td colspan="4">
                                ★★★★★ 精技固定為"件"如需更改請在調整此選項 ★★★★★
                                <input type="text" name="saleUnit" id="saleUnit" value="件" size="50" required="required"/>
                                <%--<%=Dn.Item("saleUnit").ToString %>--%>

                            </td>
                        </tr>
                        <tr>
                            <td class="bg"><font class="red">(必填)</font>是否商品指定到貨日期 :</td>
                             <td>
                                <%if dt.Rows.Count <> 0 Then%>
                                    <%If dt.Select().FirstOrDefault.Item("isPointReachDate").ToString = "是" Then%>
                                       <input type="radio" name="isPointReachDate" value="是" checked="checked" required="required">是
                                       <input type="radio" name="isPointReachDate" value="否" required="required">否
                                    <%ElseIf dt.Select().FirstOrDefault.Item("isPointReachDate").ToString = "否" Then%>
                                       <input type="radio" name="isPointReachDate" value="是" required="required">是
                                       <input type="radio" name="isPointReachDate" value="否" checked="checked" required="required">否
                                    <%End If %>
                                <% Else %>
                                      <input type="radio" name="isPointReachDate" value="是" required="required">是
                                      <input type="radio" name="isPointReachDate" value="否" required="required">否
                                <%End If %>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
             <tr>
                <td colspan="2" style="background-color: #faf5dd">
                    <div class="cus_accordion" id="div_attrsetting">
                        <div class="expanded" style="float: left"></div>
                        連動選項 : 
                <div class="expanded" style="float: right"></div>
                    </div>
                </td>
            </tr>
            <tr class="div_attrsetting">
                <td colspan="2">
                    <table border="0" width="100%" class2="doc-table">
                        <tr>
                            <td class="bg"><font class="red">(必填)</font>商品品牌 :</td>
                            <td colspan="4">
                                <select name="webBrandNo" id="webBrandNo" required="required">
                                    <option value="">未選擇</option>
                                    <%=MomowebBrand.ToString %>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <td class="bg" width="100"><font class="red">(必填)</font>商品分類 :</td>
                            <td colspan="5">
                                <select name="mainEcCategoryCode" id="mainEcCategoryCode" required="required">
                                    <option value="">未選擇</option>
                                    <%=MomomainEcCategoryCode.ToString %>
                                </select> 
                                 <font style="color:red;"><font style="color:white;">____</font>影響品牌代碼提報欄位驗證</font>
                            </td>
                        </tr>
                        <tr>
                            <td class="bg" width="100" colspan="2"><font class="red">(必填)</font>商品屬性 : 
                                <font style="color:red;">請先選擇商品分類會自動帶出必填的商品屬性</font>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <div id="attrdiv" name="attrdiv">
                                    <%=MomomAttrIndexCode.ToString %> 
                                </div>
                                
                                <input style="display:none;" id="attrdivcount" name="attrdivcount" required="required"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="bg">商品規格1 :</td>
                            <td colspan="4">
                                <select name="colSeq1" id="colSeq1" required="required">
                                    <%=Momocol.Item("colSeq1").ToString %>
                                    <option value="">未選擇</option>
                                    <option value="001">尺寸</option>
                                    <option value="002">容量</option>
                                    <option value="003">重量</option>
                                    <option value="004">顏色</option>
                                    <option value="005">口味</option>
                                    <option value="006">規格</option>
                                </select>
                              <input  id="colDetail1" name="colDetail1" required="required" value="<%=Momocol.Item("colDetail1").ToString %>"/>
                                <font style="color: red;"><font style="color: white;">____</font>規格1與2請勿相同</font>
                            </td>
                        </tr>
                        <tr>
                            <td class="bg">商品規格2 :</td>
                            <td colspan="4">
                                <select name="colSeq2" id="colSeq2" required="required">
                                    <%=Momocol.Item("colSeq2").ToString %>
                                    <option value="">未選擇</option>
                                    <option value="001">尺寸</option>
                                    <option value="002">容量</option>
                                    <option value="003">重量</option>
                                    <option value="004">顏色</option>
                                    <option value="005">口味</option>
                                    <option value="006">規格</option>
                                </select>
                              <input id="colDetail2" name="colDetail2" required="required" value="<%=Momocol.Item("colDetail2").ToString %>"/>
                                <font style="color: red;"><font style="color: white;">____</font>規格1與2請勿相同</font>
                            </td>
                        </tr>
                        <%--目前分類皆無連動必填項目故不實作--%>

                       <%-- <tr>
                            <td class="bg" width="100">業績屬性 :</td>
                            <td colspan="5">
                                <select name="main_achievement" id="main_achievement" required="required">
                                    <option value=""></option>
                                </select> 
                            </td>
                        </tr>
                        <tr>
                            <td class="bg" width="100">約定送貨 :</td>
                           
                                <td>
                                <%if dt.Rows.Count <> 0Then%>
                                    <%If dt.Select().FirstOrDefault.Item("agreed_delivery_yn").ToString = "是" Then%>
                                       <input type="radio" name="agreed_delivery_yn" value="是" checked="checked" required="required">是
                                       <input type="radio" name="agreed_delivery_yn" value="否" required="required">否
                                    <%ElseIf dt.Select().FirstOrDefault.Item("agreed_delivery_yn").ToString = "否" Then%>
                                       <input type="radio" name="agreed_delivery_yn" value="是" required="required">是
                                       <input type="radio" name="agreed_delivery_yn" value="否" checked="checked" required="required">否
                                    <%End If %>
                                <% Else %>
                                      <input type="radio" name="agreed_delivery_yn" value="是" required="required">是
                                      <input type="radio" name="agreed_delivery_yn" value="否" required="required">否
                                <%End If %>
                            </td>
                            
                        </tr>
                        <tr>
                            <td class="bg" width="100">應免稅 :</td>
                            <td>
                                <%if dt.Rows.Count <> 0%>
                                    <%If dt.Select().FirstOrDefault.Item("tax_yn").ToString = "是" Then%>
                                       <input type="radio" name="tax_yn" value="是" checked="checked" required="required">是
                                       <input type="radio" name="tax_yn" value="否" required="required">否
                                    <%ElseIf dt.Select().FirstOrDefault.Item("tax_yn").ToString = "否" Then%>
                                       <input type="radio" name="tax_yn" value="是" required="required">是
                                       <input type="radio" name="tax_yn" value="否" checked="checked" required="required">否
                                    <%End If %>
                                <% Else %>
                                      <input type="radio" name="tax_yn" value="是" required="required">是
                                      <input type="radio" name="tax_yn" value="否" required="required">否
                                <%End If %>
                            </td>
                        </tr>
                        <tr>
                            <td class="bg" width="100">廢四機 :</td>
                            <td>
                                <%if dt.Rows.Count <> 0%>
                                    <%If dt.Select().FirstOrDefault.Item("disc_mach_yn").ToString = "是" Then%>
                                       <input type="radio" name="disc_mach_yn" value="是" checked="checked" required="required">是
                                       <input type="radio" name="disc_mach_yn" value="否" required="required">否
                                    <%ElseIf dt.Select().FirstOrDefault.Item("disc_mach_yn").ToString = "否" Then%>
                                       <input type="radio" name="disc_mach_yn" value="是" required="required">是
                                       <input type="radio" name="disc_mach_yn" value="否" checked="checked" required="required">否
                                    <%End If %>
                                <% Else %>
                                      <input type="radio" name="disc_mach_yn" value="是" required="required">是
                                      <input type="radio" name="disc_mach_yn" value="否" required="required">否
                                <%End If %>
                            </td>
                        </tr>
                        <tr>
                            <td class="bg" width="100">政府補助 :</td>
                            <td>
                                <%if dt.Rows.Count <> 0%>
                                    <%If dt.Select().FirstOrDefault.Item("gov_subsidize_yn").ToString = "是" Then%>
                                       <input type="radio" name="gov_subsidize_yn" value="是" checked="checked" required="required">是
                                       <input type="radio" name="gov_subsidize_yn" value="否" required="required">否
                                    <%ElseIf dt.Select().FirstOrDefault.Item("gov_subsidize_yn").ToString = "否" Then%>
                                       <input type="radio" name="gov_subsidize_yn" value="是" required="required">是
                                       <input type="radio" name="gov_subsidize_yn" value="否" checked="checked" required="required">否
                                    <%End If %>
                                <% Else %>
                                      <input type="radio" name="gov_subsidize_yn" value="是" required="required">是
                                      <input type="radio" name="gov_subsidize_yn" value="否" required="required">否
                                <%End If %>
                            </td>
                        </tr>--%>
                    </table>
                </td>
            </tr>
        <tr>
            <td colspan="2" style="background-color: #faf5dd">
                <div class="cus_accordion" id="div_attrsetting">
                    <div class="expanded" style="float: left"></div>
                    進階選項 :
                <div class="expanded" style="float: right"></div>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table border="0" width="100%" class2="doc-table">
                    <tr>
                        <td class="bg">有無售後保固 :</td>
                        <td>
                            <%if dt.Rows.Count <> 0 Then%>
                            <%If dt.Select().FirstOrDefault.Item("hasAs").ToString = "有" Then%>
                            <input type="radio" name="hasAs" value="有" checked="checked" required="required">有
                                       <input type="radio" name="hasAs" value="無" required="required">無
                                    <%ElseIf dt.Select().FirstOrDefault.Item("hasAs").ToString = "無" Then%>
                            <input type="radio" name="hasAs" value="有" required="required">有
                                       <input type="radio" name="hasAs" value="無" checked="checked" required="required">無
                                    <%End If %>
                            <% Else %>
                            <input type="radio" name="hasAs" value="有" required="required">有
                                      <input type="radio" name="hasAs" value="無" required="required">無
                                <%End If %>
                            
                            <font style="color:red;"><font style="color:white;">____</font>如商品屬於3C家電，</br>一定要有保固，保固天數可填0，若填0則一定要有保固說明，不為0則不一定要有說明。</font>
                        </td>
                    </tr>
                    <td class="bg">保固天數 :</td>
                    <td colspan="4">

                        <input type="text" name="asDays" id="asDays" value="<%=Dn.Item("asDays").ToString %>" size="50"  />
                        <font style="color: red;"><font style="color: white;">____</font>永久保固請輸入99999</font>
                    </td>
        </tr>
        <tr>
            <td class="bg">保固說明 :</td>
            <td colspan="4">

                <textarea type="text" name="asNote" id="asNote" ><%=Dn.Item("asNote").ToString %></textarea>

            </td>
        </tr>
        <tr>
            <td class="bg">首次備貨量 :</td>
            <td colspan="4">
                <input type="text" name="ecFirstQty" id="ecFirstQty" value="<%=Dn.Item("ecFirstQty").ToString %>" size="50"  />
            </td>
        </tr>
        <tr>
            <td class="bg">最低採購量 :</td>
            <td colspan="4">
                <input type="text" name="ecMinQty" id="ecMinQty" value="<%=Dn.Item("ecMinQty").ToString %>" size="50"  />
            </td>
        </tr>
        <tr>
            <td class="bg">備貨週期 :</td>
            <td colspan="4">
                <input type="text" name="ecLeadTime" id="ecLeadTime" value="<%=Dn.Item("ecLeadTime").ToString %>" size="50"  />
            </td> 
            <tr>
            <td class="bg">是否代收代付 :</td>
            <td>
                <%if dt.Rows.Count <> 0 Then%>
                <%If dt.Select().FirstOrDefault.Item("isCommission").ToString = "是" Then%>
                <input type="radio" name="isCommission" value="是" checked="checked" required="required">是
                                       <input type="radio" name="isCommission" value="否" required="required">否
                                    <%ElseIf dt.Select().FirstOrDefault.Item("isCommission").ToString = "否" Then%>
                <input type="radio" name="isCommission" value="是" required="required">是
                                       <input type="radio" name="isCommission" value="否" checked="checked" required="required">否
                                    <%End If %>
                <% Else %>
                <input type="radio" name="isCommission" value="是" required="required">是
                                      <input type="radio" name="isCommission" value="否" required="required">否
                                <%End If %>
            </td>
        </tr>
        <tr>
            <td class="bg">保存期限(天) :</td>
            <td colspan="4">
                <input type="text" name="expDays" id="expDays" value="<%=Dn.Item("expDays").ToString %>" size="50" required="required" />
                <font style="color: red;"><font style="color: white;">____</font>若商品無效期請輸入99999</font>
            </td>

        </tr>
        <tr>
            <td class="bg">國旅卡設定 :</td>
            <td>
                <%if dt.Rows.Count <> 0 Then%>
                <%If dt.Select().FirstOrDefault.Item("isAcceptTravelCard").ToString = "是" Then%>
                <input type="radio" name="isAcceptTravelCard" value="是" checked="checked" required="required">是
                                       <input type="radio" name="isAcceptTravelCard" value="否" required="required">否
                                    <%ElseIf dt.Select().FirstOrDefault.Item("isAcceptTravelCard").ToString = "否" Then%>
                <input type="radio" name="isAcceptTravelCard" value="是" required="required">是
                                       <input type="radio" name="isAcceptTravelCard" value="否" checked="checked" required="required">否
                                    <%End If %>
                <% Else %>
                <input type="radio" name="isAcceptTravelCard" value="是" required="required">是
                                      <input type="radio" name="isAcceptTravelCard" value="否" required="required">否
                                <%End If %>
            </td>
        </tr>
        <tr>
            <td class="bg">大型家電是否含安裝 :</td>
            <td>
                <%if dt.Rows.Count <> 0 Then%>
                <%If dt.Select().FirstOrDefault.Item("isIncludeInstall").ToString = "是" Then%>
                <input type="radio" name="isIncludeInstall" value="是" checked="checked" required="required">是
                                       <input type="radio" name="isIncludeInstall" value="否" required="required">否
                                    <%ElseIf dt.Select().FirstOrDefault.Item("isIncludeInstall").ToString = "否" Then%>
                <input type="radio" name="isIncludeInstall" value="是" required="required">是
                                       <input type="radio" name="isIncludeInstall" value="否" checked="checked" required="required">否
                                    <%End If %>
                <% Else %>
                <input type="radio" name="isIncludeInstall" value="是" required="required">是
                                      <input type="radio" name="isIncludeInstall" value="否" required="required">否
                                <%End If %>
            </td>
        </tr>
        </table>
                </td>
            </tr>
       <%-- 20191219 育誠 一律改為使用目錄內的 10001_B1.jpg ， 再由後台人員去MomoSCM更改商品圖片 --%>
       <%-- <tr>
            <td colspan="2" style="background-color: #faf5dd">
                <div class="cus_accordion" id="div_image">
                    <div class="expanded" style="float: left"></div>
                    Momo多媒體欄位設定(既有EC欄位)
                <div class="expanded" style="float: right"></div>
                </div>
            </td>
        </tr>
        <tr class="div_image">
            <td colspan="2">
                <table border="0" width="100%">
                    <tr>
                        <td class="bg" width="80"><span class="mustinput">*</span>原圖:<br />
                            <%if MomoIMGUrl = "" Then %>
                            <font style="color: red;"><%=MomoOldIMG(0) %></font><%=MomoOldIMG(1) %>
                            <%End If %>
                            </td>
                        <td>
                            <%if MomoIMGUrl = "" Then %>
                            <img style="border: 2px red dashed;width:50%;" alt="<%=tb.CategoryO %>" src="<%=EC.mng.Info.Eclife_HomeURL & tb.CategoryO%>" /><a href="<%=EC.mng.Info.Eclife_HomeURL & tb.CategoryO%>" target="_blank">View</a>
                            <td><input type="file" id="MomoNewIMG" name="MomoNewIMG" required="required"/></td>
                            <%else %>
                            <img style="border: 2px red dashed;width:100%;" alt="" src="<%=MomoIMGUrl %>" />
                            <%End if %>
                        </td>
                        
                    </tr>

                </table>
            </td>
        </tr>--%>
        <tr>
            <td colspan="2" align="center">

                <%-- 2015-04-20 小葉 從主頁移至表單的 --%>
                <%If EC.mng.Login.ISDeveloper Or (_ID > 0 And prglimit.Modify) Then%>
                <%--<%If tb.NG = 2 Then%>
                    <a href="#" onclick="javascript:openform_NgFree(<%=tb.cno%>);" class="easyui-linkbutton">福利品</a>
                    <%Else %>--%>
                <%--<a href="#" onclick="javascript:ProdToNG(<%=tb.cno%>);" class="easyui-linkbutton" >轉NG</a>--%>
                <%-- <%End If%>--%>
                <%end If %>
                <%--                    <%If _ID > 0 And (EC.mng.Login.ISDeveloper Or prglimit.Delete) Then%>
                    <a href="#" onclick="javascript:del(<%=_ID%>);" class="easyui-linkbutton" icon="icon-cancel">刪除</a>
                    <%End If%>--%>
                <%If EC.mng.Login.ISDeveloper Or (_ID = 0 And prglimit.Add) Or (_ID > 0 And prglimit.Modify) Then%>
                <a onclick="check()" href="call_MomoAPI.aspx?ProductNo=<%=ProductNo %>" class="easyui-linkbutton" icon="icon-ok">提報</a>
                <input type="submit" onclick="check()" value="暫存" />
                <%End If%>
                <a href="#" onclick="javascript:closeme();" class="easyui-linkbutton" icon="icon-cancel">取消</a>
            </td>
        </tr>
        </table>
    </form>
    <script type="text/javascript">
        var _cno = '<%=tb.cno%>';                      //商品編號
        var _prgid = '<%=prgid%>';                     //取主程式ID = 主選單MenuID
        var _winlevel = 2;

        var _houseno = '<%=tb.houseno%>';              //館別
        var _largeno = '<%=tb.largeno%>';              //大類
        var _mediumno = '<%=tb.MediumNo%>';            //中類
        var _mediumsubno = '<%=tb.MediumSubNO%>';      //小類
    </script>
    <script>

        function check(data) {
            //傳遞動態下拉選項數目至Form_CRUD_Save
            $("#attrdivcount").val($("#attrdiv>span").length);
            
            //check
            alert('確定?')
        }

        //開啟插入圖片的 popup (2012-09-07 友)
        function OpenImg2ckEditor(editor) {
            var title = '插入圖片: CNO=' + _cno + ' PrgID=' + _prgid;
            var url = '/mng/product/Unitech/UpPhoto/UpPhoto.aspx?PrgID=' + _prgid + '&CNO=' + _cno + '&editor=' + editor;
            parent.OpenCRUDForm(window, title, url, 680, 400, _winlevel);  //呼叫 /default.aspx 的 OpenCRUDForm( Level = 1)
        }

        //連動屬性
         $("#mainEcCategoryCode").on("change", function(){
            //alert($("#subStationId").val());
             $("#attrdiv").find("option:selected").text("");
            $("#attrdiv").empty();
            var attrs;
            $.ajax({
                type: 'POST',
                async: false,  //使用同步
                url: "/mng/Product/Unitech/momoshoppingscm/ajax_Get_attributes.ashx",
                data: 'mainEcCategoryCode=' + $("#mainEcCategoryCode").val(),
                dataType: "text",
                success: function (info) {
                    
                    attrs = info
                    //console.log(info)
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //var message = '轉資料時發生錯誤';
                    //alert(message);    //提示視窗
                    alert('此分類無屬性確定選取?')
                }
            })
      
             $("#attrdiv").html(attrs);
             //傳遞動態下拉選項數目至Form_CRUD_Save
             $("#attrdivcount").val($("#attrdiv>span").length);
             //alert($("#attrdiv>span").length);
         });

     




    </script>

</asp:Content>
