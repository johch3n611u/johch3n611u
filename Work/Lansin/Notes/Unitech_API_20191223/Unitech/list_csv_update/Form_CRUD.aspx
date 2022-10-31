<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="Form_CRUD.aspx.vb" Inherits="mng_ShoppingCash_SC_UserList_Form_CRUD" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<form id="ff" action="Form_CRUD_Save.aspx?PrgID=<%=PrgID%>" method="post" enctype="multipart/form-data" style2="padding: 10px 20px 10px 40px;">
<input type="hidden" name="scGroupId" id="scGroupId" value="<%=_ID%>" />
<input type="hidden" name="action" id="action" value="" />

<table border="1" width="100%" class="doc-table">

    <tr>
        <td align="right"><font color="red">＊</font>CSV名單:</td>
        <td>
            <a href="sample.csv" target="_blank">範例檔下載</a>
            <br />
            <input type="file" name="file1" id="file1">

        </td>
    </tr>

    <tr>
        <td colspan="2" align="center">
            <%If _ID > 0 And (EC.mng.Login.ISDeveloper Or prglimit.Delete) Then%>
            <a href="#" onclick="javascript:del(<%=_ID%>);" class="easyui-linkbutton" icon="icon-cancel">刪除</a>
            <%End If%>
            <%If EC.mng.Login.ISDeveloper Or (_ID = 0 And prglimit.Add) Or (_ID > 0 And prglimit.Modify) Then%>
            <a href="#" onclick="javascript:$('#ff').submit();" class="easyui-linkbutton" icon="icon-ok">儲存</a>
            <%End If%>
            <a href="#" onclick="javascript:closeme();" class="easyui-linkbutton" icon="icon-cancel">取消</a>
        </td>
    </tr>
</table>
</form>


<script type="text/javascript">
    var _prgid = '<%=prgid%>';        //取主程式ID = 主選單MenuID

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
                    parent.reloadgrid();
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

    //關閉(自己) easyui-window 
    function closeme() {
        parent.CloseCRUDForm();     //呼叫的是 /default.aspx 內的 CloseCRUDForm()
    }

    $(function () {

        //表單 Bind easyui-form
        $('#ff').form({
            onSubmit: function () {
                //alert('onSubmit');

           

                <%If tb.scGroupId = 0 Then%>
                //名單
                if ($('#file1').val() != '') {
                    if (!checkFileExt($('#file1'), ['.txt', '.csv'])) { return false; }
                } else {
                    if ($('#file1').val() == '') {
                        alert('請上傳CSV名單');
                        $('#file1').focus();
                        return false;
                    }
                }
                <%End If%>


                if (!confirm('資料正確嗎?')) {
                    return false;
                }

                return $(this).form('validate');   //欄位驗證

            },
            success: function (data) {
                var rtn = eval(data);
                var status = rtn[0].status;      //回傳結果: ok=成功
                var message = rtn[0].message;    //回傳訊息
                var runScript = rtn[0].script;   //回傳執行 javascript

                parent.messager_alert(parent._FromWindow, message, runScript)    //呼叫提示視窗

                if (status == 'ok') {
                    parent.reloadgrid();       //重新載入 default.aspx 的 Grid
                    closeme();                 //關閉此頁
                //} else {
                //    $('#action').val('');      //清除表單 del 動作
                //    if (runScript != '') eval(data2[0].runScript);     //執行 script
                }
            }
        });
    });
</script>
</asp:Content>