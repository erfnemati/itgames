using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class scoreandtimerending : MonoBehaviour
{
    public float level1;
    public int totalscore;
    public Text scoretxt,timertxt;
    //InputField phonenumber;
    private string phonelist;
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {

        level1 = PlayerPrefs.GetFloat("level1timer");
        totalscore = PlayerPrefs.GetInt("totalstars");

        scoretxt.text = totalscore.ToString();
        timertxt.text = level1.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void savephone(string input)
    {

        phonelist = input;
        Debug.Log(phonelist);
        //i++;
  
    }
}
