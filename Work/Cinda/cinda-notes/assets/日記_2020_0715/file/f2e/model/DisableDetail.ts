import DisableDetailData from './DisableDetailData';

export default class DisableDetail {
    UserEmpNo: string;
    User: string;
    CloseDate: Date;
    Type: number;
    Data: DisableDetailData[];
    DisabledDate: Date; // 停用及設備繳回日期
    Id: number;
}
