using System;
using System.Collections.Generic;
using UnityEngine;

// Observer Base
public interface IObserver
{
    void OnNotify(string eventType);
}

// Subject (Publisher)
public class GameEventManager : MonoBehaviour
{
    private List<IObserver> observers = new List<IObserver>();

    // Oyun içindeki olaylarý bildirmek için kullanýlýr
    public void AddObserver(IObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void NotifyObservers(string eventType)
    {
        foreach (var observer in observers)
        {
            observer.OnNotify(eventType);
        }
    }

    // Örnek bir oyun olayý
    public void TriggerGameOver()
    {
        NotifyObservers("GameOver");
    }
}

// Concrete Observer
public class UIManager : MonoBehaviour, IObserver
{
    public void OnNotify(string eventType)
    {
        if (eventType == "GameOver")
        {
            Debug.Log("Game Over! UI Güncelleniyor...");
            // UI'yi güncelle
        }
    }
}
