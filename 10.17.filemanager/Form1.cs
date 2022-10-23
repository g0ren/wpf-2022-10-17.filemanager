namespace _10._17.filemanager
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static DriveInfo[] drvList = DriveInfo.GetDrives();
        public static Stack<string> paths=new Stack<string>();

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach(DriveInfo drv in drvList)
            {
                listBox1.Items.Add($"{drv.Name} ({drv.DriveType})");
            }
        }

        private void populateList()
        {
            listBox2.Items.Clear();
            addressBar.Text = paths.Peek();
            foreach (string file in Directory.GetDirectories(addressBar.Text))
            {
                listBox2.Items.Add($"{file}");
            }
            foreach (string file in Directory.GetFiles(addressBar.Text))
            {
                listBox2.Items.Add(file);
            }
        }

        private void OpenDisk(object sender, EventArgs e)
        {
            if(addressBar.Text != null &&
                addressBar.Text != String.Empty)
            {
                paths.Push(addressBar.Text);
            }
            paths.Push($"{drvList[listBox1.SelectedIndex].Name}");
            populateList();
        }

        private void OpenDirectory(object sender, MouseEventArgs e)
        {
            if(listBox2.SelectedItem == null)
            {
                return;
            }
            string path = listBox2.SelectedItem.ToString();
            if (Directory.Exists(path))
            {
                if (addressBar.Text != null &&
                addressBar.Text != String.Empty)
                {
                    paths.Push(addressBar.Text);
                }
                paths.Push(path);
                populateList();
            }
        }

        private void Back(object sender, EventArgs e)
        {
            addressBar.Text = paths.Pop();
            populateList();
        }

        private void Up(object sender, EventArgs e)
        {
            int slashPosition = addressBar.Text.LastIndexOf("\\");
            if (slashPosition != -1)
            {
                paths.Push(addressBar.Text.Substring(0, slashPosition +1));
                populateList();
            }
        }
    }
}