using Assets.Scripts.UI;
using Photon.Realtime;
using Hashtable = ExitGames.Client.Photon.Hashtable;

namespace Assets.Scripts.Data
{
    public class ScoreController : GameManager<ScoreController>
    {
        public void UpdateScore(int score, Player player)
        {
            object value;
            player.CustomProperties.TryGetValue("Score", out value);

            Hashtable hashtable = new Hashtable();
            hashtable.Add("Score", (int)value + score);

            player.SetCustomProperties(hashtable);
            UIManager.Instance.uiGame.UpdateScore((int)value + score);
        }

        public int GetScore(Player player)
        {
            object value;
            player.CustomProperties.TryGetValue("Score", out value);
            return (int)value;
        }
    }
}