using UnityEngine;

namespace Code
{
    public class TimeController : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
                Time.timeScale = 1;
            
            if (Input.GetKeyDown(KeyCode.W))
                Time.timeScale = 5;
            
            if (Input.GetKeyDown(KeyCode.E))
                Time.timeScale = 10;
            
            if (Input.GetKeyDown(KeyCode.R))
                Time.timeScale = 15;
        }
    }
}