using System;
using System.Collections.Generic;

public class SurealDB
{
    private List<PlayerData> SurealList = null;

    public SurealDB()
	{
        this.InitializeSurrealists();
	}

    public List<PlayerData> GetSurealList()
    {
        return SurealList;
    }

    private void InitializeSurrealists()
    {
        SurealList = new List<PlayerData>();
        PlayerData player1 = new PlayerData("Zlaxen", 150, 70, 20, 7, PlayerData.Status.Death);
        PlayerData player2 = new PlayerData("Lionss", 170, 65, 10, 3, PlayerData.Status.Death);
        PlayerData player3 = new PlayerData("Dodo", 550, 30, 220, 4, PlayerData.Status.Alive);
        PlayerData player4 = new PlayerData("FAngel", 65, 110, 130, 5, PlayerData.Status.Death);

        SurealList.Add(player1);
        SurealList.Add(player2);
        SurealList.Add(player3);
        SurealList.Add(player4);
    }
}
