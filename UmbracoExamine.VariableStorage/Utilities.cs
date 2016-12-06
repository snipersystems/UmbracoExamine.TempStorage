using ClientDependency.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core;

namespace UmbracoExamine.VariableStorage
{
    internal static class Utilities
    {
        internal static string GetPathWithTokensReplaced(string path)
        {
            return path
                .Replace("{machinename}", NetworkHelper.FileSafeMachineName)
                .EnsureEndsWith('/');
        }
    }
}
