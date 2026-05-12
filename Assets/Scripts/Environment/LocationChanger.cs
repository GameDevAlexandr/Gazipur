using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LocationChanger : MonoBehaviour
{
    [Inject] private Sounds _sounds;
    [SerializeField] private Text _locationText;

    private string currentTag;

    private void OnTriggerEnter(Collider other)
    {
        string tag = other.tag;

        if (currentTag != tag)
        {
            switch (tag)
            {
                case "AreaVillage":
                    _sounds.ChangeBackground(_sounds.Background[0]);
                    _locationText.text = "Деревня";
                    break;

                case "AreaRich":
                    _sounds.ChangeBackground(_sounds.Background[1]);
                    _locationText.text = "Рангаредди";
                    break;

                case "AreaDanger":
                    _sounds.ChangeBackground(_sounds.Background[2]);
                    _locationText.text = "Роро-Хиллз";
                    break;
            }

            currentTag = tag;
        }        
    }
}