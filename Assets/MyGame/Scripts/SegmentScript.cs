using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SegmentScript : MonoBehaviour
{
    public Image[] segments;
    public float segmentDelay = 0.2f;
    private bool isAnimating = false;
    private int currentNumber = 0;

    void Start()
    {
        ClearSegments();
    }

    public void ShowNumber(int number)
    {
        if (!isAnimating)
        {
            currentNumber = number;
            bool[] numberSegments = GetSegmentsForNumber(number);

            for (int i = 0; i < segments.Length; i++)
            {
                if (i < numberSegments.Length)
                {
                    SetSegmentState(i, numberSegments[i]);
                }
            }
        }
    }

    public void ShowAnimatedNumber(int number)
    {
        if (!isAnimating)
        {
            StartCoroutine(AnimateSegmentsForNumber(number));
        }
    }

    public int GetCurrentNumber()
    {
        return currentNumber; 
    }

    private IEnumerator AnimateSegmentsForNumber(int number)
    {
        isAnimating = true;
        ClearSegments();

        bool[] segmentsToActivate = GetSegmentsForNumber(number);

        for (int i = 0; i < segmentsToActivate.Length; i++)
        {
            SetSegmentState(i, segmentsToActivate[i]);
            yield return new WaitForSeconds(segmentDelay);
        }

        isAnimating = false;
    }

    public void ClearSegments()
    {
        foreach (Image segment in segments)
        {
            segment.enabled = false;
        }
    }

    public bool[] GetSegmentsForNumber(int number)
    {
        bool[] numberSegments = new bool[7];

        switch (number)
        {
            case 0:
                numberSegments = new bool[] { true, true, true, false, true, true, true };
                break;
            case 1:
                numberSegments = new bool[] { false, false, true, false, false, true, false };
                break;
            case 2:
                numberSegments = new bool[] { false, true, true, true, true, false, true };
                break;
            case 3:
                numberSegments = new bool[] { false, true, true, true, false, true, true };
                break;
            case 4:
                numberSegments = new bool[] { true, false, true, true, false, true, false };
                break;
            case 5:
                numberSegments = new bool[] { true, true, false, true, false, true, true };
                break;
            case 6:
                numberSegments = new bool[] { true, true, false, true, true, true, true };
                break;
            case 7:
                numberSegments = new bool[] { false, true, true, false, false, true, false };
                break;
            case 8:
                numberSegments = new bool[] { true, true, true, true, true, true, true };
                break;
            case 9:
                numberSegments = new bool[] { true, true, true, true, false, true, true };
                break;
        }

        return numberSegments;
    }

    public void SetSegmentState(int segmentIndex, bool state)
    {
        if (segmentIndex >= 0 && segmentIndex < segments.Length)
        {
            segments[segmentIndex].enabled = state;
        }
    }
}