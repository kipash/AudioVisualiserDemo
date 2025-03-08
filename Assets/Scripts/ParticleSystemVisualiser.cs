using UnityEngine;

public class ParticleSystemVisualiser : Visualiser
{
    [Range(0, 200)] public int ScoreMultiplier;

    public ParticleSystem ParticleSystem;
    public override void OnGateTriggered(int gateScore)
    {
        ParticleSystem.Emit(gateScore * ScoreMultiplier);
    }
}
