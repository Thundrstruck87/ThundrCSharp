// This program utilizes an already existing SQL Database and Windows Form //


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient; // This needs to be added for SQL Integration
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vinyl_Catalog
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // ----- SQL Database Connection ----- //

            string connectionString;
            SqlConnection cnn;
            connectionString = @"Data Source=[Server];Initial Catalog=catalog;User ID=sa;Password=[Password]";
            cnn = new SqlConnection(connectionString);
            cnn.Open();

            // ----- Commands to show tables ----- //

            SqlCommand command;
            SqlDataReader dataReader;
            String sql, Output = "";

            sql = "Select artist,album,year from vinyl";

            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                Output = Output + dataReader.GetValue(0) + " - " + dataReader.GetValue(1) + " - " + dataReader.GetValue(2) + "\n"; // ----- this section displays the output ----- //
                // ----- dataReader.GetValue(0) is the first column, dataReader.GetValue(1) is the second column, dataReader.GetValue(2) is the third column. First position index to 0 ----- //
            }
            MessageBox.Show(Output);

            dataReader.Close();
            command.Dispose();
            cnn.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'catalogDataSet.vinyl' table. You can move, or remove it, as needed.
            this.vinylTableAdapter.Fill(this.catalogDataSet.vinyl);

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=[server];Initial Catalog=catalog;User ID=sa;Password=[password]");
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into vinyl(artist, album, year) values ('"+txt_artist.Text+"','"+txt_album.Text+"','"+txt_year.Text+"')", con);
            int i = cmd.ExecuteNonQuery();
            if (i != 0)
            {
                MessageBox.Show("The album has been added!");
                this.catalogDataSet.Reset();
                this.vinylTableAdapter.Fill(this.catalogDataSet.vinyl);
            }
            else
            {
                MessageBox.Show("error");
            }
            con.Close();            
        }
        
         private void remove_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=[server];Initial Catalog=catalog;User ID=sa;Password=[Password]");
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from vinyl where album =('" + remove_album.Text +"')", con);
            int i = cmd.ExecuteNonQuery();
            if (i != 0)
            {
                MessageBox.Show("The album has been removed!");
                this.catalogDataSet.Reset();
                this.vinylTableAdapter.Fill(this.catalogDataSet.vinyl);
            }
            else
            {
                MessageBox.Show("Sorry, please try again");
            }
            con.Close();
        }
    }
}
