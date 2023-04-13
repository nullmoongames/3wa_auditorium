using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEmitter : MonoBehaviour
{
    [SerializeField]
    GameObject _particlePrefab;
    [SerializeField]
    [Range(.02f, .08f)]
    float _speed = .04f;
    [SerializeField]
    [Range(.1f, .5f)]
    float _circleRadius;
    [SerializeField]
    [Range(.1f, .6f)]
    float _delayBetweenParticles = .5f;

    float _nextParticleTime;

    private void Awake()
    {
        _nextParticleTime = Time.time;
    }

    private void Update()
    {
        if(Time.time > _nextParticleTime)
        {
            GenerateParticle();

            _nextParticleTime = Time.time + _delayBetweenParticles;
        }
    }

    private void GenerateParticle()
    {
        Vector2 randomPosition = (Random.insideUnitCircle * _circleRadius) + (Vector2)transform.position;

        GameObject particleGO = Instantiate(_particlePrefab, randomPosition, Quaternion.identity);

        Rigidbody2D particleRb2d = particleGO.GetComponent<Rigidbody2D>();
        particleRb2d.AddForce(transform.right * _speed, ForceMode2D.Impulse);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _circleRadius);
    }
}
