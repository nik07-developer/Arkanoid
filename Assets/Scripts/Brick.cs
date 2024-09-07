using UnityEngine;

namespace Game
{
    public class Brick : MonoBehaviour
    {
        public void Hit()
        {
            Debug.Log("By");
            gameObject.SetActive(false);
        }
    }
}
