using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int keyGenerateChanger;

    public int arraylength;
    public int[] generalKeyArray;
    public int[] playerKeyArray;

    public int[] evenNumberArray = { -210, -150, -90, -30, 30, 90, 150, 210 };
    public int[] oddNumberArray = { -240, -180, -120, -60, 0, 60, 120, 180, 240 };
    public int y = 96;

    public int o;
    public int e;

    public int keyNumber;
    public bool isKeyEnter = false;

    public bool isOneTurn = false;

    public bool isGenerate = false;

    [SerializeField] GameObject canvas;

    public GameObject UpArrowBlack;
    public GameObject UpArrowBlue;
    public GameObject UpArrowOrange;
    public GameObject UpArrowPink;

    public GameObject DownArrowBlack;
    public GameObject DownArrowBlue;
    public GameObject DownArrowOrange;
    public GameObject DownArrowPink;

    public GameObject RightArrowBlack;
    public GameObject RightArrowBlue;
    public GameObject RightArrowOrange;
    public GameObject RightArrowPink;

    public GameObject LeftArrowBlack;
    public GameObject LeftArrowBlue;
    public GameObject LeftArrowOrange;
    public GameObject LeftArrowPink;

    public GameObject FourColorKeys;
    public GameObject FourColorKeyNormal;

    public GameObject[] generateGeneralGameObjectArray;
    public GameObject[] generatePlayerGameObjectArray;

    [SerializeField] int maxHP = 25;
    [SerializeField] int damage = 4;
    [SerializeField] int MissDamage = 10;
    [SerializeField] int BadDamage = 4;
    public int currentHP;

    public Image HPGauge1;
    public Image HPGauge2;
    public Image HPGauge3;
    public Image HPGauge4;
    public Image HPGauge5;
    public Image HPGauge6;
    public Image HPGauge7;
    public Image HPGauge8;
    public Image HPGauge9;
    public Image HPGauge10;
    public Image HPGauge11;
    public Image HPGauge12;
    public Image HPGauge13;
    public Image HPGauge14;
    public Image HPGauge15;
    public Image HPGauge16;
    public Image HPGauge17;
    public Image HPGauge18;
    public Image HPGauge19;
    public Image HPGauge20;
    public Image HPGauge21;
    public Image HPGauge22;
    public Image HPGauge23;
    public Image HPGauge24;
    public Image HPGauge25;

    public int[] HPGaugesArray = new int[25];

    [SerializeField] public Image KnockGauge;
    [SerializeField] float maxMP = 300;
    [SerializeField] int MPPoint = 10;
    public float currentMP;
    public float mpRatio;
    public Text MPRatioText;

    [SerializeField] int healPoint = 3;
    [SerializeField] int balanceHealPoint = 1;

    [SerializeField] public Image TimerGauge;
    [SerializeField] float maxTime = 3;
    [SerializeField] float timeIncrease = 1.0f;
    public float currentTime;
    public Text timerText;

    public int judge = 0;
    public bool isKnock = false;
    public bool isKnockEnter = false;

    public int combo;
    [SerializeField] public int maxCombo = 99;
    public Text comboText;
    public Text judgeMiss;
    public Text judgeBad;
    public Text judgeGreat;
    public Text judgePerfect;

    public static int getPoint;
    public Text getPointText;
    [SerializeField] public int GreatPoint = 100;
    [SerializeField] public int PerfectPoint = 500;

    public static int KnockoutRobot;
    public Text KnockoutRobotText;

    Animator anim;
    public GameObject player;

    Animator robot1anim;
    public GameObject Robot1;

    public AudioClip generalKeySound;
    public AudioClip getKeySuccessSound;
    public AudioClip getKeyFailSound;
    public AudioClip KnockSound;
    public AudioClip hitRobotSound1;
    public AudioClip hitRobotSound2;
    public AudioClip hitRobotSound3;
    public AudioClip hitRobotSound4;

    public AudioClip FourColorsKeySound;

    public AudioClip judgeMissSound;
    public AudioClip judgeBadSound;
    public AudioClip judgeGreatSound;
    public AudioClip judgePerfectSound;

    AudioSource audioSource;

    public Text countDownText;
    public Text gameOverText;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        judgeMiss.enabled = false;
        judgeBad.enabled = false;
        judgeGreat.enabled = false;
        judgePerfect.enabled = false;

        countDownText.text = "";
        StartCoroutine(CountDown());

        currentHP = maxHP;
        currentMP = 0;
        combo = 0;
        getPoint = 0;
        KnockoutRobot = 0;
        anim = player.gameObject.GetComponent<Animator>();
        robot1anim = Robot1.gameObject.GetComponent<Animator>();
        anim.SetInteger("state", 0);
        HPGaugesArray = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
    }

    void Update()
    {
        if (isOneTurn == true && isGenerate == true && currentHP > 0)
        {
            GameGenerater();
            robot1anim.SetInteger("robot1state", 0);
            isKeyEnter = true;
            isGenerate = false;
        }

        if (isKeyEnter == true)
        {
            PlayerKeyInput();
        }

        if (keyNumber > arraylength)
        {
            isKeyEnter = false;
        }

        if (isOneTurn == true && isGenerate == false)
        {
            currentTime += Time.deltaTime * timeIncrease;

            if (currentHP <= 0)
            {
                Invoke("GameOver", 1);
            }

            if ((keyNumber == arraylength) || (currentTime >= maxTime) || isKnockEnter == true)
            {
                isKeyEnter = false;
                isOneTurn = false;
                isKnockEnter = false;
                if(currentHP > 0)
                {
                    StartCoroutine("Animation");
                }
            }
        }

        if (currentMP >= maxMP)
        {
            currentMP = maxMP;
            isKnock = true;
        }

        if (currentMP < 0)
        {
            currentMP = 0;
        }

        if (combo >= maxCombo)
        {
            combo = maxCombo;
        }

        if (currentHP <= 0)
        {
            StartCoroutine(GameOver());
        }

        comboText.text = combo.ToString();
        getPointText.text = getPoint.ToString();
        timerText.text = (maxTime - currentTime).ToString("f1");
        KnockoutRobotText.text = KnockoutRobot.ToString();
        GaugeReduction();
    }

    public void KeyGenerate1()
    //SmallEnemyの場合に呼び出すKeyGenerate
    {
        arraylength = Random.Range(4, 7);
        generalKeyArray = new int[arraylength];
        generateGeneralGameObjectArray = new GameObject[arraylength];

        audioSource.PlayOneShot(generalKeySound);

        //Debug.Log("配列の長さ" + arraylength);
        int o = (9 - arraylength) / 2;
        int e = (8 - arraylength) / 2;

        for (int i = 0; i < arraylength; i++)
        {
            GameObject generateGameobject = null;
            int k = Random.Range(1, 5);
            generalKeyArray[i] = k;
            //Debug.Log("キー番号" + generalKeyArray[i]);

            if (arraylength % 2 == 1)
            {
                if (k == 1)
                {
                    generateGameobject = Instantiate(UpArrowOrange, new Vector3(oddNumberArray[o + i], y, 0), Quaternion.identity);
                    generateGameobject.transform.SetParent(canvas.transform, false);
                }

                if (k == 2)
                {
                    generateGameobject = Instantiate(DownArrowOrange, new Vector3(oddNumberArray[o + i], y, 0), Quaternion.identity);
                    generateGameobject.transform.SetParent(canvas.transform, false);
                }

                if (k == 3)
                {
                    generateGameobject = Instantiate(RightArrowOrange, new Vector3(oddNumberArray[o + i], y, 0), Quaternion.identity);
                    generateGameobject.transform.SetParent(canvas.transform, false);
                }

                if (k == 4)
                {
                    generateGameobject = Instantiate(LeftArrowOrange, new Vector3(oddNumberArray[o + i], y, 0), Quaternion.identity);
                    generateGameobject.transform.SetParent(canvas.transform, false);
                }
            }
            if (arraylength % 2 == 0)
            {
                if (k == 1)
                {
                    generateGameobject = Instantiate(UpArrowOrange, new Vector3(evenNumberArray[e + i], y, 0), Quaternion.identity);
                    generateGameobject.transform.SetParent(canvas.transform, false);
                }

                if (k == 2)
                {
                    generateGameobject = Instantiate(DownArrowOrange, new Vector3(evenNumberArray[e + i], y, 0), Quaternion.identity);
                    generateGameobject.transform.SetParent(canvas.transform, false);
                }

                if (k == 3)
                {
                    generateGameobject = Instantiate(RightArrowOrange, new Vector3(evenNumberArray[e + i], y, 0), Quaternion.identity);
                    generateGameobject.transform.SetParent(canvas.transform, false);
                }

                if (k == 4)
                {
                    generateGameobject = Instantiate(LeftArrowOrange, new Vector3(evenNumberArray[e + i], y, 0), Quaternion.identity);
                    generateGameobject.transform.SetParent(canvas.transform, false);
                }
            }

            generateGeneralGameObjectArray[i] = generateGameobject;
        }
    }

    public void KeyGenerate2()
    //LargeEnemyの場合に呼び出すKeyGenerate
    {
        arraylength = Random.Range(7, 10);
        generalKeyArray = new int[arraylength];
        generateGeneralGameObjectArray = new GameObject[arraylength];

        audioSource.PlayOneShot(generalKeySound);

        //Debug.Log("配列の長さ" + arraylength);
        int o = (9 - arraylength) / 2;
        int e = (8 - arraylength) / 2;

        for (int i = 0; i < arraylength; i++)
        {
            GameObject generateGameobject = null;
            int k = Random.Range(1, 6);
            generalKeyArray[i] = k;
            //Debug.Log("キー番号" + generalKeyArray[i]);

            if (arraylength % 2 == 1)
            {
                if (k == 1)
                {
                    generateGameobject = Instantiate(UpArrowOrange, new Vector3(oddNumberArray[o + i], y, 0), Quaternion.identity);
                    generateGameobject.transform.SetParent(canvas.transform, false);
                }

                if (k == 2)
                {
                    generateGameobject = Instantiate(DownArrowOrange, new Vector3(oddNumberArray[o + i], y, 0), Quaternion.identity);
                    generateGameobject.transform.SetParent(canvas.transform, false);
                }

                if (k == 3)
                {
                    generateGameobject = Instantiate(RightArrowOrange, new Vector3(oddNumberArray[o + i], y, 0), Quaternion.identity);
                    generateGameobject.transform.SetParent(canvas.transform, false);
                }

                if (k == 4)
                {
                    generateGameobject = Instantiate(LeftArrowOrange, new Vector3(oddNumberArray[o + i], y, 0), Quaternion.identity);
                    generateGameobject.transform.SetParent(canvas.transform, false);
                }

                if (k == 5)
                {
                    generateGameobject = Instantiate(FourColorKeys, new Vector3(oddNumberArray[o + i], y, 0), Quaternion.identity);
                    generateGameobject.transform.SetParent(canvas.transform, false);
                }
            }
            if (arraylength % 2 == 0)
            {
                if (k == 1)
                {
                    generateGameobject = Instantiate(UpArrowOrange, new Vector3(evenNumberArray[e + i], y, 0), Quaternion.identity);
                    generateGameobject.transform.SetParent(canvas.transform, false);
                }

                if (k == 2)
                {
                    generateGameobject = Instantiate(DownArrowOrange, new Vector3(evenNumberArray[e + i], y, 0), Quaternion.identity);
                    generateGameobject.transform.SetParent(canvas.transform, false);
                }

                if (k == 3)
                {
                    generateGameobject = Instantiate(RightArrowOrange, new Vector3(evenNumberArray[e + i], y, 0), Quaternion.identity);
                    generateGameobject.transform.SetParent(canvas.transform, false);
                }

                if (k == 4)
                {
                    generateGameobject = Instantiate(LeftArrowOrange, new Vector3(evenNumberArray[e + i], y, 0), Quaternion.identity);
                    generateGameobject.transform.SetParent(canvas.transform, false);
                }

                if (k == 5)
                {
                    generateGameobject = Instantiate(FourColorKeys, new Vector3(evenNumberArray[e + i], y, 0), Quaternion.identity);
                    generateGameobject.transform.SetParent(canvas.transform, false);
                }
            }
            generateGeneralGameObjectArray[i] = generateGameobject;
        }
    }

    public void PlayerKeyAllayGenerate()
    {
        playerKeyArray = new int[arraylength];
        generatePlayerGameObjectArray = new GameObject[arraylength];
        //Debug.Log("プレイヤー配列の長さ" + arraylength);   
    }

    public void PlayerKeyInput()
    {
        int o = (9 - arraylength) / 2;
        int e = (8 - arraylength) / 2;

        if (isKeyEnter)
        {
            if (arraylength % 2 == 1)
            {
                GameObject generatePlayerGameobject = null;
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    playerKeyArray[keyNumber] = 1;
                    Destroy(generateGeneralGameObjectArray[keyNumber]);

                    if (playerKeyArray[keyNumber] == generalKeyArray[keyNumber])
                    {
                        audioSource.PlayOneShot(getKeySuccessSound);
                        generatePlayerGameobject = Instantiate(UpArrowPink, new Vector3(oddNumberArray[o + keyNumber], y, 0), Quaternion.identity);
                        generatePlayerGameobject.transform.SetParent(canvas.transform, false);
                        currentMP += MPPoint;
                        judge++;
                    }
                    else if (generalKeyArray[keyNumber] == 5)
                    {
                        audioSource.PlayOneShot(FourColorsKeySound);
                        generatePlayerGameobject = Instantiate(UpArrowBlue, new Vector3(oddNumberArray[o + keyNumber], y, 0), Quaternion.identity);
                        generatePlayerGameobject.transform.SetParent(canvas.transform, false);
                        currentHP += healPoint;
                        judge++;
                    }
                    else
                    {
                        audioSource.PlayOneShot(getKeyFailSound);
                        generatePlayerGameobject = Instantiate(UpArrowBlack, new Vector3(oddNumberArray[o + keyNumber], y, 0), Quaternion.identity);
                        generatePlayerGameobject.transform.SetParent(canvas.transform, false);
                        currentHP -= damage;
                    }

                    generatePlayerGameObjectArray[keyNumber] = generatePlayerGameobject;

                    keyNumber++;
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    playerKeyArray[keyNumber] = 2;
                    Destroy(generateGeneralGameObjectArray[keyNumber]);

                    if (playerKeyArray[keyNumber] == generalKeyArray[keyNumber])
                    {
                        audioSource.PlayOneShot(getKeySuccessSound);
                        generatePlayerGameobject = Instantiate(DownArrowPink, new Vector3(oddNumberArray[o + keyNumber], y, 0), Quaternion.identity);
                        generatePlayerGameobject.transform.SetParent(canvas.transform, false);
                        currentMP += MPPoint;
                        judge++;
                    }
                    else if (generalKeyArray[keyNumber] == 5)
                    {
                        audioSource.PlayOneShot(FourColorsKeySound);
                        generatePlayerGameobject = Instantiate(DownArrowBlue, new Vector3(oddNumberArray[o + keyNumber], y, 0), Quaternion.identity);
                        generatePlayerGameobject.transform.SetParent(canvas.transform, false);
                        combo += 2;
                        judge++;
                    }
                    else
                    {
                        audioSource.PlayOneShot(getKeyFailSound);
                        generatePlayerGameobject = Instantiate(DownArrowBlack, new Vector3(oddNumberArray[o + keyNumber], y, 0), Quaternion.identity);
                        generatePlayerGameobject.transform.SetParent(canvas.transform, false);
                        currentHP -= damage;
                    }

                    generatePlayerGameObjectArray[keyNumber] = generatePlayerGameobject;

                    keyNumber++;
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    playerKeyArray[keyNumber] = 3;
                    Destroy(generateGeneralGameObjectArray[keyNumber]);

                    if (playerKeyArray[keyNumber] == generalKeyArray[keyNumber])
                    {
                        audioSource.PlayOneShot(getKeySuccessSound);
                        generatePlayerGameobject = Instantiate(RightArrowPink, new Vector3(oddNumberArray[o + keyNumber], y, 0), Quaternion.identity);
                        generatePlayerGameobject.transform.SetParent(canvas.transform, false);
                        currentMP += MPPoint;
                        judge++;
                    }
                    else if (generalKeyArray[keyNumber] == 5)
                    {
                        audioSource.PlayOneShot(FourColorsKeySound);
                        generatePlayerGameobject = Instantiate(RightArrowBlue, new Vector3(oddNumberArray[o + keyNumber], y, 0), Quaternion.identity);
                        generatePlayerGameobject.transform.SetParent(canvas.transform, false);
                        currentMP += MPPoint * 3;
                        judge++;
                    }
                    else
                    {
                        audioSource.PlayOneShot(getKeyFailSound);
                        generatePlayerGameobject = Instantiate(RightArrowBlack, new Vector3(oddNumberArray[o + keyNumber], y, 0), Quaternion.identity);
                        generatePlayerGameobject.transform.SetParent(canvas.transform, false);
                        currentHP -= damage;
                    }

                    generatePlayerGameObjectArray[keyNumber] = generatePlayerGameobject;

                    keyNumber++;
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    playerKeyArray[keyNumber] = 4;
                    Destroy(generateGeneralGameObjectArray[keyNumber]);

                    if (playerKeyArray[keyNumber] == generalKeyArray[keyNumber])
                    {
                        audioSource.PlayOneShot(getKeySuccessSound);
                        generatePlayerGameobject = Instantiate(LeftArrowPink, new Vector3(oddNumberArray[o + keyNumber], y, 0), Quaternion.identity);
                        generatePlayerGameobject.transform.SetParent(canvas.transform, false);
                        currentMP += MPPoint;
                        judge++;
                    }
                    else if (generalKeyArray[keyNumber] == 5)
                    {
                        audioSource.PlayOneShot(FourColorsKeySound);
                        generatePlayerGameobject = Instantiate(LeftArrowBlue, new Vector3(oddNumberArray[o + keyNumber], y, 0), Quaternion.identity);
                        generatePlayerGameobject.transform.SetParent(canvas.transform, false);
                        currentHP += balanceHealPoint;
                        currentMP += balanceHealPoint;
                        combo++;
                        judge++;
                    }
                    else
                    {
                        audioSource.PlayOneShot(getKeyFailSound);
                        generatePlayerGameobject = Instantiate(LeftArrowBlack, new Vector3(oddNumberArray[o + keyNumber], y, 0), Quaternion.identity);
                        generatePlayerGameobject.transform.SetParent(canvas.transform, false);
                        currentHP -= damage;
                    }

                    generatePlayerGameObjectArray[keyNumber] = generatePlayerGameobject;

                    keyNumber++;
                }
            }

            if (arraylength % 2 == 0)
            {
                GameObject generatePlayerGameobject = null;
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    playerKeyArray[keyNumber] = 1;
                    Destroy(generateGeneralGameObjectArray[keyNumber]);

                    if (playerKeyArray[keyNumber] == generalKeyArray[keyNumber])
                    {
                        audioSource.PlayOneShot(getKeySuccessSound);
                        generatePlayerGameobject = Instantiate(UpArrowPink, new Vector3(evenNumberArray[e + keyNumber], y, 0), Quaternion.identity);
                        generatePlayerGameobject.transform.SetParent(canvas.transform, false);
                        currentMP += MPPoint;
                        judge++;
                    }
                    else if (generalKeyArray[keyNumber] == 5)
                    {
                        audioSource.PlayOneShot(FourColorsKeySound);
                        generatePlayerGameobject = Instantiate(UpArrowBlue, new Vector3(evenNumberArray[e + keyNumber], y, 0), Quaternion.identity);
                        generatePlayerGameobject.transform.SetParent(canvas.transform, false);
                        currentHP += healPoint;
                        judge++;
                    }
                    else
                    {
                        audioSource.PlayOneShot(getKeyFailSound);
                        generatePlayerGameobject = Instantiate(UpArrowBlack, new Vector3(evenNumberArray[e + keyNumber], y, 0), Quaternion.identity);
                        generatePlayerGameobject.transform.SetParent(canvas.transform, false);
                        currentHP -= damage;
                    }

                    generatePlayerGameObjectArray[keyNumber] = generatePlayerGameobject;

                    keyNumber++;
                }
                else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    playerKeyArray[keyNumber] = 2;
                    Destroy(generateGeneralGameObjectArray[keyNumber]);

                    if (playerKeyArray[keyNumber] == generalKeyArray[keyNumber])
                    {
                        audioSource.PlayOneShot(getKeySuccessSound);
                        generatePlayerGameobject = Instantiate(DownArrowPink, new Vector3(evenNumberArray[e + keyNumber], y, 0), Quaternion.identity);
                        generatePlayerGameobject.transform.SetParent(canvas.transform, false);
                        currentMP += MPPoint;
                        judge++;
                    }
                    else if (generalKeyArray[keyNumber] == 5)
                    {
                        audioSource.PlayOneShot(FourColorsKeySound);
                        generatePlayerGameobject = Instantiate(DownArrowBlue, new Vector3(evenNumberArray[e + keyNumber], y, 0), Quaternion.identity);
                        generatePlayerGameobject.transform.SetParent(canvas.transform, false);
                        combo += 2;
                        judge++;
                    }
                    else
                    {
                        audioSource.PlayOneShot(getKeyFailSound);
                        generatePlayerGameobject = Instantiate(DownArrowBlack, new Vector3(evenNumberArray[e + keyNumber], y, 0), Quaternion.identity);
                        generatePlayerGameobject.transform.SetParent(canvas.transform, false);
                        currentHP -= damage;
                    }

                    generatePlayerGameObjectArray[keyNumber] = generatePlayerGameobject;

                    keyNumber++;
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    playerKeyArray[keyNumber] = 3;
                    Destroy(generateGeneralGameObjectArray[keyNumber]);

                    if (playerKeyArray[keyNumber] == generalKeyArray[keyNumber])
                    {
                        audioSource.PlayOneShot(getKeySuccessSound);
                        generatePlayerGameobject = Instantiate(RightArrowPink, new Vector3(evenNumberArray[e + keyNumber], y, 0), Quaternion.identity);
                        generatePlayerGameobject.transform.SetParent(canvas.transform, false);
                        currentMP += MPPoint;
                        judge++;
                    }
                    else if (generalKeyArray[keyNumber] == 5)
                    {
                        audioSource.PlayOneShot(FourColorsKeySound);
                        generatePlayerGameobject = Instantiate(RightArrowBlue, new Vector3(evenNumberArray[e + keyNumber], y, 0), Quaternion.identity);
                        generatePlayerGameobject.transform.SetParent(canvas.transform, false);
                        currentMP += MPPoint * 3;
                        judge++;
                    }
                    else
                    {
                        audioSource.PlayOneShot(getKeyFailSound);
                        generatePlayerGameobject = Instantiate(RightArrowBlack, new Vector3(evenNumberArray[e + keyNumber], y, 0), Quaternion.identity);
                        generatePlayerGameobject.transform.SetParent(canvas.transform, false);
                        currentHP -= damage;
                    }

                    generatePlayerGameObjectArray[keyNumber] = generatePlayerGameobject;

                    keyNumber++;
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    playerKeyArray[keyNumber] = 4;
                    Destroy(generateGeneralGameObjectArray[keyNumber]);

                    if (playerKeyArray[keyNumber] == generalKeyArray[keyNumber])
                    {
                        audioSource.PlayOneShot(getKeySuccessSound);
                        generatePlayerGameobject = Instantiate(LeftArrowPink, new Vector3(evenNumberArray[e + keyNumber], y, 0), Quaternion.identity);
                        generatePlayerGameobject.transform.SetParent(canvas.transform, false);
                        currentMP += MPPoint;
                        judge++;
                    }
                    else if (generalKeyArray[keyNumber] == 5)
                    {
                        audioSource.PlayOneShot(FourColorsKeySound);
                        generatePlayerGameobject = Instantiate(LeftArrowBlue, new Vector3(evenNumberArray[e + keyNumber], y, 0), Quaternion.identity);
                        generatePlayerGameobject.transform.SetParent(canvas.transform, false);
                        currentHP += balanceHealPoint;
                        currentMP += MPPoint * 2;
                        combo++;
                        judge++;
                    }
                    else
                    {
                        audioSource.PlayOneShot(getKeyFailSound);
                        generatePlayerGameobject = Instantiate(LeftArrowBlack, new Vector3(evenNumberArray[e + keyNumber], y, 0), Quaternion.identity);
                        generatePlayerGameobject.transform.SetParent(canvas.transform, false);
                        currentHP -= damage;
                    }

                    generatePlayerGameObjectArray[keyNumber] = generatePlayerGameobject;

                    keyNumber++;
                }
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (isKnock == true)
                {
                    Knock();
                    isKnock = false;
                    isKnockEnter = true;
                }
            }
        }
    }

    public void GaugeReduction()
    {
        //ここからしばらく愚かしいコードたち
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }

        if (currentHP <= 0)
        {
            currentHP = 0;
        }

        if (currentHP == 0)
        {
            HPGaugesArray = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        }
        else if (currentHP == 1)
        {
            HPGaugesArray = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 };
        }
        else if (currentHP == 2)
        {
            HPGaugesArray = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1 };
        }
        else if (currentHP == 3)
        {
            HPGaugesArray = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1 };
        }
        else if (currentHP == 4)
        {
            HPGaugesArray = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1 };
        }
        else if (currentHP == 5)
        {
            HPGaugesArray = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1 };
        }
        else if (currentHP == 6)
        {
            HPGaugesArray = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1 };
        }
        else if (currentHP == 7)
        {
            HPGaugesArray = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1 };
        }
        else if (currentHP == 8)
        {
            HPGaugesArray = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1 };
        }
        else if (currentHP == 9)
        {
            HPGaugesArray = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        }
        else if (currentHP == 10)
        {
            HPGaugesArray = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        }
        else if (currentHP == 11)
        {
            HPGaugesArray = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        }
        else if (currentHP == 12)
        {
            HPGaugesArray = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        }
        else if (currentHP == 13)
        {
            HPGaugesArray = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        }
        else if (currentHP == 14)
        {
            HPGaugesArray = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        }
        else if (currentHP == 15)
        {
            HPGaugesArray = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        }
        else if (currentHP == 16)
        {
            HPGaugesArray = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        }
        else if (currentHP == 17)
        {
            HPGaugesArray = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        }
        else if (currentHP == 18)
        {
            HPGaugesArray = new int[] { 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        }
        else if (currentHP == 19)
        {
            HPGaugesArray = new int[] { 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        }
        else if (currentHP == 20)
        {
            HPGaugesArray = new int[] { 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        }
        else if (currentHP == 21)
        {
            HPGaugesArray = new int[] { 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        }
        else if (currentHP == 22)
        {
            HPGaugesArray = new int[] { 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        }
        else if (currentHP == 23)
        {
            HPGaugesArray = new int[] { 0, 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        }
        else if (currentHP == 24)
        {
            HPGaugesArray = new int[] { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        }
        else if (currentHP == maxHP)
        {
            HPGaugesArray = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        }

        if (HPGaugesArray[0] == 1)
        {
            HPGauge1.enabled = true;
        }
        else
        {
            HPGauge1.enabled = false;
        }

        if (HPGaugesArray[1] == 1)
        {
            HPGauge2.enabled = true;
        }
        else
        {
            HPGauge2.enabled = false;
        }

        if (HPGaugesArray[2] == 1)
        {
            HPGauge3.enabled = true;
        }
        else
        {
            HPGauge3.enabled = false;
        }

        if (HPGaugesArray[3] == 1)
        {
            HPGauge4.enabled = true;
        }
        else
        {
            HPGauge4.enabled = false;
        }

        if (HPGaugesArray[4] == 1)
        {
            HPGauge5.enabled = true;
        }
        else
        {
            HPGauge5.enabled = false;
        }

        if (HPGaugesArray[5] == 1)
        {
            HPGauge6.enabled = true;
        }
        else
        {
            HPGauge6.enabled = false;
        }

        if (HPGaugesArray[6] == 1)
        {
            HPGauge7.enabled = true;
        }
        else
        {
            HPGauge7.enabled = false;
        }

        if (HPGaugesArray[7] == 1)
        {
            HPGauge8.enabled = true;
        }
        else
        {
            HPGauge8.enabled = false;
        }

        if (HPGaugesArray[8] == 1)
        {
            HPGauge9.enabled = true;
        }
        else
        {
            HPGauge9.enabled = false;
        }

        if (HPGaugesArray[9] == 1)
        {
            HPGauge10.enabled = true;
        }
        else
        {
            HPGauge10.enabled = false;
        }

        if (HPGaugesArray[10] == 1)
        {
            HPGauge11.enabled = true;
        }
        else
        {
            HPGauge11.enabled = false;
        }

        if (HPGaugesArray[11] == 1)
        {
            HPGauge12.enabled = true;
        }
        else
        {
            HPGauge12.enabled = false;
        }

        if (HPGaugesArray[12] == 1)
        {
            HPGauge13.enabled = true;
        }
        else
        {
            HPGauge13.enabled = false;
        }

        if (HPGaugesArray[13] == 1)
        {
            HPGauge14.enabled = true;
        }
        else
        {
            HPGauge14.enabled = false;
        }

        if (HPGaugesArray[14] == 1)
        {
            HPGauge15.enabled = true;
        }
        else
        {
            HPGauge15.enabled = false;
        }

        if (HPGaugesArray[15] == 1)
        {
            HPGauge16.enabled = true;
        }
        else
        {
            HPGauge16.enabled = false;
        }

        if (HPGaugesArray[16] == 1)
        {
            HPGauge17.enabled = true;
        }
        else
        {
            HPGauge17.enabled = false;
        }

        if (HPGaugesArray[17] == 1)
        {
            HPGauge18.enabled = true;
        }
        else
        {
            HPGauge18.enabled = false;
        }

        if (HPGaugesArray[18] == 1)
        {
            HPGauge19.enabled = true;
        }
        else
        {
            HPGauge19.enabled = false;
        }

        if (HPGaugesArray[19] == 1)
        {
            HPGauge20.enabled = true;
        }
        else
        {
            HPGauge20.enabled = false;
        }

        if (HPGaugesArray[20] == 1)
        {
            HPGauge21.enabled = true;
        }
        else
        {
            HPGauge21.enabled = false;
        }

        if (HPGaugesArray[21] == 1)
        {
            HPGauge22.enabled = true;
        }
        else
        {
            HPGauge22.enabled = false;
        }

        if (HPGaugesArray[22] == 1)
        {
            HPGauge23.enabled = true;
        }
        else
        {
            HPGauge23.enabled = false;
        }

        if (HPGaugesArray[23] == 1)
        {
            HPGauge24.enabled = true;
        }
        else
        {
            HPGauge24.enabled = false;
        }

        if (HPGaugesArray[24] == 1)
        {
            HPGauge25.enabled = true;
        }
        else
        {
            HPGauge25.enabled = false;
        }

        //float fillProp = 0.75f;
        KnockGauge.fillAmount = (float)currentMP / (float)maxMP;
        //KnockGauge.fillAmount *= fillProp;
        mpRatio = Mathf.Ceil(((float)currentMP / (float)maxMP) * 100);

        MPRatioText.text = mpRatio.ToString();

        TimerGauge.fillAmount = currentTime / maxTime;
    }

    public void GameGenerater()
    {
        keyGenerateChanger = Random.Range(1, 3);
        //Debug.Log(keyGenerateChanger);
        if (keyGenerateChanger == 1)
        {
            KeyGenerate1();
        }
        if (keyGenerateChanger == 2)
        {
            KeyGenerate2();
        }
        //Debug.Log(keyGenerateChanger);

        keyNumber = 0;
        currentTime = 0;
        judge = 0;

        PlayerKeyAllayGenerate();
        //Debug.Log("現在のkeyNumber" + keyNumber);
    }

    public void OneTurnSwitcher()
    {
        judgeMiss.enabled = false;
        judgeBad.enabled = false;
        judgeGreat.enabled = false;
        judgePerfect.enabled = false;
        isOneTurn = true;
        isGenerate = true;
    }

    public void UIReset()
    {
        foreach (GameObject i in generatePlayerGameObjectArray)
        {
            Destroy(i);
        }

        foreach (GameObject i in generateGeneralGameObjectArray)
        {
            Destroy(i);
        }
        judgeMiss.enabled = false;
        judgeBad.enabled = false;
        judgeGreat.enabled = false;
        judgePerfect.enabled = false;
    }

    public void Judge()
    {
        if (judge < arraylength / 3)
        {
            judgeMiss.enabled = true;
            currentHP -= MissDamage;
            audioSource.PlayOneShot(judgeMissSound);
            combo = 0;
        }
        if (judge >= arraylength / 3 && judge <= arraylength / 2)
        {
            judgeBad.enabled = true;
            currentHP -= BadDamage;
            audioSource.PlayOneShot(judgeBadSound);
            combo = 0;
        }
        if (judge > arraylength / 2 && judge < arraylength)
        {
            judgeGreat.enabled = true;
            audioSource.PlayOneShot(judgeGreatSound);
            combo = (int)Mathf.Ceil(combo * 0.5f);
            getPoint += GreatPoint * combo;
        }
        if (judge == arraylength)
        {
            judgePerfect.enabled = true;
            audioSource.PlayOneShot(judgePerfectSound);
            combo += 5;
            getPoint += PerfectPoint * combo;
            KnockoutRobot++;
        }
    }

    public void Knock()
    {
        audioSource.PlayOneShot(KnockSound);

        foreach (GameObject i in generatePlayerGameObjectArray)
        {
            Destroy(i);
        }

        int o = (9 - arraylength) / 2;
        int e = (8 - arraylength) / 2;
        currentMP = 0;
        judge = arraylength;
        for (int i = 0; i < arraylength; i++)
        {
            playerKeyArray[i] = generalKeyArray[i];
            if (arraylength % 2 == 1)
            {
                GameObject generatePlayerGameobject = null;
                if (generalKeyArray[i] == 1)
                {
                    Destroy(generateGeneralGameObjectArray[i]);
                    generatePlayerGameobject = Instantiate(UpArrowPink, new Vector3(oddNumberArray[o + i], y, 0), Quaternion.identity);
                    generatePlayerGameobject.transform.SetParent(canvas.transform, false);
                    generatePlayerGameObjectArray[i] = generatePlayerGameobject;
                }
                else if (generalKeyArray[i] == 2)
                {
                    Destroy(generateGeneralGameObjectArray[i]);
                    generatePlayerGameobject = Instantiate(DownArrowPink, new Vector3(oddNumberArray[o + i], y, 0), Quaternion.identity);
                    generatePlayerGameobject.transform.SetParent(canvas.transform, false);
                    generatePlayerGameObjectArray[i] = generatePlayerGameobject;
                }
                else if (generalKeyArray[i] == 3)
                {
                    Destroy(generateGeneralGameObjectArray[i]);
                    generatePlayerGameobject = Instantiate(RightArrowPink, new Vector3(oddNumberArray[o + i], y, 0), Quaternion.identity);
                    generatePlayerGameobject.transform.SetParent(canvas.transform, false);
                    generatePlayerGameObjectArray[i] = generatePlayerGameobject;
                }
                else if (generalKeyArray[i] == 4)
                {
                    Destroy(generateGeneralGameObjectArray[i]);
                    generatePlayerGameobject = Instantiate(LeftArrowPink, new Vector3(oddNumberArray[o + i], y, 0), Quaternion.identity);
                    generatePlayerGameobject.transform.SetParent(canvas.transform, false);
                    generatePlayerGameObjectArray[i] = generatePlayerGameobject;
                }
                else if (generalKeyArray[i] == 5)
                {
                    Destroy(generateGeneralGameObjectArray[i]);
                    generatePlayerGameobject = Instantiate(FourColorKeyNormal, new Vector3(oddNumberArray[o + i], y, 0), Quaternion.identity);
                    generatePlayerGameobject.transform.SetParent(canvas.transform, false);
                    generatePlayerGameObjectArray[i] = generatePlayerGameobject;
                }
            }

            if (arraylength % 2 == 0)
            {
                GameObject generatePlayerGameobject = null;
                if (generalKeyArray[i] == 1)
                {
                    Destroy(generateGeneralGameObjectArray[i]);
                    generatePlayerGameobject = Instantiate(UpArrowPink, new Vector3(evenNumberArray[e + i], y, 0), Quaternion.identity);
                    generatePlayerGameobject.transform.SetParent(canvas.transform, false);
                    generatePlayerGameObjectArray[i] = generatePlayerGameobject;
                }
                else if (generalKeyArray[i] == 2)
                {
                    Destroy(generateGeneralGameObjectArray[i]);
                    generatePlayerGameobject = Instantiate(DownArrowPink, new Vector3(evenNumberArray[e + i], y, 0), Quaternion.identity);
                    generatePlayerGameobject.transform.SetParent(canvas.transform, false);
                    generatePlayerGameObjectArray[i] = generatePlayerGameobject;
                }
                else if (generalKeyArray[i] == 3)
                {
                    Destroy(generateGeneralGameObjectArray[i]);
                    generatePlayerGameobject = Instantiate(RightArrowPink, new Vector3(evenNumberArray[e + i], y, 0), Quaternion.identity);
                    generatePlayerGameobject.transform.SetParent(canvas.transform, false);
                    generatePlayerGameObjectArray[i] = generatePlayerGameobject;
                }
                else if (generalKeyArray[i] == 4)
                {
                    Destroy(generateGeneralGameObjectArray[i]);
                    generatePlayerGameobject = Instantiate(LeftArrowPink, new Vector3(evenNumberArray[e + i], y, 0), Quaternion.identity);
                    generatePlayerGameobject.transform.SetParent(canvas.transform, false);
                    generatePlayerGameObjectArray[i] = generatePlayerGameobject;
                }
                else if (generalKeyArray[i] == 5)
                {
                    Destroy(generateGeneralGameObjectArray[i]);
                    generatePlayerGameobject = Instantiate(FourColorKeyNormal, new Vector3(evenNumberArray[e + i], y, 0), Quaternion.identity);
                    generatePlayerGameobject.transform.SetParent(canvas.transform, false);
                    generatePlayerGameObjectArray[i] = generatePlayerGameobject;
                }
            }
        }
    }

    public IEnumerator Animation()
    {
        if (judge > arraylength / 3)
        {
            for (int i = 0; i < arraylength; i++)
            {
                if (playerKeyArray[i] == 1)
                {
                    anim.SetInteger("state", 1);
                    yield return new WaitForSeconds(0.3f);
                    audioSource.PlayOneShot(hitRobotSound1);
                }
                if (playerKeyArray[i] == 2)
                {
                    anim.SetInteger("state", 2);
                    yield return new WaitForSeconds(0.3f);
                    audioSource.PlayOneShot(hitRobotSound2);
                }
                if (playerKeyArray[i] == 3)
                {
                    anim.SetInteger("state", 3);
                    yield return new WaitForSeconds(0.3f);
                    audioSource.PlayOneShot(hitRobotSound3);
                }
                if (playerKeyArray[i] == 4)
                {
                    anim.SetInteger("state", 3);
                    yield return new WaitForSeconds(0.3f);
                    audioSource.PlayOneShot(hitRobotSound4);
                }
                //yield return new WaitForSeconds(0.3f);
            }
        }
        robot1anim.SetInteger("robot1state", 1);


        UIReset();
        Invoke("Jumpin", 2.0f);
        Invoke("OneTurnSwitcher", 3f);
        anim.SetInteger("state", 0);
        Judge();
    }

    public IEnumerator GameOver()
    {
        UIReset();
        gameOverText.text = "GAME OVER";
        yield return new WaitForSeconds(2.0f);

        SceneManager.LoadScene("Result");
    }

    public void Jumpin()
    {
        robot1anim.SetInteger("robot1state", 2);
    }

    public IEnumerator CountDown()
    {
        countDownText.text = "5";
        yield return new WaitForSeconds(1.0f);

        countDownText.text = "4";
        yield return new WaitForSeconds(1.0f);

        countDownText.text = "3";
        yield return new WaitForSeconds(1.0f);

        countDownText.text = "2";
        yield return new WaitForSeconds(1.0f);

        countDownText.text = "1";
        yield return new WaitForSeconds(1.0f);

        countDownText.text = "START";
        yield return new WaitForSeconds(1.0f);

        countDownText.text = "";

        isOneTurn = true;
        isGenerate = true;
    }
}
