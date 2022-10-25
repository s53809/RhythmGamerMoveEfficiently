using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Station
{
    public List<Int32> line;
    public List<Station> adjNode;
    public List<Tuple<Int32, Int32>> transferTime;
    public Boolean haveArcadeCeneter;
    public ArcadeCeneter arcadeCenter;
}
public class StationManagement : MonoBehaviour
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
