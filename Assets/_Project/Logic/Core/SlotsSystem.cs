using static _Project.Logic.Core.Line;

namespace _Project.Logic.Core
{
    public class SlotsSystem
    {
        private SlotsRepository _slotsRepository;
        private SlotsContainer _slotsContainer;

        public SlotsSystem(SlotsRepository slotsRepository, SlotsContainer slotsContainer)
        {
            _slotsRepository = slotsRepository;
            _slotsContainer = slotsContainer;
        }

        public void Prepare()
        {
            Line line = 0;
            int nextIndexRow = 0;
            int countElementsInRow = _slotsRepository.Slots.Length / 5;
            int currentLengthInRow = countElementsInRow;
            
            for (int row = 0; row < 5; ++row)
            {
                for (int i = nextIndexRow; i < currentLengthInRow; ++i)
                    _slotsRepository.Slots[i].Setup(_slotsContainer.Slots.Length > i 
                            ? _slotsContainer.Slots[i] 
                            : null, line);

                line++;
                nextIndexRow += countElementsInRow;
                currentLengthInRow += countElementsInRow;
            }
        }
    }
}