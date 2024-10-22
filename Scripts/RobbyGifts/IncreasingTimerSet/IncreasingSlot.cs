using Cysharp.Threading.Tasks;
using KotletaGames.AdditionalModule;
using System;

namespace KotletaGames.RobbyGiftsModule
{
    public class IncreasingSlot
    {
        private readonly IncreasingView _mono;
        private readonly IncreasingSetConfig _config;
        private ActualTextData _actualTextData;

        private const int _delay = 1000;

        public IncreasingSlot(IncreasingView mono, IncreasingSetConfig config, ActualTextData actualTextData)
        {
            _actualTextData = actualTextData;
            _mono = mono;
            _config = config;

            _mono.SetRateColor(_config.RateColor);
            _mono.SetForbiddenColor(_config.ForbiddenButtonColor);
            _mono.SetDescription(_actualTextData.GiftDescription[config.Id]);
        }

        public string FormattedTime { get; private set; } = string.Empty;

        public bool IsComplated { get; private set; } = false;

        public void StartTimer(Action onComplated = null)
        {
            UniTask.Create(async () =>
            {
                int second = GetTotalTimeAsSecond();

                for (int i = second; i >= 0; i--)
                {
                    FormattedTime = _actualTextData.GetTime(i);

                    _mono.SetTimeText(FormattedTime);

                    await UniTask.Delay(_delay);
                }

                onComplated?.Invoke();
                IsComplated = true;
                _mono.Allow();
                _mono.SetAllowColor(_config.AllowButtonColor);
            });
        }

        public void Open()
        {
            _config.Gifts.GetRandomElement().Receive();
        }

        private int GetTotalTimeAsSecond()
        {
            return (int)(_config.TimeMinute * 60f);
        }
    }
}