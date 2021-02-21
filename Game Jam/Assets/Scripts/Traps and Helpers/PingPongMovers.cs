using UnityEngine;

namespace LesserKnown.TrapsAndHelpers
{
    /// <summary>
    /// This class does exactly what you think it does
    /// It playes ping pong
    /// And yeah it moves an object from one place and back in a ping pong matter
    /// </summary>
    public class PingPongMovers: MonoBehaviour
    {
        public float speed = 5f;
        public Vector3 left_angle;
        public Vector3 right_angle;

        private void Update()
        {
            var t = Mathf.PingPong(Time.time * speed, 1f);
            transform.rotation = Quaternion.Slerp(Quaternion.Euler(left_angle), Quaternion.Euler(right_angle), t);
        }
    }
}
