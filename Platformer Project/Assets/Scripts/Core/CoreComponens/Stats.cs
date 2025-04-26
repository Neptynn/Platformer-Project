using System;
using Platformer.CoreSystem.StatsSystem;
using UnityEngine;

namespace Platformer.CoreSystem
{
    public class Stats : CoreComponent
    {
        public IHealthData HealthData { get; private set; }
        public IPoiseData PoiseData { get; private set; }
        public IPointData PointData { get; private set; }

        [field: SerializeField] public Stat Health { get; private set; }
        [field: SerializeField] public Stat Poise { get; private set; }
        [field: SerializeField] public Stat PoiseRecoveryRate { get; private set; }
        [field: SerializeField] public Stat Point { get; private set; }
        [field: SerializeField] public Stat PointReduce { get; private set; }


        protected override void Awake()
        {


            BaseData baseData = GetComponentInParent<BaseData>();
            if (baseData != null)
            {
                HealthData = baseData.GetHealthData();
                PoiseData = baseData.GetPoiseData();
                PointData = baseData.GetPointData();
            }

            Health?.SetMaxValue(HealthData.MaxHealth);

            if (PoiseData != null)
            {
                Poise.SetMaxValue(PoiseData.Poise);
                PoiseRecoveryRate.SetMaxValue(PoiseData.PoiseRecoveryRate);
            }

            if (PointData != null)
            {
                Point.SetMaxValue(PointData.Point);
                PointReduce.SetMaxValue(PointData.PointReduce);
            }


            base.Awake();
            Health.Init();
            Poise.Init();

        }

        private void Update()
        {
            if (Poise.CurrentValue.Equals(Poise.MaxValue))
                return;
            Poise.Increace(PoiseRecoveryRate.MaxValue * Time.deltaTime);
        }

    }

    public interface IHealthData
    {
        float MaxHealth { get; }

    }

    public interface IPoiseData
    {
        float Poise { get; }
        float PoiseRecoveryRate { get; }
    }

    public interface IPointData
    {
        float Point { get; }
        float PointReduce { get; }
    }


}
