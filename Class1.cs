using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cursa_Model
{
    class Class1
    {
        private void y0()
        {

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
    }
}
