using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace LesserKnown.TrapsAndHelpers 
{
    /// <summary>
    /// This class makes an object move in the direction that you want
    /// Usually it's used with Spwaner class
    /// </summary>
    public class Mover : MonoBehaviour
    {
        [HideInInspector]
        public Vector3 movement_vector;
        private bool move_bool;

       public void Reset_Timer(float reset_delay)
        {
            StartCoroutine(Reset_TimerIE(reset_delay));
        }

        private IEnumerator Reset_TimerIE(float reset_delay)
        {
            yield return new WaitForSeconds(reset_delay);
            Reset_Object();
        }

        public void Move()
        {
            gameObject.SetActive(true);
            StartCoroutine(MoveIE());
        }
        
        private void Reset_Object()
        {
            move_bool = false;
            transform.localPosition = new Vector3(0,0,0);
            gameObject.SetActive(false);
        }

        private IEnumerator MoveIE()
        {
            move_bool = true;
            while (move_bool)
            {
                transform.position += movement_vector * Time.deltaTime;
                yield return null;
            }
        }
    }
}
