/*/////////////////////////////////////////////////////////////////////////////////
' 主頁/表單共用的 Function
'
' 建檔日期: 2013-03-05
' 建檔人員: 阿友
' 修改記錄: 2015-02-13 天 增加呼叫任選禮品的Function (搜尋:2015-02-13 天 好禮任選)
'
' 相關程式: default.aspx , Form_CRUD.aspx , Form_CRUD_Spec_yellow.aspx
' 呼叫來源: 
/////////////////////////////////////////////////////////////////////////////////*/

/* 頁面載入完成後 ****************************************************************/
$(function () {

});

/* Function ***********************************************************************************************/

//設定商品在合作廠商匯出記錄表的 DnDate (2014-12-09 友)
function openform_cooperation_setdndate(_cno) {
    //_cno = 29913;   //For test
    var _url = '/mng/product/list/Cooperation_SetDnDate/Form.aspx?PrgID=' + _prgid + '&cno=' + _cno;
    var _crudform_width = 500;
    var _crudform_height = 300;
    parent.OpenCRUDForm(window, '外網下架 cno:' + _cno, _url, _crudform_width, _crudform_height, _winlevel);
}

//NG福利品的檢測表填寫 (2014-02-26 友)
function openform_NgFree(_cno) {
    //_cno = 29913;   //For test
    var _url = '/mng/product/NewProd/Form_NGFree.aspx?PrgID=' + _prgid + '&cno=' + _cno;
    var _crudform_width = 'auto';
    var _crudform_height = 450;
    parent.OpenCRUDForm(window, '福利品 cno:' + _cno, _url, _crudform_width, _crudform_height, _winlevel);
}


//將商品複製一份為NG品
function ProdToNG(_cno) {
    if (confirm('確定將商品複製成NG品嗎?')) {
        $.ajax({
            type: 'POST',
            async: false,  //使用同步
            url: "/mng/product/list/ProdToNG.aspx",
            data: 'PrgID=' + _prgid + '&cno=' + _cno,
            dataType: "json",
            success: function (info) {
                var status = info.status;
                var message = info.message;
                alert(message);
                if (status == 'ok') closeme();
            },
            error: function (html) {
                var message = '轉資料時發生錯誤';
                alert(message);    //提示視窗
            }
        })
    }
}

//2015-02-13 天 好禮任選
//開啟 "好禮任選" 的表單 *************************************************************
function openform_SelNGift(_cno) {
    var _url = '/mng/Product/SelNGift/List/Form_CRUD_SelNGift.aspx?cno=' + _cno;
    var _crudform_width = 1000;
    var _crudform_height = 400;
    parent.OpenCRUDForm(window, '好禮任選: cno=' + _cno, _url, _crudform_width, _crudform_height, _winlevel);
}

//開啟 "商品預覽" 的表單 *************************************************************
function openform_preview(_cno) {
    var _url = '/mng/product/list/preview/moreinfo.aspx?cno=' + _cno;
    var _crudform_width = 850;
    var _crudform_height = 500;
    parent.OpenCRUDForm(window, '商品預覽: cno=' + _cno, _url, _crudform_width, _crudform_height, _winlevel);
}

//開啟 "價格/可購量" 的表單 ***************************************************************
function openform_totalbuy(_cno) {
    var _url = '/mng/product/list/Form_CRUD_TotalBuy.aspx?cno=' + _cno;
    var _crudform_width = 500;
    var _crudform_height = 250;
    parent.OpenCRUDForm(window, '價格/可購量: cno=' + _cno, _url, _crudform_width, _crudform_height, _winlevel);
}

//開啟 "上下架" 的表單 ***************************************************************
function openform_online(_cno) {
    var _url = '/mng/product/list/Form_CRUD_Online.aspx?cno=' + _cno;
    var _crudform_width = 400;
    var _crudform_height = 300;
    parent.OpenCRUDForm(window, '上下架: cno=' + _cno, _url, _crudform_width, _crudform_height, _winlevel);
}

//開啟 "加價購" 的表單 ***************************************************************
function openform_buyother(_cno) {
    var _url = '/mng/product/list/Form_CRUD_BuyOther.aspx?cno=' + _cno;
    var _crudform_width = 500;
    var _crudform_height = 300;
    parent.OpenCRUDForm(window, '加價購: cno=' + _cno, _url, _crudform_width, _crudform_height, _winlevel);
}

//開啟 "原廠活動" 的表單 *************************************************************
function openform_cooperationevent(_cno) {
    var _url = '/mng/product/list/Form_CRUD_CooperationEvent.aspx?cno=' + _cno;
    var _crudform_width = 600;
    var _crudform_height = 350;
    parent.OpenCRUDForm(window, '原廠活動: cno=' + _cno, _url, _crudform_width, _crudform_height, _winlevel);
}

//開啟 "Coupon設定" 的表單 ***********************************************************
function openform_coupon(_cno) {
    var _url = '/mng/product/list/Form_CRUD_Coupon.aspx?cno=' + _cno;
    var _crudform_width = 500;
    var _crudform_height = 280;
    parent.OpenCRUDForm(window, 'Coupon設定: cno=' + _cno, _url, _crudform_width, _crudform_height, _winlevel);
}

//開啟 "異動記錄" 的表單 *************************************************************
function openform_list_mod_rec(_cno) {
    var _url = '/mng/product/Unitech/list_mod_rec/Form_List_Mod_Rec.aspx?cno=' + _cno;
    var _crudform_width = 700;
    var _crudform_height = 450;
    parent.OpenCRUDForm(window, '異動記錄: cno=' + _cno, _url, _crudform_width, _crudform_height, _winlevel);
}

//黃區活動列表頁
function openform_yellow_area(_cno) {
    var _url = '/mng/product/Yellow_Area/default.aspx?cno=' + _cno;
    var _crudform_width = 950;
    var _crudform_height = 500;
    parent.OpenCRUDForm(window, '黃區活動列表: cno=' + _cno, _url, _crudform_width, _crudform_height, _winlevel);
}

//開啟 "配送設定" 的表單 *************************************************************
function Prod_ShipSetup(_cno) {
    var _url = '/mng/product/list/list_ShipSetup/Form.aspx?cno=' + _cno;
    var _crudform_width = 400;
    var _crudform_height = 150;
    parent.OpenCRUDForm(window, '配送設定: cno=' + _cno, _url, _crudform_width, _crudform_height, _winlevel);
}


//2015-04-20 小葉 購買設定 (原 上下架 + 配送設定 + 價格/可購量)
//開啟 "購買設定" 的表單 *************************************************************
function BuyTheSet(_cno) {
    var _url = '/mng/product/list/BuyTheSet/Form.aspx?cno=' + _cno;
    var _crudform_width = 700;
    var _crudform_height = 360;
    parent.OpenCRUDForm(window, '購買設定: cno=' + _cno, _url, _crudform_width, _crudform_height, _winlevel);
}

//2015-12-17 小葉 
//開啟 "文章設定" 的表單 *************************************************************
function openform_articleSet(_cno) {
    var _url = '/mng/product/list/articleSet/Form.aspx?cno=' + _cno;
    var _crudform_width = 700;
    var _crudform_height = 360;
    parent.OpenCRUDForm(window, '文章設定: cno=' + _cno, _url, 400, 200, _winlevel);
}

//2016-01-22 譓茹 
//開啟 "特殊欄位" 的表單 *************************************************************
function openform_specialNeeded(_cno) {
    var _url = '/mng/product/list/specialNeeded/Form.aspx?cno=' + _cno;
    var _crudform_width = 1000;
    var _crudform_height = 360;
    parent.OpenCRUDForm(window, '特殊欄位: cno=' + _cno, _url, _crudform_width, _crudform_height, _winlevel);
}

