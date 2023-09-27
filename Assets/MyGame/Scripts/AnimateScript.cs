using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AnimateScript : MonoBehaviour
{
    public SegmentScript segmentDisplay;
    public Button[] numberButtons;
    public Button animateButton;
    public float segmentDelay = 0.5f;

    private bool isAnimating = false;

    void Start()
    {
        for (int i = 0; i < numberButtons.Length; i++)
        {
            int number = i;
            numberButtons[i].onClick.AddListener(() => OnNumberButtonClick(number));
        }

        animateButton.onClick.AddListener(StartAnimation);
    }

    void OnNumberButtonClick(int number)
    {
        if (!isAnimating)
        {
            int currentNumber = segmentDisplay.GetCurrentNumber();
            segmentDisplay.ShowNumber(number);
        }
    }

    public void StartAnimation()
    {
        if (!isAnimating)
        {
            isAnimating = true;
            int numberToAnimate = segmentDisplay.GetCurrentNumber();
            StartCoroutine(AnimateSegments(numberToAnimate));
        }
    }

    IEnumerator AnimateSegments(int number)
    {
        bool[] segmentsToActivate = segmentDisplay.GetSegmentsForNumber(number);

        for (int i = 0; i < segmentsToActivate.Length; i++)
        {
            segmentDisplay.SetSegmentState(i, segmentsToActivate[i]);
            yield return new WaitForSeconds(segmentDelay);
        }

        isAnimating = false;
    }
}
