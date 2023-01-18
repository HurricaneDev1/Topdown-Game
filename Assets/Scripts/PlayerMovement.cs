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
    // Start is called before the first frame update
    void Start()
    {
        state = PlayerState.Normal;
    }

    // Update is called once per frame
    void Update()
    {
        switch(state){
            case PlayerState.Normal:       
                if(Input.GetKeyDown(KeyCode.K)){
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
}
