using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RippleCollision : MonoBehaviour
{
    private const int MaxWaveCount = 8;
    private int _wavenumber;
    
    public float DistanceX, DistanceZ;
    public List<Vector2> ImpactPos;
    public List<float> Distance;

    public float MagnitudeDivider;
    public float SpeedWaveSpread;

    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        for (var i = 0; i < MaxWaveCount; i++)
        {
            _renderer.material.SetFloat("_WaveAmplitude" + (i + 1), 0);
            Distance[i] = 0;
        }
    }

    private void LateUpdate()
    {
        for (var i = 0; i < MaxWaveCount; i++)
        {
            var amplitude = _renderer.material.GetFloat("_WaveAmplitude" + (i + 1));
            if (amplitude > 0.01f)
            {
                Distance[i] += SpeedWaveSpread * Time.deltaTime;
                amplitude *= 0.97f;
                _renderer.material.SetFloat("_Distance" + (i + 1), Distance[i]);
                _renderer.material.SetFloat("_WaveAmplitude" + (i + 1), amplitude);
            }
            else
            {
                _renderer.material.SetFloat("_WaveAmplitude" + (i + 1), 0);
                Distance[i] = 0;
            }
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.rigidbody)
        {
            CreateWave(col.transform.position, col.rigidbody.velocity.magnitude * MagnitudeDivider);
        }
    }

    public void CreateWave(Vector3 position, float magnitude)
    {
        _wavenumber++;
        if (_wavenumber >= MaxWaveCount + 1)
        {
            _wavenumber = 1;
        }

        Distance[_wavenumber - 1] = 0;

        DistanceX = transform.position.x - position.x;
        DistanceZ = transform.position.z - position.z;
        ImpactPos[_wavenumber - 1] = new Vector2(position.x, position.z);

        _renderer.material.SetFloat("_xImpact" + _wavenumber, position.x);
        _renderer.material.SetFloat("_zImpact" + _wavenumber, position.z);

        _renderer.material.SetFloat("_OffsetX" + _wavenumber, DistanceX * 2f);
        _renderer.material.SetFloat("_OffsetZ" + _wavenumber, DistanceZ * 2f);

        _renderer.material.SetFloat("_WaveAmplitude" + _wavenumber, magnitude * MagnitudeDivider);
    }
}
