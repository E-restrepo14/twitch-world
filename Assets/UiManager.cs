using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class UiManager : MonoBehaviour
{
    public int subsDonated = 123;
    public TMP_Text subsNumberText;


    // Start is called before the first frame update
    void Awake()
    {
       // subsNumberText = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        ShowSubsDonated();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowSubsDonated()
    {
        // acá mismo podría ordenar a las umaru chans que hagan la animacion de baile
        subsNumberText.GetComponent<TMP_Text>().text = subsDonated.ToString();
    }
}
