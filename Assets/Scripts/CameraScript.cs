using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Start is called before the first frame update

    public float followSpeed = 10f;

    private Transform player;

    private Vector3 pos;
    void Start()
    {
        player = GameObject.Find("Player").transform;
        pos = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position + pos, followSpeed*Time.deltaTime);
        }
    }
}
