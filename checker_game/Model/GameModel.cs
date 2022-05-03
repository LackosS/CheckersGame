using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using checker_game.Persistence;

namespace checker_game.Model
{
    
    public class GameModel
    {
        #region Private variables
        private ModelTable _gameTable;
        private IModelDataAccess _dataAccess;
        #endregion
        #region Properties
        public ModelTable GameTable { get => _gameTable; set => _gameTable = value; }
        public Int32 White { get=>_gameTable.White; set=>_gameTable.White=value; }
        public Int32 Black { get=>_gameTable.Black; set=>_gameTable.Black=value; }
        public Player ActualPlayer { get=>_gameTable.ActualPlayer; set=>_gameTable.ActualPlayer=value; }
        public Boolean IsOver { get => (_gameTable.Black == 0 || _gameTable.White == 0); }
        public int GetValue(Int32 x, Int32 y){return _gameTable[x, y];}
        #endregion
        #region Events
        public event Action<Player> GameOver;
        public event EventHandler GameCreated;
        #endregion
        #region Constructor
        public GameModel(IModelDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
            _gameTable = new ModelTable();
            ActualPlayer = Player.PlayerWhite;
        }
        #endregion
        #region Game Methods
        public void Step(int xP, int yP, int xT, int yT)
        {
            if (IsOver)
            {
                return;
            }
            if (_gameTable.GetLevel(xP, yP) == 1)
            {
                StepLevelOne(xP, yP, xT, yT);
            }
            if (_gameTable.GetLevel(xP, yP) == 2)
            {
                StepLevelTwo(xP, yP, xT, yT);
            }
            if (IsOver)
            {
                GameOver?.Invoke(ActualPlayer);
            }
        }
        private void StepLevelOne(int xP, int yP, int xT, int yT)
        {
            if (ActualPlayer == Player.PlayerWhite)
            {
                if (_gameTable.GetValue(xT, yT) == 0)
                {
                    if ((xT - xP) == 1 && Math.Abs(yT - yP) == 1)
                    {
                        _gameTable.SetValue(xT, yT, 1, _gameTable.GetLevel(xP, yP));
                        _gameTable.SetValue(xP, yP, 0, 0);
                        ActualPlayer = Player.PlayerBlack;
                        if (xT == _gameTable.Size - 1)
                        {
                            _gameTable.SetValue(xT, yT, 1, 2);
                        }
                    }
                    else if (_gameTable.GetValue(Math.Sign(xT - xP) + xP, Math.Sign(yT - yP) + yP) == 2 && ((xT - xP) == 2 && Math.Abs(yT - yP) == 2))
                    {
                        _gameTable.SetValue(xT, yT, 1, _gameTable.GetLevel(xP, yP));
                        _gameTable.SetValue(xP, yP, 0, 0);
                        _gameTable.SetValue(Math.Sign(xT - xP) + xP, Math.Sign(yT - yP) + yP, 0, 0);
                        ActualPlayer = Player.PlayerBlack;
                        _gameTable.Black--;
                        if (xT == _gameTable.Size - 1)
                        {
                            _gameTable.SetValue(xT, yT, 1, 2);
                        }
                    }
                }
            }
            if (ActualPlayer == Player.PlayerBlack)
            {
                if (_gameTable.GetValue(xT, yT) == 0)
                {
                    if ((xT - xP) == -1 && Math.Abs(yT - yP) == 1)
                    {
                        _gameTable.SetValue(xT, yT, 2, _gameTable.GetLevel(xP, yP));
                        _gameTable.SetValue(xP, yP, 0, 0);
                        ActualPlayer = Player.PlayerWhite;
                        if (xT == 0)
                        {
                            _gameTable.SetValue(xT, yT, 2, 2);
                        }
                    }
                    else if (_gameTable.GetValue(Math.Sign(xT - xP) + xP, Math.Sign(yT - yP) + yP) == 1 && ((xT - xP) == -2 && Math.Abs(yT - yP) == 2))
                    {
                        _gameTable.SetValue(xT, yT, 2, _gameTable.GetLevel(xP, yP));
                        _gameTable.SetValue(xP, yP, 0, 0);
                        _gameTable.SetValue(Math.Sign(xT - xP) + xP, Math.Sign(yT - yP) + yP, 0, 0);
                        ActualPlayer = Player.PlayerWhite;
                        _gameTable.White--;
                        if (xT == 0)
                        {
                            _gameTable.SetValue(xT, yT, 2, 2);
                        }
                    }
                }
            }
        }
        private void StepLevelTwo(int xP, int yP, int xT, int yT)
        {
            if (ActualPlayer == Player.PlayerWhite)
            {
                if (_gameTable.GetValue(xT, yT) == 0)
                {
                    if (Math.Abs(xT - xP) == 1 && Math.Abs(yT - yP) == 1)
                    {
                        _gameTable.SetValue(xT, yT, 1, _gameTable.GetLevel(xP, yP));
                        _gameTable.SetValue(xP, yP, 0, 0);
                        ActualPlayer = Player.PlayerBlack;
                        if (xT == _gameTable.Size - 1)
                        {
                            _gameTable.SetValue(xT, yT, 1, 2);
                        }
                    }
                    else if (_gameTable.GetValue(Math.Sign(xT - xP) + xP, Math.Sign(yT - yP) + yP) == 2 && (Math.Abs(xT - xP) == 2 && Math.Abs(yT - yP) == 2))
                    {
                        _gameTable.SetValue(xT, yT, 1, _gameTable.GetLevel(xP, yP));
                        _gameTable.SetValue(xP, yP, 0, 0);
                        _gameTable.SetValue(Math.Sign(xT - xP) + xP, Math.Sign(yT - yP) + yP, 0, 0);
                        ActualPlayer = Player.PlayerBlack;
                        _gameTable.Black--;
                        if (xT == _gameTable.Size - 1)
                        {
                            _gameTable.SetValue(xT, yT, 1, 2);
                        }
                    }
                }
            }
            if (ActualPlayer == Player.PlayerBlack)
            {
                if (_gameTable.GetValue(xT, yT) == 0)
                {
                    if (Math.Abs(xT - xP) == 1 && Math.Abs(yT - yP) == 1)
                    {
                        _gameTable.SetValue(xT, yT, 2, _gameTable.GetLevel(xP, yP));
                        _gameTable.SetValue(xP, yP, 0, 0);
                        ActualPlayer = Player.PlayerWhite;
                        if (xT == 0)
                        {
                            _gameTable.SetValue(xT, yT, 2, 2);
                        }
                    }
                    else if (_gameTable.GetValue(Math.Sign(xT - xP) + xP, Math.Sign(yT - yP) + yP) == 1 && (Math.Abs(xT - xP) == 2 && Math.Abs(yT - yP) == 2))
                    {
                        _gameTable.SetValue(xT, yT, 2, _gameTable.GetLevel(xP, yP));
                        _gameTable.SetValue(xP, yP, 0, 0);
                        _gameTable.SetValue(Math.Sign(xT - xP) + xP, Math.Sign(yT - yP) + yP, 0, 0);
                        ActualPlayer = Player.PlayerWhite;
                        _gameTable.White--;
                        if (xT == 0)
                        {
                            _gameTable.SetValue(xT, yT, 2, 2);
                        }

                    }
                }
            }
        }
        private void GenerateFields()
        {
            for (Int32 i = 0; i < _gameTable.Size; i++)
            {
                for (Int32 j = 0; j < _gameTable.Size; j++)
                {
                    _gameTable.SetValue(i, j, 0, 0);
                }
            }

            //PlayerOne - White
            _gameTable.SetValue(0, 0, 1, 1);
            _gameTable.SetValue(0, 2, 1, 1);
            _gameTable.SetValue(0, 4, 1, 1);
            _gameTable.SetValue(0, 6, 1, 1);

            _gameTable.SetValue(1, 1, 1, 1);
            _gameTable.SetValue(1, 3, 1, 1);
            _gameTable.SetValue(1, 5, 1, 1);
            _gameTable.SetValue(1, 7, 1, 1);

            _gameTable.SetValue(2, 0, 1, 1);
            _gameTable.SetValue(2, 2, 1, 1);
            _gameTable.SetValue(2, 4, 1, 1);
            _gameTable.SetValue(2, 6, 1, 1);

            //PlayerTwo - Black
            _gameTable.SetValue(_gameTable.Size - 1, 1, 2, 1);
            _gameTable.SetValue(_gameTable.Size - 1, 3, 2, 1);
            _gameTable.SetValue(_gameTable.Size - 1, 5, 2, 1);
            _gameTable.SetValue(_gameTable.Size - 1, 7, 2, 1);

            _gameTable.SetValue(_gameTable.Size - 2, 0, 2, 1);
            _gameTable.SetValue(_gameTable.Size - 2, 2, 2, 1);
            _gameTable.SetValue(_gameTable.Size - 2, 4, 2, 1);
            _gameTable.SetValue(_gameTable.Size - 2, 6, 2, 1);

            _gameTable.SetValue(_gameTable.Size - 3, 1, 2, 1);
            _gameTable.SetValue(_gameTable.Size - 3, 3, 2, 1);
            _gameTable.SetValue(_gameTable.Size - 3, 5, 2, 1);
            _gameTable.SetValue(_gameTable.Size - 3, 7, 2, 1);
        }
        #endregion
        #region Menu Methods
        public void NewGame()
        {
            _gameTable = new ModelTable();
            ActualPlayer = Player.PlayerWhite;
            White = 12;
            Black = 12;
            GenerateFields();
        }
        public async Task LoadGame(String path)
        {
            if (_dataAccess == null)
                throw new InvalidOperationException("No data access is provided.");

            _gameTable = await _dataAccess.LoadAsync(path);
            ActualPlayer = _gameTable.ActualPlayer;
        }

        public async Task SaveGame(String path)
        {
            if (_dataAccess == null)
                throw new InvalidOperationException("No data access is provided.");

            await _dataAccess.SaveAsync(path, _gameTable);
        }
        #endregion
        #region Event methods
        private void OnGameCreated()
        {
            if (GameCreated != null)
                GameCreated(this, EventArgs.Empty);
        }
        #endregion
    }
}
