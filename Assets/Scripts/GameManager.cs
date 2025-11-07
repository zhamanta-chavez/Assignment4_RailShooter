using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Image hitFlash;
    public Image hurtDisplay;

    public int playerHealth = 5;

    public GameObject deathPanel;

    public float hurtDisplayAlpha = 0;
    public float hurtDisplayerTimer = .5f;

    private void Start()
    {
        hitFlash.enabled = false;
        deathPanel.SetActive(false);
    }

    private void Update()
    {
        Color _hurt = hurtDisplay.color;
        _hurt.a = hurtDisplayAlpha;

        hurtDisplay.color = _hurt;

        if (hurtDisplayAlpha > 0)
        {
            hurtDisplayAlpha -= Time.deltaTime;
        }
    }

    public void HurtPlayer()
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

    void PlayerDead()
    {
        deathPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    IEnumerator HurtState()
    {
        hitFlash.enabled = true;
        yield return new WaitForSeconds(.05f);
        hitFlash.enabled = false;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
