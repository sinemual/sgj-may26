using System;
using System.Collections.Generic;
using UnityEngine;

namespace Client.ECS.CurrentGame.Player
{
    [Serializable]
    public class Stat
    {
        [SerializeField] private float BaseValue;
        private float Value;
        private float ValueAfterUpgrades;
        private float MaxValue;

        private readonly List<StatModifier> _statModifiers = new List<StatModifier>();

        public Stat(float baseValue)
        {
            BaseValue = baseValue;
            Value = baseValue;
            ValueAfterUpgrades = baseValue;
            MaxValue = baseValue;
            CalculateFinalValue();
        }

        public void AddModifier(StatModifier mod)
        {
            _statModifiers.Add(mod);
            _statModifiers.Sort(CompareModifiersOrder);
            Value = CalculateFinalValue();
            if (Value > MaxValue)
                MaxValue = Value;
        }

        public void SetAllValues(float newValue)
        {
            BaseValue = newValue;
            Value = newValue;
            MaxValue = newValue;
        }

        public float GetInPercent() => Value / MaxValue;
        public float GetPercent(float percent) => MaxValue * percent;
        public void ResetToBase() => Value = MaxValue;
        public void SaveThisUpgradeModifier() => ValueAfterUpgrades = Value;

        public void RemoveModifier(StatModifier mod)
        {
            _statModifiers.Remove(mod);
            Value = CalculateFinalValue();
        }

        public void RemoveAllModifiersFromSource(IStatModifierSource source)
        {
            for (int i = _statModifiers.Count - 1; i >= 0; i--)
            {
                if (_statModifiers[i].Source == source)
                {
                    _statModifiers.RemoveAt(i);
                    CalculateFinalValue();
                    return;
                }
            }
        }

        public void RemoveAllModifiers()
        {
            for (int i = _statModifiers.Count - 1; i >= 0; i--)
            {
                _statModifiers.RemoveAt(i);
                CalculateFinalValue();
                return;
            }
        }

        private float CalculateFinalValue()
        {
            float finalValue = BaseValue;
            float percentSum = 0;

            foreach (StatModifier modifier in _statModifiers)
            {
                switch (modifier.Type)
                {
                    case StatModifierType.Flat:
                        finalValue += modifier.Value;
                        break;
                    case StatModifierType.PercentMultiplication:
                        finalValue *= modifier.Value;
                        break;
                    case StatModifierType.PercentAddition:
                        percentSum += modifier.Value;
                        break;
                    case StatModifierType.JustValues:
                        finalValue = modifier.Value;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            finalValue *= 1 + percentSum;
            return finalValue;
        }

        public float GetValue() => Value;
        public float GetBaseValue() => BaseValue;
        public void AddValue(float value) => Value += value;

        private int CompareModifiersOrder(StatModifier a, StatModifier b)
        {
            if (a.Order < b.Order)
                return -1;
            if (a.Order > b.Order)
                return 1;
            return 0;
        }
    }
}