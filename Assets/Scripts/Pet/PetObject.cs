using UnityEngine;

namespace Pet
{
    public class PetObject : MonoBehaviour
    {
        public float speed = 2.0f;
        public Vector3 offset = Vector3.zero;
        private GameObject _player;

        public void Initialize(GameObject player, Vector3 offset)
        {
            this._player = player;
            this.offset = offset;
        }

        void Update()
        {
            if (_player == null) return;

            float distance = Vector3.Distance(this.transform.position, _player.transform.position + offset);
            if (distance > 1.0f)
            {
                transform.position = Vector3.MoveTowards(transform.position, _player.transform.position + offset,
                    speed * Time.deltaTime);
            }
            
            transform.LookAt(_player.transform);
        }
    }
}