using UnityEngine;
using System.Collections;

namespace LesserKnown.TrapsAndHelpers
{
    public class PortalsControl : MonoBehaviour
    {
        public Portal[] portals = new Portal[2];



        public void Teleport(Portal triggered_portal, Transform object_to_tp)
        {
            foreach (var portal in portals)
            {
                if (portal != triggered_portal)
                    object_to_tp.position = portal.transform.position;
            }
        }
    }
}
