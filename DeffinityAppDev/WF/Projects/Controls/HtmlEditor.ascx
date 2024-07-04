<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_HtmlEditor" Codebehind="HtmlEditor.ascx.cs" %>
<%@ Register assembly="Infragistics2.WebUI.WebHtmlEditor.v7.2" namespace="Infragistics.WebUI.WebHtmlEditor" tagprefix="ighedit" %>
<ighedit:WebHtmlEditor ID="WebHtmlEditor1" runat="server"  style="margin-bottom:0px;padding-bottom:0px;" CssClass="nostyle" RightClickBehavior="Nothing" TabStripDisplay="false" BackColor="#e1e1e1" ImageDirectory="~/HtmlEditor/">
         <Toolbar CssClass="nostyletext" style="margin-bottom:0px;padding-bottom:0px;padding-left:1px;margin-left:2px;margin-right:1px">
                                    <ighedit:ToolbarImage Type="DoubleSeparator"></ighedit:ToolbarImage>
                                    <ighedit:ToolbarButton Type="Bold"></ighedit:ToolbarButton>
                                    <ighedit:ToolbarButton Type="Italic"></ighedit:ToolbarButton>
                                    <ighedit:ToolbarButton Type="Underline"></ighedit:ToolbarButton>
                                    <ighedit:ToolbarButton Type="Strikethrough"></ighedit:ToolbarButton>
                                    <ighedit:ToolbarImage Type="Separator"></ighedit:ToolbarImage>
                                    <ighedit:ToolbarButton Type="Subscript"></ighedit:ToolbarButton>
                                    <ighedit:ToolbarButton Type="Superscript"></ighedit:ToolbarButton>
                                    <ighedit:ToolbarImage Type="Separator"></ighedit:ToolbarImage>
                                    <ighedit:ToolbarButton Type="Cut"></ighedit:ToolbarButton>
                                    <ighedit:ToolbarButton Type="Copy"></ighedit:ToolbarButton>
                                    <ighedit:ToolbarButton Type="Paste"></ighedit:ToolbarButton>
                                    <ighedit:ToolbarImage Type="Separator"></ighedit:ToolbarImage>
                                    <ighedit:ToolbarButton Type="Undo"></ighedit:ToolbarButton>
                                    <ighedit:ToolbarButton Type="Redo"></ighedit:ToolbarButton>
                                    <ighedit:ToolbarImage Type="Separator"></ighedit:ToolbarImage>
                                    <ighedit:ToolbarButton Type="JustifyLeft"></ighedit:ToolbarButton>
                                    <ighedit:ToolbarButton Type="JustifyCenter"></ighedit:ToolbarButton>
                                    <ighedit:ToolbarButton Type="JustifyRight"></ighedit:ToolbarButton>
                                    <ighedit:ToolbarButton Type="JustifyFull"></ighedit:ToolbarButton>
                                    <ighedit:ToolbarImage Type="Separator" ></ighedit:ToolbarImage>
                                    <ighedit:ToolbarButton Type="Indent"></ighedit:ToolbarButton>
                                    <ighedit:ToolbarButton Type="Outdent"></ighedit:ToolbarButton>
                                    <ighedit:ToolbarImage Type="Separator"></ighedit:ToolbarImage>
                                    <ighedit:ToolbarButton Type="UnorderedList"></ighedit:ToolbarButton>
                                    <ighedit:ToolbarButton Type="OrderedList"></ighedit:ToolbarButton>
                                    <ighedit:ToolbarImage Type="Separator"></ighedit:ToolbarImage>
                                    <ighedit:ToolbarDialogButton Type="FontColor">
                                        <Dialog BorderStyle="Solid" BackColor="#ECE9D8" ForeColor="Black" Strings="" BorderWidth="1px" BorderColor="Black" Font-Size="8pt" Font-Names="sans-serif"></Dialog>
                                    </ighedit:ToolbarDialogButton>
                                    <ighedit:ToolbarDialogButton Type="FontHighlight">
                                        <Dialog BorderStyle="Solid" BackColor="#ECE9D8" ForeColor="Black" Strings="" BorderWidth="1px" BorderColor="Black" Font-Size="8pt" Font-Names="sans-serif"></Dialog>
                                    </ighedit:ToolbarDialogButton>
                                    <ighedit:ToolbarImage Type="Separator"></ighedit:ToolbarImage>
                                    <ighedit:ToolbarDialogButton Type="SpecialCharacter">
                                        <Dialog Strings="" InternalDialogType="SpecialCharacterPicker" Type="InternalWindow"></Dialog>
                                    </ighedit:ToolbarDialogButton>
                                     <ighedit:ToolbarMenuButton Type="InsertTable">
                                        <Menu Width="80px">
                                            <ighedit:HtmlBoxMenuItem Act="TableProperties">
                                                <Dialog Strings="" InternalDialogType="InsertTable"></Dialog>
                                            </ighedit:HtmlBoxMenuItem>
                                            <ighedit:HtmlBoxMenuItem Act="InsertColumnRight"></ighedit:HtmlBoxMenuItem>
                                            <ighedit:HtmlBoxMenuItem Act="InsertColumnLeft"></ighedit:HtmlBoxMenuItem>
                                            <ighedit:HtmlBoxMenuItem Act="InsertRowAbove"></ighedit:HtmlBoxMenuItem>
                                            <ighedit:HtmlBoxMenuItem Act="InsertRowBelow"></ighedit:HtmlBoxMenuItem>
                                            <ighedit:HtmlBoxMenuItem Act="DeleteRow"></ighedit:HtmlBoxMenuItem>
                                            <ighedit:HtmlBoxMenuItem Act="DeleteColumn"></ighedit:HtmlBoxMenuItem>
                                            <ighedit:HtmlBoxMenuItem Act="IncreaseColspan"></ighedit:HtmlBoxMenuItem>
                                            <ighedit:HtmlBoxMenuItem Act="DecreaseColspan"></ighedit:HtmlBoxMenuItem>
                                            <ighedit:HtmlBoxMenuItem Act="IncreaseRowspan"></ighedit:HtmlBoxMenuItem>
                                            <ighedit:HtmlBoxMenuItem Act="DecreaseRowspan"></ighedit:HtmlBoxMenuItem>
                                            <ighedit:HtmlBoxMenuItem Act="CellProperties">
                                                <Dialog Strings="" InternalDialogType="CellProperties"></Dialog>
                                            </ighedit:HtmlBoxMenuItem>
                                            <ighedit:HtmlBoxMenuItem Act="TableProperties">
                                                <Dialog Strings="" InternalDialogType="ModifyTable"></Dialog>
                                            </ighedit:HtmlBoxMenuItem>
                                        </Menu>
                                    </ighedit:ToolbarMenuButton>
                                    <ighedit:ToolbarButton Type="ToggleBorders"></ighedit:ToolbarButton>
                                     <ighedit:ToolbarImage Type="Separator"></ighedit:ToolbarImage>
                                    <ighedit:ToolbarButton Type="InsertLink"></ighedit:ToolbarButton>
                                    <ighedit:ToolbarButton Type="RemoveLink"></ighedit:ToolbarButton>
                                    <ighedit:ToolbarImage Type="RowSeparator"></ighedit:ToolbarImage>
                                    <ighedit:ToolbarImage Type="DoubleSeparator"></ighedit:ToolbarImage>
                                    <ighedit:ToolbarButton Type="Preview"></ighedit:ToolbarButton>
                                    <ighedit:ToolbarDialogButton Type="FindReplace">
                                        <Dialog Strings="" InternalDialogType="FindReplace"></Dialog>
                                    </ighedit:ToolbarDialogButton>
                                    <ighedit:ToolbarButton Type="TogglePositioning"></ighedit:ToolbarButton>
                                    <ighedit:ToolbarImage Type="Separator"></ighedit:ToolbarImage>
                                    <ighedit:ToolbarDropDown Type="FontName"></ighedit:ToolbarDropDown>
                                    <ighedit:ToolbarDropDown Type="FontSize"></ighedit:ToolbarDropDown>
                                    <ighedit:ToolbarDropDown Type="FontFormatting"></ighedit:ToolbarDropDown>
                                    <ighedit:ToolbarDropDown Type="FontStyle"></ighedit:ToolbarDropDown>
                                </Toolbar>
       <TextWindow />
       <TabStrip style="margin-bottom:-7px;padding-bottom:0px;" />
         </ighedit:WebHtmlEditor>