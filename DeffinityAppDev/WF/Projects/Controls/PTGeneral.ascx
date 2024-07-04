<%@ Control Language="C#" AutoEventWireup="true" Inherits="controls_ProjectTracker_General" Codebehind="PTGeneral.ascx.cs" %>
<style>
    /*.roundGreen
    {
        border: 0px solid Silver;
        padding: 5px 5px;
        background: #E6F7E6;
        width: 100%;
        border-radius: 8px;
        margin-bottom:5px;
    }
    .roundViolet
    {
        border: 0px solid Silver;
        padding: 5px 5px;
        background: #DBE3F7;
        width: 100%;
        border-radius: 8px;
        margin-bottom:5px;
    }
    .roundOrange
    {
        border: 0px solid Silver;
        padding: 5px 5px;
        background: #FFF3E8;
        width: 100%;
        border-radius: 8px;
         margin-bottom:5px;
    }
    .roundDarkGreen
    {
        border: 0px solid Silver;
        padding: 5px 5px;
        background: #E7F3F1;
        width: 100%;
        border-radius: 8px;
        margin-bottom:5px;
    }
    .roundPink
    {
        border: 0px solid Silver;
        padding: 5px 5px;
        background: #FAE7EC;
        width: 100%;
        border-radius: 8px;
        margin-bottom:5px;
    }*/
</style>
<div class="row">
     <div class="col-md-12">
         &nbsp;
         </div>
    </div>
<div class="row">
   
<div class="col-md-5">
<div id="ProjectFinancial">
    <div class="form-group">
        <div class="col-md-12 text-bold no-bottom-padding">
        <strong>  <%= Resources.DeffinityRes.ProjectFinancials%> </strong>
            <hr class="hr-group-no-margin" />
            </div>
    </div>
    
        <div class="form-group well">
      <div class="col-md-12">
             <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label"> BUDGET SALES VALUE:</label>
                                      <div class="col-sm-4 control-label"> <asp:TextBox ID="txtOriginalSalesValue" runat="server" ValidationGroup="ProjectValues1"
                            SkinID="Price" ReadOnly="True"></asp:TextBox>
					</div>
				</div>
                </div>
             <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label">  GP:</label>
                                      <div class="col-sm-4 control-label "> <asp:TextBox ID="txtGP" runat="server" ReadOnly="True" Enabled="False" SkinID="Price"></asp:TextBox>
					</div>
				</div>
                </div>
             <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label">ORIGINAL PROFIT:</label>
                                      <div class="col-sm-3 control-label pull-right"><asp:Label ID="lblOriginalProfit" runat="server" SkinID="Price"></asp:Label>
					</div>
				</div>
                </div>
           
        </div>
            </div>
      
        <div class="form-group well">
      <div class="col-md-12">
             <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label">Variations:</label>
                                      <div class="col-sm-3 control-label pull-right"><asp:Label ID="lblNewVariations" runat="server" SkinID="Price"></asp:Label>
					</div>
				</div>
                </div>
        </div>
            </div>
       
       <div class="form-group well">
      <div class="col-md-12">
             <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label">CURRENT SALES VALUE:</label>
                                      <div class="col-sm-3 control-label pull-right"><asp:Label ID="lblCurrentSalesValue" runat="server" SkinID="Price"></asp:Label>
					</div>
				</div>
                </div>
            
             <div class="form-group">
             <div class="col-md-12">
                                       <div class="col-sm-7 control-label form-inline">  Minus MCD @&nbsp;<asp:TextBox ID="txtMinusMCD" runat="server" SkinID="Price_50px"></asp:TextBox>%&nbsp;<span style="vertical-align:baseline;"> <asp:LinkButton
                            ID="imgMinusMCD" runat="server" SkinID="BtnLinkIndent"
                            CausesValidation="false" OnClick="imgMinusMCD_Click" /></span></div>
                                      <div class="col-sm-3 control-label pull-right"> <asp:Label ID="lblMinusMCD" runat="server"></asp:Label>
					</div>
				</div>
                </div>
             <div class="form-group">
             <div class="col-md-12">
                                       <div class="col-sm-7 "> Net Sales:</div>
                                      <div class="col-sm-3 control-label pull-right"> <asp:Label ID="lblNetSales" runat="server" SkinID="Price"></asp:Label>
					</div>
				</div>
                </div>
            
       </div>
       </div>
        <div class="form-group well">
      <div class="col-md-12">
             <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label"> Forecast Cost to Complete:</label>
                                      <div class="col-sm-3 control-label pull-right"><asp:Label ID="lblForecastCostToComplete" runat="server" SkinID="Price"></asp:Label>
					</div>
				</div>
                </div>
             <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label"> Spent to date:</label>
                                      <div class="col-sm-3 control-label pull-right"> <asp:Label ID="lblSpentToDate" runat="server" SkinID="Price"></asp:Label>
					</div>
				</div>
                </div>
       </div>
       </div>
       <div class="form-group well">
      <div class="col-md-12">
             <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label"> FORECAST SALES VALUE:</label>
                                      <div class="col-sm-3 control-label pull-right"><asp:Label ID="lblForecastSalesValues" runat="server" SkinID="Price"></asp:Label>
					</div>
				</div>
                </div>
             <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label"> FORECAST COST VALUE:</label>
                                      <div class="col-sm-3 control-label pull-right"> <asp:Label ID="lblForecastCostValue" runat="server" SkinID="Price"></asp:Label>
					</div>
				</div>
                </div>
             <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label"> FORECAST GP (%):</label>
                                      <div class="col-sm-3 control-label pull-right"><asp:Label ID="lblForecastGP" runat="server" SkinID="Price"></asp:Label>
					</div>
				</div>
                </div>
             <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label"> FORECAST PROFIT:</label>
                                      <div class="col-sm-3 control-label pull-right">
                                          <asp:Label ID="lblForecastProfit" runat="server" SkinID="Price"></asp:Label>
					</div>
				</div>
                </div>
        </div>
           </div>
    
                                    
    </div>
    
   
   
       <div class="form-group well">
      <div class="col-md-12">
           
            <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label"><%= Resources.DeffinityRes.TotalBonuses%>:</label>
                                      <div class="col-sm-3 control-label pull-right">
                                          <asp:Label ID="lblBonuses" runat="server" SkinID="Price"></asp:Label>
					</div>
				</div>
                </div>
            <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label">  <%= Resources.DeffinityRes.TotalAbatements%>:</label>
                                      <div class="col-sm-3 control-label pull-right">
                                          <asp:Label ID="lblAbatements" runat="server" SkinID="Price"></asp:Label>
					</div>
				</div>
                </div>
           
        </div></div>
       
      
   
   </div>

    <div class="col-md-1">
        </div>
    <div class="col-md-5">
         <div class="form-group">
        <div class="col-md-12 text-bold">
        <strong> Variations Summary</strong>
            <hr class="hr-group-no-margin" />
            </div>
    </div>
   
         <div class="form-group well">
             <div class="col-md-12">
         <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label"><%= Resources.DeffinityRes.TotalvariationsApproved%>:</label>
                                      <div class="col-sm-3 control-label pull-right">
                                          <asp:Label ID="lblVariations" runat="server" Width="80px" Style="text-align: right"></asp:Label>
					</div>
				</div>
                </div>
            <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label">  <%= Resources.DeffinityRes.TotalvariationsUnApproved%>:</label>
                                      <div class="col-sm-3 control-label pull-right">
                                           <asp:Label ID="lblVariationsUnapproved" runat="server" Width="80px" Style="text-align: right"></asp:Label>
					</div>
				</div>
                </div>
            <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label"> <%= Resources.DeffinityRes.Totalcostvariations%>:</label>
                                      <div class="col-sm-3 control-label pull-right"><asp:Label ID="lblIndirectcost" runat="server" Width="80px" Style="text-align: right"></asp:Label>
					</div>
				</div>
                </div>
             </div>
             </div>
      
      <div class="form-group">
        <div class="col-md-12 text-bold">
        <strong>  Invoiced Summary</strong>
            <hr class="hr-group-no-margin" />
            </div>
    </div>
      
        <div class="form-group well">
      <div class="col-md-12">
            <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label"><%= Resources.DeffinityRes.Invoicedtodate%>:</label>
                                      <div class="col-sm-3 control-label pull-right">
                                          <asp:Label ID="lblInvoiced" runat="server" Width="80px" Style="text-align: right"></asp:Label>
					</div>
				</div>
                </div>
            <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label"> <%= Resources.DeffinityRes.Outstandingtobeinvoiced%>:</label>
                                      <div class="col-sm-3 control-label pull-right">
                                          <asp:Label ID="lblOutStanding" runat="server" Width="80px" Style="text-align: right"></asp:Label>
					</div>
				</div>
                </div>

           
        </div></div>
      
        <div class="form-group">
        <div class="col-md-12 text-bold">
        <strong>  Current Actual Costs </strong>
            <hr class="hr-group-no-margin" />
            </div>
    </div>
       <div class="form-group well">
      <div class="col-md-12">
            <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label"> Actual project cost to date:</label>
                                      <div class="col-sm-3 control-label pull-right"><asp:Label ID="lblactualProjectCosttodate" runat="server" Width="80px" Style="text-align: right"></asp:Label>
					</div>
				</div>
                </div>
             <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label">  Actual hours booked:</label>
                                      <div class="col-sm-3 control-label pull-right"> <asp:Label ID="lblactualhours" runat="server" Width="80px" Style="text-align: right"></asp:Label>
					</div>
				</div>
                </div>
            
        </div></div>
          <div class="form-group">
        <div class="col-md-12 text-bold">
        <strong>  Credit Note</strong>
            <hr class="hr-group-no-margin" />
            </div>
    </div>
          <div class="form-group well">
      <div class="col-md-12">
            <div class="form-group">
             <div class="col-md-12">
                                       <label class="col-sm-7 control-label"> Credit note total:</label>
                                      <div class="col-sm-3 control-label pull-right"> <asp:Label ID="lblCreditnote" runat="server" Width="80px" Style="text-align: right"></asp:Label>
					</div>
				</div>
                </div>
          </div>
              </div>
         
    </div>
    </div>
    