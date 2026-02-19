using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
    public float timeRemaining = 90f;
    public TextMeshProUGUI timerText;

    void Update()
    {
        if (timeRemaining > 0)
        {
  
            timeRemaining -= Time.deltaTime;
            int seconds = Mathf.FloorToInt(timeRemaining);
            timerText.text = "Time Left: " + seconds.ToString() + "Seconds";
        }
        else
        {
            #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
            #else
                        Application.Quit();
            #endif
        }
    }
  
}