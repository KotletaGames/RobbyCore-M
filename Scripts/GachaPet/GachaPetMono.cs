using System.Linq;
using UnityEngine;
using Zenject;

namespace KotletaGames.RobbyGachaPetModule
{
    [RequireComponent(typeof(Collider))]
    public abstract class GachaPetMono<TItem> : MonoBehaviour
        where TItem : IItemInformation
    {
        [SerializeField] private GachaPetConfig<TItem> _gachaPetConfig;
        [SerializeField] private GachaPetView<TItem> _gachaPetView;
        [SerializeField] private GachaPetWorldButton _gachaPetWorldButton;

        private IInventoryItemReceiver _inventoryReceiver;
        private IInputGachaUsing _inputUsing;
        private GachaAcquire<TItem> _gachaAcquire;
        private IUiCamera _uiCamera;

        private bool _isFocus = false;

        [Inject]
        private void Construct(GachaAcquire<TItem> gachaAcquire, IInventoryItemReceiver characterInventoryReceiver, IInputGachaUsing inputUsing, IUiCamera uiCamera)
        {
            _gachaAcquire = gachaAcquire;
            _inventoryReceiver = characterInventoryReceiver;
            _inputUsing = inputUsing;
            _uiCamera = uiCamera;
        }
        /*
        private void OnValidate()
        {
            float sum = 0;
            foreach (var pet in _gachaPetConfig.Pets)
                sum += pet.Ratio;

            if (sum > 1)
                Debug.Log($"{sum} > 1 : {transform.parent.name}");
        }
        */

        protected virtual void Start()
        {
            _gachaPetView.Init(_gachaPetConfig);
            OnUpdateView();
            ChooseDisplayButtonByPlatform();
        }

        protected abstract void ChooseDisplayButtonByPlatform();

        private void OnEnable()
        {
            _inventoryReceiver.OnUpdate += OnUpdateView;
            _inputUsing.OnUsed += OnUse;

            _gachaPetWorldButton.AddListener(OnUse);
        }

        private void OnDisable()
        {
            _inventoryReceiver.OnUpdate -= OnUpdateView;
            _inputUsing.OnUsed -= OnUse;

            _gachaPetWorldButton.RemoveListener(OnUse);
        }

        public void DisplayAsDesktop()
        {
            _gachaPetWorldButton.DisplayAsDesktop();
        }

        public void DisplayAsMobile()
        {
            _gachaPetWorldButton.DisplayAsMobile();
        }

        public void Show()
        {
            _gachaPetView.Show();
            _uiCamera.Show();

            _isFocus = true;
        }

        public void Hide()
        {
            _gachaPetView.Hide();
            _uiCamera.Hide();

            _isFocus = false;
        }

        protected virtual void Buy(GachaPetConfig<TItem> gachaPetConfig)
        {
            _gachaAcquire.Buy(gachaPetConfig);
        }

        private void OnUpdateView()
        {
            if (HasResourceEnough() == true)
                _gachaPetView.ShowAllowedBuy();
            else
                _gachaPetView.ShowForbiddenBuy();
        }

        protected void OnUse()
        {
            if (_isFocus == false)
                return;

            if (HasResourceEnough() == false)
                return;

            Buy(_gachaPetConfig);
            OnUpdateView();
        }

        protected void SetInteractionText(string keyText, string labelText)
        {
            _gachaPetWorldButton.SetInteractionText(keyText, labelText);
        }

        private bool HasResourceEnough()
        {
            return _gachaPetConfig.CostUnlockingDatas
                .All(i => _inventoryReceiver.CountBy(i.Item.Id) >= i.Count);
        }
    }
}