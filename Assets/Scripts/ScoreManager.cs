using UnityEngine;
using UnityEngine.UI;

namespace WhoaAlgebraic
{
    public class ScoreManager : MonoBehaviour
    {
        public static int score;        // The player's score.


        public Text text;                      // Reference to the Text component.


        void Awake()
        {
            // Reset the score.
            score = 0;
        }


        void Update()
        {
            // Set the displayed text to be the word "Score" followed by the score value.
            text.text = "Score: " + score;
        }
    }
}