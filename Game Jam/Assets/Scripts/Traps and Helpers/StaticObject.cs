using UnityEngine;
using System.Collections;
using LesserKnown.Player;

namespace LesserKnown.TrapsAndHelpers
{
    public class StaticObject : MonoBehaviour
    {
        public float death_timer = 7f;
        private Rigidbody2D rb;
        public bool is_attatched;

        private void Start()
        {
            if (is_attatched)
            {
                death_timer = Mathf.Infinity;
            }
            
        }
        public void Activate()
        {
            gameObject.SetActive(true);
            StartCoroutine(DespawnIE());
        }

        private IEnumerator DespawnIE()
        {
            yield return new WaitForSeconds(death_timer);
            transform.localPosition = Vector3.zero;
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Player")
            {
               /* collision.GetComponent<CharacterController2D>().Die();*/
            }
        }

      
    }
}
