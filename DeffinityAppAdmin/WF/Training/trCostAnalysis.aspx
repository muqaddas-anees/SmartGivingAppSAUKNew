<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="Training_trCostAnalysis" Codebehind="trCostAnalysis.aspx.cs" %>

<%@ Register src="controls/TrainingTabs.ascx" tagname="TrainingTabs" tagprefix="uc1" %>
<%@ Register Src="controls/dropdownView.ascx" TagName="DropDownList" TagPrefix="uc2" %>

<%@ Register assembly="Infragistics2.WebUI.UltraWebChart.v7.2, Version=7.2.20072.61, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.UltraWebChart" tagprefix="igchart" %>
<%@ Register assembly="Infragistics2.WebUI.UltraWebChart.v7.2, Version=7.2.20072.61, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.UltraChart.Resources.Appearance" tagprefix="igchartprop" %>
<%@ Register assembly="Infragistics2.WebUI.UltraWebChart.v7.2, Version=7.2.20072.61, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.UltraChart.Data" tagprefix="igchartdata" %>

<%@ Register assembly="Infragistics2.WebUI.UltraWebChart.v7.2, Version=7.2.20072.61, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.UltraWebChart" tagprefix="igchart" %>

<%@ Register assembly="Infragistics2.WebUI.UltraWebChart.v7.2, Version=7.2.20072.61, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" namespace="Infragistics.WebUI.UltraWebChart" tagprefix="igchart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
    <uc1:TrainingTabs ID="TrainingTabs1" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table class="data_carrier" width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>    
      <td>
          <h1 class="section2">
              <span>
                  <label id="lblTitle" runat="server">
                  </label>
              </span>
          </h1>
          
      </td>
  </tr>
  <tr>    
    <td class="p_section2 data_carrier_block" valign="top">
    <div style="float:right;width:270px">
    <uc2:DropDownList ID="dropDownList" runat="server" />
    </div>
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
<tr>
<td valign="top" >

<div class="tab_subheader" style="border-bottom:solid 1px Silver;width:95%;">
Training Cost Analysis 
</div>
<div>

<label id="lblDepartment" runat="server" style="font-weight:bold">Department</label>
    <asp:DropDownList ID="ddlDepartment" runat="server" Width="150px" 
        onselectedindexchanged="ddlDepartment_SelectedIndexChanged">
    </asp:DropDownList>
    
    <label id="Label1" runat="server" style="font-weight:bold">Area</label>
     <asp:DropDownList ID="ddlArea" runat="server" Width="150px" 
        onselectedindexchanged="ddlArea_SelectedIndexChanged">
    </asp:DropDownList>
    <label style="font-weight:bold">From Date</label>
              <asp:TextBox ID="txtFromDate" runat="server" width="75px">
            </asp:TextBox>
                <asp:Image ID="imgFromDate" runat="server" SkinID="Calender" ImageAlign="Middle"/>
                <ajaxToolkit:CalendarExtender ID="cldExtender1"   CssClass="MyCalendar"
                TargetControlID="txtFromDate" PopupButtonID="imgFromDate" runat="server">
                </ajaxToolkit:CalendarExtender>
                
                    <asp:RegularExpressionValidator ID="REV1" runat="server" ErrorMessage="Please enter valid from date"
                    ControlToValidate="txtFromDate" ValidationGroup="Group4" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d" Display="None">
                    </asp:RegularExpressionValidator >
                
                <label style="font-weight:bold">To Date</label>
              <asp:TextBox ID="txtToDate" runat="server" width="75px" >
            </asp:TextBox>
                <asp:Image ID="imgToDate" runat="server" SkinID="Calender" ImageAlign="Middle"/>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1"   CssClass="MyCalendar"
                TargetControlID="txtToDate" PopupButtonID="imgToDate" runat="server">
                </ajaxToolkit:CalendarExtender>
              
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Please enter valid to date"
                    ControlToValidate="txtToDate" ValidationGroup="Group4" ValidationExpression="(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d" Display="None">
                    </asp:RegularExpressionValidator>
              <asp:CompareValidator ID="CompareValidator4" runat="server" Display="None" ControlToCompare="txtFromDate"
                ErrorMessage="To date should be greater than from date" Type="Date" ControlToValidate="txtToDate" Operator="GreaterThan" ValidationGroup="Group4"></asp:CompareValidator>
                
    <asp:ImageButton ID="btnView" runat="server" SkinID="ImgView" ValidationGroup="Group4" 
        onclick="btnView_Click" />
        </div>
        <div>
         <div >
      <asp:ValidationSummary ID="ValidationSummaryFilter" runat="server" ValidationGroup="Group4" ShowSummary="true" />
      <asp:Label ID="lblException" runat="server" ></asp:Label>
  </div>
        </div>
        <div align="center"> 
<igchart:UltraChart ID="UltraChart1" runat="server" Version="7.2" Width="750px" Height="400px">
<Axis>
<PE ElementType="None" Fill="Cornsilk"></PE>

<X Visible="True" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="0">
<MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255"></MajorGridLines>

<MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255"></MinorGridLines>

<Labels ItemFormatString="&lt;ITEM_LABEL&gt;" Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" VerticalAlign="Center" Orientation="VerticalLeftFacing">
<SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Center" VerticalAlign="Center" Orientation="Horizontal">
<Layout Behavior="Auto"></Layout>
</SeriesLabels>

<Layout Behavior="Auto"></Layout>
</Labels>
</X>

<Y Visible="True" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="20">
<MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255"></MajorGridLines>

<MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255"></MinorGridLines>

<Labels ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Far" VerticalAlign="Center" Orientation="Horizontal">
<SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Center" VerticalAlign="Center" Orientation="VerticalLeftFacing">
<Layout Behavior="Auto"></Layout>
</SeriesLabels>

<Layout Behavior="Auto"></Layout>
</Labels>
</Y>

<Y2 Visible="False" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="20">
<MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255"></MajorGridLines>

<MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255"></MinorGridLines>

<Labels ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" VerticalAlign="Center" Orientation="Horizontal">
<SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Center" VerticalAlign="Center" Orientation="VerticalLeftFacing">
<Layout Behavior="Auto"></Layout>
</SeriesLabels>

<Layout Behavior="Auto"></Layout>
</Labels>
</Y2>

<X2 Visible="False" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="0">
<MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255"></MajorGridLines>

<MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255"></MinorGridLines>

<Labels ItemFormatString="&lt;ITEM_LABEL&gt;" Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Far" VerticalAlign="Center" Orientation="VerticalLeftFacing">
<SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Center" VerticalAlign="Center" Orientation="Horizontal">
<Layout Behavior="Auto"></Layout>
</SeriesLabels>

<Layout Behavior="Auto"></Layout>
</Labels>
</X2>

<Z Visible="False" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="0">
<MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255"></MajorGridLines>

<MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255"></MinorGridLines>

<Labels ItemFormatString="" Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near" VerticalAlign="Center" Orientation="Horizontal">
<SeriesLabels Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Center" VerticalAlign="Center" Orientation="Horizontal">
<Layout Behavior="Auto"></Layout>
</SeriesLabels>

<Layout Behavior="Auto"></Layout>
</Labels>
</Z>

<Z2 Visible="False" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="0">
<MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255"></MajorGridLines>

<MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255"></MinorGridLines>

<Labels ItemFormatString="" Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near" VerticalAlign="Center" Orientation="Horizontal">
<SeriesLabels Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Center" VerticalAlign="Center" Orientation="Horizontal">
<Layout Behavior="Auto"></Layout>
</SeriesLabels>

<Layout Behavior="Auto"></Layout>
</Labels>
</Z2>
</Axis>

                <Border Thickness="0" />

<Effects><Effects>
<igchartprop:GradientEffect></igchartprop:GradientEffect>
</Effects>
</Effects>

                <ColumnChart ColumnSpacing="1">
                </ColumnChart>
                <Legend Location="Bottom" SpanPercentage="15" Visible="True" 
                    BackgroundColor="White" BorderThickness="0"></Legend>
                <Data ZeroAligned="True">
                </Data>

<ColorModel ModelStyle="CustomLinear" AlphaLevel="150"></ColorModel>
            </igchart:UltraChart>
    </div>

</td>
</tr>
</table>
</td>
</tr>
    </table>
</asp:Content>


