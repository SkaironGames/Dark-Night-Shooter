using System.Collections;
using TMPro;
using UnityEngine;

public class BossComing : MonoBehaviour
{
    public static int ZombiePulseCounter = 0;

    public GameObject HealthScreen;
    public GameObject ZombieSp;
    public GameObject Boss;
    public TMP_Text KillText;
    public TMP_Text TotalKill;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt("ZombiePulseCounter", ZombiePulseCounter);
        TotalKill.text = PlayerPrefs.GetInt("ZombiePulseCounter").ToString();

        KillText.text = ZombiePulseCounter + " /50 : Kill".ToString();

        if (ZombiePulseCounter >= 50)
        {
            ZombieSp.SetActive(false);
            KillText.text = "WARNING: BOSS COMING".ToString();
            Invoke("BossCo", 5);
        }
    }

    void BossCo()
    {
        Boss.SetActive(true);
        HealthScreen.SetActive(true);
        KillText.enabled = false;
    }
}
