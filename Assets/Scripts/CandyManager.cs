using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyManager : MonoBehaviour
{
    const int DefaultCandyAmount = 30;
    const int RecoverySeconds = 10;
    //現在のキャンディのストック数
    public int candy = DefaultCandyAmount;

    //ストック回復までの残り秒数
    int counter;

    public void ConsumeCandy() {
        if (candy > 0) {
            candy--;
        }
    }

    public int GetCandyAmount() {
        return candy;
    }

    public void AddCandy(int amount) {
        candy += amount;
    }

    private void OnGUI() {
        GUI.color = Color.black;
        //キャンディのストックを表示
        //string label = "Candy" + candy;
        string label = $"Candy:{candy}";
        //回復している時だけ秒数を表示
        if (counter > 0) label = label + "(" + counter + "s)";
        GUI.Label(new Rect(50, 50, 100, 30), label);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //キャンディのストックがデフォルトより少なく、回復カウントをしていないときにカウンターをスタートさせる
        if (candy < DefaultCandyAmount && counter <= 0) {
            StartCoroutine(RecoverCandy());
        }
    }

    IEnumerator RecoverCandy() {
        counter = RecoverySeconds;
        //1秒ずつカウントを進める
        while (counter > 0) {
            yield return new WaitForSeconds(1.0f);
            counter--;
        }
        candy++;
    }
}
