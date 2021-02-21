using UnityEngine;
using System.Collections;
using LesserKnown.Player;

namespace LesserKnown.TrapsAndHelpers
{
    public class BoxControl : MonoBehaviour
    {
        public CharacterController2D player; // -----------------------Must add solver to this object reference
        private Rigidbody2D rb;
        private Collider2D m_collider;
        private bool is_picked;
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            m_collider = GetComponent<Collider2D>();
        }
        public void Start_Animation(AnimManager player)
        {
            transform.SetParent(player.transform, true);
            StartCoroutine(Pick_BoxIE(player));
        }

        public void Throw()
        {
            StartCoroutine(ThrowIE());
        }

        private IEnumerator ThrowIE()
        {
            yield return new WaitForSeconds(.22f);
            transform.SetParent(null);
            rb.bodyType = RigidbodyType2D.Dynamic;
            m_collider.isTrigger = false;
            if (player.IsLookingLeft())                                 // ----------if character look left
                rb.AddForce(new Vector2(-8, 2.5f), ForceMode2D.Impulse); //--------- throw box left side
            if (!player.IsLookingLeft())                                //---------- if character look right
                rb.AddForce(new Vector2(8, 2.5f), ForceMode2D.Impulse); //-------------throw box right side
            is_picked = false;
        }

        private void Update()
        {
            if(rb.velocity.magnitude <= .2f && !is_picked)
            {
                rb.bodyType = RigidbodyType2D.Static;
                m_collider.isTrigger = true;
            }
        }

        private IEnumerator Pick_BoxIE(AnimManager _player)
        {
            yield return new WaitForSeconds(1f);

            while (_player.Has_Stop_Animation())
            {
                transform.position += new Vector3(0, 5 * Time.deltaTime, 0);

                transform.position = Vector3.Lerp(transform.position, _player.controller.box_placer.position, .1f);   

                yield return null;
            }
            is_picked = true;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }
}
