using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class General : MonoBehaviour
{
    [SerializeField]
    DB db; // DataBase Refrence
    int num1, num2, result;
    public GameObject Game, UI, Lose_txt, SL;
    public Text Round, Result, Score_txt, Rank_Num;
    public Slider Timer;
    int Score_int = 0;
    float Time_ = 10f;
    bool True_False, isplaying = false;
    void Start()
    { 
        SL.GetComponent<SL>().Load();
        UpdateRank();//Update Rank    
       
    }
    void Update()
    {
        Score_txt.text = Score_int.ToString();
        if (isplaying)//if isplaying is true Start Timer
        {
            Time_ -= Time.deltaTime;
            Timer.value = Time_;
        }
        if (Time_ < +0f)//Lose
        {
            UI.SetActive(true);
            Game.SetActive(false);
            Lose_txt.SetActive(true);
            UpdateRank();
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            SL.GetComponent<SL>().Save();
            Application.Quit();
        }   
    }
    //Generating each round
    public void round()
    {

        Game.SetActive(true);
        
        UI.SetActive(false);
        isplaying = true;
        int operators = Random.Range(1, 5);
        
        True_False = RandomBool();
        string op="";
        
 
        if (operators == 1)
        {
            op = " + ";
            num1 = Random.Range(1, 100);
            num2 = Random.Range(1, 100);
            result = num1 + num2;
        }
        else if (operators == 2)
        {
            op = " - ";
            num1 = Random.Range(1, 100);
            num2 = Random.Range(1, 100);
            result = num1 - num2;
        }
        else if (operators == 3)
        {
            op = " * ";
            num1 = Random.Range(1, 20);
            num2 = Random.Range(1, 20);
            result = num1 * num2;
        }
        else if (operators == 4)
        {
            op = " / ";

            do
            {
                num1 = Random.Range(1, 20);
                num2 = Random.Range(1, 20);
                if (num1 % num2 == 0)
                {
                    result = num1 / num2;
                    break;
                }

            } while (true);
        }
        Round.text = num1 + op + num2 + " =";
        TRUE_FALSE();
    }
    void TRUE_FALSE()
    {
        
        if (True_False)
        {
            Result.text = result.ToString();
        }
        else
        {
            int rand = Random.Range(-20, 20);
            result += rand;
            Result.text = result + "";
        }

    }
    //Check if the answer is true add score
    public void Score(Button button)
    {
        if (button.name == "True" && True_False)
        {
            if (Time_ + 3 >= 10)
                Time_ = 10f;
            else
                Time_ += 3f;
            Score_int++;
        }
        else if (button.name == "False" && !True_False)
        {
            Score_int++;
            if (Time_ + 3 >= 10)
                Time_ = 10f;
            else
                Time_ += 3f;
        }
        else
        {
            Time_ -= 3f;
        }

        round();
    }
    //Generate random bool
    bool RandomBool()
    {
        if (Random.value >= 0.5)
        {
            return true;
        }
            return false;
    }
    //Update rank
    //Check if score is higher than rank update db.Rank
    void UpdateRank()
    {
        if (Score_int > db.Rank)
            db.Rank = Score_int;
        Score_int = 0;
        Rank_Num.text = db.Rank.ToString();
       
        Time_ = 10f;
    }
}
