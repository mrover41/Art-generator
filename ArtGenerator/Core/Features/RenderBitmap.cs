using AdminToys;
using ArtGenerator.Core.Events.EventHandles;
using Exiled.API.Features;
using MEC;
using Mirror;
using System;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

namespace ArtGenerator.Core.Features {
    public class RenderBitmap {
        Bitmap _bitmap;
        CoroutineHandle _renderCoroutine;
        public RenderBitmap(Bitmap bitmap) { 
            _bitmap = bitmap;
        }

        public void Render(float scale, Vector3 position) {
            if (!Handles.OnRunningRenderEvent(new Events.EventArgs.RunningRenderEventArg(this)).isAllowed) return;
            _renderCoroutine = Timing.RunCoroutine(Render(_bitmap, scale, position), Segment.Update);
            Handles.OnRunRenderEvent(new Events.EventArgs.RunRenderEventArg(this));
        }

        public void StopRender() {
            if (_renderCoroutine.IsRunning) {
                Timing.KillCoroutines(_renderCoroutine);
            }
        }

        IEnumerator<float> Render(Bitmap bitmap, float size, Vector3 position) {
            Log.Info($"Generating art at: {position}");
            for (int x = 0; x < bitmap.Width; x++) {
                for (int y = 0; y < bitmap.Height; y++) {
                    try {
                        int invertedY = bitmap.Height - 1 - y;
                        System.Drawing.Color color = bitmap.GetPixel(x, invertedY);
                        string hex = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
                        Log.Info($"Spawning cube at {x},{y} Color: {hex}");
                        //GameObject obj = PrefabHelper.Spawn(Exiled.API.Enums.PrefabType.PrimitiveObjectToy, new Vector3(position.x + x * size, position.y + y * size, position.z), Quaternion.identity);
                        GameObject obj = UnityEngine.Object.Instantiate(Exiled.API.Features.PrefabHelper.GetPrefab(Exiled.API.Enums.PrefabType.PrimitiveObjectToy), new Vector3(position.x + x * size, position.y + y * size, position.z), Quaternion.identity);
                        obj.transform.localScale = new Vector3(size * 1.5f, size * 1.5f, size * 1.5f);
                        obj.GetComponent<PrimitiveObjectToy>().NetworkMaterialColor = new UnityEngine.Color(color.R / 255f, color.G / 255f, color.B / 255f);
                        //obj.GetComponent<Renderer>().material.SetMaterialType(MaterialId.LitStandard);
                        NetworkServer.Spawn(obj);
                    } catch (Exception ex) {
                        Log.Error($"[ART ERROR] at ({x},{y}): {ex}");
                    }

                    yield return Timing.WaitForOneFrame;
                }
            }
            Handles.OnEndRenderEvent(new Events.EventArgs.EndRenderEventArg(this));
        }
    }
}
