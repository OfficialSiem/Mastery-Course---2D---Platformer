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
    [SerializeField]
    private float speed = 1;

    private float positionPercent;
    private int direction = 1;


    // Update is called once per frame
    void Update()
    {
        //The distance between start and end will help us determine...
        float distance = Vector3.Distance(start.position, end.position);

        // At what constant rate the saw must travel..
        float speedForDistance = speed/distance;

        // knowing the rate will help calculate how far along the saw is in its journey
        positionPercent += Time.deltaTime * direction * speedForDistance;

        // We lerp the saw's position accounting for the overall percentage of its total trek
        // (0% or 0.0, still at start, 100% or 1.0, finally reached the end) 

        //Lerping, or 'linear interpolation' is a formula to find a point between two numbers given
        // a percentage value. - Lerping in Unity - "You HAVE to know this!!" - Tarodev - Sep 9, 2021 (https://www.youtube.com/watch?v=JS7cNHivmHw&t)
        // Tarodev introduces the AnimationCurve which I would have loved to use to ease the saw a bit or add other behaviors!
        sawBladeSprite.position = Vector3.Lerp(start.position, end.position, positionPercent);

        if(positionPercent >= 1 && direction == 1) {
            direction = -1;
        }
        else if (positionPercent <= 0 && direction == -1){
            //Once we hit the end, turn around and start moving backwards lol
            direction = 1;
        }


    }
}
