using System.Collections.Generic;

namespace _Project.Logic.Core
{
    public class RunableRepository
    {
        private List<IRunable> _runables;
        
        public IReadOnlyList<IRunable> Runables => _runables;

        public RunableRepository(int capacity) => 
            _runables = new(capacity);

        public void Register(IRunable runable) => 
            _runables.Add(runable);
        
        public void Unregister(IRunable runable) => 
            _runables.Remove(runable);
    }
}