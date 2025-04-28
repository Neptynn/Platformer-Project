using System;
using Platformer.Combat.Damage;
using UnityEngine;

namespace Platformer.CoreSystem.StatsSystem
{
    [Serializable]
    public class Stat
    {
        public event Action OnCurrentValueZero;
        
        [field: SerializeField] public float MaxValue { get; private set; }

        public float CurrentValue
        {
            get => currentValue;
            set
            {
                currentValue = Mathf.Clamp(value, 0, MaxValue);

                if (currentValue <= 0f)
                {
                    OnCurrentValueZero?.Invoke();
                }
            }
        }

        public void SetMaxValue(float value)
        {
            MaxValue = value;
        }
        private float currentValue;

        public void Init() => CurrentValue = MaxValue;
        public void Increace(float amount) => CurrentValue += amount;
        public void Decrease(float amount) => CurrentValue -= amount;
    }
}