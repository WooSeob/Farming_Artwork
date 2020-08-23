using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TransitionManager{

    public static class TransitionData{
        static List<string> Scenes = new List<string>();
        static int cursor = 0;

        static TransitionData(){
            Scenes.Add("Game");
            Scenes.Add("WooJae");
            Scenes.Add("Ahrang");
        }

        public static string getThisScene(){
            return Scenes[cursor];
        }
        public static string shiftToNext(){
            if(cursor < Scenes.Count - 1){
                cursor++;
            }
            return Scenes[cursor];
        }
        public static string shiftToPrev(){
            if(cursor > 0){
                cursor--;
            }
            return Scenes[cursor];
        }
        
    }
    public static class LayerManager{
        public static GameManager instance;
        static string currentScene;
        static int currentLayer = 0;
        static int LAYER_OFFSET = 25;
        static int LAYER_LENGTH = 2;

        public static void moveRight(GameObject BackGroundLayer, Transform Player){
        //
            if(currentLayer < LAYER_LENGTH - 1){
                BackGroundLayer.transform.position = new Vector3(-LAYER_OFFSET * (++currentLayer), 0, 1);
                Player.position = new Vector3(-8, Player.position.y, 0);
                Debug.Log("current Layer : " + currentLayer);
            }else{
                SceneManager.LoadScene(TransitionData.shiftToNext());
            }
        }
        public static void moveLeft(GameObject BackGroundLayer, Transform Player){
            if(currentLayer > 0){
                BackGroundLayer.transform.position = new Vector3(-LAYER_OFFSET * (--currentLayer), 0, 1);
                Player.position = new Vector3(8, Player.position.y, 0);
                Debug.Log("current Layer : " + currentLayer);
            }else{
                SceneManager.LoadScene(TransitionData.shiftToPrev());
            }
        }
    }

    public static class Movement{

        static bool isJump = false;
        public static void move(Rigidbody2D rigidbody2D,Transform Player){
            if(!GameManager.instance.isInConversation()){
            
                if(Input.GetKey(KeyCode.RightArrow)){
                    Player.position = new Vector3(Player.position.x + 0.1f, Player.position.y, 0);    
                }else if(Input.GetKey(KeyCode.LeftArrow)){
                    Player.position = new Vector3(Player.position.x - 0.1f, Player.position.y, 0);
                }

                //점프
                if(Input.GetKey(KeyCode.Space)){
                    if(!isJump){
                        isJump = true;
                        rigidbody2D.AddForce(Vector3.up * 300);
                    }
                }
            }
        }

        public static void finishJump(){
            isJump = false;
            Debug.Log("점프 완료");
        }
    }
}