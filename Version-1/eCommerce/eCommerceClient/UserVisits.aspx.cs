using Client.Code;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace Client
{
    public partial class UserVisits : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UserHandler a = new UserHandler();
            Data_cart.DataSource = a.getUsersVisitsInDate(Session["Date"].ToString());
            Data_cart.DataBind();

            DataSet ds = a.getUsersVisitsInDateChart(Session["Date"].ToString());
            //Now bind the xValues and yVlaues for each series
            List<string> xValues = new List<string>();
            List<int> yValues = new List<int>();

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                Console.Out.WriteLine(row[0]);
                Console.Out.WriteLine(row[1]);
                Console.Out.WriteLine(row[2]);
                Console.Out.WriteLine(row[3]);
                Console.Out.WriteLine(row[4]);
                string xValue = "RegisteredUsers";
                string str = row[0].ToString();
                int yValue = int.Parse(str.Substring(1,str.Length-2));
                xValues.Add(xValue);
                yValues.Add(yValue);
                xValue = "Guests";
                str = row[1].ToString();
                yValue = int.Parse(str.Substring(1,str.Length-2));
                xValues.Add(xValue);
                yValues.Add(yValue);
                xValue = "Managers";
                str = row[2].ToString();
                yValue = int.Parse(str.Substring(1,str.Length-2));
                xValues.Add(xValue);
                yValues.Add(yValue);
                xValue = "Owners";
                str = row[3].ToString();
                yValue = int.Parse(str.Substring(1,str.Length-2));
                xValues.Add(xValue);
                yValues.Add(yValue);
                xValue = "Admins";
                str = row[4].ToString();
                yValue = int.Parse(str.Substring(1,str.Length-2));
                xValues.Add(xValue);
                yValues.Add(yValue);
                Chart1.Width = 750;
                Chart1.Height = 750;
                Chart1.Series.Add("User Visits Classification");
                Chart1.Series["User Visits Classification"].ChartType = System.Web.UI.DataVisualization.Charting.SeriesChartType.Line;
                Chart1.Series["User Visits Classification"].ChartArea = "ChartArea1";
                Chart1.Series["User Visits Classification"].Legend = "Legend1";
                Chart1.Series["User Visits Classification"].LegendText = "User Visits Classification";
                Chart1.Series["User Visits Classification"].Points.DataBindXY(xValues, yValues);
                return;
            }
            Chart1.Visible = false;
        }
    }
}