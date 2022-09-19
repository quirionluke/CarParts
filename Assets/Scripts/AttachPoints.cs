using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AttachPoints represents where attached CarParts will be located
public class AttachPoints : MonoBehaviour
{
    public float attachDist = 3;

    public float closestDist = float.MaxValue;
    public GameObject closestConnection;
    public GameObject attaching;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
    }


    public void CalcAttachables()
    {
        foreach(Transform child in transform)
        {
            // the static attachpoints variable holds the attachpoints who's transforms will be used
            foreach (GameObject g in ObjectInteract.attachPoints)
            {

                // check if the two attachpoints are siblings
                if(!g.transform.IsChildOf(transform))
                {
                    float dist = Vector3.Distance(g.transform.position, child.transform.position);

                    if (dist <= attachDist)
                    {
                        if (dist < closestDist)
                        {
                            closestDist = dist;
                            closestConnection = g;
                            attaching = child.gameObject;
                        }
                    }
                }

            }
        }
            
        
    }
}
