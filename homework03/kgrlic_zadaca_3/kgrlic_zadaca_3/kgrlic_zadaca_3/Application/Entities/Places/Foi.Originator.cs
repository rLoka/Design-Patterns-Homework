using System.Collections.Generic;

namespace kgrlic_zadaca_3.Application.Entities.Places
{
    partial class Foi
    {

        public FoiMemento CreateMemento()
        {
            List<Place> state = new List<Place>();

            foreach (var place in Places)
            {
                state.Add(place.Clone());
            }

            return new FoiMemento(state);
        }
            
        public void SetMemento(FoiMemento foiMemento)
        {
            Places = foiMemento.Places;
        }
    }
}
