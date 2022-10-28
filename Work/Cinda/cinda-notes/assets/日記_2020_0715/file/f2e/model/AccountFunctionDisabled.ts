export default class AccountFunctionDisabled {
    Department: string;
    UserEmpNo: string;
    User: string;
    CloseDate: Date;
    Status: number; // FormStatus: 文件處理狀態
    // 0 已派工
    // 1 待主管送件
    // 2 中止
    // 3 結案
    F0RM_TYPE: number; // 離職帳號或權限停用單狀態
    Item: string; // FormType
    // 0 沒有選擇
    // 1 權限提前停用，但AD/Note/Novell帳號及設備不提前及繳回
    // 2 AD / Note / Novell帳號提前停用及設備提前繳回
    // 3 帳號與權限皆不提前停用
    SignFormId: number;
}
