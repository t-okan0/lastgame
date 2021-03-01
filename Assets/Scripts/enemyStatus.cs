using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyStatus : MonoBehaviour
{
    [SerializeField] GameObject enemyDeath;
    [SerializeField] int enemyHp;
    [SerializeField] AudioClip breaking;
    AudioSource audioSource;

    public void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void OnDamage()
    {
        enemyHp -= 1;
        if (enemyHp == 0)
        {
            DestroyEnemy();
        }
    }
    public void DestroyEnemy()
    {
        //Destroyの後に音を付属するとデストロイした時点で鳴らなくなるためエラーが発生
        audioSource.PlayOneShot(breaking);
        //enemyDeath = やられた時のエフェクト
        //Instantiate = Prefabを発生させる
        Instantiate(enemyDeath, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject);
    }
}
