using UnityEngine;
using LesserKnown.Player;
public class SpikesBehaviour : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
           CharacterController2D _player = collision.GetComponent<CharacterController2D>();

            //You will have to make this function in the CharacterController2d
            //_player.Damage(); => so you can create a function inside the CharacterController2d which is name Damage or Died or whatever you like
            //The AnimManager is already initiated in the class CharacterController2d , so you only need to apply the reset position you made and call anim_manager.Die()
        }
    }
}
