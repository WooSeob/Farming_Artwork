using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;

    GameObject MessageUI;
    bool inConversation = false;


    private void Awake() {
        instance = this;
    }

    void Start()
    {
        MessageUI = GameObject.Find("Message");
        MessageUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool isInConversation(){
        return inConversation;
    }
    public void setInConversation(bool c){
        inConversation = c;
    }
}
