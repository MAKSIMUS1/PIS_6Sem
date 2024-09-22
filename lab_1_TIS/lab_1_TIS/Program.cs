using static System.Net.WebRequestMethods;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        const string FIO = "KMO";

        app.MapGet("/{path}.KMO", (string parmA, string parmB) => $"GET-Http-{FIO}:ParmA = {parmA},ParmB = {parmB}");

        app.MapGet("/html", (HttpContext context) =>
        {
            var filePath = "index1.html";

            if (System.IO.File.Exists(filePath))
            {
                var htmlContent = System.IO.File.ReadAllText(filePath);

                context.Response.Headers.Add("Content-Type", "text/html");

                return context.Response.WriteAsync(htmlContent);
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                return Task.CompletedTask;
            }
        });

        app.MapGet("/html-form", (HttpContext context) =>
        {
            var filePath = "index2.html";

            if (System.IO.File.Exists(filePath))
            {
                var htmlContent = System.IO.File.ReadAllText(filePath);

                context.Response.Headers.Add("Content-Type", "text/html");

                return context.Response.WriteAsync(htmlContent);
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                return Task.CompletedTask;
            }
        });

        app.MapPost("/{path}.KMO", (string parmA, string parmB) => $"POST-Http-{FIO}:ParmA = {parmA},ParmB = {parmB}");

        app.MapPost("/calculate/sum", (HttpContext context) =>
        {
            var parmX = context.Request.Form["parmX"];
            var parmY = context.Request.Form["parmY"];

            if (int.TryParse(parmX, out int x) && int.TryParse(parmY, out int y))
            {
                var sum = x + y;
                return context.Response.WriteAsync($"Сумма чисел {parmX} и {parmY} равна {sum}");
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                return context.Response.WriteAsync("Ошибка: неверные параметры");
            }
        });

        app.MapPost("/calculate/mult", (HttpContext context) =>
        {
            var parmX = context.Request.Form["x"];
            var parmY = context.Request.Form["y"];

            if (int.TryParse(parmX, out int x) && int.TryParse(parmY, out int y))
            {
                var product = x * y;
                return context.Response.WriteAsync($"The product of the numbers {parmX} and {parmY} is equal to {product}");
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                return context.Response.WriteAsync("Ошибка: неверные параметры");
            }
        });


        app.MapPut("/{path}.KMO", (string parmA, string parmB) => $"PUT-Http-{FIO}:ParmA = {parmA},ParmB = {parmB}");

        app.Run();
    }
}