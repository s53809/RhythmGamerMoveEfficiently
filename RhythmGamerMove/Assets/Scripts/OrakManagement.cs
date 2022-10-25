using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KindOfOrak
{
    Pump,
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

    private List<KindOfOrak> orakList;
}


public class OrakManagement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
