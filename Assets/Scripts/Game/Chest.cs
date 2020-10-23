using UnityEngine;
using DG.Tweening;

namespace Digg.Game
{

    // For a singular chest a singleton is a tolerable solution IMO
    // In future a reference to the chest can be used
    // during treasure pool initialization

    public sealed class Chest : MonoBehaviour
    {
        public static Chest Instance { get; private set; }

        [SerializeField] private Transform _lid = default;
        [SerializeField] private Vector3 _openedLidRotation = new Vector3(90, 0, 0);
        [SerializeField] private Vector3 _closedLidRotation = new Vector3(0, 0, 0);
        [SerializeField] private float _animationDuration = 0.5f;

        private Tween _lidTween;
        private bool _isHovering;

        public void Initialize()
        {
            Instance = this;
        }

        public void Open()
        {
            if (_lidTween != null)
            {
                _lidTween.Kill();
            } 

            _lidTween = _lid.DOLocalRotate(_openedLidRotation, _animationDuration, RotateMode.Fast)
                .SetEase(Ease.OutExpo);
        }

        public void Close()
        {
            if (_lidTween != null)
            {
                _lidTween.Kill();
            } 

            _lidTween = _lid.DOLocalRotate(_closedLidRotation, _animationDuration, RotateMode.Fast)
                .SetEase(Ease.InExpo);
        }

        public bool IsHovered()
        {
            return _isHovering;
        }

        void OnMouseEnter()
        {
            _isHovering = true;
        }

        void OnMouseExit()
        {
            _isHovering = false;
        }
    }
}