<%@ Page Language="C#" AutoEventWireup="true" Inherits="WebSamplesCS.WebCharts.Gallery.Gantt.TaskStatus" Codebehind="TaskStatus.aspx.cs" %>

<%--<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.UltraChart.Resources.Appearance" TagPrefix="igchartprop" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.UltraChart.Data" TagPrefix="igchartdata" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v7.3, Version=7.3.20073.38, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebChart" TagPrefix="igchart" %>--%>
    
    <%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v7.2, Version=7.2.20072.61, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebChart" TagPrefix="igchart" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v7.2, Version=7.2.20072.61, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.UltraChart.Resources.Appearance" TagPrefix="igchartprop" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v7.2, Version=7.2.20072.61, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.UltraChart.Data" TagPrefix="igchartdata" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Project Plan</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
        <td style="font-size:large;color:Orange;padding-top:20px;padding-bottom:5px" align="left">
        
        <asp:Label ID="lblmsg" runat="server"></asp:Label>
        </td>
        </tr>
             <tr>
                <td >
                      
                 <igchart:UltraChart id="UltraChart1" runat="server" ChartType="GanttChart" BackgroundImageFileName="" BorderColor="Black" BorderWidth="1px" EmptyChartText="Data Not Available. Please call UltraChart.Data.DataBind() after setting valid Data.DataSource" Version="7.3">
                                <TitleRight Extent="33" Visible="True">
                                </TitleRight>
                                <Data>
                                    <EmptyStyle>
                                        <LineStyle DrawStyle="Dash"></LineStyle>
                                    </EmptyStyle>
                                </Data>
                                <TitleLeft Extent="33" Visible="True">
                                </TitleLeft>
                                <ColorModel ColorBegin="Pink" ColorEnd="DarkRed" AlphaLevel="150" ModelStyle="CustomLinear">
                                    <Skin ApplyRowWise="False">
                                        <PEs>
                                            <igchartprop:PaintElement FillGradientStyle="Vertical" ElementType="Gradient" Fill="108, 162, 36" FillStopColor="148, 244, 17" StrokeWidth="0"></igchartprop:PaintElement>
                                            <igchartprop:PaintElement FillGradientStyle="Vertical" ElementType="Gradient" Fill="7, 108, 176" FillStopColor="53, 200, 255" StrokeWidth="0"></igchartprop:PaintElement>
                                            <igchartprop:PaintElement FillGradientStyle="Vertical" ElementType="Gradient" Fill="230, 190, 2" FillStopColor="255, 255, 81" StrokeWidth="0"></igchartprop:PaintElement>
                                            <igchartprop:PaintElement FillGradientStyle="Vertical" ElementType="Gradient" Fill="215, 0, 5" FillStopColor="254, 117, 16" StrokeWidth="0"></igchartprop:PaintElement>
                                            <igchartprop:PaintElement FillGradientStyle="Vertical" ElementType="Gradient" Fill="252, 122, 10" FillStopColor="255, 108, 66" StrokeWidth="0"></igchartprop:PaintElement>
                                        </PEs>
                                    </Skin>
                                </ColorModel>
                                <Axis>
                                    <Y Visible="True" TickmarkInterval="2" LineThickness="1" Extent="108" TickmarkStyle="Smart">
                                        <Labels ItemFormatString="&lt;ITEM_LABEL&gt;" VerticalAlign="Center" Font="Verdana, 10pt" FontColor="DimGray" Orientation="Horizontal" HorizontalAlign="Center">
                                            <SeriesLabels Font="Verdana, 10pt" Visible="False" HorizontalAlign="Center" Orientation="VerticalLeftFacing" FontColor="DimGray" VerticalAlign="Center">
                                                <Layout Behavior="Auto">
                                                </Layout>
                                            </SeriesLabels>
                                            <Layout Behavior="Auto">
                                            </Layout>
                                        </Labels>
                                        <MajorGridLines AlphaLevel="255" DrawStyle="Dot" Color="Gainsboro" Visible="True" Thickness="1"></MajorGridLines>
                                        <MinorGridLines AlphaLevel="255" DrawStyle="Dot" Color="LightGray" Visible="True" Thickness="1"></MinorGridLines>
                                    </Y>
                                    <Y2 Visible="False" TickmarkInterval="2" LineThickness="1" TickmarkStyle="Smart">
                                        <Labels ItemFormatString="&lt;ITEM_LABEL&gt;" VerticalAlign="Center" Font="Verdana, 7pt" FontColor="Gray" Orientation="Horizontal" HorizontalAlign="Far">
                                            <SeriesLabels Font="Verdana, 7pt" HorizontalAlign="Far" Orientation="VerticalLeftFacing" FontColor="Gray" VerticalAlign="Center">
                                                <Layout Behavior="Auto">
                                                </Layout>
                                            </SeriesLabels>
                                            <Layout Behavior="Auto">
                                            </Layout>
                                        </Labels>
                                        <MajorGridLines AlphaLevel="255" DrawStyle="Dot" Color="Gainsboro" Visible="True" Thickness="1"></MajorGridLines>
                                        <MinorGridLines AlphaLevel="255" DrawStyle="Dot" Color="LightGray" Visible="False" Thickness="1"></MinorGridLines>
                                    </Y2>
                                    <X2 Visible="True" TickmarkInterval="50" LineThickness="0" TickmarkStyle="Smart" TickmarkIntervalType="Hours" Extent="46">
                                        <Labels ItemFormatString="&lt;ITEM_LABEL:dd-MM-yy&gt;" VerticalAlign="Center" Font="Verdana, 7pt" FontColor="Gray" Orientation="VerticalLeftFacing" HorizontalAlign="Near">
                                            <SeriesLabels Font="Verdana, 7pt" HorizontalAlign="Near" FormatString="" Orientation="VerticalLeftFacing" FontColor="Gray" VerticalAlign="Center">
                                                <Layout Behavior="Auto">
                                                </Layout>
                                            </SeriesLabels>
                                            <Layout Behavior="Auto">
                                            </Layout>
                                        </Labels>
                                        <MajorGridLines AlphaLevel="255" DrawStyle="Dot" Color="Gainsboro" Visible="True" Thickness="1"></MajorGridLines>
                                        <MinorGridLines AlphaLevel="255" DrawStyle="Dot" Color="LightGray" Visible="False" Thickness="1"></MinorGridLines>
                                    </X2>
                                    <Z2 Visible="False" TickmarkInterval="0" LineThickness="1" TickmarkStyle="Smart">
                                        <Labels ItemFormatString="" VerticalAlign="Center" Font="Verdana, 7pt" FontColor="Gray" Orientation="Horizontal" HorizontalAlign="Near">
                                            <SeriesLabels Font="Verdana, 7pt" HorizontalAlign="Near" Orientation="Horizontal" FontColor="Gray" VerticalAlign="Center">
                                                <Layout Behavior="Auto">
                                                </Layout>
                                            </SeriesLabels>
                                            <Layout Behavior="Auto">
                                            </Layout>
                                        </Labels>
                                        <MajorGridLines AlphaLevel="255" DrawStyle="Dot" Color="Gainsboro" Visible="True" Thickness="1"></MajorGridLines>
                                        <MinorGridLines AlphaLevel="255" DrawStyle="Dot" Color="LightGray" Visible="False" Thickness="1"></MinorGridLines>
                                    </Z2>
                                    <Z Visible="False" TickmarkInterval="0" LineThickness="1" TickmarkStyle="Smart">
                                        <Labels ItemFormatString="" VerticalAlign="Center" Font="Verdana, 7pt" FontColor="DimGray" Orientation="Horizontal" HorizontalAlign="Near">
                                            <SeriesLabels Font="Verdana, 7pt" HorizontalAlign="Near" Orientation="Horizontal" FontColor="DimGray" VerticalAlign="Center">
                                                <Layout Behavior="Auto">
                                                </Layout>
                                            </SeriesLabels>
                                            <Layout Behavior="Auto">
                                            </Layout>
                                        </Labels>
                                        <MajorGridLines AlphaLevel="255" DrawStyle="Dot" Color="Gainsboro" Visible="True" Thickness="1"></MajorGridLines>
                                        <MinorGridLines AlphaLevel="255" DrawStyle="Dot" Color="LightGray" Visible="False" Thickness="1"></MinorGridLines>
                                    </Z>
                                    <X Visible="False" TickmarkInterval="50" LineThickness="1" Extent="54" TickmarkStyle="Smart" TickmarkIntervalType="Hours">
                                        <Labels ItemFormatString="&lt;ITEM_LABEL:dd-mm-yy" VerticalAlign="Center" Font="Verdana, 7pt" FontColor="DimGray" Orientation="Custom" HorizontalAlign="Near" Visible="False">
                                            <SeriesLabels Font="Verdana, 7pt" HorizontalAlign="Far" FormatString="" Orientation="VerticalLeftFacing" FontColor="DimGray" VerticalAlign="Center">
                                                <Layout Behavior="Auto">
                                                </Layout>
                                            </SeriesLabels>
                                            <Layout Behavior="Auto">
                                            </Layout>
                                        </Labels>
                                        <MajorGridLines AlphaLevel="255" DrawStyle="Dot" Color="Gainsboro" Visible="True" Thickness="1"></MajorGridLines>
                                        <MinorGridLines AlphaLevel="255" DrawStyle="Dot" Color="LightGray" Visible="True" Thickness="1"></MinorGridLines>
                                        <Margin>
                                            <Near Value="0.93457943925233633" />
                                        </Margin>
                                    </X>
                                    <PE ElementType="None" Fill="Cornsilk" />
                                </Axis>
                                <TitleBottom Visible="False" Extent="33">
                                </TitleBottom>
                                <TitleTop Visible="False">
                                </TitleTop>
                                <GanttChart>
                                    <LinkLineStyle EndStyle="ArrowAnchor"></LinkLineStyle>
                                    <CompletePercentagesPE Fill="Yellow"></CompletePercentagesPE>
                                    <OwnersLabelStyle Font="Microsoft Sans Serif, 7.8pt" HorizontalAlign="Center" ClipText="False"></OwnersLabelStyle>
                                    <EmptyPercentagesPE Fill="White"></EmptyPercentagesPE>
                                </GanttChart>
                     <Effects>
                         <Effects>
                             <igchartprop:GradientEffect>
                             </igchartprop:GradientEffect>
                         </Effects>
                     </Effects>
                     <Tooltips Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                         Font-Underline="False" />
                            </igchart:UltraChart>
                
        </td></tr></table>
   
     </div>
    </form>
</body>
</html>
