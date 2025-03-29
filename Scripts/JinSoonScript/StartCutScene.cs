using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StartCutScene : MonoBehaviour
{
    private PlayableDirector timeLine;

    private void Awake()
    {
        timeLine = GetComponent<PlayableDirector>();
        //timeLine
    }

}
