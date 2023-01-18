using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public enum EnemyState{
        Chase,
        Wander,
        Inactive
    }

    public EnemyState state;
    [SerializeField]private int moveSpeed;
    [SerializeField]private float timeBetweenMoves;
    [SerializeField]private Rigidbody2D rb;
    [SerializeField]private PlayerMovement player;
    private Vector3 direction;
    private bool movedRandom = false;

    // Start is called before the first frame update
    void Start()
    {
        state = EnemyState.Wander;   
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case EnemyState.Wander:
                if(movedRandom == false){
                    StartCoroutine(MoveRandomly());
                }
                break;
            case EnemyState.Chase:
                Chase();
                break;
            
        }
        if(state != EnemyState.Inactive){
            rb.AddForce(direction * moveSpeed * Time.deltaTime);
        }
        if(player.state == PlayerMovement.PlayerState.Normal){
            state = EnemyState.Chase;
        }
        if(player.state == PlayerMovement.PlayerState.Disguised){
            state = EnemyState.Wander;
        }
    }

    private IEnumerator MoveRandomly(){
        movedRandom = true;
        direction = new Vector3(Random.Range(-1,2), Random.Range(-1,2), 0);
        direction.Normalize();
        rb.velocity = new Vector2(0,0);
        yield return new WaitForSeconds(timeBetweenMoves);
        movedRandom = false;
    }

    private void Chase(){
        PlayerMovement player = FindObjectOfType<PlayerMovement>();
        Vector3 moveDirection = player.gameObject.transform.position - transform.position;
        moveDirection.Normalize();
        direction = moveDirection;
    }

}
