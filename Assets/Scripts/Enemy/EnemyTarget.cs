using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTarget : MonoBehaviour
{
    
    public GameObject target;

    #region Singleton;

    public static EnemyTarget instance;

    void Awake() {
        instance = this;
        target = GameObject.FindGameObjectWithTag("Player");
    }

    #endregion


}