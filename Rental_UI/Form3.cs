using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rental_UI
{
    public partial class fmVidSearch : Form
    {
        DafestyEntities context;

        public string GetVideoCode
        {
            get { return dataGridView1.CurrentRow.Cells[0].Value.ToString(); }
        }

        public fmVidSearch()
        {
            InitializeComponent();
            context = new DafestyEntities();
            dataGridView1.DataSource = context.Movies.ToList();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = context.Movies.Where(x =>
                                        x.MovieTitle.StartsWith(txtTitle.Text)).ToList();
        }

        private void txtType_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = context.Movies.Where(x =>
                                        x.MovieType.StartsWith(txtType.Text)).ToList();
        }
    }
}
