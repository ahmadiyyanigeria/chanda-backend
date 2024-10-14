using Hangfire.Dashboard;

namespace API.Filters
{
    public class AllowAllUsersAuthorizationFilter : Hangfire.Dashboard.IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            // Example: Allow all users access (you can customize this as needed)
            return true;
        }
    }
}
