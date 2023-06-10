using MPI_Thu_Thap.BUS;

namespace MPI_Thu_Thap
{
    public partial class Form1 : Form
    {
        Thread process1Thread;
        Thread process2Thread;

        int pageNumber;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cbbLimit.Text = "5";
            cbbLimit.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        
        private void btnStart_Click(object sender, EventArgs e)
        {
            pageNumber = int.Parse(cbbLimit.Text);
            lblStatus.Text = "Đã kết nối thành công.";
            btnStart.Text = "Processing...";
            btnStart.Enabled = false;
            //Khởi động process 1 - Chạy xuôi
            process1Thread = new Thread(processTask1);
            process1Thread.Start();

            //Khởi động process 2 - Chạy ngược
            //process2Thread = new Thread(processTask2);
            //process2Thread.Start();
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            getData data = new getData(pageNumber);
            data.download();            
        }

        private void backgroundWorker2_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            getDataRev dataRev = new getDataRev(pageNumber);
            dataRev.download();
        }
        void processTask1()
        {
            backgroundWorker1.RunWorkerAsync();
        }

        void processTask2()
        {
            backgroundWorker2.RunWorkerAsync();
        }
        
    }
}