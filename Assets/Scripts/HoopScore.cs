using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class HoopScore : MonoBehaviour
{
    public int points = 10;
    public TMP_Text scoreText;

    private static int totalScore = 0;
    private HashSet<int> scoredPlanes = new HashSet<int>();

    private void OnTriggerEnter(Collider other)
    {
        Transform plane = other.transform.root;

        if (!plane.CompareTag("PaperPlane"))
        {
            return;
        }

        int planeID = plane.gameObject.GetInstanceID();

        if (scoredPlanes.Contains(planeID))
        {
            return;
        }

        scoredPlanes.Add(planeID);
        totalScore += points;

        if (scoreText != null)
        {
            scoreText.text = "Score: " + totalScore;
        }

        Debug.Log("Hoop scored! +" + points +
                  " points. Total: " + totalScore);
    }
}