using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

namespace BizzManWebErp
{
    [ServiceContract(Namespace = "ApplicationService")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ApplicationService
    {
        clsMain objMain;
        public ApplicationService()
        {
            objMain = new clsMain();
        }
        
        
        [OperationContract]
        public void DoWork()
        {
            // Add your operation implementation here
            return;
        }


     



        [OperationContract]
        [WebInvoke(Method = "POST")]

        public string KpiGroupList()
        {
            // do not create this object, use session, after use, dospose/free memory

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
    }
}
