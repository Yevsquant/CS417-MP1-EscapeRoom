using UnityEngine;
using UnityEngine.Events;
public class NewMonoBehaviourScript : MonoBehaviour
{
    public int goalCount = 3;
    public int currentCount = 0;

    public UnityEvent onGoalReached;

    public void AddCount()
    {
        currentCount++;
        if (currentCount >= goalCount)
        {
            onGoalReached.Invoke();
        }
    }

    public void RemoveCount()
    {
        currentCount--;
    }

}
