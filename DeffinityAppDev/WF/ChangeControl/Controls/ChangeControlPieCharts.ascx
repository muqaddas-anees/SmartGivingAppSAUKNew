<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_ServiceDeskCharts" Codebehind="ChangeControlPieCharts.ascx.cs" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v7.2, Version=7.2.20072.61, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.UltraChart.Resources.Appearance" TagPrefix="igchartprop" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v7.2, Version=7.2.20072.61, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.UltraChart.Data" TagPrefix="igchartdata" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v7.2, Version=7.2.20072.61, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebChart" TagPrefix="igchart" %>
    
    <style type="text/css">
    .clean-gray
    {
        border: solid 1px #DEDEDE;
        background: #EFEFEF;
        color: #222222;
        padding: 4px;
        text-align: left;
        font-variant:small-caps;
    }
</style>

<table width="100%" border="0" cellpadding="0" cellspacing="0">
<tr>
    <td colspan="3"><label style="font-weight:bold">Select <asp:Label ID="lblCategory" runat="server"></asp:Label></label><asp:DropDownList 
            ID="ddlmastercategory" runat="server"
                            AutoPostBack="True" 
         Width="175px" 
            ondatabound="ddlmastercategory_DataBound">
                            
                        </asp:DropDownList>
                      <%--  <asp:ObjectDataSource ID="CategorDropDownFiller" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="GetProjectMasterCategory" TypeName="Deffinity.PortfolioSLAmanager.PortfolioSLA">
                                <SelectParameters>
        <asp:SessionParameter Name="PortfolioID" SessionField="PortfolioID" />
        </SelectParameters>
                                </asp:ObjectDataSource>--%>
        <ajaxToolkit:CascadingDropDown ID="CascadingCategory" runat="server" TargetControlID="ddlmastercategory"
                Category="Task1" PromptText="Please select..." PromptValue="0" ServicePath="~/ServiceMgr.asmx"
                ServiceMethod="GetProjectMasterCategory" />
                        </td>
</tr>
    <tr style="vertical-align: top">
        <td style="vertical-align: middle">
            <span id="spanWeekly" runat="Server" enableviewstate="False" />
            <igchart:UltraChart ID="WeeklyChart" runat="server"  Version="7.2" DataSourceID="sqlLastWeek"
                PieChart-PieThickness="30" BorderColor="#E0E0E0" BorderWidth="0px" EmptyChartText="No Service requests for this Week"
                BackgroundImageFileName="" ChartType="PieChart" Width="350px" Transform3D-Scale="75"
                Transform3D-XRotation="65" Transform3D-YRotation="30" 
                Transform3D-ZRotation="5" onchartdrawitem="WeeklyChart_ChartDrawItem">
                <Axis>
                    <PE ElementType="Gradient" Fill="192, 192, 255"></PE>
                    <X Visible="True" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="0" 
                        LineColor="Blue">
                        <MajorGridLines Visible="True" DrawStyle="Dot" Color="Blue" Thickness="1" AlphaLevel="255">
                        </MajorGridLines>
                        <MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255">
                        </MinorGridLines>
                        <Labels ItemFormatString="&lt;ITEM_LABEL&gt;" Visible="False" Font="Verdana, 7pt"
                            FontColor="DimGray" HorizontalAlign="Near" VerticalAlign="Center" Orientation="Horizontal">
                            <SeriesLabels FormatString="" Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near"
                                VerticalAlign="Center" Orientation="Horizontal">
                                <Layout Behavior="Auto">
                                </Layout>
                            </SeriesLabels>
                            <Layout Behavior="Auto">
                            </Layout>
                        </Labels>
                    </X>
                    <Y Visible="True" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="40"
                        LineColor="Blue">
                        <MajorGridLines Visible="True" DrawStyle="Dot" Color="Blue" Thickness="1" AlphaLevel="255">
                        </MajorGridLines>
                        <MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255">
                        </MinorGridLines>
                        <Labels ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Visible="False" Font="Verdana, 7pt"
                            FontColor="DimGray" HorizontalAlign="Near" VerticalAlign="Center" 
                            Orientation="Horizontal">
                            <SeriesLabels FormatString="" Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near"
                                VerticalAlign="Center" Orientation="Horizontal">
                                <Layout Behavior="Auto">
                                </Layout>
                            </SeriesLabels>
                            <Layout Behavior="Auto">
                            </Layout>
                        </Labels>
                    </Y>
                    <Y2 Visible="False" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="40"
                        LineColor="0, 0, 192">
                        <MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255">
                        </MajorGridLines>
                        <MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255">
                        </MinorGridLines>
                        <Labels ItemFormatString="" Visible="False" Font="Verdana, 7pt" FontColor="Gray"
                            HorizontalAlign="Near" VerticalAlign="Center" Orientation="Horizontal">
                            <SeriesLabels FormatString="" Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near"
                                VerticalAlign="Center" Orientation="Horizontal">
                                <Layout Behavior="Auto">
                                </Layout>
                            </SeriesLabels>
                            <Layout Behavior="Auto">
                            </Layout>
                        </Labels>
                    </Y2>
                    <X2 Visible="False" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="0"
                        LineColor="Blue">
                        <MajorGridLines Visible="True" DrawStyle="Dot" Color="Blue" Thickness="1" AlphaLevel="255">
                        </MajorGridLines>
                        <MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255">
                        </MinorGridLines>
                        <Labels ItemFormatString="" Visible="False" Font="Verdana, 7pt" FontColor="Gray"
                            HorizontalAlign="Near" VerticalAlign="Center" Orientation="Horizontal">
                            <SeriesLabels FormatString="" Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near"
                                VerticalAlign="Center" Orientation="Horizontal">
                                <Layout Behavior="Auto">
                                </Layout>
                            </SeriesLabels>
                            <Layout Behavior="Auto">
                            </Layout>
                        </Labels>
                    </X2>
                    <Z Visible="False" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="0">
                        <MajorGridLines Visible="True" DrawStyle="Dot" Color="Blue" Thickness="1" AlphaLevel="143">
                        </MajorGridLines>
                        <MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255">
                        </MinorGridLines>
                        <Labels ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Visible="False" Font="Verdana, 7pt"
                            FontColor="DimGray" HorizontalAlign="Near" VerticalAlign="Center" 
                            Orientation="Horizontal">
                            <SeriesLabels FormatString="" Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near"
                                VerticalAlign="Center" Orientation="Horizontal">
                                <Layout Behavior="Auto">
                                </Layout>
                            </SeriesLabels>
                            <Layout Behavior="Auto">
                            </Layout>
                        </Labels>
                    </Z>
                    <Z2 Visible="False" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="0">
                        <MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255">
                        </MajorGridLines>
                        <MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255">
                        </MinorGridLines>
                        <Labels ItemFormatString="" Visible="False" Font="Verdana, 7pt" FontColor="Gray"
                            HorizontalAlign="Near" VerticalAlign="Center" Orientation="Horizontal">
                            <SeriesLabels FormatString="" Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near"
                                VerticalAlign="Center" Orientation="Horizontal">
                                <Layout Behavior="Auto">
                                </Layout>
                            </SeriesLabels>
                            <Layout Behavior="Auto">
                            </Layout>
                        </Labels>
                    </Z2>
                </Axis>
                <TitleTop  FontColor="Gray" FontSizeBestFit="false" Font="Verdana, 10pt" >
                </TitleTop>
                <Border Color="224, 224, 224" CornerRadius="10"></Border>
                <PieChart BreakAllSlices="True" BreakAlternatingSlices="True" 
                    BreakDistancePercentage="3" PieThickness="10">
                    <Labels FormatString="&lt;DATA_VALUE:00.##&gt;" />
                    <ChartText>
                        <igchartprop:ChartTextAppearance ChartTextFont="Arial, 7pt" Column="-2" 
                            ItemFormatString="&lt;DATA_VALUE:00&gt;" Row="-2" Visible="True" />
                    </ChartText>
                </PieChart>
                <Tooltips Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" FontColor="Navy" BackColor="192, 192, 255" EnableFadingEffect="True"
                    Overflow="ChartArea" Display="Never"></Tooltips>
                <Effects>
                    <Effects>
                        <igchartprop:GradientEffect>
                        </igchartprop:GradientEffect>
                    </Effects>
                </Effects>
                <Legend Location="Bottom" FontColor="Gray" Visible="True" MoreIndicatorText="More.."
                    SpanPercentage="40" Font="Verdana, 7pt">
                    <Margins Bottom="10" Left="0" Right="0" Top="0" />
                </Legend>
                <TitleBottom Extent="33" Visible="False" Text="Copyright 2009 Deffinity.com" Font="Lucida Console, 10.2pt"
                    FontColor="0, 0, 192" Location="Bottom">
                </TitleBottom>
                <TitleLeft Extent="33" Visible="True" FontColor="0, 0, 192" Location="Left">
                </TitleLeft>
                <ColorModel ModelStyle="CustomSkin" ColorBegin="Blue" ColorEnd="DarkBlue" 
                    AlphaLevel="150">
                    <Skin>
                        <PEs>
                            <igchartprop:PaintElement Fill="White">
                            </igchartprop:PaintElement>
                        </PEs>
                    </Skin>
                </ColorModel>
            </igchart:UltraChart>
        </td>
        <td style="vertical-align: middle">
            <span id="spanMonthly" runat="Server" enableviewstate="False" />
            <igchart:UltraChart ID="MonthlyChart" runat="server" DataSourceID="sqlLastMonth"
                PieChart-PieThickness="30" BorderColor="#E0E0E0" BorderWidth="0px" EmptyChartText="No Service requests for this Month"
                BackgroundImageFileName="" ChartType="PieChart" Transform3D-Scale="75" Width="350px"
                Transform3D-XRotation="65" Transform3D-YRotation="30" 
                Transform3D-ZRotation="5" Version="7.2" 
                onchartdrawitem="MonthlyChart_ChartDrawItem">
                <Axis>
                    <PE ElementType="Gradient" Fill="192, 192, 255"></PE>
                    <X Visible="True" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="0" 
                        LineColor="Blue">
                        <MajorGridLines Visible="True" DrawStyle="Dot" Color="Blue" Thickness="1" AlphaLevel="255">
                        </MajorGridLines>
                        <MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255">
                        </MinorGridLines>
                        <Labels ItemFormatString="&lt;ITEM_LABEL&gt;" Visible="False" Font="Verdana, 7pt"
                            FontColor="DimGray" HorizontalAlign="Near" VerticalAlign="Center" Orientation="Horizontal">
                            <SeriesLabels FormatString="" Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near"
                                VerticalAlign="Center" Orientation="Horizontal">
                                <Layout Behavior="Auto">
                                </Layout>
                            </SeriesLabels>
                            <Layout Behavior="Auto">
                            </Layout>
                        </Labels>
                    </X>
                    <Y Visible="True" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="40"
                        LineColor="Blue">
                        <MajorGridLines Visible="True" DrawStyle="Dot" Color="Blue" Thickness="1" AlphaLevel="255">
                        </MajorGridLines>
                        <MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255">
                        </MinorGridLines>
                        <Labels ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Visible="False" Font="Verdana, 7pt"
                            FontColor="DimGray" HorizontalAlign="Near" VerticalAlign="Center" 
                            Orientation="Horizontal">
                            <SeriesLabels FormatString="" Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near"
                                VerticalAlign="Center" Orientation="Horizontal">
                                <Layout Behavior="Auto">
                                </Layout>
                            </SeriesLabels>
                            <Layout Behavior="Auto">
                            </Layout>
                        </Labels>
                    </Y>
                    <Y2 Visible="False" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="40"
                        LineColor="0, 0, 192">
                        <MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255">
                        </MajorGridLines>
                        <MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255">
                        </MinorGridLines>
                        <Labels ItemFormatString="" Visible="False" Font="Verdana, 7pt" FontColor="Gray"
                            HorizontalAlign="Near" VerticalAlign="Center" Orientation="Horizontal">
                            <SeriesLabels FormatString="" Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near"
                                VerticalAlign="Center" Orientation="Horizontal">
                                <Layout Behavior="Auto">
                                </Layout>
                            </SeriesLabels>
                            <Layout Behavior="Auto">
                            </Layout>
                        </Labels>
                    </Y2>
                    <X2 Visible="False" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="0"
                        LineColor="Blue">
                        <MajorGridLines Visible="True" DrawStyle="Dot" Color="Blue" Thickness="1" AlphaLevel="255">
                        </MajorGridLines>
                        <MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255">
                        </MinorGridLines>
                        <Labels ItemFormatString="" Visible="False" Font="Verdana, 7pt" FontColor="Gray"
                            HorizontalAlign="Near" VerticalAlign="Center" Orientation="Horizontal">
                            <SeriesLabels FormatString="" Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near"
                                VerticalAlign="Center" Orientation="Horizontal">
                                <Layout Behavior="Auto">
                                </Layout>
                            </SeriesLabels>
                            <Layout Behavior="Auto">
                            </Layout>
                        </Labels>
                    </X2>
                    <Z Visible="False" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="0">
                        <MajorGridLines Visible="True" DrawStyle="Dot" Color="Blue" Thickness="1" AlphaLevel="143">
                        </MajorGridLines>
                        <MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255">
                        </MinorGridLines>
                        <Labels ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Visible="False" Font="Verdana, 7pt"
                            FontColor="DimGray" HorizontalAlign="Near" VerticalAlign="Center" 
                            Orientation="Horizontal">
                            <SeriesLabels FormatString="" Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near"
                                VerticalAlign="Center" Orientation="Horizontal">
                                <Layout Behavior="Auto">
                                </Layout>
                            </SeriesLabels>
                            <Layout Behavior="Auto">
                            </Layout>
                        </Labels>
                    </Z>
                    <Z2 Visible="False" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="0">
                        <MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255">
                        </MajorGridLines>
                        <MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255">
                        </MinorGridLines>
                        <Labels ItemFormatString="" Visible="False" Font="Verdana, 7pt" FontColor="Gray"
                            HorizontalAlign="Near" VerticalAlign="Center" Orientation="Horizontal">
                            <SeriesLabels FormatString="" Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near"
                                VerticalAlign="Center" Orientation="Horizontal">
                                <Layout Behavior="Auto">
                                </Layout>
                            </SeriesLabels>
                            <Layout Behavior="Auto">
                            </Layout>
                        </Labels>
                    </Z2>
                </Axis>
                <TitleTop  FontColor="Gray" FontSizeBestFit="false" Font="Verdana, 10pt" >
                </TitleTop>
                <Border Color="224, 224, 224" CornerRadius="10"></Border>
                <PieChart BreakAllSlices="True" BreakAlternatingSlices="True" 
                    BreakDistancePercentage="3" PieThickness="10">
                    <Labels FormatString="&lt;DATA_VALUE:00.##&gt;" />
                    <ChartText>
                        <igchartprop:ChartTextAppearance ChartTextFont="Arial, 7pt" Column="-2" 
                            ItemFormatString="&lt;DATA_VALUE:00&gt;" Row="-2" Visible="True" />
                    </ChartText>
                </PieChart>
                <Tooltips Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" FontColor="Navy" BackColor="192, 192, 255" EnableFadingEffect="True"
                    Overflow="ChartArea" Display="Never"></Tooltips>
                <Effects>
                    <Effects>
                        <igchartprop:GradientEffect>
                        </igchartprop:GradientEffect>
                    </Effects>
                </Effects>
                <Legend Location="Bottom" FontColor="Gray" Visible="True" MoreIndicatorText="More.."
                    SpanPercentage="40" Font="Verdana, 7pt">
                    <Margins Bottom="10" Left="0" Right="0" Top="0" />
                </Legend>
                <TitleBottom Extent="33" Visible="False" Text="Copyright 2009 Deffinity.com" Font="Lucida Console, 10.2pt"
                    FontColor="0, 0, 192">
                </TitleBottom>
                <TitleLeft Extent="33" Visible="True" FontColor="0, 0, 192">
                </TitleLeft>
                <ColorModel ModelStyle="CustomSkin" ColorBegin="Blue" ColorEnd="DarkBlue" 
                    AlphaLevel="150">
                    <Skin>
                        <PEs>
                            <igchartprop:PaintElement Fill="White">
                            </igchartprop:PaintElement>
                        </PEs>
                    </Skin>
                </ColorModel>
            </igchart:UltraChart>
        </td>
        <td style="vertical-align: middle">
            <span id="spanYearly" runat="Server" enableviewstate="False" />
            <igchart:UltraChart ID="YearlyChart" runat="server"  Version="7.2" DataSourceID="sqlLastYear"
                PieChart-PieThickness="30" BorderColor="#E0E0E0" BorderWidth="0px" EmptyChartText="No Service requests for this year"
                BackgroundImageFileName="" ChartType="PieChart" Width="350px" Transform3D-Scale="75"
                Transform3D-XRotation="65" Transform3D-YRotation="30" 
                Transform3D-ZRotation="5" onchartdrawitem="YearlyChart_ChartDrawItem">
                <Axis>
                    <PE ElementType="Gradient" Fill="192, 192, 255"></PE>
                    <X Visible="True" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="0" 
                        LineColor="Blue">
                        <MajorGridLines Visible="True" DrawStyle="Dot" Color="Blue" Thickness="1" AlphaLevel="255">
                        </MajorGridLines>
                        <MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255">
                        </MinorGridLines>
                        <Labels ItemFormatString="&lt;ITEM_LABEL&gt;" Visible="False" Font="Verdana, 7pt"
                            FontColor="DimGray" HorizontalAlign="Near" VerticalAlign="Center" Orientation="Horizontal">
                            <SeriesLabels FormatString="" Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near"
                                VerticalAlign="Center" Orientation="Horizontal">
                                <Layout Behavior="Auto">
                                </Layout>
                            </SeriesLabels>
                            <Layout Behavior="Auto">
                            </Layout>
                        </Labels>
                    </X>
                    <Y Visible="True" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="40"
                        LineColor="Blue">
                        <MajorGridLines Visible="True" DrawStyle="Dot" Color="Blue" Thickness="1" AlphaLevel="255">
                        </MajorGridLines>
                        <MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255">
                        </MinorGridLines>
                        <Labels ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Visible="False" Font="Verdana, 7pt"
                            FontColor="DimGray" HorizontalAlign="Near" VerticalAlign="Center" 
                            Orientation="Horizontal">
                            <SeriesLabels FormatString="" Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near"
                                VerticalAlign="Center" Orientation="Horizontal">
                                <Layout Behavior="Auto">
                                </Layout>
                            </SeriesLabels>
                            <Layout Behavior="Auto">
                            </Layout>
                        </Labels>
                    </Y>
                    <Y2 Visible="False" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="40"
                        LineColor="0, 0, 192">
                        <MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255">
                        </MajorGridLines>
                        <MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255">
                        </MinorGridLines>
                        <Labels ItemFormatString="" Visible="False" Font="Verdana, 7pt" FontColor="Gray"
                            HorizontalAlign="Near" VerticalAlign="Center" Orientation="Horizontal">
                            <SeriesLabels FormatString="" Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near"
                                VerticalAlign="Center" Orientation="Horizontal">
                                <Layout Behavior="Auto">
                                </Layout>
                            </SeriesLabels>
                            <Layout Behavior="Auto">
                            </Layout>
                        </Labels>
                    </Y2>
                    <X2 Visible="False" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="0"
                        LineColor="Blue">
                        <MajorGridLines Visible="True" DrawStyle="Dot" Color="Blue" Thickness="1" AlphaLevel="255">
                        </MajorGridLines>
                        <MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255">
                        </MinorGridLines>
                        <Labels ItemFormatString="" Visible="False" Font="Verdana, 7pt" FontColor="Gray"
                            HorizontalAlign="Near" VerticalAlign="Center" Orientation="Horizontal">
                            <SeriesLabels FormatString="" Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near"
                                VerticalAlign="Center" Orientation="Horizontal">
                                <Layout Behavior="Auto">
                                </Layout>
                            </SeriesLabels>
                            <Layout Behavior="Auto">
                            </Layout>
                        </Labels>
                    </X2>
                    <Z Visible="False" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="0">
                        <MajorGridLines Visible="True" DrawStyle="Dot" Color="Blue" Thickness="1" AlphaLevel="143">
                        </MajorGridLines>
                        <MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255">
                        </MinorGridLines>
                        <Labels ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Visible="False" Font="Verdana, 7pt"
                            FontColor="DimGray" HorizontalAlign="Near" VerticalAlign="Center" 
                            Orientation="Horizontal">
                            <SeriesLabels FormatString="" Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Near"
                                VerticalAlign="Center" Orientation="Horizontal">
                                <Layout Behavior="Auto">
                                </Layout>
                            </SeriesLabels>
                            <Layout Behavior="Auto">
                            </Layout>
                        </Labels>
                    </Z>
                    <Z2 Visible="False" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="0">
                        <MajorGridLines Visible="True" DrawStyle="Dot" Color="Gainsboro" Thickness="1" AlphaLevel="255">
                        </MajorGridLines>
                        <MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255">
                        </MinorGridLines>
                        <Labels ItemFormatString="" Visible="False" Font="Verdana, 7pt" FontColor="Gray"
                            HorizontalAlign="Near" VerticalAlign="Center" Orientation="Horizontal">
                            <SeriesLabels FormatString="" Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Near"
                                VerticalAlign="Center" Orientation="Horizontal">
                                <Layout Behavior="Auto">
                                </Layout>
                            </SeriesLabels>
                            <Layout Behavior="Auto">
                            </Layout>
                        </Labels>
                    </Z2>
                </Axis>
                <TitleTop FontColor="Gray" FontSizeBestFit="false" Font="Verdana, 10pt"  >
                </TitleTop>                
                <Border Color="224, 224, 224" CornerRadius="10"></Border>
                <PieChart BreakAllSlices="True" BreakAlternatingSlices="True" 
                    BreakDistancePercentage="3" PieThickness="10">
                    <Labels FormatString="&lt;DATA_VALUE:00.##&gt;" />
                    <ChartText>
                        <igchartprop:ChartTextAppearance ChartTextFont="Arial, 7pt" Column="-2" 
                            ItemFormatString="&lt;DATA_VALUE:00&gt;" Row="-2" Visible="True" />
                    </ChartText>
                </PieChart>
                <Tooltips Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                    Font-Underline="False" FontColor="Navy" BackColor="192, 192, 255" EnableFadingEffect="True"
                    Overflow="ChartArea" Display="Never"></Tooltips>
                <Effects>
                    <Effects>
                        <igchartprop:GradientEffect>
                        </igchartprop:GradientEffect>
                    </Effects>
                </Effects>
                <Legend Location="Bottom" FontColor="Gray" Visible="True" MoreIndicatorText="More.."
                    SpanPercentage="40" Font="Verdana, 7pt">
                    <Margins Bottom="10" Left="0" Right="0" Top="0" />
                </Legend>
                <TitleBottom Extent="33" Visible="False" Text="Copyright 2009 Deffinity.com" Font="Lucida Console, 10.2pt"
                    FontColor="0, 0, 192" Location="Bottom">
                </TitleBottom>
                <TitleLeft Extent="33" Visible="True" FontColor="0, 0, 192" Location="Left">
                </TitleLeft>
                <ColorModel ModelStyle="CustomSkin" ColorBegin="Blue" ColorEnd="DarkBlue" 
                    AlphaLevel="150">
                    <Skin>
                        <PEs>
                            <igchartprop:PaintElement Fill="White">
                            </igchartprop:PaintElement>
                        </PEs>
                    </Skin>
                </ColorModel>
            </igchart:UltraChart>
        </td>
    </tr>
</table>

<asp:HiddenField ID="Hid_incidenttype" runat="server" />
<asp:SqlDataSource ID="sqlLastWeek" runat="server" 
SelectCommand="select count(*) as no,SubCategoryName as Category from v_ChangeControl where  lower(status) = 'closed' and SubCategoryID>0   and datediff(mm,DateLogged,getdate())=1 and DateLogged < (GETDATE()+1)  and CategoryID =@CategoryID  group by SubCategoryID,SubCategoryName"
    ConnectionString="<%$ ConnectionStrings:DBstring %>" OnSelected="sqlLastWeek_Selected">
    <SelectParameters>
        <asp:SessionParameter Name="PortfolioID" SessionField="PortfolioID" />
        <asp:ControlParameter ControlID="ddlmastercategory" DefaultValue="0" Name="CategoryID" PropertyName="SelectedValue" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqlLastMonth" runat="server" SelectCommand="select count(*) as no,SubCategoryName as Category from v_ChangeControl where  lower(status) = 'closed' and SubCategoryID>0  and datediff(mm,DateLogged,getdate())=0 and DateLogged < (GETDATE()+1)  and CategoryID =@CategoryID group by SubCategoryID,SubCategoryName"
    ConnectionString="<%$ ConnectionStrings:DBstring %>" OnSelected="sqlLastMonth_Selected">
    <SelectParameters>
        <asp:SessionParameter Name="PortfolioID" SessionField="PortfolioID" />
        <asp:ControlParameter ControlID="ddlmastercategory" DefaultValue="0" Name="CategoryID" PropertyName="SelectedValue" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqlLastYear" runat="server" SelectCommand="select count(*) as no,SubCategoryName as Category from v_ChangeControl where  lower(status) = 'closed' and SubCategoryID>0  and year(DateLogged) = year(getdate()) and CategoryID =@CategoryID group by SubCategoryID,SubCategoryName"
    ConnectionString="<%$ ConnectionStrings:DBstring %>" OnSelected="sqlLastYear_Selected">
    <SelectParameters>
        <asp:SessionParameter Name="PortfolioID" SessionField="PortfolioID" />
        <asp:ControlParameter ControlID="ddlmastercategory" DefaultValue="0" Name="CategoryID" PropertyName="SelectedValue" />
    </SelectParameters>
</asp:SqlDataSource>