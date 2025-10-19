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
            foreach (SlotConfig slot in _slotsContainer.Slots) 
                _slotsRepository.Slots[slot.SlotIndex].ChangeType(slot);
        }
    }
}