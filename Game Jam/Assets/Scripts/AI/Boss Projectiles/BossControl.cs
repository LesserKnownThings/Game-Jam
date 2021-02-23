using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LesserKnown.Player;

namespace LesserKnown.AI
{
    public class BossControl : MonoBehaviour
    {
        public int boss_health = 15;
        private bool is_invicible;
        public Transform current_player;
        private Animator anim;

        [Space(10)]
        [Header("Objects to Spawn")]
        public GameObject light_flash;
        public float fire_flash_delay = .5f;
        private float current_flash_delay = .5f;

        public float fire_flash_cd = 5f;
        private float fire_flash_current_cd;
        public float spell_duration = 3f;
        public float movement_cd = 1.5f;
        public float current_movement_cd;

        private SpriteRenderer sprite;
        private void Start()
        {
            current_flash_delay = fire_flash_delay;
            fire_flash_current_cd = fire_flash_cd;
            anim = GetComponent<Animator>();
            current_movement_cd = movement_cd;
            sprite = GetComponent<SpriteRenderer>();
        }

      
        public void Spawn_Boss(Transform player)
        {
            current_player = player;
        }

        private void Update()
        {
            if (current_player == null)
                return;

            Vector3 _dir = (transform.position - current_player.position).normalized;

                sprite.flipX = _dir.x < 0;


            if(fire_flash_current_cd >= fire_flash_cd)
            {
                StartCoroutine(Fire_IE());
                fire_flash_current_cd = 0f;
            }
            fire_flash_current_cd += Time.deltaTime;

            if (!is_invicible)
            {
                if(current_movement_cd >= movement_cd)
                {
                    transform.position -= new Vector3(_dir.x / 2f,0,0);
                    anim.SetTrigger("Move");
                    current_movement_cd = 0f;
                }
                current_movement_cd += Time.deltaTime;
            }
        }

     
        private bool Player_Too_Close()
        {
            Vector3 _direction = transform.position - current_player.position;
            float _distance = _direction.magnitude;

            if (_distance <= 2f)
                return true;

            return false;
        }



        private IEnumerator Fire_IE()
        {
            float current_duration = 0f;
            anim.SetTrigger("Hide");
            is_invicible = true;
            current_movement_cd = 0f;
            while(current_duration < spell_duration)
            {

                if (current_flash_delay >= fire_flash_delay)
                {
                    current_flash_delay = 0f;
                    var _copy = Instantiate(light_flash, current_player.position - new Vector3(0,0,5), Quaternion.identity);
                }
                current_flash_delay += Time.deltaTime;

                current_duration += Time.deltaTime;
                yield return null;
            }
            anim.SetTrigger("Appear");
            is_invicible = false;
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
                current_player = collision.transform;
        }

        public void Get_Hit(int amount)
        {
            if (is_invicible)
                return;

            boss_health -= amount;
            anim.SetTrigger("Hit");

            Vector3 _dir = (transform.position - current_player.position).normalized;
            transform.position += new Vector3(_dir.x/2f, 0, 0);
        }
    }
}
