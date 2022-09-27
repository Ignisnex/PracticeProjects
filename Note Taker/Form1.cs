using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Note_Taker
{
    public partial class Form1 : Form


    {
        static DataTable dt;
        static readonly string path = System.IO.Path.Combine(Environment.GetFolderPath(
                          Environment.SpecialFolder.MyDoc‌​uments), "NoteTaker", "SavedNotes.xml");
        static readonly string dirPath = System.IO.Path.Combine(Environment.GetFolderPath(
                          Environment.SpecialFolder.MyDoc‌​uments), "NoteTaker");
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            /*
             * On form load, establish the data items we'll need.
             * Initialize the DataTable, and create the columns we need.
             * Check if there is a save file.
             * Refresh the screen so the user gets an updated look of the saved file.
             */ 
            dt = new DataTable();
            dt.TableName = "Notes";
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Note", typeof(string));
            getSaveFile();
            refresh();
        }

        static void getSaveFile()
        {
            /*
             * Check if there is a save file.
             * If there isn't, create the NoteTaker folder, and SavedNotes.xml
             * If there is, dump the data in our DataTable, and load the file instead
             */
            if (!System.IO.File.Exists(path))
            {
                System.IO.Directory.CreateDirectory(dirPath);
                var filePath = System.IO.File.Create(path);
                filePath.Close();
                dt.WriteXml(path, XmlWriteMode.WriteSchema);
            }
            else
            {
                dt.Rows.Clear();
                dt.ReadXml(path);
            }
        }

        private void savBtn_Click(object sender, EventArgs e)
        {
            /*
             * Saves all the notes when the Save Changes button is clicked
             * Ensures no Out of Bounds errors will happen, right off the bat.
             * If an index is selected in the Note List, overwrite the data at that
             * index in the DataTable, otherwise add a new row to the DataTable.
             * Save entire DataTable to SavedNotes.xml, and refresh the Notes List.
             */
            int index = noteList.SelectedIndex;
            if (index > -1)
            {
                dt.Rows[index]["Name"] = noteName.Text;
                dt.Rows[index]["Note"] = noteBox.Text;
            }
            else
            {
                dt.Rows.Add(noteName.Text, noteBox.Text);
            }
            dt.WriteXml(path, XmlWriteMode.WriteSchema);
            refresh();

        }

        public void refresh() 
        {
            /*
             * Clears the DataTable, and pulls the data from SavedNotes.xml,
             * then clears the Notes List and populates it again with the DataTable.
             */
            dt.Rows.Clear();
            dt.ReadXml(path);
            noteList.Items.Clear();
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                noteList.Items.Add(dt.Rows[i].ItemArray[0].ToString());
            }
        }

        private void noteList_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*
             * Populates the Note Name and Note boxes with the DataTable contents
             * at a selected index of the Notes List
             */
            int index = noteList.SelectedIndex;
            if (index > -1)
            {
                noteName.Text = dt.Rows[index].ItemArray[0].ToString();
                noteBox.Text = dt.Rows[index].ItemArray[1].ToString();
            }
        }

        private void delBtn_Click(object sender, EventArgs e)
        {
            /*
             * Removes the DataTable row at the index selected in the Notes List.
             * Ensures no Out of Bounds errors, then removes at the specified index.
             * Writes updated table to SavedNotes.xml, then refreshes Notes List.
             */
            int index = noteList.SelectedIndex;
            if (index > -1)
            {
                dt.Rows[index].Delete();
                dt.WriteXml(path, XmlWriteMode.WriteSchema);
            }
            refresh();
        }

        private void newBtn_Click(object sender, EventArgs e)
        {
            /*
             * Clears selected index from Notes List so saved note will create new row.
             */
            noteList.ClearSelected();
            noteBox.Clear();
            noteName.Clear();
        }
    }
}
