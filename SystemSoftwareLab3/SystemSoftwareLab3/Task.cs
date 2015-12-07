using System;

namespace _1SPZv1
{
    public class Task
    {
        public bool Complited;
        public readonly int WorkTime;
        public int StartTick;
        public readonly int CreateTick;
        public int FinishTick;
        public int LeftWorkTime;
        public readonly int Priority;

        private const int MaxDuration = 20;

        public Task(int createTick, int p)
        {
            StartTick = -1;
            FinishTick = -1;
            CreateTick = createTick;
            var rand = new Random();

            WorkTime = rand.Next(1, MaxDuration);
            LeftWorkTime = WorkTime;
            Complited = false;
            Priority = p;
        }

        public bool Processing(int t)
        {
            Complited = LeftWorkTime < 0;
            if (Complited) return Complited;
            if (StartTick == -1)
                StartTick = t;
            if (LeftWorkTime-- == 0)
                FinishTick = t;
            Complited = LeftWorkTime < 0;
            return Complited;
        }
    }
}
