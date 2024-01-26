using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System;
using System.Collections;
using System.Collections.Generic;


    public class ItemTooltip : MonoBehaviour
    {
        public static ItemTooltip Instance;

        [SerializeField] private Text nameText;
        [SerializeField] private Text slotTypeText;
        [SerializeField] private Text statsText;

        private StringBuilder sb = new StringBuilder();

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
            gameObject.SetActive(false);
        }

        public void ShowTooltip(ItemData itemToShow)
        {
            if (!(itemToShow is EquippableItem))
            {
                return;
            }

            EquippableItem item = (EquippableItem)itemToShow;

            nameText.text = item.Name;
            slotTypeText.text = item.EquipmentType.ToString();

            sb.Length = 0;

            AddStatText(item.StrengthBonus, " Strength");
            AddStatText(item.SpeedBonus, " Speed");
            AddStatText(item.HealthBonus, " Health");

            AddStatText(item.StrengthPercentBonus * 100, "% Strength");
            AddStatText(item.SpeedPercentBonus * 100, "% Speed");
            AddStatText(item.HealthPercentBonus * 100, "% Health");
 
            statsText.text = sb.ToString();
        }

        public void HideTooltip()
        {
            gameObject.SetActive(false);
        }

        private void AddStatText(float statBonus, string statName)
        {
            if (statBonus != 0)
            {
                if (sb.Length > 0)
                {
                    sb.AppendLine();
                }

                if (statBonus > 0)
                {
                    sb.Append("+");
                    sb.Append(statBonus);
                    sb.Append(statName);

                    ApplyColorToSubstring(sb.ToString(), Color.green);
                }
                else 
                {
                    sb.Append(statBonus);
                    sb.Append(statName);

                    ApplyColorToSubstring(sb.ToString(), Color.red);
                }
            }
        }

        private void ApplyColorToSubstring(string substring, Color color)
        {
            int startIndex = sb.ToString().IndexOf(substring);
            int endIndex = startIndex + substring.Length;

            if (startIndex >= 0)
            {
                string colorTag = $"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>";
                string endColorTag = "</color>";
                sb.Insert(endIndex, endColorTag);
                sb.Insert(startIndex, colorTag);
            }
    }
}
