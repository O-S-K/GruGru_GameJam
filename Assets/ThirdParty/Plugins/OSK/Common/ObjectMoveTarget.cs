using UnityEngine;

namespace SquadHeroes
{
    public class ObjectMoveTarget : MonoBehaviour
    {

        /* ----------------------------- Runtime Fields ----------------------------- */
        private Transform _player;

        public float maxSpeed = 250;

        private int _range = 5;

        private float _speedCounter = 10;
        private float _currentSpeed = 0;
        private float _velocity = 0;
        private float _time = 1;
        private float _timeTarget = 0.15f;

        private bool _isMoveTarget = false;
         
        /* -----------------------------  Methods ----------------------------- */
        public void SetTarget(Transform player)
        {
            this._player = player;
        }
 

        private void Update()
        {
            if(Vector2.Distance(transform.position, _player.position) < _range && !_isMoveTarget)
            {
                _currentSpeed = -_speedCounter;
                _isMoveTarget = true;
            }


            if (!_isMoveTarget) 
                return;

            if (_timeTarget > 0)
            {
                _timeTarget -= Time.deltaTime;
            }
            else
            {
                _currentSpeed = Mathf.SmoothDamp(_currentSpeed, maxSpeed, ref _velocity, _time);
                var dir = (_player.position - transform.position).normalized;
                transform.position += _currentSpeed * dir * Time.deltaTime;
                if (Vector2.Distance(transform.position, _player.position) <= 0.5f)
                {
                    //OSK.Observer.Notify(KeyObserver.EffectRes);
                    Destroy(gameObject);
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _range);
        }

        /* -------------------------------------------------------------------------- */

    }
}
