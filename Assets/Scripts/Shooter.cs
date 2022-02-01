using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    const int MaxShotPower = 5;
    const int RecoverySeconds = 3;
    int shotPower = MaxShotPower;
    public GameObject[] candyPrefabs;
    public Transform candyParentTransform; //親子関係を結ぶときにtransformの参照が必要なため
    public CandyManager candyManager;
    public float shotForce;
    public float shortTorque;
    public float baseWidth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Shot() {
        //キャンディを生成できる条件外ならばShotしない
        if (candyManager.GetCandyAmount() <= 0) {
            return;
        }
        if (shotPower <= 0) {
            return;
        }
        //プレハブからCandyオブジェクトを生成
        GameObject candy = (GameObject) Instantiate(
            SampleCandy(),
            GetInstantiatePosition(),
            Quaternion.identity
            );
        //生成したCandyオブジェクトの親をcandyParentTransformに設定する
        candy.transform.parent = candyParentTransform;
        //GameObject型の場合、=candyParentTransform.transform となる
        //CandyオブジェクトのRigidbodyを取得し力と回転を加える
        Rigidbody candyRigidbody = candy.GetComponent<Rigidbody>();
        candyRigidbody.AddForce(transform.forward * shotForce);
        candyRigidbody.AddTorque(new Vector3(0, shortTorque, 0));
        //キャンディのストックを消費
        candyManager.ConsumeCandy();
        //ShotPowerを消費
        ConsumePower();
    }

    private void OnGUI() {
        GUI.color = Color.black;
        //ShotPowerの残数を+の数で表示
        string label = "";
        for (int i = 0; i < shotPower; i++) {
            label = label + "+";
            GUI.Label(new Rect(50, 65, 100, 30), label);
        }
    }

    void ConsumePower() {
        //ShotPowerを消費すると同時に回復のカウントをスタート
        shotPower--;
        StartCoroutine(RecoverPower());
    }

    IEnumerator RecoverPower() {
        //一定秒数待った後にhotPowerを回復
        yield return new WaitForSeconds(RecoverySeconds);
        shotPower++;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) Shot();
    }

    //キャンディのプレハブからランダムに1つ選ぶメソッド(戻り値はGameObject型)
    GameObject SampleCandy() {
        int index = Random.Range(0, candyPrefabs.Length);
        return candyPrefabs[index];
    }

    Vector3 GetInstantiatePosition() {
        //画面のサイズとInputの割合からキャンディ生成のポジションを計算
        float x = baseWidth * (Input.mousePosition.x / Screen.width) - (baseWidth / 2);
        return transform.position + new Vector3(x, 0, 0);
    }
}
