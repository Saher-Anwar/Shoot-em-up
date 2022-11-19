using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLocator : MonoBehaviour
{
    public float range=0.25f;
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
