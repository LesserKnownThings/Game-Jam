using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LesserKnown.TrapsAndHelpers
{
    public interface IPortalInterface
    {
        bool portal_transition { get; set; }
        void Teleport(float transition_duration);
    }
}
