﻿using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class csReport : System.Web.UI.Page
{
 
    public int marks = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (str.checkSpace((string)Session["ques"]))
        {
            if (!Page.IsPostBack)
            {
                ViewState["datatable"] = Session["datatable"];
                Session["datatable"] = null;
                GridView1.DataSource = ViewState["datatable"];
                GridView1.DataBind();
            }
        }
        else
        {
            Response.Redirect("ques.aspx");
        }
    }

    public object CAns(object o)
    {
        return DataBinder.Eval(o, (string)DataBinder.Eval(o, "correctans"));
    }

    public object ans(object o)
    {
        int i = Int32.Parse(DataBinder.Eval(o, "id").ToString());
        string s;

        if (((Dictionary<int, string>)Session["res"]).TryGetValue(i, out s)) ;// I know it
        return s;
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label l2 = (Label)e.Row.FindControl("Label2");
            Label l3 = (Label)e.Row.FindControl("Label3");
            if (!String.IsNullOrEmpty(l3.Text))
            {
                if (String.Equals(l2.Text, l3.Text, StringComparison.CurrentCulture))
                {
                    l3.ForeColor = System.Drawing.Color.Green;
                    marks++;
                }
                else
                    l3.ForeColor = System.Drawing.Color.Red;
                //Response.Write();
            }
            if (l3.Text == "")
            {
                l3.Text = "NA";
                l3.ForeColor = System.Drawing.Color.DarkOrange;
            }
        }
    }


    protected void logout_Click(object sender, EventArgs e)
    {
        FormsAuthentication.SignOut();
        Session.Abandon();
        FormsAuthentication.RedirectToLoginPage();
    }
}