using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LesserKnown.Player;
using LesserKnown.Public;
public class AiControlPoisonChamp : MonoBehaviour
{
    public float HP_ENEMY = 10;
    private Animator anim;
    public float appear_distance = 8f;
    public List<CharacterController2D> players;
    public List<CharacterController2D> players_in_danger;

    private float spawn_delay = 2f;
    private float current_delay;
    public bool poison_is_active;
    public bool is_appeared = false;

    public Renderer fakePoisonCloud; //GREEN FAKE POISON CIRCLE
    public float poisonTime = 4f; //nb de temps entre poison cloud

    private void Start()
    {
        anim = GetComponent<Animator>();
        InvokeRepeating("Appear", 0, .1f);

        current_delay = spawn_delay;
        fakePoisonCloud.enabled = false; 
    }

    private void Update()
    {
        if (poison_is_active)
        {
            if (players_in_danger.Count > 0)
            {
                if (current_delay >= spawn_delay)
                {
                    foreach (var player in players_in_danger)
                    {
                        //if (PublicVariables.MERGED_PLAYER)
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
    }

    private void Appear()
    {

        anim.SetBool("Appear", Verify_Distance());

        if (Verify_Distance() && !is_appeared)
        {
            Debug.Log("A WILD MUSHROOM APPEAR!");
            InvokeRepeating("PoisonBurst", poisonTime, poisonTime);
            is_appeared = true;
        }
    }

    /// <summary>
    /// Activates the Mushroom Burst Periodic Poison Region
    /// </summary>
    public void PoisonBurst() //VISUAL TEMPORARY , WITH A CIRCLE
    {
        //add a PLaySoundAttack

        if (!poison_is_active)
        {
            fakePoisonCloud.enabled = true;
            poison_is_active = true;
        }
        else
        {
            fakePoisonCloud.enabled = false;
            poison_is_active = false;
        }
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
    /// <summary>
    /// Activates the Mushroom Death animation
    /// </summary>
    public void Death()
    {
        anim.SetTrigger("Death");
        HP_ENEMY = 0;
    }

    /// <summary>
    /// Activates the MushRoom Hurt animation
    /// </summary>
    public void Hurt()
    {
        anim.SetTrigger("Hurt");
        HP_ENEMY -= 1;
    }

}
