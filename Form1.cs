using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cursa_Model
{
    public partial class Form1 : Form
    {
        UInt16 A, B;                //множители
        UInt32 C;                   //произведение
        Byte Cr;                    //счетчик
        UInt16 a;                   //состояние
        bool[] D, Q;                //множество D и Q сигналов
        bool[] y, x;                //Вектор y и x сигналов
        bool[] A_out;
        DataGridView[] dgvArray;
        bool run = true;
        int l = 0;

        public Form1()
        {
            InitializeComponent();
            dgvArray = new[] { dgvA, dgvB, dgvRgA, dgvRgB, dgvRgC, dgvRgCr };
            run = false;
            a = 0;
            Q = new bool[4];
            D = new bool[4];
            Q[0] = false;
            Q[1] = true;
            Q[3] = false;
            Q[2] = false;
            A_out = new bool[8];
            y = new bool[15];
            x = new bool[7];

            //chkBxOUQ0.Checked = true;
            //chkBxOUD1.Checked = true;
            //stateChanged();

        }
        private void setDgvs(DataGridView dgvX, UInt32 valX)
        {
            UInt32 saveValX = valX;

            if (dgvX.Name == "dgvRgC")
            {

                for (Int32 col = dgvX.ColumnCount - 1; col > -1; col--)
                {
                    dgvX[col, 0].Value = valX % 2;
                    valX = valX / 2;
                }
                fillTBbyValue(txBoxC, saveValX);
            }
            else
            {
                for (Int32 col = dgvX.ColumnCount - 1; col > -1; col--)
                {
                    dgvX[col, 0].Value = valX % 2;
                    valX = valX / 2;
                }
            }
        }
        // Получение десятичного значения числа, записанного в DataGridView
        private UInt32 getValue(DataGridView dtGridX)
        {
            UInt32 res = 0;
            for (Byte col = 1; col < dtGridX.ColumnCount; col++)
            {
                res += (UInt32)Math.Pow(2, dtGridX.ColumnCount - col - 1) * UInt32.Parse(dtGridX[col, 0].Value.ToString());
            }
            return res;
        }

        // Запись числа в TextBox
        private void fillTBbyValue(TextBox txBxX, UInt32 valX)
        {
            Double res = 0;
            if (txBxX.Name == "txBoxC")
            {
                valX = valX % (UInt32)Math.Pow(2, 16);
                for (Byte pow = 15; pow >= 0 && pow < 16; pow--)
                {
                    res += valX / (UInt32)Math.Pow(2, pow) * Math.Pow(2, pow - 16);
                    valX = valX % (UInt32)Math.Pow(2, pow);
                }
            }
            else
            {
                valX = valX % (UInt32)Math.Pow(2, 15);
                for (Byte pow = 14; pow >= 0 && pow < 15; pow--)
                {
                    res += valX / (UInt32)Math.Pow(2, pow) * Math.Pow(2, pow - 15);
                    valX = valX % (UInt32)Math.Pow(2, pow);
                }
            }

            if (txBxX.Name == "txBoxC")
            {
                res = Math.Round(res, 10);
                txBxX.Text = res.ToString("0.##########");
            }
            else
            {
                res = Math.Round(res, 5);
                txBxX.Text = res.ToString("0.#####");
            }

            if (dgvRgC[0, 0].Value.ToString() == "1" && txBxX.Name == "txBoxC")
                txBoxC.Text = txBoxC.Text.Insert(0, "-");
        }

        // Изменение значения ячейки исходных данных по клику
        private void dtGridSource_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == 0)
            {
                if ((sender as DataGridView)[e.ColumnIndex, e.RowIndex].Value == "0")
                    (sender as DataGridView)[e.ColumnIndex, e.RowIndex].Value = "1";
                else
                    (sender as DataGridView)[e.ColumnIndex, e.RowIndex].Value = "0";
                //помещение в textBox десятичного значения, введенного в dataGridView
                if ((sender as DataGridView).Name == "dgvA")
                {
                    fillTBbyValue(txBoxA, getValue(sender as DataGridView));
                    if ((sender as DataGridView)[0, 0].Value == "1" && !txBoxA.Text.Equals("0"))
                        txBoxA.Text = txBoxA.Text.Insert(0, "-");
                }
                else if ((sender as DataGridView).Name == "dgvB")
                {
                    fillTBbyValue(txBoxB, getValue(sender as DataGridView));
                    if ((sender as DataGridView)[0, 0].Value == "1" && !txBoxB.Text.Equals("0"))
                        txBoxB.Text = txBoxB.Text.Insert(0, "-");
                }
            }
        }


        private void rdBtnStep_CheckedChanged(object sender, EventArgs e)
        {
            btnStart.Text = " Такт";
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void rdBtnAuto_CheckedChanged(object sender, EventArgs e)
        {
            btnStart.Text = "Пуск";
        }
        private void rdBtnMP_CheckedChanged(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void rdBtnOU_CheckedChanged(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
        }
        private void dtGridBindingContextChanged(object sender, EventArgs e)
        {
            for (Byte col = 0; col < (sender as DataGridView).Columns.Count; col++)
            {
                (sender as DataGridView)[col, 0].Value = "0";
            }
        }
    }

       
}

