using System;
using System.Collections.Generic;
using System.Text;

namespace FlappyBird.Shared
{
    [Serializable]
    public class SaveData
    {
        public int CurrentScore = 0;
        public int BestScore;
    }
}
