using UnityEngine;

public class TextSpawnTimeCalculator : MonoBehaviour
{
    private float startTime;
    private float stopTime;
    private bool isTiming;

    public void StartSpawnTimer()
    {
        startTime = Time.time;
        isTiming = true;
    }

    public void StopSpawnTimer()
    {
        stopTime = Time.time;
        CalculateAndLogTime();
        isTiming = false;
    }

    private void CalculateAndLogTime()
    {
        if (isTiming)
        {
            float currentTime = Time.time;
            float elapsedTime = currentTime - startTime;

            // In dit voorbeeld loggen we de tijd wanneer het inspelen van tekst begint
            // en wanneer het inspelen stopt.
            Debug.Log("Text spawning started at " + startTime.ToString("F2") + " seconds.");
            Debug.Log("Text spawning stopped at " + stopTime.ToString("F2") + " seconds.");
            Debug.Log("Total spawn time: " + elapsedTime.ToString("F2") + " seconds.");
        }
    }
}
