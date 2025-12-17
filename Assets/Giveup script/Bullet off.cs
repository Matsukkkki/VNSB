using UnityEngine;

public class Bulletoff : MonoBehaviour
{
    // 衝突判定のためにコライダーが必要です（弾にカプセルコライダーが必要）
    void OnCollisionEnter(Collision collision)
    {
      
        // エネミータグと衝突した場合
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("エネミーに衝突しました！");

            // エネミーの現在のY座標を保持
            float currentY = collision.gameObject.transform.position.y;

            // XとZのランダムな±12f, ±7.5f の4パターンの位置をランダムに選択
            float randomX = Random.Range(0, 2) == 0 ? 25f : -25f;  // Xの値を±12fにランダムで設定
            float randomZ = Random.Range(0, 2) == 0 ? 20f : -20f;  // Zの値を±7.5fにランダムで設定

            // エネミーをカメラ外に送る（YはそのままでXとZを変更）
            Vector3 offscreenPosition = new Vector3(randomX, currentY, randomZ);

            // エネミーの位置をカメラ外に設定
            collision.gameObject.transform.position = offscreenPosition;

            // このオブジェクト（弾など）を削除
            Destroy(this.gameObject);
        }
    }
}
