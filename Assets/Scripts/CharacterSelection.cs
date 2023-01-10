using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    Vector3 targetRot;
    Vector3 currentAngle;
    int currentSelection ;
    int totalcharacters = 4;
    // Start is called before the first frame update
    void Start()
    {
        currentSelection = 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && currentSelection < totalcharacters)
        {
            currentAngle = transform.eulerAngles;
            targetRot = targetRot + new Vector3(0, 90, 0);
            currentSelection++;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && currentSelection > 1)
        {
            currentAngle = transform.eulerAngles;
            targetRot = targetRot - new Vector3(0, 90, 0);
            currentSelection--;
        }

        currentAngle = new Vector3(0, Mathf.LerpAngle(currentAngle.y, targetRot.y, Time.deltaTime * 5), 0);
        transform.eulerAngles = currentAngle;
    }
}
