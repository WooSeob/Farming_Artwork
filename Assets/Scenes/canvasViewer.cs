using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvasViewer : MonoBehaviour
{
    string path;
    bool isInArea = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setPath(string path){
        this.path = path;
    }
    // Update is called once per frame
    void Update()
    {
        //그림 보기
        if(Input.GetKey(KeyCode.UpArrow) && isInArea){
            Debug.Log(GameManager.instance.isInConversation());

            if(!GameManager.instance.isInConversation()){
                GameManager.instance.setInConversation(true);
                GameObject.Find("CanvasViewer").GetComponent<Transform>().position = new Vector3(0, 0, 0);
                GameObject.Find("content").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(path);
            }
        }
        if(Input.GetKey(KeyCode.Escape) && isInArea){
            if(GameManager.instance.isInConversation()){
                GameManager.instance.setInConversation(false);
                GameObject.Find("CanvasViewer").GetComponent<Transform>().position = new Vector3(0, 20, 0);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.name == "Player"){
            isInArea = true;
        }
        Debug.Log(isInArea);
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.name == "Player"){
            isInArea = false;
        }
        Debug.Log(isInArea);
    }
}
