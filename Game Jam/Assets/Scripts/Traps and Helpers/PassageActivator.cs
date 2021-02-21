using UnityEngine;
using LesserKnown.Manager;
namespace LesserKnown.TrapsAndHelpers
{
    /// <summary>
    /// This activates something else
    /// Usually it's for a lever that opens a door or something
    /// </summary>
    public class PassageActivator: MonoBehaviour
    {
        /// <summary>
        /// This delegate is for when you die it will reset the activator to false that way you need to open it again
        /// </summary>
        private DelegateControler delegate_controller;
        private bool isActive;
        public DeathPlatform[] death_platforms;


        private void Start()
        {
            delegate_controller = FindObjectOfType<DelegateControler>();

            foreach (var platform in death_platforms)
            {
                delegate_controller.reset_on_death += platform.ResetPlatform;
            }

            delegate_controller.reset_on_death += Reset_Object;
        }

        public void Reset_Object()
        {
            isActive = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Player")
            {
                isActive = true;
                foreach (var platform in death_platforms)
                {
                    platform.Activate();
                }
            }
        }
    }
}
