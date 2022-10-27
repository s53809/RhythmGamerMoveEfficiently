using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagement : MonoBehaviour
{
    [SerializeField] private InputField StateInput;
    [SerializeField] private GameObject State_FloatingUIBar;
    [SerializeField] private GameObject State_Button;
    [SerializeField] private GameObject Arcade_FloatingUIBar;
    [SerializeField] private GameObject Arcade_Button;
    [SerializeField] private GameObject View_Orak;
    private List<GameObject> buttons;
    private List<KindOfOrak> oraks;
    private List<GameObject> Arcade_Buttons;
    private Boolean isDijkstraStart = false;

    public String[] Arcade_list;
    public KindOfOrak[] Arcade_list_2;


    void Start()
    {
        View_Orak.SetActive(false);
        Arcade_list = new String[] { "펌프", "태고의 달인", "유비트", "이지투", "비트온", "DDR", "사운드볼텍스", "리플랙비트", "팝픈", "비트마니아", "드럼마니아", "기타프릭스", "노스텔지아", "댄스러쉬", "마이마이", "왓카", "츄니즘", "그루브코스터" };
        Arcade_list_2 = new KindOfOrak[] { KindOfOrak.Pump, KindOfOrak.Taego, KindOfOrak.UBeat, KindOfOrak.Ez2ac, KindOfOrak.Beaton, KindOfOrak.DDR, KindOfOrak.SoundVoltex, KindOfOrak.ReflectBeat, KindOfOrak.PopunMusic, KindOfOrak.Beatmania, KindOfOrak.Drummania, KindOfOrak.Gitapricks, KindOfOrak.Nosteljia, KindOfOrak.DRS, KindOfOrak.Maimai, KindOfOrak.WACCA, KindOfOrak.Chunithm, KindOfOrak.GrooveCoster };
        State_FloatingUIBar.SetActive(false);
        buttons = new List<GameObject>();
        Arcade_Buttons = new List<GameObject>();
        oraks = new List<KindOfOrak>();
        for(Int32 i = 0; i < Arcade_list.Length; i++)
        {
            GameObject temp = Instantiate(Arcade_Button);
            temp.transform.parent = Arcade_FloatingUIBar.transform.GetChild(0).GetChild(0);
            temp.GetComponent<StateButtonEditor>().RefreshButton(Arcade_list[i]);
            String tmp = Arcade_list[i];
            temp.GetComponent<Button>().onClick.AddListener(() => OnClickArcadeButton(tmp));
            Arcade_Buttons.Add(temp);
        }
    }

    
    void Update()
    {
        
    }

    public void OnStateInputChange()
    {
        State_FloatingUIBar.SetActive(true);
        for(Int32 i = 0; i < buttons.Count; i++)
        {
            Destroy(buttons[i]);
        }
        buttons.Clear();
        foreach (KeyValuePair<String, Station> kvp in StationManagement.Instance.dic)
        {
            if (kvp.Value.name.Contains(StateInput.text))
            {
                GameObject but = Instantiate(State_Button);
                but.transform.parent = State_FloatingUIBar.transform.GetChild(0).GetChild(0);
                but.GetComponent<Button>().onClick.AddListener(() => OnClickStateButton(kvp.Value.name));
                but.GetComponent<StateButtonEditor>().RefreshButton(kvp.Value.name);
                buttons.Add(but);
            }
        }
    }

    public void OnStateInputExit()
    {
        State_FloatingUIBar.SetActive(false);
    }

    public void OnClickStateButton(String name)
    {
        isDijkstraStart = true;
        Dijkstra.TamsekStart(name);
        StateInput.text = name;
        OnStateInputExit();
    }

    public void OnClickArcadeButton(String name)
    {
        KindOfOrak temp = new KindOfOrak();
        Boolean isNotBreak = true;
        Int32 N = 0;
        for(Int32 i = 0; i < Arcade_list.Length; i++)
        {
            if(name == Arcade_list[i])
            {
                isNotBreak = false;
                temp = Arcade_list_2[i];
                N = i;
                break;
            }
        }
        if (isNotBreak) { Debug.Log(name + "did not found in button"); return; }
        if (oraks.Contains(temp))
        {
            Color color;
            if (ColorUtility.TryParseHtmlString("#879FED", out color))
            { Arcade_Buttons[N].GetComponent<Image>().color = color; }
            oraks.Remove(temp);
        }
        else
        {
            Color color;
            if (ColorUtility.TryParseHtmlString("#2080E1", out color))
            { Arcade_Buttons[N].GetComponent<Image>().color = color; }
            oraks.Add(temp);
        }
    }

    public void OnFindButtonClick()
    {
        if(oraks.Count == 0)
        {
            Debug.Log("orak not found");
            return;
        }
        if (!isDijkstraStart)
        {
            Debug.Log("station not found");
            return;
        }
        Dijkstra.SearchArcade(oraks);
        Debug.Log(Dijkstra.goalStation.name);
        Debug.Log(Dijkstra.goalCenter.name);
        List<Station> path = Dijkstra.ViewPath();
        View_Orak.SetActive(true);
        View_Orak.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text =
            Dijkstra.goalCenter.name + " | " + Dijkstra.goalStation.name;
        View_Orak.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "";
        for (Int32 i = path.Count - 1; i >= 0; i--)
        {
            View_Orak.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Text>().text += path[i].name + " ";
        }
        View_Orak.transform.GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Text>().text = "";
        for (Int32 i = 0; i < Dijkstra.goalCenter.orakList.Length; i++)
        {
            if(Dijkstra.goalCenter.orakList[i] >= 1)
            {
                View_Orak.transform.GetChild(3).GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Text>().text +=
                    Arcade_list[i] + " " + Dijkstra.goalCenter.orakList[i] + "대 ";
            }
        }
    }
}
