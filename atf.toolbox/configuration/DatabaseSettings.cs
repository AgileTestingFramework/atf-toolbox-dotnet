using atf.toolbox.interfaces;
using System.Configuration;

namespace atf.toolbox.configuration
{
    public class DatabaseSettings : ConfigurationSection, IDatabaseSettings
    {
        private static IDatabaseSettings _settings = ConfigurationManager.GetSection("DatabaseSettings") as IDatabaseSettings;

        public static IDatabaseSettings DatabaseTestSettings
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

        [ConfigurationProperty("db-connection-string-name", IsRequired = false)]
        public string DatabaseConnectionStringName
        {
            get { return this["db-connection-string-name"].ToString().Trim(); }
            set { this["db-connection-string-name"] = value; }
        }
    }
}
