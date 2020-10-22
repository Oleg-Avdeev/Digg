using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;

namespace Digg.Game
{
    public sealed class CounterUI : MonoBehaviour
    {
        [SerializeField] private Text _counterText = default;
        [SerializeField] private Transform _counterIcon = default;
        [SerializeField] private Vector3 _punchVector = default;

        public void HandleNewValue(int value)
        {
            _counterText.text = value.ToString();
            _counterIcon.DOPunchScale(_punchVector, 0.4f, 1);
        }
    }
}