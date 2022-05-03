using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using checker_game.Model;
using checker_game.Persistence;

namespace checker_game.ViewModel
{
    public class GameViewModel : ViewModelBase
    {
        #region Private variables
        private GameModel _model;
        private Boolean _selected = false;
        private int _x;
        private int _y;
        #endregion
        #region Properties
        public Player ActualPlayer { get { return _model.ActualPlayer; } }
        public DelegateCommand StepCommand { get; private set; }
        public DelegateCommand NewGameCommand{ get; private set;}
        public DelegateCommand LoadGameCommand { get; private set; }
        public DelegateCommand SaveGameCommand { get; private set; }
        public ObservableCollection<GameField> Fields { get; set; }
        #endregion
        #region Events
        public event EventHandler NewGame;
        public event EventHandler LoadGame;
        public event EventHandler SaveGame;
        #endregion
        #region Constructor
        public GameViewModel(GameModel model)
        {
            _model = model;
            _model.GameCreated += new EventHandler(Model_GameCreated);
            NewGameCommand = new DelegateCommand(param => onNewGame());
            LoadGameCommand = new DelegateCommand(param => onLoadGame());
            SaveGameCommand = new DelegateCommand(param => onSaveGame());
            Fields = new ObservableCollection<GameField>();
            for (Int32 i = 0; i < _model.GameTable.Size; i++) // inicializáljuk a mezőket
            {
                for (Int32 j = 0; j < _model.GameTable.Size; j++)
                {
                    FieldImage img = FieldImage.NONE;
                    if(_model.GameTable.GetLevel(i,j)==1 && _model.GameTable.GetValue(i, j) == 1)
                    {
                        img = FieldImage.PlayerWhiteOne;
                    }
                    if (_model.GameTable.GetLevel(i, j) == 1 && _model.GameTable.GetValue(i, j) == 2)
                    {
                        img = FieldImage.PlayerBlackOne;
                    }
                    if (_model.GameTable.GetLevel(i, j) == 2 && _model.GameTable.GetValue(i, j) == 1)
                    {
                        img = FieldImage.PlayerWhiteTwo;
                    }
                    if (_model.GameTable.GetLevel(i, j) == 2 && _model.GameTable.GetValue(i, j) == 2)
                    {
                        img = FieldImage.PlayerBlackTwo;
                    }
                    Fields.Add(new GameField
                    {
                        Text = String.Empty,
                        Img = img,
                        X = i,
                        Y = j,
                        Color =((j%2)+i)%2,
                        Number = i * _model.GameTable.Size + j,
                        StepCommand = new DelegateCommand(param => Step(Convert.ToInt32(param)))
                    }) ;
                }
            }

            RefreshTable();
        }
        #endregion
        #region GameViewModel methods
        public void RefreshTable()
        {
            foreach (GameField field in Fields)
            {
                field.Img = FieldImage.NONE;
                if (_model.GameTable.GetLevel(field.X, field.Y) == 1 && _model.GameTable.GetValue(field.X, field.Y) == 1)
                {
                    field.Img = FieldImage.PlayerWhiteOne;
                }
                if (_model.GameTable.GetLevel(field.X, field.Y) == 1 && _model.GameTable.GetValue(field.X, field.Y) == 2)
                {
                    field.Img = FieldImage.PlayerBlackOne;
                }
                if (_model.GameTable.GetLevel(field.X, field.Y) == 2 && _model.GameTable.GetValue(field.X, field.Y) == 1)
                {
                    field.Img = FieldImage.PlayerWhiteTwo;
                }
                if (_model.GameTable.GetLevel(field.X, field.Y) == 2 && _model.GameTable.GetValue(field.X, field.Y) == 2)
                {
                    field.Img = FieldImage.PlayerBlackTwo;
                }
            }
            OnPropertyChanged("ActualPlayer");
        }

        private void Step(Int32 index)
        {
            GameField btn = Fields[index];
            int x = btn.X;
            int y = btn.Y;
            if (_selected)
            {
                if (_model.GetValue(x, y) == 0)
                {
                    _model.Step(_x, _y, x, y);
                    _selected = false;
                }
            }
            else
            {
                if (_model.GetValue(x, y) == 1 && _model.ActualPlayer == Player.PlayerWhite)
                {
                    _x = x;
                    _y = y;
                    _selected = true;
                }
                if (_model.GetValue(x, y) == 2 && _model.ActualPlayer == Player.PlayerBlack)
                {
                    _x = x;
                    _y = y;
                    _selected = true;
                }
            }
            RefreshTable();
        }


        private void Model_GameCreated(object sender, EventArgs e)
        {
            RefreshTable();
        }
        #endregion
        #region Event methods
        private void onNewGame()
        {
            if (NewGame != null)
                NewGame(this, EventArgs.Empty);
        }
        private void onLoadGame()
        {
            if (LoadGame != null)
                LoadGame(this, EventArgs.Empty);
        }
        private void onSaveGame()
        {
            if (SaveGame != null)
                SaveGame(this, EventArgs.Empty);
        }
        #endregion
    }
}
