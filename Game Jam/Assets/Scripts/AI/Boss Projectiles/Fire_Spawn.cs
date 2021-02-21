using UnityEngine;
using System.Collections;

namespace LesserKnown.AI
{
    public class Fire_Spawn: MonoBehaviour
    {
        public float death_timer;
        public float fire_death_timer;
        public GameObject fire_spell;

        private void Start()
        {
            StartCoroutine(Delay_FireIE());
        }

        private IEnumerator Delay_FireIE()
        {
            yield return new WaitForSeconds(death_timer);
            var _copy = Instantiate(fire_spell, transform.position, Quaternion.identity);
            Destroy(_copy, fire_death_timer);
            Destroy(gameObject, fire_death_timer);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            
        }
    }
}
