using UnityEngine;
using System.Collections;

namespace LesserKnown.Traps_and_Helpers
{
    public class PortalsControl: MonoBehaviour
    {
        public Portal[] portals = new Portal[2];

      

        public void Teleport(Portal triggered_portal, Transform object_to_tp)
        {
            foreach (var portal in portals)
            {
                if (portal != triggered_portal)
                StartCoroutine(TeleportIE(portal.transform,object_to_tp));
            }
        }
    private IEnumerator TeleportIE(Transform portal, Transform tp_ojb)
        {
            tp_ojb.gameObject.SetActive(false);
            yield return new WaitForSeconds(.1f);
            tp_ojb.position = portal.position;
            yield return new WaitForSeconds(.1f);
            tp_ojb.gameObject.SetActive(true);
        }
    }
}
