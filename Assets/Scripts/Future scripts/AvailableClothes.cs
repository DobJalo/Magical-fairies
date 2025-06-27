using System.Collections.Generic;
using UnityEngine;

public class AvailableClothes : MonoBehaviour
{
    public GameObject lipsParent;  

    void Start()
    {
        int LipsChildCount = lipsParent.transform.childCount;

        for (int i = 0; i < LipsArray.items.Length && i < LipsChildCount; i++)
        {
            if (LipsArray.GetValue(i) == 0)
            {
                lipsParent.transform.GetChild(i).gameObject.SetActive(true); // CHANGE IT TO FALSE
            }
        }
    }
}
