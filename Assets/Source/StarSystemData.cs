using System.Collections.Generic;
using UnityEngine;

namespace NB.SpaceGame
{
    [CreateAssetMenu(menuName = "Space Game/Star system data")]
    public class StarSystemData : ScriptableObject
    {
        public List<CelestialBodyData> Bodies;
    }
}
