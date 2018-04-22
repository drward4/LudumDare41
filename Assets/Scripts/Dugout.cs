using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dugout : MonoBehaviour
{
    public List<Transform> OnDeckPositions;
    private Queue<Player> Players;

    private void Awake()
    {
        this.Players = new Queue<Player>(3);
    }


    private void ResetPositions()
    {
        Player[] players = this.Players.ToArray();

        for (int i = 0; i < this.OnDeckPositions.Count; i++)
        {
            if (players.Length >= i + 1)
            {
                players[i].transform.SetParent(this.OnDeckPositions[i].transform, false);
            }
        }
    }


    public void PushPlayer(Player player)
    {
        this.Players.Enqueue(player);
        this.ResetPositions();
    }


    public Player PopPlayer()
    {
        Player player = null;

        if (this.Players.Count > 0)
        {
            player = this.Players.Dequeue();
        }

        return player;
    }
}
