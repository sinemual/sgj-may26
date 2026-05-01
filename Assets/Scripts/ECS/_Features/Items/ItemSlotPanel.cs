using Client.Data.Equip;
using Client.Infrastructure.UI.BaseUI;
using UnityEngine;
using UnityEngine.UI;

namespace Client.Infrastructure.UI.Screens.Equipment
{
    public class ItemSlotPanel : MonoBehaviour
    {
        [SerializeField] private UIButton pickButton;
        [SerializeField] private Image itemImage;
        [SerializeField] private Image backgroundItemImage;

        public UIButton PickButton => pickButton;

        public void Init()
        {
            Disable();
        }

        public virtual void SetItem(ItemData itemData, int amount, Color rarityColor)
        {
            Enable();

            itemImage.sprite = itemData.ItemView.ItemSprite;
            backgroundItemImage.color = rarityColor;
        }

        protected virtual void Disable()
        {
            itemImage.gameObject.SetActive(false);
            backgroundItemImage.gameObject.SetActive(false);
        }

        protected virtual void Enable()
        {
            itemImage.gameObject.SetActive(true);
            backgroundItemImage.gameObject.SetActive(true);
        }
    }
}