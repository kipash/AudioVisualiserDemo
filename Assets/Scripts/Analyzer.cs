using System.Collections.Generic;
using UnityEngine;

public class Analyzer : MonoBehaviour
{
    public int Channel;
    public Color Color;

    public FFTWindow FourierWindow = FFTWindow.Hanning;
    public AudioListener AudioListener;

    public List<Visualiser> Visualisers;

    [Header("Options")]
    [SerializeField] float _incrementSmoothingFactor = 20;
    [SerializeField] float _decrementSmoothingFactor = 20;
    [SerializeField, Range(32, 4096)] int _binCount = 512;


    private float[] _inputBuffer;
    private float[] _smoothingBuffer;

    private void Awake()
    {
        _inputBuffer = new float[_binCount];
        _smoothingBuffer = new float[_binCount];
    }

    private void OnValidate()
    {
        _binCount = Mathf.ClosestPowerOfTwo(_binCount);
    }

    protected virtual void Update()
    {
        AudioListener.GetSpectrumData(_inputBuffer, Channel, FourierWindow);

        for (int i = 0; i < _inputBuffer.Length; i++)
        {
            if (!float.IsNormal(_inputBuffer[i]))
                _inputBuffer[i] = 0;
        }

        var output = HandleBuffer(_inputBuffer);

        foreach (var visualiser in Visualisers)
            visualiser.CalculateGate(output.outputBuffer, output.max);
    }

    protected virtual (float[] outputBuffer, float min, float max) HandleBuffer(float[] inputBuffer)
    {
        float min = float.MaxValue;
        float max = float.MinValue;
        for (int i = 0; i < inputBuffer.Length; i++)
        {
            float smoothingFactor = inputBuffer[i] > _smoothingBuffer[i] ? _incrementSmoothingFactor : _decrementSmoothingFactor;

            _smoothingBuffer[i] = Mathf.Lerp(_smoothingBuffer[i], inputBuffer[i], smoothingFactor * Time.deltaTime);
            if (_smoothingBuffer[i] < min)
                min = _smoothingBuffer[i];
            if (_smoothingBuffer[i] > max)
                max = _smoothingBuffer[i];
        }

        for (int i = Mathf.RoundToInt(_binCount * 0.02f); i >= 0; i--)
        {
            _smoothingBuffer[i] = _smoothingBuffer[i + 1];
        }

        return (_smoothingBuffer, min, max);
    }
}
