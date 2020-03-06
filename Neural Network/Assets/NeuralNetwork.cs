using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NeuralNetwork
{
    public int[] layers;
    public float[][] neurons;
    public float[][][] axons;

    int x;
    int y;
    int z;

    public NeuralNetwork()
    {
        // For Serialization Debug
    }

    public NeuralNetwork(int[] _layers)
    {
        layers = new int[_layers.Length];

        for (x = 0; x < _layers.Length; x++)
        {
            layers[x] = _layers[x];
        }

        InitNeurons();

        InitAxons();
    }

    void InitNeurons()
    {
        List<float[]> neuronsList = new List<float[]>();

        for (x = 0; x < layers.Length; x++)
        {
            neuronsList.Add(new float[layers[x]]);
        }

        neurons = neuronsList.ToArray();
    }

    void InitAxons()
    {
        List<float[][]> axonsList = new List<float[][]>();

        for (x = 1; x < layers.Length; x++)
        {
            List<float[]> axonsPerLayerList = new List<float[]>();

            int neuronsInPreviousLayer = layers[x - 1];

            for (y = 0; y < layers[x]; y++)
            {
                float[] axonsPerNeuronArray = new float[neuronsInPreviousLayer];

                for (z = 0; z < neuronsInPreviousLayer; z++)
                {
                    axonsPerNeuronArray[z] = UnityEngine.Random.Range(-1f, 1f);
                }

                axonsPerLayerList.Add(axonsPerNeuronArray);
            }

            axonsList.Add(axonsPerLayerList.ToArray());
        }

        axons = axonsList.ToArray();
    }

    public void CopyNet(NeuralNetwork net)
    {
        for (x = 0; x < net.axons.Length; x++)
        {
            for (y = 0; y < net.axons[x].Length; y++)
            {
                for (z = 0; z < net.axons[x][y].Length; z++)
                {
                    axons[x][y][z] = net.axons[x][y][z];
                }
            }
        }
    }

    float value;

    public void FeedForward(float[] inputs)
    {
        neurons[0] = inputs; // Premier Layer de Neurones, les Inputs

        for (x = 1; x < layers.Length; x++)
        {
            for (y = 0; y < layers[x]; y++)
            {
                value = 0;

                for (z = 0; z < layers[x - 1]; z++)
                {
                    value += neurons[x - 1][z] * axons[x - 1][y][z];
                }

                neurons[x][y] = (float)Math.Tanh(value);
            }
        }
    }

    float randomValue;

    public void Mutate(float mutationProb)
    {
        for (x = 0; x < axons.Length; x++)
        {
            for (y = 0; y < axons[x].Length; y++)
            {
                for (z = 0; z < axons[x][y].Length; z++)
                {
                    value = axons[x][y][z];

                    randomValue = UnityEngine.Random.Range(0f, 100f);

                    if (randomValue < 0.06f * mutationProb) value = UnityEngine.Random.Range(-1f, 1f);
                    else if (randomValue < 0.07f * mutationProb) value *= -1f;
                    else if (randomValue < 0.5f * mutationProb) value += 0.1f * UnityEngine.Random.Range(-1f, 1f);
                    else if (randomValue < 0.75f * mutationProb) value *= UnityEngine.Random.Range(0, 1f) + 1f;
                    else if (randomValue < 1f * mutationProb) value *= UnityEngine.Random.Range(0, 1f);

                    axons[x][y][z] = value;
                }
            }
        }
    }
}
