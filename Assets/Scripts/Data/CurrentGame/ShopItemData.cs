using System;
using UnityEngine;

namespace Client.Data.Equip
{
    [CreateAssetMenu(menuName = "SharedData/BuyItemData", fileName = "BuyItemData")]
    [Serializable]
    public class ShopItemData : ItemData
    {
        public int OpenLabPoints;
    }
}