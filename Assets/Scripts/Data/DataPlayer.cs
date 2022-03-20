using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Data
{
    [Serializable]
    public struct DataPlayer
    {
        public string nickname;
        public int score;

        public DataPlayer(string nickname, int score)
        {
            this.nickname = nickname;
            this.score = score;
        }
    }
}