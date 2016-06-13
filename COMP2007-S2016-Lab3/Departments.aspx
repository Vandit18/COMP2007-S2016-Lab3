<%@ Page Title="Departments" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Departments.aspx.cs" Inherits="COMP2007_S2016_Lab3.Departments" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <div class="container">
        <div class="row">
            <div class="col-md-offset-2 col-md-8">
                <h1>Departments List</h1>
                <a href="DepartmentDetail.aspx" class="btn btn-success btn-sm"><i class="fa fa-plus"></i>Add Department</a>

                <div>
                    <label for="PageSizeDropDownList">Records per Page: </label>
                    <asp:DropDownList ID="PageSizeDropDownList" runat="server"
                        AutoPostBack="true" CssClass="btn btn-default bt-sm dropdown-toggle"
                        OnSelectedIndexChanged="PageSizeDropDownList_SelectedIndexChanged">
                        <asp:ListItem Text="3" Value="3" />
                        <asp:ListItem Text="5" Value="5" />
                        <asp:ListItem Text="10" Value="10" />
                        <asp:ListItem Text="All" Value="10000" />
                    </asp:DropDownList>
                </div>
                
                <asp:GridView runat="server" CssClass="table table-bordered table-striped table-hover"
                    ID="DeptGridView" AutoGenerateColumns="false" DataKeyNames="DepartmentID"
                     OnRowDeleting="DeptGridView_RowDeleting" AllowPaging="true" PageSize="3"
                     OnPageIndexChanging="DeptGridView_PageIndexChanging" AllowSorting="true"
                     OnSorting="DeptGridView_Sorting" OnRowDataBound="DeptGridView_RowDataBound"
                    PagerStyle-CssClass="pagination-ys">
                    <Columns>
                        <asp:BoundField DataField="DepartmentID" HeaderText="Dept ID" Visible="true" SortExpression="DepartmentID" />
                        <asp:BoundField DataField="Name" HeaderText="Dept Name" Visible="true" SortExpression="Name" />
                        <asp:BoundField DataField="Budget" HeaderText="Budget" Visible="true" SortExpression="Budget" />
                        
                        <asp:HyperLinkField HeaderText="Edit" Text="<i class='fa fa-pencil-square-o fa-lg'></i> Edit" 
                            NavigateUrl="~/DepartmentDetail.aspx.cs" ControlStyle-CssClass="btn btn-primary btn-sm" runat="server"
                            DataNavigateUrlFields="DepartmentID" DataNavigateUrlFormatString="DepartmentDetail.aspx?DepartmentID={0}" />
                        <asp:CommandField  HeaderText="Delete" DeleteText="<i class='fa fa-trash-o fa-lg'></i> Delete"
                            ShowDeleteButton="true" ButtonType="Link" ControlStyle-CssClass="btn btn-danger btn-sm" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
