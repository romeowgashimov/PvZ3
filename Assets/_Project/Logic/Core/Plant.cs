namespace _Project.Logic.Core
{
    public class Plant : Useable
    {
        public override Useable Use(Slot slot)
        {
            return this;
        }
    }
}