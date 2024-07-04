using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for Asset
/// </summary>
public class DetailsViewTemplate:ITemplate
{
    //A variable to hold the type of ListItemType.
    ListItemType _templateType;

    //A variable to hold the column name.
    string _columnName;
 
    //Constructor where we define the template type and column name.
    public DetailsViewTemplate(ListItemType type, string colname)
    {
        //Stores the template type.
        _templateType = type;

        //Stores the column name.
        _columnName = colname;
    }

    void ITemplate.InstantiateIn(System.Web.UI.Control container)
    {
        switch (_templateType)
        {
            case ListItemType.Header:
                //Creates a new label control and add it to the container.
                Label lbl = new Label();            //Allocates the new label object.
                lbl.Text = _columnName;             //Assigns the name of the column in the lable.
                container.Controls.Add(lbl);        //Adds the newly created label control to the container.
                break;

            case ListItemType.Item:
                //Creates a new text box control and add it to the container.
                Image img = new Image();
                img.DataBinding += new EventHandler(tb1_DataBinding);   
                container.Controls.Add(img);                            
                break;

            case ListItemType.EditItem:
                //As, I am not using any EditItem, I didnot added any code here.
                break;

            case ListItemType.Footer:
                CheckBox chkColumn = new CheckBox();
                chkColumn.ID = "Chk" + _columnName;
                container.Controls.Add(chkColumn);
                break;
        }
    }

    /// <summary>
    /// This is the event, which will be raised when the binding happens.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void tb1_DataBinding(object sender, EventArgs e)
    {
        Image imgData = (Image)sender;
        DetailsView container = (DetailsView)imgData.NamingContainer;
        object dataValue = DataBinder.Eval(container.DataItem, _columnName);
        if (dataValue != DBNull.Value)
        {
            imgData.ImageUrl = string.Format("~/Asset.SingleImage.ashx?RecordID={0}", dataValue);
        }
    }
}
