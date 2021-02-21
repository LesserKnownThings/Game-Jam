using LesserKnown.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DangerZone : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            CharacterController2D player = other.GetComponent<CharacterController2D>();
            player.Dead();
        }
    }

}