using ArtGenerator.Core.Extensions;
using ArtGenerator.Core.Features;
using CommandSystem;
using Exiled.API.Features;
using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Corwarx_Gameplay.Commands {
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    internal class OpenImage : ICommand {
        public string Command => "Generate_art";

        public string[] Aliases => new[] { "ar", "ga" };

        public string Description => "Generate art";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response) {
            response = "Use: Generate_art <Full path from image> <compression ratio (recommended 5)>";
            if (arguments.Count != 2) return false; response = "File is null";
            if (!File.Exists(arguments.FirstElement())) return false; response = "Value not int";
            if (!int.TryParse(arguments.Last(), out int scale)) return false;

            Bitmap bitmap = arguments.FirstElement().FindBitmap().Scale(scale);

            Player player = Player.Get(sender);
            new RenderBitmap(bitmap).Render(bitmap.CalculateScale(), player.Position, player.Rotation.SnapRotation(20), false);

            response = "Done";
            return true;
        }
    }
}
