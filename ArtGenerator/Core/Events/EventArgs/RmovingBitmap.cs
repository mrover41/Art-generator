using ArtGenerator.Core.Features;

namespace ArtGenerator.Core.Events.EventArgs {
    public class RemovingRenderEventArg {
        public RenderBitmap RenderBitmap { get; set; }
        public bool isAllowed { get; set; } = true;
        public ReferenceHub RefHub { get; set; } = null;

        public RemovingRenderEventArg(RenderBitmap renderBitmap, ReferenceHub hub = null) {
            this.RenderBitmap = renderBitmap;
            RefHub = hub;
        }
    }
}
