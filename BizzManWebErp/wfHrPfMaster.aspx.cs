﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BizzManWebErp
{
    public partial class wfHrPfMaster : System.Web.UI.Page
    {
        clsMain objMain = new clsMain();
        public string gstrInsertEdit;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MultiView1.ActiveViewIndex = 0;
                LoadRecord();
                gstrInsertEdit = "";
            }
            else
            {

            }

        }

        protected void Button_Create(object sender, EventArgs e)
        {
            gstrInsertEdit = "insert";
            MultiView1.ActiveViewIndex = 1;
            btnSave.Visible = true;
        }

        protected void Button_View(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
            LoadRecord();
        }

        /*
        void clear()
        {
            txtDepartmentName.Text = "";
        }
        */

        
        protected void Button_Save(object sender, EventArgs e)
        {
            btnSave.Visible= true;

        }
        
        

        public bool DataValidation(string strInsertEdit)
        {
            try
            {
                //Id,Name,Description
                //tblSdSalesItemCategory
                string strSql, strTemp, strCheck;

                //strCheck = "SELECT BranchName,BranchCode FROM tblBranchMaster WHERE BranchName='" + txtBranchName.Text + "' OR BranchCode ='" + txtBranchCode.Text + "'";
                //check duplicate value


                if (strInsertEdit == "insert")
                {
                   // strTemp = objMain.strFetchDate("select DeptName,CreateUser from tblHrDeptMaster where DeptName='" + txtDepartmentName.Text + "' ");

                    Debug.WriteLine("======================");
                   // Debug.WriteLine(strTemp);
                    Debug.WriteLine("======================");

                    /*
                    if (strTemp != "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Department Name Already Exists')", true);
                        return false;
                    }
                    */

                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        private void SaveData()    // for Member save data
        {
            //string s;
            //s = "";
            //s = " select BranchCode from tblHrBranchMaster where BranchCode='" + txtBranchCode.Text + "'";
            //  objMain.CheckData(s);

            try
            {
                /*
                string strSQL, strTemp;
                strSQL = "";
                strSQL = " insert into tblHrDeptMaster (DeptName,CreateUser) ";
                strSQL = strSQL + "  values('" + txtDepartmentName.Text + "','" + txtCreateUser.Text + "')";


                if (objMain.Insert(strSQL))
                {
                    strTemp = "sucess";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
                    //  ClearScreen();
                    // FetchData();
                    //  ScreenDiaseble();
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Already Exists')", true);
                }

                */


                if (DataValidation("insert") == true)
                {
                    string strSQL;
                    strSQL = "";


                    strSQL = " insert into tblHrDeptMaster (DeptName,CreateUser,CreateDate) ";
                   // strSQL = strSQL + "  values('" + txtDepartmentName.Text + "','" + Session["Id"].ToString() + "','" + System.DateTime.Today.ToString("yyyyMMdd") + "')";
                    if (objMain.Insert(strSQL))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
                        //  ClearScreen();
                        // FetchData();
                        //  ScreenDiaseble();
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Is Not Inserted')", true);
                    }
                }
                else
                {
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Is Not Inserted!!')", true);
                }


            }

            catch (Exception e)
            {
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Is Not Inserted!!')", true);
                return;
            }

        }

        protected void grdData_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            LoadRecord();
        }



        void LoadRecord()
        {
            string strSQL;
            strSQL = " select EmployeesContribution,EmployerContribution,BasicLimit,FixEmployerContribution,AdminCharges,EDLI,FinancialYear from tblHrPfMaster ";

            // Debug.WriteLine(objMain);
            // Debug.WriteLine("===================");

            //clsMain objSecond = new clsMain();
            // Debug.WriteLine(objSecond);

            if (objMain != null)
            {
                DataTable dt = (objMain.dtFetchData(strSQL));

                GridView1.DataSource = dt;
                GridView1.DataBind();

            }

        }
    }
}