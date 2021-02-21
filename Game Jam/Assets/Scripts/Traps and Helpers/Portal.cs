using UnityEngine;

namespace LesserKnown.Traps_and_Helpers
{
    public class Portal: MonoBehaviour
    {
        private PortalsControl controller;

        private void Start()
        {
            controller = GetComponentInParent<PortalsControl>();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            controller.Teleport(this, collision.transform);
        }
    }
}
