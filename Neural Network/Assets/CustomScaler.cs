using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomScaler : MonoBehaviour
{
    public static double IntScale(int valueToScale, int valueMin, int valueMax, int minScaleTo, int maxScaleTo)
    {
        double scaledValue = minScaleTo + (double)(valueToScale - valueMin) / (valueMax - valueMin) * (maxScaleTo - minScaleTo);
        return scaledValue;
    }
    public static float FloatScale(float valueToScale, float valueMin, float valueMax, float minScaleTo, float maxScaleTo)
    {
        float scaledValue = minScaleTo + (valueToScale - valueMin) / (valueMax - valueMin) * (maxScaleTo - minScaleTo);
        return scaledValue;
    }
}
