using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxing : MonoBehaviour
{
    public Transform[] backgrounds; //Array of all back and foregrounds to be parallaxed
    private float []parallaxScales; // the proportion of the camera's moevement to move the background by
    public float smoothing = 1f; // how smooth the parallax is going to be. Make sure to set this above 0

    private Transform cam;  // Reference to the main camera's transform
    private Vector3 previousCamPos; // store the position of the camera in the previos frame
    //Awake is call before start(). Great for references
    private void Awake(){
        // set up camera the reference
        cam = Camera.main.transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        // The previous frame had the current frame's camera position
        previousCamPos = cam.position;
        //asigning corresponding parallaxScales;
        parallaxScales = new float[backgrounds.Length];
        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // for each background:
        for (int i = 0; i < backgrounds.Length; i++)
        {
            // the parallax is the opposite of the camera movement because the previous frame multiplied by the sacale
            float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];

            // set a target x position wich is the current position plus the parallax.
            float backgroundTargetPosX = backgrounds[i].position.x + parallax;
            // create a target position wich is the background's current position with it's target x position
            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);
            // fade between current position and the target position using lerp
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }
        // set the previousCamPos to the camera's position at the end of the frame
        previousCamPos = cam.position;
    }
}
