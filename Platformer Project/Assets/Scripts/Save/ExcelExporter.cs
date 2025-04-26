using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Reflection;
using ClosedXML.Excel;
using UnityEngine.SceneManagement;

namespace Platformer
{
    public static class ExcelExporter
{
    public static void ExportSessionToExcel()
    {
        int level = SceneManager.GetActiveScene().buildIndex;
        string path = SaveManager.GetSessionPath(level);

        if (!File.Exists(path))
        {
            Debug.LogWarning("Файл не знайдено для експорту.");
            return;
        }

        string json = File.ReadAllText(path);
        SessionSaveData data = JsonUtility.FromJson<SessionSaveData>(json);

        ExportToExcel(data, $"Збереження рівня {level}.xlsx", "Дані сесії");
    }

    public static void ExportResultToExcel(int level)
    {
        string path = SaveManager.GetResultPath(level);

        if (!File.Exists(path))
        {
            Debug.LogWarning("Файл результату не знайдено для експорту.");
            return;
        }

        string json = File.ReadAllText(path);
        LevelResultData data = JsonUtility.FromJson<LevelResultData>(json);

        ExportToExcel(data, $"Результат проходження рівня {level}.xlsx", "Результат рівня");
    }

    private static void ExportToExcel(object data, string filename, string sheetName)
    {
        var workbook = new XLWorkbook();
        var ws = workbook.Worksheets.Add(sheetName);

        ws.Cell(1, 1).Value = "Назва";
        ws.Cell(1, 2).Value = "Значення";

        var rows = new List<List<string>>();
        bool isRoot = true;
        ExtractDataRecursive(data, new List<string>(), rows, ref isRoot);

        for (int i = 0; i < rows.Count; i++)
        {
            var row = rows[i];
            ws.Cell(i + 2, 1).Value = row[0];
            ws.Cell(i + 2, 2).Value = row[1];
        }

        ws.Columns().AdjustToContents();

        string fullPath = Path.Combine(Application.persistentDataPath, filename);
        workbook.SaveAs(fullPath);

        Debug.Log($"Excel файл збережено: {fullPath}");
    }

    private static void ExtractDataRecursive(object obj, List<string> path, List<List<string>> result, ref bool isRoot)
    {
        if (obj == null) return;

        var type = obj.GetType();
        var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);

        foreach (var field in fields)
        {
            object value = field.GetValue(obj);
            string name = field.Name;
            string translated = FieldLocalization.FieldTranslations.TryGetValue(name, out var tr) ? tr : name;

            if (value == null)
            {
                var full = new List<string>(path) { translated };
                result.Add(new List<string> { string.Join(" → ", full), "NULL" });
            }
            else if (field.FieldType.IsPrimitive || field.FieldType == typeof(string) || field.FieldType == typeof(bool) || field.FieldType == typeof(float) || field.FieldType == typeof(double))
            {
                string valStr = value switch
                {
                    float f => f.ToString("0.##"),
                    double d => d.ToString("0.##"),
                    _ => value.ToString()
                };

                var full = new List<string>(path) { translated };
                result.Add(new List<string> { string.Join(" → ", full), valStr });
            }
            else if (field.FieldType.IsValueType || field.FieldType.IsClass)
            {
                // Якщо це перший (кореневий) рівень - пропускаємо назву (SessionSaveData або LevelResultData)
                bool skip = isRoot;
                isRoot = false;

                if (!skip)
                    path.Add(translated);

                ExtractDataRecursive(value, path, result, ref isRoot);

                if (!skip)
                    path.RemoveAt(path.Count - 1);
            }
        }
    }
}

}