using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class TankDamage2 : MonoBehaviour
{
    NavMeshAgent agent;
    private int initHp = 100;
    private int currHp = 0;
    public GameObject fire;

    public Canvas hudCanvas;  //탱크 밑에 Canvas
    public Image hpBar;  //Filled 타입의 Image UI 항목을 연결할 변수
    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        currHp = initHp; //현재 생명치를 초기 생명치로 초기값 설정
        hpBar.color = Color.green; //Filled 이미지 색상을 녹색으로 설정
    }
    void OnTriggerEnter(Collider col)
    {
        if (currHp > 0 && col.tag == "BULLET")
        {
            currHp -= 20;
            hpBar.fillAmount = (float)currHp / (float)initHp;//현재 생명치 백분율
                                                             //생명 수치에 따라 Filled 이미지의 색상을 변경
            if (hpBar.fillAmount <= 4.0f)
                hpBar.color = Color.red;
            else if (hpBar.fillAmount <= 0.6f)
                hpBar.color = Color.yellow;

            if (currHp <= 0)
            {
                EnemyCount.leftEnemy--;
                if (EnemyCount.leftEnemy <= 0)
                {
                    SceneManager.LoadScene("Stage2Clear");
                }
                GameObject copy_fire = Instantiate(fire) as GameObject;
                copy_fire.transform.position = this.gameObject.transform.position;
                Destroy(copy_fire, 3.0f);
                hudCanvas.enabled = false; //HUD 비활성화
                agent.enabled = false;
                gameObject.GetComponent<Enemy>().enabled = false;
                Destroy(this.gameObject, 3.0f);
            }
        }

        if (currHp > 0 && col.tag == "DamagePoint")
        {
            currHp -= 100;
            hpBar.fillAmount = (float)currHp / (float)initHp;//현재 생명치 백분율
                                                             //생명 수치에 따라 Filled 이미지의 색상을 변경

            if (currHp <= 0)
            {
                EnemyCount.leftEnemy--;
                if (EnemyCount.leftEnemy <= 0)
                {
                    SceneManager.LoadScene("Stage2Clear");
                }
                GameObject copy_fire = Instantiate(fire) as GameObject;
                copy_fire.transform.position = this.gameObject.transform.position;
                Destroy(copy_fire, 3.0f);
                hudCanvas.enabled = false; //HUD 비활성화
                agent.enabled = false;
                gameObject.GetComponent<Enemy>().enabled = false;
                Destroy(this.gameObject, 3.0f);
            }
        }
    }

}