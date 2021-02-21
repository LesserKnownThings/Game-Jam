using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LesserKnown.Player;
using LesserKnown.Public;
using System.Linq;

namespace LesserKnown.AI {
    public class AiControlPoisonChamp : MonoBehaviour, Enemy_Interface
    {
        public float HP_ENEMY = 10;
        private Animator anim;
        public float appear_distance = 8f;

        private CharacterController2D[] players;

        public float spawn_delay = 4f;
        private float current_delay;
        public bool is_appeared = false;

        public GameObject fakePoisonCloud; //GREEN FAKE POISON CIRCLE
        public float poisonTime = 4f; //nb de temps entre poison cloud

        private void Start()
        {
            anim = GetComponent<Animator>();
            InvokeRepeating("Appear", 0, .1f);

            current_delay = spawn_delay;

            players = FindObjectsOfType<CharacterController2D>();
        }

        private void Update()
        {                
                    if (current_delay >= spawn_delay)
                    {
                        foreach (var player in Verify_Distance(appear_distance - 2f))
                        {
                    PoisonBurst(false);
                    PoisonBurst(true);
                            //if (PublicVariables.MERGED_PLAYER)
                            if (PublicVariables.IS_FUSIONED)
                                player.Get_Hit(1);
                            else
                                player.Dead();
                        }
                        current_delay = 0f;
                    }
                    current_delay += Time.deltaTime;
            
        }

        private void Appear()
        {           

            anim.SetBool("Appear", Verify_Distance(appear_distance).Count > 0);
        }

        /// <summary>
        /// Activates the Mushroom Burst Periodic Poison Region
        /// </summary>
        public void PoisonBurst(bool poison_active) //VISUAL TEMPORARY , WITH A CIRCLE
        {
            //add a PLaySoundAttack           
                fakePoisonCloud.SetActive(poison_active);
        }


     

        public List<CharacterController2D> Verify_Distance(float distance_to_use)
        {
            List<CharacterController2D> _players = new List<CharacterController2D>();

            foreach (var item in players)
            {
                Vector3 _direction = item.transform.position - transform.position;
                float _distance = _direction.magnitude;

                if (_distance <= distance_to_use)
                    _players.Add(item);
            }

            return _players;
        }
        /// <summary>
        /// Activates the Mushroom Death animation
        /// </summary>
        public void Death()
        {
            anim.SetTrigger("Death");
            HP_ENEMY = 0;
        }

        /// <summary>
        /// Activates the MushRoom Hurt animation
        /// </summary>
        public void Hurt()
        {
            anim.SetTrigger("Hurt");
            HP_ENEMY -= 1;
        }

        public void Get_Hit(int amount)
        {
            Death();
            Destroy(gameObject, .5f);
        }
    }
}