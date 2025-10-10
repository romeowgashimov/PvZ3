using _Project.Logic.Core;
using Cysharp.Threading.Tasks;

namespace _Project.Logic.Implementation.WindowAnimators
{
    public class DefaultWindowAnimator : WindowAnimator
    {
        public override void Prepare()
        {
            
        }
         
        public override UniTask Show()
        {
            gameObject.SetActive(true);
            return UniTask.CompletedTask;
        }
        
        public override UniTask Hide()
        {
            gameObject.SetActive(false);
            return UniTask.CompletedTask;
        }
    }
}