using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class test : MonoBehaviour
{
    public Rigidbody rb;

    int syoutotu = 0;//アームと景品が衝突したかどうか
    int count = 0;//試行回数
    float[] array = new float[9];//景品口との距離を記録する配列

    //int bunkatu = 3;//分割数
    //int i = 0;
    //int j = 0;

    float keihinx=0;//景品の初期x座標
    float keihiny = 0;//景品の初期y座標
    float keihinz = 0;//景品の初期z座標
    float angkeihinx = 0;//景品の初期x角度
    float angkeihiny = 0;//景品の初期y角度
    float angkeihinz = 0;//景品の初期z角度
    float scalekeihinx = 0;//景品のxサイズ
    float scalekeihiny = 0;//景品のyサイズ
    float scalekeihinz = 0;//景品のzサイズ

    //ゲームオブジェクトを呼び出す
    [SerializeField] private GameObject arm;
    [SerializeField] private GameObject keihin;
    [SerializeField] private GameObject ana;
    [SerializeField] private GameObject seikai;


    void Start()
    {
        Time.timeScale = 60.0f;//倍速にする

        Transform keihinTransform = this.transform;//景品の位置，角度，サイズを取得
        //景品の初期位置
        Vector3 keihinworldPos = keihinTransform.position;
        keihinx = keihinworldPos.x;
        keihiny=keihinworldPos.y;
        keihinz=keihinworldPos.z;
        //景品の初期角度
        Vector3 keihinworldAngle = keihinTransform.eulerAngles;
        angkeihinx=keihinworldAngle.x;
        angkeihiny=keihinworldAngle.y;
        angkeihinz=keihinworldAngle.z;
        //景品のサイズ
        Vector3 keihinlocalScale = keihinTransform.localScale;
        scalekeihinx=keihinlocalScale.x; 
        scalekeihiny=keihinlocalScale.y; 
        scalekeihinz=keihinlocalScale.z; 
                                   
    }

    void Update()
     {

     }
   
   void FixedUpdate()
    {
        if (syoutotu == 1)//衝突判定が1のとき（衝突した後）
        {
            if (rb.IsSleeping())//景品が静止したとき
            {
                syoutotu = 0;//衝突判定を0に戻しておく

                Vector3 targetPos = keihin.transform.position;//現在の景品の位置
                Vector3 anaPos = ana.transform.position;//獲得口の位置     
                float dis = Vector3.Distance(targetPos, anaPos);//景品と獲得口の距離を計算
                array[count] = dis;//配列に記録     
                print("dis[" + count + "]=" + array[count]);//距離を表示

                count++;//試行回数
                

                Transform keihinTransform = this.transform;//景品の情報を取得
                //景品を初期位置に戻す
                Vector3 keihinworldPos = keihinTransform.position;
                keihinworldPos.x = keihinx;
                keihinworldPos.y = keihiny;
                keihinworldPos.z = keihinz;
                keihinTransform.position = keihinworldPos;
                //景品を初期角度に戻す
                Vector3 keihinworldAngle = keihinTransform.eulerAngles;
                keihinworldAngle.x = angkeihinx;
                keihinworldAngle.y = angkeihiny;
                keihinworldAngle.z = angkeihinz;
                keihinTransform.eulerAngles = keihinworldAngle; 


                Transform armTransform = arm.transform;//アームの情報を取得
                //アームの角度を0にする
                Vector3 armworldAngle = armTransform.eulerAngles;
                armworldAngle.x = 0.0f;
                armworldAngle.y = 0.0f;
                armworldAngle.z = 0.0f;
                armTransform.eulerAngles = armworldAngle;

                //アームの位置を設定
                Vector3 armworldPos = armTransform.position;
                if (count < 3)//アームが前列にある時
                {                               
                     armworldPos.x = (keihinx-(scalekeihinx/2)) + count*(scalekeihinx/2);
                     armworldPos.y = 10.0f;
                     armworldPos.z = 0.5f + keihinz - (scalekeihinz/2);
                     armTransform.position = armworldPos; // 座標を設定
                     Debug.Log(arm.transform.position);//アームの座標を表示

                }
                else if (count < 6)//アームが中列にある時
                {
                     armworldPos.x = (keihinx - (scalekeihinx / 2)) + (count-3) * (scalekeihinx / 2);
                     armworldPos.y = 10.0f;
                     armworldPos.z = keihinz;
                     armTransform.position = armworldPos; // 座標を設定
                     Debug.Log(arm.transform.position);//アームの座標を表示
                }
                else if (count < 9)//アームが後列にある時
                {
                     armworldPos.x = (keihinx - (scalekeihinx / 2)) + (count - 6) * (scalekeihinx / 2);
                     armworldPos.y = 10.0f;
                     armworldPos.z = -0.5f + keihinz + (scalekeihinz / 2);
                     armTransform.position = armworldPos; // 座標を設定
                     Debug.Log(arm.transform.position);//アームの座標を表示
                }

                //最適解を提示
                if (count == 9)//全パターン終了時
                {
                    //配列中の最小値を計算
                    float min = array.Min();
                    float minIndex = Array.IndexOf(array, min);//最小値が格納された配列番号
                    print("min=" + minIndex);//最適解の番号を表示

                    //最適解オブジェクトを提示
                    if (minIndex < 3)//最適解が前列の場合
                    {
                        seikai.transform.position = new Vector3((keihinx - (scalekeihinx / 2)) + minIndex * (scalekeihinx / 2), 10.0f, 0.5f + keihinz - (scalekeihinz / 2));
                    }
                    else if (minIndex < 6)//最適解が中列の場合
                    {
                        seikai.transform.position = new Vector3((keihinx - (scalekeihinx / 2)) + (minIndex - 3) * (scalekeihinx / 2), 10.0f, keihinz);
                    }
                    else if (minIndex < 9)//最適解が後列の場合
                    {
                        seikai.transform.position = new Vector3((keihinx - (scalekeihinx / 2)) + (minIndex - 6) * (scalekeihinx / 2), 10.0f, -0.5f + keihinz + (scalekeihinz / 2));
                    }
                    Debug.Log(seikai.transform.position);//最適解の座標を表示
                }

                //分割を自動化しようとして失敗したコード
                /*
                if (count % bunkatu == bunkatu-1)
                {
                    i++;
                }

                j++;
                if (count % bunkatu == 0)
                {
                    j = 0;
                }
                while (i<bunkatu)
                {
                    while (j<bunkatu)
                    {
                            armworldPos.x = (keihinx - (scalekeihinx / 2)) + (count-(i*bunkatu) * (scalekeihinx / (bunkatu-1)));
                            armworldPos.y = 10.0f;
                            armworldPos.z = keihinz - ((scalekeihinz / 2)+(i*(1/bunkatu)));
                            armTransform.position = armworldPos; // 座標を設定
                            Debug.Log(arm.transform.position);
                    }
                }
                print("i=" + i);
                print("j=" + j);
                */
            }
        }
    }  
    void OnCollisionEnter(Collision collision)//衝突判定
    {
        if (collision.gameObject.name == "arm")//景品とアームが衝突したとき
        {
            syoutotu = 1;//衝突判定を1にする

        }
    }
}
