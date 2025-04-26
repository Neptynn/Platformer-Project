using System;
using UnityEngine;

namespace Platformer.CoreSystem
{
    public class Death : CoreComponent
    {
        [SerializeField] private GameObject[] deathParticles;
    
        private ParticleManager ParticleManager => particleManager ??= core.GetCoreComponent<ParticleManager>();
        private ParticleManager particleManager;

        private Stats Stats  => stats ??= core.GetCoreComponent<Stats>(); 
        private Stats stats;

        public void Die()
        {

            if (Stats.PointData != null)
            {
                int currentPoints = PlayerPrefs.GetInt("Points", 0);
                currentPoints += (int)Stats.Point.MaxValue;
                PlayerPrefs.SetInt("Points", currentPoints);
                PlayerPrefs.Save();
                Debug.Log(PlayerPrefs.GetInt("Points", 0));
            }

            foreach (var particle in deathParticles) 
            {
                ParticleManager.StartParticles(particle);
            }
            
            core.transform.parent.gameObject.SetActive(false);  
        }
        
        private void OnEnable()
        {
            Stats.Health.OnCurrentValueZero += Die;
        }

        private void OnDisable()
        {
            Stats.Health.OnCurrentValueZero -= Die;
        }

    }
}
