using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    Transform t;
    Rigidbody2D rbody;
    bool isJump = false;


    void Start()
    {
        t = transform;
        rbody = GetComponent<Rigidbody2D>();
        Debug.Log(Screen.width);
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.instance.isInConversation()){
            if(Input.GetKey(KeyCode.RightArrow)){
                transform.position = new Vector3(t.position.x + 0.1f, t.position.y, 0);    
            }else if(Input.GetKey(KeyCode.LeftArrow)){
                transform.position = new Vector3(t.position.x - 0.1f, t.position.y, 0);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "ground"){
            Debug.Log("점프 완료");
            isJump = false;
        }
        
        if(other.gameObject.tag == "deadzone"){
            transform.position = new Vector3(0,0,0);
        }
        
        if(other.gameObject.tag == "next"){
            GameManager.instance.moveNext(transform);
        }else if(other.gameObject.tag == "prev"){
            GameManager.instance.movePrev(transform);
        }
        
    }    
}
