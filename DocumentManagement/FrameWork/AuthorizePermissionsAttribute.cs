//using Microsoft.AspNet.Identity;
//using System;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;
//using System.Web.Http.Controllers;

//namespace DocumentManagement.FrameWork
//{
//    [AttributeUsageAttribute(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
//    public class AuthorizePermissionsAttribute : AuthorizeAttribute
//    {
//        public string PermissionName { get; set; }
//        public string GroupingName { get; set; }
//        public AuthorizePermissionsAttribute(params string[] roles) : base()
//        {
//        }
//        protected override bool IsAuthorized(HttpActionContext actionContext)
//        {
//            bool rt = PermissionHelper.HasEffectivePermission(int.Parse(actionContext.RequestContext.Principal.Identity.GetUserId()), PermissionName, GroupingName);
//            return rt;
//        }
//        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
//        {
//            actionContext.Response = new HttpResponseMessage
//            {

//                StatusCode = HttpStatusCode.Forbidden,
//                Content = new StringContent("You do not have permission to access this function!")
//            };
//        }
//    }
//}
