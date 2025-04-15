using UnityEngine;

public class LogarithmicAnalyzer : Analyzer
{
    protected override (float[] outputBuffer, float min, float max) HandleBuffer(float[] inputBuffer)
    {
        for (int i = 0; i < inputBuffer.Length; i++)
        {
            float input = inputBuffer[i] + 0.1f;
            inputBuffer[i] = (Mathf.Log(inputBuffer[i] + 0.1f, 10) + 1) / 1.0414f;
        }

        return base.HandleBuffer(inputBuffer);
    }
}