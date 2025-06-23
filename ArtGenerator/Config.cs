using Exiled.API.Interfaces;
using System.ComponentModel;

namespace ArtGenerator {
    internal class Config : IConfig {
        [Description("Plugin config")]
        public bool IsEnabled { get; set; } = true;
        public bool Debug { get; set; }
    }
}
