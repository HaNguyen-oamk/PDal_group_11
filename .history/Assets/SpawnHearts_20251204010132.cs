using UnityEngine;

public class SpawnHearts : MonoBehaviour
{
    public GameObject heartPrefab;  // Drag Heart prefab
    public int heartsToSpawn = 8;   // How many
    public Vector2 spawnAreaMin = new Vector2(-15, -8);  // Adjust to your map
    public Vector2 spawnAreaMax = new Vector2(15, 8);

    void Start()
    {
        for (int i = 0; i < heartsToSpawn; i++)
        {
            Vector2 pos = new Vector2(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y)
            );
            Instantiate(heartPrefab, pos, Quaternion.identity);
        }
    }
}