using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeuralNetworkViewer : MonoBehaviour
{
    public static NeuralNetworkViewer instance;

    void Awake()
    {
        instance = this;
    }

    public Gradient colorGradient;

    const float decalX = 100;
    const float decalY = 20;

    public Transform viewerGroup;

    public RectTransform neuronPrefab;
    public RectTransform axonPrefab;

    public RectTransform fitnessPrefab;
    RectTransform fitness;

    public Agent agent;

    Image[][] neurons;
    Text[][] neuronsValue;

    Image[][][] axons;

    RectTransform neuron;
    RectTransform axon;
    Text fitnessText;

    float yAdd;
    float posY;

    float zAdd;
    float posZ;

    public void Init(Agent _agent)
    {
        agent = _agent;
        CreateViewer(agent.net);
    }

    void CreateViewer(NeuralNetwork net)
    {
        for (int u = viewerGroup.childCount -1; u > -1; u--)
        {
            DestroyImmediate(viewerGroup.GetChild(u).gameObject);
        }

        neurons = new Image[net.neurons.Length][];
        neuronsValue = new Text[net.neurons.Length][];

        for (int x = 0; x < net.neurons.Length; x++)
        {
            neurons[x] = new Image[net.neurons[x].Length];
            neuronsValue[x] = new Text[net.neurons[x].Length];

            for (int y = 0; y < net.neurons[x].Length; y++)
            {
                if (net.neurons[x].Length % 2 == 0) yAdd = 1f;
                else yAdd = 0;

                if (y % 2 == 0) posY = y + yAdd;
                else posY = -y - 1 + yAdd;

                neuron = Instantiate(neuronPrefab, Vector3.zero, Quaternion.identity, viewerGroup);

                neuron.anchoredPosition = new Vector2(x * decalX, posY * decalY);
                neurons[x][y] = neuron.GetComponent<Image>();
                neuronsValue[x][y] = neuron.transform.GetChild(0).GetComponent<Text>();
            }
        }

        axons = new Image[net.axons.Length][][];

        for (int x = 0; x < net.axons.Length; x++)
        {
            axons[x] = new Image[net.axons[x].Length][];

            for (int y = 0; y < net.axons[x].Length; y++)
            {
                axons[x][y] = new Image[net.axons[x][y].Length];

                for (int z = 0; z < net.axons[x][y].Length; z++)
                {
                    if (net.axons[x].Length % 2 == 0) yAdd = 1f;
                    else yAdd = 0;

                    if (y % 2 == 0) posY = y + yAdd;
                    else posY = -y - 1 + yAdd;

                    if (net.axons[x][y].Length % 2 == 0) zAdd = 1f;
                    else zAdd = 0;

                    if (z % 2 == 0) posZ = z + zAdd;
                    else posZ = -z - 1 + zAdd;

                    float midPosX = decalX * (x + 0.5f);
                    float midPosY = (posY + posZ) * 0.5f * decalY;

                    float zAngle = Mathf.Atan2((posY - posZ) * decalY, decalX) * Mathf.Rad2Deg;

                    axon = Instantiate(axonPrefab, Vector3.zero, Quaternion.identity, viewerGroup);

                    axon.anchoredPosition = new Vector2(midPosX, midPosY);
                    axon.eulerAngles = new Vector3(0, 0, zAngle);

                    axon.sizeDelta = new Vector2(new Vector2(decalX, (posY - posZ) * decalY).magnitude - 35, 2);

                    axons[x][y][z] = axon.GetComponent<Image>();
                }
            }
        }

        fitness = Instantiate(fitnessPrefab, Vector3.zero, Quaternion.identity, viewerGroup);
        fitness.anchoredPosition = new Vector2(decalX * net.neurons.Length * 0.5f * 300, 0);

        fitnessText = fitness.GetComponent<Text>();
    }

    int x;
    int y;

    void Update()
    {
        for (x = 0; x < agent.net.neurons.Length; x++)
        {
            for (y = 0; y < agent.net.neurons[x].Length; y++)
            {
                neurons[x][y].color = colorGradient.Evaluate((agent.net.neurons[x][y] + 1) * 0.5f);
                neuronsValue[x][y].text = agent.net.neurons[x][y].ToString("F2");
            }
        }

        fitnessText.text = agent.fitness.ToString("F1");
    }

    public void RefreshAxon()
    {
        for (int x = 0; x < agent.net.axons.Length; x++)
        {
            for (int y = 0; y < agent.net.axons[x].Length; y++)
            {
                for (int z = 0; z < agent.net.axons[x][y].Length; z++)
                {
                    axons[x][y][z].color = colorGradient.Evaluate((agent.net.axons[x][y][z] + 1) * 0.5f);
                }
            }
        }
    }
}
