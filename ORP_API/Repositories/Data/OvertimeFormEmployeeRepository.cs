﻿using Dapper;
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
        public IConfiguration Configuration { get; }
        public OvertimeFormEmployeeRepository(MyContext myContext, IConfiguration configuration) : base(myContext)
        {
            myContext.Set<OvertimeFormEmployee>();
            this.myContext = myContext;
            this.Configuration = configuration;
        }

        public int ApplyRequest(RequestViewModels requestViewModels)
        {
            var addRequest = new OvertimeFormEmployee()
            {
                NIK = requestViewModels.NIK,  //get from session
                CustomerId = requestViewModels.CustomerId,
                OvertimeFormId = requestViewModels.OvertimeFormId,
                Status = StatusRequest.Waiting
            };

            myContext.Add(addRequest);
            var resultRequest = myContext.SaveChanges();

            if (resultRequest > 0)
            {
                RequestViewModels result = null;

                string connectStr = Configuration.GetConnectionString("MyConnection");
                var employeeCondition = myContext.Employee.Where(a => a.NIK == requestViewModels.NIK).FirstOrDefault();

                if (employeeCondition != null)
                {
                    using (IDbConnection db = new SqlConnection(connectStr))
                    {
                        string readSp = "sp_get_email_employee";
                        var parameter = new { NIK = requestViewModels.NIK, CustomerId = requestViewModels.CustomerId };
                        result = db.Query<RequestViewModels>(readSp, parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();
                    }
                }
                sendEmail.SendNotificationToEmployee(result.Email);

                RequestViewModels result2 = null;
                using (IDbConnection db = new SqlConnection(connectStr))
                {
                    string readSp = "sp_get_email_supervisor";
                    var parameter = new { CustomerId = result.CustomerId, RoleId = 3 };
                    result2 = db.Query<RequestViewModels>(readSp, parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
                sendEmail.SendNotificationToSupervisor(result2.Email);

                return 1;
            }
            else
            {
                return 0;
            }
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
                RequestViewModels result3 = null;

                string connectStr = Configuration.GetConnectionString("MyConnection");
                using (IDbConnection db = new SqlConnection(connectStr))
                {
                    string readSp = "sp_get_email_relational_manager";
                    var parameter = new { CustomerId = 1, RoleId = 2 };
                    result3 = db.Query<RequestViewModels>(readSp, parameter, commandType: CommandType.StoredProcedure).FirstOrDefault();
                }
                sendEmail.SendNotificationToRelationalManager(result3.Email);
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
    }
}
