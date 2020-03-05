using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Modes
{
    OnlyLeft,
    OnlyRight,
    Randomized,
    Gate
}

public class CheckPointManager : MonoBehaviour
{
    public static CheckPointManager instance;

    public Transform firstCheckPoint;
    public Color spawnPointColor;
    public Material baseMat;

    public GameObject wallPrefab;
    public Modes mode;

    [Range(0.1f, 0.5f)]
    public float size;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        CreateWalls(mode);
    }

    [ContextMenu("Init CheckPoint")]
    public void Init()
    {
        for (int i = 0; i < transform.childCount - 1; i++)
        {
            transform.GetChild(i).GetComponent<CheckPoint>().nextCheckpoint = transform.GetChild(i + 1);
            transform.GetChild(i).GetComponent<MeshRenderer>().material = baseMat;
        }

        transform.GetChild(transform.childCount - 1).GetComponent<CheckPoint>().nextCheckpoint = transform.GetChild(0);

        firstCheckPoint.GetComponent<MeshRenderer>().material.color = spawnPointColor;
    }

    void CreateWalls(Modes _mode)
    {
        foreach (Transform tr in transform)
        {
            if (_mode == Modes.OnlyLeft)
            {
                InstantiateWall(size, -1, tr);
            }
            else if (_mode == Modes.OnlyRight)
            {
                InstantiateWall(size, 1, tr);
            }
            else if (_mode == Modes.Randomized)
            {
                List<int> list = new List<int> { -1, 1};
                int sign = list[Random.Range(0, 2)];

                InstantiateWall(size, sign, tr);
            }
            else if (_mode == Modes.Gate)
            {
                InstantiateWall(size, 1, tr);
                InstantiateWall(size, -1, tr);
            }
        }
    }

    private void InstantiateWall(float _size, int sign, Transform _tr)
    {
        GameObject instance = Instantiate(wallPrefab, Vector3.zero, Quaternion.identity, _tr);

        instance.transform.localPosition = new Vector3(sign * 0.4f, 0, -1f);
        instance.transform.localRotation = Quaternion.Euler(0, 0, 0);
        instance.transform.localScale = new Vector3(_size, 1, 1);
    }
}
