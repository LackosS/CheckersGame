using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace checkers_game.Persistence
{
    public enum Player { PlayerWhite, PlayerBlack }; //1 and 2
    public class ModelTable
    {
        #region Private variables
        private Int32 _gameSize;
        private Int32[,] _fieldValues;
        private Int32[,] _levels;
        private Player _actualPLayer;
        private Int32 _white = 12;
        private Int32 _black = 12;
        #endregion
        #region Properties
        public Int32 White { get => _white; set => _white = value; }
        public Int32 Black { get => _black; set => _black = value; }
        public Int32 Size { get { return _fieldValues.GetLength(0); } }
        public Int32 this[Int32 x, Int32 y] { get { return GetValue(x, y); } }
        public Player ActualPlayer { get => _actualPLayer; set => _actualPLayer = value; }
        #endregion
        #region Constructor
        public ModelTable() : this(8) { }
        public ModelTable(int gameSize)
        {
            if (gameSize < 0)
                throw new ArgumentOutOfRangeException("The table size is less than 0.", "tableSize");
            _gameSize = gameSize;
            _fieldValues = new Int32[gameSize, gameSize];
            _levels = new int[gameSize, gameSize];
        }
        #endregion
        #region Table Methods
        /// <summary>
        /// Getting a value of a matrix element
        /// </summary>
        public Int32 GetValue(Int32 x, Int32 y)
        {
            if (x < 0 || x >= _fieldValues.GetLength(0))
                throw new ArgumentOutOfRangeException("x", "The X coordinate is out of range.");
            if (y < 0 || y >= _fieldValues.GetLength(1))
                throw new ArgumentOutOfRangeException("y", "The Y coordinate is out of range.");

            return _fieldValues[x, y];
        }
        /// <summary>
        /// Getting a level of a matrix element
        /// </summary>
        public Int32 GetLevel(Int32 x, Int32 y)
        {
            if (x < 0 || x >= _fieldValues.GetLength(0))
                throw new ArgumentOutOfRangeException("x", "The X coordinate is out of range.");
            if (y < 0 || y >= _fieldValues.GetLength(1))
                throw new ArgumentOutOfRangeException("y", "The Y coordinate is out of range.");

            return _levels[x, y];
        }

        /*
         0 empty
         1 white
         2 black 
         */
        /// <summary>
        /// Setting a value of a matrix element
        /// </summary>
        public void SetValue(Int32 x, Int32 y, Int32 value, Int32 level)
        {
            if (x < 0 || x >= _fieldValues.GetLength(0))
                throw new ArgumentOutOfRangeException("x", "The X coordinate is out of range.");
            if (y < 0 || y >= _fieldValues.GetLength(1))
                throw new ArgumentOutOfRangeException("y", "The Y coordinate is out of range.");
            if (value < 0 || value > _fieldValues.GetLength(0) + 1)
                throw new ArgumentOutOfRangeException("value", "The value is out of range.");
            _fieldValues[x, y] = value;
            _levels[x,y] = level;
        }
        /// <summary>
        /// Checking the emptiness of a matrix element
        /// </summary>
        public Boolean IsEmpty(Int32 x, Int32 y)
        {
            if (x < 0 || x >= _fieldValues.GetLength(0))
                throw new ArgumentOutOfRangeException("x", "The X coordinate is out of range.");
            if (y < 0 || y >= _fieldValues.GetLength(1))
                throw new ArgumentOutOfRangeException("y", "The Y coordinate is out of range.");

            return _fieldValues[x, y] == 0;
        }
        #endregion
    }
}
