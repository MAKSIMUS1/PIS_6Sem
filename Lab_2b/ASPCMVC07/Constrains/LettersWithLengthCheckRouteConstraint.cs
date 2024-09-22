using System.Globalization;

namespace ASPCMVC07.Constrains
{
    public class LettersWithLengthCheckRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {

            if (values.TryGetValue(routeKey, out var routeValue))
            {
                var parameterValueString = Convert.ToString(routeValue, CultureInfo.InvariantCulture);
                return parameterValueString.Length >= 3 && parameterValueString.Length <= 4;
            }

            return false;
        }
    }
}
