using Exiled.API.Features;

namespace ArtGenerator {
    internal class Loader : Plugin<Config> {
        public static Loader Instance { get; private set; }

        public Loader() => Instance = this;

        public override void OnEnabled() {
            base.OnEnabled();
        }

        public override void OnDisabled() {
            base.OnDisabled();
        }
    }
}
