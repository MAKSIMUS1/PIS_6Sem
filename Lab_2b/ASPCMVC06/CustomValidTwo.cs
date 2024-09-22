namespace ASPCMVC06
{
    public class CustomValidTwo : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var action = values["MResearch"] as string;
            var str = values["str"] as string;

            if (action == null) return true;

            if (str == "") return false;

            return true;
        }
    }
}
