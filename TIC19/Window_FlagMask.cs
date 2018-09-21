﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TIC19.MyClass;

namespace TIC19
{
    public partial class Window_FlagMask : Form
    {
        private Form1 mainForm;
        private static bool mIsChecked = false;
        private static bool[] mCheckBoxeItemsSate = new bool[255];

        public Window_FlagMask(Form1 form1)
        {
            InitializeComponent();

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);

            mainForm = form1;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var parms = base.CreateParams;
                parms.Style &= ~0x02000000;  // Turn off WS_CLIPCHILDREN
                return parms;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++) checkedListBox1.SetItemChecked(i, mIsChecked ? false : true);
            mIsChecked = mIsChecked ? false : true;
        }

        private void Window_FlagMask_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }

        private void Window_FlagMask_FormClosed(object sender, FormClosedEventArgs e)
        {
            // using ulong because of big integger
            ulong flagMask = 0;

            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    // item's text
                    string s = checkedListBox1.Items[i].ToString();
                    // flag mask += get number between "[123]"
                    flagMask += Convert.ToUInt64(s.Remove(s.IndexOf(']')).Substring(s.IndexOf('[') + 1));
                }
                mCheckBoxeItemsSate[i] = checkedListBox1.GetItemChecked(i);
            }
            QueryHandler.column_Flags = flagMask;
        }

        private void Window_FlagMask_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
                checkedListBox1.SetItemChecked(i, mCheckBoxeItemsSate[i]);
        }
    }
}
