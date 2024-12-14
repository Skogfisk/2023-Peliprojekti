using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float panSpeed = 5f;
    public float panBorder = 25f;
    public float panLimitY = 3f;
    public float panLimitX = 3f;

    // Update is called once per frame
    void Update()
    {

        Vector3 pos = transform.position;


        if (Input.GetKey("w") || Input.mousePosition.y > Screen.height - panBorder)
        {
            pos.y += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s") || Input.mousePosition.y < panBorder)
        {
            pos.y -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d") || Input.mousePosition.x > Screen.width - panBorder)
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a") || Input.mousePosition.x < panBorder)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }

        pos.y = Mathf.Clamp(pos.y, -panLimitY, panLimitY);
        pos.x = Mathf.Clamp(pos.x, -panLimitX, panLimitX);

        transform.position = pos;

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
