using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int totalHelms = 0;

    public void AddHelm(int amount)
    {
        totalHelms += amount;
    }
}