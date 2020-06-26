using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    Transform playerTrans;

    void Start()
    {
        playerTrans = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        CameraMovement();
    }

    private void CameraMovement()
    {
        this.transform.position = new Vector3(playerTrans.transform.position.x, playerTrans.transform.position.y, -10);
    }


}
