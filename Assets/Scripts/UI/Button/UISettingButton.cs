using UnityEngine;
using UnityEngine.UI;

namespace WinterGameJam
{
    public class UISettingButton : MonoBehaviour, IScriptableObjectProperty
    {

        [SerializeField] private Setting setting;
        [SerializeField] private Text titleText;
        [SerializeField] private Text valueText;

        [SerializeField] private Image previousImage;
        [SerializeField] private Image nextImage;

        private void Start()
        {
            ApplyProperty(setting);
        }

        public void ApplyProperty(ScriptableObject property)
        {
            if (property == null) return;

            if (property is Setting == false)
                return;
            setting = property as Setting;

            UpdateInfo();
        }

        public void SetNextValueSetting()
        {
            setting?.SetNextValue();
            setting?.Apply();
            UpdateInfo();
        }
        public void SetPrevoiusValueSetting()
        {
            setting?.SetPreviousValue();
            setting?.Apply();
            UpdateInfo();
        }

        private void UpdateInfo()
        {
            titleText.text = setting.Title;
            valueText.text = setting.GetStringValue();

            previousImage.enabled = !setting.IsMinValue;
            nextImage.enabled = !setting.IsMaxValue;
        }
    }
}