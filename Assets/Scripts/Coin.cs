using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent<Player>(out _))
            Destroy(gameObject);
    }
}