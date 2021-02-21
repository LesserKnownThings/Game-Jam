using UnityEngine;
using LesserKnown.Player;
using LesserKnown.Public;
using LesserKnown.Audio;

namespace LesserKnown.Collectibles
{
    public class Collectible : MonoBehaviour
    {
        public AudioClip[] appleSnd;
        public AudioClip[] coinSnd;
        
        public bool is_coin;
        private Animator anim;
        private AudioSource playerSource;
        private void Start()
        {
            anim = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                CharacterController2D player = other.GetComponent<CharacterController2D>();
                playerSource = other.GetComponent<AudioSource>();
                anim.SetTrigger("Collect");
                Destroy(gameObject, .5f);

                if (player.is_fighter && !is_coin)
                    PublicVariables.APPLES++;
                else if (!player.is_fighter && is_coin)
                    PublicVariables.COINS++;

            }
        }

        public void PlaySoundApple()
        {
            SetRandomVariations();
            playerSource.PlayOneShot(appleSnd[Random.Range(0, appleSnd.Length - 1)]);
        }
        public void PlaySoundCoin()
        {
            SetRandomVariations();
            playerSource.PlayOneShot(coinSnd[Random.Range(0, coinSnd.Length - 1)]);
        }
        void SetRandomVariations()
        {
            playerSource.pitch = (float)Random.Range(0.8f,1.2f);
            playerSource.volume = (float)Random.Range(0.5f, 0.7f);
        }

    }
}
