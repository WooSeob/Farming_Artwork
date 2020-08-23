using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sogam : MonoBehaviour
{
    bool isStart = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void startEnding(){
        transform.localPosition = new Vector3(transform.localPosition.x, -18.8f, 0);
        isStart = true;

    }
    // Update is called once per frame
    void Update()
    {
        if(isStart){
            if(transform.localPosition.y > 1){
                isStart = false;
            }else{
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + 0.9f*Time.deltaTime, 0);
            }
        }
    }
}
