using Microsoft.Extensions.Configuration;
using ORP_API.Context;
using ORP_API.Models;
using ORP_API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORP_API.Repositories.Data
{
    public class OvertimeFormRepository : GeneralRepository<OvertimeForm, MyContext, int>
    {
        private readonly MyContext myContext;
        public OvertimeFormRepository(MyContext myContext) : base(myContext)
        {
            myContext.Set<OvertimeForm>();
            this.myContext = myContext;
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
            //List<DetailOvertimeRequest> data = new List<DetailOvertimeRequest>();
            for(int i = 0; i < overtimeFormViewModels.listdetails.Count; i++)
            {
                TimeSpan difference = overtimeFormViewModels.listdetails[i].EndTime - overtimeFormViewModels.listdetails[i].StartTime;
                int totalHours = difference.Hours;
                var listdata = new DetailOvertimeRequest();
                listdata.StartTime = overtimeFormViewModels.listdetails[i].StartTime;
                listdata.EndTime = overtimeFormViewModels.listdetails[i].EndTime;
                listdata.Act = overtimeFormViewModels.listdetails[i].Act;
                listdata.AdditionalSalary = totalHours * 100000;
                listdata.OvertimeFormId = overtimeForm.Id;
                //data.Add(listdata);
                myContext.Add(listdata);
            };
            var resultDetails = myContext.SaveChanges();

            if (resultOvertimeForm > 0 && resultDetails > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        }
}
