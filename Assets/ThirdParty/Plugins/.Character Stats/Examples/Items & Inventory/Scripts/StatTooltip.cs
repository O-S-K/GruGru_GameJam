using System.Text;
using UnityEngine;
using UnityEngine.UI;


    public class StatTooltip : MonoBehaviour
    {
        public static StatTooltip Instance;

        [SerializeField] Text statNameText;
        [SerializeField] Text finalValueText;
        [SerializeField] Text modifiersListText;

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

        public void ShowTooltip(CharacterStat stat, string statName)
        {
            gameObject.SetActive(true);

            statNameText.text = FirstLetterToUpper(statName);
            finalValueText.text = GetValueText(stat);
            modifiersListText.text = GetModifiersText(stat);
        }

        public void HideTooltip()
        {
            gameObject.SetActive(false);
        }

        private string GetValueText(CharacterStat stat)
        {
            sb.Length = 0;

            sb.Append(stat.Value);
            sb.Append(" (");
            sb.Append(stat.BaseValue);

            float valueEquip = (float)System.Math.Round(stat.Value - stat.BaseValue, 4);
            if (valueEquip >= 0)
            {
                sb.Append(" + ");
            }
            sb.Append(valueEquip);
            sb.Append(")");
            return sb.ToString();
        }

        private string GetModifiersText(CharacterStat stat)
        {
            sb.Length = 0;

            for (int i = 0; i < stat.StatModifiers.Count; i++)
            {
                StatModifier mod = stat.StatModifiers[i];

                sb.Append(((ItemData)mod.Source).name);
                sb.Append(": ");

                Debug.Log("mod.Value: " + mod.Value);
                if (mod.Value > 0)
                {
                    sb.Append("+");
                }

                if (mod.Type == StatModType.Flat)
                {
                    sb.Append(mod.Value);
                }
                else
                {
                    sb.Append(mod.Value * 100);
                    sb.Append("%");
                }

                if (i < stat.StatModifiers.Count - 1)
                {
                    sb.AppendLine();
                }
                if (mod.Value > 0)
                {
                    ApplyColorToSubstring(sb.ToString(), Color.green);
                }
                else
                {
                    ApplyColorToSubstring(sb.ToString(), Color.red);
                }
            }

            return sb.ToString();
        }

        private string FirstLetterToUpper(string s)
        {
            if (string.IsNullOrEmpty(s))
                return null;

            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
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
