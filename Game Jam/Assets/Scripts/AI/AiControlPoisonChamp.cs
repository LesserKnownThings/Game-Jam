using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LesserKnown.Player;
using LesserKnown.Public;
public class AiControlPoisonChamp : MonoBehaviour
{
    private Animator anim;
    public float appear_distance = 8f;
    public List<CharacterController2D> players;
    public List<CharacterController2D> players_in_danger;

    public float spawn_delay = 2f;
    private float current_delay;

    private void Start()
    {
        anim = GetComponent<Animator>();
        InvokeRepeating("Appear", 0, .1f);

        current_delay = spawn_delay;

    }

    private void Update()
    {
        if(players_in_danger.Count > 0)
        {
            if(current_delay >= spawn_delay)
            {
                foreach (var player in players_in_danger)
                {
                    if (PublicVariables.IS_FUSIONED)
                        player.Get_Hit(1);
                    else
                        player.Dead();
                }
                current_delay = 0f;
            }
            current_delay += Time.deltaTime;
        }
    }

    private void Appear()
    {
        anim.SetBool("Appear", Verify_Distance());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            players_in_danger.Add(other.GetComponent<CharacterController2D>());
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
            players_in_danger.Remove(other.GetComponent<CharacterController2D>());
    }

    public bool Verify_Distance()
    {
        foreach (var item in players)
        {
            Vector3 _direction = item.transform.position - transform.position;
            float _distance = _direction.magnitude;

            if (_distance <= appear_distance)
                return true;
        }

        return false;
    }

}
