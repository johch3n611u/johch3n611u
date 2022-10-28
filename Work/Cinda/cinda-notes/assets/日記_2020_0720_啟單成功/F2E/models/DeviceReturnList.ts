export default class DeviceReturnList {

    // 前端原欄位名稱

    // Department: string;

    // EmpName: string;

    // EmpNo: string;

    // Xfort: string;

    // NB: string

    // Xfort_AssetId: string;

    // NB_AssetId: string;

    // 後端欄位名稱

    Id: string; // Id: string 帳號關閉及設備繳回單 ID

    ReturnDate: Date | string | null; // ReturnDate: Date; // 設備繳回日期 RETURN_DATE

    DeviceReturn: boolean; // DeviceReturn: boolean; // 設備不繳回 DEVICE_RETURN

    DeviceReturnDesc: string;

    XfortReturn: boolean; // Xfort: boolean; // XFORT_RETURN

    XfortDesc: string;

    NbBringout: boolean;  // NB: boolean; // NB_BRINGOUT

    NbBringoutDesc: string;

    SignFormId: number | null;

    AccountId: number | null;

    // Device_AssetId: string // DEVICE_RETURN_DESC

    XfortAssetNo: number;  // NB_AssetId: string;

    NbAssetNo: number; // Xfort_AssetId: string;

    // FK JOIN

    ResignName: string;   // EmpName: string; // 姓名

    ResignEmpNo: string;   // EmpNo: string; // 工號

    ResignDepartment: string; // Department: string; // 部門

    //

    XfortControl: string; // Xfort 是否有Xfort管控?

    NbCustody: string; //  NB 是否有NB攜出保管證?

    SignFormNo: string; // 簽核表單單號
}
