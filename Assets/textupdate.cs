using UnityEngine;
using TMPro;

public class WallDisplay : MonoBehaviour
{
    public PlayerInventory player; 
    public TextMeshProUGUI textElement; 

    void Update()
    {
        textElement.text = "Helms Collected: " + player.totalHelms.ToString();
    }
}