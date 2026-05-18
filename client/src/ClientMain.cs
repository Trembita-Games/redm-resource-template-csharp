using CitizenFX.Core;
using RedmResourceTemplateCSharp.Shared.Data;
using RedmResourceTemplateCSharp.Shared.Logging;

namespace RedmResourceTemplateCSharp.Client
{
    public class ClientMain : BaseScript
    {
        public ClientMain()
        {
            LogHelper.Debug(Debug.WriteLine, "Client runtime initializing.");

            foreach (var entry in ResourceData.ClientEntries)
            {
                LogHelper.Debug(Debug.WriteLine, entry.Name + ": " + entry.Version);
            }

            LogHelper.Debug(Debug.WriteLine, "Client runtime initialized.");
        }
    }
}
