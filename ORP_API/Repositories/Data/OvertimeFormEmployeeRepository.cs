using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
    public class OvertimeFormEmployeeRepository : GeneralRepository<OvertimeFormEmployee, MyContext, int>
    {
        private readonly MyContext myContext;
        private readonly SendEmail sendEmail = new SendEmail();
        private readonly OvertimeFormRepository overtimeFormRepository;
        public IConfiguration Configuration { get; }
        public OvertimeFormEmployeeRepository(MyContext myContext, IConfiguration configuration, OvertimeFormRepository overtimeFormRepository) : base(myContext)
        {
            myContext.Set<OvertimeFormEmployee>();
            this.myContext = myContext;
            this.Configuration = configuration;
            this.overtimeFormRepository = overtimeFormRepository;
        }

        public int SupervisorApproval(OvertimeFormEmployee overtimeFormEmployee)
        {
            var result = new OvertimeFormEmployee()
            {
                Id = overtimeFormEmployee.Id,
                CustomerId = overtimeFormEmployee.CustomerId,
                NIK = overtimeFormEmployee.NIK,
                Status = overtimeFormEmployee.Status,
                OvertimeFormId = overtimeFormEmployee.OvertimeFormId
            };

            myContext.Entry(result).State = EntityState.Modified;
            var finalResult = myContext.SaveChanges();

            if (result.Status == StatusRequest.ApproveBySupervisor)
            {
                OvertimeFormViewModels result3 = null;

                string connectStr = Configuration.GetConnectionString("MyConnection");
                using (IDbConnection db = new SqlConnection(connectStr))
                {
                    string readSp = "sp_get_email_relational_manager";
                    var parameter = new { CustomerId = 1, RoleId = 2 };
                    result3 = db.Query<OvertimeFormViewModels>(readSp, parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
                sendEmail.SendNotificationToRelationalManager(result3.Email);
            }
            else
            {
                OvertimeFormViewModels result3 = null;

                string connectStr = Configuration.GetConnectionString("MyConnection");
                using (IDbConnection db = new SqlConnection(connectStr))
                {
                    string readSp = "sp_get_email_employee";
                    var parameter = new { NIK = overtimeFormEmployee.NIK, CustomerId = overtimeFormEmployee.CustomerId };
                    result3 = db.Query<OvertimeFormViewModels>(readSp, parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
                sendEmail.SendRejectNotificationToEmployee(result3.Email);
            }

            return finalResult;
        }

        public int RelationalManagerApproval(OvertimeFormEmployee overtimeFormEmployee)
        {
            var result = new OvertimeFormEmployee()
            {
                Id = overtimeFormEmployee.Id,
                CustomerId = overtimeFormEmployee.CustomerId,
                NIK = overtimeFormEmployee.NIK,
                Status = overtimeFormEmployee.Status,
                OvertimeFormId = overtimeFormEmployee.OvertimeFormId
            };

            myContext.Entry(result).State = EntityState.Modified;
            var finalResult = myContext.SaveChanges();

            if (result.Status == StatusRequest.ApproveByRelatonalManager)
            {
                RequestViewModels result3 = null;

                string connectStr = Configuration.GetConnectionString("MyConnection");
                using (IDbConnection db = new SqlConnection(connectStr))
                {
                    string readSp = "sp_get_email_employee";
                    var parameter = new { NIK = overtimeFormEmployee.NIK, CustomerId = overtimeFormEmployee.CustomerId };
                    result3 = db.Query<RequestViewModels>(readSp, parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
                sendEmail.SendApproveNotificationToEmployee(result3.Email);
            }
            else
            {
                RequestViewModels result3 = null;

                string connectStr = Configuration.GetConnectionString("MyConnection");
                using (IDbConnection db = new SqlConnection(connectStr))
                {
                    string readSp = "sp_get_email_employee";
                    var parameter = new { NIK = overtimeFormEmployee.NIK, CustomerId = overtimeFormEmployee.CustomerId };
                    result3 = db.Query<RequestViewModels>(readSp, parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
                sendEmail.SendRejectNotificationToEmployee(result3.Email);

            }

            return finalResult;
        }

        public IEnumerable<OvertimeFormEmployee> GetSpecificForm(string NIK)
        {
            var getInfo = myContext.Employee.Where(a => a.NIK == NIK).FirstOrDefault();

            if (getInfo == null)
            {
                return null;
            }
            var getResult = myContext.OvertimeFormEmployee.Where(b => b.CustomerId == getInfo.CustomerId).AsEnumerable();
            
            return getResult;
        }
    }
}
