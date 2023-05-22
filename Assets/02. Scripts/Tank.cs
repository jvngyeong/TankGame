using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tank : MonoBehaviour
{
    #region 멤버 변수;
    //앞 뒤 이동
    public int moveSpeed;
    public float move;
    public float moveVertical;

    public Image hpBar;
    //좌우 이동
    public int rotSpeed;
    public float rotate;
    public float rotHorizon;

    //터렛 회전
    public float rotTurret;
    public GameObject turret;
    public GameObject Boostoff;
    public GameObject HP1;
    public GameObject HP2;
    public GameObject HP3;
    public GameObject HP4;
    public GameObject HP5;
    public GameObject SBullet_icon;
    public int power;
    public int spower;
    public GameObject bulletPrefab;
    public GameObject GBEffectPrefab;
    public GameObject SBulletPrefab;
    public GameObject EnginePrefab;
    public GameObject HealPrefab;
    private Transform spPoint;
    private Transform SspPoint;
    public float DestroyTime = 2.0f;
    public float ChargingTime = 1.0f;
    private float fTime1;
    private float fTime2;
    private float fTime3 = 0.5f;
    public GameObject GBEffect;
    public GameObject SBullet;
    //포신 회전
    public float keyGun;
    public GameObject gunBase;
    public AudioClip Charging;
    public GameObject ChargingEffect;
    public GameObject BuffEffectPrefab;
    public GameObject BuffEffect;
    public GameObject EngineSound;
    public GameObject HealSound;
    public int initHp = 100;
    public int currHp = 0;
    private Transform MyTank;
    public AudioSource audio1;
    #endregion

    void Awake()
    {
        currHp = initHp; //현재 생명치를 초기 생명치로 초기값 설정
        hpBar.color = Color.green; //Filled 이미지 색상을 녹색으로 설정
    }

    void Start()
    {
        SBullet_icon.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        moveSpeed = 20;
        rotSpeed = 120;
        power = 1500;
        spower = 2000;
        spPoint = GameObject.Find("spPoint").transform;
        SspPoint = GameObject.Find("SspPoint").transform;
        MyTank = GameObject.Find("MyTank").transform;
    }

    void Update()
    {
        fTime1 += Time.deltaTime;
        move = moveSpeed * Time.deltaTime;
        rotate = rotSpeed * Time.deltaTime;
        
        moveVertical = Input.GetAxis("Vertical");
        rotHorizon = Input.GetAxis("Horizontal");
        rotTurret = Input.GetAxis("Mouse X");

        transform.Translate(Vector3.forward * move * moveVertical);
        transform.Rotate(new Vector3(0.0f, rotate * rotHorizon, 0.0f));
        turret.transform.Rotate(Vector3.up * rotTurret * rotate);

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
        {
            EngineSound = Instantiate(EnginePrefab, MyTank.position, MyTank.rotation) as GameObject;
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            Destroy(EngineSound);
        }

        //탱크 이동속도 UP
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            BuffEffect = Instantiate(BuffEffectPrefab, MyTank.position, MyTank.rotation) as GameObject;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Boostoff.SetActive(false);
            moveSpeed = 40;
            BuffEffect.transform.position = MyTank.transform.position;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Destroy(BuffEffect);
            Boostoff.SetActive(true);
            moveSpeed = 20;
        }

        //총알 발사
        if (Input.GetMouseButton(0))
        {
            if(fTime1 > 0.5f) 
            {
                GameObject bullet = Instantiate(bulletPrefab, spPoint.position, spPoint.rotation) as GameObject;
                Rigidbody bulletAddforce = bullet.GetComponent<Rigidbody>();
                bulletAddforce.AddForce(turret.transform.forward * power);
                Destroy(bullet, DestroyTime);

                fTime1 = 0.0f;
            }
        }

        //특수탄환
        if (Input.GetMouseButtonDown(1))
        {
            GBEffect = Instantiate(GBEffectPrefab, SspPoint.position, SspPoint.rotation) as GameObject;
        }
        if (Input.GetMouseButton(1))
        {
            fTime3 += Time.deltaTime;
            if(fTime2 > 1f){
            SBullet_icon.SetActive(true);

            }
            if (fTime3 > ChargingTime)
            {
                AudioSource.PlayClipAtPoint(Charging, SspPoint.transform.position);
                fTime3 = 0.0f;
            }
            GBEffect.transform.position = SspPoint.transform.position;
            GBEffect.transform.rotation = SspPoint.transform.rotation;
            fTime2 += Time.deltaTime;
        }
        if (Input.GetMouseButtonUp(1))
        {
            if (fTime2 > 1f)
            {
                GameObject bullet2 = Instantiate(SBulletPrefab, SspPoint.position, SspPoint.rotation) as GameObject;
                Rigidbody bullet2Addforce = bullet2.GetComponent<Rigidbody>();
                bullet2Addforce.AddForce(turret.transform.forward * spower);
            }
            fTime2 = 0.0f;
            Destroy(GBEffect);
            SBullet_icon.SetActive(false);
        }

        float keyGun = Input.GetAxis("Mouse ScrollWheel");
        gunBase.transform.Rotate(Vector3.right * keyGun, 4);

        Vector3 ang = gunBase.transform.eulerAngles;
        if (ang.x > 180)
            ang.x -= 360;
        ang.x = Mathf.Clamp(ang.x, -15, 5);
        gunBase.transform.eulerAngles = ang;
    }


     void OnTriggerEnter(Collider col)
    {
    
        if(currHp < 100 && col.gameObject.tag == "HPbox"){
            currHp += 20;
            HealSound = Instantiate(HealPrefab, MyTank.position, MyTank.rotation) as GameObject;
            hpBar.fillAmount = (float)currHp / (float)initHp;//현재 생명치 백분율
            if (hpBar.fillAmount >= 1.0f)
                hpBar.color = Color.green;
            else if (hpBar.fillAmount >= 0.6f)
                hpBar.color = Color.red;                                          //생명 수치에 따라 Filled 이미지의 색상을 변경
        }
            if (currHp > 0 && col.tag == "EBullet")
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
                    SceneManager.LoadScene("Lose");
                }
            }
    }
}