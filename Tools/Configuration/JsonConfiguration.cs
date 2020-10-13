using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Primitives;

namespace Tools
{
    /// <summary>
    /// AppConfiguration
    /// </summary>
    public sealed class AppConfiguration
    {
        static readonly JsonConfiguration app;
        /// <summary>
        /// AppConfigChangeEvent
        /// </summary>
        public static Action OnChange;
        /// <summary>
        /// AppConfigInstance
        /// </summary>
        public static IConfiguration Configuration { get; set; }
        static AppConfiguration()
        {
            app = new JsonConfiguration("appsettings");
            Configuration = app.Configuration;
            ChangeToken.OnChange(() => Configuration.GetReloadToken(), () =>
            {
                var now = DateTime.Now.ToTimeStamp();
                if ((now - app.ChangeTime) <= JsonConfiguration.Time) { return; }
                app.ChangeTime = now;
                OnChange?.Invoke();
            });
        }
        private AppConfiguration() { }
    }
    /// <summary>
    /// JsonConfiguration
    /// </summary>
    public class JsonConfiguration
    {
        public const uint Time = 500;
        public long ChangeTime = 0;
        /// <summary>
        /// ConfigChangeEvent
        /// </summary>
        public Action OnChange;
        /// <summary>
        /// ConfigInstance
        /// </summary>
        public IConfiguration Configuration { get; set; }
        /// <summary>
        /// ConfigInit
        /// </summary>
        /// <param name="jsonName"></param>
        /// <param name="reloadOnChange"></param>
        public JsonConfiguration(string jsonName, bool reloadOnChange = true)
        {
            Configuration = new ConfigurationBuilder()
                .Add(new JsonConfigurationSource { Path = $"{jsonName}.json", ReloadOnChange = reloadOnChange })
                .Build();
            ChangeToken.OnChange(() => Configuration.GetReloadToken(), () =>
            {
                var now = DateTime.Now.ToTimeStamp();
                if ((now - ChangeTime) <= Time) { return; }
                ChangeTime = now;
                OnChange?.Invoke();
            });
        }
    }
}