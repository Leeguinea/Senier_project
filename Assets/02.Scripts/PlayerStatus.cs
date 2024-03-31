using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    protected float curHp;  //현재 HP
    public float maxHp;     //최대 HP
    public Slider HpSlider;

    public void SetHp(float hp) //초기 체력 설정 함수
    {
        maxHp = hp;
        curHp = maxHp;
    }

    public void CheckHP() //HP갱신 함수
    {
        if (HpSlider != null)
        {
            HpSlider.value = curHp / maxHp;
        }
    }

    public void Damage(float damage) //받는 데미지 함수
    {

        if (maxHp == 0 || curHp <= 0) // 이미 최대 체력이 0이거나 현재 체력이 0이라면 패스
        {
            return;
        }

        curHp = curHp - damage; 
        CheckHP();

        if (curHp <= 0) // 체력 0 이하 시
        {
            //게임오버
        }
    }

    public void Heal(float heal) // 받는 힐 함수
    {
        if (curHp < maxHp) // 현재 체력이 최대 체력보다 작다면
        {
            curHp = curHp + heal;
            CheckHP();
        }

        if (curHp <= maxHp) // 현재 체력이 최대 체력보다 많거나 같다면
        {
            curHp = maxHp;
            CheckHP();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
