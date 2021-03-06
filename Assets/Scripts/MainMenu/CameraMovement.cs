﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 avatarPosition;
    public GameObject avatarMasked;
    public float offset = 5f;
    public float offsetSmoothing = 0.5f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
         
      if ( avatarMasked.activeInHierarchy == true )
        {
        // camera should only change x-axis
        avatarPosition = new Vector3(avatarMasked.transform.position.x, transform.position.y, transform.position.z);

        // player moves to the right
        if(avatarMasked.transform.localScale.x > 0f)
        {
          avatarPosition = new Vector3(avatarMasked.transform.position.x + offset, avatarPosition.y, avatarPosition.z);
        }
        else
        {
          avatarPosition = new Vector3(avatarMasked.transform.position.x - offset, avatarPosition.y, avatarPosition.z);
        }
        }
        
      // ensuring camera change is slow and smoothe
      {transform.position = Vector3.Lerp(transform.position, avatarPosition, offsetSmoothing*Time.deltaTime);}
    
  }

}
