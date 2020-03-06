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


}
