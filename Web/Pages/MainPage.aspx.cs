using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.Pages
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                categoryDropDown.DataSource = CreateDataSource();
                categoryDropDown.DataTextField = "ColorTextField";
                categoryDropDown.DataValueField = "ColorValueField";

                categoryDropDown.DataBind();

                categoryDropDown.SelectedIndex = 0;
            }
        }

        ICollection CreateDataSource()
        {

            // Create a table to store data for the DropDownList control.
            DataTable dt = new DataTable();

            // Define the columns of the table.
            dt.Columns.Add(new DataColumn("ColorTextField", typeof(String)));
            dt.Columns.Add(new DataColumn("ColorValueField", typeof(String)));

            // Populate the table with sample values.
            dt.Rows.Add(CreateRow("White", "White", dt));
            dt.Rows.Add(CreateRow("Silver", "Silver", dt));
            dt.Rows.Add(CreateRow("Dark Gray", "DarkGray", dt));
            dt.Rows.Add(CreateRow("Khaki", "Khaki", dt));
            dt.Rows.Add(CreateRow("Dark Khaki", "DarkKhaki", dt));

            // Create a DataView from the DataTable to act as the data source
            // for the DropDownList control.
            DataView dv = new DataView(dt);
            return dv;

        }

        DataRow CreateRow(String Text, String Value, DataTable dt)
        {

            // Create a DataRow using the DataTable defined in the 
            // CreateDataSource method.
            DataRow dr = dt.NewRow();

            // This DataRow contains the ColorTextField and ColorValueField 
            // fields, as defined in the CreateDataSource method. Set the 
            // fields with the appropriate value. Remember that column 0 
            // is defined as ColorTextField, and column 1 is defined as 
            // ColorValueField.
            dr[0] = Text;
            dr[1] = Value;

            return dr;

        }
    }
}