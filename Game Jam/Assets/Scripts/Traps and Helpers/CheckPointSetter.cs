using UnityEngine;
using LesserKnown.Player;

namespace LesserKnown.TrapsAndHelpers
{
    /// <summary>
    /// This is a checkpoint
    /// I'm not sure if we're going to use them
    /// But if we are, this is it
    /// </summary>
    public class CheckPointSetter: MonoBehaviour
    {
        private bool isSet;
        private Animator anim;

        private void Start()
        {
            anim = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (isSet)
                return;

            if(collision.tag == "Player")
            {
               /* collision.GetComponent<CharacterController2D>().Checkpoint(transform.position);*/
                isSet = true;
                anim.SetTrigger("Activate");
            }
        }
    }
}
