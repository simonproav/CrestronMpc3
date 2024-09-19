using proAV.Core.Config;
using proAV.Core.Attributes;
using proAV.Core.Framework;

namespace CrestronMpc3.Configs {
    public class ProjectConfigs : JsonConfigContainer {
        [ConfigName("projectconfig.json")]
        public Mpc3Config Mpc3Config { get; set; }
    }
}