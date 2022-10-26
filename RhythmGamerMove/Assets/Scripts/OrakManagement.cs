using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KindOfOrak
{
    Pump,
    Taego,
    UBeat,
    Ez2ac,
    Beaton,
    DDR,
    SoundVoltex,
    ReflectBeat,
    PopunMusic,
    Beatmania,
    Drummania,
    Gitapricks,
    Nosteljia,
    DRS,
    Maimai,
    WACCA,
    Chunithm,
    GrooveCoster
}

public class ArcadeCeneter
{
    private String _name;
    public String name { get { return _name; } }

    public Int32[] orakList = new Int32[18];
    public Station where = null;

    public ArcadeCeneter(String name)
    {
        for(Int32 i = 0; i < 18; i++)
        {
            orakList[i] = 0;
        }
        _name = name;
    }
}


public class OrakManagement : MonoBehaviour
{
    private static OrakManagement instance;
    public static OrakManagement Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new OrakManagement();
                return instance;
            }
            else
            {
                return instance;
            }
        }
    }

    public Dictionary<String, ArcadeCeneter> dic { get; private set; }
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        dic = new Dictionary<String, ArcadeCeneter>();
    }

    public ArcadeCeneter AddArcade(String key)
    {
        if (!dic.ContainsKey(key))
        {
            dic.Add(key, new ArcadeCeneter(key));
            return dic[key];
        }
        else
        {
            return dic[key];
        }
    }

    public Boolean AlreadyZonzae(String key)
    {
        if (dic.ContainsKey(key))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public ArcadeCeneter getArcade(String key)
    {
        if (!dic.ContainsKey(key))
        {
            Debug.Log("존재하지 않은 역입니다 : " + key);
            return null;
        }
        return dic[key];
    }
}
