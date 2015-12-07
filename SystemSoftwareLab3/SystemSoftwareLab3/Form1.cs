using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace _1SPZv1
{
    public partial class Form1 : Form
    {
        readonly List<Task> _tasks = new List<Task>();
        readonly List<List<int>> _filo = new List<List<int>>();
        int _processingTask = -1;
        int _currentTick;
        public readonly int PrioritySizeOfQp = 10;

        public Form1()
        {
            InitializeComponent();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label2.Text = trackBar1.Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var rand = new Random();

            if (rand.Next(100) < Convert.ToInt32(label2.Text))
            {
                var q = new List<int>();
                var p = rand.Next(PrioritySizeOfQp);
                q.Add(_tasks.Count);
                q.Add(p);
                _filo.Add(q);
                _tasks.Add(new Task(_currentTick, p));
            }

            if (_processingTask == -1)
                if (_filo.Count == 0)
                    return;
                else
                {
                    _processingTask = _filo[0][0];
                    _filo.RemoveAt(0);
                }

            if (_tasks[_processingTask].Processing(_currentTick))
            {
                _processingTask = -1;
                if (_filo.Count != 0)
                {
                    _processingTask = _filo[0][0];
                    _filo.RemoveAt(0);
                }
            }

            label8.Text = _currentTick.ToString();
            PrintTasks();
            _currentTick++;
        }

        public void PrintTasks()
        {
            var waitTime = 0;
            listView1.Items.Clear();
            for (var i = 0; i < _tasks.Count; i++)
            {
                waitTime += (_tasks[i].FinishTick == -1)
                    ? (_currentTick - _tasks[i].CreateTick)
                    : _tasks[i].FinishTick - _tasks[i].CreateTick - _tasks[i].WorkTime;
                label9.Text = (_processingTask != -1)? _processingTask.ToString(): "*";
                listView1.Items.Add(i.ToString());
                listView1.Items[i].SubItems.Add(_tasks[i].CreateTick.ToString());
                listView1.Items[i].SubItems.Add(_tasks[i].WorkTime.ToString());
                listView1.Items[i].SubItems.Add(((_tasks[i].FinishTick == -1)? (_currentTick - _tasks[i].CreateTick): _tasks[i].FinishTick - _tasks[i].CreateTick - _tasks[i].WorkTime).ToString());
                listView1.Items[i].SubItems.Add((_tasks[i].FinishTick == -1)? "Not finished yet": _tasks[i].FinishTick.ToString());
                listView1.Items[i].SubItems.Add(_tasks[i].Complited? "Completed": (i == _currentTick)? "Working": "Waiting");
                listView1.EnsureVisible(i);
            }
            label5.Text = (_tasks.Count == 0? 0: waitTime / _tasks.Count).ToString() + @" ms";
            label3.Text = _filo.Count.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            _filo.Clear();
            _tasks.Clear();
            listView1.Items.Clear();
            _processingTask = -1;
            _currentTick = 0;
            label3.Text = @"0";
            label5.Text = @"0";
            label8.Text = @"0";
            label9.Text = @"0";
        }

        public void AddElementToPq(List<int> q)
        {
            for (var i = 0; i < _filo.Count; i++)
                if (q[1] <= _filo[i][1])
                {
                    _filo.Insert(i, q);
                    return;
                }
            _filo.Add(q);
        }
    }
}
