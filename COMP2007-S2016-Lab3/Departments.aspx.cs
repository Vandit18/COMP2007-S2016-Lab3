using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/**
 *This Lab is done by Vandit Kothari Jose Mathew 
 *  
 * 
 */

// using statements that are required to connect to EF DB
using COMP2007_S2016_Lab3.Models;
using System.Web.ModelBinding;
using System.Linq.Dynamic;

namespace COMP2007_S2016_Lab3
{
    public partial class Departments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // if loading the page for the first time, populate the dept grid
            if (!IsPostBack)
            {
                Session["SortColumn"] = "DepartmentID"; // default sort column
                Session["SortDirection"] = "ASC";
                // Get the student data
                this.GetDepartments();
            }
        }
        protected void GetDepartments()
        {
            // connect to EF
            using (DefaultConnection db = new DefaultConnection())
            {
                string SortString = Session["SortColumn"].ToString() + " " + Session["SortDirection"].ToString();

                
                var Dept = (from allDepartments in db.Departments
                            select allDepartments);

                // bind the result to the GridView
                DeptGridView.DataSource = Dept.AsQueryable().OrderBy(SortString).ToList();
                DeptGridView.DataBind();
            }
        }
        /**
       * <summary>
       * This event handler deletes a Department from the db using EF
       * </summary>
       * 
       * @method DeptGridView_RowDeleting
       * @param {object} sender
       * @param {GridViewDeleteEventArgs} e
       * @returns {void}
       */
        protected void DeptGridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // store which row was clicked
            int selectedRow = e.RowIndex;

           
            int DepartmentID = Convert.ToInt32(DeptGridView.DataKeys[selectedRow].Values["DepartmentID"]);

           
            using (DefaultConnection db = new DefaultConnection())
            {
                // create object of the Department class and store the query string inside of it
                Department deletedDepartment = (from departmentRecords in db.Departments
                                                where departmentRecords.DepartmentID == DepartmentID
                                                select departmentRecords).FirstOrDefault();

                
                db.Departments.Remove(deletedDepartment);

                // save my changes back to the database
                db.SaveChanges();

                // refresh the grid
                this.GetDepartments();
            }
        }

        /**
         * <summary>
         * This event handler allows pagination to occur for the Departments page
         * </summary>
         * 
         * @method DeptGridView_PageIndexChanging
         * @param {object} sender
         * @param {GridViewPageEventArgs} e
         * @returns {void}
         */
        protected void DeptGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Set the new page number
            DeptGridView.PageIndex = e.NewPageIndex;

            // refresh the grid
            this.GetDepartments();
        }

        protected void PageSizeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Set the new Page size
            DeptGridView.PageSize = Convert.ToInt32(PageSizeDropDownList.SelectedValue);

            // refresh the grid
            this.GetDepartments();
        }

        protected void DeptGridView_Sorting(object sender, GridViewSortEventArgs e)
        {
            // get the column to sorty by
            Session["SortColumn"] = e.SortExpression;

            // Refresh the Grid
            this.GetDepartments();

            // toggle the direction
            Session["SortDirection"] = Session["SortDirection"].ToString() == "ASC" ? "DESC" : "ASC";
        }

        protected void DeptGridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (IsPostBack)
            {
                if (e.Row.RowType == DataControlRowType.Header) // if header row has been clicked
                {
                    LinkButton linkbutton = new LinkButton();

                    for (int index = 0; index < DeptGridView.Columns.Count - 1; index++)
                    {
                        if (DeptGridView.Columns[index].SortExpression == Session["SortColumn"].ToString())
                        {
                            if (Session["SortDirection"].ToString() == "ASC")
                            {
                                linkbutton.Text = " <i class='fa fa-caret-up fa-lg'></i>";
                            }
                            else
                            {
                                linkbutton.Text = " <i class='fa fa-caret-down fa-lg'></i>";
                            }

                            e.Row.Cells[index].Controls.Add(linkbutton);
                        }
                    }
                }
            }
        }
    }
}
