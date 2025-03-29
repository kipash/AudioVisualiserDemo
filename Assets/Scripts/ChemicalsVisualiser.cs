using UnityEngine;

public class ChemicalsVisualiser : Visualiser
{
    public MeshRenderer MeshRenderer;

    public float BaseSpeedMultiplier = 0.1f;
    public float ExtraSpeedDecay = 1;
    public float ExtraSpeedMax = 1;
    public float ExtraSpeedReference = 1;
    public float ExtraSpeedFactorMultiplier = 1;

    float _time;
    float _extraTime;

    public override void OnGateTriggered(int gateScore)
    {
        _extraTime = Mathf.Clamp(_extraTime + gateScore, 0, ExtraSpeedMax);

    }

    private void Update()
    {
        var dt = Time.deltaTime;
        _time += (dt * BaseSpeedMultiplier) + (dt * Mathf.Clamp01(_extraTime / ExtraSpeedReference) * ExtraSpeedFactorMultiplier);

        _extraTime = Mathf.Lerp(_extraTime, 0, dt * ExtraSpeedDecay);

        MeshRenderer.material.SetFloat("_CustomTime", _time);
    }
}