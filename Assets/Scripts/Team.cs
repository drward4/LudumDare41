using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team
{
    public List<Player> Players;
    public int AtBatIndex = 0;

    public Team()
    {
        this.Players = new List<Player>(4);
    }



}
