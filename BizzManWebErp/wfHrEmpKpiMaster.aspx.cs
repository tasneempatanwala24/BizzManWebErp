﻿using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;

namespace BizzManWebErp
{
    public partial class wfHrEmpKpiMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Id"] != null)
            {
                loginuser.Value = Convert.ToString(Session["Id"]);
                 
            }
            else
            {
                Response.Redirect("wfAdminLogin.aspx");
            }
        }

        //KpiGroupList
        [WebMethod]
        public static string KpiGroupList()
        {
            clsMain objMain = new clsMain();
            DataTable dtBranchList = new DataTable();

            try
            {

                dtBranchList = objMain.dtFetchData("select Id,KpiGroupName FROM tblHrEmpKpiGroupMaster");
            }
            catch (Exception ex)
            {
                return "";
            }

            return JsonConvert.SerializeObject(dtBranchList);
        }



        [WebMethod]
        public static string JobCategoryList()
        {
            clsMain objMain = new clsMain();
            DataTable dtBranchList = new DataTable();

            try
            {

                dtBranchList = objMain.dtFetchData("select Id,JobCategoryName FROM tblHrEmpJobCategoryMaster");
            }
            catch (Exception ex)
            {
                return "";
            }

            return JsonConvert.SerializeObject(dtBranchList);
        }

        [WebMethod]
        public static string JobCtcItemList()
        {
            clsMain objMain = new clsMain();
            DataTable dtBranchList = new DataTable();

            try
            {

                dtBranchList = objMain.dtFetchData("select CtcItemName FROM tblHrPayrollCtcItemMaster");
            }
            catch (Exception ex)
            {
                return "";
            }

            return JsonConvert.SerializeObject(dtBranchList);
        }

        [WebMethod]
        public static string UnitMesureList()
        {
            clsMain objMain = new clsMain();
            DataTable dtUnitList = new DataTable();

            try
            {

                dtUnitList = objMain.dtFetchData("select UnitMesureName FROM tblFaUnitMesureMaster");
            }
            catch (Exception ex)
            {
                return "";
            }

            return JsonConvert.SerializeObject(dtUnitList);
        }

        [WebMethod]
        public static string FetchEmpJobDetails(string Id = "")
        {
            clsMain objMain = new clsMain();
            DataTable dtMaterialList = new DataTable(); 

            try
            {

               // dtMaterialList = objMain.dtFetchData(@"select Id,JobCategoryId,EmpJobName,UnitMesure,JobRate,CtcItemName from tblHrEmpJobMaster where EmpJobName='" + EmpJobName + "'");
                dtMaterialList = objMain.dtFetchData(@"select a.Id,c.KpiGroupName, b.KpiSubGroupName, a.GoalObjective GoalObjective
                                                   from tblHrEmpKpiMaster a, tblHrEmpKpiSubGroupMaster b, tblHrEmpKpiGroupMaster c
                                                     where a.KpiSubGroupId = b.id and b.KpiGroupId = c.Id and a.Id='" + Id + "'");

            }
            catch (Exception ex)
            {
                // return "";
            }


            string json = JsonConvert.SerializeObject(dtMaterialList, Formatting.None);
            return json;
        }

        [WebMethod]
        public static string FetchEmpJoblList()
        {
            clsMain objMain = new clsMain();
            DataTable dtEmpList = new DataTable();

            try
            {
               // SELECT a.Id,c.KpiGroupName, b.KpiSubGroupName, a.GoalObjective
                  // FROM tblHrEmpKpiMaster a, tblHrEmpKpiSubGroupMaster b, tblHrEmpKpiGroupMaster c
                   // where a.KpiSubGroupId = b.id and b.KpiGroupId = c.Id

              //  dtEmpList = objMain.dtFetchData(@"select e.Id,e.EmpJobName,e.UnitMesure,e.JobRate,e.CtcItemName,br.JobCategoryName
               //                                 from tblHrEmpJobMaster e
                //                                inner join tblHrEmpJobCategoryMaster br on e.JobCategoryId=br.Id");


                dtEmpList = objMain.dtFetchData(@"select a.Id,c.KpiGroupName, b.KpiSubGroupName, a.GoalObjective GoalObjective
                                                from tblHrEmpKpiMaster a, tblHrEmpKpiSubGroupMaster b, tblHrEmpKpiGroupMaster c
                                               where a.KpiSubGroupId = b.id and b.KpiGroupId = c.Id ");

            }
            catch (Exception ex)
            {
                // return "";
            }

            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.Arrays
            };
            return JsonConvert.SerializeObject(dtEmpList, settings);
        }

        [WebMethod]
        public static string CheckJobAvailability(string EmpJobName, string isUpdate)
        {
            clsMain objMain = new clsMain();
            bool checkId = new bool();

            try
            {

                if (isUpdate == "0")
                {
                    checkId = objMain.blSearchDataHO(string.Format("select 1 from tblHrEmpJobMaster where EmpJobName='{0}'", EmpJobName));
                }
                else
                {
                    checkId = false;
                }
            }
            catch (Exception ex)
            {
                return "False";
            }

            return JsonConvert.SerializeObject(checkId.ToString());
        }

        [WebMethod]
        public static string AddEmpJob(string JobCategoryId, string EmpJobName, string UnitMesure, string CtcItemName, string loginUser, string JobRate)
        {

            clsMain objMain = new clsMain();
            SqlParameter[] objParam = new SqlParameter[7];

            objParam[0] = new SqlParameter("@JobCategoryId", SqlDbType.Int);
            objParam[0].Direction = ParameterDirection.Input;
            objParam[0].Value = Convert.ToInt32(JobCategoryId);

            objParam[1] = new SqlParameter("@EmpJobName", SqlDbType.NVarChar);
            objParam[1].Direction = ParameterDirection.Input;
            objParam[1].Value = EmpJobName;

            objParam[2] = new SqlParameter("@UnitMesure", SqlDbType.NVarChar);
            objParam[2].Direction = ParameterDirection.Input;
            objParam[2].Value = UnitMesure;


            objParam[3] = new SqlParameter("@CtcItemName", SqlDbType.NVarChar);
            objParam[3].Direction = ParameterDirection.Input;
            objParam[3].Value = CtcItemName;

            objParam[4] = new SqlParameter("@CreateUser", SqlDbType.NVarChar);
            objParam[4].Direction = ParameterDirection.Input;
            objParam[4].Value = loginUser;

            objParam[5] = new SqlParameter("@UpdateUser", SqlDbType.NVarChar);
            objParam[5].Direction = ParameterDirection.Input;
            objParam[5].Value = loginUser;



            objParam[6] = new SqlParameter("@JobRate", SqlDbType.Decimal);
            objParam[6].Direction = ParameterDirection.Input;
            objParam[6].Value = Convert.ToDecimal(JobRate);



            var result = objMain.ExecuteProcedure("procHrEmpJobMaster", objParam);

            return "";
        }
    }
}