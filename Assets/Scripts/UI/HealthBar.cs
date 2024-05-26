using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MyGame.UI
{
    public class HealthBar : MonoBehaviour
    {
        public Image bar;
        public GameObject healthBar;

        protected void Update()
        {
            healthBar.transform.LookAt(UnityEngine.Camera.main.transform);
            healthBar.transform.rotation = Quaternion.LookRotation(UnityEngine.Camera.main.transform.forward);
        }

        public void UpdateHealthBar (float maxHp,  float currentHp)
        {
            bar.fillAmount = currentHp / maxHp;
        }
    }
}