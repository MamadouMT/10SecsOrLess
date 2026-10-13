
using UnityEngine;
using TMPro;

[System.Serializable]
public class HoopMovement
{
    public Transform hoop;

    public Vector3 minimumPosition;
    public Vector3 maximumPosition;
}

public class HoopTimer : MonoBehaviour
{
    public HoopMovement[] hoops;
    public TMP_Text timerText;

    public float roundTime = 10f;

    private float timeLeft;

    void Start()
    {
        timeLeft = roundTime;
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;

        if (timerText != null)
        {
            timerText.text = "Time: " + Mathf.Ceil(timeLeft);
        }

        if (timeLeft <= 0)
        {
            MoveHoops();
            timeLeft = roundTime;
        }
    }

    void MoveHoops()
    {
        foreach (HoopMovement hoopData in hoops)
        {
            if (hoopData.hoop != null)
            {
                hoopData.hoop.position = new Vector3(
                    Random.Range(hoopData.minimumPosition.x,
                                 hoopData.maximumPosition.x),
                    Random.Range(hoopData.minimumPosition.y,
                                 hoopData.maximumPosition.y),
                    Random.Range(hoopData.minimumPosition.z,
                                 hoopData.maximumPosition.z)
                );
            }
        }
    }
}
