using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    Transform t;
    Rigidbody2D rbody;
    bool isJump = false;

    int currentScene = 0;
    int SCENE_OFFSET = 25;
    int SCENE_LENGTH = 3;
    GameObject BackGroundLayer;
    void Start()
    {
        t = transform;
        rbody = GetComponent<Rigidbody2D>();
        Debug.Log(Screen.width);
        BackGroundLayer =  GameObject.Find("BackGround");
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
            
            //점프
            if(Input.GetKey(KeyCode.Space)){
                if(!isJump){
                    isJump = true;
                    rbody.AddForce(Vector3.up * 300);
                }
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
        
        if(other.gameObject.name == "Right" && currentScene < SCENE_LENGTH - 1){
            //오른쪽씬으로 이동
            BackGroundLayer.transform.position = new Vector3(-SCENE_OFFSET * (++currentScene), 0, 1);
            transform.position = new Vector3(-8, t.position.y, 0);
            Debug.Log("current Scene : " + currentScene);

        }else if(other.gameObject.name == "Left" && currentScene > 0){
            //왼쪽씬으로 이동
            BackGroundLayer.transform.position = new Vector3(-SCENE_OFFSET * (--currentScene), 0, 1);
            transform.position = new Vector3(8, t.position.y, 0);
            Debug.Log("current Scene : " + currentScene);
        }
        
    }
}
