using atf.toolbox.interfaces;
using System;
using System.Configuration;

namespace atf.toolbox.configuration
{
    public class ReportSettings : ConfigurationSection, IReportSettings
    {
        private static IReportSettings _settings = ConfigurationManager.GetSection("ReportSettings") as IReportSettings;

        public static IReportSettings ReportTestSettings
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

        [ConfigurationProperty("jserrorcollection-with-firefox", IsRequired = false)]
        public bool JSErrorCollectionWithFireFox
        {
            get
            {
                bool collectErrors;
                Boolean.TryParse(this["jserrorcollection-with-firefox"].ToString(), out collectErrors);
                return collectErrors;
            }
            set { this["jserrorcollection-with-firefox"] = value; }
        }
    }
}
