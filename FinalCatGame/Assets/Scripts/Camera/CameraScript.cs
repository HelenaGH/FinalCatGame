using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    float timeOffset;

    [SerializeField]
    Vector2 posOffset; //pozicija offset-a

    //private Vector3 velocity; //for smooth damping purposes

    [SerializeField]
    float leftLimit;

    [SerializeField]
    float rightLimit;

    [SerializeField]
    float topLimit;

    [SerializeField]
    float bottomLimit;

    void Update()
    {
        Vector3 startPos = transform.position; //trenutna pozicija kamere
        Vector3 endPos = player.transform.position; //pozicija igrača

        endPos.x += posOffset.x;
        endPos.y += posOffset.y;
        endPos.z = -10;

        //refined with lerping or smoothdamping

        //transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10); 

        transform.position = Vector3.Lerp(startPos, endPos, timeOffset * Time.deltaTime); // this is how you lerp

        transform.position = new Vector3
            (
                  Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
                  Mathf.Clamp(transform.position.y, bottomLimit, topLimit),
                  transform.position.z
            );

        //the same thing as lerping but with smooth damping
        //transform.position = Vector3.SmoothDamp(startPos, endPos, ref velocity, timeOffset); 
    }
}
