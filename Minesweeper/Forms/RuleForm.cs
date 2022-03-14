﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class RuleForm : Form
    {
        public RuleForm()
        {
            InitializeComponent();
        }

        private void Load_RuleSetting(object sender, EventArgs e)
        {
            DataManager.main.Hide();
        }

        private void FormClosed_RuleSetting(object sender, FormClosedEventArgs e)
        {
            DataManager.main.Show();
        }

        private void Click_button_set(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(text_width.Text) < 1 || int.Parse(text_height.Text) < 1 || (int.Parse(text_bombs.Text) > int.Parse(text_width.Text) * int.Parse(text_height.Text) && int.Parse(text_bombs.Text) < 1))
                {
                    return;
                }

                DataManager.main.Set_Values(int.Parse(text_width.Text), int.Parse(text_height.Text), int.Parse(text_bombs.Text));

                this.Close();
            }
            catch
            {
                return;
            }
        }

        private void Click_button_cancel(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
