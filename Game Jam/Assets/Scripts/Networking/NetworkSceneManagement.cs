using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeardedManStudios.Forge.Networking.Unity;
namespace LesserKnown.Network
{
    public class NetworkSceneManagement : MonoBehaviour
    {
        public Transform spawner;
        private void Start()
        {
            NetworkManager.Instance.InstantiatePlayer(position: spawner.position);
        }
    }
}
