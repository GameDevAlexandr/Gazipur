//using UnityEditor.UIElements;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class LocationChanger : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("AreaVillage"))
        {

        }
        
        if (other.gameObject.CompareTag("AreaRich"))
        {

        }

        if (other.gameObject.CompareTag("AreaDanger"))
        {

        }
    }
}