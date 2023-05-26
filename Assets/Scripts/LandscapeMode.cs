using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandscapeMode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }
}
