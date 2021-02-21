using UnityEngine;

namespace LesserKnown.Manager
{
    public class DelegateControler: MonoBehaviour
    {
        public delegate void Death_Reset();
        public Death_Reset reset_on_death;
    
    }
}
