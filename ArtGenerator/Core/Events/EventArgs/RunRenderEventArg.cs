using ArtGenerator.Core.Features;

namespace ArtGenerator.Core.Events.EventArgs {
    public class RunRenderEventArg {
        public RenderBitmap RenderBitmap { get; set; }

        public RunRenderEventArg(RenderBitmap renderBitmap) {
            this.RenderBitmap = renderBitmap;
        }
    }
}
