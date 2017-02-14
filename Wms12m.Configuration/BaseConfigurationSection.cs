using System.Configuration;

namespace Wms12m.Configuration
{
    public class BaseConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("service")]
        public ServiceConfigurationElement Service
        {
            get { return (ServiceConfigurationElement)this["service"]; }
            set { this["service"] = value; }
        }

        [ConfigurationProperty("presentation")]
        public PresentationConfigurationElement Presentation
        {
            get { return (PresentationConfigurationElement)this["presentation"]; }
            set { this["presentation"] = value; }
        }

        public static BaseConfigurationSection Current
        {
            get
            {
                return (BaseConfigurationSection)ConfigurationManager.GetSection("Wms12m/Base");
            }
        }
    }
    public class ServiceConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("dataConnectionString", IsRequired = true)]
        public string DataConnectionString
        {
            get { return this["dataConnectionString"].ToString(); }
            set { this["dataConnectionString"] = value; }
        }
    }
    public class PresentationConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("application", IsRequired = true)]
        public string Application
        {
            get { return this["application"].ToString(); }
            set { this["application"] = value; }
        }

        [ConfigurationProperty("channel", IsRequired = true)]
        public string Channel
        {
            get { return this["channel"].ToString(); }
            set { this["channel"] = value; }
        }
    }
}
