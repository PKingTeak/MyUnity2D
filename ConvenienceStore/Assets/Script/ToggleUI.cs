using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToggleUI : MonoBehaviour
{
    public GameObject Text;
    // Start is called before the first frame update

    private void Awake()
    {
        Text.SetActive(false);
    }

    public void TextON()
    {
        Text.SetActive(true);
    }

    public void TextOff()
    {
        Text.SetActive(false);
    }
}
