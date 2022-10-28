<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="36FormView.aspx.cs" Inherits="ASPnet._36FormView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>
        #container{
            border:2px solid;
            width:1200px;
            margin:auto;
            
        }
         #left{
           
            width:200px;
            float:left;
        }
          #right{
            width:1000px;
            float:right;
        }
          #bottom{
              clear:both;
          }

  
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
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div id="container">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div id="left">
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                            ConnectionString="<%$ ConnectionStrings:MySystemConnectionString1 %>"
                            SelectCommand="SELECT * FROM [Members]"></asp:SqlDataSource>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="Account" DataSourceID="SqlDataSource2">
                            <Columns>
                                <asp:BoundField DataField="Account" HeaderText="Account" ReadOnly="True" SortExpression="Account" />

                                <asp:CommandField ShowSelectButton="True" />

                            </Columns>
                        </asp:GridView>
                    </div>

                    <div id="right">
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                            ConnectionString="<%$ ConnectionStrings:MySystemConnectionString1 %>"
                            SelectCommand="SELECT  Members.*,iif(Members.Gender=1,'男','女') as sex,Edu.EduLevel as EduName FROM [Members] inner join [Edu] on Edu.EduLevel_Code=Members.EduLevel where account=@account">
                            <SelectParameters>
                                <asp:ControlParameter Name="account" ControlID="GridView1" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>

                        <asp:FormView ID="FormView1" runat="server" DataKeyNames="Account" DataSourceID="SqlDataSource1" OnModeChanged="FormView1_ModeChanged" OnDataBound="FormView1_DataBound">
                            <EditItemTemplate>
                                Account:
                    <asp:Label ID="AccountLabel1" runat="server" Text='<%# Eval("Account") %>' />
                                <br />
                                Pswd:
                    <asp:TextBox ID="PswdTextBox" runat="server" Text='<%# Bind("Pswd") %>' />
                                <br />
                                Name:
                    <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
                                <br />
                                Birthday:
                    <asp:TextBox ID="BirthdayTextBox" runat="server" Text='<%# Bind("Birthday") %>' />
                                <br />
                                Email:
                    <asp:TextBox ID="EmailTextBox" runat="server" Text='<%# Bind("Email") %>' />
                                <br />
                                Gender:
                    <asp:CheckBox ID="GenderCheckBox" runat="server" Checked='<%# Bind("Gender") %>' />
                                <br />
                                EduLevel:
                    <asp:TextBox ID="EduLevelTextBox" runat="server" Text='<%# Bind("EduLevel") %>' />
                                <br />
                                Notes:
                    <asp:TextBox ID="NotesTextBox" runat="server" Text='<%# Bind("Notes") %>' />
                                <br />
                                Photo:
                    <asp:TextBox ID="PhotoTextBox" runat="server" Text='<%# Bind("Photo") %>' />
                                <br />
                                IsAuth:
                    <asp:CheckBox ID="IsAuthCheckBox" runat="server" Checked='<%# Bind("IsAuth") %>' />
                                <br />
                                <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="更新" />
                                &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="取消" />
                            </EditItemTemplate>
                            <InsertItemTemplate>
                                <table id="tbMember">
                                    <caption>註冊會員</caption>
                                    <tr>
                                        <td>帳號：</td>
                                        <td>
                                         
                                                    <asp:TextBox ID="txtAccount" runat="server" placeholder="5-10碼" Text='<%# Bind("Account") %>'></asp:TextBox><asp:Button ID="Button2" runat="server" Text="檢查帳號可用性" ValidationGroup="abc123" />
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtAccount" Display="Dynamic" runat="server" ErrorMessage="姓名為必填欄位" Text="(必填)" ForeColor="Red" Font-Size="10pt"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtAccount" Display="Dynamic" ValidationExpression="[A-Za-z][A-Za-z0-9]{4,9}" runat="server" ErrorMessage="(格式有誤)" ForeColor="Red" Font-Size="10pt"></asp:RegularExpressionValidator>
                                                    <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="(帳號重複)" ForeColor="Red" Font-Size="10pt" ValidationGroup="abc123" OnServerValidate="CustomValidator1_ServerValidate"></asp:CustomValidator>
                                                
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>密碼：</td>
                                        <td>
                                            <asp:TextBox ID="txtPwd" runat="server" TextMode="Password" placeholder="8-12碼" MaxLength="12" Text='<%# Bind("Pwd") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Display="Dynamic" ControlToValidate="txtPwd" runat="server" ErrorMessage="密碼為必填欄位" Text="(必填)" ForeColor="Red" Font-Size="10pt"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" ControlToValidate="txtPwd" ValidationExpression="\S{8,12}" runat="server" ErrorMessage="(密碼不可含有空白)" ForeColor="Red" Font-Size="10pt"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>密碼確認：</td>
                                        <td>
                                            <asp:TextBox ID="txtPwd2" runat="server" TextMode="Password" placeholder="請再輸入一次密碼"></asp:TextBox>
                                            <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtPwd2" runat="server" ErrorMessage="(必填)" ForeColor="Red" Font-Size="10pt"></asp:RequiredFieldValidator>--%>
                                            <asp:CompareValidator ID="CompareValidator3" runat="server" Operator="Equal" ControlToCompare="txtPwd2" ControlToValidate="txtPwd" ErrorMessage="(兩次密碼輸入不相同)" ForeColor="Red" Font-Size="10pt"></asp:CompareValidator>
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>姓名：</td>
                                        <td>
                                            <asp:TextBox ID="txtName" runat="server" Text='<%# Bind("Name") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtName" runat="server" ErrorMessage="(必填)" ForeColor="Red" Font-Size="10pt"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>生日：</td>
                                        <td>
                                            <asp:TextBox ID="txtBirthday" runat="server" placeholder="1990-01-12" Text='<%# Bind("Birthday") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtBirthday" Display="Dynamic" runat="server" ErrorMessage="(必填)" ForeColor="Red" Font-Size="10pt"></asp:RequiredFieldValidator>
                                            <asp:CompareValidator ID="CompareValidator1" Display="Dynamic" runat="server" Operator="DataTypeCheck" Type="Date" ControlToValidate="txtBirthday" ErrorMessage="(格式錯誤)" ForeColor="Red" Font-Size="10pt"></asp:CompareValidator>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>E-mail：</td>
                                        <td>
                                            <asp:TextBox ID="txtEmail" runat="server" placeholder="abc@abc.com" Text='<%# Bind("Email") %>'></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="txtEmail" ValidationExpression="([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)" runat="server" ErrorMessage="(格式有誤)" ForeColor="Red" Font-Size="10pt"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>性別：</td>
                                        <td>
                                            <asp:RadioButtonList ID="rblGender" runat="server" RepeatDirection="Horizontal" Width="120">
                                                <asp:ListItem Text="男" Value="1" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="女" Value="0"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>學歷：</td>
                                        <td>
                                            <asp:DropDownList ID="ddlEduLevel" runat="server">
                                                <asp:ListItem Text="請選擇"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:CompareValidator ID="CompareValidator2" runat="server" Operator="NotEqual" ValueToCompare="請選擇" ControlToValidate="ddlEduLevel" ErrorMessage="(請選擇)" ForeColor="Red" Font-Size="10pt"></asp:CompareValidator>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>照片：</td>
                                        <td>
                                            <asp:FileUpload ID="fulPhoto" runat="server" /><asp:Label ID="lblPhoto" Font-Size="10pt" ForeColor="Red" runat="server"></asp:Label>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td>備註：</td>
                                        <td>
                                            <asp:TextBox ID="txtNote" TextMode="MultiLine" runat="server" Width="200" Height="150" Text='<%# Bind("Note") %>'></asp:TextBox>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Button ID="Button1" runat="server" Text="加入會員" CommandName="Insert" />
                                            <asp:Button ID="Button3" runat="server" Text="取消" CommandName="Cancel" CausesValidation="false" />
                                        </td>
                                    </tr>

                                </table>

                            </InsertItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Edit">編輯</asp:LinkButton>
                                <asp:LinkButton ID="LinkButton2" runat="server" CommandName="New">新增</asp:LinkButton>
                                <hr />

                                <table border="1">
                                    <tr>
                                        <td>帳號：</td>
                                        <td><%# Eval("Account") %>
                                            <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("IsAuth") %>' Enabled="false" />
                                        </td>
                                        <td>密碼：</td>
                                        <td>＊＊＊＊＊＊＊</td>
                                        <td rowspan="6">
                                            <img src='<%# "36GetMemberPhoto.ashx?account="+ Eval("Account") %>' />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>姓名：</td>
                                        <td><%# Eval("Name") %></td>
                                        <td>生日：</td>
                                        <td><%# Eval("Birthday","{0:d}") %></td>
                                    </tr>
                                    <tr>
                                        <td>性別：</td>
                                        <td><%# Eval("sex") %></td>
                                        <td>教育程度：</td>
                                        <td><%# Eval("EduName") %></td>
                                    </tr>
                                    <tr>
                                        <td>E-mail：</td>
                                        <td colspan="3"><%# Eval("Email") %></td>

                                    </tr>
                                    <tr>
                                        <td>備住：</td>
                                        <td colspan="3"><%# Eval("Notes") %></td>

                                    </tr>
                                </table>

                            </ItemTemplate>
                        </asp:FormView>
                    </div>
                    <div id="bottom">
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
