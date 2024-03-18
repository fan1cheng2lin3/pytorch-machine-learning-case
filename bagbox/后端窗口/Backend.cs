using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace bagbox
{
    public partial class Backend : Form
    {
        public static string StrValue22 = string.Empty;

        public Backend()
        {
            InitializeComponent();
        }

        private void 修改密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Backend_xiugaimima t1 = new Backend_xiugaimima();
            t1.ShowDialog();
        }

        private void 开始ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 注销账户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StrValue22 = Login.StrValue11;
            Front_zuxiao t1 = new Front_zuxiao();
            t1.ShowDialog();
        }

        private void Backend_Load(object sender, EventArgs e)
        {

        }
    }
}
