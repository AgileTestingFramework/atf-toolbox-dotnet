using atf.toolbox.interfaces;
using System;
using System.Configuration;

namespace atf.toolbox.configuration
{
    public class WebDriverSettings : ConfigurationSection, IWebDriverSettings
    {
        private static IWebDriverSettings _settings = ConfigurationManager.GetSection("ReportSettings") as IWebDriverSettings;

        public static IWebDriverSettings WebDriverTestSettings
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

        [ConfigurationProperty("webDriver-browser" , IsRequired = true)]
        [StringValidator(InvalidCharacters = "  ~!@#$%^&*()[]{}/;’\"|\\")]
        public string WebDriverBrowser
        {
            get { return this["webDriver-browser"].ToString().Trim().ToLower(); }
            set { this["webDriver-browser"] = value; }
        }

        [ConfigurationProperty("browser-name" , IsRequired = false)]
        [StringValidator(InvalidCharacters = "  ~!@#$%^&*()[]{}/;’\"|\\")]
        public string BrowserName
        {
            get { return this["browser-name"].ToString().Trim().ToLower(); }
            set { this["browser-name"] = value; }
        }

        [ConfigurationProperty("browser-version" , IsRequired = false)]
        [StringValidator(InvalidCharacters = "  ~!@#$%^&*()[]{}/;’\"|\\")]
        public string BrowserVersion
        {
            get { return this["browser-version"].ToString().Trim().ToLower(); }
            set { this["browser-version"] = value; }
        }

        [ConfigurationProperty("browser-platform" , IsRequired = false)]
        [StringValidator(InvalidCharacters = "  ~!@#$%^&*()[]{}/;’\"|\\")]
        public string BrowserPlatform
        {
            get { return this["browser-platform"].ToString().Trim().ToLower(); }
            set { this["browser-platform"] = value; }
        }

        [ConfigurationProperty("browser-download-path" , IsRequired = false)]
        public string BrowserDownloadPath
        {
            get { return this["browser-download-path"].ToString().Trim(); }
            set { this["browser-download-path"] = value; }
        }

        [ConfigurationProperty("browser-download-prefix" , IsRequired = false)]
        [StringValidator(InvalidCharacters = "  ~!@#$%^&*()[]{}/;’\"|\\")]
        public string BrowserDownloadPrefix
        {
            get { return this["browser-download-prefix"].ToString().Trim(); }
            set { this["browser-download-prefix"] = value; }
        }

        [ConfigurationProperty("driver-path", IsRequired = false)]
        public string DriverPath
        {
            get { return this["driver-path"].ToString().Trim(); }
            set { this["driver-path"] = value; }
        }

        [ConfigurationProperty("use-firebug", IsRequired = false)]
        public bool UseFireBug
        {
            get 
            {
                bool useFirebug;
                Boolean.TryParse(this["use-firebug"].ToString(), out useFirebug);
                return useFirebug;
            }
            set { this["use-firebug"] = value; }
        }

        [ConfigurationProperty("firebug-file", IsRequired = false)]
        public string FirebugFile
        {
            get { return this["firebug-file"].ToString().Trim(); }
            set { this["firebug-file"] = value; }
        }

        [ConfigurationProperty("use-grid", IsRequired = false)]
        public bool UseGrid
        {
            get
            {
                bool useGrid;
                Boolean.TryParse(this["use-grid"].ToString(), out useGrid);
                return useGrid;
            }
            set { this["use-grid"] = value; }
        }

        [ConfigurationProperty("grid-url", IsRequired = false)]
        public string GridURL
        {
            get { return this["grid-url"].ToString().Trim(); }
            set { this["grid-url"] = value; }
        }

        [ConfigurationProperty("browser-proxy", IsRequired = false)]
        public string BrowserProxy
        {
            get { return this["browser-proxy"].ToString().Trim(); }
            set { this["browser-proxy"] = value; }
        }

        [ConfigurationProperty("browser-file-path", IsRequired = false)]
        public string BrowserFilePath
        {
            get { return this["browser-file-path"].ToString().Trim(); }
            set { this["browser-file-path"] = value; }
        }

    }
}
