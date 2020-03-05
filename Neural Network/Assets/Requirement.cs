using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Requirement : MonoBehaviour , System.IComparable<Requirement>
{
    int[,] array2D;

    public int[][] myJaggedArray2D;
    public int[][][] myJaggedArray3D;

    public LayerMask layerMask;

    public float value;

    void Start()
    {
        TestJaggedArray2D();
    }
    
    void Update()
    {
        DoRayCast();
    }

    public int CompareTo(Requirement other)
    {
        if (value < other.value) return 1;
        else if (value > other.value) return -1;
        else return 0;
    }

    void TestJaggedArray2D()
    {
        myJaggedArray2D = new int[4][];

        for (int i = 0; i < myJaggedArray2D.Length; i++)
        {
            myJaggedArray2D[i] = new int[i + 1];
        }
    }

    void TestJaggedArray3D()
    {
        myJaggedArray3D = new int[4][][];

        for (int i = 0; i < myJaggedArray3D.Length; i++)
        {
            myJaggedArray3D[i] = new int[i + 1][];

            for (int x = 0; x < myJaggedArray3D.Length; x++)
            {
                myJaggedArray3D[i][x] = new int[x + 1];
            }
        }
    }

    void DoRayCast()
    {
        Vector3 pos = Vector3.zero;
        Vector3 direction = Vector3.up;
        RaycastHit hit;

        float rayRange = 1f;

        if (Physics.Raycast(pos, direction, out hit, rayRange, layerMask))
        {
            Debug.DrawRay(pos, direction * rayRange, Color.green);
        }
        else
        {
            Debug.DrawRay(pos, direction * rayRange, Color.red);
        }
    }
}
