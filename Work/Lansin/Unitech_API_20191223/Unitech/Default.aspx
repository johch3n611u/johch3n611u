<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="mng_Product_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title>商品管理</title>
    <script type="text/javascript">
        var _prgid = '<%=prgid%>';                                             //取主程式ID = 主選單MenuID
        var _treegrid_title_sort_field = 'ModifyDate';                           //treegrid 預設要排序的欄位
        var _treegrid_title_sort_field_desc = 'desc';                          //treegrid 預設要排序的欄位方式
        var _treegrid_pagesize = 20;                                           //每頁筆數
        var _treegrid_jsonDataURL = 'default_jsondata.aspx?PrgID=' + _prgid;   //資料來源
        var _treegrid_jsonDataURLParam = '&selhouseno=<%=houseno%>';           //資料來源URL參數
        var _crudform_width = '950';                                             //表單寬度
        var _crudform_height = 'auto';                                            //表單高度
        var _currentPath = '<%=_currentPath%>';                                //程式目錄位置 ex: /mng/Product/Category/LargeNo/
        var _winlevel = 1;
    </script>
    <script type="text/javascript" src="/lib/js/default_dataGrid_Script.js"></script>
    <script type="text/javascript">
        /**新增/修改/刪除/搜尋 功能********************************************************************************************/
        //表單: 新增/修改
        function OpenCRUDForm(action) {
            var node = $('#tt').datagrid('getSelected');
            //alert(node + ',' + action);
            var ID = 0;
            if (action != 'add') ID = action;  //node.id;
            var title = '修改';
            if (action == 'add') title = '新增';

            title += ' cno: ' + ID;

            //載入 /default.aspx 內的表單頁面
            var url = _currentPath + 'Form_CRUD.aspx?PrgID=' + _prgid + '&id=' + ID;
            parent.OpenCRUDForm(window, '<%=prgname%>' + title, url, _crudform_width, _crudform_height);
        }

        //表單Spec: 修改 
        function OpenCRUDForm_Spec_yellow() {
            var node = $('#tt').datagrid('getSelected');
            var ID = node.id;
            var title = 'spec修改';
            title += ' cno: ' + node.id;

            //載入 /default.aspx 內的表單頁面
            var url = _currentPath + 'Form_CRUD_Spec_yellow.aspx?PrgID=' + _prgid + '&id=' + ID;
            parent.OpenCRUDForm(window, '<%=prgname%>' + title, url, _crudform_width, _crudform_height);
        }

        //刪除
        function del(id) {
            if (confirm('確定刪除嗎?')) {
                $.ajax({
                    type: 'POST',
                    async: false,  //使用同步
                    url: "Del.aspx",
                    data: 'PrgID=' + _prgid + '&ID=' + id,
                    dataType: "json",
                    success: function (info) { //{"status" : "ok", "message" : "成功"}
                        var status = info.status;
                        var message = info.message;
                        reloadgrid();
                        alert(message);    //提示視窗
                        if (status == 'ok') closeme();
                    },
                    error: function (html) {
                        var message = '刪資料時發生錯誤';
                        alert(message);    //提示視窗
                    }
                });
            }
        }

        //搜尋
        function search(type) {

            var selitemcat = $('#selitemcat').val();
            var selhouseno = $('#selhouseno').val();
            var sellargeno = $('#sellargeno').val();
            var selmediumno = $('#selmediumno').val();
            var selmediumsubno = $('#selmediumsubno').val();
            var selempno = $('#selempno').val();
            var selMode = $('#selMode').val();
            var selSort = $('#selSort').val();
            var selOptions = $('#selOptions').val();
            var keyword = $('#keyword').val();

            if (selitemcat == undefined) selitemcat = '';
            if (selhouseno == undefined) selhouseno = '';
            if (sellargeno == undefined) sellargeno = '';
            if (selmediumno == undefined) selmediumno = '';
            if (selmediumsubno == undefined) selmediumsubno = '';
            if (selempno == undefined) selempno = '';
            if (selMode == undefined) selMode = '';
            if (selSort == undefined) selSort = '';
            if (selOptions == undefined) selOptions = '';

            keyword = keyword.replace(/請輸入關鍵字/, '');
            keyword = escape(keyword);   //中文編碼

            //傳遞的參數
            _treegrid_jsonDataURLParam = '&selitemcat=' + selitemcat +
                '&selhouseno=' + selhouseno +
                '&sellargeno=' + sellargeno +
                '&selmediumno=' + selmediumno +
                '&selmediumsubno=' + selmediumsubno +
                '&selempno=' + selempno +
                '&selMode=' + selMode +
                '&selSort=' + selSort +
                '&selOptions=' + selOptions +
                '&keyword=' + keyword;

            if (type == "excel") {
                var url = '/mng/product/Unitech/Default_Excel.aspx?PrgID=' + _prgid + _treegrid_jsonDataURLParam;
                $("#template_Excel").append("<iframe id='Excel' src='" + url + "' style='display:none' ></iframe>");
            } else {
                //將頁次切換為第1頁(2012-05-18 友: 這個方式只會改變 input 頁次的欄位值,但傳遞出去的值要設 $('#tt').datagrid('options').pageNumber = 1 才有用,treegrid bug.)
                var pg = $('#tt').datagrid('getPager').pagination('options');
                pg.pageNumber = 1;

                //重傳參數
                var _tt_options = $('#tt').datagrid('options');
                _tt_options.pageNumber = 1;    //將頁次切換為第1頁
                _tt_options.url = _treegrid_jsonDataURL + _treegrid_jsonDataURLParam;
                _tt_options.sortName = _treegrid_title_sort_field;
                _tt_options.sortOrder = _treegrid_title_sort_field_desc;
                reloadgrid();    //重新載入資料
            }
        }
        /**TreeGrid 欄位格式化**************************************************************************************************/

        //主標題
        function format_title(val, row) {
            var _isnew = row.isnew;  //* = 新品
            var _iszprd = row.iszprd;
            var _form_function = 'OpenCRUDForm_Spec_yellow';

            if (_isnew.length > 0) _isnew = '<font color=red>' + _isnew + '</font>';
            if (_iszprd == 'Y') {  //Z-代碼的商品
                _form_function = 'OpenCRUDForm';
            }

            var newTitle = '<a href="javascript:' + _form_function + '(' + row.id + ');" style="text-decoration: none">' + _isnew + val + '</a>';
            return newTitle;
        }

        //格式化 狀態 欄位內容
        function format_online(val, row) {
            var newOnline = ''
            if (val == 1)
                newOnline = '上線';
            else
                newOnline = '<font color=blue>下線</font>';

            return newOnline;
        }

        //位置
        //function format_role(val, row) {
        //    var str = ''
        //    switch(val) {
        //        case 0:
        //            str = '一般';
        //            break;
        //        case 6:
        //            str = '分館';
        //            break;
        //        case 7:
        //            str = '館長';
        //            break;
        //        case 8:
        //            str = '首頁';
        //            break;
        //        default:
        //            str = '';
        //            break;
        //    }
        //    return str;
        //}

        //尾碼
        function format_ls3cproductno(val, row) {
            var str = val.substr(val.length - 1);
            if ('!@%'.indexOf(str) == -1) {
                str = '';
            }
            return str;
        }

        //黃區特價
        function format_mprice(val, row) {
            var str = val;
            if (parseInt(row.giftlength) > 0) {
                str += "贈";
            }
            return str;
        }
        //金鑽區特價
        function format_vip_mprice(val, row) {
            var str = val;
            if (parseInt(row.vip_giftLength) > 0) {
                str += "贈";
            }
            return str;
        }
        //項目的功能
        function format_function(val, row) {
            var newStr = '';
            newStr = '<a href="javascript:OpenCRUDForm(' + row.id + ');" style="text-decoration: none">修改</a>';
            return newStr;
        }

        //異動紀錄 / 商品預覽
        function format_funcmemo(val, row) {
            var newStr = '';
            //newStr = '<input type=button onclick="javascript:openform_articleSet(' + row.id + ');" value="文章設定"><br>';
            newStr += '<input type=button onclick="javascript:openform_list_mod_rec(' + row.id + ');" value="異動紀錄"><br>';
            //newStr += '<input type=button onclick="javascript:openform_preview(' + row.id + ');" value="商品預覽">';
            return newStr;
        }

        //商品異動 : 上下架 / 基本資料 / 可購量 / 轉NG
        function format_funcmodify(val, row) {
            var newStr = '';
            //newStr = '<input type=button onclick="javascript:openform_online(' + row.id + ');" value="上下架"><br>';
            newStr += '<input type=button onclick="javascript:OpenCRUDForm(' + row.id + ');" value="基本資料">' +
                '<input type=button onclick="javascript:openform_specialNeeded(' + row.id + ');" value="獨享欄位"><br>';
            //newStr += '<input type=button onclick="javascript:openform_totalbuy(' + row.id + ');" value="價格/可購量"><br>';

            //福利品(2014-02-25 友)
            //if (row.isngfree == '1') {
            //    newStr += '<input type=button onclick="javascript:openform_NgFree(' + row.id + ');" value="福利品"><br>';
            //} else {
            //    //2015-04-20 小葉 AMY:移至基本資料
            //    newStr += '<input type=button onclick="javascript:ProdToNG(' + row.id + ');" value="轉NG"><br>';
            //}

            //2014-07-04 友
            //newStr += '<input type=button onclick="javascript:Prod_ShipSetup(' + row.id + ');" value="配送設定"><br>';

            //2014-12-09 友
            newStr += '<input type=button onclick="javascript:openform_cooperation_setdndate(' + row.id + ');" value="外網設定"><br>';

            //2015-04-20 小葉 整合 (上下架 + 配送設定 + 價格/可購量) 為 購買設定
            newStr += '<input type=button onclick="javascript:BuyTheSet(' + row.id + ');" value="購買設定"><br>';

            return newStr;
        }

        //活動設定 : 區間促銷 / 加價購 / 原廠活動 / Coupon設定
        function format_funcevt(val, row) {
            var newStr = '';
            newStr = '<input type=button onclick="javascript:openform_yellow_area(' + row.id + ');" value="區間促銷">';
            newStr += '<input type=button onclick="javascript:openform_buyother(' + row.id + ');" value="加價購"><br>';
            newStr += '<input type=button onclick="javascript:openform_cooperationevent(' + row.id + ');" value="原廠活動">';
            newStr += '<input type=button onclick="javascript:openform_coupon(' + row.id + ');" value="Coupon設定"><br>';
            newStr += '<input type=button onclick="javascript:openform_SelNGift(' + row.id + ');" value="好禮任選">';
            return newStr;
        }

        //20191017 育誠:需求單 list.rebuy 回購欄位增加
        function format_rebuy(val, row) {
            var newStr = '';
            if (val == 1) {
                newStr = '否';
            }
            else {
                newStr = '是';
            };

            return newStr;
        }

        //20191017 育誠:需求單 list.rebuy 回購欄位增加
        function format_rebuy(val, row) {
            var newStr = '';
            if (val == 1) {
                newStr = '否';
            }
            else {
                newStr = '是';
            };

            return newStr;
        }

        //是否為購物中心提報商品
        function format_Boolean(val, row) {
            var newStr = '';
            if (val == 'False') {
                newStr = '否';
            }
            else if (val == 'True') {
                newStr = '是';
            }
            else if (val == '') {
                newStr = '未建檔';
            }
            else {
                newStr = '已建檔';
            };
            return newStr;
        }

        //20191121 育誠:Yahoo購物中心API專案
        function format_yahooshoppingscm(val, row) {
            var newStr = '';
            newStr = '<input type=button onclick="javascript:openform_yahooshoppingscm(' + row.id + ');" value="獨享欄位"><br>';

            return newStr;
        }

        //20191121 育誠:Momo購物中心API專案
        function format_momoshoppingscm(val, row) {
            var newStr = '';
            newStr = '<input type=button onclick="javascript:openform_momoshoppingscm(' + row.id + ');" value="獨享欄位"><br>';

            return newStr;
        }

        //20191121 育誠:雅虎購物中心API專案
        //提報雅虎購物中心商品
        function list_csv_update(evtid) {

            if (evtid == undefined) evtid = 0;

            var title = '';
            if (evtid == 0) {
                title = '新增名單';
            } else {
                title = '修改名單';
            }

            //載入 表單頁面
            title += ' -- id: ' + evtid;
            var url = '/mng/product/Unitech/list_csv_update/Form_CRUD.aspx?PrgID=' + _prgid + '&id=' + evtid;
            var Level = 1;   //使用第1層
            parent.OpenCRUDForm(window, title, url, 650, 120, Level);  //呼叫 /default.aspx 的 OpenCRUDForm()

        }
        //開啟 Yahoo購物中心 的表單 *************************************************************
        function openform_yahooshoppingscm(action) {

            var node = $('#tt').datagrid('getSelected');
            //alert(node + ',' + action);
            var ID = 0;
            if (action != 'add') ID = action;  //node.id;
            var title = '修改';
            if (action == 'add') title = '新增';

            title += ' cno: ' + ID;

            //載入 /default.aspx 內的表單頁面
            var url = '/mng/product/Unitech/yahooshoppingscm/Form_CRUD.aspx?PrgID=' + _prgid + '&id=' + ID;
            parent.OpenCRUDForm(window, '<%=prgname%>' + title, url, _crudform_width, _crudform_height);

        }

        //開啟 Momo購物中心 的表單 *************************************************************
        function openform_momoshoppingscm(action) {

            var node = $('#tt').datagrid('getSelected');
            //alert(node + ',' + action);
            var ID = 0;
            if (action != 'add') ID = action;  //node.id;
            var title = '修改';
            if (action == 'add') title = '新增';

            title += ' cno: ' + ID;

            //載入 /default.aspx 內的表單頁面
            var url = '/mng/product/Unitech/momoshoppingscm/Form_CRUD.aspx?PrgID=' + _prgid + '&id=' + ID;
            parent.OpenCRUDForm(window, '<%=prgname%>' + title, url, _crudform_width, _crudform_height);

        }


        //提報所有該購物中心商品
        function call_API(action) {
            var title = "";
            title = $("#call_API").val();
            var url = "";
            switch (title) {

                case 'YahooSCM':
                    url = '/mng/product/Unitech/yahooshoppingscm/call_YahooAPI.aspx?RequestString=POST_proposals';
                    //載入 /default.aspx 內的表單頁面
                    parent.OpenCRUDForm(window, title +'<%=prgname%>', url, _crudform_width, _crudform_height);
                    break;
                case 'MomoSCM':
                    url = '/mng/product/Unitech/momoshoppingscm/call_MomoAPI.aspx?RequestString=POST_proposals';
                    //載入 /default.aspx 內的表單頁面
                    parent.OpenCRUDForm(window, title +'<%=prgname%>', url, _crudform_width, _crudform_height);
                    break;
                default:
                    alert('請選擇提報項目!');
                    break;

            }

        }


        /***********************************************************************************************************************/

        /***********************************************************************************************************************/
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="tb" style="padding: 5px; height: auto">
        <div>
            列出
            <select name="selitemcat" id="selitemcat">
                <option value="">一般商品</option>
                <option value="isngfree">NG福利品</option>
            </select>
            <select name="selhouseno" id="selhouseno"></select>
            <select name="sellargeno" id="sellargeno"></select>
            <select name="selmediumno" id="selmediumno"></select>
            <select name="selmediumsubno" id="selmediumsubno"></select>
            <select name="selempno" id="selempno"></select>
            ，搜尋
			<select name="selOptions" id="selOptions" onchange="">
                <option value="productno">良興代碼</option>
                <option value="ls3cproductno">良興品名</option>
                <option value="productname">產品名稱</option>
                <option value="offer">供應商代碼</option>
                <option value="cno">自動編號</option>
                <option value="houseno">館別編號</option>
                <option value="largeno">大類編號</option>
                <option value="mediumno">中類編號</option>
                <option value="mediumsubno">小類編號</option>
                <option value="empno">採購代碼</option>
            </select>
            <input type="text" size="15" name="keyword" id="keyword" />
            <a href="javascript:search('earch');" class="easyui-linkbutton" iconcls="icon-search">搜尋</a>
            <%--        <a href="javascript:search('excel');" class="easyui-linkbutton">Excel匯出</a>--%>
            <a href="javascript:call_API('POST_proposals');" class="easyui-linkbutton">提報所有</a>
            <select id="call_API" name="call_API">
                <option value="">未選擇</option>
                <option value="YahooSCM">YahooSCM</option>
                <option value="MomoSCM">MomoSCM</option>
            </select>商品
            <a href="javascript:list_csv_update();" class="easyui-linkbutton" iconcls="icon-add">新增提報清單</a>
            <div id="template_Excel"></div>
            <%--<a href="javascript:OpenCRUDForm('add');" class="easyui-linkbutton" iconCls="icon-add">新增</a>--%>
        </div>
    </div>

    <table id="tt" title="<%=prgname%>管理 -- (<a href='javascript:location.reload();'>重整頁面</a>)" class2="easyui-treegrid" style="width: 90%; height: auto" align="center" toolbar="#tb"
        url2="default_jsondata.aspx" idfield="id" treefield="name" pagination="true" fitcolumns="false">
        <thead>
            <tr>
                <th field="productname" sortable="true" width="300" align="left" <%--formatter="format_title"--%>>
                    <font class="cansort">產品名稱</font>
                </th>
                <th field="productno" sortable="true" width="70" align="center">
                    <font class="cansort">良興代碼</font>
                </th>
                <%--               <th field="saleprice" sortable="true" width="60" align="right">
                    <font class="cansort">市價</font>
                </th>--%>
                <th field="spicalprice" sortable="true" width="60" align="right">
                    <font class="cansort">網路價</font>
                </th>
                <th field="memsaveprice" sortable="true" width="60" align="right" formatter="format_mprice">
                    <font class="cansort">特價</font>
                </th>
                <%--                 <th field="vip_memsaveprice" sortable="true" width="60" align="right" formatter="format_vip_mprice">
                    <font class="cansort">金賺</font>
                </th>--%>
                <th field="online" sortable="true" width="45" align="center" formatter="format_online">
                    <font class="cansort">上架</font>
                </th>
                <th field="num" sortable="true" width="40" align="center">
                    <font class="cansort">庫存</font>
                </th>
                <th field="ls3cproductno" sortable="false" width="35" align="center" formatter="format_ls3cproductno">
                    <font class="">尾碼</font>
                </th>
                <th field="rebuy" sortable="" width="50" align="center" formatter="format_rebuy">
                    <font class="">回購型(耗材)</font>
                </th>
                <%--<th field="cno" width="180" align="left" formatter="format_funcmodify">
                    <font class="">商品異動</font>
                </th>--%>
                <%--  <th field="none" width="180" align="left" formatter="format_funcevt">
                    <font class="">活動設定</font>
                </th>--%>

                <th field="sort" width="100" align="left" formatter="format_funcmemo">備註
                </th>

                <th field="yahooshoppingscm" width="120" align="center" formatter="format_yahooshoppingscm">Yahoo購物中心
                </th>
                <th field="Yahoo_Report" width="120" align="center" formatter="format_Boolean">是否為Yahoo提報商品
                </th>
                <th field="Yahooposteddate" width="120" align="center" formatter="format_Boolean">是否已建檔
                </th>

                <th field="momoshoppingscm" width="120" align="center" formatter="format_momoshoppingscm">Momo購物中心
                </th>
                <th field="Yahoo_Report" width="120" align="center" formatter="format_Boolean">是否為Momo提報商品
                </th>
                <th field="Momoposteddate" width="120" align="center" formatter="format_Boolean">是否已建檔
                </th>

            </tr>
        </thead>
    </table>

    <pre>1.產品名稱 前面有 <font color="red">*</font> 為 新品資料。</pre>

    <script type="text/javascript" src="ButtonFunction.js"></script>
    <script type="text/javascript">
        $(function () {
            SelectCascading();
        });

        /* 館別/分類下拉清單 連動 for web service */
        function SelectCascading() {
            $("#selempno").FillOptions("/WebService/ws_xAdmin.asmx/Procurement_json", { datatype: "json", textfield: "text", valuefiled: "id", webservice_param: '{ _rtnFormat: 0 }' });
            $("#selempno").AddOption("全部採購", "", true, 0);

            $("#selhouseno").FillOptions("/WebService/ws_list_category.asmx/ListHouseType", { datatype: "json", textfield: "text", valuefiled: "id", webservice_param: '{ _HouseNO: "", _IsVirtual: 0, _rtnFormat: 0 }' });

            $("#selhouseno").AddOption("全部館別", "", true, 0);
            $("#sellargeno").AddOption("全部大類", "", true, 0);
            $("#selmediumno").AddOption("全部中類", "", true, 0);
            $("#selmediumsubno").AddOption("全部小類", "", true, 0);

            $("#selhouseno").CascadingSelect(  //館別 連動 大類
                $("#sellargeno"),
                "/WebService/ws_list_category.asmx/ListLargeType",
                {
                    datatype: "json", textfield: "text", valuefiled: "id", parameter: "selhouseno",
                    webservice_param: '{ _HouseNO: "$(\'#selhouseno\').val()" , _LargeNO: "", _Online: -1, _Empno: "", _rtnFormat: 0 }'
                },
                function () {
                    $("#sellargeno").AddOption("全部大類", "", true, 0);
                    $("#sellargeno").val('<%=largeno%>');
                }
            );

            $("#sellargeno").CascadingSelect(  //大類 連動 中類
                $("#selmediumno"),
                "/WebService/ws_list_category.asmx/ListMediumType",
                {
                    datatype: "json", textfield: "text", valuefiled: "id", parameter: "sellargeno",
                    webservice_param: '{ _HouseNO: "$(\'#selhouseno\').val()" , _LargeNO: "$(\'#sellargeno\').val()", _MediumNO: "", _Online: -1, _listlargetype_Online: "-1", _ShowInList: "1", _rtnFormat: 0 }'
                },
                function () {
                    $("#selmediumno").AddOption("全部中類", "", true, 0);
                    $("#selmediumno").val('<%=mediumno%>');
                }
            );

            $("#selmediumno").CascadingSelect(  //中類 連動 小類
                $("#selmediumsubno"),
                "/WebService/ws_list_category.asmx/ListMediumSubType",
                {
                    datatype: "json", textfield: "text", valuefiled: "id", parameter: "sellargeno",
                    webservice_param: '{ _HouseNO: "$(\'#selhouseno\').val()" , _LargeNO: "$(\'#sellargeno\').val()", _MediumNO: "$(\'#selmediumno\').val()", _MediumSubNO: "$(\'#selmediumsubno\').val()", _Online: -1, _listlargetype_Online: "-1", _listmediumtype_Online: "-1", _isTrueCategory: "1", _rtnFormat: 0 }'
                },
                function () {
                    $("#selmediumsubno").AddOption("全部小類", "", true, 0);
                    $("#selmediumsubno").val('<%=mediumsubno%>');
                }
            );

        }






    </script>
</asp:Content>
