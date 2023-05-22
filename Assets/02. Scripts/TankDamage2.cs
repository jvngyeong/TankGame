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

    public Canvas hudCanvas;  //��ũ �ؿ� Canvas
    public Image hpBar;  //Filled Ÿ���� Image UI �׸��� ������ ����
    // Start is called before the first frame update
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        currHp = initHp; //���� ����ġ�� �ʱ� ����ġ�� �ʱⰪ ����
        hpBar.color = Color.green; //Filled �̹��� ������ ������� ����
    }
    void OnTriggerEnter(Collider col)
    {
        if (currHp > 0 && col.tag == "BULLET")
        {
            currHp -= 20;
            hpBar.fillAmount = (float)currHp / (float)initHp;//���� ����ġ �����
                                                             //���� ��ġ�� ���� Filled �̹����� ������ ����
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
                hudCanvas.enabled = false; //HUD ��Ȱ��ȭ
                agent.enabled = false;
                gameObject.GetComponent<Enemy>().enabled = false;
                Destroy(this.gameObject, 3.0f);
            }
        }

        if (currHp > 0 && col.tag == "DamagePoint")
        {
            currHp -= 100;
            hpBar.fillAmount = (float)currHp / (float)initHp;//���� ����ġ �����
                                                             //���� ��ġ�� ���� Filled �̹����� ������ ����

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
                hudCanvas.enabled = false; //HUD ��Ȱ��ȭ
                agent.enabled = false;
                gameObject.GetComponent<Enemy>().enabled = false;
                Destroy(this.gameObject, 3.0f);
            }
        }
    }

}