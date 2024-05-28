using System.Collections;
using UnityEngine;

namespace Resource
{
    public class ResourceGatheringViewer : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private RectTransform _canvasRectTransform;

        [Space(5)]
        [Header("Settings")]
        [SerializeField] private float _showDuration = 0.2f;
        [SerializeField] private float _fadeDuration = 0.4f;
        [SerializeField] private Vector3 _initialScale = new Vector3(0.01f, 0.01f, 0.01f);
        [SerializeField] private Vector3 _highlightScale = new Vector3(0.011f, 0.011f, 0.011f);
        private Coroutine currentCoroutine;

        private void Start()
        {
            _canvasGroup.alpha = 0;
            _canvasRectTransform.localScale = _initialScale;
        }

        public void ShowAddedResource()
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
            }
            currentCoroutine = StartCoroutine(ShowAndFade());
        }

        private IEnumerator ShowAndFade()
        {
            _canvasGroup.alpha = 0;
            _canvasRectTransform.localScale = _initialScale;

            // Show Element
            yield return StartCoroutine(FadeOverTime(0, 1, _fadeDuration / 2));

            // icrease element size
            yield return StartCoroutine(ScaleOverTime(_initialScale, _highlightScale, _fadeDuration / 2));

            // return normal size element
            yield return StartCoroutine(ScaleOverTime(_highlightScale, _initialScale, _fadeDuration / 2));

            yield return new WaitForSeconds(_showDuration);

            // hide element
            yield return StartCoroutine(FadeOverTime(1, 0, _fadeDuration));
        }

        private IEnumerator FadeOverTime(float startAlpha, float endAlpha, float duration)
        {
            float elapsedTime = 0;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                _canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
                yield return null;
            }
            _canvasGroup.alpha = endAlpha;
        }

        private IEnumerator ScaleOverTime(Vector3 startScale, Vector3 endScale, float duration)
        {
            float elapsedTime = 0;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                _canvasRectTransform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / duration);
                yield return null;
            }
            _canvasRectTransform.localScale = endScale;
        }
    }
}