﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgilusFinan.Web.ErrorHandling
{
    public class NotFoundActionResult : ActionResult
    {
        private string _viewName;
        public NotFoundActionResult()
        {

        }
        public NotFoundActionResult(string viewName)
        {
            _viewName = viewName;
        }
        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.StatusCode = 404;
            context.HttpContext.Response.TrySkipIisCustomErrors = true;

            new ViewResult { ViewName = string.IsNullOrEmpty(_viewName) ? "Error" : _viewName }.ExecuteResult(context);
        }
    }
}