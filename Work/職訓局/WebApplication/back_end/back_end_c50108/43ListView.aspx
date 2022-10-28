<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="43ListView.aspx.cs" Inherits="ASPnet._43ListView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="MyStyle.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div >
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                ConnectionString="<%$ ConnectionStrings:教務系統ConnectionString %>" 
                SelectCommand="SELECT * FROM [學生]"
                 UpdateCommand="update [學生] set 姓名=@姓名, 電話=@電話 where 學號=@學號"
                  DeleteCommand="delete from [學生] where 學號=@學號"
                 InsertCommand="insert into  [學生] values(@學號,@姓名,@性別,@電話,@生日)"
                ></asp:SqlDataSource>
            <asp:ListView ID="ListView1" runat="server" DataSourceID="SqlDataSource1" DataKeyNames="學號" InsertItemPosition="FirstItem">
                <LayoutTemplate>
                     <div class="container">
                         <div class="alert-success">排序功能</div>
                         <div>
                             <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-danger" CommandName="Sort" CommandArgument="學號">學號</asp:LinkButton>
                             <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-danger" CommandName="Sort" CommandArgument="姓名">姓名</asp:LinkButton>
                         </div>
                         <hr />
                         <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#InsForm">
                             新增
                         </button>
                         
                         <hr />
                         <div class="row">
                             <asp:PlaceHolder ID="itemPlaceHolder" runat="server"></asp:PlaceHolder>
                         </div>
                         <nav class="nav justify-content-center" id="nav">
                             <asp:DataPager ID="DataPager1" runat="server" PageSize="3" class="pagination">
                                 <Fields>
                                     <asp:NextPreviousPagerField ShowNextPageButton="false" PreviousPageText="&laquo;" ShowFirstPageButton="true" />
                                     <asp:NumericPagerField />
                                     <asp:NextPreviousPagerField ShowPreviousPageButton="false" NextPageText="&raquo;" ShowLastPageButton="true" LastPageText="&ge;" />
                                 </Fields>
                             </asp:DataPager>
                         </nav>
                        
                     </div>
                </LayoutTemplate>
            
                <ItemTemplate>
                    <div class="col-md-4 col-sm-6 mt-2 mb-2">
                        <div class="card bg-light MyFont">
                            <div class="card-header">
                                <h3><strong><%# Eval("姓名") %></strong></h3>
                            </div>

                            <div class="card-body" style="height:200px;">
                                <p class="text-primary"><%# Eval("生日","{0:d}") %></p>
                                <p class="text-danger"><%# Eval("電話") %></p>
                                <p class="alert-danger"><%# Eval("性別") %></p>
                            </div>
                            <footer class="card-footer">
                                <input id="Button1" type="button" value="點台" class="btn btn-primary" />
                                <asp:Button ID="Button2" runat="server" Text="修改" CommandName="Edit" class="btn btn-secondary" />
                               
                            </footer>
                        </div>
                    </div>

                   
                </ItemTemplate>
                <EditItemTemplate>
                  <div class="col-md-4 col-sm-6 mt-2 mb-2">
                        <div class="card bg-danger MyFont">
                            <div class="card-header">
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("姓名") %>'></asp:TextBox>
                         
                            </div>

                            <div class="card-body" style="height:200px;">
                                <p class="text-primary"><%# Eval("生日","{0:d}") %></p>
                                <p> <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("電話") %>'></asp:TextBox></p>
                                
                                <p class="alert-danger"><%# Eval("性別") %></p>
                            </div>
                            <footer class="card-footer">
                                <asp:Button ID="Button3" runat="server" CommandName="Update" Text="儲存" class="btn btn-success" />
                                 <asp:Button ID="Button4" runat="server" CommandName="Delete" Text="刪除" class="btn btn-warning" OnClientClick="" />
                            </footer>
                        </div>
                    </div>
              </EditItemTemplate>

                <InsertItemTemplate>
                    <div class="col-12 modal fade" id="InsForm">
                        <div class="modal-dialog bg-light">
                            <div class="modal-header">
                                <h5 class="modal-title">資料新增</h5>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span>
                                </button>
                            </div>
                            <div class="modal-body">
                                <div class="card bg-white MyFont">
                                    <div class="card-header">
                                        姓名
                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("姓名") %>' CssClass="form-control"></asp:TextBox>

                                    </div>

                                    <div class="card-body" style="height: 350px;">
                                        <p class="">
                                            學號
                                    <asp:TextBox ID="TextBox7" runat="server" Text=' <%# Bind("學號") %>' CssClass="form-control"></asp:TextBox>
                                        </p>
                                        <p class="">
                                            生日
                                    <asp:TextBox ID="TextBox4" runat="server" Text=' <%# Bind("生日") %>' CssClass="form-control"></asp:TextBox>
                                        </p>
                                        <p class="">
                                            電話
                                    <asp:TextBox ID="TextBox5" runat="server" Text=' <%# Bind("電話") %>' CssClass="form-control"></asp:TextBox>
                                        </p>
                                        <p class="">
                                            性別
                                    <asp:TextBox ID="TextBox6" runat="server" Text=' <%# Bind("性別") %>' CssClass="form-control"></asp:TextBox>
                                        </p>
                                    </div>
                                    <footer class="card-footer">

                                        <asp:Button ID="Button2" runat="server" Text="儲存" CommandName="Insert" class="btn btn-secondary" />
                                    </footer>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                                <button type="button" class="btn btn-primary">Save changes</button>
                            </div>
                        </div>
                    </div>

                </InsertItemTemplate>

            </asp:ListView>
            
        </div>
    </form>
    
    <script src="Scripts/jquery-3.0.0.min.js"></script>
    <script src="Scripts/bootstrap.min.js"></script>
    <script>
         
        $('#nav>span>a').addClass('page-link');
        $('#nav>span>span').addClass('page-link Page');

        //$('#Button10').click(function () {
        //    $('#InsForm').removeClass('d-none');
        //});


    </script>
</body>
</html>
