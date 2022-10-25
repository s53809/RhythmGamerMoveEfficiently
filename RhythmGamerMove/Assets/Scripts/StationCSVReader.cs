using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class StationCSVReader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StreamReader sr = new StreamReader(Application.dataPath + "/Resources/CSV/StateData.csv");
        while (true)
        {
            String str = sr.ReadLine();
            if(str == null)
            {
                break;
            }
            String[] data = str.Split(",");
            Int32 line = Int32.Parse(data[0]);
            Station prevStat = StationManagement.Instance.AddStation(data[1]);
            prevStat.AddLine(line);
            for(Int32 i = 2; i < data.Length; i++)
            {
                prevStat.AddAdjNode();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
