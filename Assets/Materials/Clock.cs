using System;
using UnityEngine;

public class Clock : MonoBehaviour
{
    const float hoursToDegrees = -30f, minutesToDegrees = -6f, secondsToDegrees = -6f;
    [SerializeField] Transform hoursPivot, minutesPivot, secondsPivot;

    [SerializeField] string timeZoneId = "Eastern Standard Time";
    [SerializeField] Material clockFaceMaterial;

    void Update() {
        DateTime utcNow = DateTime.UtcNow;
        TimeZoneInfo targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        DateTime targetTime = TimeZoneInfo.ConvertTimeFromUtc(utcNow, targetTimeZone);

        TimeSpan time = targetTime.TimeOfDay;
        hoursPivot.localRotation = Quaternion.Euler(0f, 0f, hoursToDegrees * (float)time.TotalHours);
        minutesPivot.localRotation = Quaternion.Euler(0f, 0f, minutesToDegrees * (float)time.TotalMinutes);
        secondsPivot.localRotation = Quaternion.Euler(0f, 0f, secondsToDegrees * (float)time.TotalSeconds);

        UpdateClockFaceColor((float)time.TotalHours);
    }

    void UpdateClockFaceColor(float hours) {
        float normalizedHours = hours / 24f;
        Color color = Color.Lerp(Color.black, Color.white, Mathf.Sin(normalizedHours * Mathf.PI));

        if (clockFaceMaterial != null)
        {
            clockFaceMaterial.color = color;
        }
    }
}
