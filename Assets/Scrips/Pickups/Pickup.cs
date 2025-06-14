using UnityEngine;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 100f;

    private const string playerString = "Player";

    private void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerString))
        {
            OnPickup();
            Destroy(gameObject);
        }
    }

    protected abstract void OnPickup();
}
