using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class Main : Form
    {
        private int size_x, size_y, hide_num;
        private int[,] map_data;
        private bool[,] map_maybe;
        private bool[,] map_click;
        private Button[,] map_buttons;
        private readonly int HIDE = -1;
        private int time_secs;

        public Main()
        {
            InitializeComponent();
        }

        private void Minesweeper_Load(object sender, EventArgs e)
        {
            size_x = 10;
            size_y = 10;
            hide_num = 10;

            MapDataSet(true);
        }

        private void MapButtons_Click(object sender, MouseEventArgs e)
        {
            int x = 0, y = 0;
            for (x = 0; x < size_x; x++)
            {
                for (y = 0; y < size_y; y++)
                {
                    if ((Button)sender == map_buttons[x, y])
                    {
                        goto Break;
                    }
                }
            }

        Break:
            if (e.Button == MouseButtons.Left && !map_maybe[x, y])
            {
                MouseNomalClick(x, y);
            }
            else if (e.Button == MouseButtons.Right && !map_click[x, y])
            {
                if (!map_maybe[x, y])
                {
                    map_maybe[x, y] = true;
                    map_buttons[x, y].Text = "B";
                    map_buttons[x, y].BackColor = Color.Yellow;
                }
                else
                {
                    map_maybe[x, y] = false;
                    map_buttons[x, y].Text = "";
                    map_buttons[x, y].BackColor = Color.White;
                }
            }

            ClearCheak();
        }

        private void MenuNewGame_Click(object sender, EventArgs e)
        {
            MapDataSet(false);
        }

        private void MenuRuleSetting_Click(object sender, EventArgs e)
        {
            Class.main = this;
            RuleSetting set = new RuleSetting();
            set.Show();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            LabelTime.Text = $"Time: {TimeToString(time_secs++)}";
        }

        private void ClearCheak()
        {
            bool clear = true;

            for (int x = 0; x < size_x; x++)
            {
                for (int y = 0; y < size_y; y++)
                {
                    if (!((!map_click[x, y] && map_data[x, y] == HIDE) || (map_click[x, y] && map_data[x, y] != HIDE)))
                    {
                        clear = false;
                        goto Next;
                    }
                }
            }

        Next:
            if (clear)
            {
                Timer.Stop();
                MessageBox.Show("You didn't touch bomb.\nTry again?", "Wow!", MessageBoxButtons.OK);

                MapDataSet(false);
            }
        }

        private void EraseEmpty(int x, int y)
        {
            const int CHANGE_EMPTY = 2, CHANGE_NUMBER = 4, POSSIBLE_EMPTY = 1, POSSIBLE_NUMBER = 3, DEFAULT = 0;
            int[,] field = new int[size_x, size_y];
            int[,] field_past = (int[,])field.Clone();

            for (int ix = 0; ix < size_x; ix++)
            {
                for (int iy = 0; iy < size_y; iy++)
                {
                    if (map_data[ix, iy] == 0)
                    {
                        field[ix, iy] = POSSIBLE_EMPTY;
                    }
                }
            }

            field[x, y] = POSSIBLE_EMPTY;

            for (int w = 0; w < size_x; w++)
            {
                for (int h = 0; h < size_y; h++)
                {
                    if ((w + 1 < size_x && field[w + 1, h] == POSSIBLE_EMPTY) ||
                        (w + 1 < size_x && h - 1 >= 0 && field[w + 1, h - 1] == POSSIBLE_EMPTY) ||
                        (h - 1 >= 0 && field[w, h - 1] == POSSIBLE_EMPTY) ||
                        (w - 1 >= 0 && h - 1 >= 0 && field[w - 1, h - 1] == POSSIBLE_EMPTY) ||
                        (w - 1 >= 0 && field[w - 1, h] == POSSIBLE_EMPTY) ||
                        (w - 1 >= 0 && h + 1 < size_y && field[w - 1, h + 1] == POSSIBLE_EMPTY) ||
                        (h + 1 < size_y && field[w, h + 1] == POSSIBLE_EMPTY) ||
                        (w + 1 < size_x && h + 1 < size_y && field[w + 1, h + 1] == POSSIBLE_EMPTY))
                    {
                        if (field[w, h] == DEFAULT)
                        {
                            field[w, h] = POSSIBLE_NUMBER;
                        }
                    }
                }
            }

            field[x, y] = CHANGE_EMPTY;

            ReTry:
            for (int w = 0; w < size_x; w++)
            {
                for (int h = 0; h < size_y; h++)
                {
                    if ((w + 1 < size_x && field[w + 1, h] == CHANGE_EMPTY) ||
                        (w + 1 < size_x && h - 1 >= 0 && field[w + 1, h - 1] == CHANGE_EMPTY) ||
                        (h - 1 >= 0 && field[w, h - 1] == CHANGE_EMPTY) ||
                        (w - 1 >= 0 && h - 1 >= 0 && field[w - 1, h - 1] == CHANGE_EMPTY) ||
                        (w - 1 >= 0 && field[w - 1, h] == CHANGE_EMPTY) ||
                        (w - 1 >= 0 && h + 1 < size_y && field[w - 1, h + 1] == CHANGE_EMPTY) ||
                        (h + 1 < size_y && field[w, h + 1] == CHANGE_EMPTY) ||
                        (w + 1 < size_x && h + 1 < size_y && field[w + 1, h + 1] == CHANGE_EMPTY))
                    {
                        if (field[w, h] == POSSIBLE_EMPTY)
                        {
                            field[w, h] = CHANGE_EMPTY;
                        }
                    }
                }
            }

            if (!IsEqualArray(field_past, field))
            {
                field_past = (int[,])field.Clone();
                goto ReTry;
            }

            for (int w = 0; w < size_x; w++)
            {
                for (int h = 0; h < size_y; h++)
                {
                    if ((w + 1 < size_x && field[w + 1, h] == CHANGE_EMPTY) ||
                        (w + 1 < size_x && h - 1 >= 0 && field[w + 1, h - 1] == CHANGE_EMPTY) ||
                        (h - 1 >= 0 && field[w, h - 1] == CHANGE_EMPTY) ||
                        (w - 1 >= 0 && h - 1 >= 0 && field[w - 1, h - 1] == CHANGE_EMPTY) ||
                        (w - 1 >= 0 && field[w - 1, h] == CHANGE_EMPTY) ||
                        (w - 1 >= 0 && h + 1 < size_y && field[w - 1, h + 1] == CHANGE_EMPTY) ||
                        (h + 1 < size_y && field[w, h + 1] == CHANGE_EMPTY) ||
                        (w + 1 < size_x && h + 1 < size_y && field[w + 1, h + 1] == CHANGE_EMPTY))
                    {
                        if (field[w, h] == POSSIBLE_NUMBER)
                        {
                            field[w, h] = CHANGE_NUMBER;
                        }
                    }
                }
            }

            for (int w = 0; w < size_x; w++)
            {
                for (int h = 0; h < size_y; h++)
                {
                    if (field[w, h] == CHANGE_EMPTY || field[w, h] == CHANGE_NUMBER)
                    {
                        if (map_data[w, h] != 0)
                        {
                            map_buttons[w, h].Text = map_data[w, h].ToString();
                        }
                        map_buttons[w, h].BackColor = Color.YellowGreen;
                        map_click[w, h] = true;
                    }
                }
            }
        }

        private bool IsEqualArray(int[,] a, int[,] b)
        {
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    if (a[i, j] != b[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void MapButtonsSet()
        {
            int button_size = this.PanlGameBoard.Width / size_x;

            for (int y = 0; y < size_y; y++)
            {
                for (int x = 0; x < size_x; x++)
                {
                    map_buttons[x, y] = new Button
                    {
                        Parent = this.PanlGameBoard,
                        Width = button_size,
                        Height = button_size,
                        Location = new Point(button_size * x, button_size * y),
                    };
                    map_buttons[x, y].MouseDown += MapButtons_Click;
                    map_buttons[x, y].Show();
                }
            }
        }

        private void MapDataSet(bool making_new_buttons)
        {
            time_secs = 0;

            if (making_new_buttons)
            {
                map_buttons = new Button[size_x, size_y];
                MapButtonsSet();
            }
            map_data = new int[size_x, size_y];
            map_maybe = new bool[size_x, size_y];
            map_click = new bool[size_x, size_y];

            int rand_x, rand_y;
            for (int i = 0; i < hide_num; i++)
            {
                rand_x = new Random().Next(0, size_x);
                rand_y = new Random().Next(0, size_y) * new Random().Next(1, 100) % size_y;

                if (map_data[rand_x, rand_y] == HIDE)
                {
                    i--;
                    continue;
                }

                map_data[rand_x, rand_y] = HIDE;
            }

            for (int y = 0; y < size_y; y++)
            {
                for (int x = 0; x < size_x; x++)
                {
                    map_buttons[x, y].BackColor = Color.White;
                    map_buttons[x, y].Text = "";
                    
                    if (map_data[x, y] != HIDE)
                    {
                        map_data[x, y] = NearHide(x, y);
                    }
                    string str = map_data[x, y].ToString();
                    if (str[0] != '-')
                    {
                        str = $"+{map_data[x, y]} ";
                    }
                    else
                    {
                        str += " ";
                    }
                    Debug.Write(str);
                }
                Debug.Write("\n");
            }

            Timer.Start();
        }

        private int NearHide(int x, int y)
        {
            bool N = y != 0, S = y != size_y - 1, E = x != size_x - 1, W = x != 0;
            int sum = 0;

            if (!N)
            {
                if (!E)
                {
                    sum = IsHide(x - 1, y) + IsHide(x - 1, y + 1) + IsHide(x, y + 1);
                }
                else if (!W)
                {
                    sum = IsHide(x + 1, y) + IsHide(x + 1, y + 1) + IsHide(x, y + 1);
                }
                else
                {
                    sum = IsHide(x - 1, y) + IsHide(x - 1, y + 1) + IsHide(x, y + 1) + IsHide(x + 1, y) + IsHide(x + 1, y + 1);
                }
            }
            else if (!E)
            {
                if (!S)
                {
                    sum = IsHide(x, y - 1) + IsHide(x - 1, y - 1) + IsHide(x - 1, y);
                }
                else
                {
                    sum = IsHide(x, y - 1) + IsHide(x - 1, y - 1) + IsHide(x - 1, y) + IsHide(x - 1, y + 1) + IsHide(x, y + 1);
                }
            }
            else if (!W)
            {
                if (!S)
                {
                    sum = IsHide(x, y - 1) + IsHide(x + 1, y - 1) + IsHide(x + 1, y);
                }
                else
                {
                    sum = IsHide(x, y - 1) + IsHide(x + 1, y - 1) + IsHide(x + 1, y) + IsHide(x + 1, y + 1) + IsHide(x, y + 1);
                }
            }
            else if (!S)
            {
                sum = IsHide(x - 1, y) + IsHide(x - 1, y - 1) + IsHide(x, y - 1) + IsHide(x + 1, y - 1) + IsHide(x + 1, y);
            }
            else
            {
                sum = IsHide(x, y - 1) + IsHide(x + 1, y - 1) + IsHide(x + 1, y) + IsHide(x + 1, y + 1) 
                    + IsHide(x, y + 1) + IsHide(x - 1, y + 1) + IsHide(x - 1, y) + IsHide(x - 1, y - 1);
            }    
            return sum;
        }

        private int IsHide(int x, int y)
        {
            if (map_data[x, y] == HIDE)
            {
                return 1;
            }
            else
            {
                return 0;
            } 
        }

        private void ShowHide()
        {
            for (int x = 0; x < size_x; x++)
            {
                for (int y = 0; y < size_y; y++)
                {
                    if (map_data[x, y] == HIDE)
                    {
                        map_buttons[x, y].Text = "B";
                        map_buttons[x, y].BackColor = Color.Red;
                    }
                }
            }
        }

        private string TimeToString(int time_secs)
        {
            if (time_secs < 60)
            {
                return $"{time_secs} s...";
            }
            else if (time_secs / 60 < 60)
            {
                return $"{time_secs / 60}m {time_secs % 60}s...";
            }
            else
            {
                return $"{time_secs / 60 / 60}h {time_secs / 60 % 60}m {time_secs % 60}s...";
            }
        }

        private void MouseNomalClick(int x, int y)
        {
            if (map_data[x, y] == HIDE)
            {
                ShowHide();
                Timer.Stop();

                MessageBox.Show("You touched bomb.\nTry again?", "Oops...", MessageBoxButtons.OK);

                MapDataSet(false);
            }
            else if (map_data[x, y] == 0)
            {
                EraseEmpty(x, y);
            }
            else
            {
                map_buttons[x, y].Text = map_data[x, y].ToString();
                map_buttons[x, y].BackColor = Color.YellowGreen;
                map_click[x, y] = true;
            }
        }

        public void ValueControl(int width, int height, int bombs)
        {
            for (int x = 0; x < size_x; x++)
            {
                for (int y = 0; y < size_y; y++)
                {
                    map_buttons[x, y].Hide();
                }
            }

            size_x = width;
            size_y = height;
            hide_num = bombs;

            MapDataSet(true);
        }
    }
}
