using UnityEditor.ShaderGraph;
using UnityEngine;

public class SpikeVisualiser : Visualiser
{
    public MeshRenderer MeshRenderer;
    public float Smoothing = 1;

    float _controlTarget;
    float _control;
    public override void OnGateTriggered(int gateScore)
    {
        _controlTarget = Mathf.Clamp01(gateScore / GateScoreLimit.y);
    }

    private void Update()
    {
        _control = Mathf.Lerp(_control, _controlTarget, Time.deltaTime * Smoothing);
        MeshRenderer.material.SetFloat("_CONTROL", _control);
    }
}