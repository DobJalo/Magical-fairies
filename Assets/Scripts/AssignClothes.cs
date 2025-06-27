using UnityEngine;
using UnityEngine.UI;

public class AssignClothes : MonoBehaviour
{
    public GameObject BodyParent;
    int index; 


    void Start()
    {
        // get this button component in order to access OnClick
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }

    void OnClick()
    {

        if (this.gameObject.CompareTag("TakeOff"))
        {
            BodyParent.SetActive(false);
        }
        else
        {
            index = transform.GetSiblingIndex(); // get this object's index as a child
            BodyParent.SetActive(true);

            for (int i = 0; i < BodyParent.transform.childCount; i++) // check every child in BodyParent
            {
                if (i == index)
                {
                    BodyParent.transform.GetChild(i).gameObject.SetActive(true); // current object set true
                }
                else
                {
                    BodyParent.transform.GetChild(i).gameObject.SetActive(false); // other objects set false
                }
            }
        }
    }

}
