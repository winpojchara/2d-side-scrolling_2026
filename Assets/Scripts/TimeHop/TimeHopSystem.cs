using UnityEngine;
using System;

public class TimeHopSystem : MonoBehaviour
{
    private int dayCount = 1;
    private int tempDayCount = 1;
    Timephase currentPhase;
    WeekCycle currentDay;

    private float currentTime = 0f;
    private int morningTime = 600;
    private int afternoonTime = 1200;
    private int eveningTime = 1800;
    private float divide_cap = 2400f;

    private bool timeProgress = true;
    [SerializeField]private int progressSpeed = 1;

    public event Action OnNewDay;
    public event Action<Timephase> OnPhaseChange;

    public WeekCycle GetDay(int dayCount)
    {
        return WeekCycle.monday + (dayCount - 1) % 7;
    }

    public Timephase GetPhase(float currentTime)
    {
        if (currentTime >= morningTime && currentTime < afternoonTime)
        {
            return Timephase.morning;
        }
        else if (currentTime >= afternoonTime && currentTime < eveningTime)
        {
            return Timephase.afternoon;
        }
        else
        {
            return Timephase.evening;
        }
    }

    public int GetDayCount()
    {
        return dayCount;
    }

    public string GetCurrentTime()
    {
        int hours = Mathf.FloorToInt(currentTime / 100);
        int minutes = Mathf.FloorToInt(currentTime % 100 / 100 * 60);
        return string.Format("{00:00}:{01:00}", hours, minutes);
    }

    void Start()
    {
        currentTime = 600f;
        OnPhaseChange?.Invoke(currentPhase);
        OnNewDay?.Invoke();
    }

    void Update()
    {
        ProgressTime();
        if (currentPhase != GetPhase(currentTime))
        {
            currentPhase = GetPhase(currentTime);
            OnPhaseChange?.Invoke(currentPhase);
        }
        if (tempDayCount != dayCount)
        {
            tempDayCount = dayCount;
            OnNewDay?.Invoke();
        }
    }

    void ProgressTime()
    {
        float timeSpeed = GetTimeSpeed(progressSpeed);
        if (!timeProgress) return;
        currentTime += Time.deltaTime * timeSpeed;
        if (currentTime >= divide_cap)
        {
            currentTime = 0f;
            dayCount++;
        }
    }

    public void SetProgressTime(int value)
    {
        progressSpeed = value;
    }

    float GetTimeSpeed(int progressSpeed)
    {
        switch (progressSpeed)
        {
            case 0: return 0f;
            case 1:
            default:
                return 10f;
            case 2:
                return 200f;
        }
    }

    public void StartTime()
    {
        timeProgress = true;
    }

    public void StopTime()
    {
        timeProgress = false;
    }

}

public enum Timephase
{
    morning,
    afternoon,
    evening
}

public enum WeekCycle
{
    monday,
    tuesday,
    wednesday,
    thursday,
    friday,
    saturday,
    sunday
}