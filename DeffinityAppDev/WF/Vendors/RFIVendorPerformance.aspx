<%@ Page Title="" Language="C#" MasterPageFile="~/WF/MainTab.master" AutoEventWireup="true" Inherits="RFIVendorPerformance" Codebehind="RFIVendorPerformance.aspx.cs" %>

<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v7.2, Version=7.2.20072.61, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.UltraWebChart" TagPrefix="igchart" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v7.2, Version=7.2.20072.61, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.UltraChart.Resources.Appearance" TagPrefix="igchartprop" %>
<%@ Register Assembly="Infragistics2.WebUI.UltraWebChart.v7.2, Version=7.2.20072.61, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.UltraChart.Data" TagPrefix="igchartdata" %>


<%@ Register src="controls/RFIVendorMainTabNew.ascx" tagname="RFIVendorTabs" tagprefix="uc1" %>
<%@ Register src="controls/VendorRef.ascx" tagname="RFIVendorRef" tagprefix="uc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Tabs" Runat="Server">
    <uc1:RFIVendorTabs ID="RFIVendorTabs1" runat="server" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table class="data_carrier" width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td>
                <h1 class="section1">
                    <span>Vendor Performance</span>
                </h1>
            </td>
        </tr>
        <tr>
            <td class="p_section1 data_carrier_block">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="sec_header">
                            Use the following section to give the feedback on the Vendors
                        </div>
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="vertical-align: top;">
                                    <div>
                                        <asp:Label ID="lblmsg" runat="server" BackColor="White" ForeColor="Red" Visible="False">Feedback saved</asp:Label>
                                        <table width="100%" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    Vendor
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlVendors" runat="server" DataTextField="ContractorName" DataValueField="VendorID"
                                                        DataSourceID="objds_Vendors" Width="200px" OnSelectedIndexChanged="ddlVendors_SelectedIndexChanged"
                                                        AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    <asp:ObjectDataSource ID="objds_Vendors" runat="server" TypeName="Deffinity.BLL.RFI_Vendor_SVC"
                                                        SelectMethod="Fill"></asp:ObjectDataSource>
                                                </td>
                                                <asp:RequiredFieldValidator ID="R5" runat="server" ValidationGroup="Group1" InitialValue="0"
                                                    ErrorMessage="Select Resource" Display="Dynamic" ControlToValidate="ddlVendors"></asp:RequiredFieldValidator>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Timeliness
                                                </td>
                                                <td>
                                                    <ajaxToolkit:Rating ID="RatingTime" runat="server" WaitingStarCssClass="Saved" StarCssClass="ratingItem"
                                                        MaxRating="5" FilledStarCssClass="Filled" EmptyStarCssClass="Empty" CurrentRating="0"
                                                        CssClass="ratingStar">
                                                    </ajaxToolkit:Rating>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Quality of Workmanship
                                                </td>
                                                <td>
                                                    <ajaxToolkit:Rating ID="RatingQuality" runat="server" WaitingStarCssClass="Saved"
                                                        StarCssClass="ratingItem" MaxRating="5" FilledStarCssClass="Filled" EmptyStarCssClass="Empty"
                                                        CurrentRating="0" CssClass="ratingStar">
                                                    </ajaxToolkit:Rating>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Value for Money
                                                </td>
                                                <td>
                                                    <ajaxToolkit:Rating ID="RatingMoney" runat="server" WaitingStarCssClass="Saved" StarCssClass="ratingItem"
                                                        MaxRating="5" FilledStarCssClass="Filled" EmptyStarCssClass="Empty" CurrentRating="0"
                                                        CssClass="ratingStar">
                                                    </ajaxToolkit:Rating>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Communication
                                                </td>
                                                <td>
                                                    <ajaxToolkit:Rating ID="RatingCommunication" runat="server" WaitingStarCssClass="Saved"
                                                        StarCssClass="ratingItem" MaxRating="5" FilledStarCssClass="Filled" EmptyStarCssClass="Empty"
                                                        CurrentRating="0" CssClass="ratingStar">
                                                    </ajaxToolkit:Rating>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="btnSubmitHours" OnClick="btnSubmitHours_Click" runat="server"
                                                        ValidationGroup="Group1" SkinID="ImgSubmit" OnClientClick="return confirm('Do you want to add the FeedBack?');"></asp:ImageButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                                <td>
                                    <igchart:UltraChart ID="UltraChart1" runat="server" BorderWidth="0px"
                                        ChartType="LineChart" EmptyChartText="Data Not Available. Please call UltraChart.Data.DataBind() after setting valid Data.DataSource"
                                        Version="7.2">
                                        <Axis>
                                            <Z LineThickness="1" TickmarkInterval="0" Visible="False" TickmarkStyle="Smart">
                                                <MinorGridLines Color="LightGray" DrawStyle="Dot" Visible="False" Thickness="1" AlphaLevel="255">
                                                </MinorGridLines>
                                                <MajorGridLines Color="Gainsboro" DrawStyle="Dot" Visible="True" Thickness="1" AlphaLevel="255">
                                                </MajorGridLines>
                                                <Labels Orientation="Horizontal" ItemFormatString="" FontColor="DimGray" HorizontalAlign="Near"
                                                    Visible="False" Font="Verdana, 7pt" VerticalAlign="Center">
                                                    <Layout Behavior="Auto">
                                                    </Layout>
                                                    <SeriesLabels Orientation="Horizontal" FontColor="DimGray" HorizontalAlign="Near"
                                                        Font="Verdana, 7pt" VerticalAlign="Center">
                                                        <Layout Behavior="Auto">
                                                        </Layout>
                                                    </SeriesLabels>
                                                </Labels>
                                            </Z>
                                            <Y2 LineThickness="1" TickmarkInterval="40" Visible="False" TickmarkStyle="Smart">
                                                <MinorGridLines Color="LightGray" DrawStyle="Dot" Visible="False" Thickness="1" AlphaLevel="255">
                                                </MinorGridLines>
                                                <MajorGridLines Color="Gainsboro" DrawStyle="Dot" Visible="True" Thickness="1" AlphaLevel="255">
                                                </MajorGridLines>
                                                <Labels Orientation="Horizontal" ItemFormatString="&lt;DATA_VALUE:00.##&gt;" FontColor="Gray"
                                                    HorizontalAlign="Near" Visible="False" Font="Verdana, 7pt" VerticalAlign="Center">
                                                    <Layout Behavior="Auto">
                                                    </Layout>
                                                    <SeriesLabels Orientation="Horizontal" FormatString="" FontColor="Gray" HorizontalAlign="Near"
                                                        Font="Verdana, 7pt" VerticalAlign="Center">
                                                        <Layout Behavior="Auto">
                                                        </Layout>
                                                    </SeriesLabels>
                                                </Labels>
                                            </Y2>
                                            <X LineThickness="1" TickmarkInterval="0" Visible="True" TickmarkStyle="Smart">
                                                <MinorGridLines Color="LightGray" DrawStyle="Dot" Visible="False" Thickness="1" AlphaLevel="255">
                                                </MinorGridLines>
                                                <MajorGridLines Color="Gainsboro" DrawStyle="Dot" Visible="True" Thickness="1" AlphaLevel="255">
                                                </MajorGridLines>
                                                <Labels Orientation="VerticalLeftFacing" ItemFormatString="&lt;ITEM_LABEL&gt;" FontColor="DimGray"
                                                    HorizontalAlign="Near" Font="Verdana, 7pt" VerticalAlign="Center">
                                                    <Layout Behavior="Auto">
                                                    </Layout>
                                                    <SeriesLabels Orientation="VerticalLeftFacing" FormatString="" FontColor="DimGray"
                                                        HorizontalAlign="Near" Font="Verdana, 7pt" VerticalAlign="Center">
                                                        <Layout Behavior="Auto">
                                                        </Layout>
                                                    </SeriesLabels>
                                                </Labels>
                                            </X>
                                            <Y LineThickness="1" TickmarkInterval="40" Visible="True" Extent="43" TickmarkStyle="Smart">
                                                <MinorGridLines Color="LightGray" DrawStyle="Dot" Visible="False" Thickness="1" AlphaLevel="255">
                                                </MinorGridLines>
                                                <MajorGridLines Color="Gainsboro" DrawStyle="Dot" Visible="True" Thickness="1" AlphaLevel="255">
                                                </MajorGridLines>
                                                <Labels Orientation="Horizontal" ItemFormatString="&lt;DATA_VALUE:00.##&gt;" FontColor="DimGray"
                                                    HorizontalAlign="Far" Font="Verdana, 7pt" VerticalAlign="Center">
                                                    <Layout Behavior="Auto">
                                                    </Layout>
                                                    <SeriesLabels Orientation="Horizontal" FormatString="" FontColor="DimGray" HorizontalAlign="Far"
                                                        Font="Verdana, 7pt" VerticalAlign="Center">
                                                        <Layout Behavior="Auto">
                                                        </Layout>
                                                    </SeriesLabels>
                                                </Labels>
                                            </Y>
                                            <X2 LineThickness="1" TickmarkInterval="0" Visible="False" TickmarkStyle="Smart">
                                                <MinorGridLines Color="LightGray" DrawStyle="Dot" Visible="False" Thickness="1" AlphaLevel="255">
                                                </MinorGridLines>
                                                <MajorGridLines Color="Gainsboro" DrawStyle="Dot" Visible="True" Thickness="1" AlphaLevel="255">
                                                </MajorGridLines>
                                                <Labels Orientation="VerticalLeftFacing" ItemFormatString="&lt;ITEM_LABEL&gt;" FontColor="Gray"
                                                    HorizontalAlign="Far" Visible="False" Font="Verdana, 7pt" VerticalAlign="Center">
                                                    <Layout Behavior="Auto">
                                                    </Layout>
                                                    <SeriesLabels Orientation="VerticalLeftFacing" FormatString="" FontColor="Gray" HorizontalAlign="Far"
                                                        Font="Verdana, 7pt" VerticalAlign="Center">
                                                        <Layout Behavior="Auto">
                                                        </Layout>
                                                    </SeriesLabels>
                                                </Labels>
                                            </X2>
                                            <PE ElementType="None" Fill="Cornsilk"></PE>
                                            <Z2 LineThickness="1" TickmarkInterval="0" Visible="False" TickmarkStyle="Smart">
                                                <MinorGridLines Color="LightGray" DrawStyle="Dot" Visible="False" Thickness="1" AlphaLevel="255">
                                                </MinorGridLines>
                                                <MajorGridLines Color="Gainsboro" DrawStyle="Dot" Visible="True" Thickness="1" AlphaLevel="255">
                                                </MajorGridLines>
                                                <Labels Orientation="Horizontal" ItemFormatString="" FontColor="Gray" HorizontalAlign="Near"
                                                    Visible="False" Font="Verdana, 7pt" VerticalAlign="Center">
                                                    <Layout Behavior="Auto">
                                                    </Layout>
                                                    <SeriesLabels Orientation="Horizontal" FontColor="Gray" HorizontalAlign="Near" Font="Verdana, 7pt"
                                                        VerticalAlign="Center">
                                                        <Layout Behavior="Auto">
                                                        </Layout>
                                                    </SeriesLabels>
                                                </Labels>
                                            </Z2>
                                        </Axis>
                                        <Border Thickness="0"></Border>
                                        <Tooltips Font-Italic="False" Font-Strikeout="False" Font-Underline="False" Font-Overline="False"
                                            Font-Bold="False"></Tooltips>
                                        <Effects>
                                            <Effects>
                                                <igchartprop:GradientEffect>
                                                </igchartprop:GradientEffect>
                                            </Effects>
                                        </Effects>
                                        <ColorModel ModelStyle="CustomLinear" AlphaLevel="150">
                                        </ColorModel>
                                        <Legend Visible="True" Location="Bottom"></Legend>
                                        <Data SwapRowsAndColumns="True" UseRowLabelsColumn="True"></Data>
                                    </igchart:UltraChart>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <table width="900px" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td>
                                                <asp:GridView ID="gridVendorPerformance" AllowPaging="True" AllowSorting="True"
                                                    BackColor="White" EmptyDataText="No Records Exists" Font-Size="X-Small" AutoGenerateColumns="False"
                                                    Font-Names="Verdana" runat="server" OnPageIndexChanging="gridVendorPerformance_PageIndexChanging"
                                                    OnRowEditing="gridVendorPerformance_RowEditing" GridLines="None" OnRowCancelingEdit="gridVendorPerformance_CancelingEdit"
                                                    OnRowCommand="gridVendorPerformance_RowCommand" OnRowDeleting="gridVendorPerformance_RowDeleting"
                                                    OnRowUpdating="gridVendorPerformance_RowUpdating">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="" HeaderStyle-CssClass="header_bg_l" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnedit" runat="server" CausesValidation="false" CommandName="Edit"
                                                                    ImageUrl='media/ico_edit.png' ToolTip="Edit" CommandArgument='<%# Bind("ID") %> ' />
                                                                <asp:Label ID="lblID1" runat="server" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:ImageButton ID="btnupdate" runat="server" CommandName="Update" ImageUrl='media/ico_update.png'
                                                                    CommandArgument='<%# Bind("ID") %> ' ToolTip="Update" ValidationGroup="ValEdit" />
                                                                <asp:ImageButton ID="btncancel" runat="server" CausesValidation="false" CommandName="cancel"
                                                                    ImageUrl='media/ico_cancel.png' ToolTip="Cancel" />
                                                            </EditItemTemplate>
                                                            <HeaderStyle CssClass="header_bg_l" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Vendor" HeaderStyle-CssClass="header_bg_l">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblVendor" Text='<%# Eval("VENDOR") %>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Timeliness" ItemStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTimeliness" Text='<%# Eval("Timeliness")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Quality Of Work" ItemStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblQualityOfWork" Text='<%# Eval("QualityOfWork")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Value For Money" ItemStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblValueForMoney" Text='<%# Eval("ValueForMoney")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Communication" ItemStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCommunication" Text='<%# Eval("Communication")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Assigned By">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAssignedBy" Text='<%# Eval("ASSIGNEDBY")%>' runat="server"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Submitted Date">
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval(Container.DataItem, "SubmittedDate", "{0:d}")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-CssClass="header_bg_r">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="DeleteItem" ImageUrl="~/images/icon_delete.png" CommandName="Dissolve"
                                                                    CommandArgument='<%# Eval("ID")%>' runat="server" OnClientClick="return confirm('Do you want to delete the FeedBack?');"/>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="6%" />
                                                            <ItemStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger EventName="SelectedIndexChanged" ControlID="ddlVendors" />
                    </Triggers>
                </asp:UpdatePanel>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                    <ProgressTemplate>
                        <div style="font-weight: bold; left: 40%; padding-top: 26%; color: white; font-family: Arial;
                            position: absolute; top: 2px; border-top-width: thin; border-left-width: thin;
                            border-left-color: black; border-bottom-width: thin; border-bottom-color: black;
                            border-top-color: black; border-right-width: thin; border-right-color: black;">
                            <img src="media/ico_loading.gif" alt="loading" style="background-color: #F3F1E7;" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </td>
        </tr>
        
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ProgressImage" Runat="Server">
</asp:Content>

