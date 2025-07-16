using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class DissolveController : MonoBehaviour
{
    private Renderer _meshRenderer;
    private bool _isVisible = true;
    private bool _isFading;

    private void Awake()
    {
        _meshRenderer = GetComponentInChildren<Renderer>();
        Debug.Log(_meshRenderer);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (!_isFading)
            {
                _isFading = true;
                StartCoroutine(Fade(1f, !_isVisible));
            }
        }

    }

    private IEnumerator Fade(float duration, bool fadeIn)
    {
        float startValue = fadeIn ? 0f : 1f;

        _meshRenderer.material.SetFloat("_Cutoff", startValue);

        float targetValue = 1f - startValue;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            float value = Mathf.MoveTowards(startValue, targetValue, t);
            elapsedTime += Time.deltaTime;

            if (elapsedTime >= duration / 2)
            {
                _meshRenderer.shadowCastingMode = fadeIn ? ShadowCastingMode.On : ShadowCastingMode.Off;
            }

            _meshRenderer.material.SetFloat("_Cutoff", value);

            yield return null;
        }

        _meshRenderer.material.SetFloat("_Cutoff", targetValue);

        _isVisible = fadeIn;
        _isFading = false;
    }
}
