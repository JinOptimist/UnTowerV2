using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Script.Interfaces
{
    public interface ICanBeInteracted
    {
        void Interact(HeroStuff whoInteract);
        void SelectAsActive();
        void DeselectAsActive();
    }
}
