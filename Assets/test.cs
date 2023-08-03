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

    int syoutotu = 0;//�A�[���ƌi�i���Փ˂������ǂ���
    int count = 0;//���s��
    float[] array = new float[9];//�i�i���Ƃ̋������L�^����z��

    //int bunkatu = 3;//������
    //int i = 0;
    //int j = 0;

    float keihinx=0;//�i�i�̏���x���W
    float keihiny = 0;//�i�i�̏���y���W
    float keihinz = 0;//�i�i�̏���z���W
    float angkeihinx = 0;//�i�i�̏���x�p�x
    float angkeihiny = 0;//�i�i�̏���y�p�x
    float angkeihinz = 0;//�i�i�̏���z�p�x
    float scalekeihinx = 0;//�i�i��x�T�C�Y
    float scalekeihiny = 0;//�i�i��y�T�C�Y
    float scalekeihinz = 0;//�i�i��z�T�C�Y

    //�Q�[���I�u�W�F�N�g���Ăяo��
    [SerializeField] private GameObject arm;
    [SerializeField] private GameObject keihin;
    [SerializeField] private GameObject ana;
    [SerializeField] private GameObject seikai;


    void Start()
    {
        Time.timeScale = 60.0f;//�{���ɂ���

        Transform keihinTransform = this.transform;//�i�i�̈ʒu�C�p�x�C�T�C�Y���擾
        //�i�i�̏����ʒu
        Vector3 keihinworldPos = keihinTransform.position;
        keihinx = keihinworldPos.x;
        keihiny=keihinworldPos.y;
        keihinz=keihinworldPos.z;
        //�i�i�̏����p�x
        Vector3 keihinworldAngle = keihinTransform.eulerAngles;
        angkeihinx=keihinworldAngle.x;
        angkeihiny=keihinworldAngle.y;
        angkeihinz=keihinworldAngle.z;
        //�i�i�̃T�C�Y
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
        if (syoutotu == 1)//�Փ˔��肪1�̂Ƃ��i�Փ˂�����j
        {
            if (rb.IsSleeping())//�i�i���Î~�����Ƃ�
            {
                syoutotu = 0;//�Փ˔����0�ɖ߂��Ă���

                Vector3 targetPos = keihin.transform.position;//���݂̌i�i�̈ʒu
                Vector3 anaPos = ana.transform.position;//�l�����̈ʒu     
                float dis = Vector3.Distance(targetPos, anaPos);//�i�i�Ɗl�����̋������v�Z
                array[count] = dis;//�z��ɋL�^     
                print("dis[" + count + "]=" + array[count]);//������\��

                count++;//���s��
                

                Transform keihinTransform = this.transform;//�i�i�̏����擾
                //�i�i�������ʒu�ɖ߂�
                Vector3 keihinworldPos = keihinTransform.position;
                keihinworldPos.x = keihinx;
                keihinworldPos.y = keihiny;
                keihinworldPos.z = keihinz;
                keihinTransform.position = keihinworldPos;
                //�i�i�������p�x�ɖ߂�
                Vector3 keihinworldAngle = keihinTransform.eulerAngles;
                keihinworldAngle.x = angkeihinx;
                keihinworldAngle.y = angkeihiny;
                keihinworldAngle.z = angkeihinz;
                keihinTransform.eulerAngles = keihinworldAngle; 


                Transform armTransform = arm.transform;//�A�[���̏����擾
                //�A�[���̊p�x��0�ɂ���
                Vector3 armworldAngle = armTransform.eulerAngles;
                armworldAngle.x = 0.0f;
                armworldAngle.y = 0.0f;
                armworldAngle.z = 0.0f;
                armTransform.eulerAngles = armworldAngle;

                //�A�[���̈ʒu��ݒ�
                Vector3 armworldPos = armTransform.position;
                if (count < 3)//�A�[�����O��ɂ��鎞
                {                               
                     armworldPos.x = (keihinx-(scalekeihinx/2)) + count*(scalekeihinx/2);
                     armworldPos.y = 10.0f;
                     armworldPos.z = 0.5f + keihinz - (scalekeihinz/2);
                     armTransform.position = armworldPos; // ���W��ݒ�
                     Debug.Log(arm.transform.position);//�A�[���̍��W��\��

                }
                else if (count < 6)//�A�[��������ɂ��鎞
                {
                     armworldPos.x = (keihinx - (scalekeihinx / 2)) + (count-3) * (scalekeihinx / 2);
                     armworldPos.y = 10.0f;
                     armworldPos.z = keihinz;
                     armTransform.position = armworldPos; // ���W��ݒ�
                     Debug.Log(arm.transform.position);//�A�[���̍��W��\��
                }
                else if (count < 9)//�A�[�������ɂ��鎞
                {
                     armworldPos.x = (keihinx - (scalekeihinx / 2)) + (count - 6) * (scalekeihinx / 2);
                     armworldPos.y = 10.0f;
                     armworldPos.z = -0.5f + keihinz + (scalekeihinz / 2);
                     armTransform.position = armworldPos; // ���W��ݒ�
                     Debug.Log(arm.transform.position);//�A�[���̍��W��\��
                }

                //�œK�����
                if (count == 9)//�S�p�^�[���I����
                {
                    //�z�񒆂̍ŏ��l���v�Z
                    float min = array.Min();
                    float minIndex = Array.IndexOf(array, min);//�ŏ��l���i�[���ꂽ�z��ԍ�
                    print("min=" + minIndex);//�œK���̔ԍ���\��

                    //�œK���I�u�W�F�N�g���
                    if (minIndex < 3)//�œK�����O��̏ꍇ
                    {
                        seikai.transform.position = new Vector3((keihinx - (scalekeihinx / 2)) + minIndex * (scalekeihinx / 2), 10.0f, 0.5f + keihinz - (scalekeihinz / 2));
                    }
                    else if (minIndex < 6)//�œK��������̏ꍇ
                    {
                        seikai.transform.position = new Vector3((keihinx - (scalekeihinx / 2)) + (minIndex - 3) * (scalekeihinx / 2), 10.0f, keihinz);
                    }
                    else if (minIndex < 9)//�œK�������̏ꍇ
                    {
                        seikai.transform.position = new Vector3((keihinx - (scalekeihinx / 2)) + (minIndex - 6) * (scalekeihinx / 2), 10.0f, -0.5f + keihinz + (scalekeihinz / 2));
                    }
                    Debug.Log(seikai.transform.position);//�œK���̍��W��\��
                }

                //���������������悤�Ƃ��Ď��s�����R�[�h
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
                            armTransform.position = armworldPos; // ���W��ݒ�
                            Debug.Log(arm.transform.position);
                    }
                }
                print("i=" + i);
                print("j=" + j);
                */
            }
        }
    }  
    void OnCollisionEnter(Collision collision)//�Փ˔���
    {
        if (collision.gameObject.name == "arm")//�i�i�ƃA�[�����Փ˂����Ƃ�
        {
            syoutotu = 1;//�Փ˔����1�ɂ���

        }
    }
}
