using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccess;

namespace MyContactList
{
    public partial class MyContactList : Form
    {
        
        public MyContactList()
        {
            InitializeComponent();
            ReadData();
        }
        
        private void MyContactList_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            FullNameTextBox.Text = "";
            EmailTextBox.Text = "";
            PhoneNumberTextBox.Text = "";
            AddressTextBox.Text = "";
            FullNameTextBox.Focus();
        }
        private int UID;
        private string Uname, UEmail, UPhoneNumber, UAddress;

        private void UpdateContactButton_Click(object sender, EventArgs e)
        {
            int id = UID;
            string name = FullNameTextBox.Text;
            string email = EmailTextBox.Text;
            string phone = PhoneNumberTextBox.Text;
            string address = AddressTextBox.Text;
            if(Database.UpdateContact(id, name, email, phone, address))
            {
                MessageBox.Show("Contact Info Updated");
            }
            else
            {
                MessageBox.Show("Task Failed");
            }

            ReadData();

        }

        private void DeleteContactButton_Click(object sender, EventArgs e)
        {
            int id = UID;
            if (Database.DeleteContact(id))
            {
                MessageBox.Show("Contact Deleted");
                ReadData();
            }
        }

        private void AddNewContactButton_Click(object sender, EventArgs e)
        {
             string Name = FullNameTextBox.Text;
             string Email = EmailTextBox.Text;
             string PhoneNumber = PhoneNumberTextBox.Text;
             string Address = AddressTextBox.Text;
             Database.AddNewContact(Name,Email,PhoneNumber, Address);
             MessageBox.Show("New Contact Added");
        }
        public void ReadData()
        {
            DataTable dt = new DataTable();
            dt = Database.ReadData();
            dataGridView1.Rows.Clear();

            foreach (DataRow row in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = row[0].ToString();
                dataGridView1.Rows[n].Cells[1].Value = row[1].ToString();
                dataGridView1.Rows[n].Cells[2].Value = row[2].ToString();
                dataGridView1.Rows[n].Cells[3].Value = row[3].ToString();
                dataGridView1.Rows[n].Cells[4].Value = row[4].ToString();
            }


        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           if(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                UID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ID"].FormattedValue.ToString());
                Uname= FullNameTextBox.Text = dataGridView1.Rows[e.RowIndex].Cells["Name"].FormattedValue.ToString();
                UEmail = EmailTextBox.Text = dataGridView1.Rows[e.RowIndex].Cells["Email"].FormattedValue.ToString();
                UPhoneNumber = PhoneNumberTextBox.Text = dataGridView1.Rows[e.RowIndex].Cells["PhoneNumber"].FormattedValue.ToString();
                UAddress = AddressTextBox.Text = dataGridView1.Rows[e.RowIndex].Cells["Address"].FormattedValue.ToString();
            }
        }
    }
}
