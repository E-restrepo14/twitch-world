using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;



public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject overlay;

    [SerializeField] private int m_bits = 0;// hasta la linea 18 son elementos 
    [SerializeField] private TMP_Text bitsText;//necesarios para guardar score
    public string currentPlayerName = "no name";
    [SerializeField] private int hihgestScore;
    [SerializeField] private string topPlayer;

    [SerializeField] private float currentTime = 0f;
    private float startingTime = 60f;
    [SerializeField] private TMP_Text timerText;

    private int subsDonated = 0;
    [SerializeField] private TMP_Text subsNumberText;

    [SerializeField] private int comboInt = 0;
    [SerializeField] private TMP_Text comboText;


    [SerializeField] private Texture2D cursorTwitch;
    [SerializeField] private Texture2D cursorAim;
    [SerializeField] private Texture2D cursorDefault;
    private Vector2 hotSpot;
    [SerializeField] private Image aimHudImage;

    [SerializeField] private GameObject spinex;
    [SerializeField] private GameObject spiney;
    [SerializeField] private GameObject character;

    Animator canvasAnimator;

    public Vector2 mousePositionClamped;


    public static UiManager Instance; 

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        Cursor.lockState = CursorLockMode.Confined;
        // Cursor.lockState = CursorLockMode.None;

        // subsNumberText = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        LoadBits();

        currentTime = startingTime;
        ShowSubsDonated();
        hotSpot = new Vector2(cursorAim.width / 2, cursorAim.height / 2);
        Cursor.SetCursor(cursorAim, hotSpot, CursorMode.ForceSoftware);
        canvasAnimator = gameObject.GetComponent<Animator>();
        //        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }

    IEnumerator StopCharacter()
    {
        float i = 1;
        Quaternion xOrigin = spinex.transform.localRotation;
        Quaternion yOrigin = spiney.transform.localRotation;
        GameObject world = GameObject.FindWithTag("world");

        while (i > 0)
        {
            i -= (Time.deltaTime);
            world.GetComponent<Animator>().speed = i;
           

            yield return new WaitForSeconds(0);
            if (character.GetComponent<Animator>().GetBool("isLookingRight"))
            {
                spinex.transform.localRotation = Quaternion.Slerp(Quaternion.Euler(0, 0, 0), xOrigin, i);
                spiney.transform.localRotation = Quaternion.Slerp(Quaternion.Euler(0, -15, 0), yOrigin,i);
            }
            else
            {
                spinex.transform.localRotation = Quaternion.Slerp(Quaternion.Euler(0, 0, 0), xOrigin, i);
                spiney.transform.localRotation = Quaternion.Slerp(Quaternion.Euler(15, 0, 0), yOrigin,i);
            }
        }
        yield return new WaitForSeconds(0);
    }

    void CountDown()
    {
        if (currentTime > 0)
        {
            currentTime -= 1 * Time.deltaTime;
            timerText.GetComponent<TMP_Text>().text = currentTime.ToString("0");

        }
        if (currentTime <= 0 & GameManager.Instance.isPlaying)
        {
            GameManager.Instance.isPlaying = false;
            StartCoroutine(StopCharacter());
            TurnAimTwitch();

            character.GetComponent<Animator>().SetBool("hasStopped", true);
            canvasAnimator.SetTrigger("timeIsUp");

            //      canvasAnimator.Play("Base Layer.gameoverByTime", 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        CountDown();
        if (GameManager.Instance.isPlaying)
        {
            AnimateCharacter();
        }
    }

    void AnimateCharacter()
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

    public void AnimateComboBybool()
    {
        comboInt += 1;
        subsDonated += 1;
        ShowSubsDonated();
        AddPoint(subsDonated);
        comboText.GetComponent<TMP_Text>().text = comboInt.ToString();
        // el bool se desactivaría solo... mejor lo hago switch
        canvasAnimator.SetTrigger("isInCombo");
    }

    public void StopCombo()
    {
        //        canvasAnimator.ResetTrigger("isInCombo");
        comboInt = 0;
        PlaySomething();
    }

    void PlaySomething()
    {
        int randomInt = Random.Range(0, 2);
        switch (randomInt)
        {
            case 0:
                overlay.GetComponent<Animator>().SetTrigger("sonouau");
                break;
            case 1:
                overlay.GetComponent<Animator>().SetTrigger("sononyan");
                break;
            default:
                break;
        }
    }

    public void ShowSubsDonated()
    {
        // acá mismo podría ordenar a las umaru chans que hagan la animacion de baile
        subsNumberText.GetComponent<TMP_Text>().text = subsDonated.ToString();
    }

    public void TurnAimRed()
    {
        Cursor.SetCursor(cursorDefault, hotSpot, CursorMode.ForceSoftware);
    }

    private void TurnAimTwitch()
    {
        Cursor.SetCursor(cursorTwitch, hotSpot, CursorMode.ForceSoftware);
    }

    public void TurnAimWhite()
    {
        Cursor.SetCursor(cursorAim, hotSpot, CursorMode.ForceSoftware);
    }







    public void AddPoint(int point) // esto se debe llamar desde un coso externo
    {
        m_bits = point;
        bitsText.text = $"{m_bits}";
        SaveBits();
    }

    public void GameOver()
    {
        LoadBits();
    }

    [System.Serializable]
    class SaveData
    {
        public int dataUserBits;
        public string dataUserName;
    }

    public void SaveBits()
    {
        print("data saved");
        SaveData data = new SaveData();
        data.dataUserBits = m_bits;
        data.dataUserName = currentPlayerName;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadBits()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            print("data loaded");
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            hihgestScore = data.dataUserBits;
            topPlayer = data.dataUserName;
            ShowScore(topPlayer, hihgestScore);
        }
    }

    void ShowScore(string name, int score)
    {
        bitsText.GetComponent<TMP_Text>().text = score.ToString();
    }
}
