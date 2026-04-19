using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.SimpleLocalization.Scripts
{
	/// <summary>
	/// Localize text component.
	/// </summary>
    [RequireComponent(typeof(Text))]
    public class LocalizedText : MonoBehaviour
    {
        public string Text { get => _localizationKey; set { _localizationKey = value; Localize(); } }

       [SerializeField] private string _localizationKey;
        private Text _text;
        private string[] _value;
        private string _colorID;
        public void SetValue(string[] value, string key, string colorID)
        {
            _localizationKey = key;
            _value = value;
            _colorID = colorID;
            
            Localize();
        }
        public void SetValue(string[] value, string key) => SetValue(value, key, null);
        public void SetValue(string value, string key) => SetValue(new string[] { value }, key);
        public void SetValue(double[] value, string key, string colorID)
        {
            string[] v = value.Select(n => n.ToString()).ToArray();
            SetValue(v, key, colorID);
        }
        public void SetValue(double[] value, string key) => SetValue(value, key, null);
        public void SetValue(double value, string key) => SetValue(new double[] { value }, key);
        public void Start()
        {
            Localize();
            LocalizationManager.OnLocalizationChanged += Localize;
        }

        private void InitText()
        {
            if (!_text)
            {
                _text = GetComponent<Text>();
            }
        }
        public void OnDestroy()
        {
            LocalizationManager.OnLocalizationChanged -= Localize;
        }

        private void Localize()
        {
            InitText();
            if (_value == null)
            {
                _text.text = LocalizationManager.Localize(_localizationKey);
            }
            else
            {
                _text.text = MyString.SetValueInText(_value, LocalizationManager.Localize(_localizationKey), _colorID);
            }                      
        }
    }
}