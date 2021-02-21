using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LesserKnown.Player;
using BeardedManStudios.Forge.Networking.Generated;

namespace LesserKnown.Network
{
    public class PlayerNetwork : PlayerBehavior
    {
        public Behaviour[] disable_scripts;
        public bool left;
        public Sprite sprite;

        protected override void NetworkStart()
        {
            base.NetworkStart();

            if (!networkObject.IsOwner)
            {
                foreach (var item in disable_scripts)
                {
                    item.enabled = false;
                }
            }
            else
            {
                UnityEngine.Camera.main.GetComponent<LesserKnown.Camera.CameraFollow>().Set_Camera(transform, GetComponent<CharacterController2D>());
            }
                
        }

        private void Update()
        {            
            if (networkObject == null)
                return;

            if (!networkObject.IsOwner)
            {
                transform.position = networkObject.position;
                transform.rotation = networkObject.rotation;
                left = networkObject.look_left;
            }

            networkObject.position = transform.position;
            networkObject.rotation = transform.rotation;
            networkObject.look_left = left;
        }
    }
}
