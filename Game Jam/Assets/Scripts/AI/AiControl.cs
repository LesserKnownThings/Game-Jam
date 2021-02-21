using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LesserKnown.AI
{
    public class AiControl : MonoBehaviour
    {
        public GameObject[] forms;
        public float deadly_form_duration = 5f;
        public float deadly_form_cd = 25f;
        public float current_deadly_cd = 0f;

        private bool can_mutate;
        private bool player_close;

        [Space(10)]
        [Header("Objects to Spawn")]
        public SpawnedObject saw;
        public SpawnedObject spikes;
        public SpawnedObject balls;

        private Transform player;



        private void Start()
        {
            InvokeRepeating("Player_Close", 25f, .1f);
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Update()
        {
            if (can_mutate && player_close)
            {
                if(current_deadly_cd <= 0)
                Mutate();
            }
        }

        private void Mutate()
        {
            current_deadly_cd = deadly_form_cd;
            forms[0].SetActive(false);
            forms[1].SetActive(true);
        }

        private IEnumerator MutateIE()
        {

            yield return null;
        }

        public void Player_Close()
        {
            var _dir = transform.position - player.position;
            var _dist = _dir.magnitude;

            if (_dist <= 6f)
                player_close = true;
            else
                player_close = false;
        }
    }
}
