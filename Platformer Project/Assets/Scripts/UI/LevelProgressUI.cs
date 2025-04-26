using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer
{
    public class LevelProgressUI : MonoBehaviour
    {
        [Header("UI References")]
        public TextMeshProUGUI pointsText;
        public TextMeshProUGUI timerText;

        [Header("Settings")]
        public int startPoints = 1000;
        public float duration = 180f; // час, за який бонусні очки зменшуються до 0

        private float elapsedTime = 0f;
        private int lastShownPoints = -1;


        private int newPoints;

        void Start()
        {
            // Скидаємо початкові значення
            PlayerPrefs.SetInt("LevelTime", startPoints);
            PlayerPrefs.SetFloat("LevelTime", 0f);
            PlayerPrefs.Save();
        }

        void Update()
        {
            elapsedTime += Time.deltaTime;

            TimerUI();
            ReduceBonusPoints();
            UpdatePoints();
        }

        private void TimerUI()
        {
            int minutes = Mathf.FloorToInt(elapsedTime / 60f);
            int seconds = Mathf.FloorToInt(elapsedTime % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            
            PlayerPrefs.SetFloat("LevelTime", elapsedTime);
        }

        private void ReduceBonusPoints()
        {
            
            float reductionRate = startPoints / duration;
            float reducedPoints = startPoints - Mathf.Min(elapsedTime, duration) * reductionRate;
            newPoints = Mathf.Max(0, Mathf.CeilToInt(reducedPoints));
            
            PlayerPrefs.SetInt("BonusTimePoints", newPoints);
        }

        public void UpdatePoints()
        {
            int currentPoints = PlayerPrefs.GetInt("Points", 0) + PlayerPrefs.GetInt("ReducePoints", 0);
            if (currentPoints != lastShownPoints)
            {
                lastShownPoints = currentPoints;
                pointsText.text = "Очки: " + currentPoints;
            }

            PlayerPrefs.Save();
        }
        
    }
}