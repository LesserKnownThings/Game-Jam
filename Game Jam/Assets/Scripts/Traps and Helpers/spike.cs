using LesserKnown.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class spike : MonoBehaviour
{

    public CharacterController2D player1;
    public CharacterController2D player2;

    public GameObject TPLock1;
    public GameObject TPLock2;
    public GameObject Target1;
    public GameObject Target2;

    void start()
    {

    
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject == Target1)
        {
            CharacterController2D player1 = other.GetComponent<CharacterController2D>();
            player1.Dead();



            StartCoroutine(Teleport());
        }
        if (other.gameObject == Target2)
        {

            CharacterController2D player1 = other.GetComponent<CharacterController2D>();
            player1.Dead();
            StartCoroutine(Teleport2());
        }
    }

    IEnumerator Teleport()
    {
    
        yield return new WaitForSeconds(0.5f);
        Target1.transform.position = TPLock2.transform.position;
    }
    IEnumerator Teleport2()
    {
        yield return new WaitForSeconds(0.5f);
        Target2.transform.position = TPLock1.transform.position;
    }


    public void city1()
    {
        //telport scrip
        Target1.gameObject.transform.position = TPLock1.gameObject.transform.position;
    }
    public void city2()
    {
        //telport scrip
        Target1.gameObject.transform.position = TPLock2.gameObject.transform.position;
    }


}