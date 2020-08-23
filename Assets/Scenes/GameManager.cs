using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager instance;

    public GameObject msgUI;
    public Text msg;

    List<GameObject> prevContainers = new List<GameObject>();
    private List<List<CanvasSet>> ViewData = new List<List<CanvasSet>>();

    GameObject MessageUI;
    bool inConversation = false;

    public GameObject DiscriptionPrefab;
    public GameObject CanvasPrefab;
    public GameObject BackGroundLayer;
    
    int currentScene = 0;
    float SCENE_OFFSET = 21.09f;
    int SCENE_LENGTH = 9;

    private void Awake() {
        instance = this;
    }

    void Start()
    {
        MessageUI = GameObject.Find("Message");
        MessageUI.SetActive(false);

        List<CanvasSet> woojae = new List<CanvasSet>();
        woojae.Add(new CanvasSet().add(new int[2]{30, 40})
                                  .setPaths(new string[]{"1-김우재1"})
                                  .setMessages(new string[] {"<Float>: 스토리형 퍼즐어드벤처 게임, 김우재\n\n부유석을 생산하는 도시 A에서 알 수 없는 이유로 마력이 폭주해 도시 전체가 공중에\n떠버리고,마력을 사용하지 못하는 주인공은 도시 A에서 탈출하기 위해 건물 잔해를\n밟고 공항으로 향하다 누군가 의도적으로 자신이 마력을 사용하지 못하도록 만들었다는\n 사실과 도시 A의 비밀을 알게 되는데…… "}));
        woojae.Add(new CanvasSet().add(new int[2]{20, 27})
                                  .setPaths(new string[]{"1-김우재2"})
                                  .setMessages(new string[] {"<플레이어, 여성>, 김우재\n\n170 정도의 큰 키로, 성격은 조용하고 무뚝뚝하다.\n어느 날 공중에 떠버린 도시 A에서 탈출하기 위해 공항으로 향하다가 도시 A와 자신에 대한 비밀을 알게 되는데…… "}));
        ViewData.Add(woojae);


        //.setMessages(new string[] {""})
        List<CanvasSet> ahram = new List<CanvasSet>();
        ahram.Add(new CanvasSet().add(new int[2]{40, 30})
                                 .add(new int[2]{40, 30})
                                 .add(new int[2]{40, 30})
                                 .setPaths(new string[]{"2-노아람1", "2-노아람2", "2-노아람3"})
                                 .setMessages(new string[] {"<Call of Silence: 침묵의 부름>, 노아람\n\n늦은 밤 학교에서 장난삼아 의식을 치루는 몇몇 학생들에 의해 학교 내 이상한 괴물들이 출몰했습니다. 창문으로 바깥은 보고자 했지만, 가까이 다가가도 어두컴컴해 아무것도 보이지 않아요. 괴물들을 피해 가까스로 정문에 도착했지만 이미 드넓은 바다가 자리를 대신하고 있어요. 대체 무슨 일이 일어나고 있는 걸까요.\n우리는, 살아남을 수 있는 걸까요. "}));
        ViewData.Add(ahram);

        List<CanvasSet> gukhyun = new List<CanvasSet>();
        gukhyun.Add(new CanvasSet().add(new int[2]{40, 30})
                                   .add(new int[2]{40, 30})
                                   .setPaths(new string[]{"3-신국현1", "3-신국현2"})
                                   .setMessages(new string[] {"<고요한 성>, 신국현\n\n멸망한 중세도시. 군사적으로 중요한 곳에 튼튼하게 세워놓았던 성만이 남았다.\n툭 건드리면 부서질 것 같은 성은 고요하기만 하다. "}));
        ViewData.Add(gukhyun);

        List<CanvasSet> kyeongwon = new List<CanvasSet>();
        kyeongwon.Add(new CanvasSet().add(new int[2]{40, 30})
                                     .setPaths(new string[]{"4-이경원1"})
                                     .setMessages(new string[] {"<Re:flacted>: 모바일 어드벤처 게임, 이경원 (아크릴)\n\n색이 사라진 세계에서 색상을 찾기 위해 모험을 떠나는 이야기 "}));
        kyeongwon.Add(new CanvasSet().add(new int[2]{20, 20})
                                     .setPaths(new string[]{"4-이경원2"})
                                     .setMessages(new string[] {"<플레이어가 타고 다니는 이동식 요새>, 이경원 (펜드로잉) "}));
        ViewData.Add(kyeongwon);

        List<CanvasSet> jihyun = new List<CanvasSet>();
        jihyun.Add(new CanvasSet().add(new int[2]{40, 30})
                                  .add(new int[2]{40, 30})
                                  .add(new int[2]{40, 30})
                                  .setPaths(new string[]{"5-임지현1", "5-임지현2", "5-임지현3"})
                                  .setMessages(new string[] {"<꼬마 시장 A와 빈민가 소년 B의 이야기>, 임지현\n\n로봇과 인간이 공존하며 모두가 평등하고 행복한 나라 유토피아!\n……정말로 모두가 평등하고 행복한가요? "}));
        ViewData.Add(jihyun);
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

    public void moveNext(Transform Player){
        //오른쪽씬으로 이동
        if(currentScene < SCENE_LENGTH - 1){
            BackGroundLayer.transform.position = new Vector3(-SCENE_OFFSET * (++currentScene), 0, 1);
            Player.position = new Vector3(-7, Player.position.y, 0);
            Debug.Log("current Scene : " + currentScene);

            ViewManager(currentScene - 2);
            if(currentScene == 7){
                //엔딩 소감 올라가기
                GameObject.Find("sogam").GetComponent<sogam>().startEnding();
            }else if(currentScene == 8){
                Player.position = new Vector3(3, Player.position.y, 0);
            }
        }
    }
    public void movePrev(Transform Player){
        //왼쪽씬으로 이동
        if(currentScene > 0){
            BackGroundLayer.transform.position = new Vector3(-SCENE_OFFSET * (--currentScene), 0, 1);
            Player.position = new Vector3(7, Player.position.y, 0);
            Debug.Log("current Scene : " + currentScene);

            ViewManager(currentScene - 2);
            if(currentScene == 7){
                //엔딩 소감 올라가기
                GameObject.Find("sogam").GetComponent<sogam>().startEnding();
            }
        }
    }

    public void ViewManager(int page){
        if(prevContainers.Count > 0){
            foreach(GameObject container in prevContainers){
                Destroy(container);
            }
            prevContainers.Clear();
        }
    
        if(page >= 0 && page < ViewData.Count){
            List<CanvasSet> currentView = ViewData[page];

            //CanvasSet prev = null;
            float totalWidth = 0;
            for(int i = 0; i < currentView.Count; i++){
                CanvasSet cs = currentView[i];
                cs.draw();
                totalWidth += cs.getWidth();
            }

            //첫 번째 컨테이너 세팅
            float prevX = -(totalWidth/2f);
            CanvasSet prevCs = currentView[0];
            prevCs.setContainerX(prevX);

            for(int i = 1; i < currentView.Count; i++){
                float thisX = prevX + prevCs.getWidth() + 1;
                currentView[i].setContainerX(thisX);

                prevX = thisX;
                prevCs = currentView[i];
            }
        }
    }
    
    public class CanvasSet{
        public List<int[]> canvases = new List<int[]>();
        public string[] paths;
        private string[] descriptions;

        private float containerWidth, containerHeight;
        GameObject canvasContainer;
        
        public CanvasSet add(int[] size){
            canvases.Add(size);
            return this;
        }
        
        public CanvasSet setPaths(string[] paths){
            this.paths = paths;
            return this;
        }
        public CanvasSet setMessages(string[] desc){
            this.descriptions = desc;
            return this;
        }

        public List<int[]> get(){
            return canvases;
        }

        public float getWidth(){
            return containerWidth;
        }

        public void draw(){
            canvasContainer = new GameObject("CanvasContainer");
            //canvasContainer.transform.parent = GameObject.Find("Layer3").GetComponent<Transform>();

            GameObject description = Instantiate(GameManager.instance.DiscriptionPrefab) as GameObject;
            description.transform.parent = canvasContainer.transform;
            description.transform.localPosition = new Vector3(1, 0, 0);
            description.GetComponent<description>().setMessages(descriptions);
            
            float width = description.GetComponent<SpriteRenderer>().bounds.size.x;
            float height = description.GetComponent<SpriteRenderer>().bounds.size.y;
            Debug.Log("description width : " + width + ", height : " + height);

            float x = 1, y;
            GameObject addCanvas;

            for(int i = 0; i < canvases.Count; i++){
                int[] size = canvases[i];

                addCanvas = Instantiate(GameManager.instance.CanvasPrefab) as GameObject;
                addCanvas.transform.parent = canvasContainer.transform;
                addCanvas.transform.localScale = new Vector3(size[0], size[1], 0);
                addCanvas.GetComponent<canvasViewer>().setPath(paths[i]);
                
                float prevWidth = width;
                float prevHeight = height;

                width = addCanvas.GetComponent<SpriteRenderer>().bounds.size.x;
                height = addCanvas.GetComponent<SpriteRenderer>().bounds.size.y;

                x += prevWidth/2 + width/2 + 1;
                y = height/2 - 0.25f;
                
                addCanvas.transform.localPosition = new Vector3(x, y, 0);

                Debug.Log("addCanvas " + i + " width : " + width + ", height : " + height);
            }
            
            this.containerWidth = x + width/2;
            Debug.Log("containers width : " + (x + width/2));

            canvasContainer.transform.position = new Vector3(0, -3, 0);
            GameManager.instance.prevContainers.Add(canvasContainer);
        }
        
        public void setContainerX(float xPos){
            canvasContainer.transform.position = new Vector3(xPos, -3, 0);
        }
    }
}



























