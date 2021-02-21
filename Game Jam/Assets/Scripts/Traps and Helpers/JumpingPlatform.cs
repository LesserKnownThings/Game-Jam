using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LesserKnown.Player;

namespace LesserKnown.TrapsAndHelpers {
    /// <summary>
    /// This class controlls the jumping platforms
    /// If you have better ideas on how to do it you're welcome to change it
    /// </summary>
    public class JumpingPlatform : MonoBehaviour
    {
        public float trigger_delay = 1f;
        public float jump_force = 5000f;
        private bool jump;
        private Coroutine trigger_coroutine;
        private Animator anim;

        private void Start()
        {
            anim = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                trigger_coroutine =  StartCoroutine(JumpIE(collision.GetComponent<CharacterController2D>()));
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                StopCoroutine(trigger_coroutine);
            }
        }

        private IEnumerator JumpIE(CharacterController2D controller)
        {
            yield return new WaitForSeconds(trigger_delay);

            anim.SetTrigger("Activate");
            yield return new WaitForSeconds(.1f);
            controller.Jump(jump_force);           
        }
    }
}