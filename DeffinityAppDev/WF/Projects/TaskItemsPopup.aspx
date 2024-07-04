<%@ Page Language="C#" MasterPageFile="~/DeffinityPopUp.master" AutoEventWireup="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        lblProject.Text = sessionKeys.Prefix + sessionKeys.Project.ToString();        
    }
    protected string getUrl()
    {
        string s = "";
        if (Request.QueryString["ragstatus"].ToString()== "red")
        {
            s = "<img src='images/indcate_red.png' alt='RED'/>";
        }
        else if (Request.QueryString["ragstatus"].ToString() == "amber")
        {
            s = "<img src='images/indcate_yellow.png' alt='AMBER'/>";
        }
        else if (Request.QueryString["ragstatus"].ToString() == "green")
        {
            s = "<img src='images/indcate_green.png' alt='GREEN'/>";
        }
        return s;
    }
</script>
<div class="clr"></div>
    <div class="pro_ref">
		Project Reference: <b><asp:Label runat="server" ID="lblProject"></asp:Label></b>
		</div>
		<div class="pro_madatory" style="padding-top:10px"></div>
		<div class="clr"></div>	
		
     <div class="sec_header">Task Items for RAG status : <%=getUrl()%></div>
     <div>

		</div>
    <asp:GridView ID="GridView1" runat="server" DataSourceID="SqlDataSource1" Width="732px">
    <Columns>
    <asp:BoundField HeaderText="Task" DataField="ItemDescription" >
        <ItemStyle Width="300px" />
    </asp:BoundField>
        <asp:TemplateField HeaderText="Start Date">
            <EditItemTemplate>
                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("ProjectStartDate") %>'></asp:TextBox>
            </EditItemTemplate>
            <ItemTemplate>
            <%# DataBinder.Eval(Container.DataItem, "ProjectStartDate", "{0:d}")%>                
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" Width="135px" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="End Date">
           
            <ItemTemplate>
            <%# DataBinder.Eval(Container.DataItem, "ProjectEndDate", "{0:d}")%>                
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Center" Width="135px" />
        </asp:TemplateField>
    <asp:BoundField HeaderText="Status" DataField="ItemStatus" >
        <ItemStyle HorizontalAlign="Center" Width="130px" />
    </asp:BoundField>
    </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DBstring %>" SelectCommand="DN_SelectProjectTaskItems" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="0" Name="Project" QueryStringField="project" Type="Int32" />
            <asp:QueryStringParameter DefaultValue="null" Name="RAGStatus" QueryStringField="ragstatus" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
       
</asp:Content>


<%--
<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">



<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Task items</title>
    <link href="css/defficss.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
   

    <div>
    
    </div>
    </form>
</body>
</html>--%>
