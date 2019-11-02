﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraRayTool : MonoBehaviour
{

    public Camera cam;
    public GameObject cursor;
    
    private bool cursorBool;

    // Updates the cursor for the user so they know where they are looking
    public void updateCursor(bool isRenderable, Vector3 pos)
    {
        if (isRenderable)
        {
            cursor.GetComponent<MeshRenderer>().enabled = true;
        }
        else
        {
            cursor.GetComponent<MeshRenderer>().enabled = false;
        }

        cursor.transform.position = pos;
    }

    //Destroy Block
    public void DestroyBlock(GameObject obj) {
        if (obj.tag == "block") {
            DestroyImmediate(obj);
        }
    }

    //Add Block

    // Update is called once per frame
    void Update()
    {
            int layerMask = 1 << 8;
            layerMask = ~layerMask;
            RaycastHit hit;

        //scan
        if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask) && Input.GetMouseButton(2))
        {
            Debug.DrawRay(cam.transform.position, cam.transform.TransformDirection(Vector3.forward) * hit.distance, Color.magenta);
            cursorBool = true;
            updateCursor(cursorBool, hit.point);
        }
        //delete
        else if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask) && Input.GetMouseButton(0))
        {
            Debug.DrawRay(cam.transform.position, cam.transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            cursorBool = true;
            updateCursor(cursorBool, hit.point);
            DestroyBlock(hit.collider.gameObject);
        }
        //place
        else if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask) && Input.GetMouseButton(1))
        {
            Debug.DrawRay(cam.transform.position, cam.transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
            cursorBool = true;
            updateCursor(cursorBool, hit.point);
        }
        //looking at block
        else if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(cam.transform.position, cam.transform.TransformDirection(Vector3.forward) * hit.distance, Color.white);
            cursorBool = true;
            updateCursor(cursorBool, hit.point);
        }
        //looking into the void thinking about your C# grade
        else
        {
            Debug.DrawRay(cam.transform.position, cam.transform.TransformDirection(Vector3.forward) * 1000, Color.black);
            cursorBool = false;
            updateCursor(cursorBool, hit.point);
        }

       
    }
}

