using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Client.Infrastructure.UI.Screens.Equipment
{
    public class BonusStatPanel : MonoBehaviour
    {
        [SerializeField] private Image statImage;
        [SerializeField] private TextMeshProUGUI statNameText;
        [SerializeField] private TextMeshProUGUI statValueText;
        [SerializeField] private TextMeshProUGUI statAdditionalValueText;

        public void SetStat(Sprite statSprite, float stat)
        {
            statImage.sprite = statSprite;
            statValueText.text = $"{(stat * 10) :0.0}";
        }
    }
}