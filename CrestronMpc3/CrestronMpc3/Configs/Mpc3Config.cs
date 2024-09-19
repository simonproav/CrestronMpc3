using proAV.Core.Config;
using Newtonsoft.Json;

namespace CrestronMpc3.Configs {
    public class Mpc3Config : ConfigBase {
        [JsonProperty("connection")]
        public ConnectionConfig Connection { get; set; }
    }
}