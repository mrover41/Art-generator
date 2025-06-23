using ArtGenerator.Core.Features;

namespace ArtGenerator.Core.Events.EventArgs {
    public class RunningRenderEventArg {
        public RenderBitmap RenderBitmap { get; set; }
        public bool isAllowed { get; set; } = true;

        public RunningRenderEventArg(RenderBitmap renderBitmap) {
            this.RenderBitmap = renderBitmap;
        }
    }
}
