using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public GameObject[] candyPrefabs;
    public Transform candyParentTransform; //親子関係を結ぶときにtransformの参照が必要なため
    public float shotForce;
    public float shortTorque;
    public float baseWidth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Shot() {
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
