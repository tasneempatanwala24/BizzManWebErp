using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;

namespace BizzManWebErp
{
    public partial class wfHrEmpKpiSubGroup : System.Web.UI.Page
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

  
      
        [WebMethod]
        public static string KpiGroupList()
        {
            // do not create this object, use session, after use, dospose/free memory
            clsMain objMain = new clsMain();
            DataTable dtBranchList = new DataTable();

            try
            {

                dtBranchList = objMain.dtFetchData("select Id, KpiGroupName FROM tblHrEmpKpiGroupMaster");
            }
            catch (Exception ex)
            {
                return "";
            }

            return JsonConvert.SerializeObject(dtBranchList);
        }

       
        [WebMethod]
        public static string FetchMasterDetails(string Id = "")
        {
            clsMain objMain = new clsMain();
            DataTable dtMaterialList = new DataTable();

            try
            {
                dtMaterialList = objMain.dtFetchData(@"select a.Id, a.KpiGroupId, b.KpiGroupName, a.KpiSubGroupName 
                                   from tblHrEmpKpiSubGroupMaster a, tblHrEmpKpiGroupMaster b 
                                   where a.KpiGroupId=b.Id and a.Id = " + Id + "");
            }
            catch (Exception ex)
            {
                // return "";
            }
            string json = JsonConvert.SerializeObject(dtMaterialList, Formatting.None);
            return json;
        }

        [WebMethod]
        public static string FetchMasterList()
        {
            clsMain objMain = new clsMain();
            DataTable dtEmpList = new DataTable();
             
            try
            {
                dtEmpList = objMain.dtFetchData(@"select a.Id, a.KpiGroupId, b.KpiGroupName, a.KpiSubGroupName 
                                                 from tblHrEmpKpiSubGroupMaster a, tblHrEmpKpiGroupMaster b
                                                where a.KpiGroupId=b.Id");
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

        //  new style by mk
        [WebMethod]
        public static string CheckDataAvailability(string strSearchName, string isUpdate)
        {
            clsMain objMain = new clsMain();
            bool checkId = new bool();

            try
            { 

                if (isUpdate == "0")
                {
                   // checkId = objMain.blSearchDataHO(string.Format("select 1 from tblHrEmpJobMaster where EmpJobName='{0}'", strSearchbName));
                    checkId = objMain.blSearchDataHO(string.Format("select 1 from tblHrEmpKpiSubGroupMaster where KpiSubGroupName='{0}'", strSearchName));
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
        public static string AddData(int KpiGroupId, string KpiSubGroupName, string loginUser)
        {
             
            clsMain objMain = new clsMain();
            SqlParameter[] objParam = new SqlParameter[3];

            objParam[0] = new SqlParameter("@KpiGroupId", SqlDbType.Int);
            objParam[0].Direction = ParameterDirection.Input;
            objParam[0].Value = Convert.ToInt32(KpiGroupId);

            objParam[1] = new SqlParameter("@KpiSubGroupName", SqlDbType.NVarChar);
            objParam[1].Direction = ParameterDirection.Input;
            objParam[1].Value = KpiSubGroupName;

            objParam[2] = new SqlParameter("@CreateUser", SqlDbType.NVarChar);
            objParam[2].Direction = ParameterDirection.Input;
            objParam[2].Value = loginUser;

           // objParam[3] = new SqlParameter("@UpdateUser", SqlDbType.NVarChar);
          //  objParam[3].Direction = ParameterDirection.Input;
          //  objParam[3].Value = loginUser;

            var result = objMain.ExecuteProcedure("procHrEmpKpiSubGroupMaster", objParam);
            return "";
        }

 
    }
}