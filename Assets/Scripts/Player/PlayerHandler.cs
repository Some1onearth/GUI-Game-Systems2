using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHandler : Stats
{
    [Header("Damage Flash and Death")]
    public Image damageImage;
    public Image deathImage;
    public Text deathText;
    public AudioClip deathClip;
    public AudioSource playersAudio;
    public Transform currentCheckPoint;
    //                                   R  G  B  A
    public Color flashColour = new Color(1, 0, 0, 0.2f);
    public float flashSpeed = 5f;
    public static bool isDead;
    public bool isDamaged;
    public bool canHeal;
    public float healDelayTimer;

    void DeathText()
    {
        deathText.text = "You've Fallen in Battle";
    }
    void RespawnText()
    {
        deathText.text = "...But the Gods have decided it is not your time...";
    }
    void Respawn()
    {
        //RESET EVERYTHING
        deathText.text = "";
        for (int i = 0; i < attributes.Length; i++)
        {
            attributes[i].currentValue = attributes[i].maxValue;
        }
        isDead = false;
        //load position
        this.transform.position = currentCheckPoint.position;
        this.transform.rotation = currentCheckPoint.rotation;
        //Respawn
        deathImage.GetComponent<Animator>().SetTrigger("Respawn");
    }
    void Death()
    {
        //Set the death flag to dead
        isDead = true;
        //clear existing text just in case!
        deathText.text = "";
        //Set and Play the audio clip
        playersAudio.clip = deathClip;
        playersAudio.Play();
        //Trigger death screen
        deathImage.GetComponent<Animator>().SetTrigger("isDead");
        //in 2 seconds set our text when we die
        Invoke("DeathText", 2f);
        //in 6 seconds set our text when we respawn
        Invoke("RespawnText", 6f);
        //in 9 seconds respawn us
        Invoke("Respawn", 9f);
    }
    public void RegenOverTime(int valueIndex)
    {
        attributes[valueIndex].currentValue += Time.deltaTime * (attributes[valueIndex].regenValue /*plus a multiplier of a stat eg constitution or dex*/);
    }
    public void DamagePlayer(float damage)
    {
        //Turn on the red flicker
        isDamaged = true;

        //take damage
        attributes[0].currentValue -= damage;
        //delay regen healing
        canHeal = false;
        healDelayTimer = 0;
        if (attributes[0].currentValue <= 0 && !isDead)
        {
            Death();
        }
    }

    void Update()
    {
        //debug
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.X))
        {
            DamagePlayer(10);
        }
#endif
        //
        for (int i = 0; i < attributes.Length; i++)
        {
            attributes[i].displayImage.fillAmount = Mathf.Clamp01(attributes[i].currentValue / attributes[i].maxValue);
        }
        #region DamageFlash
        if (isDamaged && !isDead)
        {
            damageImage.color = flashColour;//screen flashes red to indicate damage being taken
            isDamaged = false;
        }
        else if (damageImage.color.a > 0)
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        #endregion
        #region Can Heal
        if (!canHeal)
        {
            healDelayTimer += Time.deltaTime;
            if (healDelayTimer >= 5) //heals over time
            {
                canHeal = true;
            }
        }
        if (canHeal && attributes[0].currentValue < attributes[0].maxValue && attributes[0].currentValue > 0)
        {
            RegenOverTime(0);
        }
        #endregion
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            currentCheckPoint = other.transform;
            for (int i = 0; i < attributes.Length; i++)
            {
                attributes[i].regenValue += 7;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CheckPoint"))
        {
            currentCheckPoint = other.transform;
            for (int i = 0; i < attributes.Length; i++)
            {
                attributes[i].regenValue -= 7;
            }
        }
    }
}
