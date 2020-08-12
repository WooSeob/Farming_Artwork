using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class npc : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject msgUI;
    public Text msg;

    int currentMsg = 0;
    string[] MSGs = {"메시지1  ", "메시지2 ", "메시지3 "};

    Coroutine prevCoroutine = null;

    void Start()
    {
        msg = msg.GetComponent<Text>();
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
    }

    private void OnTriggerEnter2D(Collider2D other) {
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

    IEnumerator TypingAction(string originText){
        string subText = "";
        for(int i = 0; i< originText.Length; i++){
            yield return new WaitForSeconds(0.08f);

            subText += originText.Substring(0,i);
            msg.text = subText;
            subText= "";
        }
        prevCoroutine = null;
    }
}
