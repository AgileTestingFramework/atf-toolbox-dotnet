using atf.toolbox.interfaces;
using System.Configuration;

namespace atf.toolbox.configuration
{
    public class WebSettings : ConfigurationSection, IWebSettings
    {
        protected static IWebSettings _settings = ConfigurationManager.GetSection("WebSettings") as IWebSettings;

        public static IWebSettings WebTestSettings
        {
            get
            {
                return _settings;
            }
        }

        public override bool IsReadOnly()
        {
            return false;
        }

        [ConfigurationProperty("base-URL" , IsRequired = false)]
        public string BaseURL
        {
            get { return this["base-URL"].ToString().Trim(); }
            set { this["base-URL"] = value; }
        }

        [ConfigurationProperty("application-context", IsRequired = false)]
        public string ApplicationContext
        {
            get { return this["application-context"].ToString().Trim(); }
            set { this["application-context"] = value; }
        }
    }
}
