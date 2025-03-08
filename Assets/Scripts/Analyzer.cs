using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Analyzer : MonoBehaviour
{
    public const int SPECTRUM_BUFFER_SIZE = 1024;

    public int Channel;
    public Color Color;

    public FFTWindow FourierWindow = FFTWindow.Hanning;
    public AudioListener AudioListener;

    public List<Visualiser> Visualisers;

    private void Update()
    {
        float[] buffer = new float[SPECTRUM_BUFFER_SIZE];

        AudioListener.GetSpectrumData(buffer, Channel, FourierWindow);

        float wholeSpectrumMax = FindMaxInBuffer(buffer);

        foreach (var visualiser in Visualisers)
            visualiser.CalculateGate(buffer, wholeSpectrumMax);
    }

    private float FindMaxInBuffer(float[] buffer)
    {
        float max = 0;
        for (int i = 0; i < buffer.Length; i++)
        {
            if (buffer[i] > max)
                max = buffer[i];
        }
        return max;
    }
}
