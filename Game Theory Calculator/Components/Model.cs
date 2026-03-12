using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_Theory_Calculator.Models
{
    public class Model
    {
        protected float masterX;
        protected float masterY;
        protected string[] players;
        protected string name;
        protected int ID;
        protected string defaultName = "Default Name";

        public string[] GetPlayers()
        {
            return players;
        }

        public string GetOnePlayer(int index)
        {
            return players[index];
        }

        public void SetPlayers(string[] Players)
        {
            players = Players;
        }

        public void SetOnePlayer(int index, string Player)
        {
            players[index] = Player;
        }
        public string GetName()
        {
            return name;
        }

        public void SetName(string Name)
        {
            name = Name;
        }

        public int GetID()
        {
            return ID;
        }

        public void SetID(int ID)
        {
            this.ID = ID;
        }
    }
}
