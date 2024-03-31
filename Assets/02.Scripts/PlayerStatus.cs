using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    protected float curHp;  //���� HP
    public float maxHp;     //�ִ� HP
    public Slider HpSlider;

    public void SetHp(float hp) //�ʱ� ü�� ���� �Լ�
    {
        maxHp = hp;
        curHp = maxHp;
    }

    public void CheckHP() //HP���� �Լ�
    {
        if (HpSlider != null)
        {
            HpSlider.value = curHp / maxHp;
        }
    }

    public void Damage(float damage) //�޴� ������ �Լ�
    {

        if (maxHp == 0 || curHp <= 0) // �̹� �ִ� ü���� 0�̰ų� ���� ü���� 0�̶�� �н�
        {
            return;
        }

        curHp = curHp - damage; 
        CheckHP();

        if (curHp <= 0) // ü�� 0 ���� ��
        {
            //���ӿ���
        }
    }

    public void Heal(float heal) // �޴� �� �Լ�
    {
        if (curHp < maxHp) // ���� ü���� �ִ� ü�º��� �۴ٸ�
        {
            curHp = curHp + heal;
            CheckHP();
        }

        if (curHp <= maxHp) // ���� ü���� �ִ� ü�º��� ���ų� ���ٸ�
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
