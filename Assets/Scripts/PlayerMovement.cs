using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum PlayerState{
        Normal, 
        Disguised
    }
    public PlayerState state;
    [SerializeField]private int moveSpeed;
    [SerializeField]private Rigidbody2D rb;
    [SerializeField]private GameObject player;
    [SerializeField]private GameObject enemy;
    [SerializeField]private float disguiseTime;
    [SerializeField]private float currentTime;
    [SerializeField]private GameObject win;
    [SerializeField]private GameObject lose;
    // Start is called before the first frame update
    void Start()
    {
        state = PlayerState.Normal;
        currentTime = disguiseTime;
    }

    // Update is called once per frame
    void Update()
    {
        switch(state){
            case PlayerState.Normal:       
                if(Input.GetKeyDown(KeyCode.K) && currentTime > 0){
                    state = PlayerState.Disguised;
                    player.SetActive(false);
                    enemy.SetActive(true);
                }
                break;
            case PlayerState.Disguised:
                if(Input.GetKeyDown(KeyCode.K)){
                    state = PlayerState.Normal;
                    player.SetActive(true);
                    enemy.SetActive(false);
                }
                currentTime -= Time.deltaTime;
                if(currentTime < 0){
                    state = PlayerState.Normal;
                    player.SetActive(true);
                    enemy.SetActive(false);
                }
                break;
        }

        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        direction.Normalize();
        rb.AddForce(direction * moveSpeed * 10 * Time.deltaTime);
    }

    // void CheckSling(){
    //     Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
    //     direction.Normalize();
    //     if(Input.GetKeyDown(KeyCode.K)){
    //         rb.AddForce(direction * moveSpeed * 50 * Time.deltaTime, ForceMode2D.Impulse);
    //         state = PlayerState.Move;
    //     }
    // }

    void OnTriggerEnter2D(Collider2D col){
        if(state == PlayerState.Normal && col.tag == "Enemy"){
            Debug.Log("Dead");
            lose.SetActive(true);
        }else if(col.tag == "End"){
            win.SetActive(true);
        }
    }
}
