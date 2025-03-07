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

    // Oyun i�indeki olaylar� bildirmek i�in kullan�l�r
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

    // �rnek bir oyun olay�
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
            Debug.Log("Game Over! UI G�ncelleniyor...");
            // UI'yi g�ncelle
        }
    }
}
