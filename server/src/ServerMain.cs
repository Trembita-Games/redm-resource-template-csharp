using CitizenFX.Core;
using RedmResourceTemplateCSharp.Shared.Data;
using RedmResourceTemplateCSharp.Shared.Logging;

namespace RedmResourceTemplateCSharp.Server
{
    public class ServerMain : BaseScript
    {
        public ServerMain()
        {
            Debug.WriteLine("-------------- Server runtime initialized.");

            LogHelper.Info(Debug.WriteLine, "Server runtime initializing.");

            foreach (var entry in ResourceData.ServerEntries)
            {
                LogHelper.Debug(Debug.WriteLine, entry.Name + ": " + entry.Version);
            }

            LogHelper.Info(Debug.WriteLine, "Server runtime initialized.");
        }
    }
}
