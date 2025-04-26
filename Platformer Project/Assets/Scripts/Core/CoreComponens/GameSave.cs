using System.Collections;
using System.Collections.Generic;
using Platformer.CoreSystem;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;


namespace Platformer
{
    public class GameSave : CoreComponent
    {
        protected Movement Movement
        {
            get => movement ??= core.GetCoreComponent<Movement>();
        }
        private Movement movement;
        
        protected Stats Stats
        {
            get => stats ??= core.GetCoreComponent<Stats>();
        }
        private Stats stats;
        
        private void OnEnable()
        {
            SaveManager.OnSessionSave += Save;
            SaveManager.OnSessionLoad += Load;
            SaveManager.OnResultSave += SaveResult;

        }

        private void OnDisable()
        {
            SaveManager.OnSessionSave -= Save;
            SaveManager.OnSessionLoad -= Load;
        }
        
        public void Save(ref SessionSaveData data)
        {
            data.levelNumber = SceneManager.GetActiveScene().buildIndex;
            data.movementSaveData.Position = transform.root.position;
            data.statsSaveData.currentHealth = Stats.Health.CurrentValue;
        }
        public void SaveResult(ref LevelResultData data)
        {

            data.levelNumber = SceneManager.GetActiveScene().buildIndex;
            data.rank = PlayerPrefs.GetInt("LevelRank", 0);
            data.completionTime = PlayerPrefs.GetFloat("LevelTime", 0f);
            data.statsSaveData.currentHealth = Stats.Health.CurrentValue;
            data.score = PlayerPrefs.GetInt("Points", 0);
            data.bonusPoints = PlayerPrefs.GetInt("BonusTimePoints", 0);
            data.reducePoints = PlayerPrefs.GetInt("ReducePoints", 0);
            data.totalScore = PlayerPrefs.GetInt("TotalPoints", 0);

        }
        
        
        public void Load(in SessionSaveData data)
        {
            transform.root.position = data.movementSaveData.Position;
            Stats.Health.CurrentValue = data.statsSaveData.currentHealth;
        }
    }
    

    
    [Serializable]
    public class SessionSaveData
    {
        public int levelNumber;
        public MovementSaveData movementSaveData = new();
        public StatsSaveData statsSaveData = new();

    }
    
    [Serializable]
    public struct MovementSaveData
    {
        public Vector2 Position;
       
    }
    [Serializable]
    public struct StatsSaveData
    {
        public float currentHealth;
       
    }

        
    [Serializable]
    public class LevelResultData
    {
        public int levelNumber;
        public float completionTime;
        public StatsSaveData statsSaveData = new();
        
        public int score;
        
        public int rank;
        public int bonusPoints;
        public int reducePoints;
        public int totalScore;
    }    
    
    public static class FieldLocalization
    {
        public static readonly Dictionary<string, string> FieldTranslations = new()
        {
            { "movementSaveData", "Дані руху" },
            {"statsSaveData", "Дані параметрів"},
            { "Position", "Позиція" },
            { "x", "X" },
            { "y", "Y" },
            { "currentHealth", "Поточне здоров`я" },
            { "levelNumber", "Номер рівня" },
            { "completionTime", "Час проходження" },
            { "score", "Очки" },
            { "rank", "Ранг" },
            { "bonusPoints", "Бонус з час" },
            { "reducePoints", "Очки" },
            { "totalScore", "Результат" },
        };
    }
}



