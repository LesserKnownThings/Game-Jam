using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LesserKnown.Camera;

namespace LesserKnown.Player
{
    /// <summary>
    /// This function controlls all player actions
    /// I know it's named movement, but it does pretty everything
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {
        private CharacterController2D controller;

        [Header("Player Settings")]
        public float movement_speed = 10f;
        public float climbing_speed = 8f;
        public float jump_force = 30f;
        public float wall_jump_force = 8f;
        [Space(10)]
        [Header("Player Input Keys")]
        public KeyCode jump_key;
     

        private CameraFollow cam;

        private void Start()
        {
            controller = GetComponent<CharacterController2D>();
            cam = UnityEngine.Camera.main.GetComponent<CameraFollow>();

        }

        private void Update()
        {
            if (!controller.is_active)
                return;

            if (Input.GetKeyDown(jump_key))
                controller.Jump(jump_force);

            if (controller.wall_jump)
                controller.Jump_Wall(new Vector2(wall_jump_force, jump_force));

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (controller.is_fighter)
                    controller.Attack();
                else if (!controller.is_fighter)
                    controller.Pickup();
            }

               
        }

        private void FixedUpdate()
        {

            if (!controller.is_active)
                return;

            var h = Input.GetAxisRaw("Horizontal");
            var v = Input.GetAxisRaw("Vertical");
            if (controller.horizontalEnable)
            {

                controller.Move(h * movement_speed);
            }
              
            controller.Climb(v * climbing_speed);
        }

       
    }
}
