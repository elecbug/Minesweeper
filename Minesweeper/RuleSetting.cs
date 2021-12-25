using System;
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
    public partial class RuleSetting : Form
    {
        public RuleSetting()
        {
            InitializeComponent();
        }

        private void ButtonOk_Click(object sender, EventArgs e)
        {
            try 
            {
                if (int.Parse(TextWidth.Text) < 1 || int.Parse(TextHeight.Text) < 1 || (int.Parse(TextBombs.Text) > int.Parse(TextWidth.Text) * int.Parse(TextHeight.Text) && int.Parse(TextBombs.Text) < 1))
                {
                    return;
                }

                Class.main.ValueControl(int.Parse(TextWidth.Text), int.Parse(TextHeight.Text), int.Parse(TextBombs.Text));

                this.Close();
            }
            catch
            {
                return;
            }
        }
    }
}
