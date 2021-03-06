﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ORP_API.Base.Controller;
using ORP_API.Models;
using ORP_API.Repositories.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ORP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetailOvertimeRequestController : BaseController<DetailOvertimeRequest, DetailOvertimeRequestRepository, int>
    {
        public DetailOvertimeRequestController(DetailOvertimeRequestRepository detailOvertimeRequestRepository) : base(detailOvertimeRequestRepository)
        {

        }
    }
}
