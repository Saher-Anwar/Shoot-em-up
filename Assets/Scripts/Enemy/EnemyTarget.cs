using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTarget : MonoBehaviour
{
    #region Singleton;

    public static EnemyTarget instance;

    void Awake() {
        instance = this; 
    }

    #endregion

    public GameObject target;

}