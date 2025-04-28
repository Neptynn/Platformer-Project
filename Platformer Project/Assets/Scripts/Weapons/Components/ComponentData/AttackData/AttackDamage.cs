using System;
using UnityEngine;

namespace Platformer.Weapons.Components
{
    [Serializable]
    public class AttackDamage : AttackData
    {
        [field: SerializeField] public Combat.Damage.DamageData Amount {get; private set;}
    }
}