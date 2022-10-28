<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Index2.aspx.cs" Inherits="MVC1.Index2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>產品列表</h2>
    <p>
        <a href="Create.aspx">新增產品</a>
    </p>
    <asp:GridView ID="GridView1" runat="server" CssClass="table" AutoGenerateColumns="False" GridLines="None" OnRowCommand="GridView1_RowCommand">
        <Columns>
            <asp:BoundField DataField="fId" HeaderText="產品編號" />
            <asp:BoundField DataField="fName" HeaderText="產品名稱" />
            <asp:BoundField DataField="fPrice" HeaderText="單價" DataFormatString="{0:C}" />
            <asp:ImageField DataImageUrlField="fImg" DataImageUrlFormatString="images/{0}" ControlStyle-Width="150px" HeaderText="圖示">
            <ControlStyle Width="150px"></ControlStyle>
            </asp:ImageField>
        
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" CommandArgument='<%# Eval("fId") %>'>編輯</asp:LinkButton>
                    |
                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="KillMe" CommandArgument='<%# Eval("fId") %>' OnClientClick="return confirm('您確定要刪除嗎??')">刪除</asp:LinkButton>

                </ItemTemplate>

            </asp:TemplateField>
        
        </Columns>

    </asp:GridView>

</asp:Content>
