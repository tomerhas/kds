using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Modules_UserControl_ucAccordionTest : System.Web.UI.UserControl
{
    private DataTable _DataSource;
    private DataView dvPeilut;
    protected void Page_Load(object sender, EventArgs e)
    {
        BuildSidurim(_DataSource);
    }
    protected void BuildSidurim(DataTable dtSidurim)
    {
        //AjaxControlToolkit.AccordionPane Ap = new AjaxControlToolkit.AccordionPane();
        AjaxControlToolkit.CollapsiblePanelExtender Ax;
        System.Web.UI.HtmlControls.HtmlTable hTable;
        GridView grdPeiluyot = new GridView();
        dvPeilut = new DataView(dtSidurim);
        //Label lbl;
        Panel pnlHeader, pnlContent;
        TableRow tRow;
        TableCell tCell;

        int iPrevSidur = 0;
        int i = 1;
        try
        {
            foreach (DataRow dr in dtSidurim.Rows)
            {
                if ((int.Parse(dr["mispar_sidur"].ToString())) != iPrevSidur)
                {
                    
                    //Ap = new AjaxControlToolkit.AccordionPane();                    
                    //Ap.ID = "AccordionPane" + i;
                    i++;
                 
                    //MyAccordion.Controls[0].Controls.Add(Ap);
                    
                    hTable = BuildOneSidur(dr, i);
                    
                    //Ap.Controls.Add(hTable);
                    //Ap.HeaderContainer.Controls.Add(hTable);
                    iPrevSidur = int.Parse(dr["mispar_sidur"].ToString());
                   
                    pnlHeader = new Panel();
                    pnlHeader.ID = "pnlHeader" + i;
                    pnlHeader.Controls.Add(hTable);
                    //Ap.ContentContainer.Controls.Add(pnl);
                    tRow = new TableRow();
                    tCell = new TableCell();
                    tRow.ID = "Row" + i;
                    tCell.ID = "Cell" + i;
                    tRow.Cells.Add(tCell);
                    tbHeaderSidurim.Rows.Add(tRow);

                    tCell.Controls.Add(pnlHeader);
                    pnlContent = new Panel();
                    pnlContent.ID = "pnlContent" + i;

                    tCell.Controls.Add(pnlContent);

                    grdPeiluyot = BuildSidurPeiluyot(dvPeilut, dr, i);                   
                    //Ap.ContentContainer.Controls.Add(grdPeiluyot);
                    pnlContent.Controls.Add(grdPeiluyot);
                   
                    Ax = new AjaxControlToolkit.CollapsiblePanelExtender();
                    Ax.ID = "Ax" + i;
                    Ax.Collapsed = true;
                    Ax.CollapsedSize = 2;
                    Ax.ExpandedSize = 130;
                    Ax.AutoCollapse = false;
                    Ax.AutoExpand = false;
                    Ax.ScrollContents = true;
                    Ax.TargetControlID = pnlContent.ID;
                    Ax.ExpandedText = "פתח פעילויות לסידור...";
                    Ax.CollapsedText = "הסתר פעילויות לסידור...";
                    Ax.ExpandControlID = pnlHeader.ID;
                    Ax.CollapseControlID = pnlHeader.ID;
                    Ax.ExpandedImage="images/collapse_blue.jpg" ;
                    Ax.CollapsedImage = "images/expand_blue.jpg";
                    tCell.Controls.Add(Ax);
                    //Ap.ContentContainer.Controls.Add(lbl);
                    
                    //Ap.ContentContainer.Controls.Add(Ax);
                    //Ap.Controls.Add(grdPeiluyot);
                }                
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected GridView BuildSidurPeiluyot(DataView dv, DataRow dr, int iIndex)
    {
        GridView grdPeiluyot = new GridView();
        BoundField boundGridField;
     

        grdPeiluyot.GridLines = GridLines.None;
        grdPeiluyot.ID = "grdPeiluyot" + iIndex;
        grdPeiluyot.ShowHeader = true;
        grdPeiluyot.AllowPaging = false;
        grdPeiluyot.AutoGenerateColumns = false;
        grdPeiluyot.AllowSorting = true;
        //grdPeiluyot.Width = "550px";
        //grdPeiluyot.EmptyDataRowStyle - CssClass = "GridHeader";


        boundGridField = new BoundField();
        boundGridField.HeaderText = "כיסוי תור";
        boundGridField.DataField = "kisuy_tor";
        boundGridField.SortExpression = "kisuy_tor";
        //boundGridField.ItemStyle-CssClass="ItemRow";
        //boundGridField.HeaderStyle-CssClass="GridHeader";
        //boundGridField.ItemStyle-Width="115px";


        grdPeiluyot.Columns.Add(boundGridField);

        boundGridField = new BoundField();
        boundGridField.HeaderText = "שעת יציאה";
        boundGridField.DataField = "shat_yetzia";
        boundGridField.SortExpression = "shat_yetzia";
        //boundGridField.ItemStyle-CssClass="ItemRow";
        //boundGridField.HeaderStyle-CssClass="GridHeader";
        //boundGridField.ItemStyle-Width="115px";
        boundGridField.DataFormatString = "{0:HH:MM}";
        boundGridField.HtmlEncodeFormatString = true;


        grdPeiluyot.Columns.Add(boundGridField);
        dvPeilut.RowFilter = "mispar_sidur=" + dr["mispar_sidur"].ToString();
        grdPeiluyot.DataSource = dv;
        grdPeiluyot.DataBind();
        return grdPeiluyot;
    }
    protected HtmlTable BuildOneSidur(DataRow dr, int iIndex)
    {
        HtmlTable hTable = new HtmlTable();
        HtmlTableRow hRow = new HtmlTableRow();
        HtmlTableCell hCell;
        Label lbl;
        DropDownList ddl;
        CheckBox chkBox;
        ImageButton btnImage;
        try
        {
            hTable.ID = "tblSidurim" + iIndex;
            hTable.Border = 1;
            hTable.Style["width"] = "100%";

            hRow.Style["class"] = "OvedDetailsHeader";
            hRow.Style["height"] = "50px";
            hTable.Rows.Add(hRow);

            hCell = CreateTableCell("35px", "55.7px", "CellListView", "מספר סידור");
            hTable.Rows[0].Cells.Add(hCell);

            hCell = CreateTableCell("40px", "55.7px", "CellListView", "שעת התחלה");
            hTable.Rows[0].Cells.Add(hCell);

            hCell = CreateTableCell("40px", "55.7px", "CellListView", "שעת סיום");
            hTable.Rows[0].Cells.Add(hCell);

            hCell = CreateTableCell("50px", "55.7px", "CellListView", "סיבת אי החתמה כניסה");
            hTable.Rows[0].Cells.Add(hCell);

            hCell = CreateTableCell("50px", "55.7px", "CellListView", "סיבת אי החתמה יציאה");
            hTable.Rows[0].Cells.Add(hCell);

            hCell = CreateTableCell("40px", "55.7px", "CellListView", "שעת התחלה לתשלום");
            hTable.Rows[0].Cells.Add(hCell);

            hCell = CreateTableCell("40px", "55.7px", "CellListView", "שעת גמר לתשלום");
            hTable.Rows[0].Cells.Add(hCell);

            hCell = CreateTableCell("40px", "55.7px", "CellListView", "חריגה");
            hTable.Rows[0].Cells.Add(hCell);

            hCell = CreateTableCell("40px", "55.7px", "CellListView", "פיצול/ הפסקה");
            hTable.Rows[0].Cells.Add(hCell);

            hCell = CreateTableCell("40px", "55.7px", "CellListView", "המרה");
            hTable.Rows[0].Cells.Add(hCell);

            hCell = CreateTableCell("40px", "55.7px", "CellListView", "השלמה");
            hTable.Rows[0].Cells.Add(hCell);

            hCell = CreateTableCell("40px", "55.7px", "CellListView", "מחוץ למכסה");
            hTable.Rows[0].Cells.Add(hCell);

            hCell = CreateTableCell("40px", "55.7px", "CellListView", "לא לתשלום");
            hTable.Rows[0].Cells.Add(hCell);

            hCell = CreateTableCell("40px", "55.7px", "CellListView", "בטל");
            hTable.Rows[0].Cells.Add(hCell);

            //שורה שניה
            hRow = new HtmlTableRow();
            hTable.Rows.Add(hRow);

            //hCell.Style("class") = "CellListView";
            hCell = CreateTableCell("35px", "55.7px", "", "");
            lbl = new Label();
            lbl.ID = "lblSidur" + iIndex;
            lbl.Text = dr["mispar_sidur"].ToString();
            hCell.Controls.Add(lbl);
            hTable.Rows[1].Cells.Add(hCell);

            hCell = CreateTableCell("40px", "55.7px", "", "");
            lbl = new Label();
            lbl.ID = "txtShatHatchla" + iIndex;
            lbl.Text = (DateTime.Parse(dr["shat_hatchala"].ToString())).ToShortTimeString();
            hCell.Controls.Add(lbl);
            hTable.Rows[1].Cells.Add(hCell);

            hCell = CreateTableCell("40px", "55.7px", "", "");
            lbl = new Label();
            lbl.ID = "lblShatGmar" + iIndex;
            lbl.Text = String.IsNullOrEmpty(dr["shat_gmar"].ToString()) ? "00:00" : (DateTime.Parse(dr["shat_gmar"].ToString())).ToShortTimeString();
            hCell.Controls.Add(lbl);
            hTable.Rows[1].Cells.Add(hCell);

            hCell = CreateTableCell("40px", "55.7px", "", "");
            ddl = new DropDownList();
            ddl.ID = "ddlResonIn" + iIndex;
            hCell.Controls.Add(ddl);
            hTable.Rows[1].Cells.Add(hCell);

            hCell = CreateTableCell("40px", "55.7px", "", "");
            ddl = new DropDownList();
            ddl.ID = "ddlResonOut" + iIndex;
            hCell.Controls.Add(ddl);
            hTable.Rows[1].Cells.Add(hCell);

            hCell = CreateTableCell("40px", "55.7px", "", "");
            lbl = new Label();
            lbl.ID = "lblShatHatchalaLetashlum" + iIndex;
            lbl.Text = String.IsNullOrEmpty(dr["shat_hatchala_letashlum"].ToString()) ? "00:00" : (DateTime.Parse(dr["shat_hatchala_letashlum"].ToString())).ToShortTimeString();
            hCell.Controls.Add(lbl);
            hTable.Rows[1].Cells.Add(hCell);

            hCell = CreateTableCell("40px", "55.7px", "", "");
            lbl = new Label();
            lbl.ID = "lblShatGmarLetashlum" + iIndex;
            lbl.Text = String.IsNullOrEmpty(dr["shat_Gmar_letashlum"].ToString()) ? "00:00" : (DateTime.Parse(dr["shat_gmar_letashlum"].ToString())).ToShortTimeString();
            hCell.Controls.Add(lbl);
            hTable.Rows[1].Cells.Add(hCell);

            hCell = CreateTableCell("40px", "55.7px", "", "");
            ddl = new DropDownList();
            ddl.ID = "ddlException" + iIndex;
            hCell.Controls.Add(ddl);
            hTable.Rows[1].Cells.Add(hCell);

            hCell = CreateTableCell("40px", "55.7px", "", "");
            lbl = new Label();
            lbl.ID = "lblBreak" + iIndex;
            //lbl.Text = (DateTime.Parse(dr["shat_hatchala"].ToString())).ToShortTimeString();
            hCell.Controls.Add(lbl);
            hTable.Rows[1].Cells.Add(hCell);

            hCell = CreateTableCell("40px", "55.7px", "", "");
            chkBox = new CheckBox();
            chkBox.ID = "chkHamara" + iIndex;
            //lbl.Text = (DateTime.Parse(dr["shat_hatchala"].ToString())).ToShortTimeString();
            hCell.Controls.Add(chkBox);
            hTable.Rows[1].Cells.Add(hCell);

            hCell = CreateTableCell("40px", "55.7px", "", "");
            ddl = new DropDownList();
            ddl.ID = "ddlHashlama" + iIndex;
            hCell.Controls.Add(ddl);
            hTable.Rows[1].Cells.Add(hCell);

            hCell = CreateTableCell("40px", "55.7px", "", "");
            chkBox = new CheckBox();
            chkBox.ID = "chkOutMichsa" + iIndex;
            //lbl.Text = (DateTime.Parse(dr["shat_hatchala"].ToString())).ToShortTimeString();
            hCell.Controls.Add(chkBox);
            hTable.Rows[1].Cells.Add(hCell);


            hCell = CreateTableCell("40px", "55.7px", "", "");
            chkBox = new CheckBox();
            chkBox.ID = "chkLoLetashlum" + iIndex ;
            //lbl.Text = (DateTime.Parse(dr["shat_hatchala"].ToString())).ToShortTimeString();
            hCell.Controls.Add(chkBox);
            hTable.Rows[1].Cells.Add(hCell);

            hCell = CreateTableCell("40px", "55.7px", "", "");
            btnImage = new ImageButton();
            btnImage.ID = "imgCancel" + iIndex;
            //lbl.Text = (DateTime.Parse(dr["shat_hatchala"].ToString())).ToShortTimeString();
            hCell.Controls.Add(btnImage);
            hTable.Rows[1].Cells.Add(hCell);
            return hTable;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected HtmlTableCell CreateTableCell(string sHeight, string sWidth, string sClass, string sText)
    {
        HtmlTableCell hCell = new HtmlTableCell();
        if (!String.IsNullOrEmpty(sHeight))
        {
            hCell.Style["Height"] = sHeight;
        }
        if (!String.IsNullOrEmpty(sWidth))
        {
            hCell.Style["width"] = sWidth;
        }
        if (!String.IsNullOrEmpty(sClass))
        {
            hCell.Style["class"] = sClass;
        }
        if (!String.IsNullOrEmpty(sText))
        {
            hCell.InnerHtml = sText;
        }
        return hCell;
    }
    public DataTable DataSource
    {
        set
        {
            _DataSource = value;
        }
        get
        {
            return _DataSource;
        }
    }
}
