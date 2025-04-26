using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace Platformer
{
    public class LevelSummaryUI : MonoBehaviour
    {
        public static event Action OnResultsSave;
        
        public TextMeshProUGUI timeText;
        public TextMeshProUGUI basePointsText;
        public TextMeshProUGUI bonusPointsText;
        public TextMeshProUGUI ruducePointsText;
        public TextMeshProUGUI totalPointsText;

        public Image[] stars; // 3 елементи
        public Sprite filledStar;
        public Sprite emptyStar;
        
        void OnEnable()
        {
            // Час
            float time = PlayerPrefs.GetFloat("LevelTime", 0f);
            int minutes = Mathf.FloorToInt(time / 60f);
            int seconds = Mathf.FloorToInt(time % 60f);
            timeText.text = $"Загальний час: {minutes:00}:{seconds:00}";

            // Очки
            int basePoints = PlayerPrefs.GetInt("Points", 0);
            int bonusPoints = PlayerPrefs.GetInt("BonusTimePoints", 0);
            int ruducePoints = PlayerPrefs.GetInt("ReducePoints", 0);
            int total = basePoints + ruducePoints + bonusPoints;

            basePointsText.text = $"Очки: {basePoints}";
            bonusPointsText.text = $"Очки за час: {bonusPoints}";
            ruducePointsText.text = $"Штрафні очки: {ruducePoints}";
            totalPointsText.text = $"Результат: {total}";
            
            PlayerPrefs.SetInt("TotalPoints", total);

            // Обчислення рангу
            int rank = CalculateRank(total);
            PlayerPrefs.SetInt("LevelRank", rank);

            // Виводимо зірки
            UpdateStars(rank);

            OnResultsSave?.Invoke();
        }
        
        int CalculateRank(int points)
        {
            if (points <= 250) return 0;
            if (points <= 500) return 1;
            if (points <= 750) return 2;
            return 3;
        }

        void UpdateStars(int rank)
        {
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].sprite = i < rank ? filledStar : emptyStar;
            }
        }
    }
}