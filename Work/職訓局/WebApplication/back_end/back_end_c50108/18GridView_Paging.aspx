<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="18GridView_Paging.aspx.cs" Inherits="ASPnet._18GridView_Paging" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>
        #GridView1 tr:last-child table a{
            text-decoration:none;

        }
        #GridView1 tr:last-child table a:hover{
            font-size:18pt;

        }
          #GridView1 tr:last-child table span{
            font-size:larger;
            color:red;
        }
      #GridView1 tr:last-child table a[href*="First"]{
            font-family:Webdings;
        }
       #GridView1 tr:last-child table a[href*="Last"]{
            font-family:Webdings;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:MySystemConnectionString1 %>" 
                SelectCommand="SELECT * FROM [Members]"></asp:SqlDataSource>

            <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" AllowPaging="true" PageSize="1">

                <PagerSettings Mode="NumericFirstLast" PageButtonCount="5"  NextPageText="8" PreviousPageText="7"
                     FirstPageText="9" LastPageText=":"
                    />
                <PagerStyle />
            </asp:GridView>
        </div>
    </form>
</body>
</html>
