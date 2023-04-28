using Microsoft.AspNetCore.Http;
using System.Text;

namespace Business.Extensions
{
    public static class AccessorExtensions
    {
        public static void SetString(this ISession session, string key, string value)
        {
            session.Set(key, Encoding.Default.GetBytes(value));
        }

        public static string GetString(this ISession session, string key)
        {
            session.TryGetValue(key, out byte[] value);
            return value is not null ? Encoding.Default.GetString(value) : string.Empty;
        }
    }
}