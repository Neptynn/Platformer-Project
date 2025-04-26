using Platformer.CoreSystem;
using UnityEngine;

public class BaseData : MonoBehaviour
{
    public ScriptableObject data;

    public IHealthData GetHealthData()
    {
        return data as IHealthData;
    }
    
    public IPoiseData GetPoiseData()
    {
        return data as IPoiseData;
    }
    
    public IPointData GetPointData()
    {
        return data as IPointData;
    }
}