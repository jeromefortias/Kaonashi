namespace Nexai.Kaonashi.Core.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    public static class ConfigMgt
    {
        public static T? GetFromFile<T>(string fullName) where T : new()
        {
            try
            {
                string json = File.ReadAllText(fullName);
                return JsonSerializer.Deserialize<T>(json) ?? new T();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
