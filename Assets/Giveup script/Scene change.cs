using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    // Inspector（ヒエラルキー）で設定するシーン名
    [SerializeField] private string sceneName;

    // UIボタンから呼ばれる
    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
