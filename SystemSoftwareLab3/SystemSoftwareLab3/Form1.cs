using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace _1SPZv1
{
    public partial class Form1 : Form
    {
        readonly List<Task> _tasks = new List<Task>();
        readonly List<int> _filo = new List<int>();
        int _processingTask = -1;
        int _currentTick;

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
                _filo.Add(_tasks.Count);
                _tasks.Add(new Task(_currentTick));
            }

            if (_processingTask == -1)
                if (_filo.Count == 0)
                    return;
                else
                {
                    _processingTask = _filo.Last();
                    _filo.Remove(_processingTask);
                }

            if (_tasks[_processingTask].Processing(_currentTick))
            {
                _processingTask = -1;
                if (_filo.Count != 0)
                {
                    _processingTask = _filo.Last();
                    _filo.Remove(_processingTask);
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
                waitTime += (_tasks[i].GetFinishTick() == -1)
                    ? (_currentTick - _tasks[i].GetCreateTick())
                    : _tasks[i].GetFinishTick() - _tasks[i].GetCreateTick() - _tasks[i].GetWorkTime();
                label9.Text = (_processingTask != -1)? _processingTask.ToString(): "*";
                listView1.Items.Add(i.ToString());
                listView1.Items[i].SubItems.Add(_tasks[i].GetCreateTick().ToString());
                listView1.Items[i].SubItems.Add(_tasks[i].GetWorkTime().ToString());
                listView1.Items[i].SubItems.Add(((_tasks[i].GetFinishTick() == -1)? (_currentTick - _tasks[i].GetCreateTick()): _tasks[i].GetFinishTick() - _tasks[i].GetCreateTick() - _tasks[i].GetWorkTime()).ToString());
                listView1.Items[i].SubItems.Add((_tasks[i].GetFinishTick() == -1)? "Not finished yet": _tasks[i].GetFinishTick().ToString());
                listView1.Items[i].SubItems.Add(_tasks[i].IsCoplited()? "Completed": (i == _currentTick)? "Working": "Waiting");
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
    }
}
