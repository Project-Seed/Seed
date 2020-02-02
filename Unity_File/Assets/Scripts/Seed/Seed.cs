using System;
using System.Collections.Generic;

class Seed
{
    private string Name { get => Name; }   
    private string Desc { get => Desc; }  //설명
    private SeedType seedType { get => seedType; } //씨앗타입. 설치형 or 확장형
    private InstallType installType { get => installType; } //설치장소. 벽 or 바닥
    private GrowStates growState { get => growState; set => growState = value; } //성장 단계
    private PreferredEnviroment enviroment { get => enviroment; } //선호 환경. flag

    protected struct SeedModelData
    {
        private object model;
    }
    SeedModelData seedModelData;

    enum SeedType
    {
        install, expend
    }
    enum InstallType
    {
        wall, ground
    }
    enum GrowStates
    {
        install, growing, dead
    }
    public void SetGrowState(int state)
    {
        switch (state)
        {
            case 0:
                growState = GrowStates.install;
                break;
            case 1:
                growState = GrowStates.growing;
                break;
            case 2:
                growState = GrowStates.dead;
                break;
        }
    }

    [Flags]
    enum PreferredEnviroment
    {
        basic,
        forest,
        sea,
        sand
    }

    struct GrowInfo
    {
        float growthRate { get; set; }   //성장속도
        float force { get; }        //자라는 힘
    }
    

}
