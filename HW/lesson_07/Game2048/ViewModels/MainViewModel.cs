using Commands;
using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Input;

namespace ViewModels
{
    public class MainViewModel : AViewModel
    {
        private const int DefaultValue = 0;
        private const int HigherValueChange = 10;
        private const int InitialCount = 2;
        private const int Size = 4;

        private readonly int[,] cells = new int[Size, Size];
        private readonly ICommand loadedCommand;
        private readonly ICommand moveDownCommand;
        private readonly ICommand moveLeftCommand;
        private readonly ICommand moveRightCommand;
        private readonly ICommand moveUpCommand;
        private readonly ICommand newGameCommand;
        private readonly Random random = new Random();

        private int highScore;
        private int score;

        public MainViewModel()
        {
            loadedCommand = new DelegateCommand(Loaded);
            moveDownCommand = new DelegateCommand(MoveDown);
            moveLeftCommand = new DelegateCommand(MoveLeft);
            moveRightCommand = new DelegateCommand(MoveRight);
            moveUpCommand = new DelegateCommand(MoveUp);
            newGameCommand = new DelegateCommand(NewGame);
        }

        public int Cell00
        {
            get => cells[0, 0];
            set => SetProperty(ref cells[0, 0], value);
        }

        public int Cell01
        {
            get => cells[0, 1];
            set => SetProperty(ref cells[0, 1], value);
        }

        public int Cell02
        {
            get => cells[0, 2];
            set => SetProperty(ref cells[0, 2], value);
        }

        public int Cell03
        {
            get => cells[0, 3];
            set => SetProperty(ref cells[0, 3], value);
        }

        public int Cell10
        {
            get => cells[1, 0];
            set => SetProperty(ref cells[1, 0], value);
        }

        public int Cell11
        {
            get => cells[1, 1];
            set => SetProperty(ref cells[1, 1], value);
        }

        public int Cell12
        {
            get => cells[1, 2];
            set => SetProperty(ref cells[1, 2], value);
        }

        public int Cell13
        {
            get => cells[1, 3];
            set => SetProperty(ref cells[1, 3], value);
        }

        public int Cell20
        {
            get => cells[2, 0];
            set => SetProperty(ref cells[2, 0], value);
        }

        public int Cell21
        {
            get => cells[2, 1];
            set => SetProperty(ref cells[2, 1], value);
        }

        public int Cell22
        {
            get => cells[2, 2];
            set => SetProperty(ref cells[2, 2], value);
        }

        public int Cell23
        {
            get => cells[2, 3];
            set => SetProperty(ref cells[2, 3], value);
        }

        public int Cell30
        {
            get => cells[3, 0];
            set => SetProperty(ref cells[3, 0], value);
        }

        public int Cell31
        {
            get => cells[3, 1];
            set => SetProperty(ref cells[3, 1], value);
        }

        public int Cell32
        {
            get => cells[3, 2];
            set => SetProperty(ref cells[3, 2], value);
        }

        public int Cell33
        {
            get => cells[3, 3];
            set => SetProperty(ref cells[3, 3], value);
        }

        public int HighScore
        {
            get => highScore;
            set => SetProperty(ref highScore, value);
        }

        public ICommand LoadedCommand => loadedCommand;

        public ICommand MoveDownCommand => moveDownCommand;

        public ICommand MoveLeftCommand => moveLeftCommand;

        public ICommand MoveRightCommand => moveRightCommand;

        public ICommand MoveUpCommand => moveUpCommand;

        public ICommand NewGameCommand => newGameCommand;

        public int Score
        {
            get => score;
            set => SetProperty(ref score, value);
        }

        private int CreateValue() => random.Next(0, 100) < HigherValueChange ? 4 : 2;

        private void Loaded() => NewGame();

        private void Move(Func<bool[,], bool> moveMethod)
        {
            var created = new bool[Size, Size];
            var moved = false;

            while (true)
            {
                if (moveMethod(created))
                {
                    moved = true;
                }
                else
                {
                    break;
                }
            }

            if (moved)
            {
                TryCreateNewTile();
            }
        }

        private void MoveDown()
        {
            Move(created =>
            {
                var moved = false;

                for (var row = Size - 2; row >= 0; --row)
                {
                    for (var column = 0; column < Size; ++column)
                    {
                        var oldTile = new Tile(row, column);
                        var newTile = new Tile(row + 1, column);

                        if (TryMoveToTile(oldTile, newTile, created))
                        {
                            moved = true;
                        }
                    }
                }

                return moved;
            });
        }

        private void MoveLeft()
        {
            Move(created =>
            {
                var moved = false;

                for (var row = 0; row < Size; ++row)
                {
                    for (var column = 1; column < Size; ++column)
                    {
                        var oldTile = new Tile(row, column);
                        var newTile = new Tile(row, column - 1);

                        if (TryMoveToTile(oldTile, newTile, created))
                        {
                            moved = true;
                        }
                    }
                }

                return moved;
            });
        }

        private void MoveRight()
        {
            Move(created =>
            {
                var moved = false;

                for (var row = 0; row < Size; ++row)
                {
                    for (var column = Size - 2; column >= 0; --column)
                    {
                        var oldTile = new Tile(row, column);
                        var newTile = new Tile(row, column + 1);

                        if (TryMoveToTile(oldTile, newTile, created))
                        {
                            moved = true;
                        }
                    }
                }

                return moved;
            });
        }

        private void MoveToTile(Tile oldTile, Tile newTile, int value, bool[,] created)
        {
            SetCell(newTile, value);
            SetCell(oldTile, DefaultValue);

            if (created[oldTile.Row, oldTile.Column])
            {
                created[oldTile.Row, oldTile.Column] = false;
                created[newTile.Row, newTile.Column] = true;
            }
        }

        private void MoveUp()
        {
            Move(created =>
            {
                var moved = false;

                for (var row = 1; row < Size; ++row)
                {
                    for (var column = 0; column < Size; ++column)
                    {
                        var oldTile = new Tile(row, column);
                        var newTile = new Tile(row - 1, column);

                        if (TryMoveToTile(oldTile, newTile, created))
                        {
                            moved = true;
                        }
                    }
                }

                return moved;
            });
        }

        private void NewGame()
        {
            for (var row = 0; row < Size; ++row)
            {
                for (var column = 0; column < Size; ++column)
                {
                    SetCell(new Tile(row, column), DefaultValue);
                }
            }

            for (var count = 0; count < InitialCount; ++count)
            {
                TryCreateNewTile();
            }

            Score = 0;
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.PropertyName.Equals(nameof(Score)))
                if (Score > HighScore)
                    HighScore = Score;
        }

        private void SetCell(Tile tile, int value)
        {
            var propertyName = $"Cell{tile.Row}{tile.Column}";
            PropertyInfo property = GetType().GetProperty(propertyName);
            property.SetValue(this, value);
        }

        private bool TryCreateNewTile()
        {
            var tiles = new List<Tile>();

            for (var row = 0; row < Size; ++row)
                for (var column = 0; column < Size; ++column)
                    if (cells[row, column].Equals(DefaultValue))
                        tiles.Add(new Tile(row, column));

            bool canCreateNewTile = tiles.Count > 0;
            if (canCreateNewTile)
            {
                var newTileIndex = random.Next(tiles.Count);

                Tile newTile = tiles[newTileIndex];
                int value = CreateValue();

                SetCell(newTile, value);
            }

            return canCreateNewTile;
        }

        private bool TryMoveToTile(Tile oldTile, Tile newTile, bool[,] created)
        {
            var moved = false;

            int oldValue = cells[oldTile.Row, oldTile.Column];
            if (!oldValue.Equals(DefaultValue))
            {
                int newValue = cells[newTile.Row, newTile.Column];

                if (newValue.Equals(DefaultValue))
                {
                    MoveToTile(oldTile, newTile, oldValue, created);
                    moved = true;
                }
                else if (newValue.Equals(oldValue) && !created[newTile.Row, newTile.Column] && !created[oldTile.Row, oldTile.Column])
                {
                    int increasedValue = oldValue * 2;

                    MoveToTile(oldTile, newTile, increasedValue, created);
                    created[newTile.Row, newTile.Column] = true;
                    moved = true;

                    Score += increasedValue;
                }
            }

            return moved;
        }
    }
}