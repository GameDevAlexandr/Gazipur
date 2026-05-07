using UnityEngine;
using Zenject;

public class LocationChanger : MonoBehaviour
{
    [Inject] private Sounds _sounds;

    private void OnTriggerEnter(Collider other)
    {
        var tag = other.tag;
        switch (tag)
        {
            case "AreaVillage": _sounds.ChangeBackground(_sounds.Background[0]);
                break;
            case "AreaRich": _sounds.ChangeBackground(_sounds.Background[1]);
                break;
            case "AreaDanger": _sounds.ChangeBackground(_sounds.Background[2]);
                break;

        }
    }
}