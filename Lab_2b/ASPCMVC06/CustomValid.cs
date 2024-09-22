namespace ASPCMVC06
{
    public class CustomValid : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var action = values["action"] as string;
            var id = values["id"] as string;
            if (action == "M01")
            {
                return id == "1";
            }
            return false;
        }
    }
}
