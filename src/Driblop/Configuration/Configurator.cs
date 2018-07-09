using System.IO;
using Newtonsoft.Json;

namespace Driblop.Configuration {
    public class Configurator<TConfig> where TConfig : new() {
        public TConfig ensureFile(string path) {
            if (File.Exists(path)) {
                return JsonConvert.DeserializeObject<TConfig>(File.ReadAllText(path));
            }
            var config = new TConfig();
            File.WriteAllText(path, JsonConvert.SerializeObject(config, Formatting.Indented));
            return config;
        }
    }
}