<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GridTest.aspx.cs" Inherits="DeffinityAppDev.WF.test.GridTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script type = "text/javascript">
        function GetSelectedRow(lnk) {
            var row = lnk.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var customerId = row.cells[0].innerHTML;
            var city = row.cells[1].getElementsByTagName("input")[0].value;
            //alert("RowIndex: " + rowIndex + " CustomerId: " + customerId + " City:" + city);
            alert(lnk.value);
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <asp:TextBox ID="txt1" runat="server" ClientIDMode="Static"></asp:TextBox>
        <asp:TextBox ID="txt2" runat="server" ClientIDMode="Static"></asp:TextBox>
  <asp:GridView ID="gvCustomers" runat="server" AutoGenerateColumns = "false" AllowPaging = "false" DataSourceID="SqlDataSource1">
        <Columns>
        <asp:BoundField DataField = "ID" HeaderText = "ID" />
        <asp:TemplateField HeaderText = "Name">
            <ItemTemplate>
                <asp:TextBox ID="txtCountry" runat="server" Text = '<%# Eval("Name") %>'></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:LinkButton ID="lnkSelect" runat="server" Text="Select" CommandName = "Select" OnClientClick = "return GetSelectedRow(this)" />
               
            </ItemTemplate>
        </asp:TemplateField>
             <asp:TemplateField>
            <ItemTemplate>
                <input type="checkbox" id='chk' value='<%# Eval("ID") %>' onchange="return GetSelectedRow(this)" />
            </ItemTemplate>
        </asp:TemplateField>
        </Columns>
    </asp:GridView>
          
         <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns = "false" AllowPaging = "false" DataSourceID="SqlDataSource2" >
        <Columns>
        <asp:BoundField DataField = "ID" HeaderText = "ID" />
        <asp:TemplateField HeaderText = "Name">
            <ItemTemplate>
                <asp:TextBox ID="txtCity" runat="server" Text = '<%# Eval("Name") %>'></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:LinkButton ID="lnkSelect" runat="server" Text="Select" CommandName = "Select" OnClientClick = "return GetSelectedRow(this)" />
            </ItemTemplate>
        </asp:TemplateField>
             <asp:TemplateField>
            <ItemTemplate>
                <input type="checkbox" id='chk' value='<%# Eval("ID") %>' onchange="return GetSelectedRow(this)" />
            </ItemTemplate>
        </asp:TemplateField>
        </Columns>
    </asp:GridView>
       
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>" SelectCommand="SELECT TOP (10) ID, City AS Name FROM City"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>" SelectCommand="SELECT TOP (10) ID, Country AS Name FROM Country"></asp:SqlDataSource>
       
    </div>
    </form>
</body>
</html>
