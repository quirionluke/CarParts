using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PaintBrush treats the mouse like a paintbrush, however it only allows painting on objects with a non-convex mesh collider and tagged "Paintable"
public class PaintBrush : MonoBehaviour
{
    public Color paintColor = Color.green;

    public int textureRes = 64;

    private void Start()
    {

    }

    private void Update()
    {
        if(ObjectInteract.paintmode)
        {
            if (!Input.GetMouseButton(0))
                return;

            RaycastHit hit;
            if (!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                return;


            if (hit.collider.gameObject.CompareTag("Paintable"))
            {
                // for objects with one material and one mesh
                if (hit.transform.GetComponent<MeshRenderer>())
                {
                    MeshRenderer mr = hit.transform.GetComponent<MeshRenderer>();

                    MeshCollider meshCollider = mr.gameObject.GetComponent<MeshCollider>();


                    if (mr == null || mr.material == null || meshCollider == null)
                        return;

                    foreach (Material m in mr.materials)
                    {
                        if (!m.mainTexture)
                        {
                            m.mainTexture = CreateTexture(textureRes, textureRes);
                        }


                        Texture2D tex = m.mainTexture as Texture2D;
                        Vector2 pixelUV = hit.textureCoord;

                        pixelUV.x *= tex.width;
                        pixelUV.y *= tex.height;

                        tex.SetPixel((int)pixelUV.x, (int)pixelUV.y, paintColor);
                        tex.Apply();
                    }
                    
                }
                else
                {
                    foreach (MeshRenderer mr in hit.transform.GetComponentsInChildren<MeshRenderer>())
                    {

                        MeshCollider meshCollider = mr.gameObject.GetComponent<MeshCollider>();


                        if (mr == null || mr.material == null || meshCollider == null)
                            return;

                        if (hit.collider != meshCollider)
                            continue;

                        foreach (Material m in mr.materials)
                        {

                            if (!m.mainTexture)
                            {
                                m.mainTexture = CreateTexture(textureRes, textureRes);
                            }

                            Texture2D tex = m.mainTexture as Texture2D;

                            Vector2 pixelUV = hit.textureCoord;

                            pixelUV.x *= tex.width;
                            pixelUV.y *= tex.height;

                            tex.SetPixel((int)pixelUV.x, (int)pixelUV.y, paintColor);
                            tex.Apply();
                        }
                        
                    }
                }
            }
            
        }
    }

    public Texture2D CreateTexture(int width, int height)
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, Color.clear);

        // the actual size of the texture
        texture.Resize(width, height);
        texture.Apply();

        return texture;
    }



}
