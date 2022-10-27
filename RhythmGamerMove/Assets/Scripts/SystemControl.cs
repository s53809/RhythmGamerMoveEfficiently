using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemControl : MonoBehaviour
{
    public List<KindOfOrak> myWishList;

    private void Awake()
    {
        myWishList = new List<KindOfOrak>();
    }
    void Start()
    {
        StationCSVReader.ReadStateData();
        StationCSVReader.ReadTransferData();
        StationCSVReader.ReadOrakData();

        myWishList.Add(KindOfOrak.DRS);
        myWishList.Add(KindOfOrak.SoundVoltex);

        Dijkstra.TamsekStart("상일동");
        Dijkstra.SearchArcade(myWishList);
        
    }

    
    void Update()
    {
        
    }

    public void DebugViewStationInfo(String key)
    {
        Station temp = StationManagement.Instance.getStation(key);

        String sogae = "";

        for (Int32 i = 0; i < temp.line.Count; i++)
        {
            sogae += temp.line[i].ToString();
            sogae += " ";
        }

        Debug.Log(sogae + "호선 " + temp.name);
        Debug.Log("인접 역은");

        for (Int32 i = 0; i < temp.adjNode.Count; i++)
        {
            Debug.Log(temp.adjNode[i].name);
        }

        for(Int32 i = 0; i < temp.transferList.Count; i++)
        {
            Debug.Log(temp.transferList[i].Item1 + "->" + temp.transferList[i].Item2 + " = " + temp.transferTime[i].minute + "분" +
                temp.transferTime[i].second + "초 걸림");
        }

        if (temp.haveArcadeCeneter)
        {
            Debug.Log(temp.arcadeCenter.name + "이 있고 " + temp.gotoArcade.minute + "분 걸림");
            Debug.Log(temp.arcadeCenter.name + "점에는 펌프가 " + temp.arcadeCenter.orakList[0] + "개 있습니다");
        }

        /*Debug.Log( "상일동에서 " + temp.name + "까지 " + temp.minTime.minute + "분" + temp.minTime.second + "초 걸립니다.");

        Station curStation = temp;
        while (curStation != StationManagement.Instance.getStation("상일동"))
        {
            Debug.Log(curStation.name);
            curStation = curStation.prevNode;
        }
        Debug.Log(curStation.name);*/
    }
}
