using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Linq;

namespace TrainScheduler.Core.Helpers
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }

        public static bool TryGet<T>(this ISession session, string key, out T value)
        {
            if (session.Keys.Contains(key))
            {
                var valueJson = session.GetString(key);
                value = JsonSerializer.Deserialize<T>(valueJson);
                return true;
            }

            value = default;
            return false;
        }
    }
}
