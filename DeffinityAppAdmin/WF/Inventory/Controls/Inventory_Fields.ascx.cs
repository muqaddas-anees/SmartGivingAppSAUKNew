using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventoryMgt.Entity;
using InventoryMgt.DAL;

public partial class controls_Inventory_Fields : System.Web.UI.UserControl
{
    PortfolioRepository<PortfolioMgt.Entity.ProjectPortfolio> IC = null;
    InventoryRepository<InventoryMgt.Entity.Inventory_Area> IA = null;
    InventoryRepository<InventoryMgt.Entity.Inventory_Locatin> IL = null;
    InventoryRepository<InventoryMgt.Entity.Inventory_Shelf> IS = null;
    InventoryRepository<InventoryMgt.Entity.Inventory_Bin> IB = null;
    InventoryRepository<InventoryMgt.Entity.InventoryManager> IM = null;
    InventoryRepository<InventoryFieldsConfig> INF = null;
    InventoryFieldsConfig in_f = null;
    Inventory_Area In_A = null;
    Inventory_Locatin In_L = null;
    Inventory_Shelf In_s = null;
    Inventory_Bin In_B = null;
    InventoryManager In_m = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCustomer();
            BindAreaByCustomer();
            //ddlarea.Items.Insert(0, new ListItem("Please select", "0"));
            //ddllocation.Items.Insert(0, new ListItem("Please select", "0"));
            //ddlbin.Items.Insert(0, new ListItem("Please select", "0"));
            //ddlshelf.Items.Insert(0, new ListItem("Please select", "0"));
        }
    }
    public void BindCustomer()
    {
        try
        {
            IC = new PortfolioRepository<PortfolioMgt.Entity.ProjectPortfolio>();
            var iclist = IC.GetAll().OrderBy(a => a.PortFolio).ToList();
            ddlcustomer.DataSource = iclist;
            ddlcustomer.DataTextField = "PortFolio";
            ddlcustomer.DataValueField = "ID";
            ddlcustomer.DataBind();
            ddlcustomer.Items.Insert(0, new ListItem("Please select", "0"));
            if(sessionKeys.PortfolioID >0)
            {
                ddlcustomer.SelectedValue = sessionKeys.PortfolioID.ToString();
            }

            ddlarea.Items.Clear();
            ddlarea.Items.Insert(0, new ListItem("Please select", "0"));
            ddllocation.Items.Clear();
            ddllocation.Items.Insert(0, new ListItem("Please select", "0"));
            ddlshelf.Items.Clear();
            ddlshelf.Items.Insert(0, new ListItem("Please select", "0"));
            ddlbin.Items.Clear();
            ddlbin.Items.Insert(0, new ListItem("Please select", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void BindArea(int cid)
    {
        try
        {
            IA = new InventoryRepository<InventoryMgt.Entity.Inventory_Area>();
            ddlarea.DataSource = IA.GetAll().Where(a => a.Cid == cid).OrderBy(a => a.Name).ToList();
            ddlarea.DataTextField = "Name";
            ddlarea.DataValueField = "id";
            ddlarea.DataBind();
            ddlarea.Items.Insert(0, new ListItem("Please select", "0"));

            ddllocation.Items.Clear();
            ddllocation.Items.Insert(0, new ListItem("Please select", "0"));
            ddlshelf.Items.Clear();
            ddlshelf.Items.Insert(0, new ListItem("Please select", "0"));
            ddlbin.Items.Clear();
            ddlbin.Items.Insert(0, new ListItem("Please select", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void BindLocation(int Aid)
    {
        try
        {
            IL = new InventoryRepository<InventoryMgt.Entity.Inventory_Locatin>();
            ddllocation.DataSource = IL.GetAll().Where(a => a.IA_id == Aid).OrderBy(a => a.Name).ToList();
            ddllocation.DataTextField = "Name";
            ddllocation.DataValueField = "id";
            ddllocation.DataBind();
            ddllocation.Items.Insert(0, new ListItem("Please select", "0"));

            ddlshelf.Items.Clear();
            ddlshelf.Items.Insert(0, new ListItem("Please select", "0"));
            ddlbin.Items.Clear();
            ddlbin.Items.Insert(0, new ListItem("Please select", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void BindShelf(int Lid)
    {
        try
        {
            IS = new InventoryRepository<InventoryMgt.Entity.Inventory_Shelf>();
            ddlshelf.DataSource = IS.GetAll().Where(a => a.IL_id == Lid).OrderBy(a => a.Name).ToList();
            ddlshelf.DataTextField = "Name";
            ddlshelf.DataValueField = "id";
            ddlshelf.DataBind();
            ddlshelf.Items.Insert(0, new ListItem("Please select", "0"));

            ddlbin.Items.Clear();
            ddlbin.Items.Insert(0, new ListItem("Please select", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    public void BindBin(int Sid)
    {
        try
        {
            IB = new InventoryRepository<InventoryMgt.Entity.Inventory_Bin>();
            ddlbin.DataSource = IB.GetAll().Where(a => a.IS_id == Sid).OrderBy(a => a.Name).ToList();
            ddlbin.DataTextField = "Name";
            ddlbin.DataValueField = "id";
            ddlbin.DataBind();
            ddlbin.Items.Insert(0, new ListItem("Please select", "0"));
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    private void ControlsVisibility(bool setvalue)
    {
        AreaCheckBox.Checked = setvalue;
        LocationCheckBox.Checked = setvalue;
        ShelfCheckBox.Checked = setvalue;
        BinChechBox.Checked = setvalue;
    }
    public void VisibilityChecking()
    {
        try
        {
            INF = new InventoryRepository<InventoryFieldsConfig>();
            var xList = INF.GetAll().Where(a => a.CustomerId == int.Parse(ddlcustomer.SelectedValue) && a.IsVisible == false).ToList();
            ControlsVisibility(true);
            foreach (var x in xList)
            {
                if (x.DefaultName == "Area")
                {
                    ControlsVisibility(false);
                }
                else if (x.DefaultName == "Location")
                {
                    LocationCheckBox.Checked = false;
                    ShelfCheckBox.Checked = false;
                    BinChechBox.Checked = false;
                }
                else if (x.DefaultName == "Shelf")
                {
                    ShelfCheckBox.Checked = false;
                    BinChechBox.Checked = false;
                }
                else if (x.DefaultName == "Bin")
                {
                    BinChechBox.Checked = false;
                }
                //else if (x.DefaultName == "Site")
                //{
                //    lblSite.Visible = false;
                //    ddlSite.Visible = false;
                //}
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void ddlcustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAreaByCustomer();
    }

    private void BindAreaByCustomer()
    {
        if (ddlcustomer.SelectedValue != "0")
        {
            BindArea(int.Parse(ddlcustomer.SelectedValue));
            VisibilityChecking();
        }
        else
        {
            ddlarea.Items.Clear();
            ddlarea.Items.Insert(0, new ListItem("Please select", "0"));

        }
    }
    protected void ddlarea_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlarea.SelectedValue != "0")
        {
            BindLocation(int.Parse(ddlarea.SelectedValue));
        }
        else
        {
            ddllocation.Items.Clear();
            ddllocation.Items.Insert(0, new ListItem("Please select", "0"));
        }
    }
    protected void ddllocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddllocation.SelectedValue != "0")
        {
            BindShelf(int.Parse(ddllocation.SelectedValue));
        }
        else
        {
            ddlshelf.Items.Clear();
            ddlshelf.Items.Insert(0, new ListItem("Please select", "0"));
        }
    }
    protected void ddlshelf_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlshelf.SelectedValue != "0")
        {
            BindBin(int.Parse(ddlshelf.SelectedValue));
        }
        else
        {
            ddlbin.Items.Clear();
            ddlbin.Items.Insert(0, new ListItem("Please select", "0"));
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int j = 111;
            IA = new InventoryRepository<InventoryMgt.Entity.Inventory_Area>();
            IL = new InventoryRepository<Inventory_Locatin>();
            IS = new InventoryRepository<Inventory_Shelf>();
            IB = new InventoryRepository<Inventory_Bin>();
            if (hd.Value == "area")
            {
                if (txtbox.Text.Trim() != string.Empty)
                {
                    j = IA.GetAll().Where(o => o.Name == txtbox.Text.Trim() && o.Cid == int.Parse(ddlcustomer.SelectedValue)).Count();
                    if (j == 0)
                    {
                        In_A = new Inventory_Area();
                        In_A.Cid = int.Parse(ddlcustomer.SelectedValue);
                        In_A.Name = txtbox.Text.Trim();
                        IA.Add(In_A);
                        BindArea(int.Parse(ddlcustomer.SelectedValue));
                        ddlarea.Items.FindByText(txtbox.Text.Trim()).Selected = true;
                        lblmsg.ForeColor = System.Drawing.Color.Green;
                        lblmsg.Text = "Area added successfully.";
                        popupAdd.Hide();
                        txtbox.Text = string.Empty;
                    }
                    else
                    {
                        txtbox.Text = string.Empty;
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                        lblmsg.Text = "Already exist with this name.";
                    }
                }
                else
                {
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblmsg.Text = "Please enter area name.";
                }
            }
            else if (hd.Value == "location")
            {
                if (txtbox.Text.Trim() != string.Empty)
                {
                    j = IL.GetAll().Where(o => o.Name == txtbox.Text.Trim() && o.IA_id==int.Parse(ddlarea.SelectedValue)).Count();
                    if (j == 0)
                    {
                        In_L = new Inventory_Locatin();
                        In_L.IA_id = int.Parse(ddlarea.SelectedValue);
                        In_L.Name = txtbox.Text;
                        IL.Add(In_L);
                        BindLocation(int.Parse(ddlarea.SelectedValue));
                        ddllocation.Items.FindByText(txtbox.Text).Selected = true;
                        lblmsg.ForeColor = System.Drawing.Color.Green;
                        lblmsg.Text = "Location added successfully.";
                        txtbox.Text = string.Empty;
                    }
                    else
                    {
                        txtedit.Text = string.Empty;
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                        lblmsg.Text = "Already exist with this name.";
                    }
                }
                else
                {
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblmsg.Text = "Please enter location name.";
                }
            }
            else if (hd.Value == "shelf")
            {
                if (txtbox.Text.Trim() != string.Empty)
                {
                    j = IS.GetAll().Where(o => o.Name == txtbox.Text.Trim() && o.IL_id == int.Parse(ddllocation.SelectedValue)).Count();
                    if (j == 0)
                    {
                        In_s = new Inventory_Shelf();
                        In_s.IL_id = int.Parse(ddllocation.SelectedValue);
                        In_s.Name = txtbox.Text;
                        IS.Add(In_s);
                        BindShelf(int.Parse(ddllocation.SelectedValue));
                        ddlshelf.Items.FindByText(txtbox.Text).Selected = true;
                        lblmsg.ForeColor = System.Drawing.Color.Green;
                        lblmsg.Text = "Shelf added successfully.";
                        txtbox.Text = string.Empty;
                    }
                    else
                    {
                        txtedit.Text = string.Empty;
                        lblmsg.ForeColor = System.Drawing.Color.Green;
                        lblmsg.Text = "Already exist with this name.";
                    }
                }
                else
                {
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblmsg.Text = "Please enter shelf name.";
                }
            }
            else if (hd.Value == "bin")
            {
                if (txtbox.Text.Trim() != string.Empty)
                {
                    j = IB.GetAll().Where(o => o.Name == txtbox.Text.Trim() && o.IS_id == int.Parse(ddlshelf.SelectedValue)).Count();
                    if (j == 0)
                    {
                        In_B = new Inventory_Bin();
                        In_B.IS_id = int.Parse(ddlshelf.SelectedValue);
                        In_B.Name = txtbox.Text;
                        IB.Add(In_B);
                        BindBin(int.Parse(ddlshelf.SelectedValue));
                        ddlbin.Items.FindByText(txtbox.Text).Selected = true;
                        lblmsg.ForeColor = System.Drawing.Color.Green;
                        lblmsg.Text = "Bin added successfully.";
                        txtbox.Text = string.Empty;
                    }
                    else
                    {
                        txtedit.Text = string.Empty;
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                        lblmsg.Text = "Already exist with this name.";
                    }
                }
                else
                {
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblmsg.Text = "Please enter bin name.";
                }
            }
            else
            {
                //add customer here 
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imb_Addcustomer_Click(object sender, EventArgs e)
    {
       // popupAdd.Show();
    }
    protected void imb_Addarea_Click(object sender, EventArgs e)
    {
        if (ddlcustomer.SelectedValue != "0")
        {
            hd.Value = "area";
            txtbox.Focus();
            popupAdd.Show();
            lbladd.Text = "Add Area";
        }
        else
        {
            lblmsg.Text = "please select customer";
        }
    }
    protected void imb_AddLocation_Click(object sender, EventArgs e)
    {
        if (ddlarea.SelectedValue != "0")
        {
            hd.Value = "location";
            popupAdd.Show();
            txtbox.Focus();
            lbladd.Text = "Add Location";
        }
        else
        {
            lblmsg.Text = "please select area";
        }
    }
    protected void imb_AddShelf_Click(object sender, EventArgs e)
    {
        if (ddllocation.SelectedValue != "0")
        {
            hd.Value = "shelf";
            txtbox.Focus();
            popupAdd.Show();
            lbladd.Text = "Add Shelf";
        }
        else
        {
          lblmsg.Text = "please select Location";
        }
    }
    protected void imb_AddBin_Click(object sender, EventArgs e)
    {
        if(ddlshelf.SelectedValue!="0")
        {
            hd.Value = "bin";
            popupAdd.Show();
            txtbox.Focus();
            lbladd.Text = "Add Bin";
        }
        else
        {
            lblmsg.Text = "please select shelf";
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        popupAdd.Hide();
        txtbox.Text = string.Empty;
    }
    protected void imb_Deletecustomer_Click(object sender, EventArgs e)
    {

    }
    protected void imb_Deletearea_Click(object sender, EventArgs e)
    {
        try {
            if (ddlarea.SelectedValue != "0")
            {
                IA = new InventoryRepository<Inventory_Area>();
                IL = new InventoryRepository<Inventory_Locatin>();
                IS = new InventoryRepository<Inventory_Shelf>();
                IB = new InventoryRepository<Inventory_Bin>();
                IM = new InventoryRepository<InventoryManager>();
                In_A = new Inventory_Area();
                In_L = new Inventory_Locatin();
                In_s = new Inventory_Shelf();
                In_B = new Inventory_Bin();

                var count = IM.GetAll().Where(o => o.Area == int.Parse(ddlarea.SelectedValue)).ToList();
                if (count.Count == 0)
                {
                    In_A = IA.GetAll().Where(o => o.id == int.Parse(ddlarea.SelectedValue)).FirstOrDefault();
                    List<Inventory_Locatin> illist = IL.GetAll().Where(o => o.IA_id == In_A.id).ToList();
                    foreach (Inventory_Locatin x in illist)
                    {
                        List<Inventory_Shelf> islist = IS.GetAll().Where(p => p.IL_id == x.id).ToList();
                        foreach (Inventory_Shelf y in islist)
                        {
                            List<Inventory_Bin> iblist = IB.GetAll().Where(q => q.IS_id == y.id).ToList();
                            IB.DeleteAll(iblist);
                        }
                        IS.DeleteAll(islist);
                    }
                    IL.DeleteAll(illist);
                    IA.Delete(In_A);
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    lblmsg.Text = "Deleted successfully.";
                    BindArea(int.Parse(ddlcustomer.SelectedValue));
                }
                else
                {
                    lblmsg.Text = "Area Can't be deleted.";
                }
            }
            else
            {
                lblmsg.Text = "please select area";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imb_DeleteLocation_Click(object sender, EventArgs e)
    {

        try {
            IA = new InventoryRepository<Inventory_Area>();
            IL = new InventoryRepository<Inventory_Locatin>();
            IS = new InventoryRepository<Inventory_Shelf>();
            IB = new InventoryRepository<Inventory_Bin>();
            IM = new InventoryRepository<InventoryManager>();
            In_m = new InventoryManager();
            In_A = new Inventory_Area();
            In_L = new Inventory_Locatin();
            In_s = new Inventory_Shelf();
            In_B = new Inventory_Bin();
            if(ddllocation.SelectedValue!="0")
            {
                  var count = IM.GetAll().Where(o => o.Location == int.Parse(ddllocation.SelectedValue)).ToList();
                  if (count.Count == 0)
                  {
                      In_L = IL.GetAll().Where(o => o.id == int.Parse(ddllocation.SelectedValue)).FirstOrDefault();
                      List<Inventory_Shelf> islist = IS.GetAll().Where(p => p.IL_id == In_L.id).ToList();
                      foreach (Inventory_Shelf x in islist)
                      {
                          List<Inventory_Bin> iblist = IB.GetAll().Where(q => q.IS_id == x.id).ToList();
                          IB.DeleteAll(iblist);
                      }
                      IS.DeleteAll(islist);
                      IL.Delete(In_L);
                      lblmsg.ForeColor = System.Drawing.Color.Green;
                      lblmsg.Text = "Deleted successfully.";
                      BindLocation(int.Parse(ddlarea.SelectedValue));
                  }
                  else
                  {
                      lblmsg.Text = "Location Can't be deleted.";
                  }
            }
            else
            {
                 lblmsg.Text = "please select location";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imb_DeleteShelf_Click(object sender, EventArgs e)
    {
        try {
            if (ddlshelf.SelectedValue != "0")
            {
                IS = new InventoryRepository<Inventory_Shelf>();
                IB = new InventoryRepository<Inventory_Bin>();
                IM = new InventoryRepository<InventoryManager>();
                In_m = new InventoryManager();
                In_s = new Inventory_Shelf();
                In_B = new Inventory_Bin();

                 var count = IM.GetAll().Where(o => o.Shelf == int.Parse(ddlshelf.SelectedValue)).ToList();
                 if (count.Count == 0)
                 {
                     In_s = IS.GetAll().Where(o => o.id == int.Parse(ddlshelf.SelectedValue)).FirstOrDefault();
                     List<Inventory_Bin> iblist = IB.GetAll().Where(q => q.IS_id == In_s.id).ToList();
                     IB.DeleteAll(iblist);
                     IS.Delete(In_s);
                     lblmsg.ForeColor = System.Drawing.Color.Green;
                     lblmsg.Text = "Deleted successfully.";
                     BindShelf(int.Parse(ddllocation.SelectedValue));
                 }
                 else
                 {
                     lblmsg.Text = "Shelf Can't be deleted.";
                 }
            }
            else
            {
                lblmsg.Text = "please select shelf";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imb_DeleteBin_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlbin.SelectedValue != "0")
            {
                IB = new InventoryRepository<Inventory_Bin>();
                IM = new InventoryRepository<InventoryManager>();
                In_B = new Inventory_Bin();
                In_m = new InventoryManager();
                var count = IM.GetAll().Where(o => o.Bin == int.Parse(ddlbin.SelectedValue)).ToList();
                if (count.Count == 0)
                {
                    In_B = IB.GetAll().Where(o => o.id == int.Parse(ddlbin.SelectedValue)).FirstOrDefault();
                    IB.Delete(In_B);
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    lblmsg.Text = "Deleted successfully.";
                    BindBin(int.Parse(ddlshelf.SelectedValue));
                }
                else
                {
                    lblmsg.Text = "Bin Can't be deleted.";
                }
            }
            else
            {
                lblmsg.Text = "please select bin";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imb_Editcustomer_Click(object sender, EventArgs e)
    {

    }
    protected void imb_EditArea_Click(object sender, EventArgs e)
    {
        try
        {
            IA = new InventoryRepository<Inventory_Area>();
            In_A = new Inventory_Area();
            if (ddlarea.SelectedValue != "0")
            {
                hdedit.Value = "area";
                In_A = IA.GetAll().Where(o => o.id == int.Parse(ddlarea.SelectedValue)).FirstOrDefault();
                txtedit.Text = In_A.Name;
                popupEdit.Show();
                lbledit.Text = "Edit Area";
            }
            else
            {
                lblmsg.Text = "Please select area.";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imb_EditLocation_Click(object sender, EventArgs e)
    {
        try
        {
            IL = new InventoryRepository<Inventory_Locatin>();
            In_L = new Inventory_Locatin();
            if (ddllocation.SelectedValue != "0")
            {
                hdedit.Value = "location";
                In_L = IL.GetAll().Where(o => o.id == int.Parse(ddllocation.SelectedValue)).FirstOrDefault();
                txtedit.Text = In_L.Name;
                popupEdit.Show();
                lbledit.Text = "Edit Location";
            }
            else
            {
                lblmsg.Text = "Please select location.";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imb_EditShelf_Click(object sender, EventArgs e)
    {
        try
        {
            IS = new InventoryRepository<Inventory_Shelf>();
            In_s = new Inventory_Shelf();
            if (ddlshelf.SelectedValue != "0")
            {
                hdedit.Value = "shelf";
                In_s = IS.GetAll().Where(o => o.id == int.Parse(ddlshelf.SelectedValue)).FirstOrDefault();
                txtedit.Text = In_s.Name;
                popupEdit.Show();
                lbledit.Text = "Edit Shelf";
            }
            else
            {
                lblmsg.Text = "Please select shelf.";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void imb_EditBin_Click(object sender, EventArgs e)
    {
        try
        {
            IB = new InventoryRepository<Inventory_Bin>();
            In_B = new Inventory_Bin();
            if (ddlbin.SelectedValue != "0")
            {
                hdedit.Value = "bin";
                In_B = IB.GetAll().Where(o => o.id == int.Parse(ddlbin.SelectedValue)).FirstOrDefault();
                txtedit.Text = In_B.Name;
                popupEdit.Show();
                lbledit.Text = "Edit Bin";
            }
            else
            {
                lblmsg.Text = "Please select bin.";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            int i = 11;
            IA = new InventoryRepository<Inventory_Area>();
            IL = new InventoryRepository<Inventory_Locatin>();
            IS = new InventoryRepository<Inventory_Shelf>();
            IB = new InventoryRepository<Inventory_Bin>();
            In_L = new Inventory_Locatin();
            In_A = new Inventory_Area();
            In_s = new Inventory_Shelf();
            In_B = new Inventory_Bin();
            if (hdedit.Value == "area")
            {
                if (txtedit.Text.Trim() != "")
                    {
                        i = IA.GetAll().Where(o => o.Name == txtedit.Text.Trim() && o.Cid == int.Parse(ddlcustomer.SelectedValue)).Count();
                    if (i == 0)
                    {
                        In_A = IA.GetAll().Where(o => o.id == int.Parse(ddlarea.SelectedValue) && o.Cid == int.Parse(ddlcustomer.SelectedValue)).FirstOrDefault();
                        In_A.Name = txtedit.Text.Trim();

                        IA.Edit(In_A);
                        lblmsg.ForeColor = System.Drawing.Color.Green;
                        lblmsg.Text = "Area updated successfully.";
                        UpdatePanelFields.Update();
                        BindArea(int.Parse(ddlcustomer.SelectedValue));
                        ddlarea.Items.FindByText(txtedit.Text.Trim()).Selected = true;
                        BindLocation(int.Parse(ddlarea.SelectedValue));
                        txtedit.Text = string.Empty;
                       //popupEdit.Hide();
                    }
                    else
                    {
                        txtedit.Text = string.Empty;
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                        lblmsg.Text = "Already exist with this name.";
                    }
                }
                else
                {
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    lblmsg.Text = "Please enter area name.";
                }
            }
            else if (hdedit.Value == "location")
            {
                try {
                    if (txtedit.Text.Trim() != "")
                    {
                        i = IL.GetAll().Where(o => o.Name == txtedit.Text.Trim() && o.IA_id == int.Parse(ddlarea.SelectedValue)).Count();
                        if (i == 0)
                        {
                            In_L = IL.GetAll().Where(o => o.id ==int.Parse(ddllocation.SelectedValue)).FirstOrDefault();
                            In_L.Name = txtedit.Text.Trim();
                            IL.Edit(In_L);
                            lblmsg.ForeColor = System.Drawing.Color.Green;
                            lblmsg.Text = "Location updated successfully.";
                            BindLocation(int.Parse(ddlarea.SelectedValue));
                            ddllocation.Items.FindByText(txtedit.Text.Trim()).Selected = true;
                            BindShelf(int.Parse(ddllocation.SelectedValue));
                            txtedit.Text = string.Empty;
                        }
                        else
                        {
                            txtedit.Text = string.Empty;
                            lblmsg.ForeColor = System.Drawing.Color.Red;
                            lblmsg.Text = "Already exist with this name.";
                        }
                    }
                    else
                    {
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                        lblmsg.Text = "Please enter location name.";
                    }
                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
            }
            else if (hdedit.Value == "shelf")
            {
                try {
                    if (txtedit.Text.Trim() != "")
                    {
                        i = IS.GetAll().Where(o => o.Name == txtedit.Text.Trim() && o.IL_id == int.Parse(ddllocation.SelectedValue)).Count();
                        if (i == 0)
                        {
                            In_s = IS.GetAll().Where(o => o.id == int.Parse(ddlshelf.SelectedValue)).FirstOrDefault();
                            In_s.Name = txtedit.Text.Trim();
                            IS.Edit(In_s);
                            lblmsg.ForeColor = System.Drawing.Color.Green;
                            lblmsg.Text = "Shelf updated successfully.";
                            BindShelf(int.Parse(ddllocation.SelectedValue));
                            ddlshelf.Items.FindByText(txtedit.Text.Trim()).Selected = true;
                            BindBin(int.Parse(ddlshelf.SelectedValue));
                            txtedit.Text = string.Empty;
                        }
                        else
                        {
                            txtedit.Text = string.Empty;
                            lblmsg.ForeColor = System.Drawing.Color.Red;
                            lblmsg.Text = "Already exist with this name.";
                        }
                    }
                    else
                    {
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                        lblmsg.Text = "Please enter shelf name.";
                    }
                }
                catch (Exception ex)
                {
                    LogExceptions.WriteExceptionLog(ex);
                }
            }
            else if (hdedit.Value == "bin")
            {
                try {
                    if(txtedit.Text.Trim()!="")
                    {
                        i = IB.GetAll().Where(o => o.Name == txtedit.Text.Trim() && o.IS_id == int.Parse(ddlshelf.SelectedValue)).Count();
                        if(i==0)
                        {
                            In_B=IB.GetAll().Where(o=>o.id==int.Parse(ddlbin.SelectedValue)).FirstOrDefault();
                            In_B.Name=txtedit.Text.Trim();
                            IB.Edit(In_B);
                            lblmsg.ForeColor = System.Drawing.Color.Green;
                            lblmsg.Text = "Bin updated successfully.";
                            BindBin(int.Parse(ddlshelf.SelectedValue));
                            ddlbin.Items.FindByText(txtedit.Text.Trim()).Selected=true;
                            txtedit.Text = string.Empty;
                        }
                         else
                        {
                            txtedit.Text = string.Empty;
                            lblmsg.ForeColor = System.Drawing.Color.Red;
                            lblmsg.Text = "Already exist with this name.";
                        }
                    }
                    else
                    {
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                        lblmsg.Text = "Please enter bin name.";
                    }
                }
                catch (Exception ex) { LogExceptions.WriteExceptionLog(ex); }
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }

    }
    protected void BtnEditCancel_Click(object sender, EventArgs e)
    {
        txtbox.Text = string.Empty;
        popupEdit.Hide();
       // Response.Redirect(Request.Url.AbsoluteUri);
    }
    protected void AreaCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlcustomer.SelectedValue != "0")
            {
                INF = new InventoryRepository<InventoryFieldsConfig>();
                in_f = new InventoryFieldsConfig();
                if (AreaCheckBox.Checked == false)
                {
                    LocationCheckBox.Checked = false;
                    ShelfCheckBox.Checked = false;
                    BinChechBox.Checked = false;
                }
                var xList = INF.GetAll().Where(a => a.CustomerId == int.Parse(ddlcustomer.SelectedValue) && a.DefaultName == "Area").ToList();
                if (xList.Count == 0)
                {
                    //insert
                    in_f.CustomerId = int.Parse(ddlcustomer.SelectedValue);
                    in_f.DefaultName = "Area";
                    in_f.IsVisible = AreaCheckBox.Checked;
                    INF.Add(in_f);
                    lblmsg.ForeColor = System.Drawing.Color.Green;

                    lblmsg.Text = "Inserted Successfully.";
                }
                else
                {
                    //update
                    in_f = INF.GetAll().Where(a => a.CustomerId == int.Parse(ddlcustomer.SelectedValue) && a.DefaultName == "Area").FirstOrDefault();
                    in_f.IsVisible = AreaCheckBox.Checked;
                    INF.Edit(in_f);
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    lblmsg.Text = "Updated Successfully.";
                }
            }
            else
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = "Please select customer.";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void LocationCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlcustomer.SelectedValue != "0")
            {
                INF = new InventoryRepository<InventoryFieldsConfig>();
                in_f = new InventoryFieldsConfig();
                if (LocationCheckBox.Checked == false)
                {
                    ShelfCheckBox.Checked = false;
                    BinChechBox.Checked = false;
                }
                var xList = INF.GetAll().Where(a => a.CustomerId == int.Parse(ddlcustomer.SelectedValue) && a.DefaultName == "Location").ToList();
                if (xList.Count == 0)
                {
                    //insert
                    in_f.CustomerId = int.Parse(ddlcustomer.SelectedValue);
                    in_f.DefaultName = "Location";
                    in_f.IsVisible = LocationCheckBox.Checked;
                    INF.Add(in_f);
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    lblmsg.Text = "inserted Successfully.";
                }
                else
                {
                    //update
                    in_f = INF.GetAll().Where(a => a.CustomerId == int.Parse(ddlcustomer.SelectedValue) && a.DefaultName == "Location").FirstOrDefault();
                    in_f.IsVisible = LocationCheckBox.Checked;
                    INF.Edit(in_f);
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    lblmsg.Text = "Updated Successfully.";
                }
            }
            else
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = "Please select customer.";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void ShelfCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlcustomer.SelectedValue != "0")
            {
                INF = new InventoryRepository<InventoryFieldsConfig>();
                in_f = new InventoryFieldsConfig();
                if (ShelfCheckBox.Checked == false)
                {
                    BinChechBox.Checked = false;
                }
                var xList = INF.GetAll().Where(a => a.CustomerId == int.Parse(ddlcustomer.SelectedValue) && a.DefaultName == "Shelf").ToList();
                if (xList.Count == 0)
                {
                    //insert
                    in_f.CustomerId = int.Parse(ddlcustomer.SelectedValue);
                    in_f.DefaultName = "Shelf";
                    in_f.IsVisible = ShelfCheckBox.Checked;
                    INF.Add(in_f);
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    lblmsg.Text = "inserted Successfully.";
                }
                else
                {
                    //update
                    in_f = INF.GetAll().Where(a => a.CustomerId == int.Parse(ddlcustomer.SelectedValue) && a.DefaultName == "Shelf").FirstOrDefault();
                    in_f.IsVisible = ShelfCheckBox.Checked;
                    INF.Edit(in_f);
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    lblmsg.Text = "Updated Successfully.";
                }
            }
            else
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = "Please select customer.";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
    protected void BinChechBox_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlcustomer.SelectedValue != "0")
            {
                INF = new InventoryRepository<InventoryFieldsConfig>();
                in_f = new InventoryFieldsConfig();
                var xList = INF.GetAll().Where(a => a.CustomerId == int.Parse(ddlcustomer.SelectedValue) && a.DefaultName == "Bin").ToList();
                if (xList.Count == 0)
                {
                    //insert
                    in_f.CustomerId = int.Parse(ddlcustomer.SelectedValue);
                    in_f.DefaultName = "Bin";
                    in_f.IsVisible = BinChechBox.Checked;
                    INF.Add(in_f);
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    lblmsg.Text = "inserted Successfully.";
                }
                else
                {
                    //update
                    in_f = INF.GetAll().Where(a => a.CustomerId == int.Parse(ddlcustomer.SelectedValue) && a.DefaultName == "Bin").FirstOrDefault();
                    in_f.IsVisible = BinChechBox.Checked;
                    INF.Edit(in_f);
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    lblmsg.Text = "Updated Successfully.";
                }
            }
            else
            {
                lblmsg.ForeColor = System.Drawing.Color.Red;
                lblmsg.Text = "Please select customer.";
            }
        }
        catch (Exception ex)
        {
            LogExceptions.WriteExceptionLog(ex);
        }
    }
}