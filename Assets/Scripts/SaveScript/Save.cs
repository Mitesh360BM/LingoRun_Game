using UnityEngine;

public class Save : MonoBehaviour
{
    public static Save instance;

    public static string TotalCoins="t_coins";
    public static string TotalScore="t_score";
    public static string magnetPower="Magnet";
    public static string sloMoPower= "Slow motion";
    public static string bikerPower="Bicycle";
    public static string hulkPower="Hulk";
    public static string skatePower = "Skateboard";
    public static string flyingPower = "Jet Plane";
    public static string ds_previousDate = "ds_previousDate";
    public static string ds_dailyStreak = "ds_dailyStreak";
    private void Awake()
    {
        if (instance != this)
        {
            instance = this;

        }
        else
        {
            Destroy(gameObject);

        }

       


    }

    //Basic Save Functions---Start
    public bool HasKey(string key)
    {
        return  PlayerPrefs.HasKey(key);
    }
    public void DeleteAllSave()
    {

         PlayerPrefs.DeleteAll();
       
    }

    public void SaveAll()
    {
         PlayerPrefs.Save();

    }

    //Basic Save Functions---End

    public void Set_Score(int score)
    {
        if (ServerInterface.instance.CheckInternt())
        {
            PlayerPrefs.SetInt("totalScore", score);
           
        }
        else
        {
            PlayerPrefs.SetInt("totalScore", score);
        }
    }

    public int Get_Score()
    {
       // Debug.Log( PlayerPrefs.GetInt("totalScore")+"Scoreeee");
        return  PlayerPrefs.GetInt("totalScore");

    }


    public void Set_Magnet_Info(int i)
    {
         PlayerPrefs.SetInt("totalMagnet", i);
     
    }

    public void Set_Fruit_Info(int i)
    {
         PlayerPrefs.SetInt("totalFruit", i);
       
    }

    public void Set_Multiplier_Info(int i)
    {
         PlayerPrefs.SetInt("totalscore2x", i);
       
    }

    
    public int Get_Magnet_Info()
    {
        return  PlayerPrefs.GetInt("totalMagnet");
    }

    public int Get_Fruit_Info()
    {
        return  PlayerPrefs.GetInt("totalFruit");

    }
    public int Get_Multiplier_Info()
    {
        return  PlayerPrefs.GetInt("totalscore2x");
    }

    public int GetInt(string key)
    {

        return PlayerPrefs.GetInt(key);
    
    }

    public void SetInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
    }

    public void SetString(string key, string value)
    {
        PlayerPrefs.SetString(key,value);
    
    }
    public string GetString(string key)
    {

        return PlayerPrefs.GetString(key);

    }
}
