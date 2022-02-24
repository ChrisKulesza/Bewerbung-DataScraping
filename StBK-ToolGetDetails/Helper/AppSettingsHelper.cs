using System.Collections.Specialized;
using System.Configuration;

namespace StBK_ToolGetDetails.Helper
{
    public class AppSettingsHelper
    {
        /// <summary>
        /// Gets the value of a key value pair in the app settings.
        /// </summary>
        /// <param name="section">The section in the app settings</param>
        /// <param name="key">The key of the key value pair</param>
        /// <returns>The value of the key value pair as a <see cref="string"/></returns>
        public static string ReadSettings(string section, string key)
        {
            string value = string.Empty;

            try
            {
                var tmpSection = (NameValueCollection)ConfigurationManager.GetSection(section);
                value = tmpSection[key] ?? "Not found";
            }
            catch (ConfigurationErrorsException)
            {
                System.Console.WriteLine("Error reading app settings.");
            }

            return value;
        }
    }
}