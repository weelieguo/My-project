using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_2019_1_OR_NEWER && INPUTSYSTEM_AVAILABLE
using UnityEngine.InputSystem;
#endif

using TiltFive.Logging;
using TiltFive;

public class ObjectMover : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject targetObject;
    RaycastHit hit;
    Vector3 targetObjectNextPosition;
    float targetObjectHeight;
    float triggerDisplacement;
    Vector2 stickTilt;

    void Start()
    {
        targetObjectNextPosition = targetObject.transform.position;
        targetObjectHeight = targetObject.GetComponent <MeshRenderer>().bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetMouseButtonUp(0)){
        //     Vector3 worldMousePosition = 
        //                                 Camera.main.ScreenToWorldPoint(
        //                                     new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 200f));
           if (TiltFive.Input.GetWandAvailability() && (TiltFive.Input.GetTrigger() >0)){
            stickTilt = TiltFive.Input.GetStickTilt();

            Vector3 worldMousePosition = 
                                        Camera.main.ScreenToWorldPoint(
                                            new Vector3 (stickTilt.x, stickTilt.y, stickTilt.magnitude));

            Vector3 direction = worldMousePosition - Camera.main.transform.position;

            if (Physics.Raycast(Camera.main.transform.position, direction, out hit, 100f)){
                Debug.DrawLine(Camera.main.transform.position, hit.point, Color.green,0.5f);
                if(hit.collider.gameObject.name == "Plane"){
                    targetObjectNextPosition = hit.point + new Vector3(0f,targetObjectHeight/2f,0f);
                }
                else{
                    targetObjectNextPosition = hit.point;
                }
                // targetObjectNextPosition = hit.point + new Vector3(0f,targetObjectHeight/2f,0f);
                // targetObject.transform.position = targetObjectNextPosition;
            }
            else{
                Debug.DrawLine(Camera.main.transform.position, worldMousePosition, Color.red,0.5f);
            }
        }
        targetObject.transform.position = Vector3.MoveTowards(targetObject.transform.position, targetObjectNextPosition, 5f * Time.deltaTime);
    }
}
