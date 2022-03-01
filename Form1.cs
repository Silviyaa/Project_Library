using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Project_Library
{
    public partial class Form1 : Form
    {
        Bitmap bitmap;
        //static string server = "localhost";
        //static string database = "library";
        //static string username = "root";
        //static string password = "";
        //static string constring = "SERVER=" + server + ";"  + "DATABASE=" + database + ";" + "UID=" + username + ";"+ "password=" + password + ";";
        static string constring = "server = localhost; user id = root; persistsecurityinfo=True;database=library";
        MySqlConnection sqlConn = new MySqlConnection();
        MySqlCommand sqlCmd = new MySqlCommand();
        DataTable sqlDt = new DataTable();
        String sqlQuery;
        MySqlDataAdapter DtA = new MySqlDataAdapter();

        DataSet DS = new DataSet();

        String servicer = "localhost";
        String username = "root";
        String password = "";
        String database = "library";
        private MySqlDataReader sqlRd;

        public Form1()
        {
            InitializeComponent();
        }

        private void upLoadData()
        {
          
           sqlConn.ConnectionString =  "server = localhost; user id = root; persistsecurityinfo = True; database = library";
            sqlConn.Open();
            sqlCmd.Connection = sqlConn;
            sqlCmd.CommandText = "SELECT b.name_book, a.name_author ,b.year_issue ,p.name_publisher ,g.genre,br.`status`FROM book AS b JOIN author as a ON b.author_id = a.author_id JOIN publisher as p ON b.publisher_id = p.publisher_id JOIN genre as g ON b.genre_id = g.genre_id JOIN borrow as br ON b.book_id = br.book_id ";
            sqlRd = sqlCmd.ExecuteReader();
            sqlDt.Load(sqlRd);
            sqlRd.Close();
            sqlConn.Close();
            dataGridView1.DataSource = sqlDt;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            upLoadData();

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            DialogResult iExit;
            try
            {
                iExit = MessageBox.Show("Confirm if you want to exit", "My Library Project"
                    , MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (iExit == DialogResult.Yes)
                {
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Control c in panel3.Controls)
                {
                    if (c is TextBox)
                    {
                        ((TextBox)c).Clear();
                    }
                }
                textBox11.Text = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            try
            {
                int heiht = dataGridView1.Height;
                dataGridView1.Height = dataGridView1.RowCount * dataGridView1.RowTemplate.Height * 2;
                bitmap = new Bitmap(dataGridView1.Width, dataGridView1.Height);
                dataGridView1.DrawToBitmap(bitmap, new Rectangle(0, 0, dataGridView1.Width, dataGridView1.Height));
                printPreviewDialog1.PrintPreviewControl.Zoom = 1;
                printPreviewDialog1.ShowDialog();
                dataGridView1.Height = heiht;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void printDocument1_PrintPage_1(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                e.Graphics.DrawImage(bitmap, 0, 0);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonAddNew_Click(object sender, EventArgs e)
        {
            sqlConn.ConnectionString = "server = localhost; user id = root; persistsecurityinfo = True; database = library";

            try
            {
                sqlConn.ConnectionString = constring;
                sqlConn.Open();
                sqlCmd.CommandType = CommandType.Text;

                sqlCmd.CommandText = "INSERT INTO book (name_book,year_issue)" + "values(" + textBox13.Text + ", " + textBox12.Text + ")"+
                            "INSERT INTO author(name_author)" + "values('" + textBox14.Text + "')" +
                            "INSERT INTO publisher(name_publisher)" + "values('" + textBox15.Text + "')" +
                            "INSERT INTO genre(genre)" + "values(" + textBox16.Text + ")" +
                            "INSERT INTO borrow(status)" + "values(" + textBox17.Text + ")";
                sqlCmd.ExecuteNonQueryAsync();
                //sqlCmd = new MySqlCommand(sqlQuery, sqlConn);
                //sqlRd = sqlCmd.ExecuteReader();

                sqlConn.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlConn.Close();
            }
            upLoadData();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {

        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {

        }
    }
}
