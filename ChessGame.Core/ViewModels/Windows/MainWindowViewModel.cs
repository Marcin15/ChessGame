using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace ChessGame.Core
{
    public class MainWindowViewModel : BaseViewModel
    {
        public ObservableCollection<IField> FieldsList { get; set; } = new ObservableCollection<IField>(new CollectionMerger().MergeTwoListIntoOne());
        private readonly IManager mFieldHightlightManager = new FieldHightlightManager();
        private readonly IManager mFigureInteractionManager = new FigureInteractionManager();
        private readonly IFigureCreator mFactory = new FigureFactory();
        public ICommand ClickCommand { get; set; }
        public MainWindowViewModel()
        {
            ClickCommand = new RelayCommand(Click);
            mFactory.Create(new List<IField>(FieldsList));
        }
        public void Click(object obj)
        {
            var fieldVM = obj as IField;

            mFigureInteractionManager.Container(fieldVM, FieldsList);
            mFieldHightlightManager.Container(fieldVM, FieldsList);

            Debug.WriteLine($"{fieldVM.IsClicked}");
        }
    }
}
