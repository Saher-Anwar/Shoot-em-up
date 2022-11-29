using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Declaring required variables
    [SerializeField]
    private GameObject enemyPrefab;     // Enemy model to clone
    [SerializeField]
    private float spawnInterval = 2f;   // Interval between each spawn

    public Character player;            // To use player position when casting sphere
    public float spawnRange = 25f;      // Radius of the sphere
    public LayerMask spawnMask;         // Layer of the spawnpoints

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Checking all collisions with spawn points and saving in array
        Collider[] contactPoints = Physics.OverlapSphere(player.transform.position, spawnRange, spawnMask);
        foreach (var contactPoint in contactPoints)
        {
            Spawn(contactPoint?.transform);  // Spawning for each collision
        }
    }

    // Enemy Spawner at set intervals
    void Spawn(Transform spawnLocation)
    {
        spawnInterval -= Time.deltaTime;
        if (spawnInterval <= 0)
        {
            Instantiate(enemyPrefab, spawnLocation.position, spawnLocation.rotation);
            spawnInterval = 2f;
        }
    }

    // Drawing a gizmo sphere for better visualization
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(player.transform.position, spawnRange);
    }

}