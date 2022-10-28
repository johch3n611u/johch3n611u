/*/////////////////////////////////////////////////////////////////////////////////
' 表單共用的 Function
'
' 建檔日期: 2012-08-28
' 建檔人員: 阿友
' 修改記錄: 2012-09-06 友: 增加 Function/event.
'
' 相關程式: Form_CRUD.aspx , Form_CRUD_Spec_yellow.aspx
' 呼叫來源: 
/////////////////////////////////////////////////////////////////////////////////*/

/* 頁面載入完成後 ****************************************************************/
$(function () {

    /* 館/大/中/小類 onchange 時 */
    $("#houseno").change(function () {  //館別onchange
        //alert('houseno0');
        $('#largeno').find('option').remove();
        $('#mediumno').find('option').remove();
        $('#mediumsubno').find('option').remove();

        $("#largeno").AddOption("全部大類", "", false, 0);
        $("#mediumno").AddOption("全部中類", "", false, 0);
        $("#mediumsubno").AddOption("全部小類", "", false, 0);
    })
    $("#largeno").change(function () {  //大類onchange
        //alert('largeno');
        $('#mediumno').find('option').remove();
        $('#mediumsubno').find('option').remove();

        $("#mediumno").AddOption("全部中類", "", false, 0);
        $("#mediumsubno").AddOption("全部小類", "", false, 0);
    })
    $("#mediumno").change(function () {  //中類onchange
        //alert('mediumno');
        $('#mediumsubno').find('option').remove();
        $("#mediumsubno").AddOption("全部小類", "", false, 0);
    })

    //tr 欄位縮合項目 ********************************************************************
    $('.cus_accordion').each(function () {
        $(this).attr('title', 'Click一下可縮合或展開');
    }).click(function () {
        var _Main_Div = $(this);

        var _Main_Div_ID = _Main_Div.attr('id');
        //alert(_Main_Div_ID);

        var _child_div = _Main_Div.children('div');
        var _child_div_class = _child_div.attr('class');     //取縮合div 的class名稱

        //alert(_child_div.length + ' , ' + _child_div_class);

        _child_div.removeClass(_child_div_class);
        if (_child_div_class == 'expanded') {  //縮
            _child_div.addClass('collapsed');
        } else {  //合
            _child_div.addClass('expanded');
        }

        //Show or Hide 項目
        var _obj = $('.' + _Main_Div_ID);
        _obj.toggle();
    }).change(function () {
        alert('change=' + $(this).attr('id'));
    });
    //end tr 欄位縮合項目 ********************************************************************

});


/* Function ***********************************************************************************************/

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

//開啟 "異動記錄" 的表單 *************************************************************
function openform_list_mod_rec() {
    var _url = '/mng/product/list/list_mod_rec/Form_List_Mod_Rec.aspx?cno=' + _cno;
    var _crudform_width = 700;
    var _crudform_height = 450;
    parent.OpenCRUDForm(window, '異動記錄: cno=' + _cno, _url, _crudform_width, _crudform_height, _winlevel);
}

//關閉(自己) easyui-window *************************************************************
function closeme() {
    //alert('_winlevel=' + _winlevel + ',' + winlevel);
    parent.CloseCRUDForm(winlevel);     //呼叫的是 /default.aspx 內的 CloseCRUDForm()
}

//黃區活動列表頁
function openform_yellow_area() {
    var _url = '/mng/product/Yellow_Area/default.aspx?cno=' + _cno;
    var _crudform_width = 700;
    var _crudform_height = 500;
    parent.OpenCRUDForm(window, '黃區活動列表: cno=' + _cno, _url, _crudform_width, _crudform_height, _winlevel);
}