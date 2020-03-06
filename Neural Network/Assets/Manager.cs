using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public int populationSize = 100;
    public float trainingDuration = 10;
    public float mutationRate = 1;

    public Agent agentPrefab;
    public Transform agentGroupParent;
    PaintTex paintTex;

    Agent agent;
    public List<Agent> agents = new List<Agent>();

    void Start()
    {
        paintTex = FindObjectOfType<PaintTex>();
        StartCoroutine(InitCoroutine());
    }

    IEnumerator InitCoroutine()
    {
        NewGeneration();
        //InitNeuralNetworkViewer();
        Focus();
        yield return new WaitForSeconds(trainingDuration);
        StartCoroutine(LoopCoroutine());
    }

    IEnumerator LoopCoroutine()
    {
        NewGeneration();
        Focus();
        yield return new WaitForSeconds(trainingDuration);
        StartCoroutine(LoopCoroutine());
    }

    void NewGeneration()
    {
        agents.Sort();
        AddOrRemoveAgent();
        Mutate();
        ResetAgent();
        SetColor();
    }

    private void AddOrRemoveAgent()
    {
        if (agents.Count != populationSize)
        {
            int dif = populationSize - agents.Count;

            if (agents.Count <= populationSize)
            {
                for (int u = 0; u < dif; u++)
                {
                    AddAgent();
                }
            }
            else
            {
                for (int w = 0; w < dif; w++)
                {
                    RemoveAgent();
                }
            }
        }
    }
    
    void AddAgent()
    {
        agent = Instantiate(agentPrefab, Vector3.zero, Quaternion.identity, agentGroupParent);
        agent.net = new NeuralNetwork(agent.net.layers);
        agents.Add(agent);
    }

    void RemoveAgent()
    {
        Destroy(agents[agents.Count - 1].gameObject);
        agents.RemoveAt(agents.Count - 1);
    }

    private void Mutate()
    {
        for (int g = agents.Count / 2; g < agents.Count; g++) // Prend les 50 moins bons (derniers de la liste)
        {
            agents[g].net.CopyNet(agents[g - (agents.Count/2)].net); // Les remplace par les 50 meilleurs (premiers de la liste)
            agents[g].net.Mutate(mutationRate);
            agents[g].SetMutatedColor();
        }
    }

    private void ResetAgent()
    {
        paintTex.car = new Agent[agents.Count];
        paintTex.carPos = new Vector2Int[agents.Count];
        paintTex.folderIndex += 1;
        paintTex.CreatePng();

        if (paintTex.isReset == true)
        {
            paintTex.StartTex();
        }

        for (int k = 0; k < agents.Count; k++)
        {
            agents[k].ResetAgent();

            paintTex.car[k] = agents[k];
        }


    }

    private void SetColor()
    {
        for (int s = 1; s < agents.Count/2; s++)
        {
            agents[s].SetDefaultColor();
        }

        agents[0].SetFirstColor();
    }
    private void Focus()
    {
        //NeuralNetworkViewer.instance.agent = agents[0];
        //NeuralNetworkViewer.instance.RefreshAxon();
        CameraController.instance.target = agents[0].tr;
    }

    public void Save()
    {
        List<NeuralNetwork> nets = new List<NeuralNetwork>();

        for (int v = 0; v < agents.Count; v++)
        {
            nets.Add(agents[v].net);
        }

        DataManager.instance.Save(nets);
    }

    public void Load()
    {
        Data data = DataManager.instance.Load();

        if(data != null)
        {
            for (int b = 0; b < agents.Count; b++)
            {
                agents[b].net = data.nets[b];
            }
        }

        End();
    }

    public void End()
    {
        StopAllCoroutines();
        StartCoroutine(LoopCoroutine());
    }

    public void ResetNets()
    {
        for (int p = 0; p < agents.Count; p++)
        {
            agents[p].net = new NeuralNetwork(agent.net.layers);
        }

        End();
    }
}
