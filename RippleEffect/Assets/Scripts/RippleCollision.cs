using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RippleCollision : MonoBehaviour
{
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
        for (int i = 0; i < 8; i++)
        {
            _renderer.material.SetFloat("_WaveAmplitude" + (i + 1), 0);
            Distance[i] = 0;
        }

    }

    private void Update()
    {
        for (int i = 0; i < 8; i++)
        {
            var amplitude = _renderer.material.GetFloat("_WaveAmplitude" + (i+1));
            if (amplitude > 0.01f)
            {
                Distance[i] += SpeedWaveSpread*Time.deltaTime;
                amplitude *= 0.985f;
                _renderer.material.SetFloat("_Distance" + (i+1), Distance[i]);
                _renderer.material.SetFloat("_WaveAmplitude" + (i+1), amplitude);
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
            _wavenumber++;
            if (_wavenumber >= 9)
            {
                _wavenumber = 1;
            }

            Distance[_wavenumber - 1] = 0;

            DistanceX = transform.position.x - col.transform.position.x;
            DistanceZ = transform.position.z - col.transform.position.z;
            ImpactPos[_wavenumber - 1] = new Vector2(col.transform.position.x, col.transform.position.z);

            _renderer.material.SetFloat("_xImpact" + _wavenumber, col.transform.position.x);
            _renderer.material.SetFloat("_zImpact" + _wavenumber, col.transform.position.z);

            _renderer.material.SetFloat("_OffsetX" + _wavenumber,DistanceX * 2f);
            _renderer.material.SetFloat("_OffsetZ" + _wavenumber,DistanceZ * 2f);

            _renderer.material.SetFloat("_WaveAmplitude" + _wavenumber, col.rigidbody.velocity.magnitude * MagnitudeDivider);
        }
    }
}
