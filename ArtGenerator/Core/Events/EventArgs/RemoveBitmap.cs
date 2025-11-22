using ArtGenerator.Core.Features;

namespace ArtGenerator.Core.Events.EventArgs {
    public class RemoveRenderEventArg {
        public RenderBitmap RenderBitmap { get; set; }
        public ReferenceHub RefHub { get; set; } = null;
        public RemoveRenderEventArg(RenderBitmap renderBitmap, ReferenceHub hub = null) {
            this.RenderBitmap = renderBitmap;
            RefHub = hub;
        }
    }
}
