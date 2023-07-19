using UnityEngine;

public class UiPlayButton : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.Instance.MoveToNextLevel();
    }
}
