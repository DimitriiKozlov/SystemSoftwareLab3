using System;

namespace _1SPZv1
{
    class Task
    {
        private bool _complited;
        private int _workTime;
        private int _startTick;
        private readonly int _createTick;
        private int _finishTick;
        private int _leftWorkTime;

        private const int MaxDuration = 20;

        public Task(int createTick)
        {
            _startTick = -1;
            _finishTick = -1;
            _createTick = createTick;
            var rand = new Random();

            _workTime = rand.Next(1, MaxDuration);
            _leftWorkTime = _workTime;
            _complited = false;
        }

        public bool Processing(int t)
        {
            _complited = _leftWorkTime < 0;
            if (_complited) return _complited;
            if (_startTick == -1)
                _startTick = t;
            if (_leftWorkTime-- == 0)
                _finishTick = t;
            _complited = _leftWorkTime < 0;
            return _complited;
        }

        public bool IsCoplited()
        {
            return _complited;
        }

        public int GetWorkTime()
        {
            return _workTime;
        }

        public int GetStartTick()
        {
            return _startTick;
        }

        public int GetCreateTick()
        {
            return _createTick;
        }

        public int GetFinishTick()
        {
            return _finishTick;
        }
    }
}
