using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using checker_game.Persistence;

namespace checker_game.ViewModel
{
    public class GameField : ViewModelBase
    {
        #region Private variables
        private String _text;
        private int _color;
        private FieldImage _img;
        #endregion

        #region Properties
        public FieldImage Img
        {
            get { return _img; }
            set
            {
                if (_img != value)
                {
                    _img = value;
                    OnPropertyChanged();
                }
            }
        }
        public int Color
        {
            get { return _color; }
            set
            {
                if (_color != value)
                {
                    _color = value;
                    OnPropertyChanged();
                }
            }
        }
        public String Text
        {
            get { return _text; }
            set
            {
                if (_text != value)
                {
                    _text = value;
                    OnPropertyChanged();
                }
            }
        }
        public Int32 X { get; set; }
        public Int32 Y { get; set; }
        public Int32 Number { get; set; }
        public DelegateCommand StepCommand { get; set; }
        #endregion
    }
}
