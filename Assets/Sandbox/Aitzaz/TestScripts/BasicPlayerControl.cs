using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlayerControl : MonoBehaviour
{

    public float moveSpeed;
    public Rigidbody playerBody;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        playerBody.velocity = new Vector3(Input.GetAxis("Horizontal")*moveSpeed, playerBody.velocity.y, Input.GetAxis("Vertical") * moveSpeed);
    }
}
