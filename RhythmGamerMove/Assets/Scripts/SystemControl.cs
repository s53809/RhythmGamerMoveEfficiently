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

        Dijkstra.TamsekStart("���ϵ�");
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

        Debug.Log(sogae + "ȣ�� " + temp.name);
        Debug.Log("���� ����");

        for (Int32 i = 0; i < temp.adjNode.Count; i++)
        {
            Debug.Log(temp.adjNode[i].name);
        }

        for(Int32 i = 0; i < temp.transferList.Count; i++)
        {
            Debug.Log(temp.transferList[i].Item1 + "->" + temp.transferList[i].Item2 + " = " + temp.transferTime[i].minute + "��" +
                temp.transferTime[i].second + "�� �ɸ�");
        }

        if (temp.haveArcadeCeneter)
        {
            Debug.Log(temp.arcadeCenter.name + "�� �ְ� " + temp.gotoArcade.minute + "�� �ɸ�");
            Debug.Log(temp.arcadeCenter.name + "������ ������ " + temp.arcadeCenter.orakList[0] + "�� �ֽ��ϴ�");
        }

        /*Debug.Log( "���ϵ����� " + temp.name + "���� " + temp.minTime.minute + "��" + temp.minTime.second + "�� �ɸ��ϴ�.");

        Station curStation = temp;
        while (curStation != StationManagement.Instance.getStation("���ϵ�"))
        {
            Debug.Log(curStation.name);
            curStation = curStation.prevNode;
        }
        Debug.Log(curStation.name);*/
    }
}
