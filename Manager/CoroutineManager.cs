using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CoroutineManager 
{
    private static Dictionary<float, WaitForSeconds> waitForSeconds = new Dictionary<float, WaitForSeconds>();
    private static Dictionary<float, WaitForSecondsRealtime> waitForSecondsRealtime = new Dictionary<float, WaitForSecondsRealtime>();
    private static readonly object waitUpdate = null;
     
    public static object WaitUpdate {get => waitUpdate;}

    public static WaitForSeconds WaitSeconds(float waitingTime){
        if(!waitForSeconds.ContainsKey(waitingTime))
            waitForSeconds.Add(waitingTime, new WaitForSeconds(waitingTime));

        return waitForSeconds[waitingTime];
    }

    public static WaitForSecondsRealtime WaitSecondsRealTime(float waitingTime){
        if(!waitForSecondsRealtime.ContainsKey(waitingTime))
            waitForSecondsRealtime.Add(waitingTime, new WaitForSecondsRealtime(waitingTime));

        return waitForSecondsRealtime[waitingTime];
    }
}
