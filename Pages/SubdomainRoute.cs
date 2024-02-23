using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Memories.Pages
{
    public class SubdomainRoute 
    { 
        //public SubdomainRoute(string template, string name, IInlineConstraintResolver constraintResolver, RouteValueDictionary defaults, IDictionary<string, object> constraints, RouteValueDictionary dataTokens) : base(template, name, constraintResolver, defaults, constraints, dataTokens)
        //{
        //}

        //protected override Task OnRouteMatched(RouteContext httpContext)
        //{
        //    var host = httpContext.Request.Url.Host;
        //    var index = host.IndexOf(".");
        //    string[] segments = httpContext.Request.Url.PathAndQuery.Split('/');
        //    if (index < 0)
        //        return null;
        //    var subdomain = host.Substring(0, index);
        //    string controller = (segments.Length > 0) ? segments[0] : "Home";
        //    string action = (segments.Length > 1) ? segments[1] : "Index";
        //    var routeData = new RouteData(this, new MvcRouteHandler());
        //    routeData.Values.Add("controller", controller);
        //    routeData.Values.Add("action", action);
        //    routeData.Values.Add("subdomain", subdomain);
        //    return routeData;
        //}

        //protected override VirtualPathData OnVirtualPathGenerated(VirtualPathContext context)
        //{
        //    throw new System.NotImplementedException();
        //}
    }
}
