using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class description : MonoBehaviour
{
    private GameObject target;
    private string text;
    private Coroutine prevCoroutine = null;
    public GameObject msgUI;
    public Text msg;

    int currentMsg = 0;
    string[] Messages;

    bool isInArea = false;

    // Start is called before the first frame update
    void Start()
    {
        msgUI = GameManager.instance.msgUI;
        msg = GameManager.instance.msg;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space)){
            if(GameManager.instance.isInConversation() && prevCoroutine == null){
                Debug.Log(currentMsg);
                if(currentMsg < Messages.Length - 1){
                    //다음 메시지
                    prevCoroutine = StartCoroutine("TypingAction", Messages[++currentMsg]);
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
                prevCoroutine = StartCoroutine("TypingAction", Messages[currentMsg]);
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

    public void setMessages(string[] Messages){
        this.Messages = Messages;
    }
    IEnumerator TypingAction(string originText){

        string subText = "";
        for(int i = 0; i< originText.Length; i++){
            yield return new WaitForSeconds(0);

            subText += originText.Substring(0,i);
            msg.text = subText;
            subText= "";
        }
        prevCoroutine = null;
    }
}



/*
void FixedUpdate(){

        if (Input.GetMouseButtonDown (0)) {
            CastRay ();
            if (target == this.gameObject) {  //타겟 오브젝트가 스크립트가 붙은 오브젝트라면

                // 여기에 실행할 코드를 적습니다.
                
    
            } 
        }
    }

    void CastRay(){ // 유닛 히트처리 부분.  레이를 쏴서 처리합니다. 
        target = null;
        Vector2 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);
        if (hit.collider != null) { //히트되었다면 여기서 실행
            Debug.Log (hit.collider.name);  //이 부분을 활성화 하면, 선택된 오브젝트의 이름이 찍혀 나옵니다. 
            target = hit.collider.gameObject;  //히트 된 게임 오브젝트를 타겟으로 지정
        }
    }
*/