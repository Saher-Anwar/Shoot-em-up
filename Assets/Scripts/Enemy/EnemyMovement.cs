using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    public float moveSpeed;
    public CharacterController enemyControl;
    public Character playerControl;

    // Start is called before the first frame update
    void Start()
    {
        enemyControl = GetComponent<CharacterController>();
        playerControl = FindObjectOfType<Character>();
    }

    void FixedUpdate() {
        enemyControl.Move(transform.forward * moveSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(playerControl.transform.position);
    }
}
