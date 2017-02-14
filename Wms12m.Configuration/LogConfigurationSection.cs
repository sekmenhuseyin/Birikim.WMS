using System;
using System.Configuration;

namespace Wms12m.Configuration
{
    public class LogConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("enabled", DefaultValue = "false", IsRequired = false)]
        public bool Enabled
        {
            get { return (bool)this["enabled"]; }
            set { this["enabled"] = value; }
        }

        [ConfigurationProperty("database")]
        public DatabaseConfigurationElement Database
        {
            get { return (DatabaseConfigurationElement)this["database"]; }
            set { this["database"] = value; }
        }

        public static LogConfigurationSection Current
        {
            get
            {
                return (LogConfigurationSection)ConfigurationManager.GetSection("Wms12m/Log");
            }
        }
    }

    public class DatabaseConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return this["name"].ToString(); }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("enabled", IsRequired = false, DefaultValue = "false")]
        public bool Enabled
        {
            get { return (Boolean)this["enabled"]; }
            set { this["enabled"] = value; }
        }

        [ConfigurationProperty("severity", IsRequired = true, DefaultValue = "Information")]
        public string Severity
        {
            get { return this["severity"].ToString(); }
            set { this["severity"] = value; }
        }

        [ConfigurationProperty("connectionString", IsRequired = true)]
        public string ConnectionString
        {
            get { return this["connectionString"].ToString(); }
            set { this["connectionString"] = value; }
        }
    }
}
