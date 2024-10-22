using KotletaGames.AdditionalModule;
using Zenject;

namespace KotletaGames.RobbyGiftsModule
{
    public class IncreasingTimerSet : IInitializable
    {
        private readonly IncreasingSetConfig[] _configs;
        private readonly OnlyContianerPrefabSpawner<IncreasingView> _spawner;
        private readonly IncreasingNotificationCounter _notificationCounter;

        private IncreasingSlot[] _slots;
        private ActualTextData _actualTextData;

        public IncreasingTimerSet(IncreasingSetConfig[] configs, OnlyContianerPrefabSpawner<IncreasingView> spawner, 
            IncreasingNotificationCounter notificationCounter, ActualTextData actualTextData)
        {
            _actualTextData = actualTextData;
            _configs = configs;
            _spawner = spawner;
            _notificationCounter = notificationCounter;
        }

        public void Initialize()
        {
            _notificationCounter.NextGiftText.text = _actualTextData.NextGift;
            _notificationCounter.SetTotalCount(_configs.Length);

            _slots = new IncreasingSlot[_configs.Length];
            for (int i = 0; i < _configs.Length; i++)
            {
                IncreasingView mono = _spawner.Spawn(); 
                IncreasingSetConfig config = _configs[i];
                IncreasingSlot slot = _slots[i] = new IncreasingSlot(mono, config, _actualTextData);
                slot.StartTimer(_notificationCounter.AddAllowed);

                mono.OnOpened += () =>
                {
                    slot.Open();
                    mono.MarkAsOpened();
                    mono.SetForbiddenColor(config.ForbiddenButtonColor);
                    _notificationCounter.AddForbidden();
                };

                mono.GettingText = _actualTextData.GiftGettingText;
            }

            _notificationCounter.TrackTime(_slots);
        }
    }
}