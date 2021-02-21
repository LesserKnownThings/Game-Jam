using UnityEngine;

namespace LesserKnown.AI
{
    [System.Serializable]
    public class SpawnedObject
    {
        public GameObject obj_spawn;
        public float spawn_amount;
        public float spawn_delay;
        public float spawn_cd;
    }
}
