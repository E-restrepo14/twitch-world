using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UiManager : MonoBehaviour
{
    [SerializeField] private int subsDonated = 123;
    [SerializeField] private TMP_Text subsNumberText;

    [SerializeField] private Texture2D cursorAim;
    [SerializeField] private Texture2D cursorDefault;
    [SerializeField] private Vector2 hotSpot;

    
    [SerializeField] private Image aimHudImage;

    [SerializeField] private GameObject spinex;
    [SerializeField] private GameObject spiney;
    [SerializeField] private GameObject character;


    public Vector2 mousePositionClamped;


    // Start is called before the first frame update
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Confined;
        // Cursor.lockState = CursorLockMode.None;

        // subsNumberText = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        ShowSubsDonated();
        hotSpot = new Vector2(cursorAim.width / 2, cursorAim.height / 2);
        Cursor.SetCursor(cursorAim, hotSpot, CursorMode.ForceSoftware);

        //        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 vectorMousePosition = Input.mousePosition;
        mousePositionClamped.x = Mathf.Clamp01(vectorMousePosition.x / Screen.width);
        mousePositionClamped.y = Mathf.Clamp01(vectorMousePosition.y / Screen.height);

        float ejeX = ((mousePositionClamped.x * 1920) * 0.0625f) - 60; // -----> esta ecuacion es para que valla de: -60 a 60... si el mouse en x es de: 0 a 1
        spinex.transform.localRotation = Quaternion.Euler(0, (ejeX), 0);

        if (ejeX < 0.5f)
        {
            character.GetComponent<Animator>().SetBool("isLookingRight", false);
        }
        if (ejeX > 0.5f)
        {
            character.GetComponent<Animator>().SetBool("isLookingRight", true);
        }

        float ejeY = ((mousePositionClamped.y * 1080) * -0.06852f) + 74; // -----> esta ecuacion es para que valla de: 60 a -14... si el mouse en y es de: 0 a 1
        spiney.transform.localRotation = Quaternion.Euler(ejeY, 0, 0);
    }

    public void ShowSubsDonated()
    {
        // acá mismo podría ordenar a las umaru chans que hagan la animacion de baile
        subsNumberText.GetComponent<TMP_Text>().text = subsDonated.ToString();
    }


}
