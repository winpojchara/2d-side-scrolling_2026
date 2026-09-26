using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeHopUIDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI dateText;
    [SerializeField] private Image phaseImage;
    [Space]
    [SerializeField] private Sprite morningSprite;
    [SerializeField] private Sprite afternoonSprite;
    [SerializeField] private Sprite eveningSprite;

    [SerializeField] private TimeHopSystem timeHopSystem;

    void Start()
    {
        timeHopSystem.OnNewDay += () => { UpdateDateText(); UpdateDayText(); };
        timeHopSystem.OnPhaseChange += (phase) => UpdatePhaseImage(phase);
    }

    void Update()
    {
        UpdateTimeText();
    }


    private void UpdatePhaseImage(Timephase phase)
    {
        switch (phase)
        {
            case Timephase.morning:
                phaseImage.sprite = morningSprite;
                break;
            case Timephase.afternoon:
                phaseImage.sprite = afternoonSprite;
                break;
            case Timephase.evening:
                phaseImage.sprite = eveningSprite;
                break;
        }
    }

    private void UpdateTimeText()
    {
        timeText.text = timeHopSystem.GetCurrentTime();
    }


    private void UpdateDateText()
    {
        dateText.text = timeHopSystem.GetDayCount().ToString();
    }

    private void UpdateDayText()
    {
        int dayCount = timeHopSystem.GetDayCount();
        dayText.text = GetDayName(timeHopSystem.GetDay(dayCount));
    }

    string GetDayName(WeekCycle day)
    {
        switch (day)
        {
            case WeekCycle.monday:
                return "Mon";
            case WeekCycle.tuesday:
                return "Tue";
            case WeekCycle.wednesday:
                return "Wed";
            case WeekCycle.thursday:
                return "Thu";
            case WeekCycle.friday:
                return "Fri";
            case WeekCycle.saturday:
                return "Sat";
            case WeekCycle.sunday:
                return "Sun";
            default:
                return "";
        }
    }
}
