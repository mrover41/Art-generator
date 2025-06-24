using UnityEngine;

namespace ArtGenerator.Core.Extensions {
    public static class Qut {
        public static Quaternion SnapRotation(this Quaternion q, float snapAngle = 45f) {
            Vector3 euler = q.eulerAngles;

            euler.x = Mathf.Round(euler.x / snapAngle) * snapAngle;
            euler.y = Mathf.Round(euler.y / snapAngle) * snapAngle;
            euler.z = Mathf.Round(euler.z / snapAngle) * snapAngle;

            return Quaternion.Euler(euler);
        }
    }
}
