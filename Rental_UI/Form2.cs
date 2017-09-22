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
    public partial class fmCustSearch : Form
    {
        DafestyEntities context;        

        public string GetID
        {
            get { return dataGridView1.CurrentRow.Cells[0].Value.ToString(); }
        }

        public fmCustSearch()
        {
            InitializeComponent();
            context = new DafestyEntities();
            dataGridView1.DataSource = context.Customers.ToList();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void txtCustName_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = context.Customers.Where(x => 
                                        x.CustomerName.StartsWith(txtCustName.Text)).ToList();
        }

        private void txtMobile_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = context.Customers.Where(x =>
                                        x.PhoneNumber.StartsWith(txtMobile.Text)).ToList();
        }
    }
}
