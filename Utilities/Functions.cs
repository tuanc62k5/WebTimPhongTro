using Microsoft.AspNetCore.Http;

namespace DoAn.Utilities
{
    public static class Functions
    {
        public static bool IsLogin(HttpContext context)
        {
            return context.Session.GetInt32("TK_ID") != null;
        }

        public static void Logout(HttpContext context)
        {
            context.Session.Clear();
        }
    }
}