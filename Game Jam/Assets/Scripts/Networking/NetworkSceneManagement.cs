using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Unity;
namespace LesserKnown.Network
{
    public class NetworkSceneManagement : MonoBehaviour
    {
        public List<Transform> spawners = new List<Transform>();

        private void Awake()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                spawners.Add(transform.GetChild(i));
            }
        }

        private void Start()
        {
            int _random = Random.Range(0, spawners.Count);

            NetworkManager.Instance.InstantiatePlayer(position: spawners[_random].position);
        }
    }
}
