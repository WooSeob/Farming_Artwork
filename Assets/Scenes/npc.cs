using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class npc : MonoBehaviour
{
    // Start is called before the first frame update

    bool isInArea = false;

    public GameObject msgUI;
    public Text msg;

    int currentMsg = 0;
    string[] MSGs = {"Hello, I’m DaHyun, president of Farming-Games in HKNU.\nWe proceeded with the Artwork Project last semester(2020.5~7).\nThis gallery displays the works that were done then.\nI hope you enjoy it comfortably. "};

    Coroutine prevCoroutine = null;

    void Start()
    {
        msgUI = GameManager.instance.msgUI;
        msg = GameManager.instance.msg;
    }

    // Update is called once per frame
    void Update()
    {
        //다음
        if(Input.GetKey(KeyCode.Space)){
            if(GameManager.instance.isInConversation() && prevCoroutine == null){
                Debug.Log(currentMsg);
                if(currentMsg < MSGs.Length - 1){
                    //다음 메시지
                    prevCoroutine = StartCoroutine("TypingAction", MSGs[++currentMsg]);
                }else{
                    //대화 종료
                    msgUI.SetActive(false);
                    GameManager.instance.setInConversation(false);
                    currentMsg = 0;
                }
            }
        }
        //설명 보기
        if(Input.GetKey(KeyCode.UpArrow) && isInArea){
            Debug.Log(GameManager.instance.isInConversation());

            if(!GameManager.instance.isInConversation()){
                GameManager.instance.setInConversation(true);
                msgUI.SetActive(true);

                if(prevCoroutine != null){
                    StopCoroutine(prevCoroutine);
                }
                prevCoroutine = StartCoroutine("TypingAction", MSGs[currentMsg]);
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

    IEnumerator TypingAction(string originText){
        string subText = "";
        for(int i = 0; i< originText.Length; i++){
            yield return new WaitForSeconds(0.01f);

            subText += originText.Substring(0,i);
            msg.text = subText;
            subText= "";
        }
        prevCoroutine = null;
    }
}
