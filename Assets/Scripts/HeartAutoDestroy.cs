using UnityEngine;

public class HeartAutoDestroy : MonoBehaviour
{
    public float lifetime = 15f;

    void Start()
    {
        Invoke(nameof(RemoveHeart), lifetime);
    }

    void RemoveHeart()
    {
        SpawnHearts spawner = FindObjectOfType<SpawnHearts>();
        if (spawner != null)
            spawner.HeartCollected();

        Destroy(gameObject);
    }
}
