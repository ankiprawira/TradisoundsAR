using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageCheck : MonoBehaviour
{
    public string PopUpTrigger;
    public string FadeTrigger;
    private Animator nAnimation;
    // Start is called before the first frame update
    void Start()
    {
        nAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update(){

    }

    public void ScaleUp(){
        nAnimation.SetTrigger(PopUpTrigger);
        nAnimation.ResetTrigger(FadeTrigger);
    }

    public void FadeDown(){
        nAnimation.SetTrigger(FadeTrigger);
        nAnimation.ResetTrigger(PopUpTrigger);
    }
}
