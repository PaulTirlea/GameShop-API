namespace SummerPracticePaul.App_Start
{
    public class RouteConfig
    {
        public static string RoutePattern { get; private set; } = "api/{controller}/{action}/{id?}";

        public static void ConfigureRoutes(IEndpointRouteBuilder endpoints)
        {
            MapApiRoutes(endpoints);
        }

        private static void MapApiRoutes(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: RoutePattern
            );
        }
    }
}
