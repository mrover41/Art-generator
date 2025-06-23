using ArtGenerator.Core.Features;

namespace ArtGenerator.Core.Events.EventArgs {
    public class EndRenderEventArg {
        public RenderBitmap RenderBitmap { get; set; }

        public EndRenderEventArg(RenderBitmap renderBitmap) {
            this.RenderBitmap = renderBitmap;
        }
    }
}
