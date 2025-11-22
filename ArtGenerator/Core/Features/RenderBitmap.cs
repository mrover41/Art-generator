using ArtGenerator.Core.Events.EventHandles;
using Exiled.API.Features;
using Exiled.API.Features.Toys;
using MEC;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using UnityEngine;

namespace ArtGenerator.Core.Features
{
    public class RenderBitmap {
        Bitmap _bitmap;
        CoroutineHandle _renderCoroutine;
        public int millis = 50;
        public List<Primitive> SpawnedObjects { get; private set; } = new List<Primitive>();
        public float Progress => (float)SpawnedObjects.Count / (_bitmap.Width * _bitmap.Height);
        public RenderBitmap(Bitmap bitmap) { 
            _bitmap = bitmap;
        }

        public void Render(float scale, Vector3 position, Quaternion rotation, bool isCollided = false) {
            if (!Handles.OnRunningRenderEvent(new Events.EventArgs.RunningRenderEventArg(this)).isAllowed) return;
            _renderCoroutine = Timing.RunCoroutine(Render(_bitmap, scale, position, rotation, !isCollided), Segment.Update);
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

            Vector3 centerOffset = new Vector3(bitmap.Width * size, bitmap.Height * size, 0f) * 0.5f;

            var data = new System.Drawing.Color[bitmap.Width, bitmap.Height];

            for (int x = 0; x < bitmap.Width; x++)
                for (int y = 0; y < bitmap.Height; y++)
                    data[x, y] = bitmap.GetPixel(x, bitmap.Height - 1 - y); //Чтоб не дёргать диск

            for (int x = 0; x < bitmap.Width; x++) {
                for (int y = 0; y < bitmap.Height; y++) {
                    //int invertedY = bitmap.Height - 1 - y;
                    System.Drawing.Color color = data[x, y];
                    //string hex = $"#{color.R:X2}{color.G:X2}{color.B:X2}";

                    Vector3 localOffset = new Vector3(x * size, y * size, 0f) - centerOffset;
                    Vector3 rotatedOffset = rotation * localOffset;
                    Vector3 finalPosition = position + rotatedOffset; //Работает - не трогай)

                    Primitive primitve = Primitive.Create(PrimitiveType.Cube, finalPosition, spawn: false);
                    primitve.Transform.localScale = new Vector3(size, size, size);
                    primitve.Transform.rotation = rotation;
                    primitve.Collidable = false;

                    primitve.Color = new UnityEngine.Color(color.R / 255f, color.G / 255f, color.B / 255f);
                    primitve.IsStatic = true;

                    primitve.Spawn();

                    SpawnedObjects.Add(primitve);

                    if (stopwatch.ElapsedMilliseconds >= millis) {
                        stopwatch.Restart();
                        yield return Timing.WaitForOneFrame;
                    }
                }
            }
            Handles.OnEndRenderEvent(new Events.EventArgs.EndRenderEventArg(this));
        }
    }
}
