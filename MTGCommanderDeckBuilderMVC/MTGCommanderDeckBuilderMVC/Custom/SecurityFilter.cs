using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MTGCommanderDeckBuilderMVC.Custom
{
    public class SecurityFilter : ActionFilterAttribute
    {
        //Dependencies
        private readonly string _Key;
        private readonly object[] _Args;

        //Constructor
        public SecurityFilter(string key, params object[] args)
        {
            _Key = key;
            _Args = args;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            if(session[_Key] == null || !_Args.Contains(session[_Key]))
            {
                filterContext.Result = new RedirectResult("/Account/Login", false);
            }

            base.OnActionExecuting(filterContext);
        }
    }
}