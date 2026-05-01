using Client.DevTools.MyTools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Client.Infrastructure.UI.Screens.Equipment
{
    public class ResourcePanel : MonoBehaviour
    {
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Image resourceImage;
        [SerializeField] private TextMeshProUGUI resourceValueText;

        public void SetResource(Sprite resourceSprite, double value)
        {
            Enable();
            resourceImage.sprite = resourceSprite;
            resourceValueText.text = $"{Utility.Format((value * 10))}%";
        }

        public void SetResourceWithColor(Sprite resourceSprite, double value, Color color, Sprite sprite)
        {
            Enable();
            resourceImage.sprite = resourceSprite;
            //backgroundImage.color = color;
            backgroundImage.sprite = sprite;
            resourceValueText.text = $"x{Utility.Format((value))}";
        }

        public void Disable()
        {
            resourceImage.gameObject.SetActive(false);
            backgroundImage.gameObject.SetActive(false);
            resourceValueText.gameObject.SetActive(false);
        }

        private void Enable()
        {
            resourceImage.gameObject.SetActive(true);
            backgroundImage.gameObject.SetActive(true);
            resourceValueText.gameObject.SetActive(true);
        }

        public void SetResourceWithNeeded(Sprite resourceSprite, double value, double neededValue)
        {
            Enable();
            resourceImage.sprite = resourceSprite;
            if (value >= neededValue)
                resourceValueText.text = $"{Utility.Format(value)}/{Utility.Format(neededValue)}";
            else
                resourceValueText.text = $"<color=#ef3143>{Utility.Format(value)}</color>/{Utility.Format(neededValue)}";

        }
    }
}