using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public static class StationCSVReader
{
    // Start is called before the first frame update
    public static void ReadStateData()
    {
        StreamReader sr = new StreamReader(Application.dataPath + "/Resources/CSV/StateData.csv");
        if (sr == null) { Debug.Log("ERROR of read State Data"); return; }
        while (true)
        {
            String str = sr.ReadLine();
            if (str == null)
            {
                break;
            }
            String[] data = str.Split(",");            
            Boolean isDirectGraph = false;
            if (data[0][0] == '!')
            {
                isDirectGraph = true;
                String temp = "";
                temp = temp + data[0][1];
                data[0] = temp;
            }
            Int32 line = Int32.Parse(data[0]);
            Station prevStat;
            if (StationManagement.Instance.AlreadyZonzae(data[1])) { prevStat = StationManagement.Instance.getStation(data[1]); }
            else { prevStat = StationManagement.Instance.AddStation(data[1]); }
            prevStat.AddLine(line);
            for (Int32 i = 2; i < data.Length; i++)
            {
                Station temp = StationManagement.Instance.AddStation(data[i]);
                if (temp != null)
                {
                    prevStat.AddAdjNode(temp);
                    temp.AddLine(line);
                    if (!isDirectGraph) temp.AddAdjNode(prevStat);
                    prevStat = temp;
                }
            }
        }
    }

    public static void ReadOrakData()
    {
        StreamReader sr = new StreamReader(Application.dataPath + "/Resources/CSV/OrakData.csv");
        if(sr == null) { Debug.Log("ERROR of read Orak Data"); return; }
        String str = sr.ReadLine();
        while (true)
        {
            str = sr.ReadLine();
            if (str == null)
            {
                break;
            }
            String[] data = str.Split(",");
            ArcadeCeneter temp = OrakManagement.Instance.AddArcade(data[0]);
            if (!StationManagement.Instance.AlreadyZonzae(data[2]))
            {
                Debug.Log(data[2] + "가 역data에 없대요 ㅅㅂ");
            }
            else
            {
                StationManagement.Instance.getStation(data[2]).AddArcade(temp, new TimeUnit(0, Int32.Parse(data[3])));
                temp.where = StationManagement.Instance.getStation(data[2]);
            }
            for(Int32 i = 4; i < 22; i++)
            {
                temp.orakList[i - 4] = Int32.Parse(data[i]);
            }
        }
    }

    public static void ReadTransferData()
    {
        StreamReader sr = new StreamReader(Application.dataPath + "/Resources/CSV/TransferData.csv");
        if (sr == null) { Debug.Log("ERROR of read Transfers Data"); return; }
        while (true)
        {
            String str = sr.ReadLine();
            if (str == null)
            {
                break;
            }
            String[] data = str.Split(",");
            Int32 line = Int32.Parse(data[1]);
            String num = "";
            num = num + data[3][0];
            Int32 endLine = Int32.Parse(num);
            TimeUnit time = new TimeUnit(Int32.Parse(data[5]), Int32.Parse(data[4]));
            if (StationManagement.Instance.AlreadyZonzae(data[2]))
            {
                StationManagement.Instance.getStation(data[2]).AddTransfer(line, endLine, time);
            }
            else
            {
                Debug.Log("아니 내가 환승 정보를 저장하려고 하는데 쟤가 " + data[2] + "역 정보가 없대요 ㅡㅡ");
            }
        }
    }
}
