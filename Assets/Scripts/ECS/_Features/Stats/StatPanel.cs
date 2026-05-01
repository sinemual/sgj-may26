using Client.DevTools.MyTools;
using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Client.Infrastructure.UI.Screens.Equipment
{
    public class StatPanel : MonoBehaviour
    {
        [SerializeField] private Image statImage;
        [SerializeField] private TextMeshProUGUI statNameText;
        [SerializeField] private TextMeshProUGUI statValueText;

        public void SetStat(StatData data, float value)
        {
            statImage.sprite = data.StatSprite;
            statNameText.text = $"{data.StatShortName}:";
            statValueText.text = $"{Utility.Format(value)}";
        }

        public void SetStatInPercent(StatData data, float value, float additionalValue = 0.0f)
        {
            statImage.sprite = data.StatSprite;
            statNameText.text = $"{data.StatShortName}";
            if (additionalValue > 0.0f)
                statValueText.text = $"+{(value * 10):0.0}% <color=#13fcc4>(+{(additionalValue * 10):0.0}%)</color>";
            else
                statValueText.text = $"+{(value * 10):0.0}%";
        }
    }
}