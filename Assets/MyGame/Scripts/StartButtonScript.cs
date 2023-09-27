using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartButtonScript : MonoBehaviour
{
    public SegmentScript[] segmentDisplays;
    public Button[] numberButtons;
    public Button startButton;
    public float animationDelay = 0.1f;

    private bool isAnimating = false;
    private int lastPressedNumber = 0;

    void Start()
    {
        startButton.onClick.AddListener(StartAnimation);

        for (int i = 0; i < numberButtons.Length; i++)
        {
            int number = i;
            numberButtons[i].onClick.AddListener(() => OnNumberButtonClick(number));
        }
    }

    void OnNumberButtonClick(int number)
    {
        if (!isAnimating)
        {
            lastPressedNumber = number;
            segmentDisplays[0].ShowNumber(number);
        }
    }

    public void StartAnimation()
    {
        if (!isAnimating)
        {
            isAnimating = true;
            StartCoroutine(AnimateAllSegments());
        }
    }

    IEnumerator AnimateAllSegments()
    {
        foreach (SegmentScript segmentDisplay in segmentDisplays)
        {
            int numberToAnimate = lastPressedNumber;
            bool[] segmentsToActivate = segmentDisplay.GetSegmentsForNumber(numberToAnimate);

            segmentDisplay.ClearSegments();

            for (int i = 0; i < segmentsToActivate.Length; i++)
            {
                segmentDisplay.SetSegmentState(i, segmentsToActivate[i]);
                yield return new WaitForSeconds(animationDelay);
            }
        }

        yield return new WaitForSeconds(animationDelay);

        foreach (SegmentScript segmentDisplay in segmentDisplays)
        {
            segmentDisplay.ClearSegments();
        }

        isAnimating = false;
    }
}