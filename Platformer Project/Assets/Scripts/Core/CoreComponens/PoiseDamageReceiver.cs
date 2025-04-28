using Platformer.Combat.Damage;
using Platformer.Combat.PoiseDamage;
using Platformer.Interfaces;
using UnityEngine;

namespace Platformer.CoreSystem
{
    public class PoiseDamageReceiver : CoreComponent, IPoiseDamageable
    {
        private Stats stats;
        public void DamagePoise(PoiseDamageData amount)
        {
            stats.Poise.Decrease(amount.Amount);
        }

        protected override void Awake()
        {
            base.Awake();
            
            stats = core.GetCoreComponent<Stats>()  ;
        }
    }
}