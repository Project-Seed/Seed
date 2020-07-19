using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class New_world : MonoBehaviour
{
    string now_map = "";

    public Image me;
    public GameObject line;
    public Text text;

    private void Awake()
    {
        me.color = new Color(1, 1, 1, 0);
        line.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 3);
        text.color = new Color(1, 1, 1, 0);
    }

    private void Update()
    { 
        if(now_map != GameSystem.instance.map_name)
        {
            StartCoroutine(view());

            switch(GameSystem.instance.map_name)
            {
                case "TM11.FaroffLandmass2":
                    text.text = "머나먼 섬";
                    PlayerState.instance.radiation_level = 1;
                    break;

                case "TM11.Starsand":
                    text.text = "별 모래";
                    PlayerState.instance.radiation_level = 2;
                    break;

                case "TM11_DreamBeach":
                    text.text = "꿈꾸는 해변";
                    PlayerState.instance.radiation_level = 1;
                    break;

                case "TM11_FaroffLandmass":
                    text.text = "머나먼 섬";
                    PlayerState.instance.radiation_level = 2;
                    break;

                case "TM11_lonelyIsland":
                    text.text = "외로운 섬";
                    PlayerState.instance.radiation_level = 1;
                    break;

                case "TM11_StartValley":
                    text.text = "시작의 협곡";
                    PlayerState.instance.radiation_level = 0;
                    break;


                case "TM12.DreamBeach2.001":
                    text.text = "꿈꾸는 해변";
                    PlayerState.instance.radiation_level = 1;
                    break;

                case "TM12.Dryfork":
                    text.text = "말라붙은 갈림길";
                    PlayerState.instance.radiation_level = 2;
                    break;

                case "TM12.MineEntrance":
                    text.text = "사막 입구";
                    PlayerState.instance.radiation_level = 2;
                    break;

                case "TM12.MissingHill":
                    text.text = "잃어버린 언덕";
                    PlayerState.instance.radiation_level = 1;
                    break;

                case "TM12.NextValley":
                    text.text = "내일의 협곡";
                    PlayerState.instance.radiation_level = 1;
                    break;

                case "TM12.ThreelineValley_Down":
                    text.text = "삼선 협곡 하류";
                    PlayerState.instance.radiation_level = 2;
                    break;

                case "TM12.ThreelineValley_Up":
                    text.text = "삼선 협곡 상류";
                    PlayerState.instance.radiation_level = 2;
                    break;

                case "TM12_Starsand2":
                    text.text = "별 모래";
                    PlayerState.instance.radiation_level = 1;
                    break;


                case "TM13.BearsSole":
                    text.text = "곰 발바닥";
                    PlayerState.instance.radiation_level = 2;
                    break;

                case "TM13.EndHighVelly":
                    text.text = "높은 계곡 끝자락";
                    PlayerState.instance.radiation_level = 2;
                    break;

                case "TM13.EntranceHighVelly":
                    text.text = "높은 계곡 입구";
                    PlayerState.instance.radiation_level = 2;
                    break;

                case "TM13.MiddleHighVelly":
                    text.text = "높은 계곡 중간지점";
                    PlayerState.instance.radiation_level = 2;
                    break;

                case "TM13.SideBearsSole":
                    text.text = "곰 발바닥 옆";
                    PlayerState.instance.radiation_level = 2;
                    break;

                case "TM13.StartHighVelly":
                    text.text = "높은 계곡 출발점";
                    PlayerState.instance.radiation_level = 2;
                    break;


                case "TM15.DiamondMine":
                    text.text = "다이아몬드 광산";
                    PlayerState.instance.radiation_level = 3;
                    break;

                case "TM15.EndMine":
                    text.text = "광산 끝자락";
                    PlayerState.instance.radiation_level = 2;
                    break;

                case "TM15.MiddleMine":
                    text.text = "광산 중간지점";
                    PlayerState.instance.radiation_level = 2;
                    break;

                case "TM15.MineShortcut":
                    text.text = "광산 지름길";
                    PlayerState.instance.radiation_level = 2;
                    break;

                case "TM15.NewMine":
                    text.text = "새로운 광산";
                    PlayerState.instance.radiation_level = 2;
                    break;

                case "TM15.SideMine":
                    text.text = "광산 옆";
                    PlayerState.instance.radiation_level = 3;
                    break;

                case "TM15.TwinsMine":
                    text.text = "쌍둥이 광산";
                    PlayerState.instance.radiation_level = 3;
                    break;


                case "TM14.EngineersSea":
                    text.text = "공학자의 바다";
                    PlayerState.instance.radiation_level = 1;
                    break;

                case "TM14.EngineersVillage":
                    text.text = "공학자의 마을";
                    PlayerState.instance.radiation_level = 0;
                    break;

                case "TM14.EngineersWarehouse":
                    text.text = "공학자의 창고";
                    PlayerState.instance.radiation_level = 2;
                    break;

                case "TM14.ExitMine":
                    text.text = "광산 출구";
                    PlayerState.instance.radiation_level = 2;
                    break;

                case "TM14.MountainBehind":
                    text.text = "산 뒷 편";
                    PlayerState.instance.radiation_level = 2;
                    break;

                case "TM14.RapidStream":
                    text.text = "거센 강";
                    PlayerState.instance.radiation_level = 2;
                    break;

                case "TM14.UpperMountain":
                    text.text = "산 윗 자락";
                    PlayerState.instance.radiation_level = 2;
                    break;
            }

            now_map = GameSystem.instance.map_name;
        }
    }

    IEnumerator view()
    {
        for (int i = 0; i <= 200; i++)
        {
            if (i <= 20)
                me.color = new Color(1, 1, 1, i / 20f);
            else if (i <= 50)
            {
                line.GetComponent<RectTransform>().sizeDelta = new Vector2((i - 20) / 30f * 622f, 3);
                text.color = new Color(1, 1, 1, (i - 20) / 30f);
            }
            else if (i >= 170)
            {
                me.color = new Color(1, 1, 1, (200 - i) / 30f);
                line.GetComponent<RectTransform>().sizeDelta = new Vector2((200 - i) / 30f * 622f, 3);
                text.color = new Color(1, 1, 1, (200 - i) / 30f);
            }

            yield return new WaitForSeconds(0.01f);
        }
    }
}
