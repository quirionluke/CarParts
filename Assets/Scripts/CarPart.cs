using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// CarPart is given to anything that is a car part and can be attached to other CarParts
public class CarPart : MonoBehaviour
{

    Vector3 dist;
    Vector3 startPos;
    float posX;
    float posZ;
    float posY;

    public List<GameObject> attached;

    private void Update()
    {
        if(attached.Count != 0)
        {
            foreach(GameObject g in attached)
            {
                transform.position = g.transform.position;
            }
        }

    }

    void OnMouseDown()
    {
        if (!ObjectInteract.paintmode)
        {
            if(attached.Count == 0)
            {
                ObjectInteract.selectedObj = gameObject;

            }
            else
            {
                foreach(GameObject g in attached)
                {
                    ObjectInteract.selectedObj = g.transform.root.gameObject;
                }

            }
            startPos = transform.position;
            dist = Camera.main.WorldToScreenPoint(ObjectInteract.selectedObj.transform.position);

            posX = Input.mousePosition.x - dist.x;
            posY = Input.mousePosition.y - dist.y;
            posZ = Input.mousePosition.z - dist.z;
        }

        
    }

    void OnMouseDrag()
    {
        if (!ObjectInteract.paintmode)
        {

            float disX = Input.mousePosition.x - posX;
            float disY = Input.mousePosition.y - posY;
            float disZ = Input.mousePosition.z - posZ;
            Vector3 lastPos = Camera.main.ScreenToWorldPoint(new Vector3(disX, disY, disZ));
            ObjectInteract.selectedObj.transform.position = new Vector3(lastPos.x, startPos.y, lastPos.z);
        }
        
    }


    private void OnMouseUp()
    {
        if (!ObjectInteract.paintmode && attached.Count == 0)
        {

            ObjectInteract.selectedObj = null;

            AttachPoints[] ap;
            if (GetComponentsInChildren<AttachPoints>() != null)
            {
                ap = GetComponentsInChildren<AttachPoints>();

                foreach (AttachPoints a in ap)
                {
                    a.CalcAttachables();

                    // add the offset between the center of the object and the attachpoint so it moves the attachpoint to be on the other one

                    if (a.closestConnection != null && a.closestConnection.transform.root.GetComponent<CarPart>() && a.closestConnection.transform.root.GetComponent<CarPart>().attached.Count == 0)
                    {
                        AttachTo(a.attaching, a.closestConnection);
                        a.closestConnection = null;
                        a.attaching = null;
                        a.closestDist = float.MaxValue;

                    }
                }

            }
        }
        
    }

    // a and b are both attachpoints
    private void AttachTo(GameObject a, GameObject b)
    {
        // moves the whole middle of the original object to the attachpoint
        // what we want is to align the original object so that the two attachpoints are touching

        Quaternion q = Quaternion.FromToRotation(a.transform.forward, b.transform.forward);
        a.transform.root.rotation = q * a.transform.root.rotation;

        (a.transform.root.position) += (b.transform.position - a.transform.position);

        attached.Add(b);
    }
}
