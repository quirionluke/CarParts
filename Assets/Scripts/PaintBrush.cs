using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PaintBrush treats the mouse like a paintbrush, however it only allows painting on objects with a mesh collider and tagged "Paintable"
public class PaintBrush : MonoBehaviour
{
    public Color paintColor = Color.green;

    Texture2D texture;

    private void Start()
    {
        texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, Color.clear);

        // the actual size of the texture
        texture.Resize(64, 64);
        texture.Apply();
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

                    if (mr.materials.Length != 0)
                    {
                        foreach (Material m in mr.materials)
                        {
                            if (!m.mainTexture)
                            {
                                m.mainTexture = texture;
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
                else
                {
                    foreach (MeshRenderer mr in hit.transform.GetComponentsInChildren<MeshRenderer>())
                    {
                        mr.material.mainTexture = texture;

                        MeshCollider meshCollider = mr.gameObject.GetComponent<MeshCollider>();


                        if (mr == null || mr.material == null || mr.material.mainTexture == null || meshCollider == null)
                            return;

                        if (mr.materials.Length != 0)
                        {
                            foreach (Material m in mr.materials)
                            {
                                if (!m.mainTexture)
                                {
                                    m.mainTexture = texture;
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
    }



}
