using System;
using System.Linq;
using UnityEngine;

public abstract class Visualiser : MonoBehaviour
{
    public AnimationCurve EQFilter;
    [Range(0f, 1f)] public float FilterGate;
    public Vector2 GateScoreLimit;

    private float[] _EQAppliedBuffer;

    [SerializeField] private bool _showDebug;
    public virtual void CalculateGate(float[] buffer, float spectrumMax)
    {
        _EQAppliedBuffer = buffer.ToArray();
        for (int i = 0; i < _EQAppliedBuffer.Length; i++)
        {
            float x = (float)i / buffer.Length;
            float y = (float)buffer[i];// / spectrumMax;
            _EQAppliedBuffer[i] = Mathf.Clamp01(EQFilter.Evaluate(x)) * y;
        }

        int gateScore = CalculateGateScore(_EQAppliedBuffer, FilterGate);
        //if (GateScoreLimit.x <= gateScore && gateScore <= GateScoreLimit.y)
            OnGateTriggered(gateScore);

        // Debug
        if (_showDebug)
        {
            Debug.DrawLine(new Vector3(0, FilterGate, 0), new Vector3(1, FilterGate, 0), Color.red);

            for (int i = 0; i < _EQAppliedBuffer.Length - 1; i++)
            {
                Debug.DrawLine(new Vector3((i + 0f) / _EQAppliedBuffer.Length, _EQAppliedBuffer[i + 0], 0),
                               new Vector3((i + 1f) / _EQAppliedBuffer.Length, _EQAppliedBuffer[i + 1], 0));
            }
        }
    }

    private int CalculateGateScore(float[] buffer, float gate)
    {
        int score = 0;
        for (int i = 0; i < buffer.Length; i++)
        {
            if (buffer[i] >= gate)
                score++;
        }
        return score;
    }

    public abstract void OnGateTriggered(int gateScore);
}
