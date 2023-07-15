using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBlade : MonoBehaviour
{
    [SerializeField]
    private Transform start;
    [SerializeField]
    private Transform end;
    [SerializeField]
    private Transform sawBladeSprite;
    
    private float positionPercent;
    private int direction = 1;


    // Update is called once per frame
    void Update()
    {
        positionPercent += Time.deltaTime * direction;

        //Lerping, or 'linear interpolation' is a formula to find a point between two numbers given
        // a percentage value. - Lerping in Unity - "You HAVE to know this!!" - Tarodev - Sep 9, 2021 (https://www.youtube.com/watch?v=JS7cNHivmHw&t)
        // Tarodev introduces the AnimationCurve which I would have loved to use to ease the saw a bit or add other behaviors!
        sawBladeSprite.position = Vector3.Lerp(start.position, end.position, positionPercent);

        if(positionPercent >= 1 && direction == 1) {
            direction = -1;
        }
        else if (positionPercent <= 0 && direction == -1){
            direction = 1;
        }

        Debug.Log(positionPercent);
    }
}
