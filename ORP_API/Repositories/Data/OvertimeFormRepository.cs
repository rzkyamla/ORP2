using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ORP_API.Context;
using ORP_API.Handler;
using ORP_API.Models;
using ORP_API.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ORP_API.Repositories.Data
{
    public class OvertimeFormRepository : GeneralRepository<OvertimeForm, MyContext, int>
    {
        private readonly SendEmail sendEmail = new SendEmail();
        private readonly MyContext myContext;
        public IConfiguration Configuration { get; }
        public OvertimeFormRepository(MyContext myContext, IConfiguration configuration) : base(myContext)
        {
            myContext.Set<OvertimeForm>();
            this.myContext = myContext;
            this.Configuration = configuration;
        }

        public int Apply(OvertimeFormViewModels overtimeFormViewModels)
        {
            DateTime date = DateTime.Now;
            var overtimeForm = new OvertimeForm()
            {
                Name = overtimeFormViewModels.Name,
                SubmissionDate = date,
                CustomerId = overtimeFormViewModels.CustomerId
            };
            myContext.Add(overtimeForm);
            var resultOvertimeForm = myContext.SaveChanges();
            for (int i = 0; i < overtimeFormViewModels.listdetails.Count; i++)
            {

                var listdata = new DetailOvertimeRequest();
                listdata.StartTime = overtimeFormViewModels.listdetails[i].StartTime;
                listdata.EndTime = overtimeFormViewModels.listdetails[i].EndTime;
                listdata.Act = overtimeFormViewModels.listdetails[i].Act;
                listdata.AdditionalSalary = overtimeFormViewModels.listdetails[i].AdditionalSalary;
                listdata.OvertimeFormId = overtimeForm.Id;

                myContext.Add(listdata);
            };
            var resultDetails = myContext.SaveChanges();

            var condition = myContext.OvertimeForm.Where(a => a.Name == overtimeFormViewModels.Name).FirstOrDefault();

            if (condition != null)
            {
                OvertimeFormViewModels result = null;

                string connectStr = Configuration.GetConnectionString("MyConnection");

                using (IDbConnection db = new SqlConnection(connectStr))
                {
                    string readSp = "sp_get_info";
                    var parameter = new { Name = overtimeFormViewModels.Name };
                    result = db.Query<OvertimeFormViewModels>(readSp, parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }

                OvertimeFormViewModels result2 = null;

                using (IDbConnection db = new SqlConnection(connectStr))
                {
                    string readSp = "sp_get_id";
                    var parameter = new { Id = condition.Id + (result.Amount - 1) };
                    result2 = db.Query<OvertimeFormViewModels>(readSp, parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }

                var addRequest = new OvertimeFormEmployee()
                {
                    NIK = overtimeFormViewModels.NIK,
                    CustomerId = overtimeFormViewModels.CustomerId,
                    OvertimeFormId = result2.Id,
                    Status = StatusRequest.Waiting
                };

                myContext.Add(addRequest);
            }
            var resultRequest = myContext.SaveChanges();

            if (resultOvertimeForm > 0 && resultDetails > 0 && resultRequest > 0)
            {
                OvertimeFormViewModels result = null;

                string connectStr = Configuration.GetConnectionString("MyConnection");
                var employeeCondition = myContext.Employee.Where(a => a.NIK == overtimeFormViewModels.NIK).FirstOrDefault();

                if (employeeCondition != null)
                {
                    using (IDbConnection db = new SqlConnection(connectStr))
                    {
                        string readSp = "sp_get_email_employee";
                        var parameter = new { NIK = overtimeFormViewModels.NIK, CustomerId = overtimeFormViewModels.CustomerId };
                        result = db.Query<OvertimeFormViewModels>(readSp, parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    }
                }
                sendEmail.SendNotificationToEmployee(result.Email);

                OvertimeFormViewModels result2 = null;
                using (IDbConnection db = new SqlConnection(connectStr))
                {
                    string readSp = "sp_get_email_supervisor";
                    var parameter = new { CustomerId = result.CustomerId, RoleId = 3 };
                    result2 = db.Query<OvertimeFormViewModels>(readSp, parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
                sendEmail.SendNotificationToSupervisor(result2.Email);
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
