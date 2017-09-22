using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Transactions;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rental_UI
{
    public partial class fmRent : Form
    {       
        DafestyEntities context;
        public fmRent()
        {
            InitializeComponent();
            context =  new Rental_UI.DafestyEntities();
        }
          

        private void Form1_Load(object sender, EventArgs e)
        {
            dtpDue.Value = dtpDue.Value.AddDays(3);
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            using (fmCustSearch f = new fmCustSearch())
            {
                f.ShowDialog();
                if (f.DialogResult == DialogResult.OK)
                {
                    txtID.Text = f.GetID;
                    lblCustName.Text = context.Customers.Where(x =>
                            x.CustomerID == txtID.Text).First().CustomerName;
                    lblCustName.ForeColor = Color.Black;
                }                   
            }
        }

        private void btnVideo_Click(object sender, EventArgs e)
        {
            using (fmVidSearch f = new fmVidSearch())
            {
                f.ShowDialog();
                txtVidCode.Text = f.DialogResult == DialogResult.OK ? f.GetVideoCode : txtID.Text;
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            using (System.Transactions.TransactionScope ts = new TransactionScope())
            {
                IssueTran it = new IssueTran();
                //it.TransactionID = context.IssueTrans.Last().TransactionID;
                it.CustomerID = txtID.Text;
                it.VideoCode = Int16.Parse(txtVidCode.Text);
                it.DateIssue = dtpIssue.Value.Date;
                it.DateDue = dtpDue.Value.Date;
                it.Remarks = txtRemarks.Text;
                it.RentalStatus = "OUT";

                Movie m = context.Movies.Where(x => x.VideoCode == it.VideoCode).First();
                m.TotalStock -= 1;
                m.NumberRented += 1;
                context.IssueTrans.Add(it);

                context.SaveChanges();

                MessageBox.Show("rented");

                ts.Complete();
            }
            
        }
        
        private void txtID_Leave(object sender, EventArgs e)
        {
            try
            {
                lblCustName.Text = context.Customers.Where(x =>
                            x.CustomerID == txtID.Text).First().CustomerName;
                lblCustName.ForeColor = Color.Black;
            }
            catch (Exception ex)
            {
                lblCustName.Text = "No records found!";
                lblCustName.ForeColor = Color.Red;
            }
        }

        private void txtID_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    lblCustName.Text = context.Customers.Where(x =>
                                x.CustomerID == txtID.Text).First().CustomerName;
                    lblCustName.ForeColor = Color.Black;
                }
            }
            catch (Exception ex)
            {
                lblCustName.Text = "No records found!";
                lblCustName.ForeColor = Color.Red;
            }
        }
    }
}
