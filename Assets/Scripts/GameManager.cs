using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Declaring required variables
    [SerializeField]
    private GameObject enemyPrefab;     // Enemy model to clone
    [SerializeField]
    private float spawnInterval = 2f;   // Interval between each spawn
    [SerializeField]
    public float targetKills;

    public Character player;            // To use player position when casting sphere
    public float spawnRange = 25f;      // Radius of the sphere
    public LayerMask spawnMask;         // Layer of the spawnpoints
    public float enemiesKilled;
    public TextMeshProUGUI killCountText;

    [Header("Component")]
    public TextMeshProUGUI timerText;
    [Header("Timer Settings")]
    public float currentTime;
    public bool countDown;

    private float setTime;

    // Start is called before the first frame update
    void Start()
    {
        enemiesKilled = 0;
        setTime = currentTime;
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

        if (countDown)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            currentTime += Time.deltaTime;
        }
        timerText.text = currentTime.ToString("0.0");
        if (countDown && currentTime <= 0 || !countDown && currentTime >= setTime)
        {
            if (targetKills <= enemiesKilled)
            {
                SceneManager.LoadScene("WinScreen");
            }
            else
            {
                SceneManager.LoadScene("LoseScreen");
            }

        }
    }

    // Enemy Spawner at set intervals
    void Spawn(Transform spawnLocation)
    {
        if (spawnLocation == null) return;
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

    public void IncreaseKillCount()
    {
        enemiesKilled++;
        killCountText.text = enemiesKilled.ToString();
    }
}

