using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HPManager : MonoBehaviour
{
    public float playerHealth = 20f;
    public float playerDamage = 5f;

    public Text playerHealthText;   // HP表示
    public Text timerText;          // ★ タイマー表示

    public float timeLimit = 90f;   // ★ 制限時間（90秒）

    private bool isGameEnd = false; // ★ ゲーム終了判定

    private void Start()
    {
        UpdateHealthUI();
        UpdateTimerUI();
    }

    private void Update()
    {
        // ゲーム終了後は処理しない
        if (isGameEnd) return;

        // タイマー減少
        timeLimit -= Time.deltaTime;

        if (timeLimit <= 0)
        {
            timeLimit = 0;
            GameClear();
        }

        UpdateTimerUI();
    }

    // ダメージ処理
    public void TakeDamage(float damage)
    {
        if (isGameEnd) return;

        playerHealth -= damage;
        Debug.Log("プレイヤーがダメージを受けました！ 現在のHP: " + playerHealth);

        if (playerHealth <= 0)
        {
            playerHealth = 0;
            GameOver();
        }

        UpdateHealthUI();
    }

    // HP UI更新
    private void UpdateHealthUI()
    {
        if (playerHealthText != null)
        {
            playerHealthText.text = "HP: " + playerHealth.ToString();
        }
    }

    // ★ タイマーUI更新
    private void UpdateTimerUI()
    {
        if (timerText != null)
        {
            timerText.text = Mathf.Ceil(timeLimit).ToString();
        }
    }

    // エネミー接触
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(playerDamage);
        }
    }

    // ゲームオーバー
    private void GameOver()
    {
        isGameEnd = true;
        SceneManager.LoadScene("GameOver");
    }

    // ★ ゲームクリア
    private void GameClear()
    {
        isGameEnd = true;
        SceneManager.LoadScene("GameClear");
    }
}
