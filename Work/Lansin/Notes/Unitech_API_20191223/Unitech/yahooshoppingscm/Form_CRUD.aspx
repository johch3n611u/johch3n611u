<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage.master" CodeFile="Form_CRUD.aspx.vb" Inherits="mng_product_list_Form" validateRequest="false"%>

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

        <%-- 新欄位 --%>
        <input type="hidden" name="ProductNo" id="ProductNo" value="<%=ProductNo%>" />
        <input type="hidden" name="attrnoncustomcount" id="attrnoncustomcount" value="" required="required"/>
        <input type="hidden" name="attrcustomcount" id="attrcustomcount" value="" required='required'/>
        <input type="hidden" name="struDataAttrClusterId" id="struDataAttrClusterId" value="<%=struDataAttrClusterId%>"" />




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
                        Yahoo購物中心新增欄位
                <div class="expanded" style="float: right"></div>
                    </div>
                </td>
            </tr>
            <tr class="div_prod_info">
                <td colspan="2">
                    <table border="0" width="100%">
                        <tr>
                            <td class="bg" width="150"><font class="red">(必填)</font>產品名稱</td>
                            <td>
                                <input type="text" id="NewProductName" name="NewProductName" size="20" maxlength="20" value="<%=NewProductName%>" required='required'/><font style="color:white">__</font><font style="color:red">最多25字(中文算2字，英文算1字)。自動擷取舊產品名稱前25字</font>
                            </td>
                        </tr>
                        <tr>
                            <td class="bg" ><font class="red">(必填)</font>申請人:</td>
                            <td>
                                <input type="text" id="applicant" name="applicant" size="20" maxlength="20" value="<%=yahoolist.Item("applicant").ToString%>" required='required'/><font style="color:white">__</font><font style="color:red">必須填寫中文不得為數字與英文</font>
                            </td>
                        </tr>
                        <tr>
                            <td class="bg"><font class="red">(必填)</font>內容級別:</td>
                            <td colspan="4">
                                <select name="contentRating" id="contentRating" required="required">
                                    <%=contentRatings%>
                                </select><font style="color:white">__</font><font style="color:red">(若為未滿18歲青少年不能購買商品，請選擇限制級)</font>
                            </td>
                        </tr>
                        <tr>
                            <td class="bg"><font class="red">(必填)</font>賣場顯示名稱:</td>
                            <td colspan="4">
                                <br />
                                <input type="text" id="displayName" name="displayName" size="20" maxlength="20" value="<%=yahoolist.Item("displayName").ToString%>" required='required'/>
                            </td>
                        </tr>
                        <tr>
                            <td class="bg"><font class="red">(必填)</font>賣場簡短說明1:</td>
                            <td colspan="4">
                                <input type="text" id="shortDescription_1" name="shortDescription_1" size="20" maxlength="20" value="<%=yahoolist.Item("shortDescription_1").ToString%>" required='required'/><font style="color:white">__</font><font style="color:red">至少需填說明1</font>
                            </td>
                        </tr>
                        <tr>
                            <td class="bg">賣場簡短說明2:</td>
                            <td colspan="4">
                                <br />
                                <input type="text" id="shortDescription_2" name="shortDescription_2" size="20" maxlength="20" value="<%=yahoolist.Item("shortDescription_2").ToString%>" />
                            </td>
                        </tr>
                        <tr>
                            <td class="bg">賣場簡短說明3:</td>
                            <td colspan="4">
                                <br />
                                <input type="text" id="shortDescription_3" name="shortDescription_3" size="20" maxlength="20" value="<%=yahoolist.Item("shortDescription_3").ToString%>" />
                            </td>
                        </tr>
                        <tr>
                            <td class="bg">賣場簡短說明4:</td>
                            <td colspan="4">
                                <br />
                                <input type="text" id="shortDescription_4" name="shortDescription_4" size="20" maxlength="20" value="<%=yahoolist.Item("shortDescription_4").ToString%>" />
                            </td>
                        </tr>
                        <tr>
                            <td class="bg">賣場簡短說明5:</td>
                            <td colspan="4">
                                <br />
                                <input type="text" id="shortDescription_5" name="shortDescription_5" size="20" maxlength="20" value="<%=yahoolist.Item("shortDescription_5").ToString%>" />
                            </td>
                        </tr>

                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="background-color: #faf5dd">
                    <div class="cus_accordion" id="div_setting">
                        <div class="expanded" style="float: left"></div>
                        雅虎類別設定 : <%=yahooCategorycatstext %>
                <div class="expanded" style="float: right"></div>
                    </div>
                </td>
            </tr>
            <tr class="div_setting">
                <td colspan="2">
                    <table border="0" width="100%" class2="doc-table">
                        <tr>
                            <td class="bg" width="120"><font class="red">(必填)</font>所屬分類:</td>
                            <td colspan="5" >
                                <select name="subStationId" id="subStationId" required="required">
                                    <%=yahooCategorysubs%>
                                </select>
                                <select name="categoryId" id="categoryId" required="required">
                                    <%=yahooCategorycats%>
                                </select>
                                <select name="catItemId" id="catItemId" required="required">
                                    <%=yahooCategorycatitems%>
                                </select>
                          
                            </td>
                        </tr>

                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="background-color: #faf5dd">
                    <div class="cus_accordion" id="div_attrsetting">
                        <div class="expanded" style="float: left"></div>
                        結構化屬性設定 : <font style="color:red">顯示屬性都是必填屬性，如要更改請依序由大中小類屬性更改</font>
                <div class="expanded" style="float: right"></div>
                    </div>
                </td>
            </tr>
            <tr class="div_attrsetting">
                <td colspan="2">
                    <table border="0" width="100%" class2="doc-table">
                        <tr>
                            <td class="bg" width="120"><font class="red">(必填)</font>level 1 屬性:</td>
                            <td colspan="5">
                                <select name="level1" id="level1" required="required">
                                    <option value="<%=level1%>"><%=level1%></option>
                                </select> 
                           
                            </td>
                            
                            <td class="bg" width="120"><font class="red">(必填)</font>level 2 屬性:</td>
                            <td colspan="5">
                                <select name="level2" id="level2" required="required">
                                    <option value="<%=level2%>"><%=level2%></option>
                                </select> 
                             
                            </td>

                        </tr>
                        <tr>
                            <td class="bg" ><font class="red">(必填)</font>lv1 屬性別稱:</td>
                            <td colspan="5">
                               
                                <input id='lv1displayname' name='lv1displayname' value="<%=lv1displayname %>" required='required'/>
                            </td>
                            
                            <td class="bg" ><font class="red">(必填)</font>lv2 屬性別稱:</td>
                            <td colspan="5">
                              
                                <input id='lv2displayname' name='lv2displayname' value="<%=lv2displayname %>" required='required'/>
                            </td>

                        </tr>

                      
                         <tr>
                            <td class="bg" ><font class="red">(必填)</font>其餘屬性:</td>
                            <td colspan="10" name="requiredattr" id="requiredattr">
                                <div id="requiredattr1" name="requiredattr1">
                                <%=attrnoncustom%>
                                    </div>
                                </br></br>
                                <div id="requiredattr2" name="requiredattr2">
                                <%=attrcustom%>
                                    </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="background-color: #faf5dd">
                    <div class="cus_accordion" id="div_image">
                        <div class="expanded" style="float: left"></div>
                        Yahoo多媒體欄位設定(既有EC欄位)
                <div class="expanded" style="float: right"></div>
                    </div>
                </td>
            </tr>
            <tr class="div_image">
                <td colspan="2">
                    <table border="0" width="100%">
                        <tr>
                            <td class="bg" width="80"><span class="mustinput">*</span><font class="red">(必填)</font>原圖:<br />
                                500*500(以上)</td>
                            <td>
                                <%If Not String.IsNullOrWhiteSpace(tb.CategoryO) Then%>
                                <input readonly="readonly" type="text" name="CategoryO_old" id="CategoryO_old" value="<%=tb.CategoryO%>" size="50" required="required"/>
                                <a href="<%=EC.mng.Info.Eclife_HomeURL & tb.CategoryO%>" target="_blank">View</a>
                                <br />
                                <%end If%>
                                <img style="border:2px red dashed;" alt="<%=tb.CategoryO %>" src="<%=EC.mng.Info.Eclife_HomeURL & tb.CategoryO%>" />
                            </td>
                        </tr>
                      
                    </table>
                </td>
            </tr>


    <tr class="div_attrsetting">
        <td colspan="2">
            <table border="0" width="100%">
                <tr>
                    <td class="bg"><span class="mustinput">*</span><font class="red">(必填)</font>商品介紹HTML檔:
                    </td>
                    <td colspan="3">
                        注意: 如有使用 html 的 Table語法,Table寬度請勿超過800,以免前台產品頁版型亂掉<br />
                        <textarea wrap="off" style="width:100%" cols="60" rows="6" name="feature" id="feature" class="ckeditor" required="required"><%=tb.feature%></textarea>
                    </td>
                </tr>
             
            </table>
        </td>
    </tr>

            <tr>
                <td colspan="2" align="center">

                    <%-- 2015-04-20 小葉 從主頁移至表單的 --%>
                    <%If EC.mng.Login.ISDeveloper Or (_ID > 0 And prglimit.Modify) Then%>
                    <%--<%If tb.NG = 2 Then%>
                    <a href="#" onclick="javascript:openform_NgFree(<%=tb.cno%>);" class="easyui-linkbutton">福利品</a>
                    <%Else %>--%>
                    <%--<a href="#" onclick="javascript:ProdToNG(<%=tb.cno%>);" class="easyui-linkbutton" >轉NG</a>--%>
                   <%-- <%End If%>--%>
                    <%end if %>
<%--                    <%If _ID > 0 And (EC.mng.Login.ISDeveloper Or prglimit.Delete) Then%>
                    <a href="#" onclick="javascript:del(<%=_ID%>);" class="easyui-linkbutton" icon="icon-cancel">刪除</a>
                    <%End If%>--%>
                    <%If EC.mng.Login.ISDeveloper Or (_ID = 0 And prglimit.Add) Or (_ID > 0 And prglimit.Modify) Then%>
                    <%--<a href="#" onclick="getlengthandsubmit()" class="easyui-linkbutton" icon="icon-ok">儲存</a>--%>
                    <a href="call_YahooAPI.aspx?ProductNo=<%=ProductNo %>" class="easyui-linkbutton" icon="icon-ok">提報</a>
                    <input type="submit" onclick="getlengthandsubmit()" value="暫存" />
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

        //AJAX大類連動中類
        $("#subStationId").on("change", function(){

            //alert($("#subStationId").val());
            $("#categoryId").find("option:selected").text("");
            $("#categoryId").empty();
            $("#catItemId").find("option:selected").text("");
             $("#catItemId").empty();
            $("#brandname").find("option:selected").text("");
            $("#brandname").empty();
            $("#level1").find("option:selected").text("");
             $("#level1").empty();
             $("#level2").find("option:selected").text("");
            $("#level2").empty();
            $("#requiredattr").find("option:selected").text("");
            $("#requiredattr").empty();
            var cats;
            $.ajax({
                type: 'POST',
                async: false,  //使用同步
                url: "/mng/Product/Unitech/yahooshoppingscm/ajax_Get_category.ashx",
                data: 'subStationId=' + $("#subStationId").val() ,
                dataType: "text",
                success: function (info) {
                    
                    cats = info
                    console.log(info)
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //var message = '轉資料時發生錯誤';
                    //alert(message);    //提示視窗
                    alert(XMLHttpRequest+textStatus+errorThrown+'，AJAX失敗')
                }
            })

            $("#categoryId").html(cats);
            
        });
        //AJAX中類連動小類
         $("#categoryId").on("change", function(){

            //alert($("#subStationId").val());
            $("#catItemId").find("option:selected").text("");
             $("#catItemId").empty();
             $("#brandname").find("option:selected").text("");
             $("#brandname").empty();
             $("#level1").find("option:selected").text("");
             $("#level1").empty();
             $("#level2").find("option:selected").text("");
             $("#level2").empty();
             $("#requiredattr").find("option:selected").text("");
            $("#requiredattr").empty();
            var catitems;
            $.ajax({
                type: 'POST',
                async: false,  //使用同步
                url: "/mng/Product/Unitech/yahooshoppingscm/ajax_Get_category.ashx",
                data: 'categoryId=' + $("#categoryId").val() ,
                dataType: "text",
                success: function (info) {
                    
                    catitems = info
                    console.log(info)
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //var message = '轉資料時發生錯誤';
                    //alert(message);    //提示視窗
                    alert(XMLHttpRequest+textStatus+errorThrown+'，AJAX失敗')
                }
            })

            $("#catItemId").html(catitems);
            
         });





        //AJAX小類連動屬性 - level1
         $("#catItemId").on("change", function(){

            //alert($("#subStationId").val());
             $("#level1").find("option:selected").text("");
             $("#level1").empty();
             $("#level2").find("option:selected").text("");
             $("#level2").empty();
             $("#requiredattr").find("option:selected").text("");
            $("#requiredattr").empty();
             
            var attrs;
            $.ajax({
                type: 'POST',
                async: false,  //使用同步
                url: "/mng/Product/Unitech/yahooshoppingscm/ajax_Get_attributes.ashx",
                data: 'catItemId=' + $("#catItemId").val()+'&attrclass=level1' ,
                dataType: "text",
                success: function (info) {
                    
                    attrs = info
                    console.log(info)
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //var message = '轉資料時發生錯誤';
                    //alert(message);    //提示視窗
                    alert('此小類無品牌屬性確定選取?')
                }
            })
             $("#level1").html(attrs);
        
            
         });

        //AJAX小類連動屬性 - level2
         $("#level1").on("change", function(){

            //alert($("#subStationId").val());
           
            $("#level2").find("option:selected").text("");
             $("#level2").empty();
             $("#requiredattr").find("option:selected").text("");
            $("#requiredattr").empty();
            var attrs;
            $.ajax({
                type: 'POST',
                async: false,  //使用同步
                url: "/mng/Product/Unitech/yahooshoppingscm/ajax_Get_attributes.ashx",
                data: 'catItemId=' + $("#catItemId").val()+'&attrclass=level2&level1='+$("#level1").val() ,
                dataType: "text",
                success: function (info) {
                    
                    attrs = info
                    console.log(info)
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //var message = '轉資料時發生錯誤';
                    //alert(message);    //提示視窗
                    alert('此小類無品牌屬性確定選取?')
                }
            })
      
            $("#level2").html(attrs);
            
         });

        //AJAX小類連動屬性 - 必填屬性
        $("#level2").on("change", function(){

            //alert($("#subStationId").val());
            $("#requiredattr").find("option:selected").text("");
            $("#requiredattr").empty();
            var attrs;
            $.ajax({
                type: 'POST',
                async: false,  //使用同步
                url: "/mng/Product/Unitech/yahooshoppingscm/ajax_Get_attributes.ashx",
                data: 'catItemId=' + $("#catItemId").val()+'&attrclass=required&level1='+$("#level1").val()+'&level2='+$("#level2").val()  ,
                dataType: "text",
                success: function (info) {
                    
                    attrs = info
                    console.log(info)
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //var message = '轉資料時發生錯誤';
                    //alert(message);    //提示視窗
                    alert('此小類無必填屬性確定選取?')
                }
            })

            $("#requiredattr").html(attrs);
            
            
        });

        //AJAX小類連動屬性 - 取小類ID屬性
        $("#catItemId").on("change", function(){

            //alert($("#subStationId").val());
            $("#requiredattr").find("option:selected").text("");
            $("#requiredattr").empty();
            var attrs;
            $.ajax({
                type: 'POST',
                async: false,  //使用同步
                url: "/mng/Product/Unitech/yahooshoppingscm/ajax_Get_attributes.ashx",
                data: 'catItemId=' + $("#catItemId").val()+'&attrclass=struDataAttrClusterId',
                dataType: "text",
                success: function (info) {
                    
                    attrs = info
                    console.log(info)
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //var message = '轉資料時發生錯誤';
                    //alert(message);    //提示視窗
                    alert('此小類無屬性ID確定選取?')
                }
            })
            
            //alert(attrs);
            $("#struDataAttrClusterId").val(attrs);
            
        });


        //傳遞動態下拉選項數目至Form_CRUD_Save

       function getlengthandsubmit(){

           //alert($("#requiredattr1>select").length)
           //alert($("#requiredattr2>input").length/2)

           $("#attrnoncustomcount").val($("#requiredattr1>select").length);
           $("#attrcustomcount").val($("#requiredattr2>input").length / 2);
           

           //$('#ff').submit();

        };

            //開啟插入圖片的 popup (2012-09-07 友)
    function OpenImg2ckEditor(editor) {
        var title = '插入圖片: CNO=' + _cno + ' PrgID=' + _prgid;
        var url = '/mng/product/Unitech/UpPhoto/UpPhoto.aspx?PrgID=' + _prgid + '&CNO=' + _cno + '&editor=' + editor;
        parent.OpenCRUDForm(window, title, url, 680, 400, _winlevel);  //呼叫 /default.aspx 的 OpenCRUDForm( Level = 1)
    }



    </script>
   
</asp:Content>
