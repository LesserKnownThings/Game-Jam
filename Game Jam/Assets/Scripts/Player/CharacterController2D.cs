using LesserKnown.Public;
using UnityEngine;
using System.Collections;
using LesserKnown.Camera;
using LesserKnown.TrapsAndHelpers;
using System.Collections.Generic;
using LesserKnown.AI;
using System;

namespace LesserKnown.Player
{
    /// <summary>
    /// This is a custom character controller
    /// It controlls all the player movements and the player behaviours
    /// </summary>
    public class CharacterController2D : MonoBehaviour
    {

        private Rigidbody2D rb;
        private Collider2D p_collider;
        private SpriteRenderer player_sprite;
        private AnimManager anim_manager;
        private CharacterController2D other_player;
        /// <summary>
        /// This is the CameraFollow script from LesserKnown.Camera attatched to the camera
        /// </summary>
        private CameraFollow cam;
        public GameObject blood_splatter;

        [Header("Layers")]
        [Tooltip("Used to detect the terrain")]
        public LayerMask ground;
        [Tooltip("Used to detect the platforms")]
        public LayerMask platform;
        [Tooltip("Used to detect enemies")]
        public ContactFilter2D enemy_filter;

        [Space(10)]
        [Header("Modifiers")]
        [Range(1f, 2f)]
        /// <summary>
        /// This is the fall speed modifier
        /// </summary>
        public float fall_modifier;
        

        [Space(10)]
        // private PlayerNetwork player_network;

        /// <summary>
        /// This checks if the player can do a wall jump
        /// </summary>
        [HideInInspector]
        public bool wall_jump;
        /// <summary>
        /// The modifier for the wall jump, depending on it's direction
        /// </summary>
        private float touching_wall;
        /// <summary>
        /// Verifies if the player can climb a ladder
        /// </summary>
        private bool can_climb_ladder;
        /// <summary>
        /// Verifies if the player is climbing a ladder
        /// </summary>
        [HideInInspector]
        public bool is_climbing_ladder;
        /// <summary>
        /// This is for testing only, activate to not take damage
        /// Still need to implement it
        /// </summary>
        [HideInInspector]
        public bool is_invicible;
        /// <summary>
        /// Verfies if the player is looking right or left
        /// It's for local use only, in network you need to use the network variable
        /// </summary>
        private bool local_left;

        /// <summary>
        /// Verifies if the player has an object in hands
        /// </summary>
        private bool is_holding_object;

        /// <summary>
        /// Verifies if the player is over a box trigger
        /// </summary>
        private bool can_pick_up;

        [HideInInspector]
        public bool horizontalEnable;
        [Space(10)]
        public float attack_distance = 1f;
        [HideInInspector]
        public float direction_vector;


        [Space(10)]
        [Header("Boundaries Ground")]
        public Vector2 boundary_size;
        public Vector2 boundary_placement;

        [Space(10)]
        [Header("Boundaries Right")]
        public Vector2 boundary_size_r;
        public Vector2 boundary_placement_r;

        [Space(10)]
        [Header("Boundaries Left")]
        public Vector2 boundary_size_l;
        public Vector2 boundary_placement_l;


        private void Start()
        {
            horizontalEnable = true;
            rb = GetComponent<Rigidbody2D>();
            player_sprite = GetComponent<SpriteRenderer>();
            anim_manager = GetComponent<AnimManager>();

            //This is for network use only, it's not for this project
            // player_network = GetComponent<PlayerNetwork>();
            p_collider = GetComponent<Collider2D>();
            cam = UnityEngine.Camera.main.GetComponent<CameraFollow>();
          

            CharacterController2D[] players = FindObjectsOfType<CharacterController2D>();

            foreach (var player in players)
            {
                if (player != this)
                    other_player = player;
            }

            blood_splatter.SetActive(false);
        }

        private void Update()
        {
            //player_sprite.flipX = player_network.left;
            player_sprite.flipX = local_left;

            //This changes the y velocity when touching a wall so it gives the feeling the player is sliding on the wall
            if (IsWallFalling())
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / fall_modifier);

            Climbing_Ladder();

            //This flips the sprite according to the moving direction
            if (IsWallFalling() && IsTouchingLeft())
                Flip(false);
            else if (IsWallFalling() && IsTouchingRight())
                Flip(true);


            //This triggers the move animation
            anim_manager.Move(rb.velocity.magnitude > 0 && (IsGrounded() || IsOnPlatform()));

            direction_vector = rb.velocity.y;
        }

        /// <summary>
        /// Changes the player collider when climbing a ladder
        /// When on the ladder it's trigger so it doesn't interact with the terrain
        /// This is done for the platforms when you need to go down
        /// </summary>
        private void Climbing_Ladder()
        {
            if (is_climbing_ladder)
            {
                p_collider.isTrigger = true;
                rb.gravityScale = 0f;
            }
            else
            {
                p_collider.isTrigger = false;
                rb.gravityScale = 6.5f;
            }
        }


      
   
     
        /// <summary>
        /// Move player, also checks for all the movement changes
        /// </summary>
        /// <param name="movement_speed">The player movement speed</param>
        public void Move(float movement_speed)
        {
            if (wall_jump || anim_manager.Has_Stop_Animation())
                return;

            if (anim_manager.Is_Attacking())
                movement_speed /= 3f;

            anim_manager.Climb(false);
            anim_manager.Climb_Stay(false);

            if (movement_speed != 0)
                is_climbing_ladder = false;

            if (!IsGrounded() && !IsOnPlatform())
            {
                if (IsTouchingRight())
                {
                    if (movement_speed > 0)
                        return;
                }

                if (IsTouchingLeft())
                {
                    if (movement_speed < 0)
                        return;
                }
            }


            rb.velocity = new Vector2(movement_speed, rb.velocity.y);

            if (movement_speed > 0)
                Flip(false);
            else if (movement_speed < 0)
                Flip(true);
        }

        /// <summary>
        /// Frank : used by Spike Traps script
        /// </summary>
        public void Dead()
        {
            anim_manager.Die_Anim();
            if(!PublicVariables.IS_FUSIONED)
            Reset_Position();
            else
            {
                //Lose game ...
            }

        }

        private void Reset_Position()
        {
            transform.position = other_player.transform.position;
        }

        public void Get_Hit(int amount)
        {
            if (is_invicible)
                return;

            is_invicible = true;
            anim_manager.Get_Hit();
            bool death = PublicVariables.Lose_Health(amount);

            if (death)
            {
                anim_manager.Die_Anim();
                //Disable Network Player
            }

            StartCoroutine(Reset_InvicibilityIE());
        }

        private IEnumerator Reset_InvicibilityIE()
        {
            yield return new WaitForSeconds(1.5f);
            is_invicible = false;
        }

        public void Climb(float movement_speed)
        {
            if (is_holding_object)
                return;

            if (movement_speed != 0)
                is_climbing_ladder = true;


            if (!can_climb_ladder)
            {
                is_climbing_ladder = false;
                return;
            }

            if (!is_climbing_ladder)
                return;

            if (movement_speed == 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                anim_manager.Climb_Stay(true);
                return;
            }

            anim_manager.Climb_Stay(false);
            anim_manager.Climb(true);
            rb.velocity = new Vector2(rb.velocity.x, movement_speed);
        }

        private void Flip(bool left)
        {
            /* Set left on network
            player_network.left = left;            */

            local_left = left;
        }

        /// <summary>
        /// Does the player jump
        /// </summary>
        /// <param name="jump_force">The jump force</param>
        /// <param name="move_speed">The jump force used to jump when next to a wall</param>
        public void Jump(float jump_force)
        {
            if (is_holding_object || anim_manager.Has_Stop_Animation())
                return;

            if (IsTouchingLeft())
                touching_wall = 1;
            else if (IsTouchingRight())
                touching_wall = -1;

            if ((IsTouchingLeft() || IsTouchingRight()) && !IsGrounded())
                wall_jump = true;

            if (wall_jump)
            {
                Invoke("Wall_Jump", 0.08f);
  
                StartCoroutine(DontMove());
            }

            if (!IsGrounded() && !IsOnPlatform())
                return;


            anim_manager.Jump_Anim();
            rb.velocity = new Vector2(rb.velocity.x, jump_force);

            anim_manager.Climb(false);
            anim_manager.Climb_Stay(false);
            is_climbing_ladder = false;

        }
        private IEnumerator DontMove()
        {
    
            horizontalEnable = false;  //--------------------------------------------tell if player can move
            yield return new WaitForSeconds(0.25f);
            horizontalEnable = true;
        }

        public void Jump_Wall(Vector2 jump_force)
        {
            if (jump_force.x == 0)
                return;

            rb.velocity = new Vector2(jump_force.x * touching_wall, jump_force.y);
        }

        private void Wall_Jump()
        {
            wall_jump = false;
        }

        #region Public bools
        /// <summary>
        /// Verifies if the player is touching any object with the Layer Terrain bottom
        /// </summary>
        public bool IsGrounded()
        {

            return Physics2D.OverlapBox(new Vector2(transform.position.x + boundary_placement.x, transform.position.y - boundary_placement.y), new Vector2(boundary_size.x, boundary_size.y), 0f, ground);

        }

        public bool IsOnPlatform()
        {

            return Physics2D.OverlapBox(new Vector2(transform.position.x + boundary_placement.x, transform.position.y - boundary_placement.y), new Vector2(boundary_size.x, boundary_size.y), 0f, platform);

        }

        /// <summary>
        /// Verifies if the player is touching any object with Layer Terrain on left
        /// </summary>
        public bool IsTouchingLeft()
        {

            return Physics2D.OverlapBox(new Vector2(transform.position.x - boundary_placement_l.x, transform.position.y - boundary_placement_l.y), new Vector2(boundary_size_l.x, boundary_size_l.y), 0f, ground);

        }

        /// <summary>
        /// Verifies if the player is touching any object with Layer Terrain on right
        /// </summary>
        public bool IsTouchingRight()
        {
            return Physics2D.OverlapBox(new Vector2(transform.position.x + boundary_placement_r.x, transform.position.y - boundary_placement_r.y), new Vector2(boundary_size_r.x, boundary_size_r.y), 0f, ground);

        }

        /// <summary>
        /// Verifies if the player is falling next to a wall
        /// </summary>
        public bool IsWallFalling()
        {
            return (IsTouchingLeft() || IsTouchingRight()) && !IsGrounded();
        }
        #endregion

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Ladder")
                can_climb_ladder = true;

          
            if (collision.tag == "LadderTree")        // For fighter can climb tree   must a tag "LadderTree"
                can_climb_ladder = true;

        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "Ladder")
                can_climb_ladder = false;

           
            if (collision.tag == "LadderTree")       // For fighter can climb tree   must a tag "LadderTree"
                can_climb_ladder = false;
        }

        private void Send_Hit()
        {
            List<RaycastHit2D> hits = new List<RaycastHit2D>();

            if (local_left)
                Physics2D.Raycast(transform.position, -transform.right, enemy_filter, hits, attack_distance);
            else if(!local_left)
                Physics2D.Raycast(transform.position, transform.right, enemy_filter, hits, attack_distance);


            if (hits.Count > 0)
            {
                StartCoroutine(Blood_SplatterIE());
                hits[0].transform.GetComponent<Enemy_Interface>().Get_Hit(1);               
            }
        }

        private IEnumerator Blood_SplatterIE()
        {
            blood_splatter.SetActive(true);
            yield return new WaitForSeconds(.5f);
            blood_splatter.SetActive(false);
        }

        #region Warrior Region
        public void Attack()
        {
            if (anim_manager.Is_Attacking())
                return;

            anim_manager.Attack();
        }
        #endregion

        #region Solver Region
        public void Pickup()
        {
            if (anim_manager.Has_Stop_Animation() || !can_pick_up)
                return;


            is_holding_object = !is_holding_object;
            anim_manager.Pick_Throw(is_holding_object);
        }

        public bool IsLookingLeft()  // --------------------- for throw box right or left
        {

            return local_left;

        }
        #endregion

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(new Vector2(transform.position.x + boundary_placement.x, transform.position.y - boundary_placement.y), new Vector2(boundary_size.x, boundary_size.y));

            Gizmos.color = Color.blue;
            Gizmos.DrawCube(new Vector2(transform.position.x - boundary_placement_l.x, transform.position.y - boundary_placement_l.y), new Vector2(boundary_size_l.x, boundary_size_l.y));

            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(new Vector2(transform.position.x + boundary_placement_r.x, transform.position.y - boundary_placement_r.y), new Vector2(boundary_size_r.x, boundary_size_r.y));

        }

    }
}