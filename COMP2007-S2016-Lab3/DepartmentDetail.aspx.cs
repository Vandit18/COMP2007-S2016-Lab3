using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using COMP2007_S2016_Lab3.Models;
using System.Web.ModelBinding;


namespace COMP2007_S2016_Lab3
{
    public partial class DepartmentDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((!IsPostBack) && (Request.QueryString.Count > 0))
            {
                this.GetDepartment();
            }
        }
        protected void GetDepartment()
        {
            // populate teh form with existing data from the database
            int DepartmentID = Convert.ToInt32(Request.QueryString["DepartmentID"]);

            // connect to the EF DB
            using (DefaultConnection db = new DefaultConnection())
            {
                // populate a Department object instance with the DepartmentID from the URL Parameter
                Department updatedDepartment = (from Departments in db.Departments
                                                where Departments.DepartmentID == DepartmentID
                                                select Departments).FirstOrDefault();

                // map the Department properties to the form controls
                if (updatedDepartment != null)
                {
                    DeptNameTextBox.Text = updatedDepartment.Name;
                    BudgetTextBox.Text = Convert.ToString(updatedDepartment.Budget);

                }
            }
        }
        protected void CancelButton_Click(object sender, EventArgs e)
        {
            // Redirect back to Departments page
            Response.Redirect("~/Departments.aspx");
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            
            using (DefaultConnection db = new DefaultConnection())
            {
                
                // save a new record
                Department newDepartment = new Department();

                int DepartmentID = 0;

                if (Request.QueryString.Count > 0) 
                {
                    // get the id from the URL
                    DepartmentID = Convert.ToInt32(Request.QueryString["DepartmentID"]);

                    // get the current student from EF DB
                    newDepartment = (from Department in db.Departments
                                     where Department.DepartmentID == DepartmentID
                                     select Department).FirstOrDefault();
                }

                // add form data to new record
                newDepartment.Name = DeptNameTextBox.Text;
                newDepartment.Budget = Convert.ToDecimal(BudgetTextBox.Text);


                

                if (DepartmentID == 0)
                {
                    db.Departments.Add(newDepartment);
                }


                // save our changes - also updates and inserts
                db.SaveChanges();

                
                Response.Redirect("~/Departments.aspx");
            }
        }
    }
}
