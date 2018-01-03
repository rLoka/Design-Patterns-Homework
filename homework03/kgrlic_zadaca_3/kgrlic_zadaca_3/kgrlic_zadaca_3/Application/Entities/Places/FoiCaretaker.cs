namespace kgrlic_zadaca_3.Application.Entities.Places
{
    class FoiCaretaker
    {
        private static FoiCaretaker _foiCaretaker;

        private FoiCaretaker() { }

        public static FoiCaretaker GetInstance()
        {
            return _foiCaretaker ?? (_foiCaretaker = new FoiCaretaker());
        }

        public FoiMemento FoiMemento { set; get; }
    }
}
