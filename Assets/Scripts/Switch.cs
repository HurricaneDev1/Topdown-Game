using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col){
        Debug.Log("Hit");
        if(col.gameObject.GetComponent<EnemyMovement>()){
            col.gameObject.GetComponent<EnemyMovement>().state = EnemyMovement.EnemyState.Inactive;
        }
    }
}
