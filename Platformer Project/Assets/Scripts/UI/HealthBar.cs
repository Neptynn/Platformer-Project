using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Platformer.CoreSystem;

namespace Platformer
{
    public class HealthBar: MonoBehaviour
    {
        
        public MainMenu mainMenu;
        
        protected CoreSystem.Core core;
        protected virtual void Awake()
        {
            core = transform.parent.GetComponent<CoreSystem.Core>();

            if (core == null )
            {
                Debug.LogError("There is no Core on the parent");
            
            }
        }
        protected Stats Stats
        {
            get => stats ??= core.GetCoreComponent<Stats>();
        }
        private Stats stats;

        public Slider slider;
        public Gradient gradient;
        public Image fill;

        public void Start()
        {
            SetMaxHealth();
        }

        public void Update()
        {
            SetHealth();
        }
        
        public void SetMaxHealth()
        {
            slider.maxValue = Stats.Health.MaxValue;
            slider.value = Stats.Health.CurrentValue;

            fill.color = gradient.Evaluate(1f);
        }
        
        public void SetHealth()
        {
            slider.value = Stats.Health.CurrentValue;
            
            fill.color = gradient.Evaluate(slider.normalizedValue);

            if (Stats.Health.CurrentValue <= 0)
            {
                mainMenu.FallMenu();
            }
        }

        
        private void OnEnable()
        {
            Stats.Health.OnCurrentValueZero += SetHealth;
        }

        private void OnDisable()
        {
            Stats.Health.OnCurrentValueZero -= SetHealth;
        }
        
    }
}

        