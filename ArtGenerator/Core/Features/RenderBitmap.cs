using AdminToys;
using ArtGenerator.Core.Events.EventHandles;
using Exiled.API.Features;
using MEC;
using Mirror;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using UnityEngine;

namespace ArtGenerator.Core.Features {
    public class RenderBitmap {
        Bitmap _bitmap;
        CoroutineHandle _renderCoroutine;
        public List<GameObject> SpawnedObject { get; private set; } = new List<GameObject>();
        public float Progress => (float)SpawnedObject.Count / (_bitmap.Width * _bitmap.Height);
        public RenderBitmap(Bitmap bitmap) { 
            _bitmap = bitmap;
        }

        public void Render(float scale, Vector3 position, Quaternion rotation, bool isCollided = false) {
            if (!Handles.OnRunningRenderEvent(new Events.EventArgs.RunningRenderEventArg(this)).isAllowed) return;
            _renderCoroutine = Timing.RunCoroutine(Render(_bitmap, scale, position, rotation, !isCollided), Segment.Update);
            Handles.OnRunRenderEvent(new Events.EventArgs.RunRenderEventArg(this));
        }

        [Obsolete("Use Render(float scale, Vector3 position, Quaternion rotation) instead. This method will be removed in future versions.")]
        public void Render(float scale, Vector3 position, bool isCollided = false) {
            if (!Handles.OnRunningRenderEvent(new Events.EventArgs.RunningRenderEventArg(this)).isAllowed) return;
            _renderCoroutine = Timing.RunCoroutine(Render(_bitmap, scale, position, Quaternion.identity, !isCollided), Segment.Update);
            Handles.OnRunRenderEvent(new Events.EventArgs.RunRenderEventArg(this));
        }

        public void StopRender() {
            if (_renderCoroutine.IsRunning) {
                Timing.KillCoroutines(_renderCoroutine);
            }
        }

        IEnumerator<float> Render(Bitmap bitmap, float size, Vector3 position, Quaternion rotation, bool collision) {
            Log.Warn($"Generating art at: {position}");
            Stopwatch stopwatch = Stopwatch.StartNew();
            stopwatch.Start();
            for (int x = 0; x < bitmap.Width; x++) {
                for (int y = 0; y < bitmap.Height; y++) {
                    try {
                        int invertedY = bitmap.Height - 1 - y;
                        System.Drawing.Color color = bitmap.GetPixel(x, invertedY);
                        string hex = $"#{color.R:X2}{color.G:X2}{color.B:X2}";
                        Log.Debug($"Spawning cube at {x},{y} Color: {hex}");
                        //GameObject obj = PrefabHelper.Spawn(Exiled.API.Enums.PrefabType.PrimitiveObjectToy, new Vector3(position.x + x * size, position.y + y * size, position.z), Quaternion.identity);

                        Vector3 centerOffset = new Vector3(bitmap.Width * size, bitmap.Height * size, 0f) * 0.5f;
                        Vector3 localOffset = new Vector3(x * size, y * size, 0f) - centerOffset;
                        Vector3 rotatedOffset = rotation * localOffset;
                        Vector3 finalPosition = position + rotatedOffset; //Работает - не трогай)

                        GameObject obj = UnityEngine.Object.Instantiate(Exiled.API.Features.PrefabHelper.GetPrefab(Exiled.API.Enums.PrefabType.PrimitiveObjectToy), finalPosition, Quaternion.identity);
                        obj.transform.localScale = new Vector3(size, size, size);
                        obj.transform.rotation = rotation;

                        NetworkServer.Spawn(obj);

                        PrimitiveObjectToy primitiveObjectToy = obj.GetComponent<PrimitiveObjectToy>();
                        primitiveObjectToy.NetworkMaterialColor = new UnityEngine.Color(color.R / 255f, color.G / 255f, color.B / 255f);
                        primitiveObjectToy.NetworkPosition = obj.transform.position;
                        primitiveObjectToy.NetworkPrimitiveType = PrimitiveType.Cube;
                        primitiveObjectToy.NetworkIsStatic = true; // Устанавливаем объект как статический
                        //primitiveObjectToy._collider.isTrigger = collision;

                        SpawnedObject.Add(obj);
                    } catch (Exception ex) {
                        Log.Error($"[ART ERROR] at ({x},{y}): {ex}");
                    }

                    if (stopwatch.ElapsedMilliseconds >= 50) {
                        stopwatch.Restart();
                        yield return Timing.WaitForOneFrame;
                    }
                }
            }
            Handles.OnEndRenderEvent(new Events.EventArgs.EndRenderEventArg(this));
        }
    }
}
