using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace _1SPZv1
{
    public partial class Form1 : Form
    {
        List<Task> tasks = new List<Task>();
        List<int> filo = new List<int>();
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
                filo.Add(tasks.Count);
                tasks.Add(new Task(_currentTick));
            }

            if (_processingTask == -1)
                if (filo.Count == 0)
                    return;
                else
                {
                    _processingTask = filo.Last();
                    filo.Remove(_processingTask);
                }

            if (tasks[_processingTask].Processing(_currentTick))
            {
                _processingTask = -1;
                if (filo.Count != 0)
                {
                    _processingTask = filo.Last();
                    filo.Remove(_processingTask);
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
            for (var i = 0; i < tasks.Count; i++)
            {
                waitTime += (tasks[i].GetFinishTick() == -1)
                    ? (_currentTick - tasks[i].GetCreateTick())
                    : tasks[i].GetFinishTick() - tasks[i].GetCreateTick() - tasks[i].GetWorkTime();
                label9.Text = (_processingTask != -1)? _processingTask.ToString(): "*";
                listView1.Items.Add(i.ToString());
                listView1.Items[i].SubItems.Add(tasks[i].GetCreateTick().ToString());
                listView1.Items[i].SubItems.Add(tasks[i].GetWorkTime().ToString());
                listView1.Items[i].SubItems.Add(((tasks[i].GetFinishTick() == -1)? (_currentTick - tasks[i].GetCreateTick()): tasks[i].GetFinishTick() - tasks[i].GetCreateTick() - tasks[i].GetWorkTime()).ToString());
                listView1.Items[i].SubItems.Add((tasks[i].GetFinishTick() == -1)? "Not finished yet": tasks[i].GetFinishTick().ToString());
                listView1.Items[i].SubItems.Add(tasks[i].IsCoplited()? "Completed": (i == _currentTick)? "Working": "Waiting");
                listView1.EnsureVisible(i);
            }
            label5.Text = (tasks.Count == 0? 0: waitTime / tasks.Count).ToString() + " ms";
            label3.Text = filo.Count.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            filo.Clear();
            tasks.Clear();
            listView1.Items.Clear();
            _processingTask = -1;
            _currentTick = 0;
            label3.Text = "0";
            label5.Text = "0";
            label8.Text = "0";
            label9.Text = "0";
        }
    }
}
