<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="15WebForm_Validation.aspx.cs" Inherits="ASPnet._15WebForm_Validation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>
            #tbMember {
                width: 450px;
                height: 500px;
                margin: auto;
                border:3px double;
            }

            #tbMember table {
                width: 100%;
            }

            #tbMember>tbody>tr>td:first-child {
                text-align: right;
            }

            #tbMember>tbody>tr:last-child > td {
                text-align: center;
            }
            #tbMember table>tbody>tr>td {
                text-align: center;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="Button1">
        <div>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" Font-Size="10" ForeColor="Red" DisplayMode="BulletList" ShowMessageBox="true" />

            <table id="tbMember">
                <caption>會員資料</caption>
                <tr>
                    <td>帳號：</td>
                    <td>
                        <asp:TextBox ID="txtAccount" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtAccount" Display="Dynamic" runat="server" ErrorMessage="姓名為必填欄位" Text="(必填)" ForeColor="Red" Font-Size="10pt"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtAccount" ValidationExpression="[ABC][A-Za-z0-9]{4}" runat="server" ErrorMessage="(格式有誤)" ForeColor="Red" Font-Size="10pt"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>密碼：</td>
                    <td>
                        <asp:TextBox ID="txtPwd" runat="server" TextMode="Password" placeholder="8-12碼" MaxLength="12"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" ControlToValidate="txtPwd" runat="server" ErrorMessage="密碼為必填欄位" Text="(必填)" ForeColor="Red" Font-Size="10pt"></asp:RequiredFieldValidator>
                         <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtPwd" ValidationExpression="\S{8,12}" runat="server" ErrorMessage="(密碼不可含有空白)" ForeColor="Red" Font-Size="10pt"></asp:RegularExpressionValidator>
                     </td>
                </tr>
                <tr>
                    <td>密碼確認：</td>
                    <td>
                        <asp:TextBox ID="txtPwd2" runat="server" TextMode="Password" placeholder="請再輸入一次密碼"></asp:TextBox>
                      <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtPwd2" runat="server" ErrorMessage="(必填)" ForeColor="Red" Font-Size="10pt"></asp:RequiredFieldValidator>--%>
                          <asp:CompareValidator ID="CompareValidator3" runat="server" Operator="Equal" ControlToCompare="txtPwd2"  ControlToValidate="txtPwd" ErrorMessage="(兩次密碼輸入不相同)" ForeColor="Red" Font-Size="10pt"></asp:CompareValidator>
                    </td>
                </tr>
                 <tr>
                    <td>身分證字號：</td>
                    <td>
                         <asp:TextBox ID="txtID" runat="server" placeholder="A123456789" MaxLength="10"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Display="None" ControlToValidate="txtID" runat="server" ErrorMessage="身分證為必填欄位"  ForeColor="Red" Font-Size="10pt"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Display="Dynamic" ControlToValidate="txtID" ValidationExpression="[A-Za-z](1|2)\d{8}" runat="server" ErrorMessage="(格式有誤)" ForeColor="Red" Font-Size="10pt"></asp:RegularExpressionValidator>
                        <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="(不合法)" ForeColor="Red" Font-Size="10pt" OnServerValidate="CustomValidator1_ServerValidate"></asp:CustomValidator>
                        
                    </td>
                </tr>
                <tr>
                    <td>姓名：</td>
                    <td>
                         <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtName" runat="server" ErrorMessage="(必填)" ForeColor="Red" Font-Size="10pt"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                 <tr>
                    <td>生日：</td>
                    <td>
                         <asp:TextBox ID="txtBirthday" runat="server" placeholder="1990-01-12"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtBirthday" Display="Dynamic" runat="server" ErrorMessage="(必填)" ForeColor="Red" Font-Size="10pt"></asp:RequiredFieldValidator>
                         <asp:CompareValidator ID="CompareValidator1" Display="Dynamic" runat="server" Operator="DataTypeCheck" Type="Date" ControlToValidate="txtBirthday" ErrorMessage="(格式錯誤)" ForeColor="Red" Font-Size="10pt"></asp:CompareValidator>
                         <asp:RangeValidator ID="ranForBirthday" runat="server" Type="Date" MinimumValue="1912/1/1" ControlToValidate="txtBirthday" ErrorMessage="(超出範圍)" ForeColor="Red" Font-Size="10pt"></asp:RangeValidator>
                    </td>
                </tr>
                <tr>
                    <td>E-mail：</td>
                    <td>
                         <asp:TextBox ID="txtEmail" runat="server" placeholder="abc@abc.com"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="txtEmail" ValidationExpression="([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)" runat="server" ErrorMessage="(格式有誤)" ForeColor="Red" Font-Size="10pt"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td>性別：</td>
                    <td>
                        <asp:RadioButtonList ID="rblGender" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="男" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="女"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>學歷：</td>
                    <td>
                        <asp:DropDownList ID="ddlEduLevel" runat="server">
                            <asp:ListItem Text="請選擇"></asp:ListItem>
                            <asp:ListItem Text="國小"></asp:ListItem>
                            <asp:ListItem Text="國中"></asp:ListItem>
                            <asp:ListItem Text="高中"></asp:ListItem>
                            <asp:ListItem Text="大學"></asp:ListItem>
                            <asp:ListItem Text="研究所以上"></asp:ListItem>
                        </asp:DropDownList>
                         <asp:CompareValidator ID="CompareValidator2" runat="server" Operator="NotEqual" ValueToCompare="請選擇"  ControlToValidate="ddlEduLevel" ErrorMessage="(請選擇)" ForeColor="Red" Font-Size="10pt"></asp:CompareValidator>
                    </td>
                  
                </tr>
                <tr>
                    <td>興趣：</td>
                    <td>
                    
                        <table>
                            <tr>
                                <td rowspan="4">
                                    <asp:ListBox ID="ltbInterest" runat="server" Width="100" Height="150">
                                        <asp:ListItem Text="爬山"></asp:ListItem>
                                        <asp:ListItem Text="踏青"></asp:ListItem>
                                        <asp:ListItem Text="看雲"></asp:ListItem>
                                        <asp:ListItem Text="健行"></asp:ListItem>
                                        <asp:ListItem Text="聽音樂"></asp:ListItem>
                                        <asp:ListItem Text="上網"></asp:ListItem>
                                        <asp:ListItem Text="遊泳"></asp:ListItem>
                                        <asp:ListItem Text="賺錢"></asp:ListItem>
                                        <asp:ListItem Text="唱歌"></asp:ListItem>
                                    </asp:ListBox>
                                </td>
                                <td>
                                     <asp:Button ID="btnAll" runat="server" Text=">>" OnClick="btnAll_Click" CausesValidation="false" />
                       
                                </td>
                                <td rowspan="4">
                                     <asp:ListBox ID="ltbInterest2" runat="server" Width="100" Height="150">

                                     </asp:ListBox>
                                    <asp:CustomValidator ID="CustomValidator2" runat="server" ErrorMessage="(請至少選3種)" ForeColor="Red" Font-Size="10pt" OnServerValidate="CustomValidator2_ServerValidate"></asp:CustomValidator>
                        
                                </td>
                            </tr>
                            <tr>
                                <td>
                                      <asp:Button ID="btnYes" runat="server" Text=">" OnClick="btnYes_Click" CausesValidation="false" />
                     
                                </td>
                              
                            </tr>
                            <tr>
                                <td>
                                       <asp:Button ID="btnNo" runat="server" Text="<" OnClick="btnNo_Click" CausesValidation="false" />
                                </td>
                              
                            </tr>
                            <tr>
                                <td >
                                     <asp:Button ID="btnCancel" runat="server" Text="<<" OnClick="btnCancel_Click" CausesValidation="false" />
                                    
                                </td>
                              
                            </tr>
                        </table>

                    </td>
                </tr>
                 <tr>
                    <td colspan="2">
                        <asp:Button ID="Button1" runat="server" Text="確定送出" OnClick="Button1_Click" />
                        <input id="Reset1" type="reset" value="重設" />
                    </td>                  
                </tr>
                
            </table>

        </div>

    </form>
</body>
</html>
