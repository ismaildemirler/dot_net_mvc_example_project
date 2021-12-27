﻿using Domain.Infrastructure.CrossCuttingConcerns.Security.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Domain.Web.Areas.Admin.Controllers
{
    [DomainAuthorize(Roles = "Admin")]
    public class DomainController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}