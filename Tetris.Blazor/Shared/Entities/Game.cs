using System.Timers;
using Timer = System.Timers.Timer;

namespace Tetris.Blazor.Shared.Entities;

public class Game
{
  public event Action Updated;
  public event Action ScoreUpdated;
  public event Action GameOver;
  public Field Field { get; }

  private readonly Timer _timer;
  private long _interval;
  private TimeSpan _gameSpeed;
  private DateTime _lastUpdateTime = DateTime.MinValue;

  private int _score = 0;

  private readonly Random _random = new Random();
  private readonly List<Func<Figure>> _pool;

  public int Score
  {
    get => _score;
    set
    {
      _score = value;
      ScoreUpdated?.Invoke();
    }
  }

  private InputMap _inputMap;

  private Figure? _currentFigure;

  public Game()
  {
    Field = new Field(24, 10);

    _interval = 750;
    _gameSpeed = TimeSpan.FromMilliseconds(_interval);

    _inputMap = new InputMap();

    _pool = new List<Func<Figure>>()
    {
      () => new O(), () => new I(), () => new J(), () => new L(), () => new S(), () => new Z(), () => new T()
    };

    _timer = new Timer();
    _timer.Interval = 50;
    _timer.Elapsed += TimerOnElapsed;
    _timer.Enabled = true;
  }

  private void TimerOnElapsed(object? sender, ElapsedEventArgs e)
  {
    var now = DateTime.UtcNow;

    CheckForGameOver();
    CheckRowsToEliminate();

    var delta = now - _lastUpdateTime;
    if (delta < _gameSpeed)
      return;

    MoveDown();

    Updated?.Invoke();
    _lastUpdateTime = now;
  }

  private void CheckRowsToEliminate()
  {
    if (_currentFigure == null || !IsLanded(_currentFigure))
      return;

    _currentFigure = Instantiate();

    var eliminated = EliminateFullRows();
    Score += eliminated.Length;
    _interval -= eliminated.Length * 10;
    _gameSpeed = TimeSpan.FromMilliseconds(_interval);
    Updated?.Invoke();
  }

  private void CheckForGameOver()
  {
    if (!Field.GetRow(3).Any(x => !x.IsFreeFor(_currentFigure)))
      return;

    GameOver?.Invoke();
    _timer.Enabled = false;
    _currentFigure = null;
  }

  private void MoveDown()
  {
    if (_currentFigure is not null)
      PerformMovement(MoveType.Down);
    else
      _currentFigure = Instantiate();
  }

  private int[] EliminateFullRows()
  {
    var rowsToEliminate = new List<int>();
    for (int i = Field.RowCount - 1; i >= 0; i--)
    {
      var row = Field.GetRow(i);
      var isFull = true;
      foreach (var cell in row)
        isFull &= cell.State == State.Filled;

      if (isFull)
        rowsToEliminate.Add(i);
    }

    if (rowsToEliminate.Any())
      Field.Eliminate(rowsToEliminate.ToArray());

    return rowsToEliminate.ToArray();
  }

  public void PerformMovement(MoveType moveType)
  {
    var figure = _currentFigure;

    var nextPos = moveType switch
    {
      MoveType.Left => Figure.NextLeftPosition(figure.Position),
      MoveType.Right => Figure.NextRightPosition(figure.Position),
      MoveType.Down => Figure.NextDownPosition(figure.Position),
      MoveType.Ground => GroundPosition(figure),
      MoveType.Rotate => Figure.NextRotation(figure.Position),
      _ => Array.Empty<Position>()
    };

    if (nextPos is { Length:>0 })
      ApplyPosition(figure, nextPos);
  }

  private Position[] GroundPosition(Figure figure)
  {
    var currentPos = Array.Empty<Position>();
    var nextPos = figure.Position;

    while (currentPos != nextPos)
    {
      currentPos = nextPos;
      nextPos = Figure.NextDownPosition(currentPos);

      var freeCells = Field
        .GetBy(nextPos)
        .Where(x => x.IsFreeFor(figure))
        .ToList();

      if (freeCells is not { Count: 4 })
        break;
    }

    return currentPos;
  }

  private void ApplyPosition(Figure figure, Position[] nextPos)
  {
    var newCells = Field
      .GetBy(nextPos)
      .Where(x => x.IsFreeFor(figure))
      .ToList();

    if (newCells.Count != nextPos.Length)
      return;

    Field
      .GetBy(figure.Position)
      .ToList()
      .ForEach(x => x.Release());

    newCells.ForEach(x => x.Occupy(figure));
    figure.SetPosition(nextPos);
  }
  
  private Figure Instantiate()
  {
    var next = _random.Next(0, _pool.Count);
    var @new = _pool[next]();
    var pos = @new.Position;
    var cells = Field.GetBy(pos);
    if (cells.All(x => x.IsFreeFor(@new)))
      ApplyPosition(@new, pos);

    return @new;
  }

  public void HandleInput(string pressedKeyCode)
  {
    var moveType = _inputMap.GetMoveTypeBy(pressedKeyCode);
    if (moveType is null || _currentFigure is null)
      return;

    PerformMovement(moveType.Value);
  }

  public bool IsLanded(Figure figure)
  {
    var currentPos = figure.Position;
    var nextPos = Figure.NextDownPosition(currentPos);
    var cells = Field
      .GetBy(nextPos)
      .Where(x => x.IsFreeFor(figure))
      .ToList();

    return cells.Count != nextPos.Length;
  }
}