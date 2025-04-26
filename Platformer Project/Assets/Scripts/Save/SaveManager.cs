using System.IO;
using UnityEngine;

using UnityEngine.SceneManagement;

namespace Platformer
{
    public class SaveManager : MonoBehaviour
    {
        public Player player;

        private void Awake()
        {
            player = GetComponent<Player>();
        }

        private void Update()
        {
            if (player.InputHandler.SaveInput)
            {
                SaveToFile();
                ExcelExporter.ExportSessionToExcel();
            }

            if (player.InputHandler.LoadInput)
            {
                LoadFromFile();
            }
        }

        private void OnEnable()
        {
            LevelSummaryUI.OnResultsSave += SaveResult;
        }

        public delegate void SessionSaveHandler(ref SessionSaveData data);
        public static event SessionSaveHandler OnSessionSave;

        public delegate void SessionLoadHandler(in SessionSaveData data);
        public static event SessionLoadHandler OnSessionLoad;
        
        public delegate void ResultSaveHandler(ref LevelResultData data);
        public static event ResultSaveHandler OnResultSave;

        public static string GetSessionPath(int level) =>
            Path.Combine(Application.persistentDataPath, $"Збереження рівня {level}.save");

        public static string GetResultPath(int level) =>
            Path.Combine(Application.persistentDataPath, $"Результат проходження рівня {level}.save");

        public static void SaveToFile()
        {
            var data = new SessionSaveData();
            OnSessionSave?.Invoke(ref data);
            data.levelNumber = SceneManager.GetActiveScene().buildIndex;

            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(GetSessionPath(data.levelNumber), json);

            Debug.Log($"Рівень збережено: {GetSessionPath(data.levelNumber)}");
        }

        public static void LoadFromFile()
        {
            int level = SceneManager.GetActiveScene().buildIndex;
            string path = GetSessionPath(level);

            if (!File.Exists(path))
            {
                Debug.LogWarning("Файл збереження не знайдено.");
                return;
            }

            string json = File.ReadAllText(path);
            SessionSaveData data = JsonUtility.FromJson<SessionSaveData>(json);

            OnSessionLoad?.Invoke(in data);
            Debug.Log("Дані рівня завантажено.");
        }

        public static void SaveResult()
        {
            var result = new LevelResultData();
            OnResultSave?.Invoke(ref result);
            string path = GetResultPath(result.levelNumber);
            string json = JsonUtility.ToJson(result, true);
            File.WriteAllText(path, json);

            Debug.Log($"Результат збережено: {path}");
            ExcelExporter.ExportResultToExcel(result.levelNumber);
        }
    }
    
    
}
