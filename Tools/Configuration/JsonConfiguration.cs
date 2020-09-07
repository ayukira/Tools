using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Primitives;

namespace Tools
{
    public sealed class AppConfiguration
    {
        public static IConfiguration Configuration { get; set; }
        static AppConfiguration()
        {
            Configuration = new JsonConfiguration("appsettings").Configuration;
        }
        private AppConfiguration() { }
    }
    public class JsonConfiguration
    {
        public Action OnChange;
        public IConfiguration Configuration { get; set; }
        public JsonConfiguration(string jsonName, bool reloadOnChange = true)
        {
            Configuration = new ConfigurationBuilder()
                .Add(new JsonConfigurationSource { Path = $"{jsonName}.json", ReloadOnChange = reloadOnChange })
                .Build();
            ChangeToken.OnChange(() => Configuration.GetReloadToken(), () =>
            {
                OnChange?.Invoke();
            });
        }
    }
}