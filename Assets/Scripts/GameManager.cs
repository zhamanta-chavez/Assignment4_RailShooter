using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Image hitFlash;
    public Image hurtDisplay;

    public Image shotgunIcon;
    public Sprite shotFull;
    public Sprite shotEmpty;

    public int playerHealth = 5;

    public GameObject deathPanel;
    public GameObject hudPanel;
    public GameObject reloadPrompt;
    public float reloadDisplayTime = 2;
    public float reloadDisplay;

    public bool canHurt;
    public float hurtDisplayAlpha = 0;
    public float hurtDisplayerTimer = .5f;

    [Header("Set up Heart Container")]
    public int numberOfHearts;
    public Image[] _hearts;
    public Sprite _heartSprite;

    [Header("Ammo Count")]
    public int bulletCount;
    public int maxBulletCount;
    public int shellCount;

    public TMP_Text bulletText;
    public TMP_Text shellText;

    [Header("Boss Variables")]
    public float bossHealth = 100;
    public Image bossMeter;
   

    private void Start()
    {
        if (Instance = null)
        {
            Instance = this;
        }
        hitFlash.enabled = false;
        deathPanel.SetActive(false);
        canHurt = true;
    }

    private void Update()
    {
        // Shotgun Icon
        if (shellCount > 0)
        {
            shotgunIcon.sprite = shotFull;
        }
        else
        {
            shotgunIcon.sprite = shotEmpty;
        }

        // Bullet count
        bulletText.text = bulletCount.ToString();
        shellText.text = shellCount.ToString();

        Color _hurt = hurtDisplay.color;
        _hurt.a = hurtDisplayAlpha;

        hurtDisplay.color = _hurt;

        if (hurtDisplayAlpha > 0)
        {
            hurtDisplayAlpha -= Time.deltaTime;
        }

        // Reload Display
        if (hurtDisplayAlpha > 0)
        {
            hurtDisplayAlpha -= Time.deltaTime;
        }
        if (reloadDisplay > 0)
        {
            reloadPrompt.SetActive(true);
        }
        else
        {
            reloadPrompt.SetActive(false);
        }

        // Keep control of the hearts
        for (int i =0; i < _hearts.Length; i++)
        {
            if (i < playerHealth)
            {
                _hearts[i].sprite = _heartSprite;
                _hearts[i].color = new Color(1, 1, 1, 1);
            }
            else
            {
                _hearts[i].sprite = null;
                _hearts[i].color = new Color(1, 1, 1, 0);
            }

            if (i < numberOfHearts)
            {
                _hearts[i].enabled = true;
            }
            else
            {
                _hearts[i].enabled = false;
            }
        }

        if (playerHealth > numberOfHearts)
        {
            playerHealth = numberOfHearts;
        }

        bossMeter.fillAmount = bossHealth / 100;
    }

    public void HurtPlayer()
    {
        if (canHurt)
        {
            playerHealth--;
            if (playerHealth > 0)
            {
                hurtDisplayAlpha = hurtDisplayerTimer;
                StartCoroutine(HurtState());
            }
            else
            {
                PlayerDead();
            }
        }
    }

    void PlayerDead()
    {
        deathPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    IEnumerator HurtState()
    {
        canHurt = false;
        Color _flash = hitFlash.color;
        hitFlash.enabled = true;
        yield return new WaitForSeconds(.05f);
        hitFlash.enabled = false;
        yield return new WaitForSeconds(1.5f);
        canHurt = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
