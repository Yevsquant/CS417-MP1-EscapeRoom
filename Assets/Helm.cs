using UnityEngine;

public partial class Helm : MonoBehaviour
{

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            inventory.AddHelm(1);
            Destroy(gameObject);
        }
    }
}