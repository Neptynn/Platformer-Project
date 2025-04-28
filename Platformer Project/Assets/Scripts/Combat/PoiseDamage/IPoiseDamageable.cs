using Platformer.Combat.Damage;

namespace Platformer.Combat.PoiseDamage
{
    public interface IPoiseDamageable
    {
        void DamagePoise(PoiseDamageData amount);
    }
}