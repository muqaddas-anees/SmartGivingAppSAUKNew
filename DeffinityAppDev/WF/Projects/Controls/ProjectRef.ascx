<%@ Control Language="C#" ClassName="ProjectRef" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        lblPref.InnerText = sessionKeys.Prefix + QueryStringValues.Project.ToString();
        //lblTitle.InnerText = Deffinity.Bindings.DefaultDatabind.GetProjectTitle(QueryStringValues.Project);
    }
  public string ProjectTitle
    {
        get
        {
            return lblPref.InnerText;
        }
        set
        {
           lblPref.InnerText = value;           
        }
    }
   
</script>
		<%--Project Reference:<label id="lblPref" runat="server"></label> --%>
		
		<span class="space_r50 float_l">Project Reference: <b><label id="lblPref" runat="server"></label></b></span> <%--<span class="space_r50 float_l"><div>Project Title:<b><label id="lblTitle" runat="server"></label></b> </div></span>--%>

