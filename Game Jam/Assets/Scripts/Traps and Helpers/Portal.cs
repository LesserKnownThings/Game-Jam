using UnityEngine;

namespace LesserKnown.TrapsAndHelpers
{
    public class Portal: MonoBehaviour
    {
        private PortalsControl controller;
        public float teleport_transition = 2f;

        private void Start()
        {
            controller = GetComponentInParent<PortalsControl>();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            IPortalInterface portal_interface = collision.GetComponent<IPortalInterface>();

            if (portal_interface.portal_transition)
                return;

            portal_interface.Teleport(teleport_transition);
            controller.Teleport(this, collision.transform);
        }
    }
}
