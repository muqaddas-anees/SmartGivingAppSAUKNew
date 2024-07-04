<%@ Control Language="C#" AutoEventWireup="true"
    Inherits="controls_Summary" Codebehind="HealthCheckSummaryCharts.ascx.cs" %>
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
<table style="width:100%;height:350px;vertical-align:middle;position:relative">
    <tr>
        <td>
            <span id="spanWeekly" runat="Server" enableviewstate="False" />
            <igchart:UltraChart ID="WeeklyChart" runat="server" DataSourceID="sqlLastWeek" Version="7.2"
                PieChart-PieThickness="30" BorderColor="#E0E0E0" BorderWidth="0px" EmptyChartText="No Health Checks for this Week"
                BackgroundImageFileName="" ChartType="PieChart3D" Width="300px" Transform3D-Scale="75"
                Transform3D-XRotation="65" Transform3D-YRotation="30" Transform3D-ZRotation="5">
                <Axis>
                    <PE ElementType="Gradient" Fill="192, 192, 255"></PE>
                    <X Visible="False" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="0" LineColor="Blue">
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
                    <Y Visible="False" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="40"
                        LineColor="Blue">
                        <MajorGridLines Visible="True" DrawStyle="Dot" Color="Blue" Thickness="1" AlphaLevel="255">
                        </MajorGridLines>
                        <MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255">
                        </MinorGridLines>
                        <Labels ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Visible="False" Font="Verdana, 7pt"
                            FontColor="DimGray" HorizontalAlign="Far" VerticalAlign="Center" Orientation="Horizontal">
                            <SeriesLabels FormatString="" Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Far"
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
                            HorizontalAlign="Far" VerticalAlign="Center" Orientation="Horizontal">
                            <SeriesLabels FormatString="" Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Far"
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
                            FontColor="DimGray" HorizontalAlign="Far" VerticalAlign="Center" Orientation="Horizontal">
                            <SeriesLabels FormatString="" Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Far"
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
                <TitleTop Text="Completed Health Checks for this Week" FontColor="Gray" FontSizeBestFit="True">
                </TitleTop>
                <Border Color="224, 224, 224" CornerRadius="10" Raised="True" Thickness="0"></Border>
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
                <ColorModel ModelStyle="CustomLinear" ColorBegin="Blue" ColorEnd="DarkBlue" AlphaLevel="255">
                    <Skin>
                        <PEs>
                            <igchartprop:PaintElement Fill="White">
                            </igchartprop:PaintElement>
                        </PEs>
                    </Skin>
                </ColorModel>
                <PieChart3D BreakAllSlices="True" BreakAlternatingSlices="True" BreakDistancePercentage="5"
                    PieThickness="10">
                    <ChartText>
                        <igchartprop:ChartTextAppearance ChartTextFont="Arial, 7pt" Column="-2" ItemFormatString="&lt;DATA_VALUE:00&gt;"
                            Row="-2" Visible="True">
                        </igchartprop:ChartTextAppearance>
                    </ChartText>
                    <Labels FormatString="&lt;DATA_VALUE:00.##&gt;" />
                </PieChart3D>
            </igchart:UltraChart>
        </td>
        <td valign="bottom">
            <span id="spanMonthly" runat="Server" enableviewstate="False" />
            <igchart:UltraChart ID="MonthlyChart" runat="server" DataSourceID="sqlLastMonth"
                PieChart-PieThickness="30" BorderColor="#E0E0E0" BorderWidth="0px" EmptyChartText="No Health Checks for this Month"
                BackgroundImageFileName="" ChartType="PieChart3D" Transform3D-Scale="75" Width="300px"
                Transform3D-XRotation="65" Transform3D-YRotation="30" Transform3D-ZRotation="5" Version="7.2">
                <Axis>
                    <PE ElementType="Gradient" Fill="192, 192, 255"></PE>
                    <X Visible="False" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="0" LineColor="Blue">
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
                    <Y Visible="False" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="40"
                        LineColor="Blue">
                        <MajorGridLines Visible="True" DrawStyle="Dot" Color="Blue" Thickness="1" AlphaLevel="255">
                        </MajorGridLines>
                        <MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255">
                        </MinorGridLines>
                        <Labels ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Visible="False" Font="Verdana, 7pt"
                            FontColor="DimGray" HorizontalAlign="Far" VerticalAlign="Center" Orientation="Horizontal">
                            <SeriesLabels FormatString="" Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Far"
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
                            HorizontalAlign="Far" VerticalAlign="Center" Orientation="Horizontal">
                            <SeriesLabels FormatString="" Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Far"
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
                            FontColor="DimGray" HorizontalAlign="Far" VerticalAlign="Center" Orientation="Horizontal">
                            <SeriesLabels FormatString="" Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Far"
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
                <TitleTop Text="Completed Health Checks for this Month" FontColor="Gray" FontSizeBestFit="True">
                </TitleTop>
                <Border Color="224, 224, 224" CornerRadius="10" Raised="True" Thickness="0"></Border>
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
                <ColorModel ModelStyle="CustomLinear" ColorBegin="Blue" ColorEnd="DarkBlue" AlphaLevel="255">
                    <Skin>
                        <PEs>
                            <igchartprop:PaintElement Fill="White">
                            </igchartprop:PaintElement>
                        </PEs>
                    </Skin>
                </ColorModel>
                <PieChart3D BreakAllSlices="True" BreakAlternatingSlices="True" BreakDistancePercentage="5"
                    PieThickness="10">
                    <ChartText>
                        <igchartprop:ChartTextAppearance ChartTextFont="Arial, 7pt" Column="-2" ItemFormatString="&lt;DATA_VALUE:00&gt;"
                            Row="-2" Visible="True">
                        </igchartprop:ChartTextAppearance>
                    </ChartText>
                    <Labels FormatString="&lt;DATA_VALUE:00.##&gt;" />
                </PieChart3D>
            </igchart:UltraChart>
        </td>
        <td valign="bottom">
            <span id="spanYearly" runat="Server" enableviewstate="False" />
            <igchart:UltraChart ID="YearlyChart" runat="server" DataSourceID="sqlLastYear" Version="7.2"
                PieChart-PieThickness="30" BorderColor="#E0E0E0" BorderWidth="0px" EmptyChartText="No Health Checks for this year"
                BackgroundImageFileName="" ChartType="PieChart3D" Width="300px" Transform3D-Scale="75"
                Transform3D-XRotation="65" Transform3D-YRotation="30" Transform3D-ZRotation="5">
                <Axis>
                    <PE ElementType="Gradient" Fill="192, 192, 255"></PE>
                    <X Visible="False" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="0" LineColor="Blue">
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
                    <Y Visible="False" LineThickness="1" TickmarkStyle="Smart" TickmarkInterval="40"
                        LineColor="Blue">
                        <MajorGridLines Visible="True" DrawStyle="Dot" Color="Blue" Thickness="1" AlphaLevel="255">
                        </MajorGridLines>
                        <MinorGridLines Visible="False" DrawStyle="Dot" Color="LightGray" Thickness="1" AlphaLevel="255">
                        </MinorGridLines>
                        <Labels ItemFormatString="&lt;DATA_VALUE:00.##&gt;" Visible="False" Font="Verdana, 7pt"
                            FontColor="DimGray" HorizontalAlign="Far" VerticalAlign="Center" Orientation="Horizontal">
                            <SeriesLabels FormatString="" Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Far"
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
                            HorizontalAlign="Far" VerticalAlign="Center" Orientation="Horizontal">
                            <SeriesLabels FormatString="" Font="Verdana, 7pt" FontColor="Gray" HorizontalAlign="Far"
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
                            FontColor="DimGray" HorizontalAlign="Far" VerticalAlign="Center" Orientation="Horizontal">
                            <SeriesLabels FormatString="" Font="Verdana, 7pt" FontColor="DimGray" HorizontalAlign="Far"
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
                <TitleTop Text="Completed Health Checks for this Year" FontColor="Gray" FontSizeBestFit="True">
                </TitleTop>
                <Border Color="224, 224, 224" CornerRadius="10" Raised="True" Thickness="0"></Border>
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
                <ColorModel ModelStyle="CustomLinear" ColorBegin="Blue" ColorEnd="DarkBlue" AlphaLevel="255">
                    <Skin>
                        <PEs>
                            <igchartprop:PaintElement Fill="White">
                            </igchartprop:PaintElement>
                        </PEs>
                    </Skin>
                </ColorModel>
                <PieChart3D BreakAllSlices="True" BreakAlternatingSlices="True" BreakDistancePercentage="5"
                    PieThickness="10">
                    <ChartText>
                        <igchartprop:ChartTextAppearance ChartTextFont="Arial, 7pt" Column="-2" ItemFormatString="&lt;DATA_VALUE:00&gt;"
                            Row="-2" Visible="True">
                        </igchartprop:ChartTextAppearance>
                    </ChartText>
                    <Labels FormatString="&lt;DATA_VALUE:00.##&gt;" />
                </PieChart3D>
            </igchart:UltraChart>
        </td>
    </tr>
</table>
<%--<div class="clean-gray" id="compatibility">
    <p style="color: Gray; font-size: 10pt">
        <strong>Summary</strong>
    </p>
    <ul runat="server" id="lstSummary" style="color: Gray; padding-left: 20px">
    </ul>
</div>
<br />--%>
<asp:SqlDataSource ID="sqlLastWeek" runat="server" SelectCommand="SELECT COUNT(Title),Title FROM HealthCheckList HCL INNER JOIN PortfolioHealthCheck PHC ON HCL.PortfolioHealthCheckID=PHC.ID WHERE DATEADD(DD,0,DateRaised)>=DATEADD(DD,1,DATEDIFF(DD,0,DATEADD(DD,-7,DATEADD(DAY,7-DATEPART(dw, GETDATE()), GETDATE())))) AND DateRaised<=GETDATE() AND LOWER(HCL.Status)='completed' AND PortfolioID=@PortfolioID  GROUP BY Title"
    ConnectionString="<%$ ConnectionStrings:DBstring %>" OnSelected="sqlLastWeek_Selected">
    <SelectParameters>
        <asp:SessionParameter Name="PortfolioID" SessionField="PortfolioID" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqlLastMonth" runat="server" SelectCommand="SELECT &#9;COUNT(Title),Title FROM HealthCheckList HCL INNER JOIN PortfolioHealthCheck PHC ON HCL.PortfolioHealthCheckID=PHC.ID WHERE DATEADD(DD,0,DateRaised)>=CAST(CONVERT(VARCHAR(25),dateadd(dd,-day(getdate()-1),getdate()),101) AS DATETIME) AND DATEADD(DD,0,DateRaised)<GETDATE()  AND LOWER(HCL.Status)='completed' AND PortfolioID=@PortfolioID GROUP BY Title"
    ConnectionString="<%$ ConnectionStrings:DBstring %>" OnSelected="sqlLastMonth_Selected">
    <SelectParameters>
        <asp:SessionParameter Name="PortfolioID" SessionField="PortfolioID" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqlLastYear" runat="server" SelectCommand="SELECT 	COUNT(Title),Title FROM HealthCheckList HCL INNER JOIN PortfolioHealthCheck PHC ON HCL.PortfolioHealthCheckID=PHC.ID WHERE DATEADD(DD,0,DateRaised)>=DATEADD(yy, DATEDIFF(yy,0,getdate()),0)AND DATEADD(DD,0,DateRaised)<GETDATE()  AND LOWER(HCL.Status)='completed' AND PortfolioID=@PortfolioID GROUP BY Title"
    ConnectionString="<%$ ConnectionStrings:DBstring %>" OnSelected="sqlLastYear_Selected">
    <SelectParameters>
        <asp:SessionParameter Name="PortfolioID" SessionField="PortfolioID" />
    </SelectParameters>
</asp:SqlDataSource>
