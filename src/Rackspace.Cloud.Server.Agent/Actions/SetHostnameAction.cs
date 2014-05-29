using System;
using System.Management;
using Rackspace.Cloud.Server.Common.Logging;

namespace Rackspace.Cloud.Server.Agent.Actions
{
    public class SetHostnameAction : ISetHostnameAction
    {
        private readonly ILogger _logger;

        public SetHostnameAction(ILogger logger)
        {
            _logger = logger;
        }

        public string SetHostname(string hostname)
        {
            var oldName = Environment.MachineName;
            _logger.Log("Old host name: " + oldName);
            _logger.Log("New host name: " + hostname);
            if (string.IsNullOrEmpty(hostname) || oldName.Equals(hostname, StringComparison.InvariantCultureIgnoreCase))
                return 0.ToString();

            using (var cs = new ManagementObject(@"Win32_Computersystem.Name='" + oldName + "'"))
            {
                cs.Get();
                var inParams = cs.GetMethodParameters("Rename");
                inParams.SetPropertyValue("Name", hostname);
                var methodOptions = new InvokeMethodOptions(null, TimeSpan.MaxValue);
                var outParams = cs.InvokeMethod("Rename", inParams, methodOptions);
                if (outParams == null)
                    return 1.ToString();

                return Convert.ToString(outParams.Properties["ReturnValue"].Value);
            }
        }
    }
}