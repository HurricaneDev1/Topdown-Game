using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public enum EnemyState{
        Chase,
        Wander,
        Inactive,
        Patrol
    }

    public EnemyState state;
    [SerializeField]private int moveSpeed;
    [SerializeField]private float timeBetweenMoves;
    [SerializeField]private Rigidbody2D rb;
    [SerializeField]private PlayerMovement player;
    [SerializeField]private Animator anim;
    private Vector3 direction;
    private bool movedRandom = false;
    [SerializeField]private float searchDis;
    [SerializeField]private List<Transform> points = new List<Transform>();
    [SerializeField]private int nextPoint;

    // Start is called before the first frame update
    void Start()
    {
        state = EnemyState.Patrol;   
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
            case EnemyState.Patrol:
                Patrol();
                break;
            
        }
        if(state != EnemyState.Inactive){
            rb.AddForce(direction * moveSpeed * Time.deltaTime);
        }
        if(player.state == PlayerMovement.PlayerState.Normal && Vector3.Distance(transform.position, player.transform.position) < searchDis){
            state = EnemyState.Chase;
            anim.ResetTrigger("SwapState");
            anim.SetTrigger("SwapState2");
        }
        if(player.state == PlayerMovement.PlayerState.Disguised){
            state = EnemyState.Patrol;
            nextPoint = Random.Range(01, (nextPoint + 4) % points.Count);
            anim.ResetTrigger("SwapState2");
            anim.SetTrigger("SwapState");
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

    void OnCollision2D(Collider2D col){
        direction = new Vector3(-direction.x,-direction.y,0);
    }

    void Patrol(){
        Vector3 moveDirection = points[nextPoint].position - transform.position;
        moveDirection.Normalize();
        direction = moveDirection;
        if(Vector3.Distance(transform.position, points[nextPoint].position) < 1f){
            nextPoint = (nextPoint + 1) % points.Count;
            rb.velocity = new Vector2(0,0);
        }
    }
}
