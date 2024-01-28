using Microsoft.AspNetCore.Mvc.Filters;

namespace CityInfo.API.ActionFilters;

public class ResponseHeaderAttribute : ActionFilterAttribute
{
    private readonly string _name;
    private readonly string _value;

    public ResponseHeaderAttribute(string name, string value)
    {
        _name = name;
        _value = value;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        context.HttpContext.Response.Headers.Append(_name, _value);
    }
}
