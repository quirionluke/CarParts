using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * ObjectInteract will handle the general interacting of objects using the mouse.
 * 
 */
public class ObjectInteract : MonoBehaviour
{
    public static GameObject selectedObj;

    public static GameObject[] attachPoints;

    public static bool paintmode;

    // Start is called before the first frame update
    void Start()
    {
        paintmode = false;
        attachPoints = GameObject.FindGameObjectsWithTag("AttachPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if(selectedObj != null)
        {
            foreach(GameObject g in attachPoints)
            {
                g.GetComponent<MeshRenderer>().enabled = true;
                
            }
        }
        else
        {
            foreach(GameObject g in attachPoints)
            {
                g.GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }

    public void SetPaintMode()
    {
        paintmode = !paintmode;

    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
