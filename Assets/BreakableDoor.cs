using UnityEngine;

public class BreakableDoor : MonoBehaviour
{
    [Header("Break Settings")]
    [Tooltip("Force applied to the door when it breaks off the frame")]
    public float breakForce = 5f;
    private bool isBroken = false;

    private void OnTriggerEnter(Collider other)
    {
        if (isBroken) return;

        WeaponTag weapon = other.GetComponentInParent<WeaponTag>();
        if (weapon == null) return;

        BreakDoor(other);
    }

    private void BreakDoor(Collider hitCollider)
    {
        isBroken = true;

        // Detach door from its parent (the frame)
        transform.SetParent(null);

        // Get existing Rigidbody or add one, then make it dynamic
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.mass = 5f;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        // Calculate force direction: push the door away from the hit point
        Vector3 forceDir = (transform.position - hitCollider.transform.position).normalized;
        forceDir += Vector3.up * 0.3f;
        forceDir.Normalize();

        rb.AddForce(forceDir * breakForce, ForceMode.Impulse);

        // Add some spin for a more dramatic effect (not drastic)
        rb.AddTorque(Random.insideUnitSphere * breakForce * 0.5f, ForceMode.Impulse);
    }
}
