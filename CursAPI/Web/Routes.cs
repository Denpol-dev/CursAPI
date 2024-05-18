namespace CursAPI.Web
{
    /// <summary>
    /// Пути для эндпоинтов
    /// </summary>
    public class Routes
    {
        private const string StartRouteSegment = "api/v1";

        #region Auth

        private const string AuthRouteSegment = "auth";
        private const string AuthRoute = $"{StartRouteSegment}/{AuthRouteSegment}";

        public const string LoginRoute = $"{AuthRoute}/login";
        public const string RegistrationRoute = $"{AuthRoute}/registration";
        public const string RefreshRoute = $"{AuthRoute}/refresh";

        #endregion

        #region Book

        private const string BookRouteSegment = "book";
        private const string BookRoute = $"{StartRouteSegment}/{BookRouteSegment}";

        public const string AllBooks = $"{BookRoute}/all";

        #endregion
    }
}
