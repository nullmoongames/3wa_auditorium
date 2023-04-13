using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ParticleController : MonoBehaviour
{
    [SerializeField]
    float _minimumVelocityMagnitude = .02f;

    private Rigidbody2D _rigidbody;
    private MeshRenderer _meshRenderer;
    private bool _isFlaggedForDestroy;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void FixedUpdate()
    {
        if(_rigidbody.velocity.magnitude < _minimumVelocityMagnitude && !_isFlaggedForDestroy)
        {
            _isFlaggedForDestroy = true;
            StartCoroutine(IE_FadeOut(1.5f));
        }
    }

    private IEnumerator IE_FadeOut(float timeToFade)
    {
        float timer = 0f;
        Material material = _meshRenderer.material;

        while(timer < timeToFade)
        {
            float percent = timer / timeToFade;
            Color matColor = material.color;
            matColor.a = Mathf.Lerp(1f, 0f, percent);
            material.color = matColor;

            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        Color finalColor = material.color;
        finalColor.a = 0f;
        material.color = finalColor;

        Destroy(gameObject);
    }
}
