using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class BulletManager : MonoBehaviour
{
    public GameObject bulletPrefab;         // ボールのプレハブ
    public Transform[] enemies;             // 複数のエネミーのTransform
    public float spawnInterval = 1.5f;      // ボールの生成間隔（発射間隔）
    private float spawnTimer = 0f;          // タイマー

    private List<GameObject> bullets = new List<GameObject>();  // 現在発射されている弾のリスト
    private int maxBulletCount = 2;         // 最大弾数（2つまで）


    void Update()
    {
        // ボールの生成
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnInterval)
        {
            SpawnBullet();
            spawnTimer = 0f;  // タイマーリセット
        }
    }

    // ボールの発射
    void SpawnBullet()
    {
        if (bulletPrefab && enemies.Length > 0)
        {
            // ランダムにエネミーを選択
            Transform targetEnemy = enemies[Random.Range(0, enemies.Length)];

            // 新しい弾を生成
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.tag = "Bullet";  // 弾にBulletタグを設定

            // エネミーの方向を計算
            Vector3 direction = (targetEnemy.position - bullet.transform.position).normalized;
            bullet.transform.forward = direction;  // 弾をエネミーに向ける

            // 弾に速度を与える（進む方向に対して移動）
            bullet.GetComponent<Rigidbody>().linearVelocity = direction * 10f;  // 弾の移動速度（例: 10f）

            bullets.Add(bullet);  // 発射された弾をリストに追加

            // 弾が最大数に達した場合、最初の弾を削除してから新しい弾を発射
            if (bullets.Count > maxBulletCount)
            {
                DestroyBullet(bullets[0]);  // 最初の弾を削除
                bullets.RemoveAt(0);        // リストから最初の弾を削除
            }
        }
    }

    // 弾の削除処理
    public void DestroyBullet(GameObject bullet)
    {
        if (bullet != null)
        {
            Destroy(bullet);         // ゲームオブジェクトを削除
        }
    }

    // 衝突判定：エネミーと接触した場合
    void OnCollisionEnter(Collision collision)
    {
        // 衝突したオブジェクトの名前とタグをログに出力
        Debug.Log("衝突オブジェクト: " + collision.gameObject.name + ", タグ: " + collision.gameObject.tag);

        // エネミーと衝突した場合
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // エネミーをカメラ外に移動させる（オフスクリーンに送る）
            Vector3 offscreenPosition = new Vector3(Random.Range(25f, 50f), collision.gameObject.transform.position.y, Random.Range(20f, 40f));
            collision.gameObject.transform.position = offscreenPosition;

            // 弾を削除
            DestroyBullet(collision.gameObject);  // 衝突した弾を削除
        }
    }
}
