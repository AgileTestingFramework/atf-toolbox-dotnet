using atf.toolbox.interfaces;
using System;
using System.Configuration;

namespace atf.toolbox.configuration
{
    public class FormSettings : ConfigurationSection, IApplicationSettings
    {
        protected static IApplicationSettings _settings = ConfigurationManager.GetSection("FormSettings") as IApplicationSettings;

        public static IApplicationSettings FormTestSettings
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

        [ConfigurationProperty("application-directory", IsRequired = false)]
        public string ApplicationDirectory
        {
            get { return this["application-directory"].ToString().Trim(); }
            set { this["application-directory"] = value; }
        }

        [ConfigurationProperty("application-startfilename", IsRequired = false)]
        public string ApplicationStartFileName
        {
            get { return this["application-startfilename"].ToString().Trim(); }
            set { this["application-startfilename"] = value; }
        }

        [ConfigurationProperty("application-launch-parameters", IsRequired = false)]
        public string ApplicationLaunchParameters
        {
            get { return this["application-launch-parameters"].ToString().Trim(); }
            set { this["application-launch-parameters"] = value; }
        }        
        
        [ConfigurationProperty("appliation-always-launch-new-instance", IsRequired = false)]
        public bool AlwaysLaunchNewInstance
        {
            get
            {
                bool alwaysLaunch;
                Boolean.TryParse(this["appliation-always-launch-new-instance"].ToString(), out alwaysLaunch);
                return alwaysLaunch;
            }
            set { this["appliation-always-launch-new-instance"] = value; }
        }
    }
}
