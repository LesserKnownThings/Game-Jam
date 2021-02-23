using UnityEngine;
using LesserKnown.Public;
using LesserKnown.Interfaces;
using LesserKnown.Network;
using LesserKnown.UI;

namespace LesserKnown.Player
{
    public class ResourceManager : MonoBehaviour, IEnemy_Interface
    {
        private AnimManager anim_manager;
        private PlayerNetwork player_network;
        private HUDManager hud_manager;

        public int health = 9;
        public int gold = 0;
        
        private void Start()
        {
            anim_manager = GetComponent<AnimManager>();
            player_network = GetComponent<PlayerNetwork>();
            hud_manager = FindObjectOfType<HUDManager>();
        }

        private void Update()
        {
            hud_manager.Set_Amounts(health, gold);
        }
        public void Get_Hit(int damage)
        {
            anim_manager.Get_Hit();
            player_network.Get_HitRPC(damage);
        }
    }
}
