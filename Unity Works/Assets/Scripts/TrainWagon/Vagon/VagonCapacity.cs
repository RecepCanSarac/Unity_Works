using UnityEngine;

public class VagonCapacity : MonoBehaviour
{
    public Transform[] turretSlots;
    private int currentTurretCount = 0;

    public bool CanAddTurret()
    {
        return currentTurretCount < turretSlots.Length;
    }

    public Transform GetNextSlot()
    {
        if (!CanAddTurret()) return null;
        return turretSlots[currentTurretCount];
    }

    public void AddTurret()
    {
        currentTurretCount++;
    }

    public int RemainingTurretSlots()
    {
        return turretSlots.Length - currentTurretCount;
    }
}