# ℹ Art generator for SCP: SL
Art genetarot API is a plugin developed for use with the EXILED framework in SCP: Secret Laboratory.

## ⭐ Features
- Generating art from an image
  - API
  - Events
- Modular and extensible design for adding new functionalities.

# 📁 Installation and Configuration
- [Installation Guide](https://github.com/northwood-studios/LabAPI/wiki/Installing-Plugins)  
- [Configuration Guide](https://github.com/northwood-studios/LabAPI/wiki/Configuring-Plugins)

1. Ensure you have the EXILED framework installed on your SCP: Secret Laboratory server.
   - Follow the installation guide at [EXILED Documentation](https://github.com/Exiled-Team/EXILED/wiki/Installation).

2. Download the compiled plugin (`ArtGenerator.dll`) or compile it yourself from the source.

3. Place the plugin in the `Plugins` folder of your server directory:
   ```
   SCP Secret Laboratory
   └── EXILED
       └── Plugins
           └── ArtGenerator.dll
   ```

> [!NOTE]
> **You can add this plugin depending on your project (plugin).**

4. Restart your server to load the plugin.

# 🌐 Authors
`Mr_Over41`

# Images
![image](https://github.com/user-attachments/assets/8d72b26e-2389-4acc-a487-d71013054ba5)

# Examples for developer

generate art
```c#
string arguments = "C:\\Images\Art.png";

Bitmap bitmap = arguments.FindBitmap().Scale(scale);

new RenderBitmap(bitmap).Render(bitmap.CalculateScale(), Player.Get(sender).Position);
```

events
```c#
private void OnEnable() {
  Handles._runningRenderEvent += RenderBegining;
  Handles._runRenderEvent += Render;
  Handles._endRenderEvent += RenderEnd;
}

private void OnDisable() {
  Handles._runningRenderEvent -= RenderBegining;
  Handles._runRenderEvent -= Render;
  Handles._endRenderEvent -= RenderEnd;
}

private void RenderBegining(RunningRenderEventArg ev) {
  if (Round.isLobby) ev.isAllowed = false;
}

private void Render(RunRenderEventArg ev) {
  //ev.RenderBitmap.StopRender();
}

private void RenderEnd(EndRenderEventArg ev) {
  Log.Warn("Render is ended!");
}
```


