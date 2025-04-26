using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterFinish : MonoBehaviour
{
    public static event Action OnFinish;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            OnFinish?.Invoke();
        }
        
    }
}