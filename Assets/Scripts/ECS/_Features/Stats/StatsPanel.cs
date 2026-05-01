using System.Collections.Generic;
using Client.Data.Equip;
using Client.ECS.CurrentGame.Player;
using UnityEngine;


namespace Client.Infrastructure.UI.Screens.Equipment
{
    public class StatsPanel : MonoBehaviour
    {
        [SerializeField] private List<StatPanel> _statPanels;


        public void Init()
        {
        }


        public void SetStats(List<StatModifier> stats)
        {
            foreach (var statPanel in _statPanels)
                statPanel.gameObject.SetActive(false);

            int counter = 0;
            foreach (var stat in stats)
            {
                if (Mathf.Abs(stat.Value) > 0.0f)
                {
                    _statPanels[counter].gameObject.SetActive(true);
                   // _statPanels[counter].SetStat(stat.Sprite, stat.Value);
                    counter++;
                
                    if (counter >= _statPanels.Count)
                        break;
                }
            }
        }

        public void Disable()
        {
            foreach (var statPanel in _statPanels)
                statPanel.gameObject.SetActive(false);
        }
    }
}