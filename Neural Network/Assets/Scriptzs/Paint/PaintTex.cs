using System;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class PaintTex : MonoBehaviour
{
    Texture2D texture;
    public Agent[] car;

    [HideInInspector] public Vector2Int[] carPos;

    public int TexResolution;
    public float dotRadius;
    Color newColor;

    MeshRenderer mat;
    Vector2Int TexCarPos;

    public Transform P1;
    public Transform P2;

    [Space]
    public float fadingSpeed = 0.01f;
    public float alphaIncrease = 0.01f;
    public bool isFading;
    public bool isReset;
    public bool isAdding;

    public int folderIndex = 0;

    [Space]
    public Color color;

    // Start is called before the first frame update
    void Awake()
    {
        mat = GetComponent<MeshRenderer>();

        texture = new Texture2D(TexResolution, TexResolution);
        mat.material.SetTexture("_MainTex", texture);

        StartTex();
    }

    // Update is called once per frame
    void Update()
    {
        posUpdate();
        Texturing();
    }

    public void StartTex()
    {
        for (int x = 0; x < TexResolution; x++)
        {
            for (int y = 0; y < TexResolution; y++)
            {
                texture.SetPixel(x, y, Color.clear);

            }
        }
        texture.Apply();
    }

    void Texturing()
    {
        if (isFading == true)
        {
            for (int x = 0; x < TexResolution; x++)
            {
                for (int y = 0; y < TexResolution; y++)
                {
                    newColor = texture.GetPixel(x, y);
                    newColor.a -= fadingSpeed;
                    texture.SetPixel(x, y, newColor);

                }
            }
        }

        if (isAdding == false)
        {
            for (int i = 0; i < car.Length; i++)
            {
                if (car[i].outVelocity / 150 >= 0.1)
                {

                    texture.SetPixel(Mathf.Clamp(carPos[i].x, 0, TexResolution),
                                        Mathf.Clamp(carPos[i].y, 0, TexResolution),
                                        new Color(2 - (car[i].outVelocity / 150 * 2 + 1f), (car[i].outVelocity / 150) * 4, 0));
                }
                else
                {
                    for (int y = 0; y < 4; y++)
                    {
                        texture.SetPixel(Mathf.Clamp(carPos[i].x + y, 0, TexResolution),
                                            Mathf.Clamp(carPos[i].y + y, 0, TexResolution),
                                            new Color(2 - (car[i].outVelocity / 150 * 2 + 1f), (car[i].outVelocity / 150) * 4, 0));
                    }
                }


            }
        }
        else
        {
            for (int i = 0; i < car.Length; i++)
            {
                    texture.SetPixel(Mathf.Clamp(carPos[i].x, 0, TexResolution),
                                        Mathf.Clamp(carPos[i].y, 0, TexResolution),
                                        new Color(1, 0, 0, texture.GetPixel(carPos[i].x, carPos[i].y).a + alphaIncrease));
                
            }
        }
        texture.Apply();

        //mat.material.SetTexture("_MainTex", texture);

    }

    void posUpdate()
    {
        for (int i = 0; i <= car.Length - 1; i++)
        {
            carPos[i] = remap2(car[i].transform.position);
        }

    }

    public void CreatePng()
    {
        byte[] bytes = texture.EncodeToPNG();
        string name = "/../Assets/PNG_Tex/SavedScreen" + folderIndex.ToString() + ".png";

        // For testing purposes, also write to a file in the project folder
        File.WriteAllBytes(Application.dataPath + name, bytes);
    }

    Vector2Int remap2(Vector3 vPos)
    {
        return new Vector2Int(Mathf.RoundToInt((vPos.x - P1.position.x) / (P2.position.x - P1.position.x) * TexResolution),
                               Mathf.RoundToInt((vPos.z - P1.position.z) / (P2.position.z - P1.position.z) * TexResolution));


    }

}
