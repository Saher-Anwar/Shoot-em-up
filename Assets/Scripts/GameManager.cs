using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private GameObject enemyPrefab;
    private float spawnInterval = 2f;

    public Character player;
    public float spawnRange = 25f;
    public LayerMask spawnMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] contactPoints = Physics.OverlapSphere(player.transform.position, spawnRange, spawnMask);
        foreach (var contactPoint in contactPoints)
        {
            Spawn(contactPoint.transform);
        }
    }

    // Enemy Spawner at set intervals
    void Spawn(Transform spawnLocation)
    {
        spawnInterval -= Time.deltaTime;
        if (spawnInterval <= 0)
        {
            Instantiate(enemyPrefab, spawnLocation);
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
