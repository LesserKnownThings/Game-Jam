using UnityEngine;
using LesserKnown.Player;
using LesserKnown.Public;

namespace LesserKnown.Collectibles
{
    public class Collectible: MonoBehaviour
    {
        public bool is_coin;
        private Animator anim;

        private void Start()
        {
            anim = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.tag == "Player")
            {
                CharacterController2D player = other.GetComponent<CharacterController2D>();

                anim.SetTrigger("Collect");
                Destroy(gameObject, .5f);

                if (player.is_fighter && !is_coin)                
                    PublicVariables.APPLES++;
                else if (!player.is_fighter && is_coin)
                    PublicVariables.COINS++;

            }
        }
    }
}
