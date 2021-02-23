using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LesserKnown.Player;
using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using LesserKnown.Public;

namespace LesserKnown.Network
{
    public class PlayerNetwork : PlayerBehavior
    {
        public Behaviour[] disable_scripts;
        [HideInInspector]
        public bool left;

        private ResourceManager resource_manager;

        private void Start()
        {          
            resource_manager = GetComponent<ResourceManager>();
        }

        public override void GetHit(RpcArgs args)
        {
            resource_manager.health -= args.GetNext<int>();        
        }

        public void Get_HitRPC(int damage)
        {
            networkObject.SendRpc(RPC_GET_HIT, Receivers.AllBuffered, damage);

        }

        protected override void NetworkStart()
        {
            base.NetworkStart();

            if (!networkObject.IsOwner)
            {
                gameObject.layer = 8;
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
                left = networkObject.lookleft;     
            }

            networkObject.position = transform.position;
            networkObject.rotation = transform.rotation;
            networkObject.lookleft = left;
        }
    }
}
