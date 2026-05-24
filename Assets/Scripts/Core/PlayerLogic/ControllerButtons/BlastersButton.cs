using Core.WeaponsLogic;
using Zenject;

namespace Core.PlayerLogic.ControllerButtons
{
    public class BlastersButton : HoldingButton
    {
        private Blasters _blasters;

        [Inject]
        private void Construct(Blasters blasters)
        {
            _blasters = blasters;
        }

        private void Update()
        {
            if (IsHolding)
            {
                _blasters.Shoot();
            }
        }
    }
}