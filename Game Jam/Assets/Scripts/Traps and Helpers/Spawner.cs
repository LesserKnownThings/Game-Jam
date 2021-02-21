using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LesserKnown.TrapsAndHelpers
{
    /// <summary>
    /// A class that's used to spwan any object
    /// It uses a pool for more efficiency instead of Instantiate
    /// </summary>
    public class Spawner : MonoBehaviour
    {
        public float reset_delay = 6f;
        public float spawn_delay = 1f;
        private float current_delay = 0f;
        public int pool_amount = 10;
        public bool static_objects;
        public Vector3 mover_vector;
        public GameObject pool_object;

        public List<Mover> pool = new List<Mover>();
        public List<StaticObject> static_pool = new List<StaticObject>();

        private void Start()
        {
            if (!static_objects)
                Spawn_Pool();
            else
                Spawn_Static_Pool();
        }

        private void Update()
        {
            if(current_delay >= spawn_delay)
            {
                if (!static_objects)
                    Move();
                else
                    Move_Static();


                current_delay = 0f;
            }

            current_delay += Time.deltaTime;
        }
        private void Move()
        {
            int _index = pool.FindIndex(x => { return !x.gameObject.activeSelf; });

            if (_index != -1)
            {
                pool[_index].Move();
                pool[_index].Reset_Timer(reset_delay);
            }
        }

        private void Move_Static()
        {
            int _index = static_pool.FindIndex(x => { return !x.gameObject.activeSelf; });

            if (_index != -1)
                static_pool[_index].Activate();
        }

        private void Spawn_Pool()
        {
            for (int i = 0; i < pool_amount; i++)
            {
                var _copy = Instantiate(pool_object, Vector3.zero, Quaternion.identity);
                _copy.transform.SetParent(transform, false);
                _copy.SetActive(false);
                Mover _mover = _copy.GetComponent<Mover>();
                _mover.movement_vector = mover_vector;
                pool.Add(_mover);
            }
        }

        private void Spawn_Static_Pool()
        {
            for (int i = 0; i < pool_amount; i++)
            {
                var _copy = Instantiate(pool_object, Vector3.zero, Quaternion.identity);
                _copy.transform.SetParent(transform, false);
                StaticObject _static = _copy.GetComponent<StaticObject>();
                _copy.SetActive(false);
                static_pool.Add(_static);
            }
        }
    }
}
